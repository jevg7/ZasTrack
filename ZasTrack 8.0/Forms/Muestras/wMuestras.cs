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

namespace ZasTrack.Forms.Muestras
{
    public partial class wMuestras : Form
    {
       
        private PacienteRepository pacienteRepository;
        private MuestraRepository muestraRepository;
        private ProyectoRepository proyectoRepository;
        private MuestraExamenRepository muestraExamenRepository;
        private int ultimoProyectoSeleccionado = -1;

        public wMuestras()
        {
            // Asegúrate de pasar la cadena de conexión correcta al constructor
            string connectionString = "YourConnectionStringHere"; // Reemplaza con tu cadena de conexión real
            muestraRepository = new MuestraRepository();
            pacienteRepository = new PacienteRepository();
            proyectoRepository = new ProyectoRepository();
            muestraExamenRepository = new MuestraExamenRepository(connectionString);
            InitializeComponent();
        }

        private void wMuestras_Load(object sender, EventArgs e)
        {
            CargarProyectos();
            fechaLock();
        }
       
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            if (!chkOrina.Checked && !chkHeces.Checked && !chkSangre.Checked)
            {
                MessageBox.Show("Debe seleccionar al menos un tipo de examen.");
                return;
            }

            guardarMuestra();
        }

        private void guardarMuestra()
        {
            if (string.IsNullOrWhiteSpace(txtFecha.Text) || string.IsNullOrWhiteSpace(txtPaciente.Text) || string.IsNullOrWhiteSpace(txtMuestrasId.Text))
            {
                MessageBox.Show("Por favor, llene todos los campos");
                return;
            }

            int idTipoExamen = 0;
            if (chkOrina.Checked)
            {
                idTipoExamen = 1; // ID para orina
            }
            else if (chkHeces.Checked)
            {
                idTipoExamen = 2; // ID para heces
            }
            else if (chkSangre.Checked)
            {
                idTipoExamen = 3; // ID para sangre
            }

            Muestra muestra = new Muestra()
            {
                IdProyecto = Convert.ToInt32(cmbProyecto.SelectedValue),
                IdPaciente = Convert.ToInt32(txtIdPaciente.Text), // Usar el Id del paciente
                IdTipoExamen = idTipoExamen,
                NumeroMuestra = Convert.ToInt32(txtMuestrasId.Text),
                FechaRecepcion = DateTime.Now
            };

            muestraRepository.GuardarMuestras(muestra);
            MessageBox.Show("Muestra guardada correctamente");
        }       
        private void cmbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProyecto.SelectedValue != null)
            {
                int idProyecto = (cmbProyecto.SelectedItem as Proyecto)?.id_proyecto ?? -1;
                ultimoProyectoSeleccionado = idProyecto;
                txtMuestrasId.Text = (muestraRepository.ObtenerUltimaMuestra(idProyecto, DateTime.Now) + 1).ToString();
            }
        }
        private void txtFecha_TextChanged_1(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFecha.Enabled = false;
        }
        private void txtMuestrasId_TextChanged(object sender, EventArgs e)
        {
            txtMuestrasId.Enabled = false;
        }      

        private void fechaLock()
        {
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFecha.Enabled = false;
            txtFecha.Font = new Font("Arial", 12, FontStyle.Regular);
        }
        private void CargarProyectos()
        {
            List<Proyecto> proyectos = proyectoRepository.ObtenerProyectos();
            cmbProyecto.DataSource = proyectos;
            cmbProyecto.DisplayMember = "nombre";
            cmbProyecto.ValueMember = "id_proyecto";
            cmbProyecto.SelectedIndex = -1;
        }
        private void LimpiarCampos(bool limpiarProyecto = true)
        {
            txtPaciente.Clear();
            chkOrina.Checked = false;
            chkHeces.Checked = false;
            chkSangre.Checked = false;
            txtMuestrasId.Clear();

            if (!limpiarProyecto && ultimoProyectoSeleccionado != -1)
            {
                cmbProyecto.SelectedValue = ultimoProyectoSeleccionado;
                txtMuestrasId.Text = (muestraRepository.ObtenerUltimaMuestra(ultimoProyectoSeleccionado, DateTime.Now) + 1).ToString();
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim();
            int idProyecto = (cmbProyecto.SelectedItem as Proyecto)?.id_proyecto ?? -1;
            List<pacientes> pacientes = pacienteRepository.BuscarPacientes(criterio, idProyecto);

            if (pacientes.Count == 0)
            {
                MessageBox.Show("No se encontraron pacientes.");
            }
            else if (pacientes.Count > 1)
            {
                MessageBox.Show("Hay múltiples pacientes con ese nombre. Intente buscar con el código de beneficiario.");
            }
            else
            {
                // Mostrar el nombre en el campo visible
                txtPaciente.Text = pacientes[0].nombres + " " + pacientes[0].apellidos;

                // Guardar el id del paciente en el campo oculto
                txtIdPaciente.Text = pacientes[0].id_paciente.ToString();
                txtIdPaciente.Visible = false; // Ocultar el campo de id paciente
                txtPaciente.Enabled = false;

            }
        }

        #region useles ahh shi
        private void txtIdPaciente_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscarPaciente_Click(object sender, EventArgs e)
        {

        }

        private void chkSangre_CheckedChanged_1(object sender, EventArgs e)
        {

        }
        private void chkHeces_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void chkOrina_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void chkSangre_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pnlProyecto_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtFecha_TextChanged(object sender, EventArgs e)
        {

        }
        private void cklExamenes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

    }
}