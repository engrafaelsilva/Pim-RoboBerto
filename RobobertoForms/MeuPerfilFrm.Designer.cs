namespace RobobertoForms
{
    partial class MeuPerfilFrm
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
            txtNome = new TextBox();
            lblNome = new Label();
            txtEmail = new TextBox();
            lblEmail = new Label();
            txtTelefone = new TextBox();
            lblTelefone = new Label();
            label1 = new Label();
            txtDepartamento = new TextBox();
            SuspendLayout();
            // 
            // txtNome
            // 
            txtNome.Location = new Point(35, 51);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(327, 27);
            txtNome.TabIndex = 3;
            txtNome.TextChanged += txtNome_TextChanged;
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.BackColor = Color.Transparent;
            lblNome.ForeColor = SystemColors.ButtonFace;
            lblNome.Location = new Point(35, 28);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(53, 20);
            lblNome.TabIndex = 2;
            lblNome.Text = "Nome:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(35, 121);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(327, 27);
            txtEmail.TabIndex = 5;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.BackColor = Color.Transparent;
            lblEmail.ForeColor = SystemColors.ButtonFace;
            lblEmail.Location = new Point(35, 98);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(49, 20);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email:";
            // 
            // txtTelefone
            // 
            txtTelefone.Location = new Point(35, 192);
            txtTelefone.Name = "txtTelefone";
            txtTelefone.Size = new Size(327, 27);
            txtTelefone.TabIndex = 7;
            txtTelefone.TextChanged += txtTelefone_TextChanged;
            // 
            // lblTelefone
            // 
            lblTelefone.AutoSize = true;
            lblTelefone.BackColor = Color.Transparent;
            lblTelefone.ForeColor = SystemColors.ButtonFace;
            lblTelefone.Location = new Point(35, 169);
            lblTelefone.Name = "lblTelefone";
            lblTelefone.Size = new Size(69, 20);
            lblTelefone.TabIndex = 6;
            lblTelefone.Text = "Telefone:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(35, 236);
            label1.Name = "label1";
            label1.Size = new Size(109, 20);
            label1.TabIndex = 8;
            label1.Text = "Departamento:";
            // 
            // txtDepartamento
            // 
            txtDepartamento.Location = new Point(35, 274);
            txtDepartamento.Name = "txtDepartamento";
            txtDepartamento.Size = new Size(327, 27);
            txtDepartamento.TabIndex = 11;
            txtDepartamento.TextChanged += txtDepartamento_TextChanged;
            // 
            // MeuPerfilFrm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 0, 192);
            ClientSize = new Size(1098, 438);
            Controls.Add(txtDepartamento);
            Controls.Add(label1);
            Controls.Add(txtTelefone);
            Controls.Add(lblTelefone);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtNome);
            Controls.Add(lblNome);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MeuPerfilFrm";
            Text = "MeuPerfilFrm";
            Load += MeuPerfilFrm_Load;
            Paint += SetBackColorDegrade;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNome;
        private Label lblNome;
        private TextBox txtEmail;
        private Label lblEmail;
        private TextBox txtTelefone;
        private Label lblTelefone;
        private Label label1;
        private TextBox txtDepartamento;
    }
}