namespace RobobertoForms
{
    partial class CategoriaFrm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lblGestaoCategorias = new Label();
            lblNome = new Label();
            txtNome = new TextBox();
            btnCadastrar = new Button();
            dgvCategorias = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Nome = new DataGridViewTextBoxColumn();
            btnDeletar = new Button();
            btnAtualizar = new Button();
            lblSelecioneCategoria = new Label();
            btnVoltar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).BeginInit();
            SuspendLayout();
            // 
            // lblGestaoCategorias
            // 
            lblGestaoCategorias.AutoSize = true;
            lblGestaoCategorias.BackColor = Color.Transparent;
            lblGestaoCategorias.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            lblGestaoCategorias.ForeColor = SystemColors.ControlLightLight;
            lblGestaoCategorias.Location = new Point(189, 9);
            lblGestaoCategorias.Name = "lblGestaoCategorias";
            lblGestaoCategorias.Size = new Size(765, 41);
            lblGestaoCategorias.TabIndex = 0;
            lblGestaoCategorias.Text = "GESTÃO DE CATEGORIAS DE CHAMADO";
            lblGestaoCategorias.Click += lblCadastroCategorias_Click;
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.BackColor = Color.Transparent;
            lblNome.ForeColor = Color.White;
            lblNome.Location = new Point(27, 105);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(226, 20);
            lblNome.TabIndex = 1;
            lblNome.Text = "Insira o nome da categoria nova:";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(27, 128);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(259, 27);
            txtNome.TabIndex = 2;
            txtNome.TextChanged += txtNome_TextChanged;
            // 
            // btnCadastrar
            // 
            btnCadastrar.ForeColor = SystemColors.ActiveCaptionText;
            btnCadastrar.Location = new Point(27, 177);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new Size(94, 29);
            btnCadastrar.TabIndex = 3;
            btnCadastrar.Text = "Cadastrar";
            btnCadastrar.UseVisualStyleBackColor = true;
            btnCadastrar.Click += btnCadastrar_Click;
            // 
            // dgvCategorias
            // 
            dgvCategorias.AccessibleRole = AccessibleRole.None;
            dgvCategorias.AllowUserToAddRows = false;
            dgvCategorias.AllowUserToDeleteRows = false;
            dgvCategorias.AllowUserToResizeColumns = false;
            dgvCategorias.AllowUserToResizeRows = false;
            dgvCategorias.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvCategorias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvCategorias.ColumnHeadersHeight = 29;
            dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCategorias.Columns.AddRange(new DataGridViewColumn[] { Id, Nome });
            dgvCategorias.Location = new Point(320, 128);
            dgvCategorias.MultiSelect = false;
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.ReadOnly = true;
            dgvCategorias.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvCategorias.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvCategorias.RowHeadersVisible = false;
            dgvCategorias.RowHeadersWidth = 51;
            dgvCategorias.RowTemplate.Height = 29;
            dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategorias.Size = new Size(520, 261);
            dgvCategorias.TabIndex = 4;
            dgvCategorias.CellContentClick += dgvCategorias_CellContentClick;
            dgvCategorias.SelectionChanged += dgvCategorias_SelectionChanged;
            // 
            // Id
            // 
            Id.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Id.DataPropertyName = "id";
            Id.HeaderText = "Id";
            Id.MinimumWidth = 6;
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Visible = false;
            // 
            // Nome
            // 
            Nome.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Nome.DataPropertyName = "Nome";
            Nome.HeaderText = "Nome";
            Nome.MinimumWidth = 6;
            Nome.Name = "Nome";
            Nome.ReadOnly = true;
            // 
            // btnDeletar
            // 
            btnDeletar.ForeColor = SystemColors.ActiveCaptionText;
            btnDeletar.Location = new Point(320, 414);
            btnDeletar.Name = "btnDeletar";
            btnDeletar.Size = new Size(126, 29);
            btnDeletar.TabIndex = 5;
            btnDeletar.Text = "Deletar";
            btnDeletar.UseVisualStyleBackColor = true;
            btnDeletar.Click += btnDeletar_Click;
            // 
            // btnAtualizar
            // 
            btnAtualizar.ForeColor = SystemColors.ActiveCaptionText;
            btnAtualizar.Location = new Point(519, 414);
            btnAtualizar.Name = "btnAtualizar";
            btnAtualizar.Size = new Size(126, 29);
            btnAtualizar.TabIndex = 6;
            btnAtualizar.Text = "Atualizar";
            btnAtualizar.UseVisualStyleBackColor = true;
            btnAtualizar.Click += btnAtualizar_Click;
            // 
            // lblSelecioneCategoria
            // 
            lblSelecioneCategoria.AutoSize = true;
            lblSelecioneCategoria.BackColor = Color.Transparent;
            lblSelecioneCategoria.ForeColor = Color.White;
            lblSelecioneCategoria.Location = new Point(320, 105);
            lblSelecioneCategoria.Name = "lblSelecioneCategoria";
            lblSelecioneCategoria.Size = new Size(430, 20);
            lblSelecioneCategoria.TabIndex = 7;
            lblSelecioneCategoria.Text = "Selecione a categoria e escolha atualizar ou deletar o chamado";
            lblSelecioneCategoria.Click += lblSelecioneCategoria_Click;
            // 
            // btnVoltar
            // 
            btnVoltar.ForeColor = SystemColors.ActiveCaptionText;
            btnVoltar.Location = new Point(948, 428);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(126, 29);
            btnVoltar.TabIndex = 8;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // CategoriaFrm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 0, 192);
            ClientSize = new Size(1116, 485);
            Controls.Add(btnVoltar);
            Controls.Add(lblSelecioneCategoria);
            Controls.Add(btnAtualizar);
            Controls.Add(btnDeletar);
            Controls.Add(dgvCategorias);
            Controls.Add(btnCadastrar);
            Controls.Add(txtNome);
            Controls.Add(lblNome);
            Controls.Add(lblGestaoCategorias);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CategoriaFrm";
            Text = "CategoriaFrm";
            Load += CategoriaFrm_Load;
            Paint += CategoriaFrm_Paint;
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblGestaoCategorias;
        private Label lblNome;
        private TextBox txtNome;
        private Button btnCadastrar;
        private DataGridView dgvCategorias;
        private Button btnDeletar;
        private Button btnAtualizar;
        private Label lblSelecioneCategoria;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Nome;
        private Button btnVoltar;
    }
}