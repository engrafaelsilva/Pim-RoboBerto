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
    public partial class MeuPerfilFrm : Form
    {
        private readonly string _token;
        private const string BaseUrl = "https://robobertov2-cvc7cxgfdke9dfdy.brazilsouth-01.azurewebsites.net/";

        public MeuPerfilFrm()
        {
            InitializeComponent();
        }
        public MeuPerfilFrm(string token)
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


        private async void MeuPerfilFrm_Load(object sender, EventArgs e)
        {
            await CarregarMeuPerfilAsync();
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDepartamento_TextChanged(object sender, EventArgs e)
        {

        }

        private async Task CarregarMeuPerfilAsync()
        {
            try
            {
                using var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                string json = await client.GetStringAsync("Usuario/eu");

                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                // Nome e telefone (como antes)
                txtNome.Text = root.GetProperty("nome").GetString() ?? "";
                txtTelefone.Text = root.GetProperty("telefone").GetString() ?? "";

                // Email (verifica se existe antes)
                if (root.TryGetProperty("email", out var emailElem))
                    txtEmail.Text = emailElem.GetString() ?? "";
                else
                    txtEmail.Text = "";

                // Departamento.nome (mantive sua verificação)
                if (root.TryGetProperty("departamento", out var dep) && dep.ValueKind == JsonValueKind.Object &&
                    dep.TryGetProperty("nome", out var depNome))
                {
                    txtDepartamento.Text = depNome.GetString() ?? "";
                }
                else
                {
                    txtDepartamento.Text = "";
                }

                // (opcional) deixe os campos somente-leitura
                txtNome.ReadOnly = true;
                txtTelefone.ReadOnly = true;
                txtEmail.ReadOnly = true;
                txtDepartamento.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar perfil: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
