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
            this.label1 = new System.Windows.Forms.Label();
            this.txtAcodigo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAnombreApellido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAobservacion = new System.Windows.Forms.RichTextBox();
            this.txtaEdad = new System.Windows.Forms.TextBox();
            this.cmbAgenero = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Codigo de Beneficiario";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtAcodigo
            // 
            this.txtAcodigo.Location = new System.Drawing.Point(176, 45);
            this.txtAcodigo.Name = "txtAcodigo";
            this.txtAcodigo.Size = new System.Drawing.Size(205, 20);
            this.txtAcodigo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nombre y Apellido";
            // 
            // txtAnombreApellido
            // 
            this.txtAnombreApellido.Location = new System.Drawing.Point(176, 115);
            this.txtAnombreApellido.Name = "txtAnombreApellido";
            this.txtAnombreApellido.Size = new System.Drawing.Size(442, 20);
            this.txtAnombreApellido.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Genero";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 244);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Fecha de Nacimiento";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(196, 244);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(257, 20);
            this.dateTimePicker1.TabIndex = 6;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(510, 250);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Edad";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 334);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Observacion";
            // 
            // txtAobservacion
            // 
            this.txtAobservacion.Location = new System.Drawing.Point(136, 334);
            this.txtAobservacion.Name = "txtAobservacion";
            this.txtAobservacion.Size = new System.Drawing.Size(471, 108);
            this.txtAobservacion.TabIndex = 10;
            this.txtAobservacion.Text = "";
            // 
            // txtaEdad
            // 
            this.txtaEdad.Location = new System.Drawing.Point(563, 247);
            this.txtaEdad.Name = "txtaEdad";
            this.txtaEdad.ReadOnly = true;
            this.txtaEdad.Size = new System.Drawing.Size(79, 20);
            this.txtaEdad.TabIndex = 11;
            // 
            // cmbAgenero
            // 
            this.cmbAgenero.FormattingEnabled = true;
            this.cmbAgenero.Location = new System.Drawing.Point(176, 189);
            this.cmbAgenero.Name = "cmbAgenero";
            this.cmbAgenero.Size = new System.Drawing.Size(150, 21);
            this.cmbAgenero.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(623, 467);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 28);
            this.button1.TabIndex = 13;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(18, 467);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 28);
            this.button2.TabIndex = 14;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // wAgregarEstudiante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 507);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbAgenero);
            this.Controls.Add(this.txtaEdad);
            this.Controls.Add(this.txtAobservacion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAnombreApellido);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAcodigo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "wAgregarEstudiante";
            this.Text = "wAgregarEstudiante";
            this.Load += new System.EventHandler(this.wAgregarEstudiante_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}