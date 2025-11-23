
namespace RobobertoForms
{
    partial class PrincipalFrm
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
            chamadosToolStripMenuItem = new ToolStripMenuItem();
            abrirChamadoToolStripMenuItem = new ToolStripMenuItem();
            meusChamadosToolStripMenuItem = new ToolStripMenuItem();
            sairToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            menuStripPrincipal.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripPrincipal
            // 
            menuStripPrincipal.BackColor = Color.Orchid;
            menuStripPrincipal.ImageScalingSize = new Size(20, 20);
            menuStripPrincipal.Items.AddRange(new ToolStripItem[] { chamadosToolStripMenuItem });
            menuStripPrincipal.Location = new Point(0, 0);
            menuStripPrincipal.Name = "menuStripPrincipal";
            menuStripPrincipal.Size = new Size(1134, 28);
            menuStripPrincipal.TabIndex = 0;
            menuStripPrincipal.Text = "menuStrip1";
            // 
            // chamadosToolStripMenuItem
            // 
            chamadosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { abrirChamadoToolStripMenuItem, meusChamadosToolStripMenuItem, sairToolStripMenuItem });
            chamadosToolStripMenuItem.Name = "chamadosToolStripMenuItem";
            chamadosToolStripMenuItem.Size = new Size(93, 24);
            chamadosToolStripMenuItem.Text = "Chamados";
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
            // sairToolStripMenuItem
            // 
            sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            sairToolStripMenuItem.Size = new Size(199, 26);
            sairToolStripMenuItem.Text = "Sair";
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 28);
            panel1.Name = "panel1";
            panel1.Size = new Size(1134, 532);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // PrincipalFrm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 0, 192);
            ClientSize = new Size(1134, 560);
            Controls.Add(panel1);
            Controls.Add(menuStripPrincipal);
            ForeColor = SystemColors.ActiveBorder;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStripPrincipal;
            MaximizeBox = false;
            Name = "PrincipalFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ROBOBERTO - HELP DESK";
            Load += PrincipalFrm_Load;
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
        private ToolStripMenuItem sairToolStripMenuItem;
        private Panel panel1;
    }
}
