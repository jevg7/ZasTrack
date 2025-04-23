namespace ZasTrack.Forms.wProyectos
{
    partial class wVerProyecto
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
            flpProyList = new FlowLayoutPanel();
            pnlCargando = new Panel();
            pnlBotones = new Panel();
            btnVerArchivados = new Button();
            btnVerActivos = new Button();
            pnlBotones.SuspendLayout();
            SuspendLayout();
            // 
            // flpProyList
            // 
            flpProyList.AutoScroll = true;
            flpProyList.FlowDirection = FlowDirection.TopDown;
            flpProyList.Location = new Point(0, 74);
            flpProyList.Margin = new Padding(3, 4, 3, 4);
            flpProyList.Name = "flpProyList";
            flpProyList.Size = new Size(1245, 672);
            flpProyList.TabIndex = 8;
            flpProyList.WrapContents = false;
            flpProyList.Paint += flpProyList_Paint;
            // 
            // pnlCargando
            // 
            pnlCargando.Location = new Point(340, 3);
            pnlCargando.Name = "pnlCargando";
            pnlCargando.Size = new Size(290, 51);
            pnlCargando.TabIndex = 9;
            // 
            // pnlBotones
            // 
            pnlBotones.Controls.Add(btnVerArchivados);
            pnlBotones.Controls.Add(btnVerActivos);
            pnlBotones.Location = new Point(0, 0);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Size = new Size(334, 67);
            pnlBotones.TabIndex = 10;
            // 
            // btnVerArchivados
            // 
            btnVerArchivados.Location = new Point(173, 3);
            btnVerArchivados.Name = "btnVerArchivados";
            btnVerArchivados.Size = new Size(155, 51);
            btnVerArchivados.TabIndex = 1;
            btnVerArchivados.Text = "Archivados";
            btnVerArchivados.UseVisualStyleBackColor = true;
            btnVerArchivados.Click += btnVerArchivados_Click;
            // 
            // btnVerActivos
            // 
            btnVerActivos.Location = new Point(12, 3);
            btnVerActivos.Name = "btnVerActivos";
            btnVerActivos.Size = new Size(155, 51);
            btnVerActivos.TabIndex = 0;
            btnVerActivos.Text = "Activos";
            btnVerActivos.UseVisualStyleBackColor = true;
            btnVerActivos.Click += btnVerActivos_Click;
            // 
            // wVerProyecto
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1275, 747);
            Controls.Add(pnlBotones);
            Controls.Add(flpProyList);
            Controls.Add(pnlCargando);
            FormBorderStyle = FormBorderStyle.None;
            Name = "wVerProyecto";
            Text = "Ver Proyectos";
            Load += wVerProyecto_Load;
            pnlBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flpProyList;
        private Panel pnlCargando;
        private Panel pnlBotones;
        private Button btnVerArchivados;
        private Button btnVerActivos;
    }
}