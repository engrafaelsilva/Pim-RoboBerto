namespace RobobertoForms
{
    partial class CadastrarFrm
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
            lblNome = new Label();
            txtNome = new TextBox();
            txtEmail = new TextBox();
            lblEmail = new Label();
            txtTelefone = new TextBox();
            lblTelefone = new Label();
            txtSenha = new TextBox();
            lblSenha = new Label();
            txtConfirmacaoSenha = new TextBox();
            lblConfirmacaoSenha = new Label();
            btnCadastrar = new Button();
            SuspendLayout();
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.ForeColor = SystemColors.ButtonFace;
            lblNome.Location = new Point(29, 18);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(170, 20);
            lblNome.TabIndex = 0;
            lblNome.Text = "Insira o nome completo:";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(29, 41);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(327, 27);
            txtNome.TabIndex = 1;
            txtNome.TextChanged += txtNome_TextChanged;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(29, 108);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(327, 27);
            txtEmail.TabIndex = 3;
            txtEmail.TextChanged += txtEmail_TextChanged;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.ForeColor = SystemColors.ButtonFace;
            lblEmail.Location = new Point(29, 85);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(114, 20);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Insira seu email:";
            // 
            // txtTelefone
            // 
            txtTelefone.Location = new Point(29, 187);
            txtTelefone.Name = "txtTelefone";
            txtTelefone.Size = new Size(327, 27);
            txtTelefone.TabIndex = 5;
            txtTelefone.TextChanged += txtTelefone_TextChanged;
            // 
            // lblTelefone
            // 
            lblTelefone.AutoSize = true;
            lblTelefone.ForeColor = SystemColors.ButtonFace;
            lblTelefone.Location = new Point(29, 164);
            lblTelefone.Name = "lblTelefone";
            lblTelefone.Size = new Size(288, 20);
            lblTelefone.TabIndex = 4;
            lblTelefone.Text = "insira seu numero de telefone (11 digitos):";
            // 
            // txtSenha
            // 
            txtSenha.Location = new Point(29, 261);
            txtSenha.Name = "txtSenha";
            txtSenha.Size = new Size(327, 27);
            txtSenha.TabIndex = 7;
            txtSenha.TextChanged += txtSenha_TextChanged;
            // 
            // lblSenha
            // 
            lblSenha.AutoSize = true;
            lblSenha.ForeColor = SystemColors.ButtonFace;
            lblSenha.Location = new Point(29, 238);
            lblSenha.Name = "lblSenha";
            lblSenha.Size = new Size(115, 20);
            lblSenha.TabIndex = 6;
            lblSenha.Text = "Insira sua senha:";
            // 
            // txtConfirmacaoSenha
            // 
            txtConfirmacaoSenha.Location = new Point(29, 330);
            txtConfirmacaoSenha.Name = "txtConfirmacaoSenha";
            txtConfirmacaoSenha.Size = new Size(327, 27);
            txtConfirmacaoSenha.TabIndex = 9;
            txtConfirmacaoSenha.TextChanged += txtConfirmacaoSenha_TextChanged;
            // 
            // lblConfirmacaoSenha
            // 
            lblConfirmacaoSenha.AutoSize = true;
            lblConfirmacaoSenha.ForeColor = SystemColors.ButtonFace;
            lblConfirmacaoSenha.Location = new Point(29, 307);
            lblConfirmacaoSenha.Name = "lblConfirmacaoSenha";
            lblConfirmacaoSenha.Size = new Size(141, 20);
            lblConfirmacaoSenha.TabIndex = 8;
            lblConfirmacaoSenha.Text = "Confirme sua senha:";
            lblConfirmacaoSenha.Click += lblConfirmacaoSenha_Click;
            // 
            // btnCadastrar
            // 
            btnCadastrar.Location = new Point(530, 393);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new Size(202, 29);
            btnCadastrar.TabIndex = 10;
            btnCadastrar.Text = "Cadastrar";
            btnCadastrar.UseVisualStyleBackColor = true;
            btnCadastrar.Click += btnCadastrar_Click;
            // 
            // CadastrarFrm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 0, 192);
            ClientSize = new Size(800, 450);
            Controls.Add(btnCadastrar);
            Controls.Add(txtConfirmacaoSenha);
            Controls.Add(lblConfirmacaoSenha);
            Controls.Add(txtSenha);
            Controls.Add(lblSenha);
            Controls.Add(txtTelefone);
            Controls.Add(lblTelefone);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtNome);
            Controls.Add(lblNome);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "CadastrarFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cadastro";
            Load += CadastrarFrm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNome;
        private TextBox txtNome;
        private TextBox txtEmail;
        private Label lblEmail;
        private TextBox txtTelefone;
        private Label lblTelefone;
        private TextBox txtSenha;
        private Label lblSenha;
        private TextBox txtConfirmacaoSenha;
        private Label lblConfirmacaoSenha;
        private Button btnCadastrar;
    }
}