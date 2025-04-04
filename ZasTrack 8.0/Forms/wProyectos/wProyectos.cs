using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Models;
using ZasTrack.Forms;
using ZasTrack.Repositories;

namespace ZasTrack.Forms.wProyectos
{
    public partial class wProyectos : Form
    {
        private ProyectoRepository proyectoRepository; // Declarar la variable

        public wProyectos()
        {
            InitializeComponent();
            proyectoRepository = new ProyectoRepository(); // Inicializar el repositorio
        }

        private void tsmiAñadirProyectos_Click(object sender, EventArgs e)
        {

        }

        private void wProyectos_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pnlProyectos_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnAggProyect_Click1(object sender, EventArgs e)
        {
            // Abrir el formulario wAñadirProyecto como un diálogo
            using (var form = new wAñadirProyecto()) ;

        }

        private void splitContainer1_Panel1_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlAggProy_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblAdmProyec_Click(object sender, EventArgs e)
        {

        }

        private void Abrir_Form(object formhijo)
        {

            if (this.pnlContenedor.Controls.Count > 0)
                this.pnlContenedor.Controls.RemoveAt(0); // Elimina cualquier control existente del panel contenedor.

            Form fh = formhijo as Form; // Convierte el objeto de entrada en un formulario.
            fh.TopLevel = false; // Establece la propiedad TopLevel del formulario como false.
            fh.Dock = DockStyle.Fill; // Establece la propiedad Dock del formulario para que ocupe todo el espacio del panel contenedor.
            this.pnlContenedor.Controls.Add(fh); // Agrega el formulario al panel contenedor.
            this.pnlContenedor.Tag = fh; // Establece la propiedad Tag del panel contenedor como el formulario.
            fh.Show(); // Muestra el formulario.
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void flpProyList_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pnlProyList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlProyFather_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splProyectos_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flpProyList_Paint_1(object sender, PaintEventArgs e)
        {

        }


        private void pnlProyChildren_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flpProyList_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void agregarProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir_Form(new Forms.wAñadirProyecto());

        }

        private void editarProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir_Form(new Forms.wEditarProyecto());
        }

        private void verProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir_Form(new Forms.wProyectos.wVerProyecto());
        }

        private void eliminarProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir_Form(new Forms.wEliminarProyecto());
        }

        private void pnlContenedor_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}


    



