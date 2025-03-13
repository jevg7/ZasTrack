namespace ZasTrack
{
    partial class wEstudiantes
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.agregarEstudiantesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarEliminarEstudiantesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlCntEstudiantes = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarEstudiantesToolStripMenuItem,
            this.editarEliminarEstudiantesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // agregarEstudiantesToolStripMenuItem
            // 
            this.agregarEstudiantesToolStripMenuItem.Name = "agregarEstudiantesToolStripMenuItem";
            this.agregarEstudiantesToolStripMenuItem.Size = new System.Drawing.Size(124, 20);
            this.agregarEstudiantesToolStripMenuItem.Text = "Agregar estudiantes";
            this.agregarEstudiantesToolStripMenuItem.Click += new System.EventHandler(this.agregarEstudiantesToolStripMenuItem_Click);
            // 
            // editarEliminarEstudiantesToolStripMenuItem
            // 
            this.editarEliminarEstudiantesToolStripMenuItem.Name = "editarEliminarEstudiantesToolStripMenuItem";
            this.editarEliminarEstudiantesToolStripMenuItem.Size = new System.Drawing.Size(160, 20);
            this.editarEliminarEstudiantesToolStripMenuItem.Text = "Editar/Eliminar estudiantes";
            // 
            // pnlCntEstudiantes
            // 
            this.pnlCntEstudiantes.Location = new System.Drawing.Point(0, 27);
            this.pnlCntEstudiantes.Name = "pnlCntEstudiantes";
            this.pnlCntEstudiantes.Size = new System.Drawing.Size(800, 424);
            this.pnlCntEstudiantes.TabIndex = 1;
            // 
            // wEstudiantes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlCntEstudiantes);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "wEstudiantes";
            this.Text = "wEstudiantes";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem agregarEstudiantesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarEliminarEstudiantesToolStripMenuItem;
        private System.Windows.Forms.Panel pnlCntEstudiantes;
    }
}