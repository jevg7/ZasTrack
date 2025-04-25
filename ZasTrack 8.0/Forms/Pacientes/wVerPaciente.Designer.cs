namespace ZasTrack.Forms.Estudiantes
{
    partial class wVerPaciente
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
            pnlBuscar = new Panel();
            chkFiltroConExamenes = new CheckBox();
            chkFiltroConMuestras = new CheckBox();
            btnBuscar = new Button();
            cmbFiltroGenero = new ComboBox();
            txtBuscar = new TextBox();
            lblCodigo = new Label();
            lblProyecto = new Label();
            cmbProyectoVer = new ComboBox();
            lblGenero = new Label();
            dgvPacientes = new DataGridView();
            pacienteRepositoryBindingSource = new BindingSource(components);
            pnlBuscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPacientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pacienteRepositoryBindingSource).BeginInit();
            SuspendLayout();
            // 
            // pnlBuscar
            // 
            pnlBuscar.BackColor = SystemColors.ActiveCaptionText;
            pnlBuscar.Controls.Add(chkFiltroConExamenes);
            pnlBuscar.Controls.Add(chkFiltroConMuestras);
            pnlBuscar.Controls.Add(btnBuscar);
            pnlBuscar.Controls.Add(cmbFiltroGenero);
            pnlBuscar.Controls.Add(txtBuscar);
            pnlBuscar.Controls.Add(lblCodigo);
            pnlBuscar.Controls.Add(lblProyecto);
            pnlBuscar.Controls.Add(cmbProyectoVer);
            pnlBuscar.Controls.Add(lblGenero);
            pnlBuscar.Dock = DockStyle.Top;
            pnlBuscar.Location = new Point(0, 0);
            pnlBuscar.Margin = new Padding(3, 4, 3, 4);
            pnlBuscar.Name = "pnlBuscar";
            pnlBuscar.Size = new Size(1030, 145);
            pnlBuscar.TabIndex = 0;
            pnlBuscar.Paint += panel1_Paint;
            // 
            // chkFiltroConExamenes
            // 
            chkFiltroConExamenes.AutoSize = true;
            chkFiltroConExamenes.ForeColor = SystemColors.ButtonHighlight;
            chkFiltroConExamenes.Location = new Point(506, 61);
            chkFiltroConExamenes.Name = "chkFiltroConExamenes";
            chkFiltroConExamenes.Size = new Size(137, 24);
            chkFiltroConExamenes.TabIndex = 12;
            chkFiltroConExamenes.Text = "Tiene Examenes";
            chkFiltroConExamenes.UseVisualStyleBackColor = true;
            // 
            // chkFiltroConMuestras
            // 
            chkFiltroConMuestras.AutoSize = true;
            chkFiltroConMuestras.ForeColor = SystemColors.ButtonFace;
            chkFiltroConMuestras.Location = new Point(370, 61);
            chkFiltroConMuestras.Name = "chkFiltroConMuestras";
            chkFiltroConMuestras.Size = new Size(130, 24);
            chkFiltroConMuestras.TabIndex = 11;
            chkFiltroConMuestras.Text = "Tiene Muestras";
            chkFiltroConMuestras.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(583, 89);
            btnBuscar.Margin = new Padding(3, 4, 3, 4);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(86, 31);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // cmbFiltroGenero
            // 
            cmbFiltroGenero.FormattingEnabled = true;
            cmbFiltroGenero.Location = new Point(681, 89);
            cmbFiltroGenero.Name = "cmbFiltroGenero";
            cmbFiltroGenero.Size = new Size(146, 28);
            cmbFiltroGenero.TabIndex = 8;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(95, 90);
            txtBuscar.Margin = new Padding(3, 4, 3, 4);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(481, 27);
            txtBuscar.TabIndex = 1;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.ForeColor = SystemColors.ButtonFace;
            lblCodigo.Location = new Point(37, 93);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(52, 20);
            lblCodigo.TabIndex = 0;
            lblCodigo.Text = "Buscar";
            // 
            // lblProyecto
            // 
            lblProyecto.AutoSize = true;
            lblProyecto.BackColor = SystemColors.ActiveCaptionText;
            lblProyecto.ForeColor = SystemColors.ButtonFace;
            lblProyecto.Location = new Point(95, 8);
            lblProyecto.Name = "lblProyecto";
            lblProyecto.Size = new Size(67, 20);
            lblProyecto.TabIndex = 3;
            lblProyecto.Text = "Proyecto";
            // 
            // cmbProyectoVer
            // 
            cmbProyectoVer.FormattingEnabled = true;
            cmbProyectoVer.Location = new Point(95, 47);
            cmbProyectoVer.Name = "cmbProyectoVer";
            cmbProyectoVer.Size = new Size(208, 28);
            cmbProyectoVer.TabIndex = 7;
            // 
            // lblGenero
            // 
            lblGenero.AutoSize = true;
            lblGenero.BackColor = SystemColors.ActiveCaptionText;
            lblGenero.ForeColor = SystemColors.ButtonFace;
            lblGenero.Location = new Point(681, 50);
            lblGenero.Name = "lblGenero";
            lblGenero.Size = new Size(57, 20);
            lblGenero.TabIndex = 5;
            lblGenero.Text = "Género";
            // 
            // dgvPacientes
            // 
            dgvPacientes.AllowUserToAddRows = false;
            dgvPacientes.AllowUserToDeleteRows = false;
            dgvPacientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPacientes.Location = new Point(12, 145);
            dgvPacientes.Margin = new Padding(3, 4, 3, 4);
            dgvPacientes.Name = "dgvPacientes";
            dgvPacientes.ReadOnly = true;
            dgvPacientes.RowHeadersWidth = 51;
            dgvPacientes.Size = new Size(1006, 856);
            dgvPacientes.TabIndex = 1;
            // 
            // pacienteRepositoryBindingSource
            // 
            pacienteRepositoryBindingSource.DataSource = typeof(Repositories.PacienteRepository);
            // 
            // wVerPaciente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1030, 1001);
            Controls.Add(dgvPacientes);
            Controls.Add(pnlBuscar);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "wVerPaciente";
            Text = "wVerEstudiantes";
            pnlBuscar.ResumeLayout(false);
            pnlBuscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPacientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)pacienteRepositoryBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBuscar;
        private Button btnBuscar;
        private TextBox txtBuscar;
        private Label lblCodigo;
        private DataGridView dgvPacientes;
        private BindingSource pacienteRepositoryBindingSource;
        private Label lblProyecto;
        private Label lblGenero;
        private ComboBox cmbProyectoVer;
        private ComboBox cmbFiltroGenero;
        private CheckBox chkFiltroConExamenes;
        private CheckBox chkFiltroConMuestras;
    }
}