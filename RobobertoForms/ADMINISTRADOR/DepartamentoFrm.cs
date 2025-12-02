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
    public partial class DepartamentoFrm : Form
    {
        private string _token;
        private string selectedDepartamentoId = null;
        private string selectedDepartamentoNome = null;

        public DepartamentoFrm()
        {
            InitializeComponent();
        }
        public DepartamentoFrm(string token)
        {
            _token = token;
            InitializeComponent();
        }

        private async void DepartamentoFrm_Load(object sender, EventArgs e)
        {
            btnAtualizar.Enabled = false;
            btnDeletar.Enabled = false;
            AjustarEstiloGrid();
            await CarregarDepartamentosAsync();
        }

        private void lblCadastroDepartamentos_Click(object sender, EventArgs e)
        {

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

        private async void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    MessageBox.Show("Digite um nome para o departamento.");
                    return;
                }

                var departamento = new
                {
                    nome = txtNome.Text
                };

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _token);

                    var json = JsonSerializer.Serialize(departamento);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(
                        "https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net/Departamento",
                        content
                    );

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Departamento cadastrado com sucesso!");

                        txtNome.Text = string.Empty;

                        // Recarrega automaticamente
                        await CarregarDepartamentosAsync();
                    }
                    else
                    {
                        var erro = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Erro ao cadastrar departamento:\n" + erro);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado: " + ex.Message);
            }
        }

        private void lblSelecioneDepartamento_Click(object sender, EventArgs e)
        {

        }

        private void dgvDepartamentos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AjustarEstiloGrid()
        {
            dgvDepartamentos.EnableHeadersVisualStyles = false;

            // iguala as cores de seleção às cores normais do cabeçalho
            var chStyle = dgvDepartamentos.ColumnHeadersDefaultCellStyle;
            chStyle.SelectionBackColor = chStyle.BackColor;
            chStyle.SelectionForeColor = chStyle.ForeColor;
            dgvDepartamentos.ColumnHeadersDefaultCellStyle = chStyle;

            // limpa qualquer seleção residual e remove a célula atual
            dgvDepartamentos.ClearSelection();
            dgvDepartamentos.CurrentCell = null;

            dgvDepartamentos.EnableHeadersVisualStyles = false;

            dgvDepartamentos.BackgroundColor = Color.White;
            dgvDepartamentos.DefaultCellStyle.BackColor = Color.White;
            dgvDepartamentos.DefaultCellStyle.ForeColor = Color.Black;

            dgvDepartamentos.RowsDefaultCellStyle.BackColor = Color.White;
            dgvDepartamentos.RowsDefaultCellStyle.ForeColor = Color.Black;

            dgvDepartamentos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            dgvDepartamentos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230);
            dgvDepartamentos.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            dgvDepartamentos.GridColor = Color.LightGray;
        }

        private async Task CarregarDepartamentosAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // token de autenticação
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                    // Faz a requisição GET para Departamento
                    string url = "Departamento?paginaAtual=0&tamanho=2000";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    // Desserializa para JsonElement (sem precisar criar DTO)
                    var departamentos = JsonSerializer.Deserialize<List<JsonElement>>(json);

                    var listaAnonima = departamentos.Select(d => new
                    {
                        Id = d.GetProperty("id").GetString(),
                        Nome = d.GetProperty("nome").GetString()
                    }).ToList();

                    // Atribui ao DataGridView de departamentos
                    dgvDepartamentos.DataSource = listaAnonima;

                    // opcional: ajusta títulos das colunas (se quiser)
                    if (dgvDepartamentos.Columns["Id"] != null) dgvDepartamentos.Columns["Id"].HeaderText = "ID";
                    if (dgvDepartamentos.Columns["Nome"] != null) dgvDepartamentos.Columns["Nome"].HeaderText = "Departamento";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar departamentos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // limpa seleção e reseta seleção atual
            dgvDepartamentos.ClearSelection();
            selectedDepartamentoId = null;
            selectedDepartamentoNome = null;
        }

        private void DepartamentoFrm_Paint(object sender, PaintEventArgs e)
        {
            SetBackColorDegrade(sender, e);
        }

        private void dgvDepartamentos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDepartamentos.SelectedRows.Count > 0)
            {
                // Obtém a linha selecionada
                selectedDepartamentoId = dgvDepartamentos.SelectedRows[0].Cells["Id"].Value?.ToString();
                selectedDepartamentoNome = dgvDepartamentos.SelectedRows[0].Cells["Nome"].Value?.ToString();

                btnAtualizar.Enabled = true;
                btnDeletar.Enabled = true;
            }
            else
            {
                selectedDepartamentoId = null;
                selectedDepartamentoNome = null;

                btnAtualizar.Enabled = false;
                btnDeletar.Enabled = false;
            }
        }

        private async void btnDeletar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedDepartamentoId))
            {
                MessageBox.Show("Selecione um departamento antes de deletar.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resp = MessageBox.Show(
                $"Confirmar remoção do departamento '{selectedDepartamentoNome}'?",
                "Confirmar exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resp != DialogResult.Yes)
                return;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _token);

                    var url = $"Departamento/{selectedDepartamentoId}";
                    HttpResponseMessage response = await client.DeleteAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Departamento removido com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        await CarregarDepartamentosAsync();
                        dgvDepartamentos.ClearSelection();
                    }
                    else
                    {
                        string err = await response.Content.ReadAsStringAsync();
                        MessageBox.Show(
                            $"Erro ao deletar: {response.StatusCode}\n{err}",
                            "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com a API: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedDepartamentoId))
            {
                MessageBox.Show("Selecione um departamento antes de atualizar.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Caixa de edição preenchida com o nome atual
            string novoNome = Microsoft.VisualBasic.Interaction.InputBox(
                "Digite o novo nome do departamento:",
                "Atualizar Departamento",
                selectedDepartamentoNome
            );

            if (string.IsNullOrWhiteSpace(novoNome))
                return; // usuário cancelou ou deixou vazio

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _token);

                    var payload = new { id = selectedDepartamentoId, nome = novoNome };
                    string json = JsonSerializer.Serialize(payload);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var url = $"Departamento/{selectedDepartamentoId}";

                    HttpResponseMessage response = await client.PutAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Departamento atualizado com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        await CarregarDepartamentosAsync();
                        dgvDepartamentos.ClearSelection();
                    }
                    else
                    {
                        string err = await response.Content.ReadAsStringAsync();
                        MessageBox.Show(
                            $"Erro ao atualizar: {response.StatusCode}\n{err}",
                            "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com a API: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
