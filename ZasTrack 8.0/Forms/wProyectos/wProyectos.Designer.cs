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
            pnlProyChildren = new Panel();
            vScrollBar1 = new VScrollBar();
            splProyectos = new SplitContainer();
            pnlProyFather = new Panel();
            pnlProyList = new Panel();
            flpProyList = new FlowLayoutPanel();
            panel1 = new Panel();
            lblAdmProyec = new Label();
            ((System.ComponentModel.ISupportInitialize)splProyectos).BeginInit();
            splProyectos.Panel1.SuspendLayout();
            splProyectos.Panel2.SuspendLayout();
            splProyectos.SuspendLayout();
            pnlProyFather.SuspendLayout();
            pnlProyList.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlProyChildren
            // 
            pnlProyChildren.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlProyChildren.Location = new Point(0, 0);
            pnlProyChildren.Name = "pnlProyChildren";
            pnlProyChildren.Size = new Size(1021, 734);
            pnlProyChildren.TabIndex = 1;
            pnlProyChildren.Paint += pnlProyectos_Paint;
            // 
            // vScrollBar1
            // 
            vScrollBar1.Location = new Point(319, 0);
            vScrollBar1.Name = "vScrollBar1";
            vScrollBar1.Size = new Size(22, 867);
            vScrollBar1.TabIndex = 0;
            vScrollBar1.Scroll += vScrollBar1_Scroll;
            // 
            // splProyectos
            // 
            splProyectos.Dock = DockStyle.Bottom;
            splProyectos.Location = new Point(0, 34);
            splProyectos.Name = "splProyectos";
            // 
            // splProyectos.Panel1
            // 
            splProyectos.Panel1.BackColor = SystemColors.ActiveCaption;
            splProyectos.Panel1.Controls.Add(pnlProyFather);
            splProyectos.Panel1.Paint += splitContainer1_Panel1_Paint_2;
            // 
            // splProyectos.Panel2
            // 
            splProyectos.Panel2.Controls.Add(pnlProyChildren);
            splProyectos.Size = new Size(1366, 734);
            splProyectos.SplitterDistance = 341;
            splProyectos.TabIndex = 2;
            // 
            // pnlProyFather
            // 
            pnlProyFather.Controls.Add(pnlProyList);
            pnlProyFather.Dock = DockStyle.Fill;
            pnlProyFather.Location = new Point(0, 0);
            pnlProyFather.Name = "pnlProyFather";
            pnlProyFather.Size = new Size(341, 734);
            pnlProyFather.TabIndex = 1;
            // 
            // pnlProyList
            // 
            pnlProyList.BackColor = SystemColors.ActiveCaption;
            pnlProyList.Controls.Add(vScrollBar1);
            pnlProyList.Controls.Add(flpProyList);
            pnlProyList.Dock = DockStyle.Fill;
            pnlProyList.Location = new Point(0, 0);
            pnlProyList.Name = "pnlProyList";
            pnlProyList.Size = new Size(341, 734);
            pnlProyList.TabIndex = 3;
            // 
            // flpProyList
            // 
            flpProyList.Location = new Point(0, 3);
            flpProyList.Name = "flpProyList";
            flpProyList.Size = new Size(341, 731);
            flpProyList.TabIndex = 0;
            flpProyList.Paint += flpProyList_Paint;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblAdmProyec);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1366, 28);
            panel1.TabIndex = 3;
            panel1.Paint += panel1_Paint;
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
            // wProyectos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1366, 768);
            Controls.Add(panel1);
            Controls.Add(splProyectos);
            FormBorderStyle = FormBorderStyle.None;
            Name = "wProyectos";
            Text = "Proyectos";
            Load += wProyectos_Load;
            splProyectos.Panel1.ResumeLayout(false);
            splProyectos.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splProyectos).EndInit();
            splProyectos.ResumeLayout(false);
            pnlProyFather.ResumeLayout(false);
            pnlProyList.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlProyChildren;
        private SplitContainer splProyectos;
        private Panel panel1;
        private Panel pnlProyFather;
        private Label lblAdmProyec;
        private Panel pnlProyList;
        private FlowLayoutPanel flpProyList;
        private VScrollBar vScrollBar1;
    }
}