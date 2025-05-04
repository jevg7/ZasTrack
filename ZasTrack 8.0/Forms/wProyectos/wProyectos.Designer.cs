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
            pnlMenuStrip = new Panel();
            mnsProyectos = new MenuStrip();
            agregarProyectoToolStripMenuItem = new ToolStripMenuItem();
            verProyectoToolStripMenuItem = new ToolStripMenuItem();
            pnlContenedor = new Panel();
            pnlMenuStrip.SuspendLayout();
            mnsProyectos.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenuStrip
            // 
            pnlMenuStrip.Controls.Add(mnsProyectos);
            pnlMenuStrip.Dock = DockStyle.Top;
            pnlMenuStrip.Location = new Point(0, 0);
            pnlMenuStrip.Name = "pnlMenuStrip";
            pnlMenuStrip.Size = new Size(1561, 29);
            pnlMenuStrip.TabIndex = 8;
            // 
            // mnsProyectos
            // 
            mnsProyectos.ImageScalingSize = new Size(20, 20);
            mnsProyectos.Items.AddRange(new ToolStripItem[] { agregarProyectoToolStripMenuItem, verProyectoToolStripMenuItem });
            mnsProyectos.Location = new Point(0, 0);
            mnsProyectos.Name = "mnsProyectos";
            mnsProyectos.Size = new Size(1561, 28);
            mnsProyectos.TabIndex = 0;
            mnsProyectos.Text = "menuStrip1";
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
            Controls.Add(pnlMenuStrip);
            FormBorderStyle = FormBorderStyle.None;
            MainMenuStrip = mnsProyectos;
            Margin = new Padding(3, 4, 3, 4);
            Name = "wProyectos";
            Text = "Proyectos";
            Load += wProyectos_Load;
            pnlMenuStrip.ResumeLayout(false);
            pnlMenuStrip.PerformLayout();
            mnsProyectos.ResumeLayout(false);
            mnsProyectos.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlMenuStrip;
        private MenuStrip mnsProyectos;
        private ToolStripMenuItem agregarProyectoToolStripMenuItem;
        private ToolStripMenuItem verProyectoToolStripMenuItem;
        private Panel pnlContenedor;
    }
}