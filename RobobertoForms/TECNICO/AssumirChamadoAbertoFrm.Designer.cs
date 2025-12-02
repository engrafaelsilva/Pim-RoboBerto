namespace RobobertoForms
{
    partial class AssumirChamadoAbertoFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtDescricao = new TextBox();
            lblDescricao = new Label();
            txtTitulo = new TextBox();
            lblTitulo = new Label();
            txtSugestao = new TextBox();
            lblSugestao = new Label();
            panel2 = new Panel();
            btnAssumirChamado = new Button();
            txtAutor = new TextBox();
            lblAutor = new Label();
            btnVoltar = new Button();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // txtDescricao
            // 
            txtDescricao.Location = new Point(33, 173);
            txtDescricao.Multiline = true;
            txtDescricao.Name = "txtDescricao";
            txtDescricao.ReadOnly = true;
            txtDescricao.ScrollBars = ScrollBars.Vertical;
            txtDescricao.Size = new Size(390, 61);
            txtDescricao.TabIndex = 9;
            txtDescricao.TextChanged += txtDescricao_TextChanged;
            // 
            // lblDescricao
            // 
            lblDescricao.AutoSize = true;
            lblDescricao.BackColor = Color.Transparent;
            lblDescricao.ForeColor = SystemColors.ButtonFace;
            lblDescricao.Location = new Point(33, 150);
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Size = new Size(132, 20);
            lblDescricao.TabIndex = 8;
            lblDescricao.Text = "Queixa do usuário:";
            // 
            // txtTitulo
            // 
            txtTitulo.Location = new Point(33, 103);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.ReadOnly = true;
            txtTitulo.Size = new Size(390, 27);
            txtTitulo.TabIndex = 7;
            txtTitulo.TextChanged += txtTitulo_TextChanged;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.ForeColor = SystemColors.ButtonFace;
            lblTitulo.Location = new Point(33, 80);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(50, 20);
            lblTitulo.TabIndex = 6;
            lblTitulo.Text = "Titulo:";
            // 
            // txtSugestao
            // 
            txtSugestao.Location = new Point(33, 272);
            txtSugestao.Multiline = true;
            txtSugestao.Name = "txtSugestao";
            txtSugestao.ReadOnly = true;
            txtSugestao.ScrollBars = ScrollBars.Vertical;
            txtSugestao.Size = new Size(390, 127);
            txtSugestao.TabIndex = 20;
            txtSugestao.TextChanged += txtSugestao_TextChanged;
            // 
            // lblSugestao
            // 
            lblSugestao.AutoSize = true;
            lblSugestao.BackColor = Color.Transparent;
            lblSugestao.ForeColor = SystemColors.ButtonFace;
            lblSugestao.Location = new Point(33, 249);
            lblSugestao.Name = "lblSugestao";
            lblSugestao.Size = new Size(288, 20);
            lblSugestao.TabIndex = 19;
            lblSugestao.Text = "Sugestão gerada por Inteligência Artifical:";
            // 
            // panel2
            // 
            panel2.Controls.Add(btnVoltar);
            panel2.Controls.Add(btnAssumirChamado);
            panel2.Controls.Add(txtAutor);
            panel2.Controls.Add(lblAutor);
            panel2.Controls.Add(txtSugestao);
            panel2.Controls.Add(lblSugestao);
            panel2.Controls.Add(txtDescricao);
            panel2.Controls.Add(lblDescricao);
            panel2.Controls.Add(txtTitulo);
            panel2.Controls.Add(lblTitulo);
            panel2.Location = new Point(-4, -3);
            panel2.Name = "panel2";
            panel2.Size = new Size(1122, 517);
            panel2.TabIndex = 21;
            panel2.Paint += panel2_Paint;
            // 
            // btnAssumirChamado
            // 
            btnAssumirChamado.Location = new Point(33, 436);
            btnAssumirChamado.Name = "btnAssumirChamado";
            btnAssumirChamado.Size = new Size(187, 29);
            btnAssumirChamado.TabIndex = 23;
            btnAssumirChamado.Text = "Assumir Chamado";
            btnAssumirChamado.UseVisualStyleBackColor = true;
            btnAssumirChamado.Click += btnAssumirChamado_Click;
            // 
            // txtAutor
            // 
            txtAutor.Location = new Point(33, 35);
            txtAutor.Name = "txtAutor";
            txtAutor.ReadOnly = true;
            txtAutor.Size = new Size(390, 27);
            txtAutor.TabIndex = 22;
            txtAutor.TextChanged += txtAutor_TextChanged;
            // 
            // lblAutor
            // 
            lblAutor.AutoSize = true;
            lblAutor.BackColor = Color.Transparent;
            lblAutor.ForeColor = SystemColors.ButtonFace;
            lblAutor.Location = new Point(33, 12);
            lblAutor.Name = "lblAutor";
            lblAutor.Size = new Size(137, 20);
            lblAutor.TabIndex = 21;
            lblAutor.Text = "Autor do chamado:";
            // 
            // btnVoltar
            // 
            btnVoltar.Location = new Point(236, 436);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(187, 29);
            btnVoltar.TabIndex = 24;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // AssumirChamadoAbertoFrm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 0, 192);
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1116, 513);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AssumirChamadoAbertoFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ChatChamadoTecnicoFrm";
            FormClosing += ChatChamadoFrm_FormClosing;
            Load += ChatChamadoFrm_Load;
            Paint += ChatChamadoTecnicoFrm_Paint;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox txtDescricao;
        private Label lblDescricao;
        private TextBox txtTitulo;
        private Label lblTitulo;
        private TextBox txtSugestao;
        private Label lblSugestao;
        private Panel panel2;
        private TextBox txtAutor;
        private Label lblAutor;
        private Button btnAssumirChamado;
        private Button btnVoltar;
    }
}