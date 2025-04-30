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
using System.Windows.Forms.DataVisualization.Charting;

namespace ZasTrack.Forms.Dashboard
{
    public partial class wDashboard : Form
    {
        private PacienteRepository pacienteRepository;
        private ProyectoRepository proyectoRepository;
        private MuestraRepository muestraRepository;
        private ExamenRepository examenRepository;
        private wMain _mainForm;
        
        public wDashboard(wMain mainForm)
        {
            _mainForm = mainForm;
            pacienteRepository = new PacienteRepository();
            proyectoRepository = new ProyectoRepository();
            muestraRepository = new MuestraRepository();
            examenRepository = new ExamenRepository();

            InitializeComponent();
        }
        private void wDashboard_Load(object sender, EventArgs e)
        {
            CargarProyectos(); 

            
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

            CargarDatosDashboard(null);


        }
        #region Metodos
        private async void CargarDatosDashboard(int? idProyectoSeleccionado)
        {
            // --- Limpieza inicial o estado "Sin Proyecto" ---
            if (!idProyectoSeleccionado.HasValue || idProyectoSeleccionado <= 0)
            {
                lblPacientesTotal.Text = "Total Pacientes:\n-";
                lblMuestrasDia.Text = "Muestras Hoy:\n-";
                lblExamenesRev.Text = "Exámenes Pendientes:\n-";
                lblInfomes.Text = "Procesados Hoy:\n-";
                LimpiarPanelDinamico(pnlMuestrasUltimas, lblMuestrasUltimas);
                LimpiarPanelDinamico(pnlExamenesUltimos, lblExamenesUltimos);
                ActualizarGraficoEstadoExamenes(0, 0); // Llama al método con ceros
                btnAccionNuevaMuestra.Enabled = false;
                btnAccionVerPendientes.Enabled = false;


                // Limpia paneles de listas
                pnlMuestrasUltimas.Controls.Clear();
                pnlMuestrasUltimas.Controls.Add(lblMuestrasUltimas); // Readiciona el título
                pnlExamenesUltimos.Controls.Clear();
                pnlExamenesUltimos.Controls.Add(lblExamenesUltimos); // Readiciona el título
                return;
            }
             btnAccionNuevaMuestra.Enabled = true;
             btnAccionVerPendientes.Enabled = true;

            int idProyecto = idProyectoSeleccionado.Value;
                DateTime hoy = DateTime.Today;
            this.Cursor = Cursors.WaitCursor;
            // --- Estado de Carga ---
            lblPacientesTotal.Text = "Total Pacientes:\nCargando...";
                lblMuestrasDia.Text = "Muestras Hoy:\nCargando...";
                lblExamenesRev.Text = "Exámenes Pendientes:\nCargando...";
                lblInfomes.Text = "Procesados Hoy:\nCargando...";
                ActualizarGraficoEstadoExamenes(0, 0); // Limpia gráfico mientras carga

                LimpiarPanelDinamico(pnlMuestrasUltimas, lblMuestrasUltimas);
                LimpiarPanelDinamico(pnlExamenesUltimos, lblExamenesUltimos);
                Application.DoEvents();



            try
            {
                // --- Carga de KPIs ASÍNCRONA ---
                // Usamos await para esperar cada tarea sin bloquear la UI
                // Task.WhenAll puede usarse para lanzarlas en paralelo si no dependen entre sí

                // Lanzar todas las tareas que no dependen entre sí
                Task<int> taskTotalPacientes = pacienteRepository.obtTotalPacientesAsync(idProyecto);
                Task<int> taskMuestrasHoy = muestraRepository.CountByProjectAndDateAsync(idProyecto, hoy);
                Task<Dictionary<string, int>> taskPendientes = examenRepository.CountPendientesByTypeByProjectAsync(idProyecto);
                Task<int> taskProcesadosHoy = examenRepository.CountProcesadosByProjectAndDateAsync(idProyecto, hoy);
                Task<List<MuestraInfoViewModel>> taskUltimasMuestras = muestraRepository.GetUltimasMuestrasPorProyectoAsync(idProyecto, 5);
                Task<List<ExamenProcesadoInfo>> taskUltimosExamenes = examenRepository.GetUltimosExamenesProcesadosPorProyectoAsync(idProyecto, 5);

                // Esperar a que todas terminen
                await Task.WhenAll(taskTotalPacientes, taskMuestrasHoy, taskPendientes, taskProcesadosHoy, taskUltimasMuestras, taskUltimosExamenes);

                // --- Actualizar UI con los resultados (AHORA que ya terminaron) ---
                lblPacientesTotal.Text = $"Total Pacientes:\n{taskTotalPacientes.Result}"; // Acceder a .Result DESPUÉS de await Task.WhenAll
                lblMuestrasDia.Text = $"Muestras Hoy:\n{taskMuestrasHoy.Result}";

                Dictionary<string, int> pendientesPorTipo = taskPendientes.Result;
                StringBuilder sbPendientes = new StringBuilder("Examenes Pendientes:\n");
                if (pendientesPorTipo.Any())
                {
                    foreach (var kvp in pendientesPorTipo.OrderBy(x => x.Key))
                    {
                        sbPendientes.AppendLine($"{kvp.Key}: {kvp.Value}");
                    }
                }
                else { sbPendientes.Append("Ninguno"); }
                lblExamenesRev.Text = sbPendientes.ToString().TrimEnd();

                int procesadosHoy = taskProcesadosHoy.Result;
                lblInfomes.Text = $"Procesados Hoy:\n{procesadosHoy}";

                // Calcular totales y actualizar gráfico
                int totalPendientesHoy = pendientesPorTipo.Values.Sum();
                Console.WriteLine($"DEBUG: Actualizando gráfico - Pendientes: {totalPendientesHoy}, Procesados: {procesadosHoy}");
                ActualizarGraficoEstadoExamenes(totalPendientesHoy, procesadosHoy);

                // Actualizar listas de última actividad
                MostrarListaEnPanel(pnlMuestrasUltimas, lblMuestrasUltimas, taskUltimasMuestras.Result.Cast<object>().ToList(), item =>
                    $"#{((MuestraInfoViewModel)item).NumeroMuestra} - {((MuestraInfoViewModel)item).Paciente} ({((MuestraInfoViewModel)item).FechaRecepcion:dd/MM/yy})");
                MostrarListaEnPanel(pnlExamenesUltimos, lblExamenesUltimos, taskUltimosExamenes.Result.Cast<object>().ToList(), item =>
                    $"#{((ExamenProcesadoInfo)item).NumeroMuestra} - {((ExamenProcesadoInfo)item).Paciente} ({((ExamenProcesadoInfo)item).TipoExamen} - {((ExamenProcesadoInfo)item).FechaProcesamiento:dd/MM/yy HH:mm})");

            }
            catch (Exception ex)
            {
                // ... (Manejo de error mejorado, igual que antes) ...
                lblPacientesTotal.Text = "Total Pacientes:\nError";
                lblMuestrasDia.Text = "Muestras Hoy:\nError";
                lblExamenesRev.Text = "Exámenes Pendientes:\nError";
                lblInfomes.Text = "Procesados Hoy:\nError";
                LimpiarPanelDinamico(pnlMuestrasUltimas, lblMuestrasUltimas);
                LimpiarPanelDinamico(pnlExamenesUltimos, lblExamenesUltimos);
                ActualizarGraficoEstadoExamenes(0, 0);
                MessageBox.Show($"Ocurrió un error al cargar los datos del Dashboard:\n{ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine($"ERROR CargarDatosDashboard: {ex.ToString()}");
            }
            finally // Asegura que esto SIEMPRE se ejecute, incluso si hay error
            {
                // Ocultar indicador de carga
                // progressBarCarga.Visible = false;
                this.Cursor = Cursors.Default; // Restaurar cursor
            }
        }
        private void ActualizarGraficoEstadoExamenes(int pendientes, int procesados)
        {
            // Asegúrate que 'chartEstadoDia' es el nombre que le diste al control Chart
            var chart = chartEstadoDia; // Accede al control Chart

            // Limpiar datos y configuración previa
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Legends.Clear();
            chart.Palette = ChartColorPalette.Pastel; // Elige una paleta de colores

            // Añadir un título al gráfico
            chart.Titles.Add("Estado Exámenes (Hoy)");
            chart.Titles[0].Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Crear la serie de datos para el gráfico Pie
            Series series = new Series("EstadoDia")
            {
                ChartType = SeriesChartType.Pie // O Doughnut para tipo Dona
            };

            // Añadir los puntos de datos (solo si hay datos)
            if (pendientes > 0 || procesados > 0)
            {
                series.Points.AddXY("Pendientes", pendientes);
                series.Points.AddXY("Procesados", procesados);

                // Opcional: Configurar etiquetas y ToolTips
                series.IsValueShownAsLabel = true; // Mostrar el número en el gráfico
                series.LabelFormat = "#"; // Mostrar el valor numérico
                // series.LabelFormat = "P0"; // Descomenta para mostrar porcentaje
                series.Font = new Font("Segoe UI", 9F);
                series.ToolTip = "#VALX: #VALY"; // Texto al pasar el mouse

                // Opcional: Colores específicos
                if (series.Points.Count > 0) series.Points[0].Color = Color.OrangeRed;
                if (series.Points.Count > 1) series.Points[1].Color = Color.MediumSeaGreen;

                // Añadir Leyenda
                Legend legend = new Legend("Default")
                {
                    Docking = Docking.Bottom, // Posición de la leyenda
                    Alignment = StringAlignment.Center,
                    Font = new Font("Segoe UI", 8F)
                };
                chart.Legends.Add(legend);
                series.Legend = "Default";
                series.LegendText = "#VALX"; // Muestra "Pendientes", "Procesados" en la leyenda
            }
            else
            {
                // Si no hay datos, mostrar un mensaje o dejarlo vacío
                // Podríamos añadir un punto "Vacío" o un título indicándolo
                series.Points.AddXY("Sin Actividad Hoy", 1);
                series.Points[0].Color = Color.LightGray;
                series.IsValueShownAsLabel = false; // No mostrar "1"
                                                    // Opcional: Añadir una anotación de texto
                TextAnnotation annotation = new TextAnnotation();
                annotation.Text = "Sin datos";
                annotation.X = 50;
                annotation.Y = 50;
                annotation.AnchorAlignment = ContentAlignment.MiddleCenter;
                annotation.Font = new Font("Segoe UI", 10F);
                annotation.ForeColor = Color.Gray;
                // chart.Annotations.Add(annotation); // Descomentar si quieres la anotación
            }


            // Añadir la serie al gráfico
            chart.Series.Add(series);

            // Opcional: Mejorar apariencia del área del gráfico
            if (chart.ChartAreas.Count == 0) chart.ChartAreas.Add("ChartArea1");
            chart.ChartAreas[0].BackColor = Color.Transparent;
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
            tituloLabel.BringToFront();
        }
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
        private void CargarProyectos()
        {
            cmbProyecto.SelectedIndexChanged -= cboProyecto_SelectedIndexChanged;

            List<Proyecto> proyectos = null; 
            try
            {
                proyectos = proyectoRepository.ObtenerProyectos(incluirArchivados: false);

                cmbProyecto.DataSource = proyectos;
                cmbProyecto.DisplayMember = "nombre"; // Muestra el nombre del proyecto
                cmbProyecto.ValueMember = "id_proyecto"; // Usa el ID como valor
                cmbProyecto.SelectedIndex = -1; // No seleccionar ningún proyecto por defecto
                cmbProyecto.Text = "Seleccione un proyecto activo..."; // Placeholder text
            }
            catch (Exception ex)
            {
                // Manejo básico de error si falla la carga de proyectos
                MessageBox.Show($"Error al cargar la lista de proyectos:\n{ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR CargarProyectos: {ex.ToString()}");
                cmbProyecto.DataSource = null;
                cmbProyecto.Items.Clear();
                cmbProyecto.Items.Add("Error al cargar proyectos");
                cmbProyecto.SelectedIndex = 0;
                cmbProyecto.Enabled = false; // Deshabilitar si hay error
            }
            finally
            {
                // Rehabilitar el evento SelectedIndexChanged SIEMPRE
                cmbProyecto.SelectedIndexChanged += cboProyecto_SelectedIndexChanged;
            }
        }
        #endregion
        #region Eventos de los botones
        private async void cboProyecto_SelectedIndexChanged(object sender, EventArgs e) // Cambiado a async void
        {
            int? idProyectoSeleccionado = null;
            if (cmbProyecto.SelectedItem is Proyecto proyectoSeleccionado && proyectoSeleccionado.id_proyecto > 0)
            {

                idProyectoSeleccionado = proyectoSeleccionado.id_proyecto;
            }
            // Llama al método centralizado para cargar/refrescar TODOS los datos
            CargarDatosDashboard(idProyectoSeleccionado);
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

        #endregion
        #region Windows Designer Auto Generated Code       

        private void pnlMuestrasDia_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblBienvenido_Click(object sender, EventArgs e)
        {

        }
        private void pnlAccionRapida_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion
    }
}
