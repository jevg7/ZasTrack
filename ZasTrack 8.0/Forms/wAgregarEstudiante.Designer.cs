namespace ZasTrack
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
            label1 = new Label();
            txtAcodigo = new TextBox();
            label2 = new Label();
            txtAnombreApellido = new TextBox();
            label3 = new Label();
            label4 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label5 = new Label();
            label6 = new Label();
            txtAobservacion = new RichTextBox();
            txtaEdad = new TextBox();
            cmbAgenero = new ComboBox();
            btnGuardarPaciente = new Button();
            btnExcel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 52);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(127, 15);
            label1.TabIndex = 0;
            label1.Text = "Codigo de Beneficiario";
            label1.Click += label1_Click;
            // 
            // txtAcodigo
            // 
            txtAcodigo.Location = new Point(205, 52);
            txtAcodigo.Margin = new Padding(4, 3, 4, 3);
            txtAcodigo.Name = "txtAcodigo";
            txtAcodigo.Size = new Size(238, 23);
            txtAcodigo.TabIndex = 1;
            txtAcodigo.TextChanged += txtAcodigo_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(50, 133);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(107, 15);
            label2.TabIndex = 2;
            label2.Text = "Nombre y Apellido";
            // 
            // txtAnombreApellido
            // 
            txtAnombreApellido.Location = new Point(205, 133);
            txtAnombreApellido.Margin = new Padding(4, 3, 4, 3);
            txtAnombreApellido.Name = "txtAnombreApellido";
            txtAnombreApellido.Size = new Size(515, 23);
            txtAnombreApellido.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(76, 218);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 4;
            label3.Text = "Genero";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(36, 282);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(119, 15);
            label4.TabIndex = 5;
            label4.Text = "Fecha de Nacimiento";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(205, 282);
            dateTimePicker1.Margin = new Padding(4, 3, 4, 3);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(238, 23);
            dateTimePicker1.TabIndex = 6;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(491, 288);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(33, 15);
            label5.TabIndex = 7;
            label5.Text = "Edad";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(50, 359);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(73, 15);
            label6.TabIndex = 8;
            label6.Text = "Observacion";
            // 
            // txtAobservacion
            // 
            txtAobservacion.Location = new Point(171, 356);
            txtAobservacion.Margin = new Padding(4, 3, 4, 3);
            txtAobservacion.Name = "txtAobservacion";
            txtAobservacion.Size = new Size(549, 124);
            txtAobservacion.TabIndex = 10;
            txtAobservacion.Text = "";
            // 
            // txtaEdad
            // 
            txtaEdad.Enabled = false;
            txtaEdad.Location = new Point(552, 285);
            txtaEdad.Margin = new Padding(4, 3, 4, 3);
            txtaEdad.Name = "txtaEdad";
            txtaEdad.ReadOnly = true;
            txtaEdad.Size = new Size(92, 23);
            txtaEdad.TabIndex = 11;
            // 
            // cmbAgenero
            // 
            cmbAgenero.FormattingEnabled = true;
            cmbAgenero.Location = new Point(205, 218);
            cmbAgenero.Margin = new Padding(4, 3, 4, 3);
            cmbAgenero.Name = "cmbAgenero";
            cmbAgenero.Size = new Size(174, 23);
            cmbAgenero.TabIndex = 12;
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
            // wAgregarEstudiante
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(901, 751);
            ControlBox = false;
            Controls.Add(btnExcel);
            Controls.Add(btnGuardarPaciente);
            Controls.Add(cmbAgenero);
            Controls.Add(txtaEdad);
            Controls.Add(txtAobservacion);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(dateTimePicker1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtAnombreApellido);
            Controls.Add(label2);
            Controls.Add(txtAcodigo);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "wAgregarEstudiante";
            Text = "wAgregarEstudiante";
            Load += wAgregarEstudiante_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAcodigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAnombreApellido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox txtAobservacion;
        private System.Windows.Forms.TextBox txtaEdad;
        private System.Windows.Forms.ComboBox cmbAgenero;
        private System.Windows.Forms.Button btnGuardarPaciente;
        private System.Windows.Forms.Button btnExcel;
    }
}