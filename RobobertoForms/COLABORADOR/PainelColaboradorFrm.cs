using System.Drawing.Drawing2D;

namespace RobobertoForms
{
    public partial class PainelColaboradorFrm : Form
    {

        private string _token;
        public PainelColaboradorFrm()
        {
            InitializeComponent();
        }
        public PainelColaboradorFrm(string token)
        {
            _token = token;
            InitializeComponent();
        }

        private void CarregarForm(Form frm)
        {
            panel1.Controls.Clear(); // limpa apenas o painel
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            panel1.Controls.Add(frm);
            frm.Show();
        }


        private void PainelColaboradorFrm_Load(object sender, EventArgs e)
        {
        }
        private void abrirChamadoToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            CarregarForm(new AbrirChamadoFrm(_token));

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SetBackColorDegrade(sender, e);
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

        private void meusChamadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CarregarForm(new MeusChamadosColaboradorFrm(_token));

        }

        private void chamadosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _token = null;

            this.Hide(); // esconde o principal
            LoginFrm login = new LoginFrm();
            login.ShowDialog(); // abre login de forma modal
            this.Close(); // fecha o principal quando o login for fechado
        }

        private void meuPerfilToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verMeuPerfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CarregarForm(new MeuPerfilFrm(_token));
        }

        private void logoutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            _token = null;

            this.Hide(); // esconde o principal
            LoginFrm login = new LoginFrm();
            login.ShowDialog(); // abre login de forma modal
            this.Close(); // fecha o principal quando o login for fechado
        }
    }
}
