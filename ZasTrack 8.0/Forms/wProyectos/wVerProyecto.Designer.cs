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
            vScrollBar1 = new VScrollBar();
            pnlCargando = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            vScrollBar2 = new VScrollBar();
            flowLayoutPanel2 = new FlowLayoutPanel();
            vScrollBar3 = new VScrollBar();
            flpProyList.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // flpProyList
            // 
            flpProyList.Controls.Add(vScrollBar1);
            flpProyList.Controls.Add(pnlCargando);
            flpProyList.Controls.Add(flowLayoutPanel1);
            flpProyList.Dock = DockStyle.Fill;
            flpProyList.Location = new Point(0, 0);
            flpProyList.Margin = new Padding(3, 4, 3, 4);
            flpProyList.Name = "flpProyList";
            flpProyList.Size = new Size(1543, 977);
            flpProyList.TabIndex = 8;
            flpProyList.Paint += flpProyList_Paint;
            // 
            // vScrollBar1
            // 
            vScrollBar1.Location = new Point(0, 0);
            vScrollBar1.Name = "vScrollBar1";
            vScrollBar1.Size = new Size(22, 1156);
            vScrollBar1.TabIndex = 4;
            // 
            // pnlCargando
            // 
            pnlCargando.Location = new Point(25, 3);
            pnlCargando.Name = "pnlCargando";
            pnlCargando.Size = new Size(269, 81);
            pnlCargando.TabIndex = 9;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(vScrollBar2);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel2);
            flowLayoutPanel1.Location = new Point(3, 1160);
            flowLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1561, 955);
            flowLayoutPanel1.TabIndex = 8;
            // 
            // vScrollBar2
            // 
            vScrollBar2.Location = new Point(0, 0);
            vScrollBar2.Name = "vScrollBar2";
            vScrollBar2.Size = new Size(22, 1156);
            vScrollBar2.TabIndex = 4;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(vScrollBar3);
            flowLayoutPanel2.Location = new Point(3, 1160);
            flowLayoutPanel2.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(1561, 955);
            flowLayoutPanel2.TabIndex = 9;
            // 
            // vScrollBar3
            // 
            vScrollBar3.Location = new Point(0, 0);
            vScrollBar3.Name = "vScrollBar3";
            vScrollBar3.Size = new Size(22, 1156);
            vScrollBar3.TabIndex = 4;
            // 
            // wVerProyecto
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1543, 977);
            Controls.Add(flpProyList);
            FormBorderStyle = FormBorderStyle.None;
            Name = "wVerProyecto";
            Text = "Ver Proyectos";
            flpProyList.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flpProyList;
        private VScrollBar vScrollBar1;
        private FlowLayoutPanel flowLayoutPanel1;
        private VScrollBar vScrollBar2;
        private FlowLayoutPanel flowLayoutPanel2;
        private VScrollBar vScrollBar3;
        private Panel pnlCargando;
    }
}