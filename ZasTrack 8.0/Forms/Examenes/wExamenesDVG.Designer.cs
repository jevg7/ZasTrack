namespace ZasTrack.Forms.Examenes
{
    partial class wExamenesDVG
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
            pnlContenedorDvg = new Panel();
            flpPacientes = new FlowLayoutPanel();
            lblPacientes = new Label();
            pnlProyecto = new Panel();
            pnlContenedorDvg.SuspendLayout();
            flpPacientes.SuspendLayout();
            SuspendLayout();
            // 
            // pnlContenedorDvg
            // 
            pnlContenedorDvg.Controls.Add(flpPacientes);
            pnlContenedorDvg.Dock = DockStyle.Bottom;
            pnlContenedorDvg.Location = new Point(0, 39);
            pnlContenedorDvg.Name = "pnlContenedorDvg";
            pnlContenedorDvg.Size = new Size(1353, 842);
            pnlContenedorDvg.TabIndex = 0;
            pnlContenedorDvg.Paint += pnlContenedorDvg_Paint;
            // 
            // flpPacientes
            // 
            flpPacientes.Controls.Add(lblPacientes);
            flpPacientes.Dock = DockStyle.Fill;
            flpPacientes.Location = new Point(0, 0);
            flpPacientes.Name = "flpPacientes";
            flpPacientes.Size = new Size(1353, 842);
            flpPacientes.TabIndex = 1;
            flpPacientes.Paint += flpPacientes_Paint;
            // 
            // lblPacientes
            // 
            lblPacientes.AutoSize = true;
            lblPacientes.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPacientes.Location = new Point(3, 0);
            lblPacientes.Name = "lblPacientes";
            lblPacientes.Size = new Size(102, 28);
            lblPacientes.TabIndex = 0;
            lblPacientes.Text = "Pacientes";
            // 
            // pnlProyecto
            // 
            pnlProyecto.Dock = DockStyle.Top;
            pnlProyecto.Location = new Point(0, 0);
            pnlProyecto.Name = "pnlProyecto";
            pnlProyecto.Size = new Size(1353, 33);
            pnlProyecto.TabIndex = 1;
            // 
            // wExamenesDVG
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1353, 881);
            Controls.Add(pnlProyecto);
            Controls.Add(pnlContenedorDvg);
            FormBorderStyle = FormBorderStyle.None;
            Name = "wExamenesDVG";
            Text = "wExamenesDVG";
            pnlContenedorDvg.ResumeLayout(false);
            flpPacientes.ResumeLayout(false);
            flpPacientes.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlContenedorDvg;
        private Panel pnlProyecto;
        private Label lblPacientes;
        private FlowLayoutPanel flpPacientes;
    }
}