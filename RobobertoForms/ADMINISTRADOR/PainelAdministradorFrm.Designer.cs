
namespace RobobertoForms
{
    partial class PainelAdministradorFrm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStripPrincipal = new MenuStrip();
            meuPerfilToolStripMenuItem = new ToolStripMenuItem();
            verMeuPerfilToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            chamadosToolStripMenuItem = new ToolStripMenuItem();
            abrirChamadoToolStripMenuItem = new ToolStripMenuItem();
            meusChamadosToolStripMenuItem = new ToolStripMenuItem();
            categoriasToolStripMenuItem = new ToolStripMenuItem();
            gerenciamentoDeCategoriasToolStripMenuItem = new ToolStripMenuItem();
            departamentosToolStripMenuItem = new ToolStripMenuItem();
            gerenciamentoDeDepartamentosToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            menuStripPrincipal.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripPrincipal
            // 
            menuStripPrincipal.BackColor = Color.Orchid;
            menuStripPrincipal.ImageScalingSize = new Size(20, 20);
            menuStripPrincipal.Items.AddRange(new ToolStripItem[] { meuPerfilToolStripMenuItem, chamadosToolStripMenuItem, categoriasToolStripMenuItem, departamentosToolStripMenuItem });
            menuStripPrincipal.Location = new Point(0, 0);
            menuStripPrincipal.Name = "menuStripPrincipal";
            menuStripPrincipal.Size = new Size(1134, 28);
            menuStripPrincipal.TabIndex = 0;
            menuStripPrincipal.Text = "menuStrip1";
            // 
            // meuPerfilToolStripMenuItem
            // 
            meuPerfilToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { verMeuPerfilToolStripMenuItem, toolStripMenuItem1 });
            meuPerfilToolStripMenuItem.ForeColor = SystemColors.ControlLightLight;
            meuPerfilToolStripMenuItem.Name = "meuPerfilToolStripMenuItem";
            meuPerfilToolStripMenuItem.Size = new Size(91, 24);
            meuPerfilToolStripMenuItem.Text = "Meu perfil";
            // 
            // verMeuPerfilToolStripMenuItem
            // 
            verMeuPerfilToolStripMenuItem.Name = "verMeuPerfilToolStripMenuItem";
            verMeuPerfilToolStripMenuItem.Size = new Size(224, 26);
            verMeuPerfilToolStripMenuItem.Text = "Ver meu perfil";
            verMeuPerfilToolStripMenuItem.Click += verMeuPerfilToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(224, 26);
            toolStripMenuItem1.Text = "Logout";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // chamadosToolStripMenuItem
            // 
            chamadosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { abrirChamadoToolStripMenuItem, meusChamadosToolStripMenuItem });
            chamadosToolStripMenuItem.ForeColor = SystemColors.ControlLightLight;
            chamadosToolStripMenuItem.Name = "chamadosToolStripMenuItem";
            chamadosToolStripMenuItem.Size = new Size(93, 24);
            chamadosToolStripMenuItem.Text = "Chamados";
            chamadosToolStripMenuItem.Click += chamadosToolStripMenuItem_Click;
            // 
            // abrirChamadoToolStripMenuItem
            // 
            abrirChamadoToolStripMenuItem.Name = "abrirChamadoToolStripMenuItem";
            abrirChamadoToolStripMenuItem.Size = new Size(199, 26);
            abrirChamadoToolStripMenuItem.Text = "Abrir Chamado";
            abrirChamadoToolStripMenuItem.Click += abrirChamadoToolStripMenuItem_Click_2;
            // 
            // meusChamadosToolStripMenuItem
            // 
            meusChamadosToolStripMenuItem.Name = "meusChamadosToolStripMenuItem";
            meusChamadosToolStripMenuItem.Size = new Size(199, 26);
            meusChamadosToolStripMenuItem.Text = "Meus chamados";
            meusChamadosToolStripMenuItem.Click += meusChamadosToolStripMenuItem_Click;
            // 
            // categoriasToolStripMenuItem
            // 
            categoriasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gerenciamentoDeCategoriasToolStripMenuItem });
            categoriasToolStripMenuItem.ForeColor = SystemColors.ControlLightLight;
            categoriasToolStripMenuItem.Name = "categoriasToolStripMenuItem";
            categoriasToolStripMenuItem.Size = new Size(94, 24);
            categoriasToolStripMenuItem.Text = "Categorias";
            categoriasToolStripMenuItem.Click += categoriasToolStripMenuItem_Click;
            // 
            // gerenciamentoDeCategoriasToolStripMenuItem
            // 
            gerenciamentoDeCategoriasToolStripMenuItem.Name = "gerenciamentoDeCategoriasToolStripMenuItem";
            gerenciamentoDeCategoriasToolStripMenuItem.Size = new Size(289, 26);
            gerenciamentoDeCategoriasToolStripMenuItem.Text = "Gerenciamento de Categorias";
            gerenciamentoDeCategoriasToolStripMenuItem.Click += gerenciamentoDeCategoriasToolStripMenuItem_Click;
            // 
            // departamentosToolStripMenuItem
            // 
            departamentosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gerenciamentoDeDepartamentosToolStripMenuItem });
            departamentosToolStripMenuItem.ForeColor = SystemColors.ControlLightLight;
            departamentosToolStripMenuItem.Name = "departamentosToolStripMenuItem";
            departamentosToolStripMenuItem.Size = new Size(126, 24);
            departamentosToolStripMenuItem.Text = "Departamentos";
            departamentosToolStripMenuItem.Click += departamentosToolStripMenuItem_Click;
            // 
            // gerenciamentoDeDepartamentosToolStripMenuItem
            // 
            gerenciamentoDeDepartamentosToolStripMenuItem.Name = "gerenciamentoDeDepartamentosToolStripMenuItem";
            gerenciamentoDeDepartamentosToolStripMenuItem.Size = new Size(319, 26);
            gerenciamentoDeDepartamentosToolStripMenuItem.Text = "Gerenciamento de departamentos";
            gerenciamentoDeDepartamentosToolStripMenuItem.Click += gerenciamentoDeDepartamentosToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Fill;
            panel1.ForeColor = Color.Transparent;
            panel1.Location = new Point(0, 28);
            panel1.Name = "panel1";
            panel1.Size = new Size(1134, 532);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // PainelAdministradorFrm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 0, 192);
            ClientSize = new Size(1134, 560);
            Controls.Add(panel1);
            Controls.Add(menuStripPrincipal);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStripPrincipal;
            MaximizeBox = false;
            Name = "PainelAdministradorFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ROBOBERTO - HELP DESK";
            Load += PrincipalFormTeste_Load;
            menuStripPrincipal.ResumeLayout(false);
            menuStripPrincipal.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private MenuStrip menuStripPrincipal;
        private ToolStripMenuItem chamadosToolStripMenuItem;
        private ToolStripMenuItem abrirChamadoToolStripMenuItem;
        private ToolStripMenuItem meusChamadosToolStripMenuItem;
        private Panel panel1;
        private ToolStripMenuItem categoriasToolStripMenuItem;
        private ToolStripMenuItem departamentosToolStripMenuItem;
        private ToolStripMenuItem gerenciamentoDeCategoriasToolStripMenuItem;
        private ToolStripMenuItem gerenciamentoDeDepartamentosToolStripMenuItem;
        private ToolStripMenuItem meuPerfilToolStripMenuItem;
        private ToolStripMenuItem verMeuPerfilToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
    }
}
