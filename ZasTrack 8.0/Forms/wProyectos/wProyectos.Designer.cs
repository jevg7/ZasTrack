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
            msProyectos = new MenuStrip();
            tsmiAñadirProyectos = new ToolStripMenuItem();
            pnlProyectos = new Panel();
            msProyectos.SuspendLayout();
            SuspendLayout();
            // 
            // msProyectos
            // 
            msProyectos.Items.AddRange(new ToolStripItem[] { tsmiAñadirProyectos });
            msProyectos.Location = new Point(0, 0);
            msProyectos.Name = "msProyectos";
            msProyectos.Size = new Size(800, 24);
            msProyectos.TabIndex = 0;
            msProyectos.Text = "msProyectos";
            msProyectos.ItemClicked += menuStrip1_ItemClicked;
            // 
            // tsmiAñadirProyectos
            // 
            tsmiAñadirProyectos.Name = "tsmiAñadirProyectos";
            tsmiAñadirProyectos.Size = new Size(109, 20);
            tsmiAñadirProyectos.Text = "Añadir Proyectos";
            tsmiAñadirProyectos.Click += tsmiAñadirProyectos_Click;
            // 
            // pnlProyectos
            // 
            pnlProyectos.Dock = DockStyle.Fill;
            pnlProyectos.Location = new Point(0, 24);
            pnlProyectos.Name = "pnlProyectos";
            pnlProyectos.Size = new Size(800, 426);
            pnlProyectos.TabIndex = 1;
            pnlProyectos.Paint += pnlProyectos_Paint;
            // 
            // wProyectos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlProyectos);
            Controls.Add(msProyectos);
            FormBorderStyle = FormBorderStyle.None;
            MainMenuStrip = msProyectos;
            Name = "wProyectos";
            Text = "Proyectos";
            Load += wProyectos_Load;
            msProyectos.ResumeLayout(false);
            msProyectos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip msProyectos;
        private ToolStripMenuItem tsmiAñadirProyectos;
        private Panel pnlProyectos;
    }
}