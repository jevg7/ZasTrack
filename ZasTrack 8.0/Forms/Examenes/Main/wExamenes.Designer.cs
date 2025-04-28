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
            pnlContProc = new Panel();
            btnLimpiarFiltros = new Button();
            btnActualizar = new Button();
            panel1 = new Panel();
            label2 = new Label();
            cmbProyecto = new ComboBox();
            btnVerRecepcionados = new Button();
            btnVerProcesados = new Button();
            pnlPacientes = new Panel();
            pnlSearchFiltro = new Panel();
            dtpFechaRecepcion = new DateTimePicker();
            gbFiltroTipo = new GroupBox();
            chkFiltroSangre = new CheckBox();
            chkFiltroHeces = new CheckBox();
            chkFiltroOrina = new CheckBox();
            txtSearch = new TextBox();
            pnlContenedor.SuspendLayout();
            pnlContProc.SuspendLayout();
            panel1.SuspendLayout();
            pnlSearchFiltro.SuspendLayout();
            gbFiltroTipo.SuspendLayout();
            SuspendLayout();
            // 
            // pnlContenedor
            // 
            pnlContenedor.Controls.Add(pnlContProc);
            pnlContenedor.Controls.Add(pnlPacientes);
            pnlContenedor.Controls.Add(pnlSearchFiltro);
            pnlContenedor.Dock = DockStyle.Fill;
            pnlContenedor.Location = new Point(0, 0);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1353, 881);
            pnlContenedor.TabIndex = 1;
            pnlContenedor.Paint += panel1_Paint;
            // 
            // pnlContProc
            // 
            pnlContProc.BackColor = SystemColors.AppWorkspace;
            pnlContProc.Controls.Add(btnLimpiarFiltros);
            pnlContProc.Controls.Add(btnActualizar);
            pnlContProc.Controls.Add(panel1);
            pnlContProc.Controls.Add(btnVerRecepcionados);
            pnlContProc.Controls.Add(btnVerProcesados);
            pnlContProc.Dock = DockStyle.Top;
            pnlContProc.Location = new Point(0, 0);
            pnlContProc.Margin = new Padding(3, 4, 3, 4);
            pnlContProc.Name = "pnlContProc";
            pnlContProc.Size = new Size(1353, 56);
            pnlContProc.TabIndex = 2;
            // 
            // btnLimpiarFiltros
            // 
            btnLimpiarFiltros.Location = new Point(999, 8);
            btnLimpiarFiltros.Margin = new Padding(3, 4, 3, 4);
            btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            btnLimpiarFiltros.Size = new Size(86, 41);
            btnLimpiarFiltros.TabIndex = 5;
            btnLimpiarFiltros.Text = "Limpiar";
            btnLimpiarFiltros.UseVisualStyleBackColor = true;
            btnLimpiarFiltros.Click += btnLimpiarFiltros_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(899, 8);
            btnActualizar.Margin = new Padding(3, 4, 3, 4);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(88, 41);
            btnActualizar.TabIndex = 5;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click_1;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Controls.Add(label2);
            panel1.Controls.Add(cmbProyecto);
            panel1.Location = new Point(611, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(281, 41);
            panel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(11, 9);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 39;
            label2.Text = "Proyecto";
            // 
            // cmbProyecto
            // 
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(99, 4);
            cmbProyecto.Margin = new Padding(3, 4, 3, 4);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(178, 28);
            cmbProyecto.TabIndex = 38;
            cmbProyecto.SelectedIndexChanged += cmbProyecto_SelectedIndexChanged;
            // 
            // btnVerRecepcionados
            // 
            btnVerRecepcionados.Location = new Point(14, 9);
            btnVerRecepcionados.Margin = new Padding(3, 4, 3, 4);
            btnVerRecepcionados.Name = "btnVerRecepcionados";
            btnVerRecepcionados.Size = new Size(285, 40);
            btnVerRecepcionados.TabIndex = 2;
            btnVerRecepcionados.Text = "Recepcionados";
            btnVerRecepcionados.UseVisualStyleBackColor = true;
            // 
            // btnVerProcesados
            // 
            btnVerProcesados.Location = new Point(322, 9);
            btnVerProcesados.Margin = new Padding(3, 4, 3, 4);
            btnVerProcesados.Name = "btnVerProcesados";
            btnVerProcesados.Size = new Size(275, 40);
            btnVerProcesados.TabIndex = 1;
            btnVerProcesados.Text = "Procesados";
            btnVerProcesados.UseVisualStyleBackColor = true;
            btnVerProcesados.Click += btnVerProcesados_Click_1;
            // 
            // pnlPacientes
            // 
            pnlPacientes.AutoScroll = true;
            pnlPacientes.Location = new Point(0, 123);
            pnlPacientes.Margin = new Padding(3, 4, 3, 4);
            pnlPacientes.Name = "pnlPacientes";
            pnlPacientes.Size = new Size(1317, 759);
            pnlPacientes.TabIndex = 1;
            pnlPacientes.Paint += pnlPacientes_Paint;
            // 
            // pnlSearchFiltro
            // 
            pnlSearchFiltro.Controls.Add(dtpFechaRecepcion);
            pnlSearchFiltro.Controls.Add(gbFiltroTipo);
            pnlSearchFiltro.Controls.Add(txtSearch);
            pnlSearchFiltro.Location = new Point(0, 57);
            pnlSearchFiltro.Margin = new Padding(3, 4, 3, 4);
            pnlSearchFiltro.Name = "pnlSearchFiltro";
            pnlSearchFiltro.Size = new Size(1353, 57);
            pnlSearchFiltro.TabIndex = 0;
            // 
            // dtpFechaRecepcion
            // 
            dtpFechaRecepcion.Location = new Point(14, 15);
            dtpFechaRecepcion.Margin = new Padding(3, 4, 3, 4);
            dtpFechaRecepcion.Name = "dtpFechaRecepcion";
            dtpFechaRecepcion.Size = new Size(291, 27);
            dtpFechaRecepcion.TabIndex = 4;
            dtpFechaRecepcion.ValueChanged += dtpFechaRecepcion_ValueChanged_1;
            // 
            // gbFiltroTipo
            // 
            gbFiltroTipo.Controls.Add(chkFiltroSangre);
            gbFiltroTipo.Controls.Add(chkFiltroHeces);
            gbFiltroTipo.Controls.Add(chkFiltroOrina);
            gbFiltroTipo.Location = new Point(974, 7);
            gbFiltroTipo.Margin = new Padding(3, 4, 3, 4);
            gbFiltroTipo.Name = "gbFiltroTipo";
            gbFiltroTipo.Padding = new Padding(3, 4, 3, 4);
            gbFiltroTipo.Size = new Size(266, 47);
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
            // txtSearch
            // 
            txtSearch.Location = new Point(322, 15);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Buscar por Codigo o por Nombre";
            txtSearch.Size = new Size(644, 27);
            txtSearch.TabIndex = 1;
            // 
            // wExamenes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1353, 881);
            Controls.Add(pnlContenedor);
            FormBorderStyle = FormBorderStyle.None;
            Name = "wExamenes";
            Text = "wExamenesTest";
            pnlContenedor.ResumeLayout(false);
            pnlContProc.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlSearchFiltro.ResumeLayout(false);
            pnlSearchFiltro.PerformLayout();
            gbFiltroTipo.ResumeLayout(false);
            gbFiltroTipo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlContenedor;
        private Panel pnlContProc;
        private Panel panel1;
        private Label label2;
        private ComboBox cmbProyecto;
        private Button btnVerRecepcionados;
        private Button btnVerProcesados;
        private Panel pnlPacientes;
        private Panel pnlSearchFiltro;
        private TextBox txtSearch;
        private Button btnActualizar;
        private GroupBox gbFiltroTipo;
        private CheckBox chkFiltroSangre;
        private CheckBox chkFiltroHeces;
        private CheckBox chkFiltroOrina;
        private DateTimePicker dtpFechaRecepcion;
        private Button btnLimpiarFiltros;
    }
}