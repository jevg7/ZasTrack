namespace ZasTrack
{
    partial class wPaciente
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
            mnsPacientes = new MenuStrip();
            importarPacientesToolStripMenuItem = new ToolStripMenuItem();
            agregarEstudiantesToolStripMenuItem = new ToolStripMenuItem();
            verEstudiantesToolStripMenuItem = new ToolStripMenuItem();
            pnlCntEstudiantes = new Panel();
            mnsPacientes.SuspendLayout();
            SuspendLayout();
            // 
            // mnsPacientes
            // 
            mnsPacientes.ImageScalingSize = new Size(20, 20);
            mnsPacientes.Items.AddRange(new ToolStripItem[] { importarPacientesToolStripMenuItem, agregarEstudiantesToolStripMenuItem, verEstudiantesToolStripMenuItem });
            mnsPacientes.Location = new Point(0, 0);
            mnsPacientes.Name = "mnsPacientes";
            mnsPacientes.Padding = new Padding(8, 3, 0, 3);
            mnsPacientes.Size = new Size(1066, 30);
            mnsPacientes.TabIndex = 0;
            mnsPacientes.Text = "menuStrip1";
            // 
            // importarPacientesToolStripMenuItem
            // 
            importarPacientesToolStripMenuItem.Name = "importarPacientesToolStripMenuItem";
            importarPacientesToolStripMenuItem.Size = new Size(146, 24);
            importarPacientesToolStripMenuItem.Text = "Importar Pacientes";
            importarPacientesToolStripMenuItem.Click += importarPacientesToolStripMenuItem_Click;
            // 
            // agregarEstudiantesToolStripMenuItem
            // 
            agregarEstudiantesToolStripMenuItem.Name = "agregarEstudiantesToolStripMenuItem";
            agregarEstudiantesToolStripMenuItem.Size = new Size(142, 24);
            agregarEstudiantesToolStripMenuItem.Text = "Agregar Pacientes";
            agregarEstudiantesToolStripMenuItem.Click += agregarEstudiantesToolStripMenuItem_Click;
            // 
            // verEstudiantesToolStripMenuItem
            // 
            verEstudiantesToolStripMenuItem.Name = "verEstudiantesToolStripMenuItem";
            verEstudiantesToolStripMenuItem.Size = new Size(109, 24);
            verEstudiantesToolStripMenuItem.Text = "Ver Pacientes";
            verEstudiantesToolStripMenuItem.Click += verEstudiantesToolStripMenuItem_Click;
            // 
            // pnlCntEstudiantes
            // 
            pnlCntEstudiantes.Dock = DockStyle.Fill;
            pnlCntEstudiantes.Location = new Point(0, 30);
            pnlCntEstudiantes.Margin = new Padding(5, 4, 5, 4);
            pnlCntEstudiantes.Name = "pnlCntEstudiantes";
            pnlCntEstudiantes.Size = new Size(1066, 662);
            pnlCntEstudiantes.TabIndex = 1;
            pnlCntEstudiantes.Paint += pnlCntEstudiantes_Paint;
            // 
            // wPaciente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1066, 692);
            Controls.Add(pnlCntEstudiantes);
            Controls.Add(mnsPacientes);
            FormBorderStyle = FormBorderStyle.None;
            MainMenuStrip = mnsPacientes;
            Margin = new Padding(5, 4, 5, 4);
            Name = "wPaciente";
            Text = "wEstudiantes";
            Load += wPaciente_Load;
            mnsPacientes.ResumeLayout(false);
            mnsPacientes.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip mnsPacientes;
        private System.Windows.Forms.ToolStripMenuItem agregarEstudiantesToolStripMenuItem;
        private System.Windows.Forms.Panel pnlCntEstudiantes;
        private ToolStripMenuItem verEstudiantesToolStripMenuItem;
        private ToolStripMenuItem importarPacientesToolStripMenuItem;
    }
}