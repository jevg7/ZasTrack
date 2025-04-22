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
using ZasTrack.Forms.Examenes;
using ZasTrack.Forms.Muestras;
using ZasTrack.Models;
using ZasTrack.Repositories;

namespace ZasTrack.Forms.Dashboard
{
    public partial class wDashboard : Form
    {
        private PacienteRepository pacienteRepository;
        private ProyectoRepository proyectoRepository;
        private MuestraRepository muestraRepository;
        private ExamenRepository examenRepository;
        private wMain _mainForm; 

        // Modifica el constructor para aceptar wMain
        public wDashboard(wMain mainForm) 
        {
            _mainForm = mainForm; 

            // Inicialización de repositorios (como lo tenías)
            pacienteRepository = new PacienteRepository();
            proyectoRepository = new ProyectoRepository();
            muestraRepository = new MuestraRepository();
            examenRepository = new ExamenRepository();

            InitializeComponent();
        }


        private void wDashboard_Load(object sender, EventArgs e)
        {
            CargarProyectos(); // Carga los proyectos en el ComboBox

            // --- Mostrar Versión de la Aplicación ---
            // Asume que tienes un Label llamado lblVersionApp en el diseñador
            // Puedes usar Find o acceder directamente si es un miembro de la clase.
            Label versionLabel = this.Controls.Find("lblVersionApp", true).FirstOrDefault() as Label;
            if (versionLabel != null)
            {
                try
                {
                    versionLabel.Text = $"Version: {Application.ProductVersion}";
                }
                catch
                {
                    versionLabel.Text = "Version: N/A"; // En caso de error al obtenerla
                }
            }

            // Carga inicial de datos SIN proyecto seleccionado (mostrará guiones o N/A)
            CargarDatosDashboard(null);
        }

        private void CargarDatosDashboard(int? idProyectoSeleccionado)
        {
            // Limpia o muestra estado inicial si no hay proyecto
            if (!idProyectoSeleccionado.HasValue || idProyectoSeleccionado <= 0)
            {
                lblPacientesTotal.Text = "Total Pacientes:\n-";
                lblMuestrasDia.Text = "Muestras Hoy:\n-";
                lblExamenesRev.Text = "Exámenes Pendientes:\n-"; // Label para desglose
                lblInfomes.Text = "Procesados Hoy:\n-"; // Usamos lblInfomes para esto
                                                        // TODO: Limpiar paneles de gráficas/listas si es necesario
                return;
            }

            int idProyecto = idProyectoSeleccionado.Value;
            DateTime hoy = DateTime.Today;

            // Muestra un estado de carga (opcional)
            lblPacientesTotal.Text = "Total Pacientes:\nCargando...";
            lblMuestrasDia.Text = "Muestras Hoy:\nCargando...";
            lblExamenesRev.Text = "Exámenes Pendientes:\nCargando...";
            lblInfomes.Text = "Procesados Hoy:\nCargando...";
            Application.DoEvents(); // Permite que la UI se refresque

            try
            {
                // --- Obtener Datos de Repositorios ---

                // 1. Total Pacientes (Usa tu método existente)
                int totalPacientes = pacienteRepository.obtTotalPacientes(idProyecto);
                lblPacientesTotal.Text = $"Total Pacientes:\n{totalPacientes}";

                // 2. Muestras Hoy (Necesita CountByProjectAndDate en MuestraRepository)
                int muestrasHoy = muestraRepository.CountByProjectAndDate(idProyecto, hoy);
                lblMuestrasDia.Text = $"Muestras Hoy:\n{muestrasHoy}";

                // 3. Exámenes Pendientes (Desglose) (Necesita CountPendientesByTypeByProject en ExamenRepository)
                Dictionary<string, int> pendientesPorTipo = examenRepository.CountPendientesByTypeByProject(idProyecto);
                StringBuilder sbPendientes = new StringBuilder("Pendientes:\n"); // Título más corto
                if (pendientesPorTipo.Any())
                {
                    // Crea líneas separadas para cada tipo
                    foreach (var kvp in pendientesPorTipo.OrderBy(x => x.Key))
                    {
                        sbPendientes.AppendLine($"{kvp.Key}: {kvp.Value}");
                    }
                }
                else
                {
                    sbPendientes.Append("Ninguno");
                }
                // Ajusta el tamaño del Label si es necesario o usa un control más grande
                lblExamenesRev.Text = sbPendientes.ToString().TrimEnd();
                lblExamenesRev.AutoSize = false; // Para que respete el tamaño del panel
                lblExamenesRev.Size = pnlExamenesRev.ClientSize; // Ajusta al tamaño del panel
                lblExamenesRev.TextAlign = ContentAlignment.TopLeft;


                // 4. Procesados Hoy (Necesita CountProcesadosByProjectAndDate en ExamenRepository)
                int procesadosHoy = examenRepository.CountProcesadosByProjectAndDate(idProyecto, hoy);
                lblInfomes.Text = $"Procesados Hoy:\n{procesadosHoy}";

                // --- Llamadas futuras para gráficas/listas ---
                // CargarGraficaMuestrasProcesadas(idProyecto);
                // CargarListaUltimasMuestras(idProyecto);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos del dashboard: {ex.Message}", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR CargarDatosDashboard: {ex.ToString()}");
                // Mostrar estado de error en labels
                lblPacientesTotal.Text = "Total Pacientes:\nError";
                lblMuestrasDia.Text = "Muestras Hoy:\nError";
                lblExamenesRev.Text = "Exámenes Pendientes:\nError";
                lblInfomes.Text = "Procesados Hoy:\nError";
            }
        }

        private void cboProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? idProyectoSeleccionado = null;

            // Obtén el ID del proyecto seleccionado de forma segura
            if (cmbProyecto.SelectedItem is Proyecto proyectoSeleccionado && proyectoSeleccionado.id_proyecto > 0)
            {
                idProyectoSeleccionado = proyectoSeleccionado.id_proyecto;
            }
            // Llama al método centralizado para cargar/refrescar TODOS los datos
            CargarDatosDashboard(idProyectoSeleccionado);
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

        private void btnAccionNuevaMuestra_Click(object sender, EventArgs e)
        {
            // Llama al método Abrir_Form del formulario principal (wMain)
            if (_mainForm != null)
            {
                _mainForm.Abrir_Form(new Forms.Muestras.wMuestras());
            }
        }

        private void btnAccionVerPendientes_Click(object sender, EventArgs e)
        {
            if (_mainForm != null)
            {
                _mainForm.Abrir_Form(new Forms.Examenes.wExamenes());
               ;
            }
        }
    }
}
