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
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.ForeColor = SystemColors.ButtonFace;
            lblTitulo.Location = new Point(23, 26);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(138, 20);
            lblTitulo.TabIndex = 4;
            lblTitulo.Text = "Titulo do chamado:";
            // 
            // txtTitulo
            // 
            txtTitulo.Location = new Point(23, 49);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(591, 27);
            txtTitulo.TabIndex = 5;
            txtTitulo.TextChanged += txtTitulo_TextChanged;
            // 
            // lblDescricao
            // 
            lblDescricao.AutoSize = true;
            lblDescricao.ForeColor = SystemColors.ButtonFace;
            lblDescricao.Location = new Point(23, 287);
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Size = new Size(168, 20);
            lblDescricao.TabIndex = 6;
            lblDescricao.Text = "Descrição do problema:";
            // 
            // txtDescricao
            // 
            txtDescricao.Location = new Point(23, 310);
            txtDescricao.Multiline = true;
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Size = new Size(591, 141);
            txtDescricao.TabIndex = 7;
            txtDescricao.TextChanged += txtDescricao_TextChanged;
            // 
            // lblPrioridade
            // 
            lblPrioridade.AutoSize = true;
            lblPrioridade.ForeColor = SystemColors.ButtonFace;
            lblPrioridade.Location = new Point(23, 92);
            lblPrioridade.Name = "lblPrioridade";
            lblPrioridade.Size = new Size(114, 20);
            lblPrioridade.TabIndex = 8;
            lblPrioridade.Text = "Insira seu email:";
            // 
            // cbmPrioridade
            // 
            cbmPrioridade.FormattingEnabled = true;
            cbmPrioridade.Location = new Point(23, 126);
            cbmPrioridade.Name = "cbmPrioridade";
            cbmPrioridade.Size = new Size(591, 28);
            cbmPrioridade.TabIndex = 9;
            cbmPrioridade.SelectedIndexChanged += cbmPrioridade_SelectedIndexChanged;
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.ForeColor = SystemColors.ButtonFace;
            lblCategoria.Location = new Point(23, 174);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(114, 20);
            lblCategoria.TabIndex = 10;
            lblCategoria.Text = "Insira seu email:";
            lblCategoria.Click += lblCategoria_Click;
            // 
            // cbmCategoria
            // 
            cbmCategoria.FormattingEnabled = true;
            cbmCategoria.Location = new Point(23, 212);
            cbmCategoria.Name = "cbmCategoria";
            cbmCategoria.Size = new Size(591, 28);
            cbmCategoria.TabIndex = 11;
            cbmCategoria.SelectedIndexChanged += cbmCategoria_SelectedIndexChanged;
            // 
            // btnGerarSugestao
            // 
            btnGerarSugestao.Location = new Point(23, 505);
            btnGerarSugestao.Name = "btnGerarSugestao";
            btnGerarSugestao.Size = new Size(327, 29);
            btnGerarSugestao.TabIndex = 12;
            btnGerarSugestao.Text = "button1";
            btnGerarSugestao.UseVisualStyleBackColor = true;
            btnGerarSugestao.Click += btnGerarSugestao_Click;
            // 
            // txtSugestaoGemini
            // 
            txtSugestaoGemini.Location = new Point(636, 49);
            txtSugestaoGemini.Multiline = true;
            txtSugestaoGemini.Name = "txtSugestaoGemini";
            txtSugestaoGemini.Size = new Size(488, 217);
            txtSugestaoGemini.TabIndex = 13;
            txtSugestaoGemini.TextChanged += txtSugestaoGemini_TextChanged;
            // 
            // btnResolveu
            // 
            btnResolveu.BackColor = Color.FromArgb(0, 192, 0);
            btnResolveu.Location = new Point(651, 379);
            btnResolveu.Name = "btnResolveu";
            btnResolveu.Size = new Size(201, 72);
            btnResolveu.TabIndex = 14;
            btnResolveu.Text = "button1";
            btnResolveu.UseVisualStyleBackColor = false;
            btnResolveu.Click += btnResolveu_Click;
            // 
            // btnNaoResolveu
            // 
            btnNaoResolveu.Location = new Point(886, 379);
            btnNaoResolveu.Name = "btnNaoResolveu";
            btnNaoResolveu.Size = new Size(201, 72);
            btnNaoResolveu.TabIndex = 15;
            btnNaoResolveu.Text = "button2";
            btnNaoResolveu.UseVisualStyleBackColor = true;
            // 
            // AbrirChamadoFrm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 0, 192);
            ClientSize = new Size(1136, 547);
            Controls.Add(btnNaoResolveu);
            Controls.Add(btnResolveu);
            Controls.Add(txtSugestaoGemini);
            Controls.Add(btnGerarSugestao);
            Controls.Add(cbmCategoria);
            Controls.Add(lblCategoria);
            Controls.Add(cbmPrioridade);
            Controls.Add(lblPrioridade);
            Controls.Add(txtDescricao);
            Controls.Add(lblDescricao);
            Controls.Add(txtTitulo);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AbrirChamadoFrm";
            Text = "Abertura de Chamado";
            Load += AbrirChamadoFrm_Load;
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
    }
}