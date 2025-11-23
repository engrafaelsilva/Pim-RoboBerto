namespace RobobertoForms
{
    public partial class PrincipalFrm : Form
    {

        private string _token;
        public PrincipalFrm()
        {
            InitializeComponent();
        }
        public PrincipalFrm(string token)
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


        private void PrincipalFrm_Load(object sender, EventArgs e)
        {
        }
        private void abrirChamadoToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            CarregarForm(new AbrirChamadoFrm(_token));

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void meusChamadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CarregarForm(new MeusChamadosForm(_token));

        }
    }
}
