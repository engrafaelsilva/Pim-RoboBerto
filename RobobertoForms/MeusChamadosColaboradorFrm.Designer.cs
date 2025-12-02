namespace RobobertoForms
{
    partial class MeusChamadosColaboradorFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeusChamadosColaboradorFrm));
            dataGridView1 = new DataGridView();
            Titulo = new DataGridViewTextBoxColumn();
            Categoria = new DataGridViewTextBoxColumn();
            DatadaAbertura = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Id = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Imagem = new DataGridViewImageColumn();
            btnVoltar = new Button();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BackgroundColor = Color.FromArgb(192, 0, 192);
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.ButtonFace;
            dataGridViewCellStyle1.Font = new Font("Arial Narrow", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeight = 45;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Titulo, Categoria, DatadaAbertura, Column7, Column8, Id, Column4, Column6, Imagem });
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.Black;
            dataGridView1.Location = new Point(5, 64);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1110, 460);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.CellMouseEnter += dataGridView1_CellMouseEnter;
            dataGridView1.CellMouseLeave += dataGridView1_CellMouseLeave;
            // 
            // Titulo
            // 
            Titulo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Titulo.DataPropertyName = "Titulo";
            Titulo.HeaderText = "Titulo";
            Titulo.MinimumWidth = 6;
            Titulo.Name = "Titulo";
            Titulo.ReadOnly = true;
            Titulo.Resizable = DataGridViewTriState.False;
            // 
            // Categoria
            // 
            Categoria.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Categoria.DataPropertyName = "Categoria";
            Categoria.HeaderText = "Categoria";
            Categoria.MinimumWidth = 6;
            Categoria.Name = "Categoria";
            Categoria.ReadOnly = true;
            Categoria.Resizable = DataGridViewTriState.False;
            // 
            // DatadaAbertura
            // 
            DatadaAbertura.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DatadaAbertura.DataPropertyName = "Data";
            DatadaAbertura.HeaderText = "Data de abertura";
            DatadaAbertura.MinimumWidth = 6;
            DatadaAbertura.Name = "DatadaAbertura";
            DatadaAbertura.ReadOnly = true;
            DatadaAbertura.Resizable = DataGridViewTriState.False;
            // 
            // Column7
            // 
            Column7.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column7.DataPropertyName = "Status";
            Column7.HeaderText = "Status";
            Column7.MinimumWidth = 6;
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            Column7.Resizable = DataGridViewTriState.False;
            // 
            // Column8
            // 
            Column8.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column8.DataPropertyName = "Prioridade";
            Column8.HeaderText = "Prioridade";
            Column8.MinimumWidth = 6;
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            Column8.Resizable = DataGridViewTriState.False;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.HeaderText = "Column1";
            Id.MinimumWidth = 6;
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Visible = false;
            Id.Width = 125;
            // 
            // Column4
            // 
            Column4.DataPropertyName = "Autor";
            Column4.HeaderText = "Column4";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Visible = false;
            Column4.Width = 125;
            // 
            // Column6
            // 
            Column6.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column6.DataPropertyName = "Descricao";
            Column6.HeaderText = "Column6";
            Column6.MinimumWidth = 6;
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            Column6.Visible = false;
            Column6.Width = 125;
            // 
            // Imagem
            // 
            Imagem.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Imagem.HeaderText = "";
            Imagem.Image = (Image)resources.GetObject("Imagem.Image");
            Imagem.MinimumWidth = 6;
            Imagem.Name = "Imagem";
            Imagem.ReadOnly = true;
            Imagem.Width = 23;
            // 
            // btnVoltar
            // 
            btnVoltar.BackColor = Color.White;
            btnVoltar.ForeColor = Color.Black;
            btnVoltar.Location = new Point(5, 30);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new Size(94, 28);
            btnVoltar.TabIndex = 2;
            btnVoltar.Text = "Voltar";
            btnVoltar.UseVisualStyleBackColor = false;
            btnVoltar.Click += btnVoltar_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnVoltar);
            panel1.Controls.Add(dataGridView1);
            panel1.Location = new Point(0, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(1135, 558);
            panel1.TabIndex = 3;
            panel1.Paint += panel1_Paint;
            // 
            // MeusChamadosFrm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 0, 192);
            ClientSize = new Size(1134, 560);
            Controls.Add(panel1);
            ForeColor = Color.FromArgb(192, 0, 192);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MeusChamadosFrm";
            Text = "MeusChamadosForm";
            Load += MeusChamadosForm_Load;
            Paint += panel1_Paint;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnVoltar;
        private DataGridViewTextBoxColumn Titulo;
        private DataGridViewTextBoxColumn Categoria;
        private DataGridViewTextBoxColumn DatadaAbertura;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewImageColumn Imagem;
        private Panel panel1;
    }
}