using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Forms.Estudiantes;
using ZasTrack.Forms.Pacientes;

namespace ZasTrack
{
    public partial class wPaciente : Form
    {
        public wPaciente()
        {
            InitializeComponent();
        }

        private void agregarEstudiantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir_Form(new wAgregarPaciente());
        }


        private void Abrir_Form(object formhijo)
        {

            if (this.pnlCntEstudiantes.Controls.Count > 0)
                this.pnlCntEstudiantes.Controls.RemoveAt(0); // Elimina cualquier control existente del panel contenedor.

            Form fh = formhijo as Form; // Convierte el objeto de entrada en un formulario.
            fh.TopLevel = false; // Establece la propiedad TopLevel del formulario como false.
            fh.Dock = DockStyle.Fill; // Establece la propiedad Dock del formulario para que ocupe todo el espacio del panel contenedor.
            this.pnlCntEstudiantes.Controls.Add(fh); // Agrega el formulario al panel contenedor.
            this.pnlCntEstudiantes.Tag = fh; // Establece la propiedad Tag del panel contenedor como el formulario.
            fh.Show(); // Muestra el formulario.
        }


        private void editarEliminarEstudiantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir_Form(new wEditarEliminarPaciente());
        }

        private void verEstudiantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir_Form(new wVerPaciente());
        }
        #region Windows Forms Designer generated code
        private void wPaciente_Load(object sender, EventArgs e)
        {

        }
        private void pnlCntEstudiantes_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion

        private void importarPacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir_Form(new wImportarPacientes());
        }
    }
}
