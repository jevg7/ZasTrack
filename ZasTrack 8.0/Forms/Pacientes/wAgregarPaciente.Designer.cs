namespace ZasTrack
{
    partial class wAgregarPaciente
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
            lblCodigoBen = new Label();
            txtCodigoBen = new TextBox();
            lblNombres = new Label();
            txtNombres = new TextBox();
            lblGenero = new Label();
            lblFechaNac = new Label();
            dtpFechaNac = new DateTimePicker();
            lblEdad = new Label();
            lblObservacion = new Label();
            txtObservacion = new RichTextBox();
            txtEdad = new TextBox();
            cmbGenero = new ComboBox();
            btnGuardarPaciente = new Button();
            txtApellidos = new TextBox();
            lblApellido = new Label();
            panel1 = new Panel();
            label1 = new Label();
            cmbProyecto = new ComboBox();
            SuspendLayout();
            // 
            // lblCodigoBen
            // 
            lblCodigoBen.AutoSize = true;
            lblCodigoBen.Location = new Point(430, 161);
            lblCodigoBen.Margin = new Padding(5, 0, 5, 0);
            lblCodigoBen.Name = "lblCodigoBen";
            lblCodigoBen.Size = new Size(165, 20);
            lblCodigoBen.TabIndex = 0;
            lblCodigoBen.Text = "Codigo de Beneficiario:";
            // 
            // txtCodigoBen
            // 
            txtCodigoBen.Location = new Point(612, 161);
            txtCodigoBen.Margin = new Padding(5, 4, 5, 4);
            txtCodigoBen.Name = "txtCodigoBen";
            txtCodigoBen.Size = new Size(221, 27);
            txtCodigoBen.TabIndex = 1;
            txtCodigoBen.TextChanged += txtAcodigo_TextChanged;
            // 
            // lblNombres
            // 
            lblNombres.AutoSize = true;
            lblNombres.Location = new Point(161, 217);
            lblNombres.Margin = new Padding(5, 0, 5, 0);
            lblNombres.Name = "lblNombres";
            lblNombres.Size = new Size(73, 20);
            lblNombres.TabIndex = 2;
            lblNombres.Text = "Nombres:";
            // 
            // txtNombres
            // 
            txtNombres.Location = new Point(245, 213);
            txtNombres.Margin = new Padding(5, 4, 5, 4);
            txtNombres.Name = "txtNombres";
            txtNombres.Size = new Size(588, 27);
            txtNombres.TabIndex = 3;
            txtNombres.TextChanged += txtAnombreApellido_TextChanged;
            // 
            // lblGenero
            // 
            lblGenero.AutoSize = true;
            lblGenero.Location = new Point(174, 324);
            lblGenero.Margin = new Padding(5, 0, 5, 0);
            lblGenero.Name = "lblGenero";
            lblGenero.Size = new Size(60, 20);
            lblGenero.TabIndex = 4;
            lblGenero.Text = "Genero:";
            // 
            // lblFechaNac
            // 
            lblFechaNac.AutoSize = true;
            lblFechaNac.Location = new Point(84, 371);
            lblFechaNac.Margin = new Padding(5, 0, 5, 0);
            lblFechaNac.Name = "lblFechaNac";
            lblFechaNac.Size = new Size(152, 20);
            lblFechaNac.TabIndex = 5;
            lblFechaNac.Text = "Fecha de Nacimiento:";
            // 
            // dtpFechaNac
            // 
            dtpFechaNac.Location = new Point(245, 366);
            dtpFechaNac.Margin = new Padding(5, 4, 5, 4);
            dtpFechaNac.Name = "dtpFechaNac";
            dtpFechaNac.Size = new Size(317, 27);
            dtpFechaNac.TabIndex = 6;
            dtpFechaNac.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // lblEdad
            // 
            lblEdad.AutoSize = true;
            lblEdad.Location = new Point(592, 368);
            lblEdad.Margin = new Padding(5, 0, 5, 0);
            lblEdad.Name = "lblEdad";
            lblEdad.Size = new Size(46, 20);
            lblEdad.TabIndex = 7;
            lblEdad.Text = "Edad:";
            // 
            // lblObservacion
            // 
            lblObservacion.AutoSize = true;
            lblObservacion.Location = new Point(423, 480);
            lblObservacion.Margin = new Padding(5, 0, 5, 0);
            lblObservacion.Name = "lblObservacion";
            lblObservacion.Size = new Size(91, 20);
            lblObservacion.TabIndex = 8;
            lblObservacion.Text = "Observacion";
            // 
            // txtObservacion
            // 
            txtObservacion.Location = new Point(162, 516);
            txtObservacion.Margin = new Padding(5, 4, 5, 4);
            txtObservacion.Name = "txtObservacion";
            txtObservacion.Size = new Size(627, 196);
            txtObservacion.TabIndex = 10;
            txtObservacion.Text = "";
            txtObservacion.TextChanged += txtAobservacion_TextChanged;
            // 
            // txtEdad
            // 
            txtEdad.Enabled = false;
            txtEdad.Location = new Point(675, 364);
            txtEdad.Margin = new Padding(5, 4, 5, 4);
            txtEdad.Name = "txtEdad";
            txtEdad.ReadOnly = true;
            txtEdad.Size = new Size(138, 27);
            txtEdad.TabIndex = 11;
            txtEdad.TextChanged += txtEdad_TextChanged;
            // 
            // cmbGenero
            // 
            cmbGenero.FormattingEnabled = true;
            cmbGenero.Location = new Point(246, 320);
            cmbGenero.Margin = new Padding(5, 4, 5, 4);
            cmbGenero.Name = "cmbGenero";
            cmbGenero.Size = new Size(198, 28);
            cmbGenero.TabIndex = 12;
            cmbGenero.SelectedIndexChanged += cmbGenero_SelectedIndexChanged;
            // 
            // btnGuardarPaciente
            // 
            btnGuardarPaciente.Cursor = Cursors.Hand;
            btnGuardarPaciente.Location = new Point(298, 740);
            btnGuardarPaciente.Margin = new Padding(5, 4, 5, 4);
            btnGuardarPaciente.Name = "btnGuardarPaciente";
            btnGuardarPaciente.Size = new Size(340, 43);
            btnGuardarPaciente.TabIndex = 13;
            btnGuardarPaciente.Text = "Guardar";
            btnGuardarPaciente.UseVisualStyleBackColor = true;
            btnGuardarPaciente.Click += btnGuardarPaciente_Click;
            // 
            // txtApellidos
            // 
            txtApellidos.Location = new Point(245, 269);
            txtApellidos.Margin = new Padding(5, 4, 5, 4);
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(588, 27);
            txtApellidos.TabIndex = 15;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(161, 273);
            lblApellido.Margin = new Padding(5, 0, 5, 0);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(75, 20);
            lblApellido.TabIndex = 16;
            lblApellido.Text = "Apellidos:";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1034, 133);
            panel1.TabIndex = 17;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(78, 158);
            label1.Name = "label1";
            label1.Size = new Size(67, 20);
            label1.TabIndex = 18;
            label1.Text = "Proyecto";
            // 
            // cmbProyecto
            // 
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(154, 158);
            cmbProyecto.Margin = new Padding(3, 4, 3, 4);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(256, 28);
            cmbProyecto.TabIndex = 19;
            cmbProyecto.SelectedIndexChanged += cmbProyecto_SelectedIndexChanged;
            // 
            // wAgregarPaciente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1034, 1024);
            ControlBox = false;
            Controls.Add(cmbProyecto);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(lblApellido);
            Controls.Add(txtApellidos);
            Controls.Add(btnGuardarPaciente);
            Controls.Add(cmbGenero);
            Controls.Add(txtEdad);
            Controls.Add(txtObservacion);
            Controls.Add(lblObservacion);
            Controls.Add(lblEdad);
            Controls.Add(dtpFechaNac);
            Controls.Add(lblFechaNac);
            Controls.Add(lblGenero);
            Controls.Add(txtNombres);
            Controls.Add(lblNombres);
            Controls.Add(txtCodigoBen);
            Controls.Add(lblCodigoBen);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(5, 4, 5, 4);
            Name = "wAgregarPaciente";
            Text = "wAgregarEstudiante";
            Load += wAgregarPaciente_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblCodigoBen;
        private System.Windows.Forms.TextBox txtCodigoBen;
        private System.Windows.Forms.Label lblNombres;
        private System.Windows.Forms.TextBox txtNombres;
        private System.Windows.Forms.Label lblGenero;
        private System.Windows.Forms.Label lblFechaNac;
        private System.Windows.Forms.DateTimePicker dtpFechaNac;
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.Label lblObservacion;
        private System.Windows.Forms.RichTextBox txtObservacion;
        private System.Windows.Forms.TextBox txtEdad;
        private System.Windows.Forms.ComboBox cmbGenero;
        private System.Windows.Forms.Button btnGuardarPaciente;
        private TextBox txtApellidos;
        private Label lblApellido;
        private Panel panel1;
        private Label label1;
        private ComboBox cmbProyecto;
    }
}