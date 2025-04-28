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
            btnGuardar.Location = new Point(805, 441);
            btnGuardar.Margin = new Padding(3, 2, 3, 2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(256, 46);
            btnGuardar.TabIndex = 53;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnRegresar
            // 
            btnRegresar.Location = new Point(523, 441);
            btnRegresar.Margin = new Padding(3, 2, 3, 2);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(256, 46);
            btnRegresar.TabIndex = 52;
            btnRegresar.Text = "Regresar";
            btnRegresar.UseVisualStyleBackColor = true;
            // 
            // txtMonocitos
            // 
            txtMonocitos.Location = new Point(718, 104);
            txtMonocitos.Margin = new Padding(3, 2, 3, 2);
            txtMonocitos.Name = "txtMonocitos";
            txtMonocitos.Size = new Size(275, 23);
            txtMonocitos.TabIndex = 47;
            // 
            // lblParasito
            // 
            lblParasito.AutoSize = true;
            lblParasito.Font = new Font("Segoe UI", 12F);
            lblParasito.Location = new Point(560, 100);
            lblParasito.Name = "lblParasito";
            lblParasito.Size = new Size(65, 21);
            lblParasito.TabIndex = 46;
            lblParasito.Text = "Parasito";
            // 
            // txtLinfocitos
            // 
            txtLinfocitos.Location = new Point(186, 172);
            txtLinfocitos.Margin = new Padding(3, 2, 3, 2);
            txtLinfocitos.Name = "txtLinfocitos";
            txtLinfocitos.Size = new Size(275, 23);
            txtLinfocitos.TabIndex = 45;
            // 
            // lblConsistencia
            // 
            lblConsistencia.AutoSize = true;
            lblConsistencia.Font = new Font("Segoe UI", 12F);
            lblConsistencia.Location = new Point(28, 169);
            lblConsistencia.Name = "lblConsistencia";
            lblConsistencia.Size = new Size(97, 21);
            lblConsistencia.TabIndex = 44;
            lblConsistencia.Text = "Consistencia";
            // 
            // txtColor
            // 
            txtColor.Location = new Point(186, 104);
            txtColor.Margin = new Padding(3, 2, 3, 2);
            txtColor.Name = "txtColor";
            txtColor.Size = new Size(275, 23);
            txtColor.TabIndex = 43;
            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Font = new Font("Segoe UI", 12F);
            lblColor.Location = new Point(28, 100);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(48, 21);
            lblColor.TabIndex = 42;
            lblColor.Text = "Color";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F);
            lblTitulo.Location = new Point(252, 7);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(397, 37);
            lblTitulo.TabIndex = 56;
            lblTitulo.Text = "Examen General de Heces (EGH)";
            // 
            // lblExamenFis
            // 
            lblExamenFis.AutoSize = true;
            lblExamenFis.Font = new Font("Segoe UI", 20F);
            lblExamenFis.Location = new Point(28, 55);
            lblExamenFis.Name = "lblExamenFis";
            lblExamenFis.Size = new Size(182, 37);
            lblExamenFis.TabIndex = 57;
            lblExamenFis.Text = "Examen Fisico";
            // 
            // lblExamenMicro
            // 
            lblExamenMicro.AutoSize = true;
            lblExamenMicro.Font = new Font("Segoe UI", 20F);
            lblExamenMicro.Location = new Point(560, 55);
            lblExamenMicro.Name = "lblExamenMicro";
            lblExamenMicro.Size = new Size(274, 37);
            lblExamenMicro.TabIndex = 58;
            lblExamenMicro.Text = "Examen Microscopico";
            // 
            // EGH
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 549);
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
            Margin = new Padding(3, 2, 3, 2);
            Name = "EGH";
            Text = "EGH";
            Load += EGH_Load;
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