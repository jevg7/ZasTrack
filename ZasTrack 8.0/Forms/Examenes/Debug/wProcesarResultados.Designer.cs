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
            btnCancelar = new Button();
            btnGuardarResultados = new Button();
            tabControlExamenes = new TabControl();
            pnlBotones.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBotones
            // 
            pnlBotones.Controls.Add(btnCancelar);
            pnlBotones.Controls.Add(btnGuardarResultados);
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Location = new Point(0, 511);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Size = new Size(984, 50);
            pnlBotones.TabIndex = 0;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.CausesValidation = false;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(448, 10);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(135, 32);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "&Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardarResultados
            // 
            btnGuardarResultados.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGuardarResultados.Location = new Point(618, 10);
            btnGuardarResultados.Name = "btnGuardarResultados";
            btnGuardarResultados.Size = new Size(354, 32);
            btnGuardarResultados.TabIndex = 0;
            btnGuardarResultados.Text = "&Guardar Resultados";
            btnGuardarResultados.UseVisualStyleBackColor = true;
            btnGuardarResultados.Click += btnGuardarResultados_Click;
            // 
            // tabControlExamenes
            // 
            tabControlExamenes.Dock = DockStyle.Fill;
            tabControlExamenes.Location = new Point(0, 0);
            tabControlExamenes.Name = "tabControlExamenes";
            tabControlExamenes.SelectedIndex = 0;
            tabControlExamenes.Size = new Size(984, 511);
            tabControlExamenes.TabIndex = 1;
            tabControlExamenes.SelectedIndexChanged += tabControlExamenes_SelectedIndexChanged;
            // 
            // wProcesarResultados
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(tabControlExamenes);
            Controls.Add(pnlBotones);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "wProcesarResultados";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ingresar/Ver Resultados de Muestra";
            pnlBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBotones;
        private Button btnCancelar;
        private Button btnGuardarResultados;
        private TabControl tabControlExamenes;
    }
}