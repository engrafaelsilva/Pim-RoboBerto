using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobobertoForms
{
    public partial class LoginFrm : Form
    {
        private CadastrarFrm cadastrarForm;

        public string Token { get; private set; } = string.Empty;
        public bool LoginOk { get; private set; } = false;
        public LoginFrm()
        {
            InitializeComponent();
        }


        private void LoginFrm_Load(object sender, EventArgs e)
        {

        }


        private async void btnEntrar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string senha = txtSenha.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Informe email e senha.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var loginData = new { email = email, senha = senha };
            string json = JsonSerializer.Serialize(loginData);

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://robobertoapi-e6cgbaawhxagdwg2.brazilsouth-01.azurewebsites.net/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("Autenticacao/login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string respJson = await response.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize<Dictionary<string, string>>(respJson);

                        if (result != null && result.ContainsKey("token"))
                        {
                            Token = result["token"];
                            LoginOk = true; // sinaliza sucesso
                            MessageBox.Show("Login realizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Abre o PrincipalFrm
                            PrincipalFrm principal = new PrincipalFrm(Token); // passamos o token
                            this.Hide(); // oculta o LoginFrm
                            principal.ShowDialog();
                            this.Close(); // fecha o LoginFrm quando PrincipalFrm for fechado
                        }
                        else
                        {
                            MessageBox.Show("Login realizado, mas token não retornou.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Falha no login:\n{response.StatusCode} - {response.ReasonPhrase}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao conectar na API:\n{ex.Message}", "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            txtSenha.PasswordChar = '*';

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            this.Hide();
            cadastrarForm = new CadastrarFrm();
            cadastrarForm.ShowDialog();

        }


    }
}
