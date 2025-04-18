namespace ZasTrack.Forms.Examenes.ExamWrite
{
    partial class EGHControl
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            lblExamenMicro = new Label();
            lblExamenFis = new Label();
            lblTitulo = new Label();
            txtParasito = new TextBox();
            lblParasito = new Label();
            txtConsistencia = new TextBox();
            lblConsistencia = new Label();
            txtColor = new TextBox();
            lblColor = new Label();
            SuspendLayout();
            // 
            // lblExamenMicro
            // 
            lblExamenMicro.AutoSize = true;
            lblExamenMicro.Font = new Font("Segoe UI", 20F);
            lblExamenMicro.Location = new Point(441, 61);
            lblExamenMicro.Name = "lblExamenMicro";
            lblExamenMicro.Size = new Size(274, 37);
            lblExamenMicro.TabIndex = 67;
            lblExamenMicro.Text = "Examen Microscopico";
            // 
            // lblExamenFis
            // 
            lblExamenFis.AutoSize = true;
            lblExamenFis.Font = new Font("Segoe UI", 20F);
            lblExamenFis.Location = new Point(34, 67);
            lblExamenFis.Name = "lblExamenFis";
            lblExamenFis.Size = new Size(182, 37);
            lblExamenFis.TabIndex = 66;
            lblExamenFis.Text = "Examen Fisico";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F);
            lblTitulo.Location = new Point(258, 19);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(397, 37);
            lblTitulo.TabIndex = 65;
            lblTitulo.Text = "Examen General de Heces (EGH)";
            // 
            // txtParasito
            // 
            txtParasito.Location = new Point(599, 110);
            txtParasito.Margin = new Padding(3, 2, 3, 2);
            txtParasito.Name = "txtParasito";
            txtParasito.Size = new Size(208, 23);
            txtParasito.TabIndex = 64;
            // 
            // lblParasito
            // 
            lblParasito.AutoSize = true;
            lblParasito.Font = new Font("Segoe UI", 12F);
            lblParasito.Location = new Point(441, 106);
            lblParasito.Name = "lblParasito";
            lblParasito.Size = new Size(65, 21);
            lblParasito.TabIndex = 63;
            lblParasito.Text = "Parasito";
            // 
            // txtConsistencia
            // 
            txtConsistencia.Location = new Point(192, 184);
            txtConsistencia.Margin = new Padding(3, 2, 3, 2);
            txtConsistencia.Name = "txtConsistencia";
            txtConsistencia.Size = new Size(219, 23);
            txtConsistencia.TabIndex = 62;
            // 
            // lblConsistencia
            // 
            lblConsistencia.AutoSize = true;
            lblConsistencia.Font = new Font("Segoe UI", 12F);
            lblConsistencia.Location = new Point(34, 181);
            lblConsistencia.Name = "lblConsistencia";
            lblConsistencia.Size = new Size(97, 21);
            lblConsistencia.TabIndex = 61;
            lblConsistencia.Text = "Consistencia";
            // 
            // txtColor
            // 
            txtColor.Location = new Point(192, 116);
            txtColor.Margin = new Padding(3, 2, 3, 2);
            txtColor.Name = "txtColor";
            txtColor.Size = new Size(219, 23);
            txtColor.TabIndex = 60;
            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Font = new Font("Segoe UI", 12F);
            lblColor.Location = new Point(34, 112);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(48, 21);
            lblColor.TabIndex = 59;
            lblColor.Text = "Color";
            // 
            // EGHControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblExamenMicro);
            Controls.Add(lblExamenFis);
            Controls.Add(lblTitulo);
            Controls.Add(txtParasito);
            Controls.Add(lblParasito);
            Controls.Add(txtConsistencia);
            Controls.Add(lblConsistencia);
            Controls.Add(txtColor);
            Controls.Add(lblColor);
            Name = "EGHControl";
            Size = new Size(863, 404);
            Load += EGHControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblExamenMicro;
        private Label lblExamenFis;
        private Label lblTitulo;
        private TextBox txtParasito;
        private Label lblParasito;
        private TextBox txtConsistencia;
        private Label lblConsistencia;
        private TextBox txtColor;
        private Label lblColor;
    }
}
