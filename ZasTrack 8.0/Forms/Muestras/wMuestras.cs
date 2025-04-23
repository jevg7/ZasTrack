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
        private int ultimoProyectoSeleccionado = -1;
        private MuestraExamenRepository muestraExamenRepository;

        public wMuestras()
        {
            // Asegúrate de pasar la cadena de conexión correcta al constructor
            string connectionString = "YourConnectionStringHere"; // Reemplaza con tu cadena de conexión real
            muestraRepository = new MuestraRepository();
            pacienteRepository = new PacienteRepository();
            proyectoRepository = new ProyectoRepository();
            muestraExamenRepository = new MuestraExamenRepository();

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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
           BuscarPac();
        }
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter en el cuadro
            if (e.KeyCode == Keys.Enter)
            {
               BuscarPac();
                e.SuppressKeyPress = true;
            }
        }

        #region Metodos
        private void BuscarPac()
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

        // En wMuestras.cs

        private void guardarMuestra()
        {
            // Validación campos básicos
            if (string.IsNullOrWhiteSpace(txtFecha.Text) || string.IsNullOrWhiteSpace(txtPaciente.Text) /* || string.IsNullOrWhiteSpace(txtMuestrasId.Text) */ )
            {
                // CORREGIDO: MessageBoxIcon.Warning
                MessageBox.Show("Por favor, busque un paciente y asegúrese de que el proyecto esté seleccionado.", "Campos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Validación selección paciente
            if (string.IsNullOrWhiteSpace(txtIdPaciente.Text) || !int.TryParse(txtIdPaciente.Text, out _))
            {
                // CORREGIDO: MessageBoxIcon.Warning
                MessageBox.Show("Debe buscar y seleccionar un paciente válido antes de guardar.", "Paciente no Seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuscar.Focus();
                return;
            }
            // Validación selección proyecto
            if (cmbProyecto.SelectedItem == null || !(cmbProyecto.SelectedItem is Proyecto proyectoSeleccionado) || proyectoSeleccionado.id_proyecto <= 0)
            {
                MessageBox.Show("Debe seleccionar un proyecto válido.", "Proyecto no Seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProyecto.Focus();
                return;
            }

            // --- INICIO: Comprobación Proyecto Archivado ---
            // Asumiendo que el objeto Proyecto cargado en el ComboBox tiene la propiedad IsArchived
            if (proyectoSeleccionado.IsArchived)
            {
                MessageBox.Show($"El proyecto '{proyectoSeleccionado.nombre}' está archivado y no se pueden registrar nuevas muestras.", "Proyecto Archivado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return; // Detiene el guardado
            }
            // Validación tipo examen
            if (!chkOrina.Checked && !chkHeces.Checked && !chkSangre.Checked)
            {
                // CORREGIDO: MessageBoxIcon.Warning
                MessageBox.Show("Debe seleccionar al menos un tipo de examen.", "Tipo Examen Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // chkOrina.Focus(); // Opcional
                return;
            }


            int idProyecto = proyectoSeleccionado.id_proyecto;
            DateTime fechaActual = DateTime.Now.Date;
            int numeroMuestra = muestraRepository.ObtenerUltimaMuestra(idProyecto, fechaActual) + 1;

            // 1. Crear y guardar la muestra
            Muestra muestra = new Muestra()
            {
                IdProyecto = idProyecto,
                IdPaciente = Convert.ToInt32(txtIdPaciente.Text),
                NumeroMuestra = numeroMuestra,
                FechaRecepcion = fechaActual
            };

            int idMuestra = 0;
            try
            {
                idMuestra = muestraRepository.GuardarMuestras(muestra);
            }
            catch (Exception exGuardadoMuestra)
            {
                // CORREGIDO: MessageBoxIcon.Error
                MessageBox.Show($"Error al guardar la cabecera de la muestra:\n{exGuardadoMuestra.Message}", "Error Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR GuardarMuestras: {exGuardadoMuestra.ToString()}");
                return;
            }

            // 2. Vincular exámenes
            if (idMuestra > 0)
            {
                try
                {
                    if (chkOrina.Checked)
                        muestraExamenRepository.VincularExamen(new MuestraExamen { IdMuestra = idMuestra, IdTipoExamen = 1 });

                    if (chkHeces.Checked)
                        muestraExamenRepository.VincularExamen(new MuestraExamen { IdMuestra = idMuestra, IdTipoExamen = 2 });

                    if (chkSangre.Checked)
                        muestraExamenRepository.VincularExamen(new MuestraExamen { IdMuestra = idMuestra, IdTipoExamen = 3 });

                    // CORREGIDO: MessageBoxIcon.Information
                    MessageBox.Show($"Muestra #{numeroMuestra} guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // --- MANTIENE LA CORRECCIÓN ANTERIOR ---
                    LimpiarCampos(false); // Llama a limpiar SIN borrar el proyecto
                                          // --- FIN CORRECCIÓN ANTERIOR ---

                    txtBuscar.Clear();
                    txtBuscar.Focus();
                }
                catch (Exception exVinculacion)
                {
                    // CORREGIDO: MessageBoxIcon.Error
                    MessageBox.Show($"Se guardó la muestra #{numeroMuestra}, pero ocurrió un error al vincular los tipos de examen:\n{exVinculacion.Message}\n\nRevise la configuración o intente editar la muestra.", "Error al Vincular Exámenes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"ERROR Vinculando Exámenes para Muestra ID {idMuestra}: {exVinculacion.ToString()}");
                    LimpiarCampos(false); // Limpia igual para la siguiente
                    txtBuscar.Clear();
                    txtBuscar.Focus();
                }
            }
            else
            {
                // CORREGIDO: MessageBoxIcon.Error
                MessageBox.Show("No se pudo obtener un ID válido al guardar la muestra. No se vincularon exámenes.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } // Fin guardarMuestra
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
        #endregion

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