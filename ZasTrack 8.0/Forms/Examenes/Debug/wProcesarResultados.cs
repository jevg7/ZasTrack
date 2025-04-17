using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Models;

namespace ZasTrack.Forms.Examenes.Debug
{
    public partial class wProcesarResultados : Form
    {
        private int _idMuestra;
        private int _numeroMuestra;
        private DateTime _fechaRecepcion;
        private string _nombrePaciente;
        private bool _esModoVistaOEditar; // Para saber si es nuevo o ver/editar
        private ExamenRepository _examenRepository; 
        public wProcesarResultados(int idMuestra, int numeroMuestra, DateTime fechaRecepcion, string nombrePaciente, bool modoVista = false)
        {
            InitializeComponent();
            _idMuestra = idMuestra;
            // Guarda los datos extras
            _numeroMuestra = numeroMuestra;
            _fechaRecepcion = fechaRecepcion;
            _nombrePaciente = nombrePaciente;
            _esModoVistaOEditar = modoVista; // Guarda el modo

            _examenRepository = new ExamenRepository();
            this.Load += WProcesarResultados_Load;

        }


        // --- Evento Load: Se ejecuta cuando el formulario se carga ---
        private void WProcesarResultados_Load(object sender, EventArgs e)
        {
            // --- Construye el título ---
            CultureInfo culturaEsp = new CultureInfo("es-NI");
            string diaSemana = _fechaRecepcion.ToString("dddd", culturaEsp);
            if (!string.IsNullOrEmpty(diaSemana)) { diaSemana = char.ToUpper(diaSemana[0]) + diaSemana.Substring(1); }
            this.Text = $"Resultados: {diaSemana} #{_numeroMuestra} - {_nombrePaciente}";
            // --- Fin Título ---

            CargarPestanasExamenes(); // Llama a la carga de pestañas
        }

        // --- Método para Cargar Pestañas (A IMPLEMENTAR) ---
        // En wProcesarResultados.cs

        // --- Método para Cargar Pestañas Dinámicamente ---
        private void CargarPestanasExamenes()
        {
            tabControlExamenes.TabPages.Clear(); // Limpia pestañas preexistentes o temporales

            // Si estamos en modo Vista/Editar, la lógica sería diferente (cargar todas las procesadas)
            // Por ahora, nos enfocamos en el modo "Procesar" (nuevo)
            if (_esModoVistaOEditar)
            {
                // TODO: Implementar lógica para modo Ver/Editar
                // - Obtener exámenes YA PROCESADOS para _idMuestra
                // - Crear pestañas para esos
                // - Cargar los datos guardados en los controles
                MessageBox.Show("Modo Ver/Editar aún no implementado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Podrías añadir pestañas vacías o deshabilitadas temporalmente
                return;
            }

            // --- Lógica para modo "Procesar" (Exámenes Pendientes) ---
            try
            {
                // 1. Llama al nuevo método del repositorio para obtener pendientes
                List<tipo_examen> examenesPendientes = _examenRepository.ObtenerTiposExamenPendientesPorMuestra(_idMuestra);

                if (!examenesPendientes.Any())
                {
                    MessageBox.Show("No se encontraron exámenes pendientes para esta muestra.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Podríamos cerrar el modal o mostrar un mensaje en lugar de pestañas
                    this.Close(); // Cierra si no hay nada que procesar
                    return;
                }

                // 2. Itera sobre la lista de exámenes pendientes
                foreach (var tipoExamen in examenesPendientes)
                {
                    // 3a. Crear una nueva TabPage
                    TabPage nuevaPestana = new TabPage(tipoExamen.nombre); // El título de la pestaña será el nombre del examen
                    nuevaPestana.Name = "tab" + tipoExamen.nombre.Replace(" ", ""); // Nombre interno útil

                    // 3b. Crear instancia del UserControl correspondiente
                    //     ¡¡IMPORTANTE!! Asume que has convertido EGO.cs, EGH.cs, BHC.cs a UserControls
                    //     llamados EGOControl, EGHControl, BHCControl. Si no, este paso fallará.
                    UserControl controlExamen = null;
                    switch (tipoExamen.id_tipo_examen) // O puedes usar tipoExamen.nombre
                    {
                        case 1: // Asumiendo ID 1 = Orina
                                // controlExamen = new EGOControl(); // Descomenta cuando tengas el UserControl
                            break;
                        case 2: // Asumiendo ID 2 = Heces
                                // controlExamen = new EGHControl(); // Descomenta cuando tengas el UserControl
                            break;
                        case 3: // Asumiendo ID 3 = Sangre
                                // controlExamen = new BHCControl(); // Descomenta cuando tengas el UserControl
                            break;
                        // Añade más casos si tienes más tipos de examen
                        default:
                            Console.WriteLine($"WARN: No se encontró UserControl para tipo examen ID: {tipoExamen.id_tipo_examen}, Nombre: {tipoExamen.nombre}");
                            // Podrías añadir una etiqueta de "No implementado" a la pestaña
                            controlExamen = new UserControl(); // Placeholder temporal
                            controlExamen.Controls.Add(new Label { Text = $"Controles para {tipoExamen.nombre} no implementados.", Dock = DockStyle.Fill });
                            break;
                    }

                    // 3c. Configurar y añadir el UserControl a la TabPage (si se creó)
                    if (controlExamen != null)
                    {
                        controlExamen.Dock = DockStyle.Fill; // Haz que ocupe toda la pestaña
                        nuevaPestana.Controls.Add(controlExamen);
                    }

                    // 3d. Añadir la nueva TabPage al TabControl
                    tabControlExamenes.TabPages.Add(nuevaPestana);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las pestañas de exámenes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Considera cerrar el modal o manejar el error de otra forma
            }
        }
        // --- Fin CargarPestanasExamenes ---


        // --- Click Guardar (A IMPLEMENTAR LÓGICA DE GUARDADO) ---
        private void btnGuardarResultados_Click(object sender, EventArgs e)
        {
            // TODO: PASO IMPORTANTE DESPUÉS DE CARGAR TABS:
            // 1. Validar que los datos en TODAS las pestañas visibles sean correctos.
            // 2. Recorrer las pestañas/UserControls visibles y obtener los datos ingresados.
            // 3. Llamar a métodos del repositorio para:
            //    a. Crear registro en tabla 'examen'.
            //    b. Crear/Actualizar registros en 'examen_orina', 'examen_heces', 'examen_sangre'.
            //    c. Poner 'procesado = true' en esas tablas de resultados.
            // 4. Si todo se guarda bien:
            MessageBox.Show("Resultados guardados (simulación).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information); // Temporal
            this.DialogResult = DialogResult.OK; // Señala éxito
            this.Close(); // Cierra el modal
            // 5. Si hay error al guardar, mostrar mensaje y NO cerrar.
        }

        // --- Click Cancelar (Normalmente solo cierra) ---
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Si asignaste DialogResult=Cancel en diseñador, esta línea no es estrictamente necesaria
            // pero no hace daño.
            this.DialogResult = DialogResult.Cancel;
            this.Close(); // Cierra el modal
        }
        private void tabControlExamenes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

} // Fin clase wProcesarResultados

  

