using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
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
            this.AcceptButton = btnEntrar;
        }

        private void LoginFrm_Paint(object sender, PaintEventArgs e)
        {
            SetBackColorDegrade(sender, e);
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

            btnEntrar.Enabled = false;
            var loginData = new { email = email, senha = senha };
            string json = JsonSerializer.Serialize(loginData);

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net/");
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
                            LoginOk = true;

                            // remove possível prefixo "Bearer "
                            string rawToken = Token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
                                ? Token.Substring("Bearer ".Length).Trim()
                                : Token;

                            try
                            {
                                var handler = new JwtSecurityTokenHandler();

                                if (handler.CanReadToken(rawToken))
                                {
                                    var jwt = handler.ReadJwtToken(rawToken);

                                    // buscar claims de roles
                                    var roleClaims = jwt.Claims
                                        .Where(c =>
                                            c.Type == ClaimTypes.Role ||
                                            c.Type.Equals("role", StringComparison.OrdinalIgnoreCase) ||
                                            c.Type.Equals("roles", StringComparison.OrdinalIgnoreCase) ||
                                            c.Type.Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", StringComparison.OrdinalIgnoreCase))
                                        .Select(c => c.Value)
                                        .ToList();

                                    string role = roleClaims.FirstOrDefault()?.ToUpperInvariant() ?? string.Empty;

                                    this.Hide();

                                    // ADMIN
                                    if (roleClaims.Any(r => r.ToUpperInvariant().Contains("ADM") ||
                                                            r.ToUpperInvariant().Contains("ADMIN")))
                                    {
                                        var adminForm = new PainelAdministradorFrm(Token);
                                        adminForm.ShowDialog();
                                    }
                                    // TÉCNICO (novo)
                                    else if (roleClaims.Any(r => r.ToUpperInvariant().Contains("TECNICO") ||
                                                                 r.ToUpperInvariant().Contains("TÉCNICO")))
                                    {
                                        var tecnicoForm = new PainelTecnicoFrm(Token);
                                        tecnicoForm.ShowDialog();
                                    }
                                    // COLABORADOR
                                    else if (roleClaims.Any(r => r.ToUpperInvariant().Contains("COLAB") ||
                                                                 r.ToUpperInvariant().Contains("COLABORADOR")))
                                    {
                                        var colaboradorForm = new PainelColaboradorFrm(Token);
                                        colaboradorForm.ShowDialog();
                                    }
                                    else
                                    {
                                        // fallback se não reconhecer a role
                                        MessageBox.Show("Role não identificada. Abrindo painel padrão.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        var colaboradorForm = new PainelColaboradorFrm(Token);
                                        colaboradorForm.ShowDialog();
                                    }

                                    this.Close();
                                    return;
                                }
                            }
                            catch
                            {
                                // se der problema ao ler JWT
                            }

                            // fallback geral
                            var defaultForm = new PainelColaboradorFrm(Token);
                            this.Hide();
                            defaultForm.ShowDialog();
                            this.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show($"Falha no login:\n{response.StatusCode} - {response.ReasonPhrase}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnEntrar.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao conectar na API:\n{ex.Message}", "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnEntrar.Enabled = true;
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

        private void LoginFrm_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void lblSenha_Click(object sender, EventArgs e)
        {

        }
    }
}
