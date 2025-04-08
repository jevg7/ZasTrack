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
            mnsProyectos = new MenuStrip();
            tsmiExamenHeces = new ToolStripMenuItem();
            tsmiExamenSangre = new ToolStripMenuItem();
            tsmiExamenOrina = new ToolStripMenuItem();
            pnlContenedor = new Panel();
            mnsProyectos.SuspendLayout();
            SuspendLayout();
            // 
            // mnsProyectos
            // 
            mnsProyectos.BackColor = SystemColors.ActiveCaptionText;
            mnsProyectos.ImageScalingSize = new Size(20, 20);
            mnsProyectos.Items.AddRange(new ToolStripItem[] { tsmiExamenHeces, tsmiExamenSangre, tsmiExamenOrina });
            mnsProyectos.Location = new Point(0, 0);
            mnsProyectos.Name = "mnsProyectos";
            mnsProyectos.Size = new Size(1353, 28);
            mnsProyectos.TabIndex = 0;
            mnsProyectos.Text = "menuStrip1";
            // 
            // tsmiExamenHeces
            // 
            tsmiExamenHeces.ForeColor = SystemColors.Control;
            tsmiExamenHeces.Name = "tsmiExamenHeces";
            tsmiExamenHeces.Size = new Size(119, 24);
            tsmiExamenHeces.Text = "Examen Heces";
            tsmiExamenHeces.Click += tsmiExamenHeces_Click;
            // 
            // tsmiExamenSangre
            // 
            tsmiExamenSangre.ForeColor = SystemColors.ButtonHighlight;
            tsmiExamenSangre.Name = "tsmiExamenSangre";
            tsmiExamenSangre.Size = new Size(125, 24);
            tsmiExamenSangre.Text = "Examen Sangre";
            tsmiExamenSangre.Click += examenSangreToolStripMenuItem_Click;
            // 
            // tsmiExamenOrina
            // 
            tsmiExamenOrina.ForeColor = SystemColors.Control;
            tsmiExamenOrina.Name = "tsmiExamenOrina";
            tsmiExamenOrina.Size = new Size(115, 24);
            tsmiExamenOrina.Text = "Examen Orina";
            tsmiExamenOrina.Click += tsmiExamenOrina_Click;
            // 
            // pnlContenedor
            // 
            pnlContenedor.Location = new Point(0, 31);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1353, 847);
            pnlContenedor.TabIndex = 1;
            // 
            // wExamenes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1353, 881);
            Controls.Add(mnsProyectos);
            Controls.Add(pnlContenedor);
            FormBorderStyle = FormBorderStyle.None;
            MainMenuStrip = mnsProyectos;
            Margin = new Padding(3, 4, 3, 4);
            Name = "wExamenes";
            Text = "wExamenes";
            Load += wExamenes_Load;
            mnsProyectos.ResumeLayout(false);
            mnsProyectos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mnsProyectos;
        private ToolStripMenuItem tsmiExamenSangre;
        private ToolStripMenuItem tsmiExamenHeces;
        private ToolStripMenuItem tsmiExamenOrina;
        private Panel pnlContenedor;
    }
}