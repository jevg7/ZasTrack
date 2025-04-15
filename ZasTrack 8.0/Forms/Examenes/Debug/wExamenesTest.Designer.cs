namespace ZasTrack.Forms.Examenes
{
    partial class wExamenesTest
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
            pnlContenedor = new Panel();
            panel2 = new Panel();
            pnlContProc = new Panel();
            panel1 = new Panel();
            label2 = new Label();
            cmbProyecto = new ComboBox();
            btnNoProcesados = new Button();
            btnProcesados = new Button();
            pnlPacientes = new Panel();
            pnlSearchFiltro = new Panel();
            txtBuscar = new TextBox();
            cmbFiltros = new ComboBox();
            pnlContenedor.SuspendLayout();
            pnlContProc.SuspendLayout();
            panel1.SuspendLayout();
            pnlSearchFiltro.SuspendLayout();
            SuspendLayout();
            // 
            // pnlContenedor
            // 
            pnlContenedor.Controls.Add(panel2);
            pnlContenedor.Controls.Add(pnlContProc);
            pnlContenedor.Controls.Add(pnlPacientes);
            pnlContenedor.Controls.Add(pnlSearchFiltro);
            pnlContenedor.Dock = DockStyle.Fill;
            pnlContenedor.Location = new Point(0, 0);
            pnlContenedor.Margin = new Padding(3, 2, 3, 2);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1184, 661);
            pnlContenedor.TabIndex = 1;
            pnlContenedor.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.Location = new Point(3, 92);
            panel2.Name = "panel2";
            panel2.Size = new Size(1181, 31);
            panel2.TabIndex = 3;
            // 
            // pnlContProc
            // 
            pnlContProc.Controls.Add(panel1);
            pnlContProc.Controls.Add(btnNoProcesados);
            pnlContProc.Controls.Add(btnProcesados);
            pnlContProc.Dock = DockStyle.Top;
            pnlContProc.Location = new Point(0, 0);
            pnlContProc.Name = "pnlContProc";
            pnlContProc.Size = new Size(1184, 42);
            pnlContProc.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Controls.Add(label2);
            panel1.Controls.Add(cmbProyecto);
            panel1.Location = new Point(566, 7);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(246, 34);
            panel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(10, 7);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 39;
            label2.Text = "Proyecto";
            // 
            // cmbProyecto
            // 
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(87, 3);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(121, 23);
            cmbProyecto.TabIndex = 38;
            cmbProyecto.SelectedIndexChanged += cmbProyecto_SelectedIndexChanged;
            // 
            // btnNoProcesados
            // 
            btnNoProcesados.Location = new Point(22, 7);
            btnNoProcesados.Name = "btnNoProcesados";
            btnNoProcesados.Size = new Size(239, 30);
            btnNoProcesados.TabIndex = 2;
            btnNoProcesados.Text = "Recepcionados";
            btnNoProcesados.UseVisualStyleBackColor = true;
            // 
            // btnProcesados
            // 
            btnProcesados.Location = new Point(282, 7);
            btnProcesados.Name = "btnProcesados";
            btnProcesados.Size = new Size(241, 30);
            btnProcesados.TabIndex = 1;
            btnProcesados.Text = "Procesados";
            btnProcesados.UseVisualStyleBackColor = true;
            // 
            // pnlPacientes
            // 
            pnlPacientes.Location = new Point(0, 129);
            pnlPacientes.Name = "pnlPacientes";
            pnlPacientes.Size = new Size(1184, 532);
            pnlPacientes.TabIndex = 1;
            pnlPacientes.Paint += pnlPacientes_Paint;
            // 
            // pnlSearchFiltro
            // 
            pnlSearchFiltro.Controls.Add(txtBuscar);
            pnlSearchFiltro.Controls.Add(cmbFiltros);
            pnlSearchFiltro.Location = new Point(0, 43);
            pnlSearchFiltro.Name = "pnlSearchFiltro";
            pnlSearchFiltro.Size = new Size(1184, 43);
            pnlSearchFiltro.TabIndex = 0;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(284, 9);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(564, 23);
            txtBuscar.TabIndex = 1;
            txtBuscar.Text = "Buscar por Codigo o por Nombre";
            // 
            // cmbFiltros
            // 
            cmbFiltros.FormattingEnabled = true;
            cmbFiltros.Location = new Point(22, 9);
            cmbFiltros.Name = "cmbFiltros";
            cmbFiltros.Size = new Size(256, 23);
            cmbFiltros.TabIndex = 0;
            // 
            // wExamenesTest
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 661);
            Controls.Add(pnlContenedor);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "wExamenesTest";
            Text = "wExamenesTest";
            pnlContenedor.ResumeLayout(false);
            pnlContProc.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlSearchFiltro.ResumeLayout(false);
            pnlSearchFiltro.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlContenedor;
        private Panel pnlContProc;
        private Panel panel1;
        private Label label2;
        private ComboBox cmbProyecto;
        private Button btnNoProcesados;
        private Button btnProcesados;
        private Panel pnlPacientes;
        private Panel pnlSearchFiltro;
        private TextBox txtBuscar;
        private ComboBox cmbFiltros;
        private Panel panel2;
    }
}