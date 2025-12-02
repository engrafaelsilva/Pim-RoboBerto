using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http; // Importante para HttpClient funcionar corretamente
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobobertoForms
{
    public partial class MeusChamadosColaboradorFrm : Form, IPanelHost
    {
        private JObject _chamadoObj;
        private string _token;
        private IPanelHost _parentHost;
        private static readonly HttpClient client = new HttpClient();
        private Control[] _previousPanelControls = Array.Empty<Control>();

        public MeusChamadosColaboradorFrm()
        {
            InitializeComponent();
        }

        // --- CORREÇÃO AQUI ---
        // Antes estava faltando "_token = token;"
        public MeusChamadosColaboradorFrm(string token)
        {
            InitializeComponent();
            _token = token; // Agora o token é salvo corretamente!
        }

        public MeusChamadosColaboradorFrm(JObject chamadoObj, string token, IPanelHost parentHost)
        {
            InitializeComponent();
            _chamadoObj = chamadoObj;
            _token = token;
            _parentHost = parentHost;
        }

        private async void MeusChamadosForm_Load(object sender, EventArgs e)
        {
            await CarregarChamadosAsync();
        }

        private void CarregarForm(Form frm)
        {
            // guarda snapshot dos controles atuais do panel2
            if (panel1.Controls.Count > 0)
            {
                _previousPanelControls = panel1.Controls.Cast<Control>().ToArray();
            }

            panel1.Controls.Clear();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            panel1.Controls.Add(frm);
            frm.Show();
        }

        public async Task RestaurarPanelAnteriorAsync()
        {
            // destrói tudo que estiver no panel2 (ex.: ChatChamadoFrm hospedado)
            foreach (Control c in panel1.Controls.Cast<Control>().ToArray())
            {
                c.Dispose();
            }
            panel1.Controls.Clear();

            // adiciona de volta os controles que estavam antes
            foreach (var ctrl in _previousPanelControls)
            {
                // Verifica se o controle ainda não foi descartado antes de adicionar
                if (ctrl != null && !ctrl.IsDisposed)
                {
                    panel1.Controls.Add(ctrl);
                    ctrl.Show();
                }
            }

            panel1.Refresh();
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Imagem")
            {
                var idChamado = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(idChamado)) return;

                var chamadoObj = await BuscarChamadoPorIdAnonimoAsync(idChamado);
                if (chamadoObj == null)
                {
                    MessageBox.Show("Chamado não encontrado nos seus chamados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // abre o ChatChamadoFrm passando o JObject e a referência do pai (this)
                CarregarForm(new ChatChamadoFrmColaborador(chamadoObj, _token, this));
            }
        }

        public async Task<JObject> BuscarChamadoPorIdAnonimoAsync(string idChamado)
        {
            // URL corrigida (V3)
            string url = "https://robobertov3-a8a4fcd8fuh4e6g8.brazilsouth-01.azurewebsites.net/Chamados/meus?paginaAtual=0&tamanho=999";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var resp = await client.GetAsync(url);

            if (!resp.IsSuccessStatusCode)
                return null;

            string json = await resp.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json))
                return null;

            var arr = JArray.Parse(json);

            // encontra o primeiro cujo "id" é igual ao idChamado (case-insensitive)
            var match = arr.FirstOrDefault(o => string.Equals((string)o["id"], idChamado, StringComparison.OrdinalIgnoreCase));

            return match as JObject;
        }

        private async Task CarregarChamadosAsync()
        {
            try
            {
                // URL corrigida (V3)
                string url = "https://robobertov3-a8a4fcd8fuh4e6g8.brazilsouth-01.azurewebsites.net/Chamados/meus?paginaAtual=0&tamanho=900";

                // Limpar headers anteriores para evitar duplicação se chamar várias vezes
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                // 2. Fazer a requisição
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                    // 3. Ler o JSON
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // 4. Converter JSON para Lista de Objetos
                    var listaChamados = JsonConvert.DeserializeObject<List<ChamadoDTO>>(jsonResponse);

                    // 5. Preparar dados para o Grid (Projection)
                    var dadosParaGrid = listaChamados.Select(c => new
                    {
                        Id = c.Id,
                        Titulo = c.Titulo,
                        Categoria = c.Categoria?.Nome,
                        Autor = c.Autor?.Nome,
                        Data = c.DataAbertura,
                        Prioridade = textInfo.ToTitleCase(c.Prioridade.ToString().Replace("_", " ").ToLower()),
                        Status = textInfo.ToTitleCase(c.Status.ToString().Replace("_", " ").ToLower()),
                        Descricao = c.Descricao
                    }).ToList();

                    // 6. Vincular ao DataGridView
                    dataGridView1.DataSource = null;
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = dadosParaGrid;
                }
                else
                {
                    MessageBox.Show($"Erro ao buscar dados: {response.StatusCode} - Verifique se o login foi realizado corretamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var idChamado = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(idChamado)) return;

            var chamadoObj = await BuscarChamadoPorIdAnonimoAsync(idChamado);
            if (chamadoObj == null)
            {
                MessageBox.Show("Chamado não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int status = (int?)chamadoObj["status"] ?? -1;
            const int SUGESTAO_GERADA = 2;

            if (status != SUGESTAO_GERADA)
            {
                CarregarForm(new ChatChamadoFrmColaborador(chamadoObj, _token, this));
                return;
            }

            // --- cria modal inline com a sugestão ---
            using (var dlg = new Form())
            {
                dlg.Text = "Sugestão gerada";
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.ClientSize = new Size(560, 340);
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;
                dlg.ShowInTaskbar = false;

                var txt = new TextBox
                {
                    Multiline = true,
                    ReadOnly = true,
                    ScrollBars = ScrollBars.Vertical,
                    Dock = DockStyle.Top,
                    Height = 260,
                    Text = (string)chamadoObj["sugestaoGemini"] ?? "(Sem sugestão)",
                    BackColor = SystemColors.Window
                };
                dlg.Controls.Add(txt);

                var pnlBtns = new FlowLayoutPanel
                {
                    Dock = DockStyle.Bottom,
                    Height = 56,
                    FlowDirection = FlowDirection.RightToLeft,
                    Padding = new Padding(8)
                };

                var btnResolveu = new Button { Text = "Resolveu", AutoSize = true, DialogResult = DialogResult.Yes, Padding = new Padding(8) };
                var btnNao = new Button { Text = "Não resolveu", AutoSize = true, DialogResult = DialogResult.No, Padding = new Padding(8) };
                var btnCancelar = new Button { Text = "Cancelar", AutoSize = true, DialogResult = DialogResult.Cancel, Padding = new Padding(8) };

                pnlBtns.Controls.Add(btnCancelar);
                pnlBtns.Controls.Add(btnNao);
                pnlBtns.Controls.Add(btnResolveu);
                dlg.Controls.Add(pnlBtns);

                dlg.AcceptButton = btnResolveu;
                dlg.CancelButton = btnCancelar;

                var dr = dlg.ShowDialog(this);

                if (dr == DialogResult.Cancel)
                {
                    CarregarForm(new ChatChamadoFrmColaborador(chamadoObj, _token, this));
                    return;
                }

                bool resolveu = dr == DialogResult.Yes;

                // --- envia PATCH ---
                try
                {
                    using var http = new HttpClient();
                    // ATUALIZADO PARA V3 (estava V2 antes)
                    string baseUrl = "https://robobertov3-a8a4fcd8fuh4e6g8.brazilsouth-01.azurewebsites.net";

                    http.BaseAddress = new Uri(baseUrl);
                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                    var req = new HttpRequestMessage(new HttpMethod("PATCH"), $"Chamados/{idChamado}/alterar-resolveu-sugestao")
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(resolveu), Encoding.UTF8, "application/json")
                    };

                    var resp = await http.SendAsync(req);

                    if (!resp.IsSuccessStatusCode)
                    {
                        var body = await resp.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao registrar resposta: {resp.StatusCode}\n{body}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Resposta registrada com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        await CarregarChamadosAsync();
                    }
                    catch
                    {
                        try { await RestaurarPanelAnteriorAsync(); } catch { }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Imagem")
                {
                    dataGridView1.Cursor = Cursors.Hand;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Detalhes do chamado";
                }
            }
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Cursor = Cursors.Default;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}