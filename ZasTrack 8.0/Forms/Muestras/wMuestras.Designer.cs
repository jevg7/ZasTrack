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
            pnlProyecto.Margin = new Padding(3, 4, 3, 4);
            pnlProyecto.Name = "pnlProyecto";
            pnlProyecto.Size = new Size(1371, 933);
            pnlProyecto.TabIndex = 0;
            pnlProyecto.Paint += pnlProyecto_Paint;
            // 
            // txtIdPaciente
            // 
            txtIdPaciente.Location = new Point(67, 69);
            txtIdPaciente.Margin = new Padding(3, 4, 3, 4);
            txtIdPaciente.Name = "txtIdPaciente";
            txtIdPaciente.Size = new Size(11, 27);
            txtIdPaciente.TabIndex = 22;
            txtIdPaciente.Visible = false;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(67, 305);
            btnBuscar.Margin = new Padding(3, 4, 3, 4);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(278, 33);
            btnBuscar.TabIndex = 21;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(67, 267);
            txtBuscar.Margin = new Padding(3, 4, 3, 4);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(277, 27);
            txtBuscar.TabIndex = 20;
            // 
            // lblExamenes
            // 
            lblExamenes.AutoSize = true;
            lblExamenes.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblExamenes.Location = new Point(67, 416);
            lblExamenes.Name = "lblExamenes";
            lblExamenes.Size = new Size(101, 28);
            lblExamenes.TabIndex = 19;
            lblExamenes.Text = "Examenes:";
            // 
            // chkSangre
            // 
            chkSangre.AutoSize = true;
            chkSangre.Font = new Font("Segoe UI", 13F);
            chkSangre.Location = new Point(287, 456);
            chkSangre.Margin = new Padding(3, 4, 3, 4);
            chkSangre.Name = "chkSangre";
            chkSangre.Size = new Size(78, 34);
            chkSangre.TabIndex = 18;
            chkSangre.Text = "BHC";
            chkSangre.UseVisualStyleBackColor = true;
            chkSangre.CheckedChanged += chkSangre_CheckedChanged_1;
            // 
            // chkOrina
            // 
            chkOrina.AutoSize = true;
            chkOrina.Font = new Font("Segoe UI", 13F);
            chkOrina.Location = new Point(183, 456);
            chkOrina.Margin = new Padding(3, 4, 3, 4);
            chkOrina.Name = "chkOrina";
            chkOrina.Size = new Size(78, 34);
            chkOrina.TabIndex = 17;
            chkOrina.Text = "EGO";
            chkOrina.UseVisualStyleBackColor = true;
            // 
            // chkHeces
            // 
            chkHeces.AutoSize = true;
            chkHeces.Font = new Font("Segoe UI", 13F);
            chkHeces.Location = new Point(67, 456);
            chkHeces.Margin = new Padding(3, 4, 3, 4);
            chkHeces.Name = "chkHeces";
            chkHeces.Size = new Size(77, 34);
            chkHeces.TabIndex = 16;
            chkHeces.Text = "EGH";
            chkHeces.UseVisualStyleBackColor = true;
            chkHeces.CheckedChanged += chkHeces_CheckedChanged;
            // 
            // txtFecha
            // 
            txtFecha.Location = new Point(375, 267);
            txtFecha.Margin = new Padding(3, 4, 3, 4);
            txtFecha.Name = "txtFecha";
            txtFecha.Size = new Size(277, 27);
            txtFecha.TabIndex = 15;
            txtFecha.TextChanged += txtFecha_TextChanged_1;
            // 
            // cmbProyecto
            // 
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(67, 186);
            cmbProyecto.Margin = new Padding(3, 4, 3, 4);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(215, 28);
            cmbProyecto.TabIndex = 13;
            cmbProyecto.SelectedIndexChanged += cmbProyecto_SelectedIndexChanged;
            // 
            // lblProyecto
            // 
            lblProyecto.AutoSize = true;
            lblProyecto.Font = new Font("Segoe UI", 12F);
            lblProyecto.Location = new Point(67, 155);
            lblProyecto.Name = "lblProyecto";
            lblProyecto.Size = new Size(94, 28);
            lblProyecto.TabIndex = 12;
            lblProyecto.Text = "Proyecto:";
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(61, 565);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(242, 55);
            btnGuardar.TabIndex = 10;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFecha.Location = new Point(375, 235);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(66, 28);
            lblFecha.TabIndex = 8;
            lblFecha.Text = "Fecha:";
            // 
            // txtPaciente
            // 
            txtPaciente.Location = new Point(67, 381);
            txtPaciente.Margin = new Padding(3, 4, 3, 4);
            txtPaciente.Name = "txtPaciente";
            txtPaciente.Size = new Size(277, 27);
            txtPaciente.TabIndex = 7;
            txtPaciente.TextChanged += txtIdPaciente_TextChanged;
            // 
            // lblRegMuestra
            // 
            lblRegMuestra.AutoSize = true;
            lblRegMuestra.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblRegMuestra.Location = new Point(61, 100);
            lblRegMuestra.Name = "lblRegMuestra";
            lblRegMuestra.Size = new Size(308, 37);
            lblRegMuestra.TabIndex = 6;
            lblRegMuestra.Text = "Registrar una Muestra:";
            // 
            // txtMuestrasId
            // 
            txtMuestrasId.Location = new Point(375, 187);
            txtMuestrasId.Margin = new Padding(3, 4, 3, 4);
            txtMuestrasId.Name = "txtMuestrasId";
            txtMuestrasId.Size = new Size(277, 27);
            txtMuestrasId.TabIndex = 5;
            txtMuestrasId.TextChanged += txtMuestrasId_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(67, 235);
            label2.Name = "label2";
            label2.Size = new Size(88, 28);
            label2.TabIndex = 1;
            label2.Text = "Paciente:";
            // 
            // lblMuestrasId
            // 
            lblMuestrasId.AutoSize = true;
            lblMuestrasId.Font = new Font("Segoe UI", 12F);
            lblMuestrasId.Location = new Point(375, 155);
            lblMuestrasId.Name = "lblMuestrasId";
            lblMuestrasId.Size = new Size(115, 28);
            lblMuestrasId.TabIndex = 0;
            lblMuestrasId.Text = "Muestra N°:";
            // 
            // proyectoBindingSource
            // 
            proyectoBindingSource.DataSource = typeof(Models.Proyecto);
            // 
            // wMuestras
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1371, 933);
            Controls.Add(pnlProyecto);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
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