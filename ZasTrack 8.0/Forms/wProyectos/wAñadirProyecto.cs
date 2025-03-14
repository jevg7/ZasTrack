using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Repositories;

namespace ZasTrack.Forms
{
    public partial class wAñadirProyecto : Form
    {
        public wAñadirProyecto()
        {
            InitializeComponent();
        }

        private void wAñadirProyecto_Load(object sender, EventArgs e)
        {

        }
        private void btnGuardarProyecto_Click(object sender, EventArgs e)
        {
            var proyecto = new Models.Poyecto
            {
                nombre = txtNombreProyecto.Text,
                fecha_inicio = dtpFechaFin.Value,
                fecha_fin = dtpFechaFin.Checked ? dtpFechaFin.Value : (DateTime?)null
            };

            var proyectoRepository = new ProyectoRepository();
            proyectoRepository.GuardarProyecto(proyecto);

            MessageBox.Show("Proyecto guardado correctamente.");
        }
    }
}
