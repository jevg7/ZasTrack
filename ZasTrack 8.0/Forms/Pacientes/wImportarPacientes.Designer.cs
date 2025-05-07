namespace ZasTrack.Forms.Pacientes
{
    partial class wImportarPacientes
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
            btnSeleccionarArchivo = new Button();
            cmbHojas = new ComboBox();
            chkTieneEncabezado = new CheckBox();
            btnImportar = new Button();
            lblHojas = new Label();
            dgvResultados = new DataGridView();
            txtRutaArchivo = new TextBox();
            lblUbicacionArc = new Label();
            lblErrores = new Label();
            pnlImportarPacientes = new Panel();
            tlpImportarPacientes = new TableLayoutPanel();
            pnlInterfaz = new Panel();
            tlpInterfaz = new TableLayoutPanel();
            pnlSeleccionarArch = new Panel();
            tlpSeleccionarArch = new TableLayoutPanel();
            pnlHojas = new Panel();
            tlpHojas = new TableLayoutPanel();
            pnlProyecto = new Panel();
            tlpTieneEncabezado = new TableLayoutPanel();
            cmbProyecto = new ComboBox();
            lblProyecto = new Label();
            tlpProyectoCmb = new TableLayoutPanel();
            pnlDvg = new Panel();
            tlpDvg = new TableLayoutPanel();
            pnlResultadoOrg = new Panel();
            progressBarImportacion = new ProgressBar();
            lblProgreso = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            pnlImportarPacientes.SuspendLayout();
            tlpImportarPacientes.SuspendLayout();
            pnlInterfaz.SuspendLayout();
            tlpInterfaz.SuspendLayout();
            pnlSeleccionarArch.SuspendLayout();
            tlpSeleccionarArch.SuspendLayout();
            pnlHojas.SuspendLayout();
            tlpHojas.SuspendLayout();
            pnlProyecto.SuspendLayout();
            tlpTieneEncabezado.SuspendLayout();
            tlpProyectoCmb.SuspendLayout();
            pnlDvg.SuspendLayout();
            tlpDvg.SuspendLayout();
            pnlResultadoOrg.SuspendLayout();
            SuspendLayout();
            // 
            // btnSeleccionarArchivo
            // 
            btnSeleccionarArchivo.Anchor = AnchorStyles.None;
            btnSeleccionarArchivo.Location = new Point(65, 42);
            btnSeleccionarArchivo.Name = "btnSeleccionarArchivo";
            btnSeleccionarArchivo.Size = new Size(314, 84);
            btnSeleccionarArchivo.TabIndex = 0;
            btnSeleccionarArchivo.Text = "Seleccionar Archivo";
            btnSeleccionarArchivo.UseVisualStyleBackColor = true;
            // 
            // cmbHojas
            // 
            cmbHojas.Dock = DockStyle.Top;
            cmbHojas.FormattingEnabled = true;
            cmbHojas.Location = new Point(82, 3);
            cmbHojas.Name = "cmbHojas";
            cmbHojas.Size = new Size(360, 28);
            cmbHojas.TabIndex = 1;
            // 
            // chkTieneEncabezado
            // 
            chkTieneEncabezado.Anchor = AnchorStyles.None;
            chkTieneEncabezado.AutoSize = true;
            chkTieneEncabezado.Location = new Point(145, 6);
            chkTieneEncabezado.Name = "chkTieneEncabezado";
            chkTieneEncabezado.Size = new Size(155, 24);
            chkTieneEncabezado.TabIndex = 3;
            chkTieneEncabezado.Text = "Tiene Encabezado.";
            chkTieneEncabezado.UseVisualStyleBackColor = true;
            // 
            // btnImportar
            // 
            btnImportar.Anchor = AnchorStyles.None;
            btnImportar.Location = new Point(80, 321);
            btnImportar.Name = "btnImportar";
            btnImportar.Size = new Size(291, 39);
            btnImportar.TabIndex = 4;
            btnImportar.Text = "Importar";
            btnImportar.UseVisualStyleBackColor = true;
            // 
            // lblHojas
            // 
            lblHojas.Anchor = AnchorStyles.None;
            lblHojas.AutoSize = true;
            lblHojas.Location = new Point(16, 10);
            lblHojas.Name = "lblHojas";
            lblHojas.Size = new Size(47, 20);
            lblHojas.TabIndex = 6;
            lblHojas.Text = "Hojas";
            lblHojas.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvResultados
            // 
            dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResultados.Dock = DockStyle.Fill;
            dgvResultados.Location = new Point(10, 52);
            dgvResultados.Margin = new Padding(10);
            dgvResultados.Name = "dgvResultados";
            dgvResultados.RowHeadersWidth = 51;
            dgvResultados.Size = new Size(587, 401);
            dgvResultados.TabIndex = 7;
            // 
            // txtRutaArchivo
            // 
            txtRutaArchivo.Dock = DockStyle.Top;
            txtRutaArchivo.Enabled = false;
            txtRutaArchivo.Location = new Point(3, 150);
            txtRutaArchivo.Name = "txtRutaArchivo";
            txtRutaArchivo.ReadOnly = true;
            txtRutaArchivo.Size = new Size(445, 27);
            txtRutaArchivo.TabIndex = 10;
            // 
            // lblUbicacionArc
            // 
            lblUbicacionArc.Anchor = AnchorStyles.None;
            lblUbicacionArc.AutoSize = true;
            lblUbicacionArc.Location = new Point(145, 4);
            lblUbicacionArc.Name = "lblUbicacionArc";
            lblUbicacionArc.Size = new Size(154, 20);
            lblUbicacionArc.TabIndex = 15;
            lblUbicacionArc.Text = "Ubicacion del Archivo";
            lblUbicacionArc.Click += lblUbicacionArc_Click;
            // 
            // lblErrores
            // 
            lblErrores.Anchor = AnchorStyles.None;
            lblErrores.AutoSize = true;
            lblErrores.Font = new Font("Segoe UI", 14F);
            lblErrores.Location = new Point(163, 5);
            lblErrores.Name = "lblErrores";
            lblErrores.Size = new Size(281, 32);
            lblErrores.TabIndex = 16;
            lblErrores.Text = "Errores de la Importacion";
            // 
            // pnlImportarPacientes
            // 
            pnlImportarPacientes.Controls.Add(tlpImportarPacientes);
            pnlImportarPacientes.Dock = DockStyle.Fill;
            pnlImportarPacientes.Location = new Point(0, 0);
            pnlImportarPacientes.Name = "pnlImportarPacientes";
            pnlImportarPacientes.Size = new Size(1070, 753);
            pnlImportarPacientes.TabIndex = 17;
            // 
            // tlpImportarPacientes
            // 
            tlpImportarPacientes.ColumnCount = 2;
            tlpImportarPacientes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42.71028F));
            tlpImportarPacientes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57.28972F));
            tlpImportarPacientes.Controls.Add(pnlInterfaz, 0, 0);
            tlpImportarPacientes.Controls.Add(pnlDvg, 1, 0);
            tlpImportarPacientes.Dock = DockStyle.Fill;
            tlpImportarPacientes.Location = new Point(0, 0);
            tlpImportarPacientes.Name = "tlpImportarPacientes";
            tlpImportarPacientes.RowCount = 1;
            tlpImportarPacientes.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpImportarPacientes.Size = new Size(1070, 753);
            tlpImportarPacientes.TabIndex = 0;
            // 
            // pnlInterfaz
            // 
            pnlInterfaz.Controls.Add(tlpInterfaz);
            pnlInterfaz.Dock = DockStyle.Fill;
            pnlInterfaz.Location = new Point(3, 3);
            pnlInterfaz.Name = "pnlInterfaz";
            pnlInterfaz.Size = new Size(451, 747);
            pnlInterfaz.TabIndex = 0;
            // 
            // tlpInterfaz
            // 
            tlpInterfaz.ColumnCount = 1;
            tlpInterfaz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpInterfaz.Controls.Add(pnlSeleccionarArch, 0, 0);
            tlpInterfaz.Controls.Add(txtRutaArchivo, 0, 1);
            tlpInterfaz.Controls.Add(pnlHojas, 0, 2);
            tlpInterfaz.Controls.Add(pnlProyecto, 0, 3);
            tlpInterfaz.Controls.Add(tlpProyectoCmb, 0, 4);
            tlpInterfaz.Controls.Add(btnImportar, 0, 5);
            tlpInterfaz.Dock = DockStyle.Fill;
            tlpInterfaz.Location = new Point(0, 0);
            tlpInterfaz.Name = "tlpInterfaz";
            tlpInterfaz.RowCount = 7;
            tlpInterfaz.RowStyles.Add(new RowStyle(SizeType.Percent, 19.6787148F));
            tlpInterfaz.RowStyles.Add(new RowStyle(SizeType.Percent, 3.61445785F));
            tlpInterfaz.RowStyles.Add(new RowStyle(SizeType.Percent, 6.285073F));
            tlpInterfaz.RowStyles.Add(new RowStyle(SizeType.Percent, 6.584362F));
            tlpInterfaz.RowStyles.Add(new RowStyle(SizeType.Percent, 5.62249F));
            tlpInterfaz.RowStyles.Add(new RowStyle(SizeType.Percent, 7.764391F));
            tlpInterfaz.RowStyles.Add(new RowStyle(SizeType.Percent, 50.2008F));
            tlpInterfaz.Size = new Size(451, 747);
            tlpInterfaz.TabIndex = 17;
            // 
            // pnlSeleccionarArch
            // 
            pnlSeleccionarArch.Controls.Add(tlpSeleccionarArch);
            pnlSeleccionarArch.Dock = DockStyle.Fill;
            pnlSeleccionarArch.Location = new Point(3, 3);
            pnlSeleccionarArch.Name = "pnlSeleccionarArch";
            pnlSeleccionarArch.Size = new Size(445, 141);
            pnlSeleccionarArch.TabIndex = 16;
            // 
            // tlpSeleccionarArch
            // 
            tlpSeleccionarArch.ColumnCount = 1;
            tlpSeleccionarArch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpSeleccionarArch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpSeleccionarArch.Controls.Add(btnSeleccionarArchivo, 0, 1);
            tlpSeleccionarArch.Controls.Add(lblUbicacionArc, 0, 0);
            tlpSeleccionarArch.Dock = DockStyle.Fill;
            tlpSeleccionarArch.Location = new Point(0, 0);
            tlpSeleccionarArch.Name = "tlpSeleccionarArch";
            tlpSeleccionarArch.RowCount = 2;
            tlpSeleccionarArch.RowStyles.Add(new RowStyle(SizeType.Percent, 20.1754379F));
            tlpSeleccionarArch.RowStyles.Add(new RowStyle(SizeType.Percent, 79.82456F));
            tlpSeleccionarArch.Size = new Size(445, 141);
            tlpSeleccionarArch.TabIndex = 0;
            // 
            // pnlHojas
            // 
            pnlHojas.Controls.Add(tlpHojas);
            pnlHojas.Dock = DockStyle.Fill;
            pnlHojas.Location = new Point(3, 177);
            pnlHojas.Name = "pnlHojas";
            pnlHojas.Size = new Size(445, 41);
            pnlHojas.TabIndex = 17;
            // 
            // tlpHojas
            // 
            tlpHojas.ColumnCount = 2;
            tlpHojas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.75281F));
            tlpHojas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 82.24719F));
            tlpHojas.Controls.Add(cmbHojas, 1, 0);
            tlpHojas.Controls.Add(lblHojas, 0, 0);
            tlpHojas.Dock = DockStyle.Fill;
            tlpHojas.Location = new Point(0, 0);
            tlpHojas.Name = "tlpHojas";
            tlpHojas.RowCount = 1;
            tlpHojas.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpHojas.Size = new Size(445, 41);
            tlpHojas.TabIndex = 0;
            // 
            // pnlProyecto
            // 
            pnlProyecto.Controls.Add(tlpTieneEncabezado);
            pnlProyecto.Dock = DockStyle.Fill;
            pnlProyecto.Location = new Point(3, 224);
            pnlProyecto.Name = "pnlProyecto";
            pnlProyecto.Size = new Size(445, 43);
            pnlProyecto.TabIndex = 17;
            // 
            // tlpTieneEncabezado
            // 
            tlpTieneEncabezado.ColumnCount = 2;
            tlpTieneEncabezado.ColumnStyles.Add(new ColumnStyle());
            tlpTieneEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTieneEncabezado.Controls.Add(cmbProyecto, 1, 0);
            tlpTieneEncabezado.Controls.Add(lblProyecto, 0, 0);
            tlpTieneEncabezado.Dock = DockStyle.Fill;
            tlpTieneEncabezado.Location = new Point(0, 0);
            tlpTieneEncabezado.Name = "tlpTieneEncabezado";
            tlpTieneEncabezado.RowCount = 1;
            tlpTieneEncabezado.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTieneEncabezado.Size = new Size(445, 43);
            tlpTieneEncabezado.TabIndex = 0;
            // 
            // cmbProyecto
            // 
            cmbProyecto.Dock = DockStyle.Top;
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(84, 3);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(358, 28);
            cmbProyecto.TabIndex = 8;
            // 
            // lblProyecto
            // 
            lblProyecto.Anchor = AnchorStyles.None;
            lblProyecto.Location = new Point(3, 11);
            lblProyecto.Name = "lblProyecto";
            lblProyecto.Size = new Size(75, 20);
            lblProyecto.TabIndex = 7;
            lblProyecto.Text = "Proyecto";
            lblProyecto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tlpProyectoCmb
            // 
            tlpProyectoCmb.ColumnCount = 1;
            tlpProyectoCmb.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpProyectoCmb.Controls.Add(chkTieneEncabezado, 0, 0);
            tlpProyectoCmb.Dock = DockStyle.Fill;
            tlpProyectoCmb.Location = new Point(3, 273);
            tlpProyectoCmb.Name = "tlpProyectoCmb";
            tlpProyectoCmb.RowCount = 1;
            tlpProyectoCmb.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tlpProyectoCmb.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpProyectoCmb.Size = new Size(445, 36);
            tlpProyectoCmb.TabIndex = 0;
            // 
            // pnlDvg
            // 
            pnlDvg.Controls.Add(tlpDvg);
            pnlDvg.Dock = DockStyle.Fill;
            pnlDvg.Location = new Point(460, 3);
            pnlDvg.Name = "pnlDvg";
            pnlDvg.Size = new Size(607, 747);
            pnlDvg.TabIndex = 1;
            // 
            // tlpDvg
            // 
            tlpDvg.ColumnCount = 1;
            tlpDvg.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDvg.Controls.Add(pnlResultadoOrg, 0, 2);
            tlpDvg.Controls.Add(lblErrores, 0, 0);
            tlpDvg.Controls.Add(dgvResultados, 0, 1);
            tlpDvg.Dock = DockStyle.Fill;
            tlpDvg.Location = new Point(0, 0);
            tlpDvg.Name = "tlpDvg";
            tlpDvg.RowCount = 3;
            tlpDvg.RowStyles.Add(new RowStyle(SizeType.Percent, 5.64102554F));
            tlpDvg.RowStyles.Add(new RowStyle(SizeType.Percent, 56.4926376F));
            tlpDvg.RowStyles.Add(new RowStyle(SizeType.Percent, 37.8848724F));
            tlpDvg.Size = new Size(607, 747);
            tlpDvg.TabIndex = 18;
            // 
            // pnlResultadoOrg
            // 
            pnlResultadoOrg.Controls.Add(progressBarImportacion);
            pnlResultadoOrg.Controls.Add(lblProgreso);
            pnlResultadoOrg.Dock = DockStyle.Fill;
            pnlResultadoOrg.Location = new Point(3, 466);
            pnlResultadoOrg.Name = "pnlResultadoOrg";
            pnlResultadoOrg.Size = new Size(601, 278);
            pnlResultadoOrg.TabIndex = 17;
            // 
            // progressBarImportacion
            // 
            progressBarImportacion.Location = new Point(71, 17);
            progressBarImportacion.Name = "progressBarImportacion";
            progressBarImportacion.Size = new Size(464, 10);
            progressBarImportacion.TabIndex = 5;
            // 
            // lblProgreso
            // 
            lblProgreso.AutoSize = true;
            lblProgreso.Location = new Point(3, 10);
            lblProgreso.Name = "lblProgreso";
            lblProgreso.Size = new Size(68, 20);
            lblProgreso.TabIndex = 8;
            lblProgreso.Text = "Progreso";
            // 
            // wImportarPacientes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1070, 753);
            ControlBox = false;
            Controls.Add(pnlImportarPacientes);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "wImportarPacientes";
            Text = "wImportarPacientes";
            Load += wImportarPacientes_Load;
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            pnlImportarPacientes.ResumeLayout(false);
            tlpImportarPacientes.ResumeLayout(false);
            pnlInterfaz.ResumeLayout(false);
            tlpInterfaz.ResumeLayout(false);
            tlpInterfaz.PerformLayout();
            pnlSeleccionarArch.ResumeLayout(false);
            tlpSeleccionarArch.ResumeLayout(false);
            tlpSeleccionarArch.PerformLayout();
            pnlHojas.ResumeLayout(false);
            tlpHojas.ResumeLayout(false);
            tlpHojas.PerformLayout();
            pnlProyecto.ResumeLayout(false);
            tlpTieneEncabezado.ResumeLayout(false);
            tlpProyectoCmb.ResumeLayout(false);
            tlpProyectoCmb.PerformLayout();
            pnlDvg.ResumeLayout(false);
            tlpDvg.ResumeLayout(false);
            tlpDvg.PerformLayout();
            pnlResultadoOrg.ResumeLayout(false);
            pnlResultadoOrg.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnSeleccionarArchivo;
        private ComboBox cmbHojas;
        private CheckBox chkTieneEncabezado;
        private Button btnImportar;
        private Label lblHojas;
        private DataGridView dgvResultados;
        private TextBox txtRutaArchivo;
        private Label lblUbicacionArc;
        private Label lblErrores;
        private Panel pnlImportarPacientes;
        private TableLayoutPanel tlpImportarPacientes;
        private Panel pnlInterfaz;
        private Panel pnlDvg;
        private Panel pnlSeleccionarArch;
        private TableLayoutPanel tlpInterfaz;
        private TableLayoutPanel tlpDvg;
        private Panel pnlProyecto;
        private Panel pnlHojas;
        private TableLayoutPanel tlpSeleccionarArch;
        private TableLayoutPanel tlpProyectoCmb;
        private TableLayoutPanel tlpTieneEncabezado;
        private TableLayoutPanel tlpHojas;
        private Panel pnlResultadoOrg;
        private ProgressBar progressBarImportacion;
        private Label lblProgreso;
        private ComboBox cmbProyecto;
        private Label lblProyecto;
    }
}