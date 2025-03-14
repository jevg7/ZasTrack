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
            SuspendLayout();
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Location = new Point(190, 138);
            dtpFechaInicio.Margin = new Padding(4, 3, 4, 3);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(238, 23);
            dtpFechaInicio.TabIndex = 7;
            // 
            // txtNombreProyecto
            // 
            txtNombreProyecto.Location = new Point(190, 90);
            txtNombreProyecto.Margin = new Padding(4, 3, 4, 3);
            txtNombreProyecto.Name = "txtNombreProyecto";
            txtNombreProyecto.Size = new Size(238, 23);
            txtNombreProyecto.TabIndex = 9;
            // 
            // lblNombreProy
            // 
            lblNombreProy.AutoSize = true;
            lblNombreProy.Location = new Point(250, 72);
            lblNombreProy.Margin = new Padding(4, 0, 4, 0);
            lblNombreProy.Name = "lblNombreProy";
            lblNombreProy.Size = new Size(123, 15);
            lblNombreProy.TabIndex = 8;
            lblNombreProy.Text = "Nombre del Proyecto:";
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Location = new Point(72, 144);
            lblFechaInicio.Margin = new Padding(4, 0, 4, 0);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(89, 15);
            lblFechaInicio.TabIndex = 10;
            lblFechaInicio.Text = "Fecha de Inicio:";
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Location = new Point(52, 196);
            lblFechaFin.Margin = new Padding(4, 0, 4, 0);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(122, 15);
            lblFechaFin.TabIndex = 11;
            lblFechaFin.Text = "Fecha de Finalización:";
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Location = new Point(190, 190);
            dtpFechaFin.Margin = new Padding(4, 3, 4, 3);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(238, 23);
            dtpFechaFin.TabIndex = 12;
            // 
            // btnGuardarProyecto
            // 
            btnGuardarProyecto.Cursor = Cursors.Hand;
            btnGuardarProyecto.Location = new Point(226, 258);
            btnGuardarProyecto.Margin = new Padding(4, 3, 4, 3);
            btnGuardarProyecto.Name = "btnGuardarProyecto";
            btnGuardarProyecto.Size = new Size(147, 32);
            btnGuardarProyecto.TabIndex = 14;
            btnGuardarProyecto.Text = "Guardar";
            btnGuardarProyecto.UseVisualStyleBackColor = true;
            btnGuardarProyecto.Click += btnGuardarProyecto_Click;
            // 
            // wAñadirProyecto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(860, 557);
            ControlBox = false;
            Controls.Add(btnGuardarProyecto);
            Controls.Add(dtpFechaFin);
            Controls.Add(lblFechaFin);
            Controls.Add(lblFechaInicio);
            Controls.Add(txtNombreProyecto);
            Controls.Add(lblNombreProy);
            Controls.Add(dtpFechaInicio);
            FormBorderStyle = FormBorderStyle.None;
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
    }
}