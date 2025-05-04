namespace ZasTrack.Forms.Examenes
{
    partial class wExamenes
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
            pnlContenedor = new Panel();
            tlpOrganizador = new TableLayoutPanel();
            pnlContProc = new Panel();
            tlpBotones = new TableLayoutPanel();
            btnVerRecepcionados = new Button();
            btnVerProcesados = new Button();
            pnlProyectos = new Panel();
            tlpProyecto = new TableLayoutPanel();
            lblProyecto = new Label();
            cmbProyecto = new ComboBox();
            btnActualizar = new Button();
            btnLimpiarFiltros = new Button();
            pnlSearchFiltro = new Panel();
            tlpFiltrado = new TableLayoutPanel();
            gbFiltroTipo = new GroupBox();
            chkFiltroSangre = new CheckBox();
            chkFiltroHeces = new CheckBox();
            chkFiltroOrina = new CheckBox();
            dtpFechaRecepcion = new DateTimePicker();
            txtSearch = new TextBox();
            flpListaMuestras = new FlowLayoutPanel();
            pnlContenedor.SuspendLayout();
            tlpOrganizador.SuspendLayout();
            pnlContProc.SuspendLayout();
            tlpBotones.SuspendLayout();
            pnlProyectos.SuspendLayout();
            tlpProyecto.SuspendLayout();
            pnlSearchFiltro.SuspendLayout();
            tlpFiltrado.SuspendLayout();
            gbFiltroTipo.SuspendLayout();
            SuspendLayout();
            // 
            // pnlContenedor
            // 
            pnlContenedor.Controls.Add(tlpOrganizador);
            pnlContenedor.Dock = DockStyle.Fill;
            pnlContenedor.Location = new Point(0, 0);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1334, 753);
            pnlContenedor.TabIndex = 1;
            pnlContenedor.Paint += panel1_Paint;
            // 
            // tlpOrganizador
            // 
            tlpOrganizador.ColumnCount = 1;
            tlpOrganizador.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpOrganizador.Controls.Add(pnlContProc, 0, 0);
            tlpOrganizador.Controls.Add(pnlSearchFiltro, 0, 1);
            tlpOrganizador.Controls.Add(flpListaMuestras, 0, 2);
            tlpOrganizador.Dock = DockStyle.Fill;
            tlpOrganizador.Location = new Point(0, 0);
            tlpOrganizador.Name = "tlpOrganizador";
            tlpOrganizador.RowCount = 3;
            tlpOrganizador.RowStyles.Add(new RowStyle());
            tlpOrganizador.RowStyles.Add(new RowStyle());
            tlpOrganizador.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpOrganizador.Size = new Size(1334, 753);
            tlpOrganizador.TabIndex = 0;
            // 
            // pnlContProc
            // 
            pnlContProc.BackColor = SystemColors.AppWorkspace;
            pnlContProc.Controls.Add(tlpBotones);
            pnlContProc.Dock = DockStyle.Top;
            pnlContProc.Location = new Point(3, 4);
            pnlContProc.Margin = new Padding(3, 4, 3, 4);
            pnlContProc.Name = "pnlContProc";
            pnlContProc.Size = new Size(1328, 56);
            pnlContProc.TabIndex = 2;
            // 
            // tlpBotones
            // 
            tlpBotones.ColumnCount = 5;
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.6279068F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.069767F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.1616535F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.022556F));
            tlpBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.398497F));
            tlpBotones.Controls.Add(btnVerRecepcionados, 0, 0);
            tlpBotones.Controls.Add(btnVerProcesados, 1, 0);
            tlpBotones.Controls.Add(pnlProyectos, 2, 0);
            tlpBotones.Controls.Add(btnActualizar, 3, 0);
            tlpBotones.Controls.Add(btnLimpiarFiltros, 4, 0);
            tlpBotones.Dock = DockStyle.Top;
            tlpBotones.Location = new Point(0, 0);
            tlpBotones.Name = "tlpBotones";
            tlpBotones.RowCount = 1;
            tlpBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBotones.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpBotones.Size = new Size(1328, 66);
            tlpBotones.TabIndex = 0;
            tlpBotones.Paint += tlpBotones_Paint;
            // 
            // btnVerRecepcionados
            // 
            btnVerRecepcionados.AutoSize = true;
            btnVerRecepcionados.Dock = DockStyle.Top;
            btnVerRecepcionados.Location = new Point(3, 4);
            btnVerRecepcionados.Margin = new Padding(3, 4, 3, 4);
            btnVerRecepcionados.Name = "btnVerRecepcionados";
            btnVerRecepcionados.Size = new Size(359, 38);
            btnVerRecepcionados.TabIndex = 2;
            btnVerRecepcionados.Text = "Recepcionados";
            btnVerRecepcionados.UseVisualStyleBackColor = true;
            // 
            // btnVerProcesados
            // 
            btnVerProcesados.Dock = DockStyle.Top;
            btnVerProcesados.Location = new Point(368, 4);
            btnVerProcesados.Margin = new Padding(3, 4, 3, 4);
            btnVerProcesados.Name = "btnVerProcesados";
            btnVerProcesados.Size = new Size(352, 40);
            btnVerProcesados.TabIndex = 1;
            btnVerProcesados.Text = "Procesados";
            btnVerProcesados.UseVisualStyleBackColor = true;
            btnVerProcesados.Click += btnVerProcesados_Click_1;
            // 
            // pnlProyectos
            // 
            pnlProyectos.BackColor = SystemColors.ActiveCaptionText;
            pnlProyectos.Controls.Add(tlpProyecto);
            pnlProyectos.Dock = DockStyle.Top;
            pnlProyectos.Location = new Point(726, 3);
            pnlProyectos.Name = "pnlProyectos";
            pnlProyectos.Size = new Size(353, 41);
            pnlProyectos.TabIndex = 0;
            // 
            // tlpProyecto
            // 
            tlpProyecto.ColumnCount = 2;
            tlpProyecto.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tlpProyecto.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpProyecto.Controls.Add(lblProyecto, 0, 0);
            tlpProyecto.Controls.Add(cmbProyecto, 1, 0);
            tlpProyecto.Dock = DockStyle.Top;
            tlpProyecto.Location = new Point(0, 0);
            tlpProyecto.Name = "tlpProyecto";
            tlpProyecto.RowCount = 1;
            tlpProyecto.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpProyecto.Size = new Size(353, 42);
            tlpProyecto.TabIndex = 40;
            // 
            // lblProyecto
            // 
            lblProyecto.AutoSize = true;
            lblProyecto.BackColor = SystemColors.ButtonHighlight;
            lblProyecto.Dock = DockStyle.Top;
            lblProyecto.Location = new Point(10, 10);
            lblProyecto.Margin = new Padding(10);
            lblProyecto.Name = "lblProyecto";
            lblProyecto.Size = new Size(70, 20);
            lblProyecto.TabIndex = 39;
            lblProyecto.Text = "Proyecto";
            lblProyecto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmbProyecto
            // 
            cmbProyecto.Dock = DockStyle.Top;
            cmbProyecto.Location = new Point(95, 5);
            cmbProyecto.Margin = new Padding(5);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(253, 28);
            cmbProyecto.TabIndex = 38;
            cmbProyecto.SelectedIndexChanged += cmbProyecto_SelectedIndexChanged;
            // 
            // btnActualizar
            // 
            btnActualizar.Dock = DockStyle.Top;
            btnActualizar.Location = new Point(1085, 4);
            btnActualizar.Margin = new Padding(3, 4, 3, 4);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(113, 41);
            btnActualizar.TabIndex = 5;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click_1;
            // 
            // btnLimpiarFiltros
            // 
            btnLimpiarFiltros.Dock = DockStyle.Top;
            btnLimpiarFiltros.Location = new Point(1204, 4);
            btnLimpiarFiltros.Margin = new Padding(3, 4, 3, 4);
            btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            btnLimpiarFiltros.Size = new Size(121, 41);
            btnLimpiarFiltros.TabIndex = 5;
            btnLimpiarFiltros.Text = "Limpiar";
            btnLimpiarFiltros.UseVisualStyleBackColor = true;
            btnLimpiarFiltros.Click += btnLimpiarFiltros_Click;
            // 
            // pnlSearchFiltro
            // 
            pnlSearchFiltro.Controls.Add(tlpFiltrado);
            pnlSearchFiltro.Dock = DockStyle.Top;
            pnlSearchFiltro.Location = new Point(3, 68);
            pnlSearchFiltro.Margin = new Padding(3, 4, 3, 4);
            pnlSearchFiltro.Name = "pnlSearchFiltro";
            pnlSearchFiltro.Size = new Size(1328, 57);
            pnlSearchFiltro.TabIndex = 0;
            // 
            // tlpFiltrado
            // 
            tlpFiltrado.ColumnCount = 3;
            tlpFiltrado.ColumnStyles.Add(new ColumnStyle());
            tlpFiltrado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFiltrado.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 257F));
            tlpFiltrado.Controls.Add(gbFiltroTipo, 2, 0);
            tlpFiltrado.Controls.Add(dtpFechaRecepcion, 0, 0);
            tlpFiltrado.Controls.Add(txtSearch, 1, 0);
            tlpFiltrado.Dock = DockStyle.Fill;
            tlpFiltrado.Location = new Point(0, 0);
            tlpFiltrado.Name = "tlpFiltrado";
            tlpFiltrado.RowCount = 1;
            tlpFiltrado.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFiltrado.Size = new Size(1328, 57);
            tlpFiltrado.TabIndex = 0;
            // 
            // gbFiltroTipo
            // 
            gbFiltroTipo.Controls.Add(chkFiltroSangre);
            gbFiltroTipo.Controls.Add(chkFiltroHeces);
            gbFiltroTipo.Controls.Add(chkFiltroOrina);
            gbFiltroTipo.Location = new Point(1074, 4);
            gbFiltroTipo.Margin = new Padding(3, 4, 3, 4);
            gbFiltroTipo.Name = "gbFiltroTipo";
            gbFiltroTipo.Padding = new Padding(3, 4, 3, 4);
            gbFiltroTipo.Size = new Size(249, 49);
            gbFiltroTipo.TabIndex = 2;
            gbFiltroTipo.TabStop = false;
            gbFiltroTipo.Text = "Filtrar por Tipo Requerido:";
            // 
            // chkFiltroSangre
            // 
            chkFiltroSangre.AutoSize = true;
            chkFiltroSangre.Location = new Point(153, 21);
            chkFiltroSangre.Margin = new Padding(3, 4, 3, 4);
            chkFiltroSangre.Name = "chkFiltroSangre";
            chkFiltroSangre.Size = new Size(77, 24);
            chkFiltroSangre.TabIndex = 2;
            chkFiltroSangre.Text = "Sangre";
            chkFiltroSangre.UseVisualStyleBackColor = true;
            // 
            // chkFiltroHeces
            // 
            chkFiltroHeces.AutoSize = true;
            chkFiltroHeces.Location = new Point(80, 21);
            chkFiltroHeces.Margin = new Padding(3, 4, 3, 4);
            chkFiltroHeces.Name = "chkFiltroHeces";
            chkFiltroHeces.Size = new Size(71, 24);
            chkFiltroHeces.TabIndex = 1;
            chkFiltroHeces.Text = "Heces";
            chkFiltroHeces.UseVisualStyleBackColor = true;
            // 
            // chkFiltroOrina
            // 
            chkFiltroOrina.AutoSize = true;
            chkFiltroOrina.Location = new Point(10, 21);
            chkFiltroOrina.Margin = new Padding(3, 4, 3, 4);
            chkFiltroOrina.Name = "chkFiltroOrina";
            chkFiltroOrina.Size = new Size(67, 24);
            chkFiltroOrina.TabIndex = 0;
            chkFiltroOrina.Text = "Orina";
            chkFiltroOrina.UseVisualStyleBackColor = true;
            // 
            // dtpFechaRecepcion
            // 
            dtpFechaRecepcion.Dock = DockStyle.Left;
            dtpFechaRecepcion.Location = new Point(3, 4);
            dtpFechaRecepcion.Margin = new Padding(3, 4, 3, 4);
            dtpFechaRecepcion.Name = "dtpFechaRecepcion";
            dtpFechaRecepcion.Size = new Size(295, 27);
            dtpFechaRecepcion.TabIndex = 4;
            dtpFechaRecepcion.ValueChanged += dtpFechaRecepcion_ValueChanged_1;
            // 
            // txtSearch
            // 
            txtSearch.Dock = DockStyle.Top;
            txtSearch.Location = new Point(304, 4);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Buscar por Codigo o por Nombre";
            txtSearch.Size = new Size(764, 27);
            txtSearch.TabIndex = 1;
            // 
            // flpListaMuestras
            // 
            flpListaMuestras.AutoScroll = true;
            flpListaMuestras.Dock = DockStyle.Fill;
            flpListaMuestras.FlowDirection = FlowDirection.TopDown;
            flpListaMuestras.Location = new Point(3, 132);
            flpListaMuestras.Name = "flpListaMuestras";
            flpListaMuestras.Size = new Size(1328, 618);
            flpListaMuestras.TabIndex = 3;
            flpListaMuestras.WrapContents = false;
            // 
            // wExamenes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1334, 753);
            Controls.Add(pnlContenedor);
            FormBorderStyle = FormBorderStyle.None;
            Name = "wExamenes";
            Text = "wExamenesTest";
            pnlContenedor.ResumeLayout(false);
            tlpOrganizador.ResumeLayout(false);
            pnlContProc.ResumeLayout(false);
            tlpBotones.ResumeLayout(false);
            tlpBotones.PerformLayout();
            pnlProyectos.ResumeLayout(false);
            tlpProyecto.ResumeLayout(false);
            tlpProyecto.PerformLayout();
            pnlSearchFiltro.ResumeLayout(false);
            tlpFiltrado.ResumeLayout(false);
            tlpFiltrado.PerformLayout();
            gbFiltroTipo.ResumeLayout(false);
            gbFiltroTipo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlContenedor;
        private Panel pnlContProc;
        private ComboBox cmbProyecto;
        private Button btnVerProcesados;
        private Panel pnlSearchFiltro;
        private TextBox txtSearch;
        private Button btnActualizar;
        private GroupBox gbFiltroTipo;
        private CheckBox chkFiltroSangre;
        private CheckBox chkFiltroHeces;
        private CheckBox chkFiltroOrina;
        private DateTimePicker dtpFechaRecepcion;
        private Button btnLimpiarFiltros;
        private TableLayoutPanel tlpOrganizador;
        private TableLayoutPanel tlpBotones;
        private Button btnVerRecepcionados;
        private TableLayoutPanel tlpFiltrado;
        private Panel pnlProyectos;
        private Label lblProyecto;
        private TableLayoutPanel tlpProyecto;
        private FlowLayoutPanel flpListaMuestras;
    }
}