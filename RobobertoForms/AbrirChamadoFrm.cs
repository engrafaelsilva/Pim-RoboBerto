using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobobertoForms
{
    public partial class AbrirChamadoFrm : Form
    {
        private readonly string _token;
        private string _ultimoChamadoId;
        public AbrirChamadoFrm(string token)
        {
            _token = token;

            InitializeComponent();
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
        private async void AbrirChamadoFrm_Load(object sender, EventArgs e)
        {
            lblDescricaoBotoes.Visible = false;
            btnNaoResolveu.Visible = false;
            btnResolveu.Visible = false;
            txtSugestaoGemini.Visible = false;
            CarregarPrioridades();
            await CarregarCategoriasAsync();
        }

        private void lblCategoria_Click(object sender, EventArgs e)
        {

        }

        private async void btnGerarSugestao_Click(object sender, EventArgs e)
        {
            btnGerarSugestao.Enabled = false;
            await EnviarChamadoAsync();
        }

        private void txtDescricao_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbmPrioridade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbmCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private async Task CarregarCategoriasAsync()
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

                var url = "https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net/Categoria?paginaAtual=0&tamanho=2000";
                var json = await client.GetStringAsync(url);

                var lista = System.Text.Json.JsonSerializer
                    .Deserialize<List<Dictionary<string, string>>>(json)
                    .Select(x => new { Id = x["id"], Nome = x["nome"] })
                    .ToList();

                cbmCategoria.DataSource = lista;
                cbmCategoria.DisplayMember = "Nome";
                cbmCategoria.ValueMember = "Id";
                cbmCategoria.SelectedIndex = -1;
            }
        }

        private void CarregarPrioridades()
        {
            // Cria uma lista de objetos anônimos com Nome e Valor
            var lista = Enum.GetValues(typeof(EPrioridade))
                            .Cast<EPrioridade>()
                            .Select(p => new { Nome = p.ToString(), Valor = (int)p })
                            .ToList();

            // Configura o ComboBox
            cbmPrioridade.DataSource = lista;
            cbmPrioridade.DisplayMember = "Nome";   // o que aparece no ComboBox
            cbmPrioridade.ValueMember = "Valor";    // valor interno
            cbmPrioridade.SelectedIndex = -1;
        }

        private void txtTitulo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSugestaoGemini_TextChanged(object sender, EventArgs e)
        {

        }
        private async Task EnviarChamadoAsync()
        {
            if (!ValidarCampos())
                return;

            // Cria o objeto para enviar
            var chamado = new
            {
                categoria = new
                {
                    nome = ((dynamic)cbmCategoria.SelectedItem).Nome
                },
                prioridade = (int)((dynamic)cbmPrioridade.SelectedItem).Valor,
                titulo = txtTitulo.Text,
                descricao = txtDescricao.Text
            };

            var json = System.Text.Json.JsonSerializer.Serialize(chamado);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

                // POST para a API
                var response = await client.PostAsync(
                    "https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net//Chamados",
                    content
                );

                if (response.IsSuccessStatusCode)
                {
                    // Lê a resposta como string
                    var responseJson = await response.Content.ReadAsStringAsync();

                    var responseObj = System.Text.Json.JsonSerializer
     .Deserialize<Dictionary<string, object>>(responseJson);

                    // Armazena o ID do chamado
                    if (responseObj.ContainsKey("id"))
                    {
                        _ultimoChamadoId = responseObj["id"].ToString();
                    }

                    // Exibe sugestão da IA
                    if (responseObj.ContainsKey("sugestaoGemini"))
                    {
                        txtSugestaoGemini.Text = responseObj["sugestaoGemini"].ToString();
                    }

                    txtSugestaoGemini.Visible = true;
                    txtTitulo.ReadOnly = true;
                    txtDescricao.ReadOnly = true;
                    lblDescricaoBotoes.Visible = true;
                    btnNaoResolveu.Visible = true;
                    btnResolveu.Visible = true;
                   

                    MessageBox.Show(
                        "✨ *Chamado cadastrado com sucesso!* ✨\n\n" +
                        "📌 Uma sugestão automática foi exibida ao lado direito.\n" +
                        "👉 Clique em **Sim** caso a sugestão tenha resolvido seu problema.\n" +
                        "👉 Clique em **Não** caso não tenha sido suficiente — neste caso, seu chamado será encaminhado para um técnico.\n\n" +
                        "Obrigado por utilizar o sistema! 💜",
                        "✅ Chamado Registrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                }
                else
                {
                    var erro = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Erro ao enviar chamado: " + erro);
                    btnGerarSugestao.Enabled = true;

                }
            }
        }

        private async void btnResolveu_Click(object sender, EventArgs e)
        {
            btnResolveu.Enabled=false;
            await EnviarResolvidoAsync(true);
            LimparCamposELiberarTextBox();
            btnResolveu.Enabled = true;

        }
        private async void btnNaoResolveu_Click(object sender, EventArgs e)
        {
            btnNaoResolveu.Enabled = false;
            await EnviarResolvidoAsync(false);
            LimparCamposELiberarTextBox();
            btnNaoResolveu.Enabled = true;


        }
        private void AbrirChamadoFrm_Paint(object sender, PaintEventArgs e)
        {
            SetBackColorDegrade(sender, e);
        }

        private void lblDescricaoBotoes_Click(object sender, EventArgs e)
        {

        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private async Task EnviarResolvidoAsync(bool resolveu)
        {
            if (string.IsNullOrEmpty(_ultimoChamadoId))
            {
                MessageBox.Show("Nenhum chamado foi cadastrado ainda.");
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

                var json = System.Text.Json.JsonSerializer.Serialize(resolveu);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var url = $"https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net/Chamados/{_ultimoChamadoId}/alterar-resolveu-sugestao";

                var response = await client.PatchAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    if (resolveu)
                    {
                        MessageBox.Show(
                            "✨ Que ótimo! O problema foi resolvido com sucesso.",
                            "Sucesso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            "🔧 Ok! Seu chamado será encaminhado para um técnico.",
                            "Encaminhado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao enviar atualização: " + await response.Content.ReadAsStringAsync());
                }
            }
        }

        private void LimparCamposELiberarTextBox()
        {
            
       
            txtDescricao.ReadOnly = false;
            txtTitulo.ReadOnly = false;
            btnGerarSugestao.Enabled = true;
            lblDescricaoBotoes.Visible = false;
            txtSugestaoGemini.Visible = false;
            btnNaoResolveu.Visible = false;
            btnResolveu.Visible = false;

            txtTitulo.Text = "";
            txtDescricao.Text = "";
            txtSugestaoGemini.Text = "";

            cbmCategoria.SelectedIndex = -1;
            cbmPrioridade.SelectedIndex = -1;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("O título é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                MessageBox.Show("A descrição é obrigatória.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbmCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione uma categoria.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbmPrioridade.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione uma prioridade.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true; // tudo OK
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {   
            this.Close();
        }

        public enum EPrioridade
        {
            Normal = 1,
            Urgente = 2,
            Baixa = 3
        }

    }
}
