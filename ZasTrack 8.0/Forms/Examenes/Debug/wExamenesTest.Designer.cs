namespace ZasTrack.Forms.Examenes
{
    partial class wExamenesTest
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
            mnsProcesados = new MenuStrip();
            procesadosToolStripMenuItem = new ToolStripMenuItem();
            noProcesadosToolStripMenuItem = new ToolStripMenuItem();
            pnlContenedor = new Panel();
            panel1 = new Panel();
            label2 = new Label();
            cmbProyecto = new ComboBox();
            mnsProcesados.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // mnsProcesados
            // 
            mnsProcesados.ImageScalingSize = new Size(20, 20);
            mnsProcesados.Items.AddRange(new ToolStripItem[] { procesadosToolStripMenuItem, noProcesadosToolStripMenuItem });
            mnsProcesados.Location = new Point(0, 0);
            mnsProcesados.Name = "mnsProcesados";
            mnsProcesados.Size = new Size(1353, 28);
            mnsProcesados.TabIndex = 0;
            mnsProcesados.Text = "menuStrip1";
            // 
            // procesadosToolStripMenuItem
            // 
            procesadosToolStripMenuItem.Name = "procesadosToolStripMenuItem";
            procesadosToolStripMenuItem.Size = new Size(98, 24);
            procesadosToolStripMenuItem.Text = "Procesados";
            procesadosToolStripMenuItem.Click += procesadosToolStripMenuItem_Click;
            // 
            // noProcesadosToolStripMenuItem
            // 
            noProcesadosToolStripMenuItem.Name = "noProcesadosToolStripMenuItem";
            noProcesadosToolStripMenuItem.Size = new Size(122, 24);
            noProcesadosToolStripMenuItem.Text = "No Procesados";
            noProcesadosToolStripMenuItem.Click += noProcesadosToolStripMenuItem_Click_1;
            // 
            // pnlContenedor
            // 
            pnlContenedor.Dock = DockStyle.Bottom;
            pnlContenedor.Location = new Point(0, 67);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1353, 814);
            pnlContenedor.TabIndex = 1;
            pnlContenedor.Paint += panel1_Paint;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Controls.Add(label2);
            panel1.Controls.Add(cmbProyecto);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 28);
            panel1.Name = "panel1";
            panel1.Size = new Size(1353, 41);
            panel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 39;
            label2.Text = "Proyecto";
            // 
            // cmbProyecto
            // 
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(99, 4);
            cmbProyecto.Margin = new Padding(3, 4, 3, 4);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(138, 28);
            cmbProyecto.TabIndex = 38;
            cmbProyecto.SelectedIndexChanged += this.cmbProyecto_SelectedIndexChanged;
            // 
            // wExamenesTest
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1353, 881);
            Controls.Add(panel1);
            Controls.Add(pnlContenedor);
            Controls.Add(mnsProcesados);
            FormBorderStyle = FormBorderStyle.None;
            MainMenuStrip = mnsProcesados;
            Name = "wExamenesTest";
            Text = "wExamenesTest";
            mnsProcesados.ResumeLayout(false);
            mnsProcesados.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mnsProcesados;
        private ToolStripMenuItem procesadosToolStripMenuItem;
        private ToolStripMenuItem noProcesadosToolStripMenuItem;
        private Panel pnlContenedor;
        private Panel panel1;
        private Label label2;
        private ComboBox cmbProyecto;
    }
}