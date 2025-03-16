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
            btnBuscar = new Button();
            txtBuscar = new TextBox();
            lblCodigo = new Label();
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
            pnlBuscar.Controls.Add(btnBuscar);
            pnlBuscar.Controls.Add(txtBuscar);
            pnlBuscar.Controls.Add(lblCodigo);
            pnlBuscar.Dock = DockStyle.Top;
            pnlBuscar.Location = new Point(0, 0);
            pnlBuscar.Name = "pnlBuscar";
            pnlBuscar.Size = new Size(901, 100);
            pnlBuscar.TabIndex = 0;
            pnlBuscar.Paint += panel1_Paint;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(607, 35);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(180, 36);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(421, 23);
            txtBuscar.TabIndex = 1;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.ForeColor = SystemColors.ButtonFace;
            lblCodigo.Location = new Point(50, 39);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(111, 15);
            lblCodigo.TabIndex = 0;
            lblCodigo.Text = "Codigo Beneficiario";
            // 
            // dgvPacientes
            // 
            dgvPacientes.AllowUserToAddRows = false;
            dgvPacientes.AllowUserToDeleteRows = false;
            dgvPacientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPacientes.Dock = DockStyle.Fill;
            dgvPacientes.Location = new Point(0, 100);
            dgvPacientes.Name = "dgvPacientes";
            dgvPacientes.ReadOnly = true;
            dgvPacientes.Size = new Size(901, 651);
            dgvPacientes.TabIndex = 1;
            dgvPacientes.CellContentClick += dgvPacientes_CellContentClick;
            // 
            // pacienteRepositoryBindingSource
            // 
            pacienteRepositoryBindingSource.DataSource = typeof(Repositories.PacienteRepository);
            // 
            // wVerPaciente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(901, 751);
            Controls.Add(dgvPacientes);
            Controls.Add(pnlBuscar);
            FormBorderStyle = FormBorderStyle.None;
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
    }
}