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
            pnlProyecto = new Panel();
            txtFecha = new TextBox();
            cmbProyecto = new ComboBox();
            lblProyecto = new Label();
            btnGuardar = new Button();
            cklExamenes = new CheckedListBox();
            lblFecha = new Label();
            txtIdPaciente = new TextBox();
            lblRegMuestra = new Label();
            txtMuestrasId = new TextBox();
            label2 = new Label();
            lblMuestrasId = new Label();
            pnlProyecto.SuspendLayout();
            SuspendLayout();
            // 
            // pnlProyecto
            // 
            pnlProyecto.Controls.Add(txtFecha);
            pnlProyecto.Controls.Add(cmbProyecto);
            pnlProyecto.Controls.Add(lblProyecto);
            pnlProyecto.Controls.Add(btnGuardar);
            pnlProyecto.Controls.Add(cklExamenes);
            pnlProyecto.Controls.Add(lblFecha);
            pnlProyecto.Controls.Add(txtIdPaciente);
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
            // txtFecha
            // 
            txtFecha.Location = new Point(328, 183);
            txtFecha.Name = "txtFecha";
            txtFecha.Size = new Size(243, 23);
            txtFecha.TabIndex = 15;
            // 
            // cmbProyecto
            // 
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(54, 140);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(189, 23);
            cmbProyecto.TabIndex = 13;
            // 
            // lblProyecto
            // 
            lblProyecto.AutoSize = true;
            lblProyecto.Font = new Font("Segoe UI", 12F);
            lblProyecto.Location = new Point(54, 116);
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
            // cklExamenes
            // 
            cklExamenes.Font = new Font("Segoe UI", 14F);
            cklExamenes.FormattingEnabled = true;
            cklExamenes.Items.AddRange(new object[] { "EGH", "EGO", "BCC" });
            cklExamenes.Location = new Point(59, 303);
            cklExamenes.Name = "cklExamenes";
            cklExamenes.Size = new Size(184, 85);
            cklExamenes.TabIndex = 9;
            cklExamenes.SelectedIndexChanged += cklExamenes_SelectedIndexChanged;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFecha.Location = new Point(249, 185);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(53, 21);
            lblFecha.TabIndex = 8;
            lblFecha.Text = "Fecha:";
            // 
            // txtIdPaciente
            // 
            txtIdPaciente.Location = new Point(59, 256);
            txtIdPaciente.Name = "txtIdPaciente";
            txtIdPaciente.Size = new Size(243, 23);
            txtIdPaciente.TabIndex = 7;
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
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(59, 232);
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
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlProyecto;
        private Label label2;
        private Label lblMuestrasId;
        private TextBox txtMuestrasId;
        private Button btnGuardar;
        private CheckedListBox cklExamenes;
        private Label lblFecha;
        private TextBox txtIdPaciente;
        private Label lblRegMuestra;
        private Label lblProyecto;
        private TextBox txtFecha;
        private ComboBox cmbProyecto;
    }
}