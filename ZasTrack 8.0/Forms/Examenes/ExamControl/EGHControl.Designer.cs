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
            lblParasito = new Label();
            txtConsistencia = new TextBox();
            lblConsistencia = new Label();
            txtColor = new TextBox();
            lblColor = new Label();
            txtParasito = new ComboBox();
            SuspendLayout();
            // 
            // lblExamenMicro
            // 
            lblExamenMicro.AutoSize = true;
            lblExamenMicro.Font = new Font("Segoe UI", 20F);
            lblExamenMicro.Location = new Point(554, 89);
            lblExamenMicro.Name = "lblExamenMicro";
            lblExamenMicro.Size = new Size(346, 46);
            lblExamenMicro.TabIndex = 67;
            lblExamenMicro.Text = "Examen Microscopico";
            // 
            // lblExamenFis
            // 
            lblExamenFis.AutoSize = true;
            lblExamenFis.Font = new Font("Segoe UI", 20F);
            lblExamenFis.Location = new Point(39, 89);
            lblExamenFis.Name = "lblExamenFis";
            lblExamenFis.Size = new Size(228, 46);
            lblExamenFis.TabIndex = 66;
            lblExamenFis.Text = "Examen Fisico";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F);
            lblTitulo.Location = new Point(219, 24);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(499, 46);
            lblTitulo.TabIndex = 65;
            lblTitulo.Text = "Examen General de Heces (EGH)";
            // 
            // lblParasito
            // 
            lblParasito.AutoSize = true;
            lblParasito.Font = new Font("Segoe UI", 12F);
            lblParasito.Location = new Point(554, 143);
            lblParasito.Name = "lblParasito";
            lblParasito.Size = new Size(81, 28);
            lblParasito.TabIndex = 63;
            lblParasito.Text = "Parasito";
            // 
            // txtConsistencia
            // 
            txtConsistencia.Location = new Point(219, 245);
            txtConsistencia.Name = "txtConsistencia";
            txtConsistencia.Size = new Size(250, 27);
            txtConsistencia.TabIndex = 62;
            // 
            // lblConsistencia
            // 
            lblConsistencia.AutoSize = true;
            lblConsistencia.Font = new Font("Segoe UI", 12F);
            lblConsistencia.Location = new Point(39, 241);
            lblConsistencia.Name = "lblConsistencia";
            lblConsistencia.Size = new Size(120, 28);
            lblConsistencia.TabIndex = 61;
            lblConsistencia.Text = "Consistencia";
            // 
            // txtColor
            // 
            txtColor.Location = new Point(219, 147);
            txtColor.Name = "txtColor";
            txtColor.Size = new Size(250, 27);
            txtColor.TabIndex = 60;
            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Font = new Font("Segoe UI", 12F);
            lblColor.Location = new Point(39, 149);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(60, 28);
            lblColor.TabIndex = 59;
            lblColor.Text = "Color";
            // 
            // txtParasito
            // 
            txtParasito.FormattingEnabled = true;
            txtParasito.Items.AddRange(new object[] { "Quiste de Entamoeba coli", "Quiste de Entamoeba histolytica", "Quiste de Blastocystis hominis", "Quiste de Gladia intestinal", "Quiste de Endolimax nana", "Quiste de lodamoeba butschlii" });
            txtParasito.Location = new Point(659, 147);
            txtParasito.Name = "txtParasito";
            txtParasito.Size = new Size(284, 28);
            txtParasito.TabIndex = 68;
            // 
            // EGHControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(txtParasito);
            Controls.Add(lblExamenMicro);
            Controls.Add(lblExamenFis);
            Controls.Add(lblTitulo);
            Controls.Add(lblParasito);
            Controls.Add(txtConsistencia);
            Controls.Add(lblConsistencia);
            Controls.Add(txtColor);
            Controls.Add(lblColor);
            Margin = new Padding(3, 4, 3, 4);
            Name = "EGHControl";
            Size = new Size(986, 539);
            Load += EGHControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblExamenMicro;
        private Label lblExamenFis;
        private Label lblTitulo;
        private Label lblParasito;
        private TextBox txtConsistencia;
        private Label lblConsistencia;
        private TextBox txtColor;
        private Label lblColor;
        private ComboBox txtParasito;
    }
}
