using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http; // Necessário para HttpClient
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobobertoForms
{
    public partial class ChatChamadoFrmColaborador : Form
    {
        // Removi o 'readonly' para evitar erros de compilação nos construtores vazios
        private JObject _chamadoObj;
        private string _token;
        private IPanelHost _parent;

        private FlowLayoutPanel _flowChat;
        private readonly string BASE = "https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net";

        // CONSTRUTOR 1: Vazio (padrão do Windows Forms)
        public ChatChamadoFrmColaborador()
        {
            InitializeComponent();
        }

        // CONSTRUTOR 2: Apenas Token (Cuidado: chamadoObj fica null aqui)
        public ChatChamadoFrmColaborador(string token)
        {
            InitializeComponent();
            _token = token;
        }

        // CONSTRUTOR 3: Completo (O que deve ser usado na grid)
        public ChatChamadoFrmColaborador(JObject chamadoObj, string token, IPanelHost parent)
        {
            InitializeComponent();

            // É importante definir as variáveis ANTES de chamar métodos que dependam delas
            _chamadoObj = chamadoObj;
            _token = token;
            _parent = parent;

            // Configura o painel manualmente caso o Designer falhe
            SetupChatPanel();
        }

        private void ChatChamadoFrm_Load(object sender, EventArgs e)
        {
            // --- CORREÇÃO DO ERRO NULL REFERENCE ---
            // Se _chamadoObj for nulo (veio pelo menu ou construtor errado), para aqui.
            if (_chamadoObj == null)
            {
                // Opcional: Avisar que está em modo de teste ou erro
                // MessageBox.Show("Nenhum chamado carregado.", "Aviso");
                return;
            }

            // Preenche os campos com segurança usando ?. para evitar crash se a chave não existir
            if (txtTitulo != null)
                txtTitulo.Text = (string)_chamadoObj["titulo"] ?? string.Empty;

            if (txtDescricao != null)
                txtDescricao.Text = (string)_chamadoObj["descricao"] ?? string.Empty;

            if (txtSugestao != null)
                txtSugestao.Text = (string)_chamadoObj["sugestaoGemini"] ?? string.Empty;
        }

        private void SetBackColorDegrade(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, Width, Height);

            // Cor base roxo
            using (Brush br = new LinearGradientBrush(
                rect,
                Color.FromArgb(192, 0, 192),   // cor inicial
                Color.FromArgb(120, 0, 120),   // cor final
                90f                              // ângulo
            ))
            {
                g.FillRectangle(br, rect);
            }
        }

        private void ChatChamadoFrm_Paint(object sender, PaintEventArgs e)
        {
            SetBackColorDegrade(sender, e);
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            // Verifica se tem pai para voltar, senão apenas fecha
            if (_parent != null)
            {
                _parent.RestaurarPanelAnteriorAsync();
            }
            this.Close(); // Use Close() em vez de Dispose() direto em Forms
        }

        // Eventos vazios mantidos para evitar erro no Designer se estiverem vinculados
        private void txtTitulo_TextChanged(object sender, EventArgs e) { }
        private void txtDescricao_TextChanged(object sender, EventArgs e) { }
        private void txtSugestao_TextChanged(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void pnlChat_Paint(object sender, PaintEventArgs e) { }
        private void txtMensagem_TextChanged(object sender, EventArgs e) { }

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            // Segurança: Se não tem chamado carregado, não envia
            if (_chamadoObj == null)
            {
                MessageBox.Show("Erro: Nenhum chamado vinculado a esta tela.");
                return;
            }

            string texto = txtMensagem.Text.Trim();
            if (texto == "") return;

            txtMensagem.Enabled = false;
            btnEnviar.Enabled = false;

            try
            {
                string chamadoId = _chamadoObj["id"]?.ToString();
                if (string.IsNullOrEmpty(chamadoId))
                {
                    MessageBox.Show("ID do chamado inválido.");
                    return;
                }

                string rota = $"{BASE}/Chamados/{chamadoId}/comentar";

                var payload = new { texto = texto };
                string json = JsonConvert.SerializeObject(payload);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var resp = await client.PatchAsync(rota, content);

                    if (!resp.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Erro ao enviar comentário: {resp.StatusCode}");
                    }
                    else
                    {
                        AddMessage(texto, incoming: false);
                        txtMensagem.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                if (txtMensagem != null) txtMensagem.Enabled = true;
                if (btnEnviar != null) btnEnviar.Enabled = true;
            }
        }

        private void ChatChamadoFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Limpeza se necessário
        }

        private void SetupChatPanel()
        {
            // Tenta encontrar o painel criado pelo Designer
            if (pnlChat == null)
            {
                var found = this.Controls.Find("pnlChat", true).FirstOrDefault() as Panel;
                if (found != null)
                {
                    pnlChat = found;
                }
                else
                {
                    // Fallback: cria painel se não existir no designer
                    pnlChat = new Panel
                    {
                        Name = "pnlChat_fallback",
                        Dock = DockStyle.Fill,
                        BackColor = Color.Transparent
                    };
                    // Tenta adicionar ao container principal ou splitcontainer se houver
                    this.Controls.Add(pnlChat);
                    pnlChat.BringToFront();
                }
            }

            pnlChat.Controls.Clear();

            _flowChat = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.BottomUp,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(8),
                BackColor = Color.Transparent // Importante para ver o degradê de fundo
            };

            _flowChat.SizeChanged += (s, e) =>
            {
                foreach (Control c in _flowChat.Controls)
                {
                    if (c is Panel bubble)
                    {
                        AdjustBubbleWidth(bubble);
                        if (bubble.Margin != null)
                        {
                            if (bubble.Anchor.HasFlag(AnchorStyles.Right))
                                bubble.Margin = new Padding(_flowChat.ClientSize.Width / 3, 6, 6, 6);
                            else
                                bubble.Margin = new Padding(6, 6, _flowChat.ClientSize.Width / 3, 6);
                        }
                    }
                }
            };

            pnlChat.Controls.Add(_flowChat);
        }

        private Panel CreateBubble(string autor, string texto, bool incoming)
        {
            var bubble = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                Margin = new Padding(6),
                Padding = new Padding(0),
                MaximumSize = new Size((int)(_flowChat.ClientSize.Width * 0.8), 0)
            };

            var lblAutor = new Label
            {
                AutoSize = true,
                Text = autor,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Padding = new Padding(6, 0, 6, 0),
                Margin = new Padding(0, 0, 0, 2)
            };

            var lblTexto = new Label
            {
                AutoSize = true,
                MaximumSize = new Size((int)(_flowChat.ClientSize.Width * 0.8) - 16, 0),
                Text = texto,
                Font = new Font("Segoe UI", 10F),
                Padding = new Padding(10),
                Margin = new Padding(0)
            };

            var bgColor = incoming ? Color.WhiteSmoke : Color.MediumPurple;
            var fgColor = incoming ? Color.Black : Color.White;

            var bubbleBody = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                Padding = new Padding(0),
                Margin = new Padding(0),
                BackColor = Color.Transparent
            };

            lblTexto.ForeColor = fgColor;
            bubbleBody.Controls.Add(lblTexto);

            bubbleBody.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, bubbleBody.Width, bubbleBody.Height);
                int radius = 14;

                using (var path = RoundedRect(rect, radius))
                using (var brush = new SolidBrush(bgColor))
                {
                    g.FillPath(brush, path);
                }
            };

            bubble.Controls.Add(lblAutor);
            bubble.Controls.Add(bubbleBody);

            if (!incoming)
            {
                bubble.Anchor = AnchorStyles.Right;
                lblAutor.TextAlign = ContentAlignment.TopRight;
                lblTexto.TextAlign = ContentAlignment.MiddleRight;
            }
            else
            {
                bubble.Anchor = AnchorStyles.Left;
                lblAutor.TextAlign = ContentAlignment.TopLeft;
                lblTexto.TextAlign = ContentAlignment.MiddleLeft;
            }

            bubble.Layout += (s, e) => AdjustBubbleWidth(bubble);
            return bubble;
        }

        private void AdjustBubbleWidth(Panel bubble)
        {
            if (_flowChat == null) return;

            if (bubble.Controls.Count >= 2)
            {
                var bubbleBody = bubble.Controls[1] as Panel;
                if (bubbleBody != null)
                {
                    int maxWidth = (int)(_flowChat.ClientSize.Width * 0.8);
                    bubble.MaximumSize = new Size(maxWidth, 0);

                    if (bubbleBody.Controls.Count > 0 && bubbleBody.Controls[0] is Label lblTexto)
                    {
                        lblTexto.MaximumSize = new Size(maxWidth - 20, 0);
                    }
                }
            }
            bubble.Invalidate();
        }

        private void AddMessage(string texto, bool incoming)
        {
            if (pnlChat.InvokeRequired)
            {
                pnlChat.Invoke(new Action(() => AddMessage(texto, incoming)));
                return;
            }

            string autor = incoming ? "Outro" : "Eu";
            var bubble = CreateBubble(autor, texto, incoming);

            if (incoming)
                bubble.Margin = new Padding(6, 6, _flowChat.ClientSize.Width / 3, 6);
            else
                bubble.Margin = new Padding(_flowChat.ClientSize.Width / 3, 6, 6, 6);

            _flowChat.Controls.Add(bubble);
            _flowChat.ScrollControlIntoView(bubble);
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}