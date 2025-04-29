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
using ZasTrack.Models.ExamenModel;
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
            // --- Limpieza inicial o estado "Sin Proyecto" ---
            if (!idProyectoSeleccionado.HasValue || idProyectoSeleccionado <= 0)
            {
                lblPacientesTotal.Text = "Total Pacientes:\n-";
                lblMuestrasDia.Text = "Muestras Hoy:\n-";
                lblExamenesRev.Text = "Exámenes Pendientes:\n-";
                lblInfomes.Text = "Procesados Hoy:\n-";

                // Limpia paneles de listas
                pnlMuestrasUltimas.Controls.Clear();
                pnlMuestrasUltimas.Controls.Add(lblMuestrasUltimas); // Readiciona el título
                pnlExamenesUltimos.Controls.Clear();
                pnlExamenesUltimos.Controls.Add(lblExamenesUltimos); // Readiciona el título
                return;
            }

            int idProyecto = idProyectoSeleccionado.Value;
            DateTime hoy = DateTime.Today;

            // --- Estado de Carga ---
            lblPacientesTotal.Text = "Total Pacientes:\nCargando...";
            lblMuestrasDia.Text = "Muestras Hoy:\nCargando...";
            lblExamenesRev.Text = "Exámenes Pendientes:\nCargando...";
            lblInfomes.Text = "Procesados Hoy:\nCargando...";
            // Limpia paneles de listas (quitando contenido anterior, no el título)
            LimpiarPanelDinamico(pnlMuestrasUltimas, lblMuestrasUltimas);
            LimpiarPanelDinamico(pnlExamenesUltimos, lblExamenesUltimos);
            Application.DoEvents();

            try
            {
                // --- Carga de KPIs (código existente) ---
                int totalPacientes = pacienteRepository.obtTotalPacientes(idProyecto);
                lblPacientesTotal.Text = $"Total Pacientes:\n{totalPacientes}";

                int muestrasHoy = muestraRepository.CountByProjectAndDate(idProyecto, hoy);
                lblMuestrasDia.Text = $"Muestras Hoy:\n{muestrasHoy}";

                Dictionary<string, int> pendientesPorTipo = examenRepository.CountPendientesByTypeByProject(idProyecto);
                StringBuilder sbPendientes = new StringBuilder("Pendientes:\n");
                if (pendientesPorTipo.Any()) // Verifica si el diccionario tiene resultados
                {
                    // Itera sobre cada par (NombreExamen, Conteo) en el diccionario
                    foreach (var kvp in pendientesPorTipo.OrderBy(x => x.Key)) // Ordena alfabéticamente por nombre de examen
                    {
                        // Añade una línea por cada tipo de examen pendiente y su conteo
                        sbPendientes.AppendLine($"{kvp.Key}: {kvp.Value}");
                    }
                }
                else
                {
                    // Si no hay pendientes, añade un mensaje indicándolo
                    sbPendientes.Append("Ninguno");
                }

                lblExamenesRev.Text = sbPendientes.ToString().TrimEnd();                    // ... (ajustes de tamaño/alineación para lblExamenesRev) ...

                int procesadosHoy = examenRepository.CountProcesadosByProjectAndDate(idProyecto, hoy);
                lblInfomes.Text = $"Procesados Hoy:\n{procesadosHoy}";


                // --- INICIO: Cargar Última Actividad ---

                // Cargar Últimas Muestras
                var ultimasMuestras = muestraRepository.GetUltimasMuestrasPorProyecto(idProyecto, 5); // Obtiene las últimas 5
                MostrarListaEnPanel(pnlMuestrasUltimas, lblMuestrasUltimas, ultimasMuestras.Cast<object>().ToList(), item =>
                    $"#{((MuestraInfoViewModel)item).NumeroMuestra} - {((MuestraInfoViewModel)item).Paciente} ({((MuestraInfoViewModel)item).FechaRecepcion:dd/MM/yy})"
                );

                // Cargar Últimos Exámenes Procesados
                var ultimosExamenes = examenRepository.GetUltimosExamenesProcesadosPorProyecto(idProyecto, 5); // Obtiene los últimos 5
                MostrarListaEnPanel(pnlExamenesUltimos, lblExamenesUltimos, ultimosExamenes.Cast<object>().ToList(), item =>
                     $"#{((ExamenProcesadoInfo)item).NumeroMuestra} - {((ExamenProcesadoInfo)item).Paciente} ({((ExamenProcesadoInfo)item).TipoExamen} - {((ExamenProcesadoInfo)item).FechaProcesamiento:dd/MM/yy HH:mm})"
                );

                // --- FIN: Cargar Última Actividad ---

            }
            catch (Exception ex)
            {
                // ... (Manejo de error existente) ...
                // Asegurarse de limpiar paneles de listas también en caso de error
                LimpiarPanelDinamico(pnlMuestrasUltimas, lblMuestrasUltimas);
                LimpiarPanelDinamico(pnlExamenesUltimos, lblExamenesUltimos);
                lblMuestrasUltimas.Text += "\nError al cargar"; // Añadir mensaje de error al título
                lblExamenesUltimos.Text += "\nError al cargar";
            }
        }
        private void LimpiarPanelDinamico(Panel panel, Label tituloLabel)
        {
            // Guarda los controles que NO son el título
            var controlesAEliminar = panel.Controls.Cast<Control>().Where(c => c != tituloLabel).ToList();
            foreach (var c in controlesAEliminar)
            {
                panel.Controls.Remove(c);
                c.Dispose(); // Libera recursos
            }
            // Asegura que el título quede posicionado correctamente (si es necesario)
            tituloLabel.Location = new Point(10, 10); // O la posición original
            tituloLabel.BringToFront();
        }

        // Muestra una lista de objetos en un panel creando Labels dinámicos
        private void MostrarListaEnPanel(Panel panel, Label tituloLabel, List<object> items, Func<object, string> formatoItem)
        {
            LimpiarPanelDinamico(panel, tituloLabel); // Limpia contenido previo

            if (items == null || !items.Any())
            {
                Label lblVacio = new Label();
                lblVacio.Text = "(Ninguno)";
                lblVacio.ForeColor = SystemColors.GrayText;
                lblVacio.AutoSize = true;
                lblVacio.Location = new Point(tituloLabel.Left, tituloLabel.Bottom + 5); // Debajo del título
                panel.Controls.Add(lblVacio);
                return;
            }

            int topPosition = tituloLabel.Bottom + 5; // Posición inicial debajo del título
            foreach (var item in items)
            {
                Label lblItem = new Label();
                lblItem.Text = formatoItem(item); // Usa la función de formato para obtener el texto
                lblItem.AutoSize = true;
                lblItem.Location = new Point(tituloLabel.Left, topPosition); // Alineado con el título
                lblItem.MaximumSize = new Size(panel.ClientSize.Width - tituloLabel.Left - 5, 0); // Evita que se salga del panel
                panel.Controls.Add(lblItem);
                topPosition += lblItem.Height + 2; // Siguiente posición vertical
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
                _mainForm.ShowChildForm(new Forms.Muestras.wMuestras());
            }
        }

        private void btnAccionVerPendientes_Click(object sender, EventArgs e)
        {
            if (_mainForm != null)
            {
                _mainForm.ShowChildForm(new Forms.Examenes.wExamenes());
                ;
            }
        }

        private void pnlAccionRapida_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
