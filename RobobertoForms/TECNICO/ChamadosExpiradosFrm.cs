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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobobertoForms
{
    public partial class ChamadosExpiradosFrm : Form, IPanelHost
    {

        private static readonly HttpClient client = new HttpClient();
        private string _token;
        private Control[] _previousPanelControls = Array.Empty<Control>();
        public ChamadosExpiradosFrm()
        {
            InitializeComponent();
        }

        public ChamadosExpiradosFrm(string token)
        {
            InitializeComponent();
            _token = token;
        }
        private async void ChamadosExpiradosFrm_Load(object sender, EventArgs e)
        {
            await CarregarChamadosExpiradosAsync();
        }
        private void CarregarForm(Form frm)
        {
            // guarda snapshot dos controles atuais do panel2
            _previousPanelControls = panel1.Controls.Cast<Control>().ToArray();

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
                panel1.Controls.Add(ctrl);
                ctrl.Show();
            }

            panel1.Refresh();
            // GarantirImagemUltima();
        }


        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Imagem")
            {
                var idChamado = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(idChamado)) return;

                var chamadoObj = await BuscarChamadoPorIdAsync(idChamado);
                if (chamadoObj == null)
                {
                    MessageBox.Show("Chamado não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //abre o ChatChamadoFrm passando o JObject e a referência do pai (this)
                CarregarForm(new AssumirChamadoAbertoFrm(chamadoObj, _token, this));
            }

        }

        public async Task<JObject> BuscarChamadoPorIdAsync(string idChamado)
        {
            if (string.IsNullOrWhiteSpace(idChamado))
                return null;

            try
            {
                string url = $"https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net/Chamados/{idChamado}";

                // garante que o header Authorization esteja configurado
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var resp = await client.GetAsync(url);

                if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                if (!resp.IsSuccessStatusCode)
                    throw new Exception($"Erro ao buscar chamado por id: {resp.StatusCode}");

                string json = await resp.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(json))
                    return null;

                // a rota retorna um objeto JSON, então usamos Parse
                var obj = JObject.Parse(json);
                return obj;
            }
            catch (Exception ex)
            {
                // opcional: log em arquivo ou debug
                MessageBox.Show($"Erro ao carregar chamado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private async Task CarregarChamadosExpiradosAsync()
        {
            try
            {
                string url = "https://robobertov3-a8a4fcd8fuh4e6g8.brazilsouth-01.azurewebsites.net/Chamados/expirados";

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);

                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Erro ao carregar chamados expirados: " + response.StatusCode);
                    return;
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                // desserializa para sua classe
                var listaChamados = JsonConvert.DeserializeObject<List<ChamadoDTO>>(jsonResponse) ?? new List<ChamadoDTO>();

                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

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

                dataGridView1.DataSource = null;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = dadosParaGrid;

                // opcional: formata coluna Data se já existir
                if (dataGridView1.Columns.Contains("Data"))
                    dataGridView1.Columns["Data"].DefaultCellStyle.Format = "g";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }


        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

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
