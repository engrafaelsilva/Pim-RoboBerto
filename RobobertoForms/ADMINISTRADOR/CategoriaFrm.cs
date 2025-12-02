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
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobobertoForms
{
    public partial class CategoriaFrm : Form
    {
        private string _token;
        private string selectedCategoriaId = null;
        private string selectedCategoriaNome = null;

        public CategoriaFrm()
        {
            InitializeComponent();
        }
        public CategoriaFrm(string token)
        {
            _token = token;
            InitializeComponent();
        }

        private async void CategoriaFrm_Load(object sender, EventArgs e)
        {
            btnAtualizar.Enabled = false;
            btnDeletar.Enabled = false;
            AjustarEstiloGrid();
            await CarregarCategoriasAsync();
        }

        private void lblCadastroCategorias_Click(object sender, EventArgs e)
        {

        }

        private async void btnCadastrar_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    MessageBox.Show("Digite um nome para a categoria.");
                    return;
                }

                var categoria = new
                {
                    nome = txtNome.Text
                };

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

                    var json = System.Text.Json.JsonSerializer.Serialize(categoria);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(
                        "https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net/Categoria",
                        content
                    );

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Categoria cadastrada com sucesso!");

                        txtNome.Text = "";

                        // Recarrega o grid automaticamente
                        await CarregarCategoriasAsync();
                    }
                    else
                    {
                        var erro = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Erro ao cadastrar categoria:\n" + erro);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado: " + ex.Message);
            }
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


        private void lblSelecioneCategoria_Click(object sender, EventArgs e)
        {

        }

        private void dgvCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AjustarEstiloGrid()
        {
            dgvCategorias.EnableHeadersVisualStyles = false;

            // iguala as cores de seleção às cores normais do cabeçalho
            var chStyle = dgvCategorias.ColumnHeadersDefaultCellStyle;
            chStyle.SelectionBackColor = chStyle.BackColor;
            chStyle.SelectionForeColor = chStyle.ForeColor;
            dgvCategorias.ColumnHeadersDefaultCellStyle = chStyle;

            // limpa qualquer seleção residual e remove a célula atual
            dgvCategorias.ClearSelection();
            dgvCategorias.CurrentCell = null;

            dgvCategorias.EnableHeadersVisualStyles = false;

            dgvCategorias.BackgroundColor = Color.White;
            dgvCategorias.DefaultCellStyle.BackColor = Color.White;
            dgvCategorias.DefaultCellStyle.ForeColor = Color.Black;

            dgvCategorias.RowsDefaultCellStyle.BackColor = Color.White;
            dgvCategorias.RowsDefaultCellStyle.ForeColor = Color.Black;

            dgvCategorias.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            dgvCategorias.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230);
            dgvCategorias.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            dgvCategorias.GridColor = Color.LightGray;
        }

        private async Task CarregarCategoriasAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Adicione o token de autenticação
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                    // Faz a requisição GET
                    string url = "Categoria?paginaAtual=0&tamanho=2000";
                    HttpResponseMessage response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode(); // lança exceção se não 200

                    string json = await response.Content.ReadAsStringAsync();

                    // Desserializa o JSON para uma lista de objetos genéricos
                    var categorias = JsonSerializer.Deserialize<List<JsonElement>>(json);

                    // Cria lista de objetos anônimos
                    var listaAnonima = categorias.Select(c => new
                    {
                        Id = c.GetProperty("id").GetString(),
                        Nome = c.GetProperty("nome").GetString()
                    }).ToList();

                    // Atribui ao DataGridView
                    dgvCategorias.DataSource = listaAnonima;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar categorias: " + ex.Message);
            }

            dgvCategorias.ClearSelection();
            selectedCategoriaId = null;
            selectedCategoriaNome = null;
            btnAtualizar.Enabled = false;
            btnDeletar.Enabled = false;
        }

        private void CategoriaFrm_Paint(object sender, PaintEventArgs e)
        {
            SetBackColorDegrade(sender, e);
        }

        private void dgvCategorias_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count > 0)
            {
                var row = dgvCategorias.SelectedRows[0].DataBoundItem;
                // Quando usamos objetos anônimos, DataBoundItem é um tipo anônimo (interno).
                // A forma robusta é obter valores pelas células:
                selectedCategoriaId = dgvCategorias.SelectedRows[0].Cells["Id"].Value?.ToString();
                selectedCategoriaNome = dgvCategorias.SelectedRows[0].Cells["Nome"].Value?.ToString();

                btnAtualizar.Enabled = true;
                btnDeletar.Enabled = true;
            }
            else
            {
                selectedCategoriaId = null;
                selectedCategoriaNome = null;
                btnAtualizar.Enabled = false;
                btnDeletar.Enabled = false;
            }
        }

        private async void btnDeletar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(selectedCategoriaId))
            {
                MessageBox.Show("Selecione uma categoria antes de deletar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resp = MessageBox.Show($"Confirmar remoção da categoria '{selectedCategoriaNome}'?", "Confirmar exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp != DialogResult.Yes) return;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

                    var url = $"Categoria/{selectedCategoriaId}";
                    HttpResponseMessage response = await client.DeleteAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Categoria removida com sucesso.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CarregarCategoriasAsync();
                        dgvCategorias.ClearSelection();
                    }
                    else
                    {
                        string err = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao deletar: {response.StatusCode}\n{err}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com a API: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCategoriaId))
            {
                MessageBox.Show("Selecione uma categoria antes de atualizar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pede novo nome (preenche com o nome atual)
            string novoNome = Microsoft.VisualBasic.Interaction.InputBox("Digite o novo nome da categoria:", "Atualizar Categoria", selectedCategoriaNome);
            if (string.IsNullOrWhiteSpace(novoNome)) return; // cancelou ou vazio

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

                    var payload = new { id = selectedCategoriaId, nome = novoNome };
                    string json = System.Text.Json.JsonSerializer.Serialize(payload);

                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    var url = $"Categoria/{selectedCategoriaId}";

                    HttpResponseMessage response = await client.PutAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Categoria atualizada com sucesso.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CarregarCategoriasAsync();
                        dgvCategorias.ClearSelection();
                    }
                    else
                    {
                        string err = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao atualizar: {response.StatusCode}\n{err}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com a API: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
