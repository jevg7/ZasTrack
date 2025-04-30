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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            lblBienvDash = new Label();
            lblBienvenido = new Label();
            tlpIndices = new TableLayoutPanel();
            pnlAccionRapida = new Panel();
            pnlBotonesAcciones = new Panel();
            btnAccionVerPendientes = new Button();
            btnAccionNuevaMuestra = new Button();
            pnlAccionRapidalbl = new Panel();
            lblAccionRapida = new Label();
            pnlPacientesTotal = new Panel();
            lblPacientesTotal = new Label();
            pnlExamenesRev = new Panel();
            lblExamenesRev = new Label();
            pnlInformes = new Panel();
            lblInfomes = new Label();
            pnlMuestrasDia = new Panel();
            lblMuestrasDia = new Label();
            cmbProyecto = new ComboBox();
            tlpGraficas = new TableLayoutPanel();
            pnlGraficoEstadoDia = new Panel();
            chartEstadoDia = new System.Windows.Forms.DataVisualization.Charting.Chart();
            pnlMuestrasProclbl = new Panel();
            lblMuestrasProcesadas = new Label();
            tlpUltimos = new TableLayoutPanel();
            pnlExamenesUltimos = new Panel();
            lblExamenesUltimos = new Label();
            pnlMuestrasUltimas = new Panel();
            lblMuestrasUltimas = new Label();
            lblEligirProyecto = new Label();
            pnltitulo = new Panel();
            pnlProyecto = new Panel();
            tlpDashBoard = new TableLayoutPanel();
            tlpIndices.SuspendLayout();
            pnlAccionRapida.SuspendLayout();
            pnlBotonesAcciones.SuspendLayout();
            pnlAccionRapidalbl.SuspendLayout();
            pnlPacientesTotal.SuspendLayout();
            pnlExamenesRev.SuspendLayout();
            pnlInformes.SuspendLayout();
            pnlMuestrasDia.SuspendLayout();
            tlpGraficas.SuspendLayout();
            pnlGraficoEstadoDia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartEstadoDia).BeginInit();
            pnlMuestrasProclbl.SuspendLayout();
            tlpUltimos.SuspendLayout();
            pnlExamenesUltimos.SuspendLayout();
            pnlMuestrasUltimas.SuspendLayout();
            pnltitulo.SuspendLayout();
            pnlProyecto.SuspendLayout();
            tlpDashBoard.SuspendLayout();
            SuspendLayout();
            // 
            // lblBienvDash
            // 
            lblBienvDash.AutoSize = true;
            lblBienvDash.Font = new Font("Segoe UI Emoji", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBienvDash.Location = new Point(13, 18);
            lblBienvDash.Name = "lblBienvDash";
            lblBienvDash.Size = new Size(171, 40);
            lblBienvDash.TabIndex = 0;
            lblBienvDash.Text = "Dashboard";
            // 
            // lblBienvenido
            // 
            lblBienvenido.AutoSize = true;
            lblBienvenido.Font = new Font("Segoe UI", 12F);
            lblBienvenido.Location = new Point(13, 58);
            lblBienvenido.Name = "lblBienvenido";
            lblBienvenido.Size = new Size(397, 28);
            lblBienvenido.TabIndex = 1;
            lblBienvenido.Text = "Bienvenido al Sistema de Gestión de ZabLab";
            lblBienvenido.Click += lblBienvenido_Click;
            // 
            // tlpIndices
            // 
            tlpIndices.ColumnCount = 5;
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpIndices.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpIndices.Controls.Add(pnlAccionRapida, 4, 0);
            tlpIndices.Controls.Add(pnlPacientesTotal, 0, 0);
            tlpIndices.Controls.Add(pnlExamenesRev, 2, 0);
            tlpIndices.Controls.Add(pnlInformes, 3, 0);
            tlpIndices.Controls.Add(pnlMuestrasDia, 1, 0);
            tlpIndices.Dock = DockStyle.Fill;
            tlpIndices.Location = new Point(3, 230);
            tlpIndices.Margin = new Padding(3, 4, 3, 4);
            tlpIndices.Name = "tlpIndices";
            tlpIndices.RowCount = 1;
            tlpIndices.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpIndices.Size = new Size(1038, 179);
            tlpIndices.TabIndex = 4;
            // 
            // pnlAccionRapida
            // 
            pnlAccionRapida.BackColor = SystemColors.GradientInactiveCaption;
            pnlAccionRapida.Controls.Add(pnlBotonesAcciones);
            pnlAccionRapida.Controls.Add(pnlAccionRapidalbl);
            pnlAccionRapida.Dock = DockStyle.Fill;
            pnlAccionRapida.Location = new Point(833, 5);
            pnlAccionRapida.Margin = new Padding(5, 5, 10, 5);
            pnlAccionRapida.Name = "pnlAccionRapida";
            pnlAccionRapida.Size = new Size(195, 169);
            pnlAccionRapida.TabIndex = 7;
            pnlAccionRapida.Paint += pnlAccionRapida_Paint;
            // 
            // pnlBotonesAcciones
            // 
            pnlBotonesAcciones.Controls.Add(btnAccionVerPendientes);
            pnlBotonesAcciones.Controls.Add(btnAccionNuevaMuestra);
            pnlBotonesAcciones.Dock = DockStyle.Fill;
            pnlBotonesAcciones.Location = new Point(0, 37);
            pnlBotonesAcciones.Name = "pnlBotonesAcciones";
            pnlBotonesAcciones.Size = new Size(195, 132);
            pnlBotonesAcciones.TabIndex = 4;
            // 
            // btnAccionVerPendientes
            // 
            btnAccionVerPendientes.Anchor = AnchorStyles.None;
            btnAccionVerPendientes.AutoSize = true;
            btnAccionVerPendientes.Location = new Point(25, 53);
            btnAccionVerPendientes.Margin = new Padding(3, 4, 3, 4);
            btnAccionVerPendientes.Name = "btnAccionVerPendientes";
            btnAccionVerPendientes.Size = new Size(141, 38);
            btnAccionVerPendientes.TabIndex = 3;
            btnAccionVerPendientes.Text = "Ver Pendientes";
            btnAccionVerPendientes.UseVisualStyleBackColor = true;
            btnAccionVerPendientes.Click += btnAccionVerPendientes_Click;
            // 
            // btnAccionNuevaMuestra
            // 
            btnAccionNuevaMuestra.Anchor = AnchorStyles.None;
            btnAccionNuevaMuestra.AutoSize = true;
            btnAccionNuevaMuestra.Location = new Point(25, 7);
            btnAccionNuevaMuestra.Margin = new Padding(3, 4, 3, 4);
            btnAccionNuevaMuestra.Name = "btnAccionNuevaMuestra";
            btnAccionNuevaMuestra.Size = new Size(141, 38);
            btnAccionNuevaMuestra.TabIndex = 1;
            btnAccionNuevaMuestra.Text = "Nueva Muestra";
            btnAccionNuevaMuestra.UseVisualStyleBackColor = true;
            btnAccionNuevaMuestra.Click += btnAccionNuevaMuestra_Click;
            // 
            // pnlAccionRapidalbl
            // 
            pnlAccionRapidalbl.Controls.Add(lblAccionRapida);
            pnlAccionRapidalbl.Dock = DockStyle.Top;
            pnlAccionRapidalbl.Location = new Point(0, 0);
            pnlAccionRapidalbl.Name = "pnlAccionRapidalbl";
            pnlAccionRapidalbl.Size = new Size(195, 37);
            pnlAccionRapidalbl.TabIndex = 1;
            // 
            // lblAccionRapida
            // 
            lblAccionRapida.Anchor = AnchorStyles.None;
            lblAccionRapida.AutoSize = true;
            lblAccionRapida.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAccionRapida.Location = new Point(25, 8);
            lblAccionRapida.Name = "lblAccionRapida";
            lblAccionRapida.Size = new Size(147, 23);
            lblAccionRapida.TabIndex = 0;
            lblAccionRapida.Text = "Acciones Rapidas";
            lblAccionRapida.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlPacientesTotal
            // 
            pnlPacientesTotal.BackColor = SystemColors.GradientInactiveCaption;
            pnlPacientesTotal.Controls.Add(lblPacientesTotal);
            pnlPacientesTotal.Dock = DockStyle.Fill;
            pnlPacientesTotal.Location = new Point(10, 5);
            pnlPacientesTotal.Margin = new Padding(10, 5, 5, 5);
            pnlPacientesTotal.Name = "pnlPacientesTotal";
            pnlPacientesTotal.Size = new Size(192, 169);
            pnlPacientesTotal.TabIndex = 5;
            // 
            // lblPacientesTotal
            // 
            lblPacientesTotal.AutoSize = true;
            lblPacientesTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPacientesTotal.Location = new Point(8, 8);
            lblPacientesTotal.Name = "lblPacientesTotal";
            lblPacientesTotal.Size = new Size(133, 23);
            lblPacientesTotal.TabIndex = 0;
            lblPacientesTotal.Text = "Total Pacientes:";
            // 
            // pnlExamenesRev
            // 
            pnlExamenesRev.BackColor = SystemColors.GradientInactiveCaption;
            pnlExamenesRev.Controls.Add(lblExamenesRev);
            pnlExamenesRev.Dock = DockStyle.Fill;
            pnlExamenesRev.Location = new Point(419, 5);
            pnlExamenesRev.Margin = new Padding(5);
            pnlExamenesRev.Name = "pnlExamenesRev";
            pnlExamenesRev.Size = new Size(197, 169);
            pnlExamenesRev.TabIndex = 6;
            // 
            // lblExamenesRev
            // 
            lblExamenesRev.AutoSize = true;
            lblExamenesRev.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblExamenesRev.Location = new Point(8, 8);
            lblExamenesRev.Name = "lblExamenesRev";
            lblExamenesRev.Size = new Size(96, 23);
            lblExamenesRev.TabIndex = 0;
            lblExamenesRev.Text = "Pendientes";
            // 
            // pnlInformes
            // 
            pnlInformes.BackColor = SystemColors.GradientInactiveCaption;
            pnlInformes.Controls.Add(lblInfomes);
            pnlInformes.Dock = DockStyle.Fill;
            pnlInformes.Location = new Point(626, 5);
            pnlInformes.Margin = new Padding(5);
            pnlInformes.Name = "pnlInformes";
            pnlInformes.Size = new Size(197, 169);
            pnlInformes.TabIndex = 6;
            // 
            // lblInfomes
            // 
            lblInfomes.AutoSize = true;
            lblInfomes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblInfomes.ImageAlign = ContentAlignment.TopLeft;
            lblInfomes.Location = new Point(8, 8);
            lblInfomes.Name = "lblInfomes";
            lblInfomes.Size = new Size(135, 23);
            lblInfomes.TabIndex = 0;
            lblInfomes.Text = "Procesados Hoy";
            // 
            // pnlMuestrasDia
            // 
            pnlMuestrasDia.BackColor = SystemColors.GradientInactiveCaption;
            pnlMuestrasDia.Controls.Add(lblMuestrasDia);
            pnlMuestrasDia.Dock = DockStyle.Fill;
            pnlMuestrasDia.Location = new Point(212, 5);
            pnlMuestrasDia.Margin = new Padding(5);
            pnlMuestrasDia.Name = "pnlMuestrasDia";
            pnlMuestrasDia.Size = new Size(197, 169);
            pnlMuestrasDia.TabIndex = 6;
            pnlMuestrasDia.Paint += pnlMuestrasDia_Paint;
            // 
            // lblMuestrasDia
            // 
            lblMuestrasDia.AutoSize = true;
            lblMuestrasDia.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMuestrasDia.Location = new Point(8, 8);
            lblMuestrasDia.Name = "lblMuestrasDia";
            lblMuestrasDia.Size = new Size(144, 23);
            lblMuestrasDia.TabIndex = 0;
            lblMuestrasDia.Text = "Muestras de Hoy";
            // 
            // cmbProyecto
            // 
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(13, 35);
            cmbProyecto.Margin = new Padding(3, 4, 3, 4);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(287, 28);
            cmbProyecto.TabIndex = 5;
            cmbProyecto.SelectedIndexChanged += cboProyecto_SelectedIndexChanged;
            // 
            // tlpGraficas
            // 
            tlpGraficas.BackColor = SystemColors.Control;
            tlpGraficas.ColumnCount = 1;
            tlpGraficas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpGraficas.Controls.Add(pnlGraficoEstadoDia, 0, 0);
            tlpGraficas.Dock = DockStyle.Fill;
            tlpGraficas.Location = new Point(3, 604);
            tlpGraficas.Margin = new Padding(3, 4, 3, 4);
            tlpGraficas.Name = "tlpGraficas";
            tlpGraficas.RowCount = 1;
            tlpGraficas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpGraficas.Size = new Size(1038, 180);
            tlpGraficas.TabIndex = 6;
            // 
            // pnlGraficoEstadoDia
            // 
            pnlGraficoEstadoDia.BackColor = SystemColors.ControlLight;
            pnlGraficoEstadoDia.Controls.Add(chartEstadoDia);
            pnlGraficoEstadoDia.Controls.Add(pnlMuestrasProclbl);
            pnlGraficoEstadoDia.Dock = DockStyle.Fill;
            pnlGraficoEstadoDia.Location = new Point(10, 4);
            pnlGraficoEstadoDia.Margin = new Padding(10, 4, 3, 4);
            pnlGraficoEstadoDia.Name = "pnlGraficoEstadoDia";
            pnlGraficoEstadoDia.Size = new Size(1025, 172);
            pnlGraficoEstadoDia.TabIndex = 0;
            // 
            // chartEstadoDia
            // 
            chartArea1.Name = "ChartArea1";
            chartEstadoDia.ChartAreas.Add(chartArea1);
            chartEstadoDia.Dock = DockStyle.Fill;
            legend1.Name = "Legend1";
            chartEstadoDia.Legends.Add(legend1);
            chartEstadoDia.Location = new Point(0, 36);
            chartEstadoDia.Margin = new Padding(3, 3, 10, 3);
            chartEstadoDia.Name = "chartEstadoDia";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartEstadoDia.Series.Add(series1);
            chartEstadoDia.Size = new Size(1025, 136);
            chartEstadoDia.TabIndex = 2;
            // 
            // pnlMuestrasProclbl
            // 
            pnlMuestrasProclbl.BackColor = SystemColors.ControlLight;
            pnlMuestrasProclbl.Controls.Add(lblMuestrasProcesadas);
            pnlMuestrasProclbl.Dock = DockStyle.Top;
            pnlMuestrasProclbl.Location = new Point(0, 0);
            pnlMuestrasProclbl.Name = "pnlMuestrasProclbl";
            pnlMuestrasProclbl.Size = new Size(1025, 36);
            pnlMuestrasProclbl.TabIndex = 1;
            // 
            // lblMuestrasProcesadas
            // 
            lblMuestrasProcesadas.AutoSize = true;
            lblMuestrasProcesadas.Location = new Point(10, 10);
            lblMuestrasProcesadas.Name = "lblMuestrasProcesadas";
            lblMuestrasProcesadas.Size = new Size(149, 20);
            lblMuestrasProcesadas.TabIndex = 0;
            lblMuestrasProcesadas.Text = "Muestras Procesadas:";
            // 
            // tlpUltimos
            // 
            tlpUltimos.ColumnCount = 2;
            tlpUltimos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpUltimos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpUltimos.Controls.Add(pnlExamenesUltimos, 1, 0);
            tlpUltimos.Controls.Add(pnlMuestrasUltimas, 0, 0);
            tlpUltimos.Dock = DockStyle.Fill;
            tlpUltimos.Location = new Point(3, 417);
            tlpUltimos.Margin = new Padding(3, 4, 3, 4);
            tlpUltimos.Name = "tlpUltimos";
            tlpUltimos.RowCount = 1;
            tlpUltimos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpUltimos.Size = new Size(1038, 179);
            tlpUltimos.TabIndex = 7;
            // 
            // pnlExamenesUltimos
            // 
            pnlExamenesUltimos.AutoScroll = true;
            pnlExamenesUltimos.BackColor = SystemColors.Info;
            pnlExamenesUltimos.Controls.Add(lblExamenesUltimos);
            pnlExamenesUltimos.Dock = DockStyle.Fill;
            pnlExamenesUltimos.Location = new Point(522, 4);
            pnlExamenesUltimos.Margin = new Padding(3, 4, 10, 4);
            pnlExamenesUltimos.Name = "pnlExamenesUltimos";
            pnlExamenesUltimos.Size = new Size(506, 171);
            pnlExamenesUltimos.TabIndex = 1;
            // 
            // lblExamenesUltimos
            // 
            lblExamenesUltimos.AutoSize = true;
            lblExamenesUltimos.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblExamenesUltimos.Location = new Point(23, 12);
            lblExamenesUltimos.Name = "lblExamenesUltimos";
            lblExamenesUltimos.Size = new Size(155, 23);
            lblExamenesUltimos.TabIndex = 0;
            lblExamenesUltimos.Text = "Ultimos Examenes";
            // 
            // pnlMuestrasUltimas
            // 
            pnlMuestrasUltimas.AutoScroll = true;
            pnlMuestrasUltimas.BackColor = SystemColors.Info;
            pnlMuestrasUltimas.Controls.Add(lblMuestrasUltimas);
            pnlMuestrasUltimas.Dock = DockStyle.Fill;
            pnlMuestrasUltimas.Location = new Point(10, 4);
            pnlMuestrasUltimas.Margin = new Padding(10, 4, 3, 4);
            pnlMuestrasUltimas.Name = "pnlMuestrasUltimas";
            pnlMuestrasUltimas.Size = new Size(506, 171);
            pnlMuestrasUltimas.TabIndex = 0;
            // 
            // lblMuestrasUltimas
            // 
            lblMuestrasUltimas.AutoSize = true;
            lblMuestrasUltimas.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMuestrasUltimas.Location = new Point(22, 12);
            lblMuestrasUltimas.Name = "lblMuestrasUltimas";
            lblMuestrasUltimas.Size = new Size(148, 23);
            lblMuestrasUltimas.TabIndex = 1;
            lblMuestrasUltimas.Text = "Ultimas Muestras";
            // 
            // lblEligirProyecto
            // 
            lblEligirProyecto.AutoSize = true;
            lblEligirProyecto.Location = new Point(13, 11);
            lblEligirProyecto.Name = "lblEligirProyecto";
            lblEligirProyecto.Size = new Size(127, 20);
            lblEligirProyecto.TabIndex = 8;
            lblEligirProyecto.Text = "Elige un Proyecto:";
            // 
            // pnltitulo
            // 
            pnltitulo.Controls.Add(lblBienvDash);
            pnltitulo.Controls.Add(lblBienvenido);
            pnltitulo.Dock = DockStyle.Fill;
            pnltitulo.Location = new Point(3, 3);
            pnltitulo.Name = "pnltitulo";
            pnltitulo.Size = new Size(1038, 107);
            pnltitulo.TabIndex = 9;
            // 
            // pnlProyecto
            // 
            pnlProyecto.Controls.Add(cmbProyecto);
            pnlProyecto.Controls.Add(lblEligirProyecto);
            pnlProyecto.Dock = DockStyle.Fill;
            pnlProyecto.Location = new Point(3, 116);
            pnlProyecto.Name = "pnlProyecto";
            pnlProyecto.Size = new Size(1038, 107);
            pnlProyecto.TabIndex = 10;
            // 
            // tlpDashBoard
            // 
            tlpDashBoard.ColumnCount = 1;
            tlpDashBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDashBoard.Controls.Add(pnltitulo, 0, 0);
            tlpDashBoard.Controls.Add(tlpGraficas, 0, 4);
            tlpDashBoard.Controls.Add(pnlProyecto, 0, 1);
            tlpDashBoard.Controls.Add(tlpUltimos, 0, 3);
            tlpDashBoard.Controls.Add(tlpIndices, 0, 2);
            tlpDashBoard.Dock = DockStyle.Fill;
            tlpDashBoard.Location = new Point(0, 0);
            tlpDashBoard.Name = "tlpDashBoard";
            tlpDashBoard.RowCount = 5;
            tlpDashBoard.RowStyles.Add(new RowStyle());
            tlpDashBoard.RowStyles.Add(new RowStyle());
            tlpDashBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tlpDashBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tlpDashBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tlpDashBoard.Size = new Size(1044, 788);
            tlpDashBoard.TabIndex = 12;
            tlpDashBoard.Paint += tableLayoutPanel1_Paint;
            // 
            // wDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1044, 788);
            Controls.Add(tlpDashBoard);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "wDashboard";
            Load += wDashboard_Load;
            tlpIndices.ResumeLayout(false);
            pnlAccionRapida.ResumeLayout(false);
            pnlBotonesAcciones.ResumeLayout(false);
            pnlBotonesAcciones.PerformLayout();
            pnlAccionRapidalbl.ResumeLayout(false);
            pnlAccionRapidalbl.PerformLayout();
            pnlPacientesTotal.ResumeLayout(false);
            pnlPacientesTotal.PerformLayout();
            pnlExamenesRev.ResumeLayout(false);
            pnlExamenesRev.PerformLayout();
            pnlInformes.ResumeLayout(false);
            pnlInformes.PerformLayout();
            pnlMuestrasDia.ResumeLayout(false);
            pnlMuestrasDia.PerformLayout();
            tlpGraficas.ResumeLayout(false);
            pnlGraficoEstadoDia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartEstadoDia).EndInit();
            pnlMuestrasProclbl.ResumeLayout(false);
            pnlMuestrasProclbl.PerformLayout();
            tlpUltimos.ResumeLayout(false);
            pnlExamenesUltimos.ResumeLayout(false);
            pnlExamenesUltimos.PerformLayout();
            pnlMuestrasUltimas.ResumeLayout(false);
            pnlMuestrasUltimas.PerformLayout();
            pnltitulo.ResumeLayout(false);
            pnltitulo.PerformLayout();
            pnlProyecto.ResumeLayout(false);
            pnlProyecto.PerformLayout();
            tlpDashBoard.ResumeLayout(false);
            ResumeLayout(false);
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
        private Panel pnlGraficoEstadoDia;
        private Panel pnlMuestrasUltimas;
        private Panel pnlExamenesUltimos;
        private Label lblExamenesUltimos;
        private Panel pnlAccionRapida;
        private Button btnAccionNuevaMuestra;
        private Label lblAccionRapida;
        private Label lblEligirProyecto;
        private Panel pnltitulo;
        private Panel pnlProyecto;
        private Label lblMuestrasUltimas;
        private Panel pnlAccionRapidalbl;
        private Panel pnlMuestrasProclbl;
        private TableLayoutPanel tlpDashBoard;
        private Button btnAccionVerPendientes;
        private Panel pnlBotonesAcciones;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEstadoDia;
    }
}