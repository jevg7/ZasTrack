namespace ZasTrack.Forms.Dashboard
{
    partial class wDashboard
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
            lblBienvDash = new Label();
            lblBienvenido = new Label();
            tlpIndices = new TableLayoutPanel();
            pnlInformes = new Panel();
            lblInfomes = new Label();
            pnlExamenesRev = new Panel();
            lblExamenesRev = new Label();
            pnlPacientesTotal = new Panel();
            lblPacientesTotal = new Label();
            pnlMuestrasDia = new Panel();
            lblMuestrasDia = new Label();
            cboProyecto = new ComboBox();
            tlpGraficas = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            pnlMuestrasProc = new Panel();
            pnlTipoExamenes = new Panel();
            pnlMuestrasUltimas = new Panel();
            pnlExamenesUltimos = new Panel();
            lblMuestrasProcesadas = new Label();
            lblExamenesTipos = new Label();
            lblMuestrasUltimas = new Label();
            lblExamenesUltimos = new Label();
            tlpIndices.SuspendLayout();
            pnlInformes.SuspendLayout();
            pnlExamenesRev.SuspendLayout();
            pnlPacientesTotal.SuspendLayout();
            pnlMuestrasDia.SuspendLayout();
            tlpGraficas.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            pnlMuestrasProc.SuspendLayout();
            pnlTipoExamenes.SuspendLayout();
            pnlMuestrasUltimas.SuspendLayout();
            pnlExamenesUltimos.SuspendLayout();
            SuspendLayout();
            // 
            // lblBienvDash
            // 
            lblBienvDash.AutoSize = true;
            lblBienvDash.Font = new Font("Segoe UI Emoji", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBienvDash.Location = new Point(41, 41);
            lblBienvDash.Name = "lblBienvDash";
            lblBienvDash.Size = new Size(138, 32);
            lblBienvDash.TabIndex = 0;
            lblBienvDash.Text = "Dashboard";
            // 
            // lblBienvenido
            // 
            lblBienvenido.AutoSize = true;
            lblBienvenido.Font = new Font("Segoe UI", 12F);
            lblBienvenido.Location = new Point(47, 73);
            lblBienvenido.Name = "lblBienvenido";
            lblBienvenido.Size = new Size(316, 21);
            lblBienvenido.TabIndex = 1;
            lblBienvenido.Text = "Bienvenido al Sistema de Gestión de ZabLab";
            // 
            // tlpIndices
            // 
            tlpIndices.ColumnCount = 4;
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.3861F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.6139F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 272F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 259F));
            tlpIndices.Controls.Add(pnlInformes, 3, 0);
            tlpIndices.Controls.Add(pnlExamenesRev, 2, 0);
            tlpIndices.Controls.Add(pnlPacientesTotal, 0, 0);
            tlpIndices.Controls.Add(pnlMuestrasDia, 1, 0);
            tlpIndices.Location = new Point(47, 140);
            tlpIndices.Name = "tlpIndices";
            tlpIndices.RowCount = 1;
            tlpIndices.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpIndices.Size = new Size(1100, 140);
            tlpIndices.TabIndex = 4;
            // 
            // pnlInformes
            // 
            pnlInformes.Controls.Add(lblInfomes);
            pnlInformes.Location = new Point(843, 3);
            pnlInformes.Name = "pnlInformes";
            pnlInformes.Size = new Size(252, 134);
            pnlInformes.TabIndex = 6;
            // 
            // lblInfomes
            // 
            lblInfomes.AutoSize = true;
            lblInfomes.Location = new Point(17, 11);
            lblInfomes.Name = "lblInfomes";
            lblInfomes.Size = new Size(95, 15);
            lblInfomes.TabIndex = 0;
            lblInfomes.Text = "Informes del dia:";
            // 
            // pnlExamenesRev
            // 
            pnlExamenesRev.Controls.Add(lblExamenesRev);
            pnlExamenesRev.Location = new Point(571, 3);
            pnlExamenesRev.Name = "pnlExamenesRev";
            pnlExamenesRev.Size = new Size(252, 134);
            pnlExamenesRev.TabIndex = 6;
            // 
            // lblExamenesRev
            // 
            lblExamenesRev.AutoSize = true;
            lblExamenesRev.Location = new Point(14, 11);
            lblExamenesRev.Name = "lblExamenesRev";
            lblExamenesRev.Size = new Size(109, 15);
            lblExamenesRev.TabIndex = 0;
            lblExamenesRev.Text = "Examenes a Revisar";
            // 
            // pnlPacientesTotal
            // 
            pnlPacientesTotal.Controls.Add(lblPacientesTotal);
            pnlPacientesTotal.Location = new Point(3, 3);
            pnlPacientesTotal.Name = "pnlPacientesTotal";
            pnlPacientesTotal.Size = new Size(252, 134);
            pnlPacientesTotal.TabIndex = 5;
            // 
            // lblPacientesTotal
            // 
            lblPacientesTotal.AutoSize = true;
            lblPacientesTotal.Location = new Point(3, 11);
            lblPacientesTotal.Name = "lblPacientesTotal";
            lblPacientesTotal.Size = new Size(88, 15);
            lblPacientesTotal.TabIndex = 0;
            lblPacientesTotal.Text = "Total Pacientes:";
            // 
            // pnlMuestrasDia
            // 
            pnlMuestrasDia.Controls.Add(lblMuestrasDia);
            pnlMuestrasDia.Location = new Point(289, 3);
            pnlMuestrasDia.Name = "pnlMuestrasDia";
            pnlMuestrasDia.Size = new Size(252, 134);
            pnlMuestrasDia.TabIndex = 6;
            // 
            // lblMuestrasDia
            // 
            lblMuestrasDia.AutoSize = true;
            lblMuestrasDia.Location = new Point(17, 11);
            lblMuestrasDia.Name = "lblMuestrasDia";
            lblMuestrasDia.Size = new Size(96, 15);
            lblMuestrasDia.TabIndex = 0;
            lblMuestrasDia.Text = "Muestras de Hoy";
            // 
            // cboProyecto
            // 
            cboProyecto.FormattingEnabled = true;
            cboProyecto.Location = new Point(50, 111);
            cboProyecto.Name = "cboProyecto";
            cboProyecto.Size = new Size(121, 23);
            cboProyecto.TabIndex = 5;
            // 
            // tlpGraficas
            // 
            tlpGraficas.ColumnCount = 2;
            tlpGraficas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpGraficas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpGraficas.Controls.Add(pnlMuestrasProc, 0, 0);
            tlpGraficas.Controls.Add(pnlTipoExamenes, 1, 0);
            tlpGraficas.Location = new Point(47, 286);
            tlpGraficas.Name = "tlpGraficas";
            tlpGraficas.RowCount = 1;
            tlpGraficas.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpGraficas.Size = new Size(1100, 232);
            tlpGraficas.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(pnlMuestrasUltimas, 0, 0);
            tableLayoutPanel2.Controls.Add(pnlExamenesUltimos, 1, 0);
            tableLayoutPanel2.Location = new Point(47, 524);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(1100, 170);
            tableLayoutPanel2.TabIndex = 7;
            // 
            // pnlMuestrasProc
            // 
            pnlMuestrasProc.Controls.Add(lblMuestrasProcesadas);
            pnlMuestrasProc.Dock = DockStyle.Fill;
            pnlMuestrasProc.Location = new Point(3, 3);
            pnlMuestrasProc.Name = "pnlMuestrasProc";
            pnlMuestrasProc.Size = new Size(544, 226);
            pnlMuestrasProc.TabIndex = 0;
            // 
            // pnlTipoExamenes
            // 
            pnlTipoExamenes.Controls.Add(lblExamenesTipos);
            pnlTipoExamenes.Dock = DockStyle.Fill;
            pnlTipoExamenes.Location = new Point(553, 3);
            pnlTipoExamenes.Name = "pnlTipoExamenes";
            pnlTipoExamenes.Size = new Size(544, 226);
            pnlTipoExamenes.TabIndex = 1;
            // 
            // pnlMuestrasUltimas
            // 
            pnlMuestrasUltimas.Controls.Add(lblMuestrasUltimas);
            pnlMuestrasUltimas.Dock = DockStyle.Fill;
            pnlMuestrasUltimas.Location = new Point(3, 3);
            pnlMuestrasUltimas.Name = "pnlMuestrasUltimas";
            pnlMuestrasUltimas.Size = new Size(544, 164);
            pnlMuestrasUltimas.TabIndex = 0;
            // 
            // pnlExamenesUltimos
            // 
            pnlExamenesUltimos.Controls.Add(lblExamenesUltimos);
            pnlExamenesUltimos.Dock = DockStyle.Fill;
            pnlExamenesUltimos.Location = new Point(553, 3);
            pnlExamenesUltimos.Name = "pnlExamenesUltimos";
            pnlExamenesUltimos.Size = new Size(544, 164);
            pnlExamenesUltimos.TabIndex = 1;
            // 
            // lblMuestrasProcesadas
            // 
            lblMuestrasProcesadas.AutoSize = true;
            lblMuestrasProcesadas.Location = new Point(3, 12);
            lblMuestrasProcesadas.Name = "lblMuestrasProcesadas";
            lblMuestrasProcesadas.Size = new Size(120, 15);
            lblMuestrasProcesadas.TabIndex = 0;
            lblMuestrasProcesadas.Text = "Muestras Procesadas:";
            // 
            // lblExamenesTipos
            // 
            lblExamenesTipos.AutoSize = true;
            lblExamenesTipos.Location = new Point(3, 12);
            lblExamenesTipos.Name = "lblExamenesTipos";
            lblExamenesTipos.Size = new Size(110, 15);
            lblExamenesTipos.TabIndex = 0;
            lblExamenesTipos.Text = "Tipos de Examenes:";
            // 
            // lblMuestrasUltimas
            // 
            lblMuestrasUltimas.AutoSize = true;
            lblMuestrasUltimas.Location = new Point(12, 9);
            lblMuestrasUltimas.Name = "lblMuestrasUltimas";
            lblMuestrasUltimas.Size = new Size(98, 15);
            lblMuestrasUltimas.TabIndex = 0;
            lblMuestrasUltimas.Text = "Últimas Muestras";
            // 
            // lblExamenesUltimos
            // 
            lblExamenesUltimos.AutoSize = true;
            lblExamenesUltimos.Location = new Point(18, 9);
            lblExamenesUltimos.Name = "lblExamenesUltimos";
            lblExamenesUltimos.Size = new Size(104, 15);
            lblExamenesUltimos.TabIndex = 0;
            lblExamenesUltimos.Text = "Ultimos Examenes";
            // 
            // wDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 700);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tlpGraficas);
            Controls.Add(cboProyecto);
            Controls.Add(tlpIndices);
            Controls.Add(lblBienvenido);
            Controls.Add(lblBienvDash);
            FormBorderStyle = FormBorderStyle.None;
            Name = "wDashboard";
            Text = "z|";
            Load += wDashboard_Load;
            tlpIndices.ResumeLayout(false);
            pnlInformes.ResumeLayout(false);
            pnlInformes.PerformLayout();
            pnlExamenesRev.ResumeLayout(false);
            pnlExamenesRev.PerformLayout();
            pnlPacientesTotal.ResumeLayout(false);
            pnlPacientesTotal.PerformLayout();
            pnlMuestrasDia.ResumeLayout(false);
            pnlMuestrasDia.PerformLayout();
            tlpGraficas.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            pnlMuestrasProc.ResumeLayout(false);
            pnlMuestrasProc.PerformLayout();
            pnlTipoExamenes.ResumeLayout(false);
            pnlTipoExamenes.PerformLayout();
            pnlMuestrasUltimas.ResumeLayout(false);
            pnlMuestrasUltimas.PerformLayout();
            pnlExamenesUltimos.ResumeLayout(false);
            pnlExamenesUltimos.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBienvDash;
        private Label lblBienvenido;
        private Label lblMuestrasProcesadas;
        private FlowLayoutPanel flowLayoutPanel1;
        private TableLayoutPanel tlpIndices;
        private Panel pnlInformes;
        private Label lblInfomes;
        private Panel pnlExamenesRev;
        private Label lblExamenesRev;
        private Panel pnlMuestrasDia;
        private Label lblMuestrasDia;
        private Panel pnlPacientesTotal;
        private Label lblPacientesTotal;
        private ComboBox cboProyecto;
        private TableLayoutPanel tlpGraficas;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel pnlMuestrasProc;
        private Panel pnlTipoExamenes;
        private Panel pnlMuestrasUltimas;
        private Panel pnlExamenesUltimos;
        private Label lblExamenesTipos;
        private Label lblMuestrasUltimas;
        private Label lblExamenesUltimos;
    }
}