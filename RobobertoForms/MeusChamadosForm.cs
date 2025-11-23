using Newtonsoft.Json;
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
    public partial class MeusChamadosForm : Form
    {

        private static readonly HttpClient client = new HttpClient();
        private string _token;
        public MeusChamadosForm()
        {
            InitializeComponent();
        }

        public MeusChamadosForm(string token)
        {
            InitializeComponent();
            _token = token;
        }
        private async void MeusChamadosForm_Load(object sender, EventArgs e)
        {
            await CarregarChamadosAsync();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async Task CarregarChamadosAsync()
        {
            try
            {
                // 1. Configurar a URL e o Token
                string url = "https://robobertoapi-e6cgbaawhxagdwg2.brazilsouth-01.azurewebsites.net/Chamados/meus?paginaAtual=0&tamanho=900";

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
                    // O Grid não sabe ler "Categoria.Nome" sozinho, então criamos uma lista plana aqui:
                    var dadosParaGrid = listaChamados.Select(c => new
                    {
                        Id = c.Id,
                        Titulo = c.Titulo,
                        Categoria = c.Categoria?.Nome, // Trata nulos
                        Autor = c.Autor?.Nome,
                        Data = c.DataAbertura,
                        Prioridade = textInfo.ToTitleCase(c.Prioridade.ToString().Replace("_", " ").ToLower()),
                        Status = textInfo.ToTitleCase(c.Status.ToString().Replace("_", " ").ToLower()),
                        Descricao = c.Descricao

                    }

                    ).ToList();
                    // 6. Vincular ao DataGridView
                    dataGridView1.DataSource = null; // Limpa anterior
                    dataGridView1.DataSource = dadosParaGrid;
                }
                else
                {
                    MessageBox.Show($"Erro ao buscar dados: {response.StatusCode}");
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
