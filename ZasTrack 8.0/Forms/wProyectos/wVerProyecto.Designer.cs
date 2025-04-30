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
            chkModoAdmin = new CheckBox();
            btnVerArchivados = new Button();
            btnVerActivos = new Button();
            tlpVerProyectos = new TableLayoutPanel();
            pnlBotones.SuspendLayout();
            tlpVerProyectos.SuspendLayout();
            SuspendLayout();
            // 
            // flpProyList
            // 
            flpProyList.AutoScroll = true;
            flpProyList.Dock = DockStyle.Fill;
            flpProyList.FlowDirection = FlowDirection.TopDown;
            flpProyList.Location = new Point(3, 77);
            flpProyList.Margin = new Padding(3, 4, 3, 4);
            flpProyList.Name = "flpProyList";
            flpProyList.Size = new Size(1000, 577);
            flpProyList.TabIndex = 8;
            flpProyList.WrapContents = false;
            // 
            // pnlCargando
            // 
            pnlCargando.Location = new Point(346, 3);
            pnlCargando.Name = "pnlCargando";
            pnlCargando.Size = new Size(299, 51);
            pnlCargando.TabIndex = 9;
            // 
            // pnlBotones
            // 
            pnlBotones.Controls.Add(chkModoAdmin);
            pnlBotones.Controls.Add(btnVerArchivados);
            pnlBotones.Controls.Add(btnVerActivos);
            pnlBotones.Controls.Add(pnlCargando);
            pnlBotones.Dock = DockStyle.Top;
            pnlBotones.Location = new Point(3, 3);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Size = new Size(1000, 67);
            pnlBotones.TabIndex = 10;
            // 
            // chkModoAdmin
            // 
            chkModoAdmin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkModoAdmin.AutoSize = true;
            chkModoAdmin.Location = new Point(872, 17);
            chkModoAdmin.Name = "chkModoAdmin";
            chkModoAdmin.Size = new Size(119, 24);
            chkModoAdmin.TabIndex = 10;
            chkModoAdmin.Text = "Modo Admin";
            chkModoAdmin.UseVisualStyleBackColor = true;
            chkModoAdmin.CheckedChanged += chkModoAdmin_CheckedChanged;
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
            // tlpVerProyectos
            // 
            tlpVerProyectos.ColumnCount = 1;
            tlpVerProyectos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpVerProyectos.Controls.Add(flpProyList, 0, 1);
            tlpVerProyectos.Controls.Add(pnlBotones, 0, 0);
            tlpVerProyectos.Dock = DockStyle.Fill;
            tlpVerProyectos.Location = new Point(0, 0);
            tlpVerProyectos.Name = "tlpVerProyectos";
            tlpVerProyectos.RowCount = 2;
            tlpVerProyectos.RowStyles.Add(new RowStyle());
            tlpVerProyectos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpVerProyectos.Size = new Size(1006, 658);
            tlpVerProyectos.TabIndex = 11;
            // 
            // wVerProyecto
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1006, 658);
            Controls.Add(tlpVerProyectos);
            FormBorderStyle = FormBorderStyle.None;
            Name = "wVerProyecto";
            Text = "Ver Proyectos";
            pnlBotones.ResumeLayout(false);
            pnlBotones.PerformLayout();
            tlpVerProyectos.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flpProyList;
        private Panel pnlCargando;
        private Panel pnlBotones;
        private Button btnVerArchivados;
        private Button btnVerActivos;
        private TableLayoutPanel tlpVerProyectos;
        private CheckBox chkModoAdmin;
    }
}