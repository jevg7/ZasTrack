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
            pnlMenu = new Panel();
            btnAgregarProyecto = new Button();
            btnDashBoard = new Button();
            pictureBox1 = new PictureBox();
            btnProyecto = new Button();
            btnReportes = new Button();
            btnExamenes = new Button();
            btnEstudiantes = new Button();
            pnlContenedor = new Panel();
            pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.White;
            pnlMenu.Controls.Add(btnAgregarProyecto);
            pnlMenu.Controls.Add(btnDashBoard);
            pnlMenu.Controls.Add(pictureBox1);
            pnlMenu.Controls.Add(btnProyecto);
            pnlMenu.Controls.Add(btnReportes);
            pnlMenu.Controls.Add(btnExamenes);
            pnlMenu.Controls.Add(btnEstudiantes);
            pnlMenu.Dock = DockStyle.Fill;
            pnlMenu.ForeColor = SystemColors.Control;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Margin = new Padding(0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(1381, 790);
            pnlMenu.TabIndex = 1;
            pnlMenu.Paint += panel1_Paint;
            // 
            // btnAgregarProyecto
            // 
            btnAgregarProyecto.BackColor = Color.Black;
            btnAgregarProyecto.Location = new Point(11, 190);
            btnAgregarProyecto.Name = "btnAgregarProyecto";
            btnAgregarProyecto.Size = new Size(151, 41);
            btnAgregarProyecto.TabIndex = 8;
            btnAgregarProyecto.Text = "Agregar Proyecto";
            btnAgregarProyecto.UseVisualStyleBackColor = false;
            btnAgregarProyecto.Click += btnAgregarProyecto_Click;
            // 
            // btnDashBoard
            // 
            btnDashBoard.BackColor = Color.Transparent;
            btnDashBoard.Cursor = Cursors.Hand;
            btnDashBoard.FlatAppearance.BorderSize = 0;
            btnDashBoard.FlatStyle = FlatStyle.Flat;
            btnDashBoard.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDashBoard.ForeColor = SystemColors.ActiveCaptionText;
            btnDashBoard.Location = new Point(17, 256);
            btnDashBoard.Margin = new Padding(2);
            btnDashBoard.Name = "btnDashBoard";
            btnDashBoard.Size = new Size(128, 42);
            btnDashBoard.TabIndex = 7;
            btnDashBoard.Text = "Dashboard";
            btnDashBoard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashBoard.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Image = Properties.Resources.Zas_log;
            pictureBox1.Location = new Point(11, 31);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(114, 101);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // btnProyecto
            // 
            btnProyecto.BackColor = Color.Transparent;
            btnProyecto.Cursor = Cursors.Hand;
            btnProyecto.FlatAppearance.BorderSize = 0;
            btnProyecto.FlatStyle = FlatStyle.Flat;
            btnProyecto.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnProyecto.ForeColor = SystemColors.ActiveCaptionText;
            btnProyecto.Location = new Point(17, 302);
            btnProyecto.Margin = new Padding(2);
            btnProyecto.Name = "btnProyecto";
            btnProyecto.Size = new Size(123, 48);
            btnProyecto.TabIndex = 4;
            btnProyecto.Text = "Proyecto";
            btnProyecto.TextAlign = ContentAlignment.MiddleLeft;
            btnProyecto.UseVisualStyleBackColor = false;
            btnProyecto.Click += btnProyecto_Click;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.Transparent;
            btnReportes.Cursor = Cursors.Hand;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReportes.ForeColor = SystemColors.ActiveCaptionText;
            btnReportes.Location = new Point(17, 487);
            btnReportes.Margin = new Padding(2);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(108, 46);
            btnReportes.TabIndex = 3;
            btnReportes.Text = "Reportes";
            btnReportes.TextAlign = ContentAlignment.MiddleLeft;
            btnReportes.UseVisualStyleBackColor = false;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnExamenes
            // 
            btnExamenes.BackColor = Color.Transparent;
            btnExamenes.Cursor = Cursors.Hand;
            btnExamenes.FlatAppearance.BorderSize = 0;
            btnExamenes.FlatStyle = FlatStyle.Flat;
            btnExamenes.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExamenes.ForeColor = SystemColors.ActiveCaptionText;
            btnExamenes.Location = new Point(17, 408);
            btnExamenes.Margin = new Padding(2);
            btnExamenes.Name = "btnExamenes";
            btnExamenes.Size = new Size(123, 55);
            btnExamenes.TabIndex = 2;
            btnExamenes.Text = "Examenes";
            btnExamenes.TextAlign = ContentAlignment.MiddleLeft;
            btnExamenes.UseVisualStyleBackColor = false;
            btnExamenes.Click += btnExamenes_Click;
            // 
            // btnEstudiantes
            // 
            btnEstudiantes.BackColor = Color.Transparent;
            btnEstudiantes.Cursor = Cursors.Hand;
            btnEstudiantes.FlatAppearance.BorderSize = 0;
            btnEstudiantes.FlatStyle = FlatStyle.Flat;
            btnEstudiantes.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEstudiantes.ForeColor = SystemColors.ActiveCaptionText;
            btnEstudiantes.Location = new Point(17, 354);
            btnEstudiantes.Margin = new Padding(2);
            btnEstudiantes.Name = "btnEstudiantes";
            btnEstudiantes.Size = new Size(123, 50);
            btnEstudiantes.TabIndex = 1;
            btnEstudiantes.Text = "Estudiantes";
            btnEstudiantes.TextAlign = ContentAlignment.MiddleLeft;
            btnEstudiantes.UseVisualStyleBackColor = false;
            btnEstudiantes.Click += btnEstudiantes_Click;
            // 
            // pnlContenedor
            // 
            pnlContenedor.Dock = DockStyle.Right;
            pnlContenedor.Location = new Point(178, 0);
            pnlContenedor.Margin = new Padding(4, 3, 4, 3);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1203, 790);
            pnlContenedor.TabIndex = 2;
            pnlContenedor.Paint += pnlContenedor_Paint;
            // 
            // wMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1381, 790);
            Controls.Add(pnlContenedor);
            Controls.Add(pnlMenu);
            Margin = new Padding(4, 3, 4, 3);
            Name = "wMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu Principal";
            pnlMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnProyecto;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnExamenes;
        private System.Windows.Forms.Button btnEstudiantes;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlContenedor;
        private Button btnDashBoard;
        private Button btnAgregarProyecto;
    }
}

