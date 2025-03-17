namespace ZasTrack.Forms.Muestras
{
    partial class wMuestras
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
            components = new System.ComponentModel.Container();
            pnlProyecto = new Panel();
            txtIdPaciente = new TextBox();
            btnBuscar = new Button();
            txtBuscar = new TextBox();
            lblExamenes = new Label();
            chkSangre = new CheckBox();
            chkOrina = new CheckBox();
            chkHeces = new CheckBox();
            txtFecha = new TextBox();
            cmbProyecto = new ComboBox();
            lblProyecto = new Label();
            btnGuardar = new Button();
            lblFecha = new Label();
            txtPaciente = new TextBox();
            lblRegMuestra = new Label();
            txtMuestrasId = new TextBox();
            label2 = new Label();
            lblMuestrasId = new Label();
            proyectoBindingSource = new BindingSource(components);
            pnlProyecto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)proyectoBindingSource).BeginInit();
            SuspendLayout();
            // 
            // pnlProyecto
            // 
            pnlProyecto.Controls.Add(txtIdPaciente);
            pnlProyecto.Controls.Add(btnBuscar);
            pnlProyecto.Controls.Add(txtBuscar);
            pnlProyecto.Controls.Add(lblExamenes);
            pnlProyecto.Controls.Add(chkSangre);
            pnlProyecto.Controls.Add(chkOrina);
            pnlProyecto.Controls.Add(chkHeces);
            pnlProyecto.Controls.Add(txtFecha);
            pnlProyecto.Controls.Add(cmbProyecto);
            pnlProyecto.Controls.Add(lblProyecto);
            pnlProyecto.Controls.Add(btnGuardar);
            pnlProyecto.Controls.Add(lblFecha);
            pnlProyecto.Controls.Add(txtPaciente);
            pnlProyecto.Controls.Add(lblRegMuestra);
            pnlProyecto.Controls.Add(txtMuestrasId);
            pnlProyecto.Controls.Add(label2);
            pnlProyecto.Controls.Add(lblMuestrasId);
            pnlProyecto.Dock = DockStyle.Fill;
            pnlProyecto.Location = new Point(0, 0);
            pnlProyecto.Name = "pnlProyecto";
            pnlProyecto.Size = new Size(1200, 700);
            pnlProyecto.TabIndex = 0;
            pnlProyecto.Paint += pnlProyecto_Paint;
            // 
            // txtIdPaciente
            // 
            txtIdPaciente.Location = new Point(304, 82);
            txtIdPaciente.Name = "txtIdPaciente";
            txtIdPaciente.Size = new Size(10, 23);
            txtIdPaciente.TabIndex = 22;
            txtIdPaciente.Visible = false;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(59, 229);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(243, 25);
            btnBuscar.TabIndex = 21;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(59, 200);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(243, 23);
            txtBuscar.TabIndex = 20;
            // 
            // lblExamenes
            // 
            lblExamenes.AutoSize = true;
            lblExamenes.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblExamenes.Location = new Point(59, 312);
            lblExamenes.Name = "lblExamenes";
            lblExamenes.Size = new Size(82, 21);
            lblExamenes.TabIndex = 19;
            lblExamenes.Text = "Examenes:";
            // 
            // chkSangre
            // 
            chkSangre.AutoSize = true;
            chkSangre.Font = new Font("Segoe UI", 13F);
            chkSangre.Location = new Point(251, 342);
            chkSangre.Name = "chkSangre";
            chkSangre.Size = new Size(63, 29);
            chkSangre.TabIndex = 18;
            chkSangre.Text = "BCC";
            chkSangre.UseVisualStyleBackColor = true;
            chkSangre.CheckedChanged += chkSangre_CheckedChanged_1;
            // 
            // chkOrina
            // 
            chkOrina.AutoSize = true;
            chkOrina.Font = new Font("Segoe UI", 13F);
            chkOrina.Location = new Point(160, 342);
            chkOrina.Name = "chkOrina";
            chkOrina.Size = new Size(66, 29);
            chkOrina.TabIndex = 17;
            chkOrina.Text = "EGO";
            chkOrina.UseVisualStyleBackColor = true;
            // 
            // chkHeces
            // 
            chkHeces.AutoSize = true;
            chkHeces.Font = new Font("Segoe UI", 13F);
            chkHeces.Location = new Point(59, 342);
            chkHeces.Name = "chkHeces";
            chkHeces.Size = new Size(65, 29);
            chkHeces.TabIndex = 16;
            chkHeces.Text = "EGH";
            chkHeces.UseVisualStyleBackColor = true;
            chkHeces.CheckedChanged += chkHeces_CheckedChanged;
            // 
            // txtFecha
            // 
            txtFecha.Location = new Point(328, 200);
            txtFecha.Name = "txtFecha";
            txtFecha.Size = new Size(243, 23);
            txtFecha.TabIndex = 15;
            txtFecha.TextChanged += txtFecha_TextChanged_1;
            // 
            // cmbProyecto
            // 
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(59, 140);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(189, 23);
            cmbProyecto.TabIndex = 13;
            cmbProyecto.SelectedIndexChanged += cmbProyecto_SelectedIndexChanged;
            // 
            // lblProyecto
            // 
            lblProyecto.AutoSize = true;
            lblProyecto.Font = new Font("Segoe UI", 12F);
            lblProyecto.Location = new Point(59, 116);
            lblProyecto.Name = "lblProyecto";
            lblProyecto.Size = new Size(74, 21);
            lblProyecto.TabIndex = 12;
            lblProyecto.Text = "Proyecto:";
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(53, 424);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(212, 41);
            btnGuardar.TabIndex = 10;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFecha.Location = new Point(328, 176);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(53, 21);
            lblFecha.TabIndex = 8;
            lblFecha.Text = "Fecha:";
            // 
            // txtPaciente
            // 
            txtPaciente.Location = new Point(59, 286);
            txtPaciente.Name = "txtPaciente";
            txtPaciente.Size = new Size(243, 23);
            txtPaciente.TabIndex = 7;
            txtPaciente.TextChanged += txtIdPaciente_TextChanged;
            // 
            // lblRegMuestra
            // 
            lblRegMuestra.AutoSize = true;
            lblRegMuestra.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblRegMuestra.Location = new Point(53, 75);
            lblRegMuestra.Name = "lblRegMuestra";
            lblRegMuestra.Size = new Size(249, 30);
            lblRegMuestra.TabIndex = 6;
            lblRegMuestra.Text = "Registrar una Muestra:";
            // 
            // txtMuestrasId
            // 
            txtMuestrasId.Location = new Point(328, 140);
            txtMuestrasId.Name = "txtMuestrasId";
            txtMuestrasId.Size = new Size(243, 23);
            txtMuestrasId.TabIndex = 5;
            txtMuestrasId.TextChanged += txtMuestrasId_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(59, 176);
            label2.Name = "label2";
            label2.Size = new Size(70, 21);
            label2.TabIndex = 1;
            label2.Text = "Paciente:";
            // 
            // lblMuestrasId
            // 
            lblMuestrasId.AutoSize = true;
            lblMuestrasId.Font = new Font("Segoe UI", 12F);
            lblMuestrasId.Location = new Point(328, 116);
            lblMuestrasId.Name = "lblMuestrasId";
            lblMuestrasId.Size = new Size(92, 21);
            lblMuestrasId.TabIndex = 0;
            lblMuestrasId.Text = "Muestra N°:";
            // 
            // proyectoBindingSource
            // 
            proyectoBindingSource.DataSource = typeof(Models.Proyecto);
            // 
            // wMuestras
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 700);
            Controls.Add(pnlProyecto);
            FormBorderStyle = FormBorderStyle.None;
            Name = "wMuestras";
            Text = "z";
            Load += wMuestras_Load;
            pnlProyecto.ResumeLayout(false);
            pnlProyecto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)proyectoBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlProyecto;
        private Label label2;
        private Label lblMuestrasId;
        private TextBox txtMuestrasId;
        private Button btnGuardar;
        private Label lblFecha;
        private TextBox txtPaciente;
        private Label lblRegMuestra;
        private Label lblProyecto;
        private TextBox txtFecha;
        private ComboBox cmbProyecto;
        private BindingSource proyectoBindingSource;
        private Label lblExamenes;
        private CheckBox chkSangre;
        private CheckBox chkOrina;
        private CheckBox chkHeces;
        private Button btnBuscar;
        private TextBox txtBuscar;
        private TextBox txtIdPaciente;
    }
}