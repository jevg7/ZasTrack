﻿namespace ZasTrack
{
    partial class wAgregarEstudiante
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
            btnExcel = new Button();
            txtApellidos = new TextBox();
            lblApellido = new Label();
            SuspendLayout();
            // 
            // lblCodigoBen
            // 
            lblCodigoBen.AutoSize = true;
            lblCodigoBen.Location = new Point(70, 55);
            lblCodigoBen.Margin = new Padding(4, 0, 4, 0);
            lblCodigoBen.Name = "lblCodigoBen";
            lblCodigoBen.Size = new Size(130, 15);
            lblCodigoBen.TabIndex = 0;
            lblCodigoBen.Text = "Codigo de Beneficiario:";
            lblCodigoBen.Click += label1_Click;
            // 
            // txtCodigoBen
            // 
            txtCodigoBen.Location = new Point(205, 52);
            txtCodigoBen.Margin = new Padding(4, 3, 4, 3);
            txtCodigoBen.Name = "txtCodigoBen";
            txtCodigoBen.Size = new Size(238, 23);
            txtCodigoBen.TabIndex = 1;
            txtCodigoBen.TextChanged += txtAcodigo_TextChanged;
            // 
            // lblNombres
            // 
            lblNombres.AutoSize = true;
            lblNombres.Location = new Point(131, 97);
            lblNombres.Margin = new Padding(4, 0, 4, 0);
            lblNombres.Name = "lblNombres";
            lblNombres.Size = new Size(59, 15);
            lblNombres.TabIndex = 2;
            lblNombres.Text = "Nombres:";
            // 
            // txtNombres
            // 
            txtNombres.Location = new Point(205, 94);
            txtNombres.Margin = new Padding(4, 3, 4, 3);
            txtNombres.Name = "txtNombres";
            txtNombres.Size = new Size(515, 23);
            txtNombres.TabIndex = 3;
            txtNombres.TextChanged += txtAnombreApellido_TextChanged;
            // 
            // lblGenero
            // 
            lblGenero.AutoSize = true;
            lblGenero.Location = new Point(142, 181);
            lblGenero.Margin = new Padding(4, 0, 4, 0);
            lblGenero.Name = "lblGenero";
            lblGenero.Size = new Size(48, 15);
            lblGenero.TabIndex = 4;
            lblGenero.Text = "Genero:";
            // 
            // lblFechaNac
            // 
            lblFechaNac.AutoSize = true;
            lblFechaNac.Location = new Point(75, 243);
            lblFechaNac.Margin = new Padding(4, 0, 4, 0);
            lblFechaNac.Name = "lblFechaNac";
            lblFechaNac.Size = new Size(122, 15);
            lblFechaNac.TabIndex = 5;
            lblFechaNac.Text = "Fecha de Nacimiento:";
            // 
            // dtpFechaNac
            // 
            dtpFechaNac.Location = new Point(205, 237);
            dtpFechaNac.Margin = new Padding(4, 3, 4, 3);
            dtpFechaNac.Name = "dtpFechaNac";
            dtpFechaNac.Size = new Size(238, 23);
            dtpFechaNac.TabIndex = 6;
            dtpFechaNac.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // lblEdad
            // 
            lblEdad.AutoSize = true;
            lblEdad.Location = new Point(490, 240);
            lblEdad.Margin = new Padding(4, 0, 4, 0);
            lblEdad.Name = "lblEdad";
            lblEdad.Size = new Size(36, 15);
            lblEdad.TabIndex = 7;
            lblEdad.Text = "Edad:";
            // 
            // lblObservacion
            // 
            lblObservacion.AutoSize = true;
            lblObservacion.Location = new Point(370, 292);
            lblObservacion.Margin = new Padding(4, 0, 4, 0);
            lblObservacion.Name = "lblObservacion";
            lblObservacion.Size = new Size(73, 15);
            lblObservacion.TabIndex = 8;
            lblObservacion.Text = "Observacion";
            // 
            // txtObservacion
            // 
            txtObservacion.Location = new Point(142, 319);
            txtObservacion.Margin = new Padding(4, 3, 4, 3);
            txtObservacion.Name = "txtObservacion";
            txtObservacion.Size = new Size(549, 148);
            txtObservacion.TabIndex = 10;
            txtObservacion.Text = "";
            txtObservacion.TextChanged += txtAobservacion_TextChanged;
            // 
            // txtEdad
            // 
            txtEdad.Enabled = false;
            txtEdad.Location = new Point(551, 237);
            txtEdad.Margin = new Padding(4, 3, 4, 3);
            txtEdad.Name = "txtEdad";
            txtEdad.ReadOnly = true;
            txtEdad.Size = new Size(92, 23);
            txtEdad.TabIndex = 11;
            txtEdad.TextChanged += txtEdad_TextChanged;
            // 
            // cmbGenero
            // 
            cmbGenero.FormattingEnabled = true;
            cmbGenero.Location = new Point(205, 178);
            cmbGenero.Margin = new Padding(4, 3, 4, 3);
            cmbGenero.Name = "cmbGenero";
            cmbGenero.Size = new Size(174, 23);
            cmbGenero.TabIndex = 12;
            cmbGenero.SelectedIndexChanged += cmbGenero_SelectedIndexChanged;
            // 
            // btnGuardarPaciente
            // 
            btnGuardarPaciente.Cursor = Cursors.Hand;
            btnGuardarPaciente.Location = new Point(603, 497);
            btnGuardarPaciente.Margin = new Padding(4, 3, 4, 3);
            btnGuardarPaciente.Name = "btnGuardarPaciente";
            btnGuardarPaciente.Size = new Size(147, 32);
            btnGuardarPaciente.TabIndex = 13;
            btnGuardarPaciente.Text = "Guardar";
            btnGuardarPaciente.UseVisualStyleBackColor = true;
            btnGuardarPaciente.Click += btnGuardarPaciente_Click;
            // 
            // btnExcel
            // 
            btnExcel.Cursor = Cursors.Hand;
            btnExcel.Location = new Point(59, 497);
            btnExcel.Margin = new Padding(4, 3, 4, 3);
            btnExcel.Name = "btnExcel";
            btnExcel.Size = new Size(147, 32);
            btnExcel.TabIndex = 14;
            btnExcel.Text = "Excel";
            btnExcel.UseVisualStyleBackColor = true;
            // 
            // txtApellidos
            // 
            txtApellidos.Location = new Point(205, 136);
            txtApellidos.Margin = new Padding(4, 3, 4, 3);
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(515, 23);
            txtApellidos.TabIndex = 15;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(131, 139);
            lblApellido.Margin = new Padding(4, 0, 4, 0);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(59, 15);
            lblApellido.TabIndex = 16;
            lblApellido.Text = "Apellidos:";
            // 
            // wAgregarEstudiante
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(901, 751);
            ControlBox = false;
            Controls.Add(lblApellido);
            Controls.Add(txtApellidos);
            Controls.Add(btnExcel);
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
            Margin = new Padding(4, 3, 4, 3);
            Name = "wAgregarEstudiante";
            Text = "wAgregarEstudiante";
            Load += wAgregarEstudiante_Load;
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
        private System.Windows.Forms.Button btnExcel;
        private TextBox txtApellidos;
        private Label lblApellido;
    }
}