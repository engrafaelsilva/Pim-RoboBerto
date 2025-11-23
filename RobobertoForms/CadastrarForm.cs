using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobobertoForms
{
    public partial class CadastrarFrm : Form
    {
        public CadastrarFrm()
        {
            InitializeComponent();
        }

        private void CadastrarFrm_Load(object sender, EventArgs e)
        {

        }

        private void lblConfirmacaoSenha_Click(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            txtSenha.PasswordChar = '*';
        }

        private void txtConfirmacaoSenha_TextChanged(object sender, EventArgs e)
        {
            txtConfirmacaoSenha.PasswordChar = '*';
        }

        private async void btnCadastrar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string email = txtEmail.Text.Trim();
            string telefone = txtTelefone.Text.Trim();
            string senha = txtSenha.Text.Trim();
            string confirmacaoSenha = txtConfirmacaoSenha.Text.Trim();

            // Validações simples
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(telefone) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Preencha todos os campos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (senha != confirmacaoSenha)
            {
                MessageBox.Show("As senhas não conferem.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cria objeto do usuário
            var usuario = new
            {
                id = Guid.NewGuid().ToString(),
                nome = nome,
                email = email,
                telefone = telefone,
                senhaHash = senha // se sua API espera o hash, você pode aplicar hash aqui
            };

            string json = JsonSerializer.Serialize(usuario);

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://robobertoapi-e6cgbaawhxagdwg2.brazilsouth-01.azurewebsites.net/");
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("Usuario", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Usuário cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoginFrm login = new LoginFrm();
                        login.Show();

                        // Fecha o formulário de cadastro
                        this.Close();
                    }
                    else
                    {
                        string respJson = await response.Content.ReadAsStringAsync();

                        try
                        {
                            // Desserializa o JSON de erro
                            var errorObj = JsonSerializer.Deserialize<Dictionary<string, object>>(respJson);

                            if (errorObj != null && errorObj.ContainsKey("errors"))
                            {
                                var errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorObj["errors"].ToString());

                                if (errors != null)
                                {
                                    string mensagens = string.Join(Environment.NewLine,
                                        errors.SelectMany(e => e.Value.Select(v => $"{e.Key}: {v}"))
                                    );

                                    MessageBox.Show(mensagens, "Erro de validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Erro desconhecido:\n" + respJson, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Erro desconhecido:\n" + respJson, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao conectar na API:\n{ex.Message}", "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }
    }
}
