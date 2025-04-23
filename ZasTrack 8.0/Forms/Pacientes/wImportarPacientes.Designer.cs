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
            progressBarImportacion = new ProgressBar();
            lblHojas = new Label();
            dgvResultados = new DataGridView();
            lblProgreso = new Label();
            lblProyecto = new Label();
            txtRutaArchivo = new TextBox();
            cmbProyecto = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            SuspendLayout();
            // 
            // btnSeleccionarArchivo
            // 
            btnSeleccionarArchivo.Location = new Point(50, 12);
            btnSeleccionarArchivo.Name = "btnSeleccionarArchivo";
            btnSeleccionarArchivo.Size = new Size(314, 84);
            btnSeleccionarArchivo.TabIndex = 0;
            btnSeleccionarArchivo.Text = "Seleccionar Archivo";
            btnSeleccionarArchivo.UseVisualStyleBackColor = true;
            btnSeleccionarArchivo.Click += btnSeleccionarArchivo_Click;
            // 
            // cmbHojas
            // 
            cmbHojas.FormattingEnabled = true;
            cmbHojas.Location = new Point(149, 144);
            cmbHojas.Name = "cmbHojas";
            cmbHojas.Size = new Size(151, 28);
            cmbHojas.TabIndex = 1;
            // 
            // chkTieneEncabezado
            // 
            chkTieneEncabezado.AutoSize = true;
            chkTieneEncabezado.Location = new Point(153, 227);
            chkTieneEncabezado.Name = "chkTieneEncabezado";
            chkTieneEncabezado.Size = new Size(155, 24);
            chkTieneEncabezado.TabIndex = 3;
            chkTieneEncabezado.Text = "Tiene Encabezado.";
            chkTieneEncabezado.UseVisualStyleBackColor = true;
            // 
            // btnImportar
            // 
            btnImportar.Location = new Point(149, 272);
            btnImportar.Name = "btnImportar";
            btnImportar.Size = new Size(155, 41);
            btnImportar.TabIndex = 4;
            btnImportar.Text = "Importar";
            btnImportar.UseVisualStyleBackColor = true;
            btnImportar.Click += btnImportar_Click;
            // 
            // progressBarImportacion
            // 
            progressBarImportacion.Location = new Point(457, 482);
            progressBarImportacion.Name = "progressBarImportacion";
            progressBarImportacion.Size = new Size(556, 11);
            progressBarImportacion.TabIndex = 5;
            // 
            // lblHojas
            // 
            lblHojas.AutoSize = true;
            lblHojas.Location = new Point(79, 147);
            lblHojas.Name = "lblHojas";
            lblHojas.Size = new Size(47, 20);
            lblHojas.TabIndex = 6;
            lblHojas.Text = "Hojas";
            // 
            // dgvResultados
            // 
            dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResultados.Location = new Point(386, 39);
            dgvResultados.Name = "dgvResultados";
            dgvResultados.RowHeadersWidth = 51;
            dgvResultados.Size = new Size(627, 420);
            dgvResultados.TabIndex = 7;
            // 
            // lblProgreso
            // 
            lblProgreso.AutoSize = true;
            lblProgreso.Location = new Point(386, 473);
            lblProgreso.Name = "lblProgreso";
            lblProgreso.Size = new Size(68, 20);
            lblProgreso.TabIndex = 8;
            lblProgreso.Text = "Progreso";
            // 
            // lblProyecto
            // 
            lblProyecto.AutoSize = true;
            lblProyecto.Location = new Point(80, 201);
            lblProyecto.Name = "lblProyecto";
            lblProyecto.Size = new Size(67, 20);
            lblProyecto.TabIndex = 9;
            lblProyecto.Text = "Proyecto";
            // 
            // txtRutaArchivo
            // 
            txtRutaArchivo.Location = new Point(50, 102);
            txtRutaArchivo.Name = "txtRutaArchivo";
            txtRutaArchivo.ReadOnly = true;
            txtRutaArchivo.Size = new Size(314, 27);
            txtRutaArchivo.TabIndex = 10;
            // 
            // cmbProyecto
            // 
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(149, 193);
            cmbProyecto.Margin = new Padding(3, 4, 3, 4);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(215, 28);
            cmbProyecto.TabIndex = 14;
            cmbProyecto.SelectedIndexChanged += cmbProyecto_SelectedIndexChanged;
            // 
            // wImportarPacientes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1025, 623);
            ControlBox = false;
            Controls.Add(cmbProyecto);
            Controls.Add(txtRutaArchivo);
            Controls.Add(lblProyecto);
            Controls.Add(lblProgreso);
            Controls.Add(dgvResultados);
            Controls.Add(lblHojas);
            Controls.Add(progressBarImportacion);
            Controls.Add(btnImportar);
            Controls.Add(chkTieneEncabezado);
            Controls.Add(cmbHojas);
            Controls.Add(btnSeleccionarArchivo);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "wImportarPacientes";
            Text = "wImportarPacientes";
            Load += wImportarPacientes_Load;
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSeleccionarArchivo;
        private ComboBox cmbHojas;
        private CheckBox chkTieneEncabezado;
        private Button btnImportar;
        private ProgressBar progressBarImportacion;
        private Label lblHojas;
        private DataGridView dgvResultados;
        private Label lblProgreso;
        private Label lblProyecto;
        private TextBox txtRutaArchivo;
        private ComboBox cmbProyecto;
    }
}