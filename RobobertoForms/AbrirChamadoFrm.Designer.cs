namespace RobobertoForms
{
    partial class AbrirChamadoFrm
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
            lblTitulo = new Label();
            txtTitulo = new TextBox();
            lblDescricao = new Label();
            txtDescricao = new TextBox();
            lblPrioridade = new Label();
            cbmPrioridade = new ComboBox();
            lblCategoria = new Label();
            cbmCategoria = new ComboBox();
            btnGerarSugestao = new Button();
            txtSugestaoGemini = new TextBox();
            btnResolveu = new Button();
            btnNaoResolveu = new Button();
            lblDescricaoBotoes = new Label();
            btnVoltar = new Button();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.ForeColor = SystemColors.ButtonFace;
            lblTitulo.Location = new Point(8, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(138, 20);
            lblTitulo.TabIndex = 4;
            lblTitulo.Text = "Titulo do chamado:";
            lblTitulo.Click += lblTitulo_Click;
            // 
            // txtTitulo
            // 
            txtTitulo.Location = new Point(8, 32);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(567, 27);
            txtTitulo.TabIndex = 5;
            txtTitulo.TextChanged += txtTitulo_TextChanged;
            // 
            // lblDescricao
            // 
            lblDescricao.AutoSize = true;
            lblDescricao.BackColor = Color.Transparent;
            lblDescricao.ForeColor = SystemColors.ButtonFace;
            lblDescricao.Location = new Point(8, 235);
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Size = new Size(168, 20);
            lblDescricao.TabIndex = 6;
            lblDescricao.Text = "Descrição do problema:";
            // 
            // txtDescricao
            // 
            txtDescricao.Location = new Point(8, 258);
            txtDescricao.Multiline = true;
            txtDescricao.Name = "txtDescricao";
            txtDescricao.ScrollBars = ScrollBars.Vertical;
            txtDescricao.Size = new Size(567, 141);
            txtDescricao.TabIndex = 7;
            txtDescricao.TextChanged += txtDescricao_TextChanged;
            // 
            // lblPrioridade
            // 
            lblPrioridade.AutoSize = true;
            lblPrioridade.BackColor = Color.Transparent;
            lblPrioridade.ForeColor = SystemColors.ButtonFace;
            lblPrioridade.Location = new Point(8, 84);
            lblPrioridade.Name = "lblPrioridade";
            lblPrioridade.Size = new Size(236, 20);
            lblPrioridade.TabIndex = 8;
            lblPrioridade.Text = "Escolha a prioridade do chamado:";
            // 
            // cbmPrioridade
            // 
            cbmPrioridade.DropDownStyle = ComboBoxStyle.DropDownList;
            cbmPrioridade.FormattingEnabled = true;
            cbmPrioridade.Location = new Point(8, 107);
            cbmPrioridade.Name = "cbmPrioridade";
            cbmPrioridade.Size = new Size(567, 28);
            cbmPrioridade.TabIndex = 9;
            cbmPrioridade.SelectedIndexChanged += cbmPrioridade_SelectedIndexChanged;
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.BackColor = Color.Transparent;
            lblCategoria.ForeColor = SystemColors.ButtonFace;
            lblCategoria.Location = new Point(8, 159);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(353, 20);
            lblCategoria.TabIndex = 10;
            lblCategoria.Text = "Escolha a categoria em que se encaixa a sua queixa:";
            lblCategoria.Click += lblCategoria_Click;
            // 
            // cbmCategoria
            // 
            cbmCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cbmCategoria.FormattingEnabled = true;
            cbmCategoria.Location = new Point(8, 191);
            cbmCategoria.Name = "cbmCategoria";
            cbmCategoria.Size = new Size(567, 28);
            cbmCategoria.TabIndex = 11;
            cbmCategoria.SelectedIndexChanged += cbmCategoria_SelectedIndexChanged;
            // 
            // btnGerarSugestao
            // 
            btnGerarSugestao.ForeColor = SystemColors.ActiveCaptionText;
            btnGerarSugestao.Location = new Point(126, 437);
            btnGerarSugestao.Name = "btnGerarSugestao";
            btnGerarSugestao.Size = new Size(327, 29);
            btnGerarSugestao.TabIndex = 12;
            btnGerarSugestao.Text = "Cadastrar Chamado e gerar sugestão";
            btnGerarSugestao.UseVisualStyleBackColor = true;
            btnGerarSugestao.Click += btnGerarSugestao_Click;
            // 
            // txtSugestaoGemini
            // 
            txtSugestaoGemini.Location = new Point(611, 37);
            txtSugestaoGemini.Multiline = true;
            txtSugestaoGemini.Name = "txtSugestaoGemini";
            txtSugestaoGemini.ReadOnly = true;
            txtSugestaoGemini.ScrollBars = ScrollBars.Vertical;
            txtSugestaoGemini.Size = new Size(511, 252);
            txtSugestaoGemini.TabIndex = 13;
            txtSugestaoGemini.TextChanged += txtSugestaoGemini_TextChanged;
            // 
            // btnResolveu
            // 
            btnResolveu.BackColor = Color.FromArgb(0, 192, 0);
            btnResolveu.ForeColor = Color.Gainsboro;
            btnResolveu.Location = new Point(673, 361);
            btnResolveu.Name = "btnResolveu";
            btnResolveu.Size = new Size(201, 72);
            btnResolveu.TabIndex = 14;
            btnResolveu.Text = "Sim, resolveu!";
            btnResolveu.UseVisualStyleBackColor = false;
            btnResolveu.Click += btnResolveu_Click;
            // 
            // btnNaoResolveu
            // 
            btnNaoResolveu.BackColor = Color.Firebrick;
            btnNaoResolveu.ForeColor = Color.Transparent;
            btnNaoResolveu.Location = new Point(903, 361);
            btnNaoResolveu.Name = "btnNaoResolveu";
            btnNaoResolveu.Size = new Size(201, 72);
            btnNaoResolveu.TabIndex = 15;
            btnNaoResolveu.Text = "Não, preciso de ajuda.";
            btnNaoResolveu.UseVisualStyleBackColor = false;
            btnNaoResolveu.Click += btnNaoResolveu_Click;
            // 
            // lblDescricaoBotoes
            // 
            lblDescricaoBotoes.AutoSize = true;
            lblDescricaoBotoes.BackColor = Color.OrangeRed;
            lblDescricaoBotoes.ForeColor = SystemColors.ButtonFace;
            lblDescricaoBotoes.Location = new Point(761, 322);
            lblDescricaoBotoes.Name = "lblDescricaoBotoes";
            lblDescricaoBotoes.Size = new Size(274, 20);
            lblDescricaoBotoes.TabIndex = 16;
            lblDescricaoBotoes.Text = "A sugestão acima resolveu o problema?";
            lblDescricaoBotoes.Click += lblDescricaoBotoes_Click;
            // 
            // btnVoltar
            // 
            btnVoltar.ForeColor = SystemColors.ActiveCaptionText;
            btnVoltar.Location = new Point(858, 470);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(246, 29);
            btnVoltar.TabIndex = 17;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btnGerarSugestao);
            panel1.Controls.Add(cbmCategoria);
            panel1.Controls.Add(lblCategoria);
            panel1.Controls.Add(cbmPrioridade);
            panel1.Controls.Add(lblPrioridade);
            panel1.Controls.Add(txtDescricao);
            panel1.Controls.Add(lblDescricao);
            panel1.Controls.Add(txtTitulo);
            panel1.Controls.Add(lblTitulo);
            panel1.Location = new Point(15, 17);
            panel1.Name = "panel1";
            panel1.Size = new Size(586, 495);
            panel1.TabIndex = 18;
            // 
            // AbrirChamadoFrm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 0, 192);
            ClientSize = new Size(1134, 532);
            Controls.Add(panel1);
            Controls.Add(btnVoltar);
            Controls.Add(lblDescricaoBotoes);
            Controls.Add(btnNaoResolveu);
            Controls.Add(btnResolveu);
            Controls.Add(txtSugestaoGemini);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AbrirChamadoFrm";
            Text = "Abertura de Chamado";
            Load += AbrirChamadoFrm_Load;
            Paint += AbrirChamadoFrm_Paint;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private TextBox txtTitulo;
        private Label lblDescricao;
        private TextBox txtDescricao;
        private Label lblPrioridade;
        private ComboBox cbmPrioridade;
        private Label lblCategoria;
        private ComboBox cbmCategoria;
        private Button btnGerarSugestao;
        private TextBox txtSugestaoGemini;
        private Button btnResolveu;
        private Button btnNaoResolveu;
        private Label lblDescricaoBotoes;
        private Button btnVoltar;
        private Panel panel1;
    }
}