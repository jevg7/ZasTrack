namespace ZasTrack.Forms.wProyectos
{
    partial class wAñadirProyecto
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
            dtpFechaInicio = new DateTimePicker();
            txtNombreProyecto = new TextBox();
            lblNombreProy = new Label();
            lblFechaInicio = new Label();
            btnGuardarProyecto = new Button();
            txtCodigo = new TextBox();
            lblCodigoProyecto = new Label();
            pnlAñadirProyecto = new Panel();
            pnlAñadirProyecto.SuspendLayout();
            SuspendLayout();
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Anchor = AnchorStyles.None;
            dtpFechaInicio.Location = new Point(294, 297);
            dtpFechaInicio.Margin = new Padding(5, 4, 5, 4);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(271, 27);
            dtpFechaInicio.TabIndex = 5;
            // 
            // txtNombreProyecto
            // 
            txtNombreProyecto.Anchor = AnchorStyles.None;
            txtNombreProyecto.Location = new Point(296, 172);
            txtNombreProyecto.Margin = new Padding(5, 4, 5, 4);
            txtNombreProyecto.Name = "txtNombreProyecto";
            txtNombreProyecto.Size = new Size(271, 27);
            txtNombreProyecto.TabIndex = 2;
            // 
            // lblNombreProy
            // 
            lblNombreProy.Anchor = AnchorStyles.None;
            lblNombreProy.AutoSize = true;
            lblNombreProy.Location = new Point(365, 148);
            lblNombreProy.Margin = new Padding(5, 0, 5, 0);
            lblNombreProy.Name = "lblNombreProy";
            lblNombreProy.Size = new Size(154, 20);
            lblNombreProy.TabIndex = 1;
            lblNombreProy.Text = "Nombre del Proyecto:";
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.Anchor = AnchorStyles.None;
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Location = new Point(174, 300);
            lblFechaInicio.Margin = new Padding(5, 0, 5, 0);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(111, 20);
            lblFechaInicio.TabIndex = 10;
            lblFechaInicio.Text = "Fecha de Inicio:";
            // 
            // btnGuardarProyecto
            // 
            btnGuardarProyecto.Anchor = AnchorStyles.None;
            btnGuardarProyecto.Cursor = Cursors.Hand;
            btnGuardarProyecto.Location = new Point(345, 348);
            btnGuardarProyecto.Margin = new Padding(5, 4, 5, 4);
            btnGuardarProyecto.Name = "btnGuardarProyecto";
            btnGuardarProyecto.Size = new Size(168, 43);
            btnGuardarProyecto.TabIndex = 6;
            btnGuardarProyecto.Text = "Guardar";
            btnGuardarProyecto.UseVisualStyleBackColor = true;
            btnGuardarProyecto.Click += btnGuardarProyecto_Click;
            // 
            // txtCodigo
            // 
            txtCodigo.Anchor = AnchorStyles.None;
            txtCodigo.ForeColor = Color.Black;
            txtCodigo.Location = new Point(296, 239);
            txtCodigo.Margin = new Padding(5, 4, 5, 4);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(271, 27);
            txtCodigo.TabIndex = 4;
            // 
            // lblCodigoProyecto
            // 
            lblCodigoProyecto.Anchor = AnchorStyles.None;
            lblCodigoProyecto.AutoSize = true;
            lblCodigoProyecto.Location = new Point(365, 215);
            lblCodigoProyecto.Margin = new Padding(5, 0, 5, 0);
            lblCodigoProyecto.Name = "lblCodigoProyecto";
            lblCodigoProyecto.Size = new Size(148, 20);
            lblCodigoProyecto.TabIndex = 3;
            lblCodigoProyecto.Text = "Codigo del Proyecto:";
            // 
            // pnlAñadirProyecto
            // 
            pnlAñadirProyecto.Controls.Add(txtNombreProyecto);
            pnlAñadirProyecto.Controls.Add(txtCodigo);
            pnlAñadirProyecto.Controls.Add(dtpFechaInicio);
            pnlAñadirProyecto.Controls.Add(lblCodigoProyecto);
            pnlAñadirProyecto.Controls.Add(lblNombreProy);
            pnlAñadirProyecto.Controls.Add(btnGuardarProyecto);
            pnlAñadirProyecto.Controls.Add(lblFechaInicio);
            pnlAñadirProyecto.Dock = DockStyle.Fill;
            pnlAñadirProyecto.Location = new Point(0, 0);
            pnlAñadirProyecto.Name = "pnlAñadirProyecto";
            pnlAñadirProyecto.Size = new Size(864, 584);
            pnlAñadirProyecto.TabIndex = 17;
            // 
            // wAñadirProyecto
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(864, 584);
            ControlBox = false;
            Controls.Add(pnlAñadirProyecto);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "wAñadirProyecto";
            Text = "wAñadirProyecto";
            Load += wAñadirProyecto_Load;
            pnlAñadirProyecto.ResumeLayout(false);
            pnlAñadirProyecto.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DateTimePicker dtpFechaInicio;
        private TextBox txtNombreProyecto;
        private Label lblNombreProy;
        private Label lblFechaInicio;
        private Button btnGuardarProyecto;
        private TextBox txtCodigo;
        private Label lblCodigoProyecto;
        private Panel pnlAñadirProyecto;
    }
}