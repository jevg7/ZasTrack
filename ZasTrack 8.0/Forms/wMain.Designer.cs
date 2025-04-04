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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wMain));
            pnlMenu = new Panel();
            btnMuestras = new Button();
            btnDashBoard = new Button();
            pictureBox1 = new PictureBox();
            btnProyecto = new Button();
            btnInformes = new Button();
            btnExamenes = new Button();
            btnPacientes = new Button();
            pnlContenedor = new Panel();
            pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.White;
            pnlMenu.Controls.Add(btnMuestras);
            pnlMenu.Controls.Add(btnDashBoard);
            pnlMenu.Controls.Add(pictureBox1);
            pnlMenu.Controls.Add(btnProyecto);
            pnlMenu.Controls.Add(btnInformes);
            pnlMenu.Controls.Add(btnExamenes);
            pnlMenu.Controls.Add(btnPacientes);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.ForeColor = SystemColors.Control;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Margin = new Padding(0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(213, 996);
            pnlMenu.TabIndex = 1;
            pnlMenu.Paint += panel1_Paint;
            // 
            // btnMuestras
            // 
            btnMuestras.BackColor = Color.Transparent;
            btnMuestras.Cursor = Cursors.Hand;
            btnMuestras.FlatAppearance.BorderSize = 0;
            btnMuestras.FlatStyle = FlatStyle.Flat;
            btnMuestras.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMuestras.ForeColor = SystemColors.ActiveCaptionText;
            btnMuestras.Location = new Point(41, 411);
            btnMuestras.Margin = new Padding(2, 3, 2, 3);
            btnMuestras.Name = "btnMuestras";
            btnMuestras.Size = new Size(141, 53);
            btnMuestras.TabIndex = 4;
            btnMuestras.Text = "Muestras";
            btnMuestras.TextAlign = ContentAlignment.MiddleLeft;
            btnMuestras.UseVisualStyleBackColor = false;
            btnMuestras.Click += btnMuestras_Click;
            // 
            // btnDashBoard
            // 
            btnDashBoard.BackColor = Color.Transparent;
            btnDashBoard.Cursor = Cursors.Hand;
            btnDashBoard.FlatAppearance.BorderSize = 0;
            btnDashBoard.FlatStyle = FlatStyle.Flat;
            btnDashBoard.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDashBoard.ForeColor = SystemColors.ActiveCaptionText;
            btnDashBoard.Location = new Point(39, 205);
            btnDashBoard.Margin = new Padding(2, 3, 2, 3);
            btnDashBoard.Name = "btnDashBoard";
            btnDashBoard.Size = new Size(138, 53);
            btnDashBoard.TabIndex = 7;
            btnDashBoard.Text = "Dashboard";
            btnDashBoard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashBoard.UseVisualStyleBackColor = false;
            btnDashBoard.Click += btnDashBoard_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Image = Properties.Resources.Zas_log;
            pictureBox1.Location = new Point(49, 16);
            pictureBox1.Margin = new Padding(5, 4, 5, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(128, 141);
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
            btnProyecto.Location = new Point(41, 269);
            btnProyecto.Margin = new Padding(2, 3, 2, 3);
            btnProyecto.Name = "btnProyecto";
            btnProyecto.Size = new Size(112, 53);
            btnProyecto.TabIndex = 4;
            btnProyecto.Text = "Proyecto";
            btnProyecto.TextAlign = ContentAlignment.MiddleLeft;
            btnProyecto.UseVisualStyleBackColor = false;
            btnProyecto.Click += btnProyecto_Click;
            // 
            // btnInformes
            // 
            btnInformes.BackColor = Color.Transparent;
            btnInformes.Cursor = Cursors.Hand;
            btnInformes.FlatAppearance.BorderSize = 0;
            btnInformes.FlatStyle = FlatStyle.Flat;
            btnInformes.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInformes.ForeColor = SystemColors.ActiveCaptionText;
            btnInformes.Location = new Point(41, 562);
            btnInformes.Margin = new Padding(2, 3, 2, 3);
            btnInformes.Name = "btnInformes";
            btnInformes.Size = new Size(141, 53);
            btnInformes.TabIndex = 3;
            btnInformes.Text = "Informes";
            btnInformes.TextAlign = ContentAlignment.MiddleLeft;
            btnInformes.UseVisualStyleBackColor = false;
            btnInformes.Click += btnReportes_Click;
            // 
            // btnExamenes
            // 
            btnExamenes.BackColor = Color.Transparent;
            btnExamenes.Cursor = Cursors.Hand;
            btnExamenes.FlatAppearance.BorderSize = 0;
            btnExamenes.FlatStyle = FlatStyle.Flat;
            btnExamenes.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExamenes.ForeColor = SystemColors.ActiveCaptionText;
            btnExamenes.Location = new Point(41, 482);
            btnExamenes.Margin = new Padding(2, 3, 2, 3);
            btnExamenes.Name = "btnExamenes";
            btnExamenes.Size = new Size(133, 53);
            btnExamenes.TabIndex = 2;
            btnExamenes.Text = "Examenes";
            btnExamenes.TextAlign = ContentAlignment.MiddleLeft;
            btnExamenes.UseVisualStyleBackColor = false;
            btnExamenes.Click += btnExamenes_Click;
            // 
            // btnPacientes
            // 
            btnPacientes.BackColor = Color.Transparent;
            btnPacientes.Cursor = Cursors.Hand;
            btnPacientes.FlatAppearance.BorderSize = 0;
            btnPacientes.FlatStyle = FlatStyle.Flat;
            btnPacientes.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPacientes.ForeColor = SystemColors.ActiveCaptionText;
            btnPacientes.Location = new Point(39, 339);
            btnPacientes.Margin = new Padding(2, 3, 2, 3);
            btnPacientes.Name = "btnPacientes";
            btnPacientes.Size = new Size(141, 53);
            btnPacientes.TabIndex = 1;
            btnPacientes.Text = "Pacientes";
            btnPacientes.TextAlign = ContentAlignment.MiddleLeft;
            btnPacientes.UseVisualStyleBackColor = false;
            btnPacientes.Click += btnEstudiantes_Click;
            // 
            // pnlContenedor
            // 
            pnlContenedor.Location = new Point(217, 0);
            pnlContenedor.Margin = new Padding(5, 4, 5, 4);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1361, 1053);
            pnlContenedor.TabIndex = 2;
            pnlContenedor.Paint += pnlContenedor_Paint;
            // 
            // wMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1543, 996);
            Controls.Add(pnlMenu);
            Controls.Add(pnlContenedor);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
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
        private System.Windows.Forms.Button btnInformes;
        private System.Windows.Forms.Button btnExamenes;
        private System.Windows.Forms.Button btnPacientes;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlContenedor;
        private Button btnDashBoard;
        private Button btnMuestras;
    }
}

