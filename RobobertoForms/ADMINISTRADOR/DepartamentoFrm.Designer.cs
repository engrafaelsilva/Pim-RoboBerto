namespace RobobertoForms
{
    partial class DepartamentoFrm
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            lblGestaoDepartamentos = new Label();
            lblNome = new Label();
            txtNome = new TextBox();
            btnCadastrar = new Button();
            dgvDepartamentos = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            Nome = new DataGridViewTextBoxColumn();
            btnDeletar = new Button();
            btnAtualizar = new Button();
            lblSelecioneDepartamento = new Label();
            btnVoltar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDepartamentos).BeginInit();
            SuspendLayout();
            // 
            // lblGestaoDepartamentos
            // 
            lblGestaoDepartamentos.AutoSize = true;
            lblGestaoDepartamentos.BackColor = Color.Transparent;
            lblGestaoDepartamentos.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            lblGestaoDepartamentos.ForeColor = SystemColors.ControlLightLight;
            lblGestaoDepartamentos.Location = new Point(237, 9);
            lblGestaoDepartamentos.Name = "lblGestaoDepartamentos";
            lblGestaoDepartamentos.Size = new Size(584, 41);
            lblGestaoDepartamentos.TabIndex = 0;
            lblGestaoDepartamentos.Text = "GESTÃO DE DEPARTAMENTOS";
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
            // dgvDepartamentos
            // 
            dgvDepartamentos.AccessibleRole = AccessibleRole.None;
            dgvDepartamentos.AllowUserToAddRows = false;
            dgvDepartamentos.AllowUserToDeleteRows = false;
            dgvDepartamentos.AllowUserToResizeColumns = false;
            dgvDepartamentos.AllowUserToResizeRows = false;
            dgvDepartamentos.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvDepartamentos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvDepartamentos.ColumnHeadersHeight = 29;
            dgvDepartamentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDepartamentos.Columns.AddRange(new DataGridViewColumn[] { Id, Nome });
            dgvDepartamentos.Location = new Point(320, 128);
            dgvDepartamentos.MultiSelect = false;
            dgvDepartamentos.Name = "dgvDepartamentos";
            dgvDepartamentos.ReadOnly = true;
            dgvDepartamentos.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvDepartamentos.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvDepartamentos.RowHeadersVisible = false;
            dgvDepartamentos.RowHeadersWidth = 51;
            dgvDepartamentos.RowTemplate.Height = 29;
            dgvDepartamentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDepartamentos.Size = new Size(520, 261);
            dgvDepartamentos.TabIndex = 4;
            dgvDepartamentos.CellContentClick += dgvDepartamentos_CellContentClick;
            dgvDepartamentos.SelectionChanged += dgvDepartamentos_SelectionChanged;
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
            // lblSelecioneDepartamento
            // 
            lblSelecioneDepartamento.AutoSize = true;
            lblSelecioneDepartamento.BackColor = Color.Transparent;
            lblSelecioneDepartamento.ForeColor = Color.White;
            lblSelecioneDepartamento.Location = new Point(320, 105);
            lblSelecioneDepartamento.Name = "lblSelecioneDepartamento";
            lblSelecioneDepartamento.Size = new Size(430, 20);
            lblSelecioneDepartamento.TabIndex = 7;
            lblSelecioneDepartamento.Text = "Selecione a categoria e escolha atualizar ou deletar o chamado";
            lblSelecioneDepartamento.Click += lblSelecioneDepartamento_Click;
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
            // DepartamentoFrm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 0, 192);
            ClientSize = new Size(1116, 485);
            Controls.Add(btnVoltar);
            Controls.Add(lblSelecioneDepartamento);
            Controls.Add(btnAtualizar);
            Controls.Add(btnDeletar);
            Controls.Add(dgvDepartamentos);
            Controls.Add(btnCadastrar);
            Controls.Add(txtNome);
            Controls.Add(lblNome);
            Controls.Add(lblGestaoDepartamentos);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DepartamentoFrm";
            Text = "CategoriaFrm";
            Load += DepartamentoFrm_Load;
            Paint += DepartamentoFrm_Paint;
            ((System.ComponentModel.ISupportInitialize)dgvDepartamentos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblGestaoDepartamentos;
        private Label lblNome;
        private TextBox txtNome;
        private Button btnCadastrar;
        private DataGridView dgvDepartamentos;
        private Button btnDeletar;
        private Button btnAtualizar;
        private Label lblSelecioneDepartamento;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Nome;
        private Button btnVoltar;
    }
}