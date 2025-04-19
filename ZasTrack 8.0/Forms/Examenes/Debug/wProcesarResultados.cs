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
using ZasTrack.Forms.Examenes.ExamWrite;
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
            tabControlExamenes.TabPages.Clear(); // Limpia siempre

            // Determina si habilitar controles o no (por defecto, sí para procesar)
            bool habilitarControles = true;

            try
            {
                List<tipo_examen> examenesParaMostrar;

                if (_esModoVistaOEditar) // --- LÓGICA PARA MODO VER/EDITAR ---
                {
                    habilitarControles = false; // Por defecto, en modo vista es solo lectura
                                                // Podrías añadir un botón "Habilitar Edición" si quieres permitirla después

                    // 1. Obtiene la lista de tipos YA PROCESADOS para esta muestra
                    examenesParaMostrar = _examenRepository.ObtenerTiposExamenProcesadosPorMuestra(_idMuestra);

                    if (!examenesParaMostrar.Any())
                    {
                        MessageBox.Show("No se encontraron exámenes procesados para esta muestra.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.BeginInvoke(new Action(() => this.Close()));
                        return;
                    }
                }
                else // --- LÓGICA PARA MODO PROCESAR (PENDIENTES) ---
                {
                    // 1. Obtiene la lista de tipos PENDIENTES (como antes)
                    examenesParaMostrar = _examenRepository.ObtenerTiposExamenPendientesPorMuestra(_idMuestra);

                    if (!examenesParaMostrar.Any())
                    {
                        MessageBox.Show("Esta muestra no tiene exámenes pendientes de procesar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.BeginInvoke(new Action(() => this.Close()));
                        return;
                    }
                }

                // 2. Itera sobre la lista de exámenes (pendientes o procesados)
                foreach (var tipoExamen in examenesParaMostrar)
                {
                    // 3a. Crea la TabPage
                    TabPage nuevaPestana = new TabPage(tipoExamen.nombre);
                    nuevaPestana.Name = "tab" + tipoExamen.nombre.Replace(" ", "");
                    nuevaPestana.Tag = tipoExamen.id_tipo_examen;

                    // 3b. Crea el UserControl correspondiente
                    UserControl controlExamen = null;
                    object datosGuardados = null; // Variable para guardar los datos recuperados

                    switch (tipoExamen.id_tipo_examen)
                    {
                        case 1: // Orina
                            controlExamen = new EGOControl();
                            if (_esModoVistaOEditar) // Si es modo vista, busca los datos
                            {
                                datosGuardados = _examenRepository.ObtenerResultadoOrina(_idMuestra);
                            }
                            break;
                        case 2: // Heces
                            controlExamen = new EGHControl();
                            if (_esModoVistaOEditar)
                            {
                                datosGuardados = _examenRepository.ObtenerResultadoHeces(_idMuestra);
                            }
                            break;
                        case 3: // Sangre
                            controlExamen = new BHCControl();
                            if (_esModoVistaOEditar)
                            {
                                datosGuardados = _examenRepository.ObtenerResultadoSangre(_idMuestra);
                            }
                            break;
                        default:
                            controlExamen = new UserControl(); // Placeholder
                            controlExamen.Controls.Add(new Label { Text = $"Controles para '{tipoExamen.nombre}' no implementados.", Dock = DockStyle.Fill, ForeColor = Color.Red });
                            break;
                    }

                    // 3c. Añade y configura el UserControl
                    if (controlExamen != null)
                    {
                        controlExamen.Dock = DockStyle.Fill;
                        nuevaPestana.Controls.Add(controlExamen);

                        // 3d. Si estamos en modo Ver/Editar Y se encontraron datos, CARGA los datos
                        if (_esModoVistaOEditar && datosGuardados != null)
                        {
                            // Llama al método CargarDatos específico del UserControl
                            if (controlExamen is EGOControl ego && datosGuardados is examen_orina dataOrina) { ego.CargarDatos(dataOrina); }
                            else if (controlExamen is EGHControl egh && datosGuardados is examen_heces dataHeces) { egh.CargarDatos(dataHeces); }
                            else if (controlExamen is BHCControl bhc && datosGuardados is examen_sangre dataSangre) { bhc.CargarDatos(dataSangre); }
                        }
                        // Si es modo nuevo (Procesar), CargarDatos del UserControl pondrá los defaults si lo implementaste así
                        else if (!_esModoVistaOEditar)
                        {
                            // Asegurarse de que se inicialicen los defaults llamando a CargarDatos(null)
                            // si no lo hiciste en el constructor del UserControl
                            if (controlExamen is EGOControl ego) ego.CargarDatos(null);
                            else if (controlExamen is EGHControl egh) egh.CargarDatos(null);
                            else if (controlExamen is BHCControl bhc) bhc.CargarDatos(null);
                        }


                        // 3e. Hacer controles solo lectura si estamos en modo Vista
                        if (!habilitarControles) // Si habilitarControles es false (Modo Vista)
                        {
                            HacerControlesSoloLectura(controlExamen);
                        }
                    }

                    // 3f. Añade la nueva TabPage al TabControl
                    tabControlExamenes.TabPages.Add(nuevaPestana);
                } // Fin foreach

                // 4. Ajustar botones según el modo
                if (!habilitarControles) // Si es modo solo lectura
                {
                    btnGuardarResultados.Enabled = false; // Deshabilita guardar
                    btnGuardarResultados.Visible = false; // Oculta guardar
                    btnCancelar.Text = "&Cerrar";        // Cambia texto de Cancelar a Cerrar
                }
                else
                {
                    btnGuardarResultados.Enabled = true;
                    btnGuardarResultados.Visible = true;
                    btnCancelar.Text = "&Cancelar";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las pestañas de exámenes: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.BeginInvoke(new Action(() => this.Close()));
            }
        }
        // --- Fin CargarPestanasExamenes ---

        private void HacerControlesSoloLectura(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox txt)
                {
                    txt.ReadOnly = true;
                    txt.BackColor = SystemColors.Control; // Fondo gris para indicar que no se edita
                }
                else if (c.HasChildren) // Si es un contenedor (Panel, GroupBox), revisa dentro
                {
                    HacerControlesSoloLectura(c);
                }
                // Podrías añadir lógica para deshabilitar ComboBox, CheckBox si los tuvieras
                // else if (c is ComboBox combo) { combo.Enabled = false; }
            }
        }
        // --- Click Guardar (A IMPLEMENTAR LÓGICA DE GUARDADO) ---
        // En wProcesarResultados.cs

        private void btnGuardarResultados_Click(object sender, EventArgs e)
        {
            // --- 1. Obtener idPaciente ---
            //    (Necesitamos añadir este método simple al ExamenRepository)
            int? idPacienteNullable = _examenRepository.ObtenerIdPacientePorMuestra(_idMuestra);
            if (!idPacienteNullable.HasValue)
            {
                MessageBox.Show("Error fatal: No se pudo obtener la información del paciente para esta muestra. No se puede guardar.",
                                "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Detiene el proceso
            }
            int idPaciente = idPacienteNullable.Value; // Tenemos el id del paciente

            // --- 2. Validar y Recolectar Datos de las Pestañas Visibles ---
            bool todosLosDatosSonValidos = true;
            examen_orina datosOrina = null;
            examen_heces datosHeces = null;
            examen_sangre datosSangre = null;

            // Deshabilita botones y muestra cursor de espera mientras procesamos
            btnGuardarResultados.Enabled = false;
            btnCancelar.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            foreach (TabPage tabPage in tabControlExamenes.TabPages)
            {
                // Asume que el UserControl es el primer (y único) control en la pestaña
                if (tabPage.Controls.Count > 0 && tabPage.Controls[0] is UserControl controlExamen)
                {
                    // Llama a ObtenerDatos() según el tipo de UserControl
                    // ObtenerDatos() debe validar internamente y devolver null si falla
                    if (controlExamen is EGOControl ego)
                    {
                        datosOrina = ego.ObtenerDatos();
                        if (datosOrina == null) // Validación falló dentro de ObtenerDatos
                        {
                            todosLosDatosSonValidos = false;
                            tabControlExamenes.SelectedTab = tabPage; // Muestra la pestaña con error
                            break; // Detiene la recolección
                        }
                    }
                    else if (controlExamen is EGHControl egh) // Asume nombre correcto
                    {
                        datosHeces = egh.ObtenerDatos();
                        if (datosHeces == null)
                        {
                            todosLosDatosSonValidos = false;
                            tabControlExamenes.SelectedTab = tabPage;
                            break;
                        }
                    }
                    else if (controlExamen is BHCControl bhc) // Asume nombre correcto
                    {
                        datosSangre = bhc.ObtenerDatos();
                        if (datosSangre == null)
                        {
                            todosLosDatosSonValidos = false;
                            tabControlExamenes.SelectedTab = tabPage;
                            break;
                        }
                    }
                    // Añadir 'else if' para otros tipos de examen si los tienes
                }
            } // Fin foreach TabPage

            // Si alguna validación interna falló, se mostró mensaje desde ObtenerDatos/Validar...
            // y detenemos el proceso aquí antes de intentar guardar.
            if (!todosLosDatosSonValidos)
            {
                // Rehabilita botones y restaura cursor
                btnGuardarResultados.Enabled = true;
                btnCancelar.Enabled = true;
                this.Cursor = Cursors.Default;
                MessageBox.Show("Por favor, corrija los errores indicados antes de guardar.", "Validación Fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Sale del método Guardar
            }

            // --- 3. Si TODO es válido, intentar guardar en la BD ---
            bool guardadoGeneralExitoso = true; // Bandera para saber si todo se guardó

            try
            {
                // Intenta guardar cada resultado que se haya recolectado
                // Nota: Cada llamada al repositorio maneja su propia transacción interna
                if (datosOrina != null)
                {
                    if (!_examenRepository.GuardarResultadoOrina(datosOrina, _idMuestra, idPaciente))
                    {
                        guardadoGeneralExitoso = false; // Marca que algo falló
                                                        // El método del repositorio ya debería haber mostrado/logueado un error más específico
                    }
                }

                if (guardadoGeneralExitoso && datosHeces != null) // Solo sigue si no ha fallado nada antes
                {
                    if (!_examenRepository.GuardarResultadoHeces(datosHeces, _idMuestra, idPaciente))
                    {
                        guardadoGeneralExitoso = false;
                    }
                }

                if (guardadoGeneralExitoso && datosSangre != null) // Solo sigue si no ha fallado nada antes
                {
                    if (!_examenRepository.GuardarResultadoSangre(datosSangre, _idMuestra, idPaciente))
                    {
                        guardadoGeneralExitoso = false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Captura cualquier error inesperado (ej. de conexión a BD)
                guardadoGeneralExitoso = false;
                MessageBox.Show($"Error inesperado al guardar resultados: {ex.Message}\n{ex.StackTrace}", "Error Crítico de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Siempre rehabilita botones y cursor
                btnGuardarResultados.Enabled = true;
                btnCancelar.Enabled = true;
                this.Cursor = Cursors.Default;
            }


            // --- 4. Cerrar el modal SOLO si TODO se guardó correctamente ---
            if (guardadoGeneralExitoso)
            {
                MessageBox.Show("Resultados guardados exitosamente.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Indica éxito al formulario padre (wExamenesNoRecep)
                this.Close(); // Cierra este modal
            }
            else
            {
                MessageBox.Show("No se pudieron guardar todos los resultados. Por favor, revise los datos o contacte al administrador.", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // El modal NO se cierra, permitiendo al usuario reintentar o cancelar.
            }
        }
        // --- Fin Click Guardar ---
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

        private void btnGuardarResultados_Click_1(object sender, EventArgs e)
        {

        }
    }

} // Fin clase wProcesarResultados

  

