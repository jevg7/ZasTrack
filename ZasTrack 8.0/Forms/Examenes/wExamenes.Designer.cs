namespace ZasTrack.Forms.Examenes
{
    partial class wExamenes
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
            menuStrip1 = new MenuStrip();
            tsmiExamenHeces = new ToolStripMenuItem();
            tsmiExamenSangre = new ToolStripMenuItem();
            tsmiExamenOrina = new ToolStripMenuItem();
            pnlContenedor = new Panel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { tsmiExamenHeces, tsmiExamenSangre, tsmiExamenOrina });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1353, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // tsmiExamenHeces
            // 
            tsmiExamenHeces.Name = "tsmiExamenHeces";
            tsmiExamenHeces.Size = new Size(119, 24);
            tsmiExamenHeces.Text = "Examen Heces";
            tsmiExamenHeces.Click += tsmiExamenHeces_Click;
            // 
            // tsmiExamenSangre
            // 
            tsmiExamenSangre.Name = "tsmiExamenSangre";
            tsmiExamenSangre.Size = new Size(125, 24);
            tsmiExamenSangre.Text = "Examen Sangre";
            tsmiExamenSangre.Click += examenSangreToolStripMenuItem_Click;
            // 
            // tsmiExamenOrina
            // 
            tsmiExamenOrina.Name = "tsmiExamenOrina";
            tsmiExamenOrina.Size = new Size(115, 24);
            tsmiExamenOrina.Text = "Examen Orina";
            tsmiExamenOrina.Click += tsmiExamenOrina_Click;
            // 
            // pnlContenedor
            // 
            pnlContenedor.Dock = DockStyle.Fill;
            pnlContenedor.Location = new Point(0, 28);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1353, 853);
            pnlContenedor.TabIndex = 1;
            // 
            // wExamenes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1353, 881);
            Controls.Add(pnlContenedor);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.None;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "wExamenes";
            Text = "wExamenes";
            Load += wExamenes_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem tsmiExamenSangre;
        private ToolStripMenuItem tsmiExamenHeces;
        private ToolStripMenuItem tsmiExamenOrina;
        private Panel pnlContenedor;
    }
}