namespace RobobertoForms
{
    partial class ChatChamadoFrmColaborador
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
            pnlChat = new Panel();
            btnEnviar = new Button();
            txtMensagem = new TextBox();
            txtDescricao = new TextBox();
            lblDescricao = new Label();
            txtTitulo = new TextBox();
            lblTitulo = new Label();
            btnVoltar = new Button();
            txtSugestao = new TextBox();
            lblSugestao = new Label();
            panel2 = new Panel();
            pnlChat.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // pnlChat
            // 
            pnlChat.BackColor = Color.Gray;
            pnlChat.Controls.Add(btnEnviar);
            pnlChat.Controls.Add(txtMensagem);
            pnlChat.Cursor = Cursors.IBeam;
            pnlChat.Location = new Point(432, 35);
            pnlChat.Name = "pnlChat";
            pnlChat.Size = new Size(661, 437);
            pnlChat.TabIndex = 0;
            pnlChat.Paint += pnlChat_Paint;
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(555, 395);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(94, 29);
            btnEnviar.TabIndex = 1;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(15, 395);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(525, 27);
            txtMensagem.TabIndex = 0;
            txtMensagem.TextChanged += txtMensagem_TextChanged;
            // 
            // txtDescricao
            // 
            txtDescricao.Location = new Point(20, 115);
            txtDescricao.Multiline = true;
            txtDescricao.Name = "txtDescricao";
            txtDescricao.ReadOnly = true;
            txtDescricao.ScrollBars = ScrollBars.Vertical;
            txtDescricao.Size = new Size(390, 127);
            txtDescricao.TabIndex = 9;
            txtDescricao.TextChanged += txtDescricao_TextChanged;
            // 
            // lblDescricao
            // 
            lblDescricao.AutoSize = true;
            lblDescricao.BackColor = Color.Transparent;
            lblDescricao.ForeColor = SystemColors.ButtonFace;
            lblDescricao.Location = new Point(20, 92);
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Size = new Size(132, 20);
            lblDescricao.TabIndex = 8;
            lblDescricao.Text = "Queixa do usuário:";
            // 
            // txtTitulo
            // 
            txtTitulo.Location = new Point(20, 45);
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
            lblTitulo.Location = new Point(20, 22);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(50, 20);
            lblTitulo.TabIndex = 6;
            lblTitulo.Text = "Titulo:";
            // 
            // btnVoltar
            // 
            btnVoltar.ForeColor = SystemColors.ActiveCaptionText;
            btnVoltar.Location = new Point(86, 443);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(246, 29);
            btnVoltar.TabIndex = 18;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // txtSugestao
            // 
            txtSugestao.Location = new Point(20, 282);
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
            lblSugestao.Location = new Point(20, 259);
            lblSugestao.Name = "lblSugestao";
            lblSugestao.Size = new Size(288, 20);
            lblSugestao.TabIndex = 19;
            lblSugestao.Text = "Sugestão gerada por Inteligência Artifical:";
            // 
            // panel2
            // 
            panel2.Controls.Add(txtSugestao);
            panel2.Controls.Add(lblSugestao);
            panel2.Controls.Add(btnVoltar);
            panel2.Controls.Add(txtDescricao);
            panel2.Controls.Add(lblDescricao);
            panel2.Controls.Add(txtTitulo);
            panel2.Controls.Add(lblTitulo);
            panel2.Controls.Add(pnlChat);
            panel2.Location = new Point(-4, 1);
            panel2.Name = "panel2";
            panel2.Size = new Size(1122, 513);
            panel2.TabIndex = 21;
            panel2.Paint += panel2_Paint;
            // 
            // ChatChamadoFrmColaborador
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 0, 192);
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1116, 513);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ChatChamadoFrmColaborador";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ChatChamadoForm";
            FormClosing += ChatChamadoFrm_FormClosing;
            Load += ChatChamadoFrm_Load;
            Paint += ChatChamadoFrm_Paint;
            pnlChat.ResumeLayout(false);
            pnlChat.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlChat;
        private TextBox txtDescricao;
        private Label lblDescricao;
        private TextBox txtTitulo;
        private Label lblTitulo;
        private Button btnVoltar;
        private TextBox txtSugestao;
        private Label lblSugestao;
        private Panel panel2;
        private Button btnEnviar;
        private TextBox txtMensagem;
    }
}