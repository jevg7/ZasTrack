namespace ZasTrack.Forms.Examenes.Debug
{
    partial class wProcesarResultados
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
            pnlBotones = new Panel();
            lblModoEdicion = new Label();
            btnHabilitarEdicion = new Button();
            btnGuardarResultados = new Button();
            btnCancelar = new Button();
            btnGuardarActual = new Button();
            tabControlExamenes = new TabControl();
            pnlBotones.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBotones
            // 
            pnlBotones.Controls.Add(lblModoEdicion);
            pnlBotones.Controls.Add(btnHabilitarEdicion);
            pnlBotones.Controls.Add(btnGuardarResultados);
            pnlBotones.Controls.Add(btnCancelar);
            pnlBotones.Controls.Add(btnGuardarActual);
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Location = new Point(0, 681);
            pnlBotones.Margin = new Padding(3, 4, 3, 4);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Size = new Size(1125, 67);
            pnlBotones.TabIndex = 0;
            // 
            // lblModoEdicion
            // 
            lblModoEdicion.AutoSize = true;
            lblModoEdicion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblModoEdicion.ForeColor = Color.Red;
            lblModoEdicion.Location = new Point(12, 22);
            lblModoEdicion.Name = "lblModoEdicion";
            lblModoEdicion.Size = new Size(449, 23);
            lblModoEdicion.TabIndex = 13;
            lblModoEdicion.Text = "MODO EDICION - Revise los cambios antes de guardar.";
            lblModoEdicion.Visible = false;
            // 
            // btnHabilitarEdicion
            // 
            btnHabilitarEdicion.Location = new Point(293, 12);
            btnHabilitarEdicion.Name = "btnHabilitarEdicion";
            btnHabilitarEdicion.Size = new Size(160, 43);
            btnHabilitarEdicion.TabIndex = 3;
            btnHabilitarEdicion.Text = " &Habilitar Edición";
            btnHabilitarEdicion.UseVisualStyleBackColor = true;
            btnHabilitarEdicion.Click += btnHabilitarEdicion_Click;
            // 
            // btnGuardarResultados
            // 
            btnGuardarResultados.Location = new Point(798, 13);
            btnGuardarResultados.Margin = new Padding(3, 4, 3, 4);
            btnGuardarResultados.Name = "btnGuardarResultados";
            btnGuardarResultados.Size = new Size(313, 43);
            btnGuardarResultados.TabIndex = 2;
            btnGuardarResultados.Text = "Guardar Todo y Cerrar";
            btnGuardarResultados.UseVisualStyleBackColor = true;
            btnGuardarResultados.Click += btnGuardarResultados_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.CausesValidation = false;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(459, 13);
            btnCancelar.Margin = new Padding(3, 4, 3, 4);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(154, 43);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "&Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardarActual
            // 
            btnGuardarActual.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGuardarActual.Location = new Point(619, 13);
            btnGuardarActual.Margin = new Padding(3, 4, 3, 4);
            btnGuardarActual.Name = "btnGuardarActual";
            btnGuardarActual.Size = new Size(173, 43);
            btnGuardarActual.TabIndex = 0;
            btnGuardarActual.Text = "Guardar Actual";
            btnGuardarActual.UseVisualStyleBackColor = true;
            btnGuardarActual.Click += btnGuardarActual_Click;
            // 
            // tabControlExamenes
            // 
            tabControlExamenes.Dock = DockStyle.Fill;
            tabControlExamenes.Location = new Point(0, 0);
            tabControlExamenes.Margin = new Padding(3, 4, 3, 4);
            tabControlExamenes.Name = "tabControlExamenes";
            tabControlExamenes.SelectedIndex = 0;
            tabControlExamenes.Size = new Size(1125, 748);
            tabControlExamenes.TabIndex = 1;
            tabControlExamenes.SelectedIndexChanged += tabControlExamenes_SelectedIndexChanged;
            // 
            // wProcesarResultados
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1125, 748);
            Controls.Add(pnlBotones);
            Controls.Add(tabControlExamenes);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "wProcesarResultados";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ingresar/Ver Resultados de Muestra";
            pnlBotones.ResumeLayout(false);
            pnlBotones.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBotones;
        private Button btnCancelar;
        private Button btnGuardarActual;
        private TabControl tabControlExamenes;
        private Button btnGuardarResultados;
        private Button btnHabilitarEdicion;
        private Label lblProyecto;
        private Label lblModoEdicion;
    }
}