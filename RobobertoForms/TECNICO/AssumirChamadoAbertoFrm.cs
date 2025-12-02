//using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobobertoForms
{
    public partial class AssumirChamadoAbertoFrm : Form
    {
        //   private HubConnection _hub;
        private readonly JObject _chamadoObj;
        private readonly string _token;

        private readonly string BASE = "https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net";

        private readonly IPanelHost _parent;

        public AssumirChamadoAbertoFrm()
        {
            InitializeComponent();
        }

        // construtor correto — recebe o JObject, o token e a instância do pai
        public AssumirChamadoAbertoFrm(JObject chamadoObj, string token, IPanelHost parent)
        {
            InitializeComponent();
            _chamadoObj = chamadoObj ?? throw new ArgumentNullException(nameof(chamadoObj));
            _token = token;
            _parent = parent;
            this.Load += ChatChamadoFrm_Load;
        }


        private async void ChatChamadoFrm_Load(object sender, EventArgs e)
        {

            txtTitulo.Text = (string)_chamadoObj["titulo"] ?? string.Empty;
            txtDescricao.Text = (string)_chamadoObj["descricao"] ?? string.Empty;
            txtSugestao.Text = (string)_chamadoObj["sugestaoGemini"] ?? string.Empty;
            txtAutor.Text = (string)_chamadoObj["autor"]?["nome"] ?? string.Empty;

            var tecnicoNode = _chamadoObj?["tecnico"];

            bool tecnicoEhNulo = tecnicoNode == null || tecnicoNode.Type == JTokenType.Null;

            if (tecnicoEhNulo)
            {
                btnAssumirChamado.Visible = true;
                btnAssumirChamado.Enabled = true;
            }
            else
            {
                // já assumido por outro técnico → botão fica desabilitado
                btnAssumirChamado.Visible = true;
                btnAssumirChamado.Enabled = false;
                btnAssumirChamado.Text = "Chamado já assumido";
            }

            //
            //          string chamadoId = _chamadoObj["id"].ToString();
            //
            //          // Conecta no HUB
            //          _hub = new HubConnectionBuilder()
            //              .WithUrl($"{BASE}/hubs/chat", options =>
            //              {
            //                  options.AccessTokenProvider = () => Task.FromResult(_token);
            //              })
            //              .WithAutomaticReconnect()
            //              .Build();
            //
            //          // Receber mensagens -> método do Hub: ReceberMensagem(chamadoId, mensagemDto)
            //          _hub.On<Guid, JObject>("ReceberMensagem", (id, msg) =>
            //          {
            //              if (id.ToString() != chamadoId) return;
            //
            //              string texto = msg["texto"]?.ToString();
            //
            //              AddMessage(texto, incoming: true);
            //          });
            //
            //          await _hub.StartAsync();
            //
            //          // Entrar no grupo do chamado
            //          await _hub.InvokeAsync("EntrarNoGrupoDoChamado", Guid.Parse(chamadoId));
            //
        }
        private void SetBackColorDegrade(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle rect = new Rectangle(0, 0, Width, Height);

            // Cor base roxo = 192,0,192
            Brush br = new LinearGradientBrush(
                rect,
                Color.FromArgb(192, 0, 192),   // cor inicial
                Color.FromArgb(120, 0, 120),   // cor final (um roxo mais escuro)
                90f                             // ângulo do degradê
            );

            g.FillRectangle(br, rect);
        }

        private void ChatChamadoTecnicoFrm_Paint(object sender, PaintEventArgs e)
        {
            SetBackColorDegrade(sender, e);
        }

        private async void btnVoltar_Click(object sender, EventArgs e)
        {
            if (_parent != null)
            {
                await _parent.RestaurarPanelAnteriorAsync();
            }

            this.Dispose();
        }
        private async void btnAssumirChamado_Click(object sender, EventArgs e)
        {
            try
            {
                if (_chamadoObj == null)
                {
                    MessageBox.Show("Chamado não carregado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var idChamado = _chamadoObj["id"]?.ToString();
                if (string.IsNullOrWhiteSpace(idChamado))
                {
                    MessageBox.Show("ID do chamado inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Evita reentrância
                btnAssumirChamado.Enabled = false;

                var confirm = MessageBox.Show("Deseja assumir este chamado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes)
                {
                    btnAssumirChamado.Enabled = true;
                    return;
                }

                using var http = new HttpClient();
                http.BaseAddress = new Uri(BASE);
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"Chamados/{idChamado}/acatar-chamado")
                {
                    Content = new StringContent("{}", Encoding.UTF8, "application/json") // se API não aceitar body, remova esta linha
                };

                var resp = await http.SendAsync(request);

                if (!resp.IsSuccessStatusCode)
                {
                    var corpo = await resp.Content.ReadAsStringAsync();
                    MessageBox.Show($"Falha ao assumir chamado: {resp.StatusCode}\n{corpo}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnAssumirChamado.Enabled = true;
                    return;
                }

                // se a API retornar o chamado atualizado como JSON, mescla no _chamadoObj
                string json = await resp.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(json) && json.TrimStart().StartsWith("{"))
                {
                    try
                    {
                        var novoObj = JObject.Parse(json);
                        _chamadoObj.Merge(novoObj, new JsonMergeSettings
                        {
                            MergeArrayHandling = MergeArrayHandling.Replace,
                            MergeNullValueHandling = MergeNullValueHandling.Merge
                        });
                    }
                    catch
                    {
                        // ignora parse se não for JSON válido
                    }
                }

                // Atualiza UI local: botão e campo tecnico/autor exibidos
                btnAssumirChamado.Enabled = false;
                btnAssumirChamado.Text = "Assumido";

                // Atualiza txtAutor/txtTecnico se existir
                txtAutor.Text = (string)_chamadoObj["autor"]?["nome"] ?? string.Empty;
                // se tiver txtTecnico:
                var nomeTecnico = (string)_chamadoObj["tecnico"]?["nome"];
                if (!string.IsNullOrWhiteSpace(nomeTecnico))
                {
                    // atualiza um controle txtTecnico se existir
                    if (this.Controls.ContainsKey("txtTecnico"))
                    {
                        var ctrl = this.Controls["txtTecnico"] as TextBox;
                        if (ctrl != null) ctrl.Text = nomeTecnico;
                    }
                }

                MessageBox.Show("Chamado assumido com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // pede pro parent restaurar e recarregar a lista (assíncrono)
                if (_parent != null)
                {
                    try
                    {
                        await _parent.RestaurarPanelAnteriorAsync();
                    }
                    catch
                    {
                        // ignora falhas de recarga
                    }
                }

                this.Dispose();
            }
            catch (Exception ex)
            {
                btnAssumirChamado.Enabled = true;
                MessageBox.Show("Erro ao assumir chamado: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtTitulo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescricao_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSugestao_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtAutor_TextChanged(object sender, EventArgs e)
        {

        }



        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            SetBackColorDegrade(sender, e);
        }



        private void txtMensagem_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddMessage(string texto, bool incoming)
        {
            //      if (pnlChat.InvokeRequired)
            //    {
            //        pnlChat.Invoke(new Action(() => AddMessage(texto, incoming)));
            //        return;
            //    }
            //
            //    Label lbl = new Label();
            //    lbl.AutoSize = true;
            //    lbl.MaximumSize = new Size(pnlChat.Width - 40, 0);
            //    lbl.Padding = new Padding(8);
            //    lbl.Text = texto;
            //    lbl.BackColor = incoming ? Color.LightGray : Color.MediumPurple;
            //    lbl.ForeColor = Color.Black;
            //    lbl.Margin = new Padding(5);
            //
            //    Panel bubble = new Panel();
            //    bubble.AutoSize = true;
            //    bubble.MaximumSize = new Size(pnlChat.Width - 20, 0);
            //    bubble.Controls.Add(lbl);
            //    bubble.Dock = DockStyle.Top;
            //
            //    pnlChat.Controls.Add(bubble);
            //    pnlChat.ScrollControlIntoView(bubble);
        }

        private async void ChatChamadoFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //    if (_hub != null)
            //    {
            //        try { await _hub.StopAsync(); } catch { }
            //        try { await _hub.DisposeAsync(); } catch { }
            //    }
        }


    }
}
