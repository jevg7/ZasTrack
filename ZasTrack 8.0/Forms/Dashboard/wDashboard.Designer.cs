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
            pnlAccionRapida = new Panel();
            btnAccionVerPendientes = new Button();
            btnAccionNuevaMuestra = new Button();
            lblAccionRapida = new Label();
            pnlPacientesTotal = new Panel();
            lblPacientesTotal = new Label();
            pnlMuestrasDia = new Panel();
            lblMuestrasDia = new Label();
            pnlExamenesRev = new Panel();
            lblExamenesRev = new Label();
            pnlInformes = new Panel();
            lblInfomes = new Label();
            cmbProyecto = new ComboBox();
            tlpGraficas = new TableLayoutPanel();
            pnlMuestrasProc = new Panel();
            lblMuestrasProcesadas = new Label();
            pnlTipoExamenes = new Panel();
            lblExamenesTipos = new Label();
            tlpUltimos = new TableLayoutPanel();
            pnlMuestrasUltimas = new Panel();
            lblMuestrasUltimas = new Label();
            pnlExamenesUltimos = new Panel();
            lblExamenesUltimos = new Label();
            tlpIndices.SuspendLayout();
            pnlAccionRapida.SuspendLayout();
            pnlPacientesTotal.SuspendLayout();
            pnlMuestrasDia.SuspendLayout();
            pnlExamenesRev.SuspendLayout();
            pnlInformes.SuspendLayout();
            tlpGraficas.SuspendLayout();
            pnlMuestrasProc.SuspendLayout();
            pnlTipoExamenes.SuspendLayout();
            tlpUltimos.SuspendLayout();
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
            lblBienvenido.Click += lblBienvenido_Click;
            // 
            // tlpIndices
            // 
            tlpIndices.ColumnCount = 5;
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.7074165F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.2925835F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 234F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 191F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175F));
            tlpIndices.Controls.Add(pnlAccionRapida, 4, 0);
            tlpIndices.Controls.Add(pnlPacientesTotal, 0, 0);
            tlpIndices.Controls.Add(pnlMuestrasDia, 1, 0);
            tlpIndices.Controls.Add(pnlExamenesRev, 2, 0);
            tlpIndices.Controls.Add(pnlInformes, 3, 0);
            tlpIndices.Location = new Point(47, 140);
            tlpIndices.Name = "tlpIndices";
            tlpIndices.RowCount = 1;
            tlpIndices.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpIndices.Size = new Size(1100, 140);
            tlpIndices.TabIndex = 4;
            // 
            // pnlAccionRapida
            // 
            pnlAccionRapida.Controls.Add(btnAccionVerPendientes);
            pnlAccionRapida.Controls.Add(btnAccionNuevaMuestra);
            pnlAccionRapida.Controls.Add(lblAccionRapida);
            pnlAccionRapida.Location = new Point(927, 3);
            pnlAccionRapida.Name = "pnlAccionRapida";
            pnlAccionRapida.Size = new Size(170, 134);
            pnlAccionRapida.TabIndex = 7;
            // 
            // btnAccionVerPendientes
            // 
            btnAccionVerPendientes.Location = new Point(17, 68);
            btnAccionVerPendientes.Name = "btnAccionVerPendientes";
            btnAccionVerPendientes.Size = new Size(137, 23);
            btnAccionVerPendientes.TabIndex = 2;
            btnAccionVerPendientes.Text = "Ver Pendientes";
            btnAccionVerPendientes.UseVisualStyleBackColor = true;
            btnAccionVerPendientes.Click += btnAccionVerPendientes_Click;
            // 
            // btnAccionNuevaMuestra
            // 
            btnAccionNuevaMuestra.Location = new Point(17, 29);
            btnAccionNuevaMuestra.Name = "btnAccionNuevaMuestra";
            btnAccionNuevaMuestra.Size = new Size(137, 23);
            btnAccionNuevaMuestra.TabIndex = 1;
            btnAccionNuevaMuestra.Text = "Nueva Muestra";
            btnAccionNuevaMuestra.UseVisualStyleBackColor = true;
            btnAccionNuevaMuestra.Click += btnAccionNuevaMuestra_Click;
            // 
            // lblAccionRapida
            // 
            lblAccionRapida.AutoSize = true;
            lblAccionRapida.Location = new Point(35, 11);
            lblAccionRapida.Name = "lblAccionRapida";
            lblAccionRapida.Size = new Size(99, 15);
            lblAccionRapida.TabIndex = 0;
            lblAccionRapida.Text = "Acciones Rapidas";
            lblAccionRapida.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlPacientesTotal
            // 
            pnlPacientesTotal.Controls.Add(lblPacientesTotal);
            pnlPacientesTotal.Location = new Point(3, 3);
            pnlPacientesTotal.Name = "pnlPacientesTotal";
            pnlPacientesTotal.Size = new Size(238, 134);
            pnlPacientesTotal.TabIndex = 5;
            pnlPacientesTotal.Paint += pnlPacientesTotal_Paint;
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
            pnlMuestrasDia.Location = new Point(271, 3);
            pnlMuestrasDia.Name = "pnlMuestrasDia";
            pnlMuestrasDia.Size = new Size(203, 134);
            pnlMuestrasDia.TabIndex = 6;
            pnlMuestrasDia.Paint += pnlMuestrasDia_Paint;
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
            // pnlExamenesRev
            // 
            pnlExamenesRev.Controls.Add(lblExamenesRev);
            pnlExamenesRev.Location = new Point(502, 3);
            pnlExamenesRev.Name = "pnlExamenesRev";
            pnlExamenesRev.Size = new Size(179, 134);
            pnlExamenesRev.TabIndex = 6;
            // 
            // lblExamenesRev
            // 
            lblExamenesRev.AutoSize = true;
            lblExamenesRev.Location = new Point(14, 11);
            lblExamenesRev.Name = "lblExamenesRev";
            lblExamenesRev.Size = new Size(65, 15);
            lblExamenesRev.TabIndex = 0;
            lblExamenesRev.Text = "Pendientes";
            // 
            // pnlInformes
            // 
            pnlInformes.Controls.Add(lblInfomes);
            pnlInformes.Location = new Point(736, 3);
            pnlInformes.Name = "pnlInformes";
            pnlInformes.Size = new Size(171, 134);
            pnlInformes.TabIndex = 6;
            // 
            // lblInfomes
            // 
            lblInfomes.AutoSize = true;
            lblInfomes.ImageAlign = ContentAlignment.TopLeft;
            lblInfomes.Location = new Point(17, 11);
            lblInfomes.Name = "lblInfomes";
            lblInfomes.Size = new Size(109, 15);
            lblInfomes.TabIndex = 0;
            lblInfomes.Text = "Examenes a Revisar";
            // 
            // cmbProyecto
            // 
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(50, 111);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(252, 23);
            cmbProyecto.TabIndex = 5;
            cmbProyecto.SelectedIndexChanged += cboProyecto_SelectedIndexChanged;
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
            // pnlMuestrasProc
            // 
            pnlMuestrasProc.Controls.Add(lblMuestrasProcesadas);
            pnlMuestrasProc.Dock = DockStyle.Fill;
            pnlMuestrasProc.Location = new Point(3, 3);
            pnlMuestrasProc.Name = "pnlMuestrasProc";
            pnlMuestrasProc.Size = new Size(544, 226);
            pnlMuestrasProc.TabIndex = 0;
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
            // pnlTipoExamenes
            // 
            pnlTipoExamenes.Controls.Add(lblExamenesTipos);
            pnlTipoExamenes.Dock = DockStyle.Fill;
            pnlTipoExamenes.Location = new Point(553, 3);
            pnlTipoExamenes.Name = "pnlTipoExamenes";
            pnlTipoExamenes.Size = new Size(544, 226);
            pnlTipoExamenes.TabIndex = 1;
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
            // tlpUltimos
            // 
            tlpUltimos.ColumnCount = 2;
            tlpUltimos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpUltimos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpUltimos.Controls.Add(pnlMuestrasUltimas, 0, 0);
            tlpUltimos.Controls.Add(pnlExamenesUltimos, 1, 0);
            tlpUltimos.Location = new Point(47, 524);
            tlpUltimos.Name = "tlpUltimos";
            tlpUltimos.RowCount = 1;
            tlpUltimos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpUltimos.Size = new Size(1100, 170);
            tlpUltimos.TabIndex = 7;
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
            // lblMuestrasUltimas
            // 
            lblMuestrasUltimas.AutoSize = true;
            lblMuestrasUltimas.Location = new Point(12, 9);
            lblMuestrasUltimas.Name = "lblMuestrasUltimas";
            lblMuestrasUltimas.Size = new Size(98, 15);
            lblMuestrasUltimas.TabIndex = 0;
            lblMuestrasUltimas.Text = "Últimas Muestras";
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
            Controls.Add(tlpUltimos);
            Controls.Add(tlpGraficas);
            Controls.Add(cmbProyecto);
            Controls.Add(tlpIndices);
            Controls.Add(lblBienvenido);
            Controls.Add(lblBienvDash);
            FormBorderStyle = FormBorderStyle.None;
            Name = "wDashboard";
            Text = "z|";
            Load += wDashboard_Load;
            tlpIndices.ResumeLayout(false);
            pnlAccionRapida.ResumeLayout(false);
            pnlAccionRapida.PerformLayout();
            pnlPacientesTotal.ResumeLayout(false);
            pnlPacientesTotal.PerformLayout();
            pnlMuestrasDia.ResumeLayout(false);
            pnlMuestrasDia.PerformLayout();
            pnlExamenesRev.ResumeLayout(false);
            pnlExamenesRev.PerformLayout();
            pnlInformes.ResumeLayout(false);
            pnlInformes.PerformLayout();
            tlpGraficas.ResumeLayout(false);
            pnlMuestrasProc.ResumeLayout(false);
            pnlMuestrasProc.PerformLayout();
            pnlTipoExamenes.ResumeLayout(false);
            pnlTipoExamenes.PerformLayout();
            tlpUltimos.ResumeLayout(false);
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
        private ComboBox cmbProyecto;
        private TableLayoutPanel tlpGraficas;
        private TableLayoutPanel tlpUltimos;
        private Panel pnlMuestrasProc;
        private Panel pnlTipoExamenes;
        private Panel pnlMuestrasUltimas;
        private Panel pnlExamenesUltimos;
        private Label lblExamenesTipos;
        private Label lblMuestrasUltimas;
        private Label lblExamenesUltimos;
        private Panel pnlAccionRapida;
        private Button btnAccionVerPendientes;
        private Button btnAccionNuevaMuestra;
        private Label lblAccionRapida;
    }
}