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
            menuStrip1 = new MenuStrip();
            agregarEstudiantesToolStripMenuItem = new ToolStripMenuItem();
            editarEliminarEstudiantesToolStripMenuItem = new ToolStripMenuItem();
            verEstudiantesToolStripMenuItem = new ToolStripMenuItem();
            pnlCntEstudiantes = new Panel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { agregarEstudiantesToolStripMenuItem, editarEliminarEstudiantesToolStripMenuItem, verEstudiantesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 2, 0, 2);
            menuStrip1.Size = new Size(933, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // agregarEstudiantesToolStripMenuItem
            // 
            agregarEstudiantesToolStripMenuItem.Name = "agregarEstudiantesToolStripMenuItem";
            agregarEstudiantesToolStripMenuItem.Size = new Size(114, 20);
            agregarEstudiantesToolStripMenuItem.Text = "Agregar Pacientes";
            agregarEstudiantesToolStripMenuItem.Click += agregarEstudiantesToolStripMenuItem_Click;
            // 
            // editarEliminarEstudiantesToolStripMenuItem
            // 
            editarEliminarEstudiantesToolStripMenuItem.Name = "editarEliminarEstudiantesToolStripMenuItem";
            editarEliminarEstudiantesToolStripMenuItem.Size = new Size(150, 20);
            editarEliminarEstudiantesToolStripMenuItem.Text = "Editar/Eliminar Pacientes";
            editarEliminarEstudiantesToolStripMenuItem.Click += editarEliminarEstudiantesToolStripMenuItem_Click;
            // 
            // verEstudiantesToolStripMenuItem
            // 
            verEstudiantesToolStripMenuItem.Name = "verEstudiantesToolStripMenuItem";
            verEstudiantesToolStripMenuItem.Size = new Size(88, 20);
            verEstudiantesToolStripMenuItem.Text = "Ver Pacientes";
            verEstudiantesToolStripMenuItem.Click += verEstudiantesToolStripMenuItem_Click;
            // 
            // pnlCntEstudiantes
            // 
            pnlCntEstudiantes.Location = new Point(0, 31);
            pnlCntEstudiantes.Margin = new Padding(4, 3, 4, 3);
            pnlCntEstudiantes.Name = "pnlCntEstudiantes";
            pnlCntEstudiantes.Size = new Size(1184, 792);
            pnlCntEstudiantes.TabIndex = 1;
            pnlCntEstudiantes.Paint += pnlCntEstudiantes_Paint;
            // 
            // wPaciente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(pnlCntEstudiantes);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.None;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            Name = "wPaciente";
            Text = "wEstudiantes";
            Load += wPaciente_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem agregarEstudiantesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarEliminarEstudiantesToolStripMenuItem;
        private System.Windows.Forms.Panel pnlCntEstudiantes;
        private ToolStripMenuItem verEstudiantesToolStripMenuItem;
    }
}