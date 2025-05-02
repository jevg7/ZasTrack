namespace ZasTrack.Forms.wProyectos 
{ 

    partial class wVerDetallesProyecto
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
            pnlInfoBasica = new Panel();
            lblEstadoArchivado = new Label();
            lblTotalExamenesProc = new Label();
            label7 = new Label();
            lblTotalMuestras = new Label();
            label5 = new Label();
            lblTotalPacientes = new Label();
            label3 = new Label();
            lblFechaFin = new Label();
            label4 = new Label();
            lblFechaInicio = new Label();
            label2 = new Label();
            lblCodigoProyecto = new Label();
            lblNombreProyecto = new Label();
            tabControlDetalles = new TabControl();
            tabPacientes = new TabPage();
            dgvPacientes = new DataGridView();
            colCodigoPaciente = new DataGridViewTextBoxColumn();
            colNombres = new DataGridViewTextBoxColumn();
            colApellidos = new DataGridViewTextBoxColumn();
            colGenero = new DataGridViewTextBoxColumn();
            colEdad = new DataGridViewTextBoxColumn();
            colFechaNacimiento = new DataGridViewTextBoxColumn();
            tabMuestras = new TabPage();
            dgvMuestras = new DataGridView();
            colNumMuestra = new DataGridViewTextBoxColumn();
            colCodigoPacienteMuestra = new DataGridViewTextBoxColumn();
            colNombrePacienteMuestra = new DataGridViewTextBoxColumn();
            colFechaRecepcion = new DataGridViewTextBoxColumn();
            colExamenesSolicitados = new DataGridViewTextBoxColumn();
            colEstadoMuestra = new DataGridViewTextBoxColumn();
            tabExamenes = new TabPage();
            dgvExamenes = new DataGridView();
            btnCerrar = new Button();
            colNumMuestraExamen = new DataGridViewTextBoxColumn();
            colPacienteExamen = new DataGridViewTextBoxColumn();
            colTipoExamen = new DataGridViewTextBoxColumn();
            colFechaProcesamiento = new DataGridViewTextBoxColumn();
            colEstadoExamen = new DataGridViewTextBoxColumn();
            colVerExamenDetalle = new DataGridViewButtonColumn();
            pnlInfoBasica.SuspendLayout();
            tabControlDetalles.SuspendLayout();
            tabPacientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPacientes).BeginInit();
            tabMuestras.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMuestras).BeginInit();
            tabExamenes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvExamenes).BeginInit();
            SuspendLayout();
            // 
            // pnlInfoBasica
            // 
            pnlInfoBasica.BackColor = SystemColors.ControlLight;
            pnlInfoBasica.Controls.Add(lblEstadoArchivado);
            pnlInfoBasica.Controls.Add(lblTotalExamenesProc);
            pnlInfoBasica.Controls.Add(label7);
            pnlInfoBasica.Controls.Add(lblTotalMuestras);
            pnlInfoBasica.Controls.Add(label5);
            pnlInfoBasica.Controls.Add(lblTotalPacientes);
            pnlInfoBasica.Controls.Add(label3);
            pnlInfoBasica.Controls.Add(lblFechaFin);
            pnlInfoBasica.Controls.Add(label4);
            pnlInfoBasica.Controls.Add(lblFechaInicio);
            pnlInfoBasica.Controls.Add(label2);
            pnlInfoBasica.Controls.Add(lblCodigoProyecto);
            pnlInfoBasica.Controls.Add(lblNombreProyecto);
            pnlInfoBasica.Dock = DockStyle.Top;
            pnlInfoBasica.Location = new Point(0, 0);
            pnlInfoBasica.Margin = new Padding(4, 5, 4, 5);
            pnlInfoBasica.Name = "pnlInfoBasica";
            pnlInfoBasica.Size = new Size(1045, 231);
            pnlInfoBasica.TabIndex = 0;
            pnlInfoBasica.Paint += pnlInfoBasica_Paint;
            // 
            // lblEstadoArchivado
            // 
            lblEstadoArchivado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblEstadoArchivado.AutoSize = true;
            lblEstadoArchivado.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEstadoArchivado.ForeColor = Color.Red;
            lblEstadoArchivado.Location = new Point(783, 20);
            lblEstadoArchivado.Margin = new Padding(4, 0, 4, 0);
            lblEstadoArchivado.Name = "lblEstadoArchivado";
            lblEstadoArchivado.Size = new Size(224, 25);
            lblEstadoArchivado.TabIndex = 12;
            lblEstadoArchivado.Text = "PROYECTO ARCHIVADO";
            lblEstadoArchivado.Visible = false;
            // 
            // lblTotalExamenesProc
            // 
            lblTotalExamenesProc.AutoSize = true;
            lblTotalExamenesProc.Location = new Point(533, 177);
            lblTotalExamenesProc.Margin = new Padding(4, 0, 4, 0);
            lblTotalExamenesProc.Name = "lblTotalExamenesProc";
            lblTotalExamenesProc.Size = new Size(15, 20);
            lblTotalExamenesProc.TabIndex = 11;
            lblTotalExamenesProc.Text = "-";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(373, 177);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(147, 19);
            label7.TabIndex = 10;
            label7.Text = "Exámenes Procesados:";
            // 
            // lblTotalMuestras
            // 
            lblTotalMuestras.AutoSize = true;
            lblTotalMuestras.Location = new Point(153, 177);
            lblTotalMuestras.Margin = new Padding(4, 0, 4, 0);
            lblTotalMuestras.Name = "lblTotalMuestras";
            lblTotalMuestras.Size = new Size(15, 20);
            lblTotalMuestras.TabIndex = 9;
            lblTotalMuestras.Text = "-";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(20, 177);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(104, 19);
            label5.TabIndex = 8;
            label5.Text = "Total Muestras:";
            // 
            // lblTotalPacientes
            // 
            lblTotalPacientes.AutoSize = true;
            lblTotalPacientes.Location = new Point(533, 138);
            lblTotalPacientes.Margin = new Padding(4, 0, 4, 0);
            lblTotalPacientes.Name = "lblTotalPacientes";
            lblTotalPacientes.Size = new Size(15, 20);
            lblTotalPacientes.TabIndex = 7;
            lblTotalPacientes.Text = "-";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(373, 138);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(106, 19);
            label3.TabIndex = 6;
            label3.Text = "Total Pacientes:";
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Location = new Point(153, 138);
            lblFechaFin.Margin = new Padding(4, 0, 4, 0);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(15, 20);
            lblFechaFin.TabIndex = 5;
            lblFechaFin.Text = "-";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(20, 138);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(71, 19);
            label4.TabIndex = 4;
            label4.Text = "Fecha Fin:";
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Location = new Point(153, 100);
            lblFechaInicio.Margin = new Padding(4, 0, 4, 0);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(15, 20);
            lblFechaInicio.TabIndex = 3;
            lblFechaInicio.Text = "-";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(20, 100);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(87, 19);
            label2.TabIndex = 2;
            label2.Text = "Fecha Inicio:";
            // 
            // lblCodigoProyecto
            // 
            lblCodigoProyecto.AutoSize = true;
            lblCodigoProyecto.ForeColor = SystemColors.GrayText;
            lblCodigoProyecto.Location = new Point(20, 54);
            lblCodigoProyecto.Margin = new Padding(4, 0, 4, 0);
            lblCodigoProyecto.Name = "lblCodigoProyecto";
            lblCodigoProyecto.Size = new Size(58, 20);
            lblCodigoProyecto.TabIndex = 1;
            lblCodigoProyecto.Text = "Codigo";
            // 
            // lblNombreProyecto
            // 
            lblNombreProyecto.AutoSize = true;
            lblNombreProyecto.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNombreProyecto.Location = new Point(16, 14);
            lblNombreProyecto.Margin = new Padding(4, 0, 4, 0);
            lblNombreProyecto.Name = "lblNombreProyecto";
            lblNombreProyecto.Size = new Size(96, 28);
            lblNombreProyecto.TabIndex = 0;
            lblNombreProyecto.Text = "Proyecto";
            // 
            // tabControlDetalles
            // 
            tabControlDetalles.Controls.Add(tabPacientes);
            tabControlDetalles.Controls.Add(tabMuestras);
            tabControlDetalles.Controls.Add(tabExamenes);
            tabControlDetalles.Dock = DockStyle.Fill;
            tabControlDetalles.Location = new Point(0, 231);
            tabControlDetalles.Margin = new Padding(4, 5, 4, 5);
            tabControlDetalles.Name = "tabControlDetalles";
            tabControlDetalles.SelectedIndex = 0;
            tabControlDetalles.Size = new Size(1045, 632);
            tabControlDetalles.TabIndex = 1;
            // 
            // tabPacientes
            // 
            tabPacientes.Controls.Add(dgvPacientes);
            tabPacientes.Location = new Point(4, 29);
            tabPacientes.Margin = new Padding(4, 5, 4, 5);
            tabPacientes.Name = "tabPacientes";
            tabPacientes.Padding = new Padding(4, 5, 4, 5);
            tabPacientes.Size = new Size(1037, 599);
            tabPacientes.TabIndex = 0;
            tabPacientes.Text = "Pacientes";
            tabPacientes.UseVisualStyleBackColor = true;
            // 
            // dgvPacientes
            // 
            dgvPacientes.AllowUserToAddRows = false;
            dgvPacientes.AllowUserToDeleteRows = false;
            dgvPacientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPacientes.Columns.AddRange(new DataGridViewColumn[] { colCodigoPaciente, colNombres, colApellidos, colGenero, colEdad, colFechaNacimiento });
            dgvPacientes.Dock = DockStyle.Fill;
            dgvPacientes.Location = new Point(4, 5);
            dgvPacientes.Margin = new Padding(4, 5, 4, 5);
            dgvPacientes.Name = "dgvPacientes";
            dgvPacientes.ReadOnly = true;
            dgvPacientes.RowHeadersWidth = 51;
            dgvPacientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPacientes.Size = new Size(1029, 589);
            dgvPacientes.TabIndex = 0;
            // 
            // colCodigoPaciente
            // 
            colCodigoPaciente.HeaderText = "Código Paciente";
            colCodigoPaciente.MinimumWidth = 6;
            colCodigoPaciente.Name = "colCodigoPaciente";
            colCodigoPaciente.ReadOnly = true;
            colCodigoPaciente.Width = 125;
            // 
            // colNombres
            // 
            colNombres.HeaderText = "Nombres";
            colNombres.MinimumWidth = 6;
            colNombres.Name = "colNombres";
            colNombres.ReadOnly = true;
            colNombres.Width = 150;
            // 
            // colApellidos
            // 
            colApellidos.HeaderText = "Apellidos";
            colApellidos.MinimumWidth = 6;
            colApellidos.Name = "colApellidos";
            colApellidos.ReadOnly = true;
            colApellidos.Width = 150;
            // 
            // colGenero
            // 
            colGenero.HeaderText = "Género";
            colGenero.MinimumWidth = 6;
            colGenero.Name = "colGenero";
            colGenero.ReadOnly = true;
            colGenero.Width = 80;
            // 
            // colEdad
            // 
            colEdad.HeaderText = "Edad";
            colEdad.MinimumWidth = 6;
            colEdad.Name = "colEdad";
            colEdad.ReadOnly = true;
            colEdad.Width = 50;
            // 
            // colFechaNacimiento
            // 
            colFechaNacimiento.HeaderText = "Fecha Nacimiento";
            colFechaNacimiento.MinimumWidth = 6;
            colFechaNacimiento.Name = "colFechaNacimiento";
            colFechaNacimiento.ReadOnly = true;
            colFechaNacimiento.Width = 125;
            // 
            // tabMuestras
            // 
            tabMuestras.Controls.Add(dgvMuestras);
            tabMuestras.Location = new Point(4, 29);
            tabMuestras.Margin = new Padding(4, 5, 4, 5);
            tabMuestras.Name = "tabMuestras";
            tabMuestras.Padding = new Padding(4, 5, 4, 5);
            tabMuestras.Size = new Size(1037, 599);
            tabMuestras.TabIndex = 1;
            tabMuestras.Text = "Muestras";
            tabMuestras.UseVisualStyleBackColor = true;
            // 
            // dgvMuestras
            // 
            dgvMuestras.AllowUserToAddRows = false;
            dgvMuestras.AllowUserToDeleteRows = false;
            dgvMuestras.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMuestras.Columns.AddRange(new DataGridViewColumn[] { colNumMuestra, colCodigoPacienteMuestra, colNombrePacienteMuestra, colFechaRecepcion, colExamenesSolicitados, colEstadoMuestra });
            dgvMuestras.Dock = DockStyle.Fill;
            dgvMuestras.Location = new Point(4, 5);
            dgvMuestras.Margin = new Padding(4, 5, 4, 5);
            dgvMuestras.Name = "dgvMuestras";
            dgvMuestras.ReadOnly = true;
            dgvMuestras.RowHeadersWidth = 51;
            dgvMuestras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMuestras.Size = new Size(1029, 589);
            dgvMuestras.TabIndex = 0;
            // 
            // colNumMuestra
            // 
            colNumMuestra.HeaderText = "Nº Muestra";
            colNumMuestra.MinimumWidth = 6;
            colNumMuestra.Name = "colNumMuestra";
            colNumMuestra.ReadOnly = true;
            colNumMuestra.Width = 125;
            // 
            // colCodigoPacienteMuestra
            // 
            colCodigoPacienteMuestra.HeaderText = "Código Paciente";
            colCodigoPacienteMuestra.MinimumWidth = 6;
            colCodigoPacienteMuestra.Name = "colCodigoPacienteMuestra";
            colCodigoPacienteMuestra.ReadOnly = true;
            colCodigoPacienteMuestra.Width = 125;
            // 
            // colNombrePacienteMuestra
            // 
            colNombrePacienteMuestra.HeaderText = "Nombre Paciente";
            colNombrePacienteMuestra.MinimumWidth = 6;
            colNombrePacienteMuestra.Name = "colNombrePacienteMuestra";
            colNombrePacienteMuestra.ReadOnly = true;
            colNombrePacienteMuestra.Width = 200;
            // 
            // colFechaRecepcion
            // 
            colFechaRecepcion.HeaderText = "Fecha Recepción";
            colFechaRecepcion.MinimumWidth = 6;
            colFechaRecepcion.Name = "colFechaRecepcion";
            colFechaRecepcion.ReadOnly = true;
            colFechaRecepcion.Width = 125;
            // 
            // colExamenesSolicitados
            // 
            colExamenesSolicitados.HeaderText = "Exámenes Solicitados";
            colExamenesSolicitados.MinimumWidth = 6;
            colExamenesSolicitados.Name = "colExamenesSolicitados";
            colExamenesSolicitados.ReadOnly = true;
            colExamenesSolicitados.Width = 150;
            // 
            // colEstadoMuestra
            // 
            colEstadoMuestra.HeaderText = "Estado";
            colEstadoMuestra.MinimumWidth = 6;
            colEstadoMuestra.Name = "colEstadoMuestra";
            colEstadoMuestra.ReadOnly = true;
            colEstadoMuestra.Width = 80;
            // 
            // tabExamenes
            // 
            tabExamenes.Controls.Add(dgvExamenes);
            tabExamenes.Location = new Point(4, 29);
            tabExamenes.Margin = new Padding(4, 5, 4, 5);
            tabExamenes.Name = "tabExamenes";
            tabExamenes.Padding = new Padding(4, 5, 4, 5);
            tabExamenes.Size = new Size(1037, 599);
            tabExamenes.TabIndex = 2;
            tabExamenes.Text = "Exámenes";
            tabExamenes.UseVisualStyleBackColor = true;
            // 
            // dgvExamenes
            // 
            dgvExamenes.AllowUserToAddRows = false;
            dgvExamenes.AllowUserToDeleteRows = false;
            dgvExamenes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExamenes.Columns.AddRange(new DataGridViewColumn[] { colNumMuestraExamen, colPacienteExamen, colTipoExamen, colFechaProcesamiento, colEstadoExamen, colVerExamenDetalle });
            dgvExamenes.Dock = DockStyle.Fill;
            dgvExamenes.Location = new Point(4, 5);
            dgvExamenes.Margin = new Padding(4, 5, 4, 5);
            dgvExamenes.Name = "dgvExamenes";
            dgvExamenes.ReadOnly = true;
            dgvExamenes.RowHeadersWidth = 51;
            dgvExamenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExamenes.Size = new Size(1029, 589);
            dgvExamenes.TabIndex = 0;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCerrar.Location = new Point(929, 811);
            btnCerrar.Margin = new Padding(4, 5, 4, 5);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(100, 35);
            btnCerrar.TabIndex = 2;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            // 
            // colNumMuestraExamen
            // 
            colNumMuestraExamen.HeaderText = "Nº Muestra";
            colNumMuestraExamen.MinimumWidth = 6;
            colNumMuestraExamen.Name = "colNumMuestraExamen";
            colNumMuestraExamen.ReadOnly = true;
            colNumMuestraExamen.Width = 125;
            // 
            // colPacienteExamen
            // 
            colPacienteExamen.HeaderText = "Paciente";
            colPacienteExamen.MinimumWidth = 6;
            colPacienteExamen.Name = "colPacienteExamen";
            colPacienteExamen.ReadOnly = true;
            colPacienteExamen.Width = 200;
            // 
            // colTipoExamen
            // 
            colTipoExamen.HeaderText = "Tipo Examen";
            colTipoExamen.MinimumWidth = 6;
            colTipoExamen.Name = "colTipoExamen";
            colTipoExamen.ReadOnly = true;
            colTipoExamen.Width = 150;
            // 
            // colFechaProcesamiento
            // 
            colFechaProcesamiento.HeaderText = "Fecha Procesamiento";
            colFechaProcesamiento.MinimumWidth = 6;
            colFechaProcesamiento.Name = "colFechaProcesamiento";
            colFechaProcesamiento.ReadOnly = true;
            colFechaProcesamiento.Width = 125;
            // 
            // colEstadoExamen
            // 
            colEstadoExamen.HeaderText = "Estado";
            colEstadoExamen.MinimumWidth = 6;
            colEstadoExamen.Name = "colEstadoExamen";
            colEstadoExamen.ReadOnly = true;
            colEstadoExamen.Width = 80;
            // 
            // colVerExamenDetalle
            // 
            colVerExamenDetalle.HeaderText = "Accion";
            colVerExamenDetalle.MinimumWidth = 6;
            colVerExamenDetalle.Name = "colVerExamenDetalle";
            colVerExamenDetalle.ReadOnly = true;
            colVerExamenDetalle.Resizable = DataGridViewTriState.True;
            colVerExamenDetalle.SortMode = DataGridViewColumnSortMode.Automatic;
            colVerExamenDetalle.Text = "Ver Detalles";
            colVerExamenDetalle.UseColumnTextForButtonValue = true;
            colVerExamenDetalle.Width = 125;
            // 
            // wVerDetallesProyecto
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 863);
            Controls.Add(btnCerrar);
            Controls.Add(tabControlDetalles);
            Controls.Add(pnlInfoBasica);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "wVerDetallesProyecto";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Detalles del Proyecto";
            Load += wVerDetallesProyecto_Load;
            pnlInfoBasica.ResumeLayout(false);
            pnlInfoBasica.PerformLayout();
            tabControlDetalles.ResumeLayout(false);
            tabPacientes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPacientes).EndInit();
            tabMuestras.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMuestras).EndInit();
            tabExamenes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvExamenes).EndInit();
            ResumeLayout(false);

        }

        #endregion

        // Declaraciones de los controles (el diseñador las genera aquí)
        private System.Windows.Forms.Panel pnlInfoBasica;
        private System.Windows.Forms.Label lblNombreProyecto;
        private System.Windows.Forms.Label lblCodigoProyecto;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotalPacientes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalMuestras;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalExamenesProc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblEstadoArchivado;
        private System.Windows.Forms.TabControl tabControlDetalles;
        private System.Windows.Forms.TabPage tabPacientes;
        private System.Windows.Forms.DataGridView dgvPacientes;
        private System.Windows.Forms.TabPage tabMuestras;
        private System.Windows.Forms.DataGridView dgvMuestras;
        private System.Windows.Forms.TabPage tabExamenes;
        private System.Windows.Forms.DataGridView dgvExamenes;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumMuestra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigoPacienteMuestra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombrePacienteMuestra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaRecepcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExamenesSolicitados;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstadoMuestra;
        private DataGridViewTextBoxColumn colCodigoPaciente;
        private DataGridViewTextBoxColumn colNombres;
        private DataGridViewTextBoxColumn colApellidos;
        private DataGridViewTextBoxColumn colGenero;
        private DataGridViewTextBoxColumn colEdad;
        private DataGridViewTextBoxColumn colFechaNacimiento;
        private DataGridViewTextBoxColumn colNumMuestraExamen;
        private DataGridViewTextBoxColumn colPacienteExamen;
        private DataGridViewTextBoxColumn colTipoExamen;
        private DataGridViewTextBoxColumn colFechaProcesamiento;
        private DataGridViewTextBoxColumn colEstadoExamen;
        private DataGridViewButtonColumn colVerExamenDetalle;
    }
}
