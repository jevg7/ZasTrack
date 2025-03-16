namespace ZasTrack.Forms.wProyectos
{
    partial class wProyectos
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
            panel1 = new Panel();
            lblAdmProyec = new Label();
            flpProyList = new FlowLayoutPanel();
            vScrollBar1 = new VScrollBar();
            panel1.SuspendLayout();
            flpProyList.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(lblAdmProyec);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1366, 28);
            panel1.TabIndex = 3;
            panel1.Paint += splitContainer1_Panel1_Paint_2;
            // 
            // lblAdmProyec
            // 
            lblAdmProyec.AutoSize = true;
            lblAdmProyec.Location = new Point(12, 9);
            lblAdmProyec.Name = "lblAdmProyec";
            lblAdmProyec.Size = new Size(154, 15);
            lblAdmProyec.TabIndex = 0;
            lblAdmProyec.Text = "Administrador de Proyectos";
            lblAdmProyec.Click += lblAdmProyec_Click;
            // 
            // flpProyList
            // 
            flpProyList.Controls.Add(vScrollBar1);
            flpProyList.Dock = DockStyle.Fill;
            flpProyList.Location = new Point(0, 28);
            flpProyList.Name = "flpProyList";
            flpProyList.Size = new Size(1366, 740);
            flpProyList.TabIndex = 7;
            // 
            // vScrollBar1
            // 
            vScrollBar1.Location = new Point(0, 0);
            vScrollBar1.Name = "vScrollBar1";
            vScrollBar1.Size = new Size(22, 867);
            vScrollBar1.TabIndex = 4;
            // 
            // wProyectos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1366, 768);
            Controls.Add(flpProyList);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "wProyectos";
            Text = "Proyectos";
            Load += wProyectos_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            flpProyList.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Label lblAdmProyec;
        private FlowLayoutPanel flpProyList;
        private VScrollBar vScrollBar1;
    }
}