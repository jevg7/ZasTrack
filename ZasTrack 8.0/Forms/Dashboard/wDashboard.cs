using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Models;
using ZasTrack.Repositories;

namespace ZasTrack.Forms.Dashboard
{
    public partial class wDashboard : Form
    {
        private PacienteRepository pacienteRepository;
        private ProyectoRepository proyectoRepository;

        public wDashboard()
        {
            pacienteRepository = new PacienteRepository();
            proyectoRepository = new ProyectoRepository();

            InitializeComponent();
        }

        private void wDashboard_Load(object sender, EventArgs e)
        {
            CargarProyectos();
        }

        private void cboProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProyecto.SelectedItem != null)
            {
                // Obtén el objeto Proyecto seleccionado
                Proyecto proyectoSeleccionado = (Proyecto)cmbProyecto.SelectedItem;

                // Accede a la propiedad id_proyecto del objeto Proyecto
                int idProyecto = proyectoSeleccionado.id_proyecto;

                // Obtén el total de pacientes asociados al proyecto seleccionado
                int totalPacientes = pacienteRepository.obtTotalPacientes(idProyecto);

                // Muestra el total de pacientes en la interfaz
                mosTotalPac(totalPacientes);


            }
        }
        private void CargarProyectos()
        {
          // Deshabilitar el evento SelectedIndexChanged
            cmbProyecto.SelectedIndexChanged -= cboProyecto_SelectedIndexChanged;

            // Cargar los proyectos en el ComboBox
            List<Proyecto> proyectos = proyectoRepository.ObtenerProyectos();
            cmbProyecto.DataSource = proyectos;
            cmbProyecto.DisplayMember = "nombre"; // Muestra el nombre del proyecto
            cmbProyecto.ValueMember = "id_proyecto"; // Usa el ID como valor
            cmbProyecto.SelectedIndex = -1; // No seleccionar ningún proyecto por defecto

            // Rehabilitar el evento SelectedIndexChanged
            cmbProyecto.SelectedIndexChanged += cboProyecto_SelectedIndexChanged;

        }

        private void pnlPacientesTotal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlMuestrasDia_Paint(object sender, PaintEventArgs e)
        {

        }

     
        private void lblBienvenido_Click(object sender, EventArgs e)
        {

        }
        private void mosTotalPac(int totalPacientes)
        {
            // Actualiza un control de la interfaz con el total de pacientes
            lblPacientesTotal.Text = $"Total Pacientes: {totalPacientes}";
        }
    }
}
