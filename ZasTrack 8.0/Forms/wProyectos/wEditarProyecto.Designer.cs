namespace ZasTrack.Forms.wProyectos
{
    partial class wEditarProyecto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wEditarProyecto));
            txtNombreProyecto = new TextBox();
            txtCodigo = new TextBox();
            dtpFechaInicio = new DateTimePicker();
            lblCodigoProyecto = new Label();
            lblNombreProy = new Label();
            btnGuardarCambios = new Button();
            lblFechaInicio = new Label();
            pnlEditarProyecto = new Panel();
            btnCancelar = new Button();
            pnlEditarProyecto.SuspendLayout();
            SuspendLayout();
            // 
            // txtNombreProyecto
            // 
            txtNombreProyecto.Anchor = AnchorStyles.None;
            txtNombreProyecto.Location = new Point(256, 114);
            txtNombreProyecto.Margin = new Padding(5, 4, 5, 4);
            txtNombreProyecto.Name = "txtNombreProyecto";
            txtNombreProyecto.Size = new Size(271, 27);
            txtNombreProyecto.TabIndex = 12;
            // 
            // txtCodigo
            // 
            txtCodigo.Anchor = AnchorStyles.None;
            txtCodigo.ForeColor = Color.Black;
            txtCodigo.Location = new Point(256, 181);
            txtCodigo.Margin = new Padding(5, 4, 5, 4);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(271, 27);
            txtCodigo.TabIndex = 14;
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Anchor = AnchorStyles.None;
            dtpFechaInicio.Location = new Point(254, 239);
            dtpFechaInicio.Margin = new Padding(5, 4, 5, 4);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(271, 27);
            dtpFechaInicio.TabIndex = 15;
            // 
            // lblCodigoProyecto
            // 
            lblCodigoProyecto.Anchor = AnchorStyles.None;
            lblCodigoProyecto.AutoSize = true;
            lblCodigoProyecto.Location = new Point(325, 157);
            lblCodigoProyecto.Margin = new Padding(5, 0, 5, 0);
            lblCodigoProyecto.Name = "lblCodigoProyecto";
            lblCodigoProyecto.Size = new Size(148, 20);
            lblCodigoProyecto.TabIndex = 13;
            lblCodigoProyecto.Text = "Codigo del Proyecto:";
            // 
            // lblNombreProy
            // 
            lblNombreProy.Anchor = AnchorStyles.None;
            lblNombreProy.AutoSize = true;
            lblNombreProy.Location = new Point(325, 90);
            lblNombreProy.Margin = new Padding(5, 0, 5, 0);
            lblNombreProy.Name = "lblNombreProy";
            lblNombreProy.Size = new Size(154, 20);
            lblNombreProy.TabIndex = 11;
            lblNombreProy.Text = "Nombre del Proyecto:";
            // 
            // btnGuardarCambios
            // 
            btnGuardarCambios.Anchor = AnchorStyles.None;
            btnGuardarCambios.Cursor = Cursors.Hand;
            btnGuardarCambios.Location = new Point(305, 288);
            btnGuardarCambios.Margin = new Padding(5, 4, 5, 4);
            btnGuardarCambios.Name = "btnGuardarCambios";
            btnGuardarCambios.Size = new Size(168, 43);
            btnGuardarCambios.TabIndex = 16;
            btnGuardarCambios.Text = "Guardar Cambios";
            btnGuardarCambios.UseVisualStyleBackColor = true;
            btnGuardarCambios.Click += btnGuardarCambios_Click;
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.Anchor = AnchorStyles.None;
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Location = new Point(134, 242);
            lblFechaInicio.Margin = new Padding(5, 0, 5, 0);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(111, 20);
            lblFechaInicio.TabIndex = 17;
            lblFechaInicio.Text = "Fecha de Inicio:";
            // 
            // pnlEditarProyecto
            // 
            pnlEditarProyecto.Controls.Add(btnCancelar);
            pnlEditarProyecto.Controls.Add(lblCodigoProyecto);
            pnlEditarProyecto.Controls.Add(txtNombreProyecto);
            pnlEditarProyecto.Controls.Add(lblFechaInicio);
            pnlEditarProyecto.Controls.Add(txtCodigo);
            pnlEditarProyecto.Controls.Add(btnGuardarCambios);
            pnlEditarProyecto.Controls.Add(dtpFechaInicio);
            pnlEditarProyecto.Controls.Add(lblNombreProy);
            pnlEditarProyecto.Dock = DockStyle.Fill;
            pnlEditarProyecto.Location = new Point(0, 0);
            pnlEditarProyecto.Name = "pnlEditarProyecto";
            pnlEditarProyecto.Size = new Size(800, 450);
            pnlEditarProyecto.TabIndex = 18;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.None;
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.Location = new Point(618, 394);
            btnCancelar.Margin = new Padding(5, 4, 5, 4);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(168, 43);
            btnCancelar.TabIndex = 18;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // wEditarProyecto
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlEditarProyecto);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "wEditarProyecto";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Editar Proyecto";
            pnlEditarProyecto.ResumeLayout(false);
            pnlEditarProyecto.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtNombreProyecto;
        private TextBox txtCodigo;
        private DateTimePicker dtpFechaInicio;
        private Label lblCodigoProyecto;
        private Label lblNombreProy;
        private Button btnGuardarCambios;
        private Label lblFechaInicio;
        private Panel pnlEditarProyecto;
        private Button btnCancelar;
    }
}