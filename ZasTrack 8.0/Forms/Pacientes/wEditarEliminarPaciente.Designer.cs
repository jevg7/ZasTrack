namespace ZasTrack.Forms.Estudiantes
{
    partial class wEditarEliminarPaciente
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
            txtBusqueda = new TextBox();
            panel1 = new Panel();
            label2 = new Label();
            cmbProyecto = new ComboBox();
            btnGuardar = new Button();
            txtGenero = new TextBox();
            btnEliminar = new Button();
            btnCancelar = new Button();
            btnEditar = new Button();
            lblApellido = new Label();
            txtApellidos = new TextBox();
            cmbGenero = new ComboBox();
            txtEdad = new TextBox();
            txtObservacion = new RichTextBox();
            lblObservacion = new Label();
            lblEdad = new Label();
            dtpFechaNac = new DateTimePicker();
            lblFechaNac = new Label();
            lblGenero = new Label();
            txtNombres = new TextBox();
            lblNombres = new Label();
            txtCodigoBen = new TextBox();
            lblCodigoBen = new Label();
            panel2 = new Panel();
            btnBuscar = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(14, 40);
            label1.Name = "label1";
            label1.Size = new Size(111, 15);
            label1.TabIndex = 0;
            label1.Text = "Codigo Beneficiario";
            // 
            // txtBusqueda
            // 
            txtBusqueda.Location = new Point(148, 37);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(445, 23);
            txtBusqueda.TabIndex = 1;
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(cmbProyecto);
            panel1.Controls.Add(btnGuardar);
            panel1.Controls.Add(txtGenero);
            panel1.Controls.Add(btnEliminar);
            panel1.Controls.Add(btnCancelar);
            panel1.Controls.Add(btnEditar);
            panel1.Controls.Add(lblApellido);
            panel1.Controls.Add(txtApellidos);
            panel1.Controls.Add(cmbGenero);
            panel1.Controls.Add(txtEdad);
            panel1.Controls.Add(txtObservacion);
            panel1.Controls.Add(lblObservacion);
            panel1.Controls.Add(lblEdad);
            panel1.Controls.Add(dtpFechaNac);
            panel1.Controls.Add(lblFechaNac);
            panel1.Controls.Add(lblGenero);
            panel1.Controls.Add(txtNombres);
            panel1.Controls.Add(lblNombres);
            panel1.Controls.Add(txtCodigoBen);
            panel1.Controls.Add(lblCodigoBen);
            panel1.Location = new Point(-2, 125);
            panel1.Name = "panel1";
            panel1.Size = new Size(887, 588);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(539, 213);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 37;
            label2.Text = "Proyecto";
            // 
            // cmbProyecto
            // 
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(604, 213);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(121, 23);
            cmbProyecto.TabIndex = 36;
            cmbProyecto.SelectedIndexChanged += cmbProyecto_SelectedIndexChanged;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(572, 467);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(131, 42);
            btnGuardar.TabIndex = 35;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Visible = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtGenero
            // 
            txtGenero.Enabled = false;
            txtGenero.Location = new Point(239, 151);
            txtGenero.Name = "txtGenero";
            txtGenero.Size = new Size(268, 23);
            txtGenero.TabIndex = 34;
            txtGenero.TextChanged += textBox2_TextChanged;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(572, 467);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(131, 42);
            btnEliminar.TabIndex = 33;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(376, 467);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(131, 42);
            btnCancelar.TabIndex = 32;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Visible = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(176, 467);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(131, 42);
            btnEditar.TabIndex = 31;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(165, 112);
            lblApellido.Margin = new Padding(4, 0, 4, 0);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(59, 15);
            lblApellido.TabIndex = 30;
            lblApellido.Text = "Apellidos:";
            // 
            // txtApellidos
            // 
            txtApellidos.Enabled = false;
            txtApellidos.Location = new Point(239, 109);
            txtApellidos.Margin = new Padding(4, 3, 4, 3);
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(515, 23);
            txtApellidos.TabIndex = 29;
            // 
            // cmbGenero
            // 
            cmbGenero.FormattingEnabled = true;
            cmbGenero.Location = new Point(239, 151);
            cmbGenero.Margin = new Padding(4, 3, 4, 3);
            cmbGenero.Name = "cmbGenero";
            cmbGenero.Size = new Size(174, 23);
            cmbGenero.TabIndex = 28;
            cmbGenero.Visible = false;
            cmbGenero.SelectedIndexChanged += cmbGenero_SelectedIndexChanged;
            // 
            // txtEdad
            // 
            txtEdad.Enabled = false;
            txtEdad.Location = new Point(611, 154);
            txtEdad.Margin = new Padding(4, 3, 4, 3);
            txtEdad.Name = "txtEdad";
            txtEdad.ReadOnly = true;
            txtEdad.Size = new Size(92, 23);
            txtEdad.TabIndex = 27;
            // 
            // txtObservacion
            // 
            txtObservacion.Enabled = false;
            txtObservacion.Location = new Point(176, 292);
            txtObservacion.Margin = new Padding(4, 3, 4, 3);
            txtObservacion.Name = "txtObservacion";
            txtObservacion.Size = new Size(549, 148);
            txtObservacion.TabIndex = 26;
            txtObservacion.Text = "";
            // 
            // lblObservacion
            // 
            lblObservacion.AutoSize = true;
            lblObservacion.Location = new Point(404, 265);
            lblObservacion.Margin = new Padding(4, 0, 4, 0);
            lblObservacion.Name = "lblObservacion";
            lblObservacion.Size = new Size(73, 15);
            lblObservacion.TabIndex = 25;
            lblObservacion.Text = "Observacion";
            // 
            // lblEdad
            // 
            lblEdad.AutoSize = true;
            lblEdad.Location = new Point(539, 157);
            lblEdad.Margin = new Padding(4, 0, 4, 0);
            lblEdad.Name = "lblEdad";
            lblEdad.Size = new Size(36, 15);
            lblEdad.TabIndex = 24;
            lblEdad.Text = "Edad:";
            // 
            // dtpFechaNac
            // 
            dtpFechaNac.Enabled = false;
            dtpFechaNac.Location = new Point(239, 210);
            dtpFechaNac.Margin = new Padding(4, 3, 4, 3);
            dtpFechaNac.Name = "dtpFechaNac";
            dtpFechaNac.Size = new Size(238, 23);
            dtpFechaNac.TabIndex = 23;
            dtpFechaNac.ValueChanged += dtpFechaNac_ValueChanged;
            // 
            // lblFechaNac
            // 
            lblFechaNac.AutoSize = true;
            lblFechaNac.Location = new Point(109, 216);
            lblFechaNac.Margin = new Padding(4, 0, 4, 0);
            lblFechaNac.Name = "lblFechaNac";
            lblFechaNac.Size = new Size(122, 15);
            lblFechaNac.TabIndex = 22;
            lblFechaNac.Text = "Fecha de Nacimiento:";
            // 
            // lblGenero
            // 
            lblGenero.AutoSize = true;
            lblGenero.Location = new Point(176, 154);
            lblGenero.Margin = new Padding(4, 0, 4, 0);
            lblGenero.Name = "lblGenero";
            lblGenero.Size = new Size(48, 15);
            lblGenero.TabIndex = 21;
            lblGenero.Text = "Genero:";
            // 
            // txtNombres
            // 
            txtNombres.Enabled = false;
            txtNombres.Location = new Point(239, 67);
            txtNombres.Margin = new Padding(4, 3, 4, 3);
            txtNombres.Name = "txtNombres";
            txtNombres.Size = new Size(515, 23);
            txtNombres.TabIndex = 20;
            // 
            // lblNombres
            // 
            lblNombres.AutoSize = true;
            lblNombres.Location = new Point(165, 70);
            lblNombres.Margin = new Padding(4, 0, 4, 0);
            lblNombres.Name = "lblNombres";
            lblNombres.Size = new Size(59, 15);
            lblNombres.TabIndex = 19;
            lblNombres.Text = "Nombres:";
            // 
            // txtCodigoBen
            // 
            txtCodigoBen.Enabled = false;
            txtCodigoBen.Location = new Point(242, 25);
            txtCodigoBen.Margin = new Padding(4, 3, 4, 3);
            txtCodigoBen.Name = "txtCodigoBen";
            txtCodigoBen.Size = new Size(238, 23);
            txtCodigoBen.TabIndex = 18;
            // 
            // lblCodigoBen
            // 
            lblCodigoBen.AutoSize = true;
            lblCodigoBen.Location = new Point(104, 28);
            lblCodigoBen.Margin = new Padding(4, 0, 4, 0);
            lblCodigoBen.Name = "lblCodigoBen";
            lblCodigoBen.Size = new Size(130, 15);
            lblCodigoBen.TabIndex = 17;
            lblCodigoBen.Text = "Codigo de Beneficiario:";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Controls.Add(btnBuscar);
            panel2.Controls.Add(txtBusqueda);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(885, 124);
            panel2.TabIndex = 3;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(626, 37);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // wEditarEliminarPaciente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(885, 712);
            Controls.Add(panel2);
            Controls.Add(panel1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "wEditarEliminarPaciente";
            Text = "wEditarEstudiante";
            Load += wEditarEliminarEstudiante_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private TextBox txtBusqueda;
        private Panel panel1;
        private Panel panel2;
        private Button btnEliminar;
        private Button btnCancelar;
        private Button btnEditar;
        private Label lblApellido;
        private TextBox txtApellidos;
        private ComboBox cmbGenero;
        private TextBox txtEdad;
        private RichTextBox txtObservacion;
        private Label lblObservacion;
        private Label lblEdad;
        private DateTimePicker dtpFechaNac;
        private Label lblFechaNac;
        private Label lblGenero;
        private TextBox txtNombres;
        private Label lblNombres;
        private TextBox txtCodigoBen;
        private Label lblCodigoBen;
        private Button btnBuscar;
        private TextBox txtGenero;
        private Button btnGuardar;
        private Label label2;
        private ComboBox cmbProyecto;
    }
}