namespace ZasTrack.Forms.wProyectos
{
    partial class wProyectos
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
            panel2 = new Panel();
            menuStrip1 = new MenuStrip();
            agregarProyectoToolStripMenuItem = new ToolStripMenuItem();
            verProyectoToolStripMenuItem = new ToolStripMenuItem();
            pnlContenedor = new Panel();
            panel2.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(menuStrip1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1561, 29);
            panel2.TabIndex = 8;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { agregarProyectoToolStripMenuItem, verProyectoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1561, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // agregarProyectoToolStripMenuItem
            // 
            agregarProyectoToolStripMenuItem.Name = "agregarProyectoToolStripMenuItem";
            agregarProyectoToolStripMenuItem.Size = new Size(139, 24);
            agregarProyectoToolStripMenuItem.Text = "Agregar Proyecto";
            agregarProyectoToolStripMenuItem.Click += agregarProyectoToolStripMenuItem_Click;
            // 
            // verProyectoToolStripMenuItem
            // 
            verProyectoToolStripMenuItem.Name = "verProyectoToolStripMenuItem";
            verProyectoToolStripMenuItem.Size = new Size(168, 24);
            verProyectoToolStripMenuItem.Text = "Administrar Proyectos";
            verProyectoToolStripMenuItem.Click += verProyectoToolStripMenuItem_Click;
            // 
            // pnlContenedor
            // 
            pnlContenedor.Dock = DockStyle.Fill;
            pnlContenedor.Location = new Point(0, 29);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1561, 995);
            pnlContenedor.TabIndex = 9;
            // 
            // wProyectos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1561, 1024);
            Controls.Add(pnlContenedor);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "wProyectos";
            Text = "Proyectos";
            Load += wProyectos_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem agregarProyectoToolStripMenuItem;
        private ToolStripMenuItem verProyectoToolStripMenuItem;
        private Panel pnlContenedor;
    }
}