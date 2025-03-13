namespace ZasTrack
{
    partial class wMain
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
            panel1 = new Panel();
            btnProbar = new Button();
            pictureBox1 = new PictureBox();
            btnProyecto = new Button();
            btnReportes = new Button();
            btnExamenes = new Button();
            btnEstudiantes = new Button();
            pnlContenedor = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel1.BackColor = Color.FromArgb(27, 27, 27);
            panel1.Controls.Add(btnProbar);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(btnProyecto);
            panel1.Controls.Add(btnReportes);
            panel1.Controls.Add(btnExamenes);
            panel1.Controls.Add(btnEstudiantes);
            panel1.Location = new Point(-1, 2);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 787);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // btnProbar
            // 
            btnProbar.Location = new Point(13, 540);
            btnProbar.Name = "btnProbar";
            btnProbar.Size = new Size(158, 119);
            btnProbar.TabIndex = 7;
            btnProbar.Text = "Probar Conexion";
            btnProbar.UseVisualStyleBackColor = true;
            btnProbar.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(41, 37);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(114, 101);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // btnProyecto
            // 
            btnProyecto.Cursor = Cursors.Hand;
            btnProyecto.FlatAppearance.BorderSize = 0;
            btnProyecto.FlatStyle = FlatStyle.Flat;
            btnProyecto.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnProyecto.ForeColor = SystemColors.ControlLightLight;
            btnProyecto.Location = new Point(0, 457);
            btnProyecto.Margin = new Padding(2);
            btnProyecto.Name = "btnProyecto";
            btnProyecto.Size = new Size(197, 78);
            btnProyecto.TabIndex = 4;
            btnProyecto.Text = "Proyecto";
            btnProyecto.TextAlign = ContentAlignment.MiddleLeft;
            btnProyecto.UseVisualStyleBackColor = true;
            btnProyecto.Click += btnProyecto_Click;
            // 
            // btnReportes
            // 
            btnReportes.Cursor = Cursors.Hand;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReportes.ForeColor = SystemColors.ControlLightLight;
            btnReportes.Location = new Point(0, 362);
            btnReportes.Margin = new Padding(2);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(197, 78);
            btnReportes.TabIndex = 3;
            btnReportes.Text = "Reportes";
            btnReportes.TextAlign = ContentAlignment.MiddleLeft;
            btnReportes.UseVisualStyleBackColor = true;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnExamenes
            // 
            btnExamenes.Cursor = Cursors.Hand;
            btnExamenes.FlatAppearance.BorderSize = 0;
            btnExamenes.FlatStyle = FlatStyle.Flat;
            btnExamenes.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExamenes.ForeColor = SystemColors.ControlLightLight;
            btnExamenes.Location = new Point(0, 270);
            btnExamenes.Margin = new Padding(2);
            btnExamenes.Name = "btnExamenes";
            btnExamenes.Size = new Size(197, 78);
            btnExamenes.TabIndex = 2;
            btnExamenes.Text = "Examenes";
            btnExamenes.TextAlign = ContentAlignment.MiddleLeft;
            btnExamenes.UseVisualStyleBackColor = true;
            btnExamenes.Click += btnExamenes_Click;
            // 
            // btnEstudiantes
            // 
            btnEstudiantes.Cursor = Cursors.Hand;
            btnEstudiantes.FlatAppearance.BorderSize = 0;
            btnEstudiantes.FlatStyle = FlatStyle.Flat;
            btnEstudiantes.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEstudiantes.ForeColor = SystemColors.ControlLightLight;
            btnEstudiantes.Location = new Point(-2, 187);
            btnEstudiantes.Margin = new Padding(2);
            btnEstudiantes.Name = "btnEstudiantes";
            btnEstudiantes.Size = new Size(197, 78);
            btnEstudiantes.TabIndex = 1;
            btnEstudiantes.Text = "Estudiantes";
            btnEstudiantes.TextAlign = ContentAlignment.MiddleLeft;
            btnEstudiantes.UseVisualStyleBackColor = true;
            btnEstudiantes.Click += btnEstudiantes_Click;
            // 
            // pnlContenedor
            // 
            pnlContenedor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlContenedor.Location = new Point(200, 0);
            pnlContenedor.Margin = new Padding(4, 3, 4, 3);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1184, 792);
            pnlContenedor.TabIndex = 2;
            // 
            // wMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1381, 790);
            Controls.Add(pnlContenedor);
            Controls.Add(panel1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "wMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu Principal";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnProyecto;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnExamenes;
        private System.Windows.Forms.Button btnEstudiantes;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlContenedor;
        private Button btnProbar;
    }
}

