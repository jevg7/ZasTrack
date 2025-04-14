namespace ZasTrack.Forms
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
            lblFechaFin = new Label();
            dtpFechaFin = new DateTimePicker();
            btnGuardarProyecto = new Button();
            txtCodigo = new TextBox();
            lblCodigoProyecto = new Label();
            SuspendLayout();
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Location = new Point(215, 245);
            dtpFechaInicio.Margin = new Padding(5, 4, 5, 4);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(271, 27);
            dtpFechaInicio.TabIndex = 7;
            // 
            // txtNombreProyecto
            // 
            txtNombreProyecto.Location = new Point(217, 120);
            txtNombreProyecto.Margin = new Padding(5, 4, 5, 4);
            txtNombreProyecto.Name = "txtNombreProyecto";
            txtNombreProyecto.Size = new Size(271, 27);
            txtNombreProyecto.TabIndex = 9;
            // 
            // lblNombreProy
            // 
            lblNombreProy.AutoSize = true;
            lblNombreProy.Location = new Point(286, 96);
            lblNombreProy.Margin = new Padding(5, 0, 5, 0);
            lblNombreProy.Name = "lblNombreProy";
            lblNombreProy.Size = new Size(154, 20);
            lblNombreProy.TabIndex = 8;
            lblNombreProy.Text = "Nombre del Proyecto:";
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Location = new Point(80, 253);
            lblFechaInicio.Margin = new Padding(5, 0, 5, 0);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(111, 20);
            lblFechaInicio.TabIndex = 10;
            lblFechaInicio.Text = "Fecha de Inicio:";
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Location = new Point(57, 322);
            lblFechaFin.Margin = new Padding(5, 0, 5, 0);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(153, 20);
            lblFechaFin.TabIndex = 11;
            lblFechaFin.Text = "Fecha de Finalización:";
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Location = new Point(215, 314);
            dtpFechaFin.Margin = new Padding(5, 4, 5, 4);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(271, 27);
            dtpFechaFin.TabIndex = 12;
            // 
            // btnGuardarProyecto
            // 
            btnGuardarProyecto.Cursor = Cursors.Hand;
            btnGuardarProyecto.Location = new Point(256, 405);
            btnGuardarProyecto.Margin = new Padding(5, 4, 5, 4);
            btnGuardarProyecto.Name = "btnGuardarProyecto";
            btnGuardarProyecto.Size = new Size(168, 43);
            btnGuardarProyecto.TabIndex = 14;
            btnGuardarProyecto.Text = "Guardar";
            btnGuardarProyecto.UseVisualStyleBackColor = true;
            btnGuardarProyecto.Click += btnGuardarProyecto_Click;
            // 
            // txtCodigo
            // 
            txtCodigo.ForeColor = Color.WhiteSmoke;
            txtCodigo.Location = new Point(217, 186);
            txtCodigo.Margin = new Padding(5, 4, 5, 4);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(271, 27);
            txtCodigo.TabIndex = 16;
            txtCodigo.TextChanged += txtCodigo_TextChanged;
            // 
            // lblCodigoProyecto
            // 
            lblCodigoProyecto.AutoSize = true;
            lblCodigoProyecto.Location = new Point(286, 162);
            lblCodigoProyecto.Margin = new Padding(5, 0, 5, 0);
            lblCodigoProyecto.Name = "lblCodigoProyecto";
            lblCodigoProyecto.Size = new Size(148, 20);
            lblCodigoProyecto.TabIndex = 15;
            lblCodigoProyecto.Text = "Codigo del Proyecto:";
            lblCodigoProyecto.Click += lblCodigoProyecto_Click;
            // 
            // wAñadirProyecto
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(983, 743);
            ControlBox = false;
            Controls.Add(txtCodigo);
            Controls.Add(lblCodigoProyecto);
            Controls.Add(btnGuardarProyecto);
            Controls.Add(dtpFechaFin);
            Controls.Add(lblFechaFin);
            Controls.Add(lblFechaInicio);
            Controls.Add(txtNombreProyecto);
            Controls.Add(lblNombreProy);
            Controls.Add(dtpFechaInicio);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "wAñadirProyecto";
            Text = "wAñadirProyecto";
            Load += wAñadirProyecto_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dtpFechaInicio;
        private TextBox txtNombreProyecto;
        private Label lblNombreProy;
        private Label lblFechaInicio;
        private Label lblFechaFin;
        private DateTimePicker dtpFechaFin;
        private Button btnGuardarProyecto;
        private TextBox txtCodigo;
        private Label lblCodigoProyecto;
    }
}