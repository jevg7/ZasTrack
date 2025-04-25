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
            pnlEditacion = new Panel();
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
            lblEdadCalculada = new Label();
            dtpFechaNac = new DateTimePicker();
            lblFechaNac = new Label();
            lblGenero = new Label();
            txtNombres = new TextBox();
            lblNombres = new Label();
            txtCodigoBen = new TextBox();
            lblCodigoBen = new Label();
            panel2 = new Panel();
            pnlEditacion.SuspendLayout();
            SuspendLayout();
            // 
            // pnlEditacion
            // 
            pnlEditacion.Controls.Add(label2);
            pnlEditacion.Controls.Add(cmbProyecto);
            pnlEditacion.Controls.Add(btnGuardar);
            pnlEditacion.Controls.Add(txtGenero);
            pnlEditacion.Controls.Add(btnEliminar);
            pnlEditacion.Controls.Add(btnCancelar);
            pnlEditacion.Controls.Add(btnEditar);
            pnlEditacion.Controls.Add(lblApellido);
            pnlEditacion.Controls.Add(txtApellidos);
            pnlEditacion.Controls.Add(cmbGenero);
            pnlEditacion.Controls.Add(txtEdad);
            pnlEditacion.Controls.Add(txtObservacion);
            pnlEditacion.Controls.Add(lblObservacion);
            pnlEditacion.Controls.Add(lblEdadCalculada);
            pnlEditacion.Controls.Add(dtpFechaNac);
            pnlEditacion.Controls.Add(lblFechaNac);
            pnlEditacion.Controls.Add(lblGenero);
            pnlEditacion.Controls.Add(txtNombres);
            pnlEditacion.Controls.Add(lblNombres);
            pnlEditacion.Controls.Add(txtCodigoBen);
            pnlEditacion.Controls.Add(lblCodigoBen);
            pnlEditacion.Location = new Point(-2, 123);
            pnlEditacion.Margin = new Padding(3, 4, 3, 4);
            pnlEditacion.Name = "pnlEditacion";
            pnlEditacion.Size = new Size(1014, 828);
            pnlEditacion.TabIndex = 2;
            pnlEditacion.Paint += panel1_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(92, 41);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 37;
            label2.Text = "Proyecto";
            // 
            // cmbProyecto
            // 
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(165, 41);
            cmbProyecto.Margin = new Padding(3, 4, 3, 4);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(243, 28);
            cmbProyecto.TabIndex = 36;
            cmbProyecto.SelectedIndexChanged += cmbProyecto_SelectedIndexChanged;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(654, 623);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(150, 56);
            btnGuardar.TabIndex = 35;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Visible = false;
            // 
            // txtGenero
            // 
            txtGenero.Enabled = false;
            txtGenero.Location = new Point(226, 215);
            txtGenero.Margin = new Padding(3, 4, 3, 4);
            txtGenero.Name = "txtGenero";
            txtGenero.Size = new Size(306, 27);
            txtGenero.TabIndex = 34;
            txtGenero.TextChanged += textBox2_TextChanged;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(654, 623);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(150, 56);
            btnEliminar.TabIndex = 33;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(430, 623);
            btnCancelar.Margin = new Padding(3, 4, 3, 4);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(150, 56);
            btnCancelar.TabIndex = 32;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Visible = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(201, 623);
            btnEditar.Margin = new Padding(3, 4, 3, 4);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(150, 56);
            btnEditar.TabIndex = 31;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(142, 162);
            lblApellido.Margin = new Padding(5, 0, 5, 0);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(75, 20);
            lblApellido.TabIndex = 30;
            lblApellido.Text = "Apellidos:";
            // 
            // txtApellidos
            // 
            txtApellidos.Enabled = false;
            txtApellidos.Location = new Point(226, 158);
            txtApellidos.Margin = new Padding(5, 4, 5, 4);
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(588, 27);
            txtApellidos.TabIndex = 29;
            // 
            // cmbGenero
            // 
            cmbGenero.FormattingEnabled = true;
            cmbGenero.Location = new Point(226, 214);
            cmbGenero.Margin = new Padding(5, 4, 5, 4);
            cmbGenero.Name = "cmbGenero";
            cmbGenero.Size = new Size(198, 28);
            cmbGenero.TabIndex = 28;
            cmbGenero.Visible = false;
            cmbGenero.SelectedIndexChanged += cmbGenero_SelectedIndexChanged;
            // 
            // txtEdad
            // 
            txtEdad.Enabled = false;
            txtEdad.Location = new Point(562, 309);
            txtEdad.Margin = new Padding(5, 4, 5, 4);
            txtEdad.Name = "txtEdad";
            txtEdad.ReadOnly = true;
            txtEdad.Size = new Size(207, 27);
            txtEdad.TabIndex = 27;
            // 
            // txtObservacion
            // 
            txtObservacion.Enabled = false;
            txtObservacion.Location = new Point(201, 389);
            txtObservacion.Margin = new Padding(5, 4, 5, 4);
            txtObservacion.Name = "txtObservacion";
            txtObservacion.Size = new Size(627, 196);
            txtObservacion.TabIndex = 26;
            txtObservacion.Text = "";
            // 
            // lblObservacion
            // 
            lblObservacion.AutoSize = true;
            lblObservacion.Location = new Point(462, 353);
            lblObservacion.Margin = new Padding(5, 0, 5, 0);
            lblObservacion.Name = "lblObservacion";
            lblObservacion.Size = new Size(91, 20);
            lblObservacion.TabIndex = 25;
            lblObservacion.Text = "Observacion";
            // 
            // lblEdadCalculada
            // 
            lblEdadCalculada.AutoSize = true;
            lblEdadCalculada.Location = new Point(562, 285);
            lblEdadCalculada.Margin = new Padding(5, 0, 5, 0);
            lblEdadCalculada.Name = "lblEdadCalculada";
            lblEdadCalculada.Size = new Size(46, 20);
            lblEdadCalculada.TabIndex = 24;
            lblEdadCalculada.Text = "Edad:";
            // 
            // dtpFechaNac
            // 
            dtpFechaNac.Enabled = false;
            dtpFechaNac.Location = new Point(225, 281);
            dtpFechaNac.Margin = new Padding(5, 4, 5, 4);
            dtpFechaNac.Name = "dtpFechaNac";
            dtpFechaNac.Size = new Size(307, 27);
            dtpFechaNac.TabIndex = 23;
            dtpFechaNac.ValueChanged += dtpFechaNac_ValueChanged;
            // 
            // lblFechaNac
            // 
            lblFechaNac.AutoSize = true;
            lblFechaNac.Location = new Point(67, 285);
            lblFechaNac.Margin = new Padding(5, 0, 5, 0);
            lblFechaNac.Name = "lblFechaNac";
            lblFechaNac.Size = new Size(152, 20);
            lblFechaNac.TabIndex = 22;
            lblFechaNac.Text = "Fecha de Nacimiento:";
            // 
            // lblGenero
            // 
            lblGenero.AutoSize = true;
            lblGenero.Location = new Point(154, 218);
            lblGenero.Margin = new Padding(5, 0, 5, 0);
            lblGenero.Name = "lblGenero";
            lblGenero.Size = new Size(60, 20);
            lblGenero.TabIndex = 21;
            lblGenero.Text = "Genero:";
            // 
            // txtNombres
            // 
            txtNombres.Enabled = false;
            txtNombres.Location = new Point(226, 102);
            txtNombres.Margin = new Padding(5, 4, 5, 4);
            txtNombres.Name = "txtNombres";
            txtNombres.Size = new Size(588, 27);
            txtNombres.TabIndex = 20;
            // 
            // lblNombres
            // 
            lblNombres.AutoSize = true;
            lblNombres.Location = new Point(142, 106);
            lblNombres.Margin = new Padding(5, 0, 5, 0);
            lblNombres.Name = "lblNombres";
            lblNombres.Size = new Size(73, 20);
            lblNombres.TabIndex = 19;
            lblNombres.Text = "Nombres:";
            lblNombres.Click += lblNombres_Click;
            // 
            // txtCodigoBen
            // 
            txtCodigoBen.Enabled = false;
            txtCodigoBen.Location = new Point(654, 42);
            txtCodigoBen.Margin = new Padding(5, 4, 5, 4);
            txtCodigoBen.Name = "txtCodigoBen";
            txtCodigoBen.Size = new Size(271, 27);
            txtCodigoBen.TabIndex = 18;
            // 
            // lblCodigoBen
            // 
            lblCodigoBen.AutoSize = true;
            lblCodigoBen.Location = new Point(479, 45);
            lblCodigoBen.Margin = new Padding(5, 0, 5, 0);
            lblCodigoBen.Name = "lblCodigoBen";
            lblCodigoBen.Size = new Size(165, 20);
            lblCodigoBen.TabIndex = 17;
            lblCodigoBen.Text = "Codigo de Beneficiario:";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1011, 115);
            panel2.TabIndex = 3;
            // 
            // wEditarEliminarPaciente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1011, 949);
            Controls.Add(panel2);
            Controls.Add(pnlEditacion);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "wEditarEliminarPaciente";
            ShowInTaskbar = false;
            Text = "wEditarEstudiante";
            Load += wEditarEliminarEstudiante_Load;
            pnlEditacion.ResumeLayout(false);
            pnlEditacion.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private TextBox txtBusqueda;
        private Panel pnlEditacion;
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
        private Label lblEdadCalculada;
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