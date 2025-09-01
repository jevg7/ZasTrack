namespace ZasTrack.Forms.Informes
{
    partial class wInformes
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
            cmbProyectoInforme = new ComboBox();
            dgvListaInformes = new DataGridView();
            btnExportarTodoPdf = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvListaInformes).BeginInit();
            SuspendLayout();
            // 
            // cmbProyectoInforme
            // 
            cmbProyectoInforme.FormattingEnabled = true;
            cmbProyectoInforme.Location = new Point(72, 53);
            cmbProyectoInforme.Name = "cmbProyectoInforme";
            cmbProyectoInforme.Size = new Size(239, 28);
            cmbProyectoInforme.TabIndex = 0;
            // 
            // dgvListaInformes
            // 
            dgvListaInformes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListaInformes.Location = new Point(11, 87);
            dgvListaInformes.Name = "dgvListaInformes";
            dgvListaInformes.RowHeadersWidth = 51;
            dgvListaInformes.Size = new Size(1053, 615);
            dgvListaInformes.TabIndex = 1;
            // 
            // btnExportarTodoPdf
            // 
            btnExportarTodoPdf.Location = new Point(726, 53);
            btnExportarTodoPdf.Name = "btnExportarTodoPdf";
            btnExportarTodoPdf.Size = new Size(338, 28);
            btnExportarTodoPdf.TabIndex = 2;
            btnExportarTodoPdf.Text = "Exportar todo a PDF";
            btnExportarTodoPdf.UseVisualStyleBackColor = true;
            btnExportarTodoPdf.Click += btnExportarTodoPdf_Click_1;
            // 
            // wInformes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1076, 714);
            Controls.Add(btnExportarTodoPdf);
            Controls.Add(dgvListaInformes);
            Controls.Add(cmbProyectoInforme);
            FormBorderStyle = FormBorderStyle.None;
            Name = "wInformes";
            Text = "Generar Informes";
            ((System.ComponentModel.ISupportInitialize)dgvListaInformes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbProyectoInforme;
        private DataGridView dgvListaInformes;
        private Button btnExportarTodoPdf;
    }
}