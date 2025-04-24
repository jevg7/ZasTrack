namespace ZasTrack.Forms.Pacientes
{
    partial class wResultadosImportacion
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
            lblResumen = new Label();
            dgvExitosos = new DataGridView();
            btnCerrar = new Button();
            colCodigo = new DataGridViewTextBoxColumn();
            colNombres = new DataGridViewTextBoxColumn();
            colApellidos = new DataGridViewTextBoxColumn();
            colGenero = new DataGridViewTextBoxColumn();
            colEdad = new DataGridViewTextBoxColumn();
            colFechaNac = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvExitosos).BeginInit();
            SuspendLayout();
            // 
            // lblResumen
            // 
            lblResumen.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblResumen.AutoSize = true;
            lblResumen.Location = new Point(328, 9);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(69, 20);
            lblResumen.TabIndex = 0;
            lblResumen.Text = "Resumen";
            lblResumen.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvExitosos
            // 
            dgvExitosos.AllowUserToAddRows = false;
            dgvExitosos.AllowUserToDeleteRows = false;
            dgvExitosos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvExitosos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvExitosos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvExitosos.Columns.AddRange(new DataGridViewColumn[] { colCodigo, colNombres, colApellidos, colGenero, colEdad, colFechaNac });
            dgvExitosos.Location = new Point(12, 32);
            dgvExitosos.Name = "dgvExitosos";
            dgvExitosos.ReadOnly = true;
            dgvExitosos.RowHeadersWidth = 51;
            dgvExitosos.Size = new Size(698, 414);
            dgvExitosos.TabIndex = 1;
            dgvExitosos.CellContentClick += dgvExitosos_CellContentClick;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnCerrar.Location = new Point(329, 452);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(68, 29);
            btnCerrar.TabIndex = 2;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            // 
            // colCodigo
            // 
            colCodigo.DataPropertyName = "codigo_beneficiario";
            colCodigo.HeaderText = "Codigo";
            colCodigo.MinimumWidth = 6;
            colCodigo.Name = "colCodigo";
            colCodigo.ReadOnly = true;
            // 
            // colNombres
            // 
            colNombres.DataPropertyName = "nombres";
            colNombres.HeaderText = "Nombres";
            colNombres.MinimumWidth = 6;
            colNombres.Name = "colNombres";
            colNombres.ReadOnly = true;
            // 
            // colApellidos
            // 
            colApellidos.DataPropertyName = "apellidos";
            colApellidos.HeaderText = "Apellidos";
            colApellidos.MinimumWidth = 6;
            colApellidos.Name = "colApellidos";
            colApellidos.ReadOnly = true;
            // 
            // colGenero
            // 
            colGenero.DataPropertyName = "genero";
            colGenero.HeaderText = "Genero";
            colGenero.MinimumWidth = 6;
            colGenero.Name = "colGenero";
            colGenero.ReadOnly = true;
            // 
            // colEdad
            // 
            colEdad.DataPropertyName = "edad";
            colEdad.HeaderText = "Edad";
            colEdad.MinimumWidth = 6;
            colEdad.Name = "colEdad";
            colEdad.ReadOnly = true;
            // 
            // colFechaNac
            // 
            colFechaNac.DataPropertyName = "fecha_nacimiento";
            colFechaNac.HeaderText = "Fecha de Nacimiento";
            colFechaNac.MinimumWidth = 6;
            colFechaNac.Name = "colFechaNac";
            colFechaNac.ReadOnly = true;
            // 
            // wResultadosImportacion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(721, 506);
            Controls.Add(btnCerrar);
            Controls.Add(dgvExitosos);
            Controls.Add(lblResumen);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "wResultadosImportacion";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Resultados de la Importación";
            ((System.ComponentModel.ISupportInitialize)dgvExitosos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblResumen;
        private DataGridView dgvExitosos;
        private Button btnCerrar;
        private DataGridViewTextBoxColumn colCodigo;
        private DataGridViewTextBoxColumn colNombres;
        private DataGridViewTextBoxColumn colApellidos;
        private DataGridViewTextBoxColumn colGenero;
        private DataGridViewTextBoxColumn colEdad;
        private DataGridViewTextBoxColumn colFechaNac;
    }
}