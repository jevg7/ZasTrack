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
            lblEligirProyecto = new Label();
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
            lblBienvDash.Location = new Point(54, 33);
            lblBienvDash.Name = "lblBienvDash";
            lblBienvDash.Size = new Size(171, 40);
            lblBienvDash.TabIndex = 0;
            lblBienvDash.Text = "Dashboard";
            // 
            // lblBienvenido
            // 
            lblBienvenido.AutoSize = true;
            lblBienvenido.Font = new Font("Segoe UI", 12F);
            lblBienvenido.Location = new Point(54, 73);
            lblBienvenido.Name = "lblBienvenido";
            lblBienvenido.Size = new Size(397, 28);
            lblBienvenido.TabIndex = 1;
            lblBienvenido.Text = "Bienvenido al Sistema de Gestión de ZabLab";
            lblBienvenido.Click += lblBienvenido_Click;
            // 
            // tlpIndices
            // 
            tlpIndices.ColumnCount = 5;
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.7074165F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.2925835F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 267F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 218F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 201F));
            tlpIndices.Controls.Add(pnlAccionRapida, 4, 0);
            tlpIndices.Controls.Add(pnlPacientesTotal, 0, 0);
            tlpIndices.Controls.Add(pnlMuestrasDia, 1, 0);
            tlpIndices.Controls.Add(pnlExamenesRev, 2, 0);
            tlpIndices.Controls.Add(pnlInformes, 3, 0);
            tlpIndices.Location = new Point(54, 187);
            tlpIndices.Margin = new Padding(3, 4, 3, 4);
            tlpIndices.Name = "tlpIndices";
            tlpIndices.RowCount = 1;
            tlpIndices.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpIndices.Size = new Size(1257, 187);
            tlpIndices.TabIndex = 4;
            // 
            // pnlAccionRapida
            // 
            pnlAccionRapida.Controls.Add(btnAccionVerPendientes);
            pnlAccionRapida.Controls.Add(btnAccionNuevaMuestra);
            pnlAccionRapida.Controls.Add(lblAccionRapida);
            pnlAccionRapida.Location = new Point(1058, 4);
            pnlAccionRapida.Margin = new Padding(3, 4, 3, 4);
            pnlAccionRapida.Name = "pnlAccionRapida";
            pnlAccionRapida.Size = new Size(194, 179);
            pnlAccionRapida.TabIndex = 7;
            // 
            // btnAccionVerPendientes
            // 
            btnAccionVerPendientes.Location = new Point(19, 91);
            btnAccionVerPendientes.Margin = new Padding(3, 4, 3, 4);
            btnAccionVerPendientes.Name = "btnAccionVerPendientes";
            btnAccionVerPendientes.Size = new Size(157, 31);
            btnAccionVerPendientes.TabIndex = 2;
            btnAccionVerPendientes.Text = "Ver Pendientes";
            btnAccionVerPendientes.UseVisualStyleBackColor = true;
            btnAccionVerPendientes.Click += btnAccionVerPendientes_Click;
            // 
            // btnAccionNuevaMuestra
            // 
            btnAccionNuevaMuestra.Location = new Point(19, 39);
            btnAccionNuevaMuestra.Margin = new Padding(3, 4, 3, 4);
            btnAccionNuevaMuestra.Name = "btnAccionNuevaMuestra";
            btnAccionNuevaMuestra.Size = new Size(157, 31);
            btnAccionNuevaMuestra.TabIndex = 1;
            btnAccionNuevaMuestra.Text = "Nueva Muestra";
            btnAccionNuevaMuestra.UseVisualStyleBackColor = true;
            btnAccionNuevaMuestra.Click += btnAccionNuevaMuestra_Click;
            // 
            // lblAccionRapida
            // 
            lblAccionRapida.AutoSize = true;
            lblAccionRapida.Location = new Point(40, 15);
            lblAccionRapida.Name = "lblAccionRapida";
            lblAccionRapida.Size = new Size(125, 20);
            lblAccionRapida.TabIndex = 0;
            lblAccionRapida.Text = "Acciones Rapidas";
            lblAccionRapida.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlPacientesTotal
            // 
            pnlPacientesTotal.Controls.Add(lblPacientesTotal);
            pnlPacientesTotal.Location = new Point(3, 4);
            pnlPacientesTotal.Margin = new Padding(3, 4, 3, 4);
            pnlPacientesTotal.Name = "pnlPacientesTotal";
            pnlPacientesTotal.Size = new Size(272, 179);
            pnlPacientesTotal.TabIndex = 5;
            pnlPacientesTotal.Paint += pnlPacientesTotal_Paint;
            // 
            // lblPacientesTotal
            // 
            lblPacientesTotal.AutoSize = true;
            lblPacientesTotal.Location = new Point(3, 15);
            lblPacientesTotal.Name = "lblPacientesTotal";
            lblPacientesTotal.Size = new Size(110, 20);
            lblPacientesTotal.TabIndex = 0;
            lblPacientesTotal.Text = "Total Pacientes:";
            // 
            // pnlMuestrasDia
            // 
            pnlMuestrasDia.Controls.Add(lblMuestrasDia);
            pnlMuestrasDia.Location = new Point(309, 4);
            pnlMuestrasDia.Margin = new Padding(3, 4, 3, 4);
            pnlMuestrasDia.Name = "pnlMuestrasDia";
            pnlMuestrasDia.Size = new Size(232, 179);
            pnlMuestrasDia.TabIndex = 6;
            pnlMuestrasDia.Paint += pnlMuestrasDia_Paint;
            // 
            // lblMuestrasDia
            // 
            lblMuestrasDia.AutoSize = true;
            lblMuestrasDia.Location = new Point(19, 15);
            lblMuestrasDia.Name = "lblMuestrasDia";
            lblMuestrasDia.Size = new Size(120, 20);
            lblMuestrasDia.TabIndex = 0;
            lblMuestrasDia.Text = "Muestras de Hoy";
            // 
            // pnlExamenesRev
            // 
            pnlExamenesRev.Controls.Add(lblExamenesRev);
            pnlExamenesRev.Location = new Point(573, 4);
            pnlExamenesRev.Margin = new Padding(3, 4, 3, 4);
            pnlExamenesRev.Name = "pnlExamenesRev";
            pnlExamenesRev.Size = new Size(205, 179);
            pnlExamenesRev.TabIndex = 6;
            // 
            // lblExamenesRev
            // 
            lblExamenesRev.AutoSize = true;
            lblExamenesRev.Location = new Point(16, 15);
            lblExamenesRev.Name = "lblExamenesRev";
            lblExamenesRev.Size = new Size(80, 20);
            lblExamenesRev.TabIndex = 0;
            lblExamenesRev.Text = "Pendientes";
            // 
            // pnlInformes
            // 
            pnlInformes.Controls.Add(lblInfomes);
            pnlInformes.Location = new Point(840, 4);
            pnlInformes.Margin = new Padding(3, 4, 3, 4);
            pnlInformes.Name = "pnlInformes";
            pnlInformes.Size = new Size(195, 179);
            pnlInformes.TabIndex = 6;
            // 
            // lblInfomes
            // 
            lblInfomes.AutoSize = true;
            lblInfomes.ImageAlign = ContentAlignment.TopLeft;
            lblInfomes.Location = new Point(19, 15);
            lblInfomes.Name = "lblInfomes";
            lblInfomes.Size = new Size(138, 20);
            lblInfomes.TabIndex = 0;
            lblInfomes.Text = "Examenes a Revisar";
            // 
            // cmbProyecto
            // 
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(57, 148);
            cmbProyecto.Margin = new Padding(3, 4, 3, 4);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(287, 28);
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
            tlpGraficas.Location = new Point(54, 381);
            tlpGraficas.Margin = new Padding(3, 4, 3, 4);
            tlpGraficas.Name = "tlpGraficas";
            tlpGraficas.RowCount = 1;
            tlpGraficas.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpGraficas.Size = new Size(1257, 309);
            tlpGraficas.TabIndex = 6;
            // 
            // pnlMuestrasProc
            // 
            pnlMuestrasProc.Controls.Add(lblMuestrasProcesadas);
            pnlMuestrasProc.Dock = DockStyle.Fill;
            pnlMuestrasProc.Location = new Point(3, 4);
            pnlMuestrasProc.Margin = new Padding(3, 4, 3, 4);
            pnlMuestrasProc.Name = "pnlMuestrasProc";
            pnlMuestrasProc.Size = new Size(622, 301);
            pnlMuestrasProc.TabIndex = 0;
            // 
            // lblMuestrasProcesadas
            // 
            lblMuestrasProcesadas.AutoSize = true;
            lblMuestrasProcesadas.Location = new Point(3, 16);
            lblMuestrasProcesadas.Name = "lblMuestrasProcesadas";
            lblMuestrasProcesadas.Size = new Size(149, 20);
            lblMuestrasProcesadas.TabIndex = 0;
            lblMuestrasProcesadas.Text = "Muestras Procesadas:";
            // 
            // pnlTipoExamenes
            // 
            pnlTipoExamenes.Controls.Add(lblExamenesTipos);
            pnlTipoExamenes.Dock = DockStyle.Fill;
            pnlTipoExamenes.Location = new Point(631, 4);
            pnlTipoExamenes.Margin = new Padding(3, 4, 3, 4);
            pnlTipoExamenes.Name = "pnlTipoExamenes";
            pnlTipoExamenes.Size = new Size(623, 301);
            pnlTipoExamenes.TabIndex = 1;
            // 
            // lblExamenesTipos
            // 
            lblExamenesTipos.AutoSize = true;
            lblExamenesTipos.Location = new Point(3, 16);
            lblExamenesTipos.Name = "lblExamenesTipos";
            lblExamenesTipos.Size = new Size(139, 20);
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
            tlpUltimos.Location = new Point(54, 699);
            tlpUltimos.Margin = new Padding(3, 4, 3, 4);
            tlpUltimos.Name = "tlpUltimos";
            tlpUltimos.RowCount = 1;
            tlpUltimos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpUltimos.Size = new Size(1257, 227);
            tlpUltimos.TabIndex = 7;
            // 
            // pnlMuestrasUltimas
            // 
            pnlMuestrasUltimas.Controls.Add(lblMuestrasUltimas);
            pnlMuestrasUltimas.Dock = DockStyle.Fill;
            pnlMuestrasUltimas.Location = new Point(3, 4);
            pnlMuestrasUltimas.Margin = new Padding(3, 4, 3, 4);
            pnlMuestrasUltimas.Name = "pnlMuestrasUltimas";
            pnlMuestrasUltimas.Size = new Size(622, 219);
            pnlMuestrasUltimas.TabIndex = 0;
            // 
            // lblMuestrasUltimas
            // 
            lblMuestrasUltimas.AutoSize = true;
            lblMuestrasUltimas.Location = new Point(14, 12);
            lblMuestrasUltimas.Name = "lblMuestrasUltimas";
            lblMuestrasUltimas.Size = new Size(122, 20);
            lblMuestrasUltimas.TabIndex = 0;
            lblMuestrasUltimas.Text = "Últimas Muestras";
            // 
            // pnlExamenesUltimos
            // 
            pnlExamenesUltimos.Controls.Add(lblExamenesUltimos);
            pnlExamenesUltimos.Dock = DockStyle.Fill;
            pnlExamenesUltimos.Location = new Point(631, 4);
            pnlExamenesUltimos.Margin = new Padding(3, 4, 3, 4);
            pnlExamenesUltimos.Name = "pnlExamenesUltimos";
            pnlExamenesUltimos.Size = new Size(623, 219);
            pnlExamenesUltimos.TabIndex = 1;
            // 
            // lblExamenesUltimos
            // 
            lblExamenesUltimos.AutoSize = true;
            lblExamenesUltimos.Location = new Point(21, 12);
            lblExamenesUltimos.Name = "lblExamenesUltimos";
            lblExamenesUltimos.Size = new Size(130, 20);
            lblExamenesUltimos.TabIndex = 0;
            lblExamenesUltimos.Text = "Ultimos Examenes";
            // 
            // lblEligirProyecto
            // 
            lblEligirProyecto.AutoSize = true;
            lblEligirProyecto.Location = new Point(57, 124);
            lblEligirProyecto.Name = "lblEligirProyecto";
            lblEligirProyecto.Size = new Size(127, 20);
            lblEligirProyecto.TabIndex = 8;
            lblEligirProyecto.Text = "Elige un Proyecto:";
            // 
            // wDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1371, 933);
            Controls.Add(lblEligirProyecto);
            Controls.Add(tlpUltimos);
            Controls.Add(tlpGraficas);
            Controls.Add(cmbProyecto);
            Controls.Add(tlpIndices);
            Controls.Add(lblBienvenido);
            Controls.Add(lblBienvDash);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "wDashboard";
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
        private Label lblEligirProyecto;
    }
}