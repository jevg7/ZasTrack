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
            txtNombres = new TextBox();
            lblGenero = new Label();
            lblFechaNac = new Label();
            dtpFechaNac = new DateTimePicker();
            lblEdad = new Label();
            lblObservacion = new Label();
            txtObservacion = new RichTextBox();
            txtEdad = new TextBox();
            cmbGenero = new ComboBox();
            txtApellidos = new TextBox();
            lblApellido = new Label();
            pnlSuperficie = new Panel();
            lblProyecto = new Label();
            cmbProyecto = new ComboBox();
            tlpOrganizador = new TableLayoutPanel();
            pnlBarra = new Panel();
            pnlContenido = new Panel();
            tlpContenido = new TableLayoutPanel();
            tlpCont1 = new TableLayoutPanel();
            pnlProyecto = new Panel();
            tlpProyectoCont = new TableLayoutPanel();
            pnlCodigo = new Panel();
            tlpCodigoCont = new TableLayoutPanel();
            tlpCont2 = new TableLayoutPanel();
            pnlNombre = new Panel();
            lblNombres = new Label();
            pnlApellido = new Panel();
            pnlGenero = new Panel();
            tlpCont3 = new TableLayoutPanel();
            pnlEdad = new Panel();
            pnlFechaNac = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            pnlObservaciones = new Panel();
            pnlBotones = new Panel();
            tlpBotonGuardar = new TableLayoutPanel();
            btnGuardarPaciente = new Button();
            pnlContenedor = new Panel();
            tlpOrganizador.SuspendLayout();
            pnlBarra.SuspendLayout();
            pnlContenido.SuspendLayout();
            tlpContenido.SuspendLayout();
            tlpCont1.SuspendLayout();
            pnlProyecto.SuspendLayout();
            tlpProyectoCont.SuspendLayout();
            pnlCodigo.SuspendLayout();
            tlpCodigoCont.SuspendLayout();
            tlpCont2.SuspendLayout();
            pnlNombre.SuspendLayout();
            pnlApellido.SuspendLayout();
            pnlGenero.SuspendLayout();
            tlpCont3.SuspendLayout();
            pnlEdad.SuspendLayout();
            pnlFechaNac.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            pnlObservaciones.SuspendLayout();
            pnlBotones.SuspendLayout();
            tlpBotonGuardar.SuspendLayout();
            pnlContenedor.SuspendLayout();
            SuspendLayout();
            // 
            // lblCodigoBen
            // 
            lblCodigoBen.Anchor = AnchorStyles.Left;
            lblCodigoBen.AutoSize = true;
            lblCodigoBen.Location = new Point(5, 25);
            lblCodigoBen.Margin = new Padding(5, 0, 5, 0);
            lblCodigoBen.Name = "lblCodigoBen";
            lblCodigoBen.Size = new Size(91, 40);
            lblCodigoBen.TabIndex = 0;
            lblCodigoBen.Text = "Codigo de Beneficiario:";
            // 
            // txtCodigoBen
            // 
            txtCodigoBen.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtCodigoBen.Location = new Point(109, 31);
            txtCodigoBen.Margin = new Padding(5, 4, 5, 4);
            txtCodigoBen.Name = "txtCodigoBen";
            txtCodigoBen.Size = new Size(409, 27);
            txtCodigoBen.TabIndex = 1;
            txtCodigoBen.TextChanged += txtAcodigo_TextChanged;
            // 
            // txtNombres
            // 
            txtNombres.Location = new Point(96, 13);
            txtNombres.Margin = new Padding(5, 4, 5, 4);
            txtNombres.Name = "txtNombres";
            txtNombres.Size = new Size(542, 27);
            txtNombres.TabIndex = 3;
            txtNombres.TextChanged += txtAnombreApellido_TextChanged;
            // 
            // lblGenero
            // 
            lblGenero.AutoSize = true;
            lblGenero.Location = new Point(42, 16);
            lblGenero.Margin = new Padding(5, 0, 5, 0);
            lblGenero.Name = "lblGenero";
            lblGenero.Size = new Size(60, 20);
            lblGenero.TabIndex = 4;
            lblGenero.Text = "Genero:";
            // 
            // lblFechaNac
            // 
            lblFechaNac.AutoSize = true;
            lblFechaNac.Location = new Point(35, 0);
            lblFechaNac.Margin = new Padding(35, 0, 0, 0);
            lblFechaNac.Name = "lblFechaNac";
            lblFechaNac.Size = new Size(152, 20);
            lblFechaNac.TabIndex = 5;
            lblFechaNac.Text = "Fecha de Nacimiento:";
            // 
            // dtpFechaNac
            // 
            dtpFechaNac.ImeMode = ImeMode.NoControl;
            dtpFechaNac.Location = new Point(35, 25);
            dtpFechaNac.Margin = new Padding(35, 4, 5, 4);
            dtpFechaNac.Name = "dtpFechaNac";
            dtpFechaNac.Size = new Size(483, 27);
            dtpFechaNac.TabIndex = 6;
            dtpFechaNac.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // lblEdad
            // 
            lblEdad.AutoSize = true;
            lblEdad.Dock = DockStyle.Top;
            lblEdad.Location = new Point(0, 0);
            lblEdad.Margin = new Padding(5, 0, 5, 0);
            lblEdad.Name = "lblEdad";
            lblEdad.Size = new Size(46, 20);
            lblEdad.TabIndex = 7;
            lblEdad.Text = "Edad:";
            // 
            // lblObservacion
            // 
            lblObservacion.AutoSize = true;
            lblObservacion.Location = new Point(1, 1);
            lblObservacion.Margin = new Padding(5, 0, 5, 0);
            lblObservacion.Name = "lblObservacion";
            lblObservacion.Size = new Size(91, 20);
            lblObservacion.TabIndex = 8;
            lblObservacion.Text = "Observacion";
            // 
            // txtObservacion
            // 
            txtObservacion.Dock = DockStyle.Bottom;
            txtObservacion.Location = new Point(0, 25);
            txtObservacion.Margin = new Padding(5, 4, 5, 4);
            txtObservacion.Name = "txtObservacion";
            txtObservacion.Size = new Size(1058, 108);
            txtObservacion.TabIndex = 10;
            txtObservacion.Text = "";
            txtObservacion.TextChanged += txtAobservacion_TextChanged;
            // 
            // txtEdad
            // 
            txtEdad.Dock = DockStyle.Left;
            txtEdad.Enabled = false;
            txtEdad.Location = new Point(0, 20);
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
            cmbGenero.Location = new Point(99, 13);
            cmbGenero.Margin = new Padding(5, 4, 5, 4);
            cmbGenero.Name = "cmbGenero";
            cmbGenero.Size = new Size(542, 28);
            cmbGenero.TabIndex = 12;
            cmbGenero.SelectedIndexChanged += cmbGenero_SelectedIndexChanged;
            // 
            // txtApellidos
            // 
            txtApellidos.Location = new Point(96, 13);
            txtApellidos.Margin = new Padding(5, 4, 5, 4);
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(542, 27);
            txtApellidos.TabIndex = 15;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(25, 13);
            lblApellido.Margin = new Padding(5, 0, 5, 0);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(75, 20);
            lblApellido.TabIndex = 16;
            lblApellido.Text = "Apellidos:";
            // 
            // pnlSuperficie
            // 
            pnlSuperficie.BackColor = SystemColors.ActiveCaptionText;
            pnlSuperficie.Dock = DockStyle.Fill;
            pnlSuperficie.Location = new Point(0, 0);
            pnlSuperficie.Margin = new Padding(3, 4, 3, 4);
            pnlSuperficie.Name = "pnlSuperficie";
            pnlSuperficie.Size = new Size(1064, 125);
            pnlSuperficie.TabIndex = 17;
            // 
            // lblProyecto
            // 
            lblProyecto.Anchor = AnchorStyles.Left;
            lblProyecto.AutoSize = true;
            lblProyecto.Location = new Point(3, 35);
            lblProyecto.Name = "lblProyecto";
            lblProyecto.Size = new Size(70, 20);
            lblProyecto.TabIndex = 18;
            lblProyecto.Text = "Proyecto:";
            // 
            // cmbProyecto
            // 
            cmbProyecto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbProyecto.FormattingEnabled = true;
            cmbProyecto.Location = new Point(83, 31);
            cmbProyecto.Margin = new Padding(5, 10, 10, 10);
            cmbProyecto.Name = "cmbProyecto";
            cmbProyecto.Size = new Size(430, 28);
            cmbProyecto.TabIndex = 19;
            cmbProyecto.SelectedIndexChanged += cmbProyecto_SelectedIndexChanged;
            // 
            // tlpOrganizador
            // 
            tlpOrganizador.ColumnCount = 1;
            tlpOrganizador.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpOrganizador.Controls.Add(pnlBarra, 0, 0);
            tlpOrganizador.Controls.Add(pnlContenido, 0, 1);
            tlpOrganizador.Dock = DockStyle.Fill;
            tlpOrganizador.Location = new Point(0, 0);
            tlpOrganizador.Name = "tlpOrganizador";
            tlpOrganizador.RowCount = 2;
            tlpOrganizador.RowStyles.Add(new RowStyle());
            tlpOrganizador.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpOrganizador.Size = new Size(1070, 753);
            tlpOrganizador.TabIndex = 20;
            // 
            // pnlBarra
            // 
            pnlBarra.Controls.Add(pnlSuperficie);
            pnlBarra.Dock = DockStyle.Fill;
            pnlBarra.Location = new Point(3, 3);
            pnlBarra.Name = "pnlBarra";
            pnlBarra.Size = new Size(1064, 125);
            pnlBarra.TabIndex = 0;
            // 
            // pnlContenido
            // 
            pnlContenido.Controls.Add(tlpContenido);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(3, 134);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(1064, 616);
            pnlContenido.TabIndex = 1;
            // 
            // tlpContenido
            // 
            tlpContenido.ColumnCount = 1;
            tlpContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpContenido.Controls.Add(tlpCont1, 0, 0);
            tlpContenido.Controls.Add(tlpCont2, 0, 1);
            tlpContenido.Controls.Add(pnlGenero, 0, 2);
            tlpContenido.Controls.Add(tlpCont3, 0, 3);
            tlpContenido.Controls.Add(pnlObservaciones, 0, 4);
            tlpContenido.Controls.Add(pnlBotones, 0, 5);
            tlpContenido.Dock = DockStyle.Fill;
            tlpContenido.Location = new Point(0, 0);
            tlpContenido.Name = "tlpContenido";
            tlpContenido.RowCount = 6;
            tlpContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 10.5519476F));
            tlpContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 22.5649357F));
            tlpContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            tlpContenido.Size = new Size(1064, 616);
            tlpContenido.TabIndex = 20;
            tlpContenido.Paint += tlpContenido_Paint;
            // 
            // tlpCont1
            // 
            tlpCont1.ColumnCount = 2;
            tlpCont1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCont1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCont1.Controls.Add(pnlProyecto, 0, 0);
            tlpCont1.Controls.Add(pnlCodigo, 1, 0);
            tlpCont1.Dock = DockStyle.Fill;
            tlpCont1.Location = new Point(3, 3);
            tlpCont1.Name = "tlpCont1";
            tlpCont1.RowCount = 1;
            tlpCont1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCont1.Size = new Size(1058, 96);
            tlpCont1.TabIndex = 3;
            // 
            // pnlProyecto
            // 
            pnlProyecto.Controls.Add(tlpProyectoCont);
            pnlProyecto.Dock = DockStyle.Fill;
            pnlProyecto.Location = new Point(3, 3);
            pnlProyecto.Name = "pnlProyecto";
            pnlProyecto.Size = new Size(523, 90);
            pnlProyecto.TabIndex = 20;
            // 
            // tlpProyectoCont
            // 
            tlpProyectoCont.ColumnCount = 2;
            tlpProyectoCont.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.9139576F));
            tlpProyectoCont.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85.0860443F));
            tlpProyectoCont.Controls.Add(cmbProyecto, 1, 0);
            tlpProyectoCont.Controls.Add(lblProyecto, 0, 0);
            tlpProyectoCont.Dock = DockStyle.Fill;
            tlpProyectoCont.Location = new Point(0, 0);
            tlpProyectoCont.Name = "tlpProyectoCont";
            tlpProyectoCont.RowCount = 1;
            tlpProyectoCont.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpProyectoCont.Size = new Size(523, 90);
            tlpProyectoCont.TabIndex = 20;
            // 
            // pnlCodigo
            // 
            pnlCodigo.Controls.Add(tlpCodigoCont);
            pnlCodigo.Dock = DockStyle.Fill;
            pnlCodigo.Location = new Point(532, 3);
            pnlCodigo.Name = "pnlCodigo";
            pnlCodigo.Size = new Size(523, 90);
            pnlCodigo.TabIndex = 21;
            // 
            // tlpCodigoCont
            // 
            tlpCodigoCont.ColumnCount = 2;
            tlpCodigoCont.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.8852768F));
            tlpCodigoCont.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80.11472F));
            tlpCodigoCont.Controls.Add(txtCodigoBen, 1, 0);
            tlpCodigoCont.Controls.Add(lblCodigoBen, 0, 0);
            tlpCodigoCont.Dock = DockStyle.Fill;
            tlpCodigoCont.Location = new Point(0, 0);
            tlpCodigoCont.Name = "tlpCodigoCont";
            tlpCodigoCont.RowCount = 1;
            tlpCodigoCont.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpCodigoCont.Size = new Size(523, 90);
            tlpCodigoCont.TabIndex = 2;
            // 
            // tlpCont2
            // 
            tlpCont2.ColumnCount = 1;
            tlpCont2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCont2.Controls.Add(pnlNombre, 0, 0);
            tlpCont2.Controls.Add(pnlApellido, 0, 1);
            tlpCont2.Dock = DockStyle.Fill;
            tlpCont2.Location = new Point(3, 105);
            tlpCont2.Name = "tlpCont2";
            tlpCont2.RowCount = 2;
            tlpCont2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpCont2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpCont2.Size = new Size(1058, 96);
            tlpCont2.TabIndex = 4;
            // 
            // pnlNombre
            // 
            pnlNombre.Controls.Add(txtNombres);
            pnlNombre.Controls.Add(lblNombres);
            pnlNombre.Dock = DockStyle.Fill;
            pnlNombre.Location = new Point(3, 3);
            pnlNombre.Name = "pnlNombre";
            pnlNombre.Size = new Size(1052, 42);
            pnlNombre.TabIndex = 0;
            // 
            // lblNombres
            // 
            lblNombres.AutoSize = true;
            lblNombres.Location = new Point(27, 13);
            lblNombres.Margin = new Padding(5, 0, 5, 0);
            lblNombres.Name = "lblNombres";
            lblNombres.Size = new Size(73, 20);
            lblNombres.TabIndex = 2;
            lblNombres.Text = "Nombres:";
            // 
            // pnlApellido
            // 
            pnlApellido.Controls.Add(txtApellidos);
            pnlApellido.Controls.Add(lblApellido);
            pnlApellido.Dock = DockStyle.Fill;
            pnlApellido.Location = new Point(3, 51);
            pnlApellido.Name = "pnlApellido";
            pnlApellido.Size = new Size(1052, 42);
            pnlApellido.TabIndex = 1;
            // 
            // pnlGenero
            // 
            pnlGenero.Controls.Add(cmbGenero);
            pnlGenero.Controls.Add(lblGenero);
            pnlGenero.Dock = DockStyle.Fill;
            pnlGenero.Location = new Point(3, 207);
            pnlGenero.Name = "pnlGenero";
            pnlGenero.Size = new Size(1058, 96);
            pnlGenero.TabIndex = 5;
            // 
            // tlpCont3
            // 
            tlpCont3.ColumnCount = 2;
            tlpCont3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCont3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCont3.Controls.Add(pnlEdad, 1, 0);
            tlpCont3.Controls.Add(pnlFechaNac, 0, 0);
            tlpCont3.Dock = DockStyle.Fill;
            tlpCont3.Location = new Point(3, 309);
            tlpCont3.Name = "tlpCont3";
            tlpCont3.RowCount = 1;
            tlpCont3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCont3.Size = new Size(1058, 59);
            tlpCont3.TabIndex = 9;
            // 
            // pnlEdad
            // 
            pnlEdad.Controls.Add(txtEdad);
            pnlEdad.Controls.Add(lblEdad);
            pnlEdad.Dock = DockStyle.Fill;
            pnlEdad.Location = new Point(532, 3);
            pnlEdad.Name = "pnlEdad";
            pnlEdad.Size = new Size(523, 53);
            pnlEdad.TabIndex = 7;
            // 
            // pnlFechaNac
            // 
            pnlFechaNac.Controls.Add(tableLayoutPanel1);
            pnlFechaNac.Dock = DockStyle.Fill;
            pnlFechaNac.Location = new Point(3, 3);
            pnlFechaNac.Name = "pnlFechaNac";
            pnlFechaNac.Size = new Size(523, 53);
            pnlFechaNac.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(dtpFechaNac, 0, 1);
            tableLayoutPanel1.Controls.Add(lblFechaNac, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 39.6226425F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 60.3773575F));
            tableLayoutPanel1.Size = new Size(523, 53);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // pnlObservaciones
            // 
            pnlObservaciones.Controls.Add(txtObservacion);
            pnlObservaciones.Controls.Add(lblObservacion);
            pnlObservaciones.Dock = DockStyle.Fill;
            pnlObservaciones.Location = new Point(3, 374);
            pnlObservaciones.Name = "pnlObservaciones";
            pnlObservaciones.Size = new Size(1058, 133);
            pnlObservaciones.TabIndex = 8;
            // 
            // pnlBotones
            // 
            pnlBotones.Controls.Add(tlpBotonGuardar);
            pnlBotones.Dock = DockStyle.Fill;
            pnlBotones.Location = new Point(3, 513);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Size = new Size(1058, 100);
            pnlBotones.TabIndex = 10;
            // 
            // tlpBotonGuardar
            // 
            tlpBotonGuardar.ColumnCount = 3;
            tlpBotonGuardar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            tlpBotonGuardar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBotonGuardar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            tlpBotonGuardar.Controls.Add(btnGuardarPaciente, 1, 0);
            tlpBotonGuardar.Dock = DockStyle.Fill;
            tlpBotonGuardar.Location = new Point(0, 0);
            tlpBotonGuardar.Name = "tlpBotonGuardar";
            tlpBotonGuardar.RowCount = 1;
            tlpBotonGuardar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBotonGuardar.Size = new Size(1058, 100);
            tlpBotonGuardar.TabIndex = 0;
            // 
            // btnGuardarPaciente
            // 
            btnGuardarPaciente.Anchor = AnchorStyles.None;
            btnGuardarPaciente.Cursor = Cursors.Hand;
            btnGuardarPaciente.Location = new Point(351, 28);
            btnGuardarPaciente.Margin = new Padding(1);
            btnGuardarPaciente.Name = "btnGuardarPaciente";
            btnGuardarPaciente.Size = new Size(356, 44);
            btnGuardarPaciente.TabIndex = 13;
            btnGuardarPaciente.TabStop = false;
            btnGuardarPaciente.Text = "Guardar";
            btnGuardarPaciente.UseVisualStyleBackColor = true;
            btnGuardarPaciente.Click += btnGuardarPaciente_Click;
            // 
            // pnlContenedor
            // 
            pnlContenedor.Controls.Add(tlpOrganizador);
            pnlContenedor.Dock = DockStyle.Fill;
            pnlContenedor.Location = new Point(0, 0);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1070, 753);
            pnlContenedor.TabIndex = 21;
            // 
            // wAgregarPaciente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1070, 753);
            ControlBox = false;
            Controls.Add(pnlContenedor);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(5, 4, 5, 4);
            Name = "wAgregarPaciente";
            Text = "wAgregarEstudiante";
            Load += wAgregarPaciente_Load;
            tlpOrganizador.ResumeLayout(false);
            pnlBarra.ResumeLayout(false);
            pnlContenido.ResumeLayout(false);
            tlpContenido.ResumeLayout(false);
            tlpCont1.ResumeLayout(false);
            pnlProyecto.ResumeLayout(false);
            tlpProyectoCont.ResumeLayout(false);
            tlpProyectoCont.PerformLayout();
            pnlCodigo.ResumeLayout(false);
            tlpCodigoCont.ResumeLayout(false);
            tlpCodigoCont.PerformLayout();
            tlpCont2.ResumeLayout(false);
            pnlNombre.ResumeLayout(false);
            pnlNombre.PerformLayout();
            pnlApellido.ResumeLayout(false);
            pnlApellido.PerformLayout();
            pnlGenero.ResumeLayout(false);
            pnlGenero.PerformLayout();
            tlpCont3.ResumeLayout(false);
            pnlEdad.ResumeLayout(false);
            pnlEdad.PerformLayout();
            pnlFechaNac.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            pnlObservaciones.ResumeLayout(false);
            pnlObservaciones.PerformLayout();
            pnlBotones.ResumeLayout(false);
            tlpBotonGuardar.ResumeLayout(false);
            pnlContenedor.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblCodigoBen;
        private System.Windows.Forms.TextBox txtCodigoBen;
        private System.Windows.Forms.TextBox txtNombres;
        private System.Windows.Forms.Label lblGenero;
        private System.Windows.Forms.Label lblFechaNac;
        private System.Windows.Forms.DateTimePicker dtpFechaNac;
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.Label lblObservacion;
        private System.Windows.Forms.RichTextBox txtObservacion;
        private System.Windows.Forms.TextBox txtEdad;
        private System.Windows.Forms.ComboBox cmbGenero;
        private TextBox txtApellidos;
        private Label lblApellido;
        private Panel pnlSuperficie;
        private Label lblProyecto;
        private ComboBox cmbProyecto;
        private TableLayoutPanel tlpOrganizador;
        private Panel pnlContenedor;
        private Panel pnlBarra;
        private Panel pnlContenido;
        private TableLayoutPanel tlpContenido;
        private Label lblNombres;
        private TableLayoutPanel tlpCont1;
        private Panel pnlProyecto;
        private Panel pnlCodigo;
        private TableLayoutPanel tlpCont2;
        private Panel pnlNombre;
        private Panel pnlApellido;
        private Panel pnlGenero;
        private Panel pnlFechaNac;
        private Panel pnlEdad;
        private Panel pnlObservaciones;
        private TableLayoutPanel tlpCont3;
        private Panel pnlBotones;
        private TableLayoutPanel tlpProyectoCont;
        private TableLayoutPanel tlpCodigoCont;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnGuardarPaciente;
        private TableLayoutPanel tlpBotonGuardar;
    }
}