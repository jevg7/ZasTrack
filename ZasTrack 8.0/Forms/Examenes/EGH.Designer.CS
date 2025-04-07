namespace ZasTrack.Forms.Examenes
{
    partial class EGH
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
            btnGuardar = new Button();
            btnRegresar = new Button();
            txtMonocitos = new TextBox();
            lblParasito = new Label();
            txtLinfocitos = new TextBox();
            lblConsistencia = new Label();
            txtColor = new TextBox();
            lblColor = new Label();
            lblTitulo = new Label();
            lblExamenFis = new Label();
            lblExamenMicro = new Label();
            SuspendLayout();
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(920, 588);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(292, 61);
            btnGuardar.TabIndex = 53;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnRegresar
            // 
            btnRegresar.Location = new Point(598, 588);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(292, 61);
            btnRegresar.TabIndex = 52;
            btnRegresar.Text = "Regresar";
            btnRegresar.UseVisualStyleBackColor = true;
            // 
            // txtMonocitos
            // 
            txtMonocitos.Location = new Point(821, 138);
            txtMonocitos.Name = "txtMonocitos";
            txtMonocitos.Size = new Size(314, 27);
            txtMonocitos.TabIndex = 47;
            // 
            // lblParasito
            // 
            lblParasito.AutoSize = true;
            lblParasito.Font = new Font("Segoe UI", 12F);
            lblParasito.Location = new Point(640, 134);
            lblParasito.Name = "lblParasito";
            lblParasito.Size = new Size(81, 28);
            lblParasito.TabIndex = 46;
            lblParasito.Text = "Parasito";
            // 
            // txtLinfocitos
            // 
            txtLinfocitos.Location = new Point(213, 229);
            txtLinfocitos.Name = "txtLinfocitos";
            txtLinfocitos.Size = new Size(314, 27);
            txtLinfocitos.TabIndex = 45;
            // 
            // lblConsistencia
            // 
            lblConsistencia.AutoSize = true;
            lblConsistencia.Font = new Font("Segoe UI", 12F);
            lblConsistencia.Location = new Point(32, 225);
            lblConsistencia.Name = "lblConsistencia";
            lblConsistencia.Size = new Size(120, 28);
            lblConsistencia.TabIndex = 44;
            lblConsistencia.Text = "Consistencia";
            // 
            // txtColor
            // 
            txtColor.Location = new Point(213, 138);
            txtColor.Name = "txtColor";
            txtColor.Size = new Size(314, 27);
            txtColor.TabIndex = 43;
            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Font = new Font("Segoe UI", 12F);
            lblColor.Location = new Point(32, 134);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(60, 28);
            lblColor.TabIndex = 42;
            lblColor.Text = "Color";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F);
            lblTitulo.Location = new Point(288, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(499, 46);
            lblTitulo.TabIndex = 56;
            lblTitulo.Text = "Examen General de Heces (EGH)";
            // 
            // lblExamenFis
            // 
            lblExamenFis.AutoSize = true;
            lblExamenFis.Font = new Font("Segoe UI", 20F);
            lblExamenFis.Location = new Point(32, 73);
            lblExamenFis.Name = "lblExamenFis";
            lblExamenFis.Size = new Size(228, 46);
            lblExamenFis.TabIndex = 57;
            lblExamenFis.Text = "Examen Fisico";
            // 
            // lblExamenMicro
            // 
            lblExamenMicro.AutoSize = true;
            lblExamenMicro.Font = new Font("Segoe UI", 20F);
            lblExamenMicro.Location = new Point(640, 73);
            lblExamenMicro.Name = "lblExamenMicro";
            lblExamenMicro.Size = new Size(346, 46);
            lblExamenMicro.TabIndex = 58;
            lblExamenMicro.Text = "Examen Microscopico";
            // 
            // EGH
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1257, 732);
            Controls.Add(lblExamenMicro);
            Controls.Add(lblExamenFis);
            Controls.Add(lblTitulo);
            Controls.Add(btnGuardar);
            Controls.Add(btnRegresar);
            Controls.Add(txtMonocitos);
            Controls.Add(lblParasito);
            Controls.Add(txtLinfocitos);
            Controls.Add(lblConsistencia);
            Controls.Add(txtColor);
            Controls.Add(lblColor);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EGH";
            Text = "EGH";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnGuardar;
        private Button btnRegresar;
        private TextBox txtMonocitos;
        private Label lblParasito;
        private TextBox txtLinfocitos;
        private Label lblConsistencia;
        private TextBox txtColor;
        private Label lblColor;
        private Label lblTitulo;
        private Label lblExamenFis;
        private Label lblExamenMicro;
    }
}