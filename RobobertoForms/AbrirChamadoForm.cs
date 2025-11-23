using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobobertoForms
{
    public partial class AbrirChamadoFrm : Form
    {
        private readonly string _token;
        public AbrirChamadoFrm(string token)
        {
            _token = token;
            InitializeComponent();
        }

        private async void AbrirChamadoFrm_Load(object sender, EventArgs e)
        {
            CarregarPrioridades();
            await CarregarCategoriasAsync();
        }

        private void lblCategoria_Click(object sender, EventArgs e)
        {

        }

        private async void btnGerarSugestao_Click(object sender, EventArgs e)
        {
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
            using (HttpClient client = new HttpClient())
            {
                // Adiciona o token no cabeçalho Authorization
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

                // Chama a API
                var json = await client.GetStringAsync(
                    "https://robobertoapi-e6cgbaawhxagdwg2.brazilsouth-01.azurewebsites.net/Categoria?paginaAtual=0&tamanho=200"
                );

                // Desserializa em lista de dicionários e transforma em objetos anônimos
                var lista = System.Text.Json.JsonSerializer
                    .Deserialize<List<Dictionary<string, string>>>(json)
                    .Select(x => new { Id = x["id"], Nome = x["nome"] })
                    .ToList();

                // Popula o ComboBox
                cbmCategoria.DataSource = lista;
                cbmCategoria.DisplayMember = "Nome";  // texto exibido
                cbmCategoria.ValueMember = "Id";      // valor interno
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
        }

        private void txtTitulo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSugestaoGemini_TextChanged(object sender, EventArgs e)
        {

        }
        private async Task EnviarChamadoAsync()
        {
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
                    "https://robobertoapi-e6cgbaawhxagdwg2.brazilsouth-01.azurewebsites.net/Chamados",
                    content
                );

                if (response.IsSuccessStatusCode)
                {
                    // Lê a resposta como string
                    var responseJson = await response.Content.ReadAsStringAsync();

                    // Desserializa em objeto anônimo
                    var responseObj = System.Text.Json.JsonSerializer
                        .Deserialize<Dictionary<string, object>>(responseJson);

                    // Pega o campo sugestaoGemini da resposta
                    if (responseObj.ContainsKey("sugestaoGemini"))
                    {
                        txtSugestaoGemini.Text = responseObj["sugestaoGemini"].ToString();
                    }

                    MessageBox.Show("Chamado enviado com sucesso!");
                }
                else
                {
                    var erro = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Erro ao enviar chamado: " + erro);
                }
            }
        }

        private void btnResolveu_Click(object sender, EventArgs e)
        {

        }

        public enum EPrioridade
        {
            Normal = 1,
            Urgente = 2,
            Baixa = 3
        }

    }
}
