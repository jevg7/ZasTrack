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
            lblAdmProyec = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            menuStrip1 = new MenuStrip();
            agregarProyectoToolStripMenuItem = new ToolStripMenuItem();
            editarProyectoToolStripMenuItem = new ToolStripMenuItem();
            eliminarProyectoToolStripMenuItem = new ToolStripMenuItem();
            verProyectoToolStripMenuItem = new ToolStripMenuItem();
            pnlContenedor = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblAdmProyec
            // 
            lblAdmProyec.AutoSize = true;
            lblAdmProyec.Location = new Point(14, 12);
            lblAdmProyec.Name = "lblAdmProyec";
            lblAdmProyec.Size = new Size(193, 20);
            lblAdmProyec.TabIndex = 0;
            lblAdmProyec.Text = "Administrador de Proyectos";
            lblAdmProyec.Click += lblAdmProyec_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(lblAdmProyec);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1561, 37);
            panel1.TabIndex = 3;
            panel1.Paint += splitContainer1_Panel1_Paint_2;
            // 
            // panel2
            // 
            panel2.Controls.Add(menuStrip1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 37);
            panel2.Name = "panel2";
            panel2.Size = new Size(1561, 29);
            panel2.TabIndex = 8;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { agregarProyectoToolStripMenuItem, editarProyectoToolStripMenuItem, eliminarProyectoToolStripMenuItem, verProyectoToolStripMenuItem });
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
            // editarProyectoToolStripMenuItem
            // 
            editarProyectoToolStripMenuItem.Name = "editarProyectoToolStripMenuItem";
            editarProyectoToolStripMenuItem.Size = new Size(124, 24);
            editarProyectoToolStripMenuItem.Text = "Editar Proyecto";
            editarProyectoToolStripMenuItem.Click += editarProyectoToolStripMenuItem_Click;
            // 
            // eliminarProyectoToolStripMenuItem
            // 
            eliminarProyectoToolStripMenuItem.Name = "eliminarProyectoToolStripMenuItem";
            eliminarProyectoToolStripMenuItem.Size = new Size(139, 24);
            eliminarProyectoToolStripMenuItem.Text = "Eliminar Proyecto";
            eliminarProyectoToolStripMenuItem.Click += eliminarProyectoToolStripMenuItem_Click;
            // 
            // verProyectoToolStripMenuItem
            // 
            verProyectoToolStripMenuItem.Name = "verProyectoToolStripMenuItem";
            verProyectoToolStripMenuItem.Size = new Size(106, 24);
            verProyectoToolStripMenuItem.Text = "Ver Proyecto";
            verProyectoToolStripMenuItem.Click += verProyectoToolStripMenuItem_Click;
            // 
            // pnlContenedor
            // 
            pnlContenedor.Dock = DockStyle.Fill;
            pnlContenedor.Location = new Point(0, 66);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1561, 958);
            pnlContenedor.TabIndex = 9;
            pnlContenedor.Paint += pnlContenedor_Paint;
            // 
            // wProyectos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1561, 1024);
            Controls.Add(pnlContenedor);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "wProyectos";
            Text = "Proyectos";
            Load += wProyectos_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label lblAdmProyec;
        private Panel panel1;
        private Panel panel2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem agregarProyectoToolStripMenuItem;
        private ToolStripMenuItem editarProyectoToolStripMenuItem;
        private ToolStripMenuItem eliminarProyectoToolStripMenuItem;
        private ToolStripMenuItem verProyectoToolStripMenuItem;
        private Panel pnlContenedor;
    }
}