using System;
using System.Drawing; // Para Color, Font, etc.
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Forms.Examenes.Debug;
using ZasTrack.Models;       // Para Proyecto y otros modelos que usemos
using ZasTrack.Models.ExamenModel;
using ZasTrack.Repositories; // Para los repositorios

// Asegúrate que el namespace sea correcto
namespace ZasTrack.Forms.wProyectos
{
    public partial class wVerDetallesProyecto : Form
    {
        private readonly int _idProyectoActual; 
        private Proyecto? _proyectoActual;     
        private bool _esArchivado;             
        private readonly ProyectoRepository _proyectoRepository;
        private readonly PacienteRepository _pacienteRepository;
        private readonly MuestraRepository _muestraRepository;
        private readonly ExamenRepository _examenRepository;

        public wVerDetallesProyecto(int idProyecto)
        {
            InitializeComponent();

            // Guardamos el ID
            if (idProyecto <= 0) throw new ArgumentException("El ID del proyecto no es válido.", nameof(idProyecto));
            _idProyectoActual = idProyecto;

                _proyectoRepository = new ProyectoRepository();
            _pacienteRepository = new PacienteRepository();
            _muestraRepository = new MuestraRepository();
            _examenRepository = new ExamenRepository();

            btnCerrar.Click += BtnCerrar_Click;
            dgvExamenes.CellContentClick += dgvExamenes_CellContentClick;
        }
        private async void wVerDetallesProyecto_Load(object sender, EventArgs e)
        {
            Console.WriteLine("--- wVerDetallesProyecto_Load INICIO ---");
            this.Cursor = Cursors.WaitCursor;

            // --- 1. OBTENER DATOS DEL PROYECTO DESDE LA BD ---
            try
            {
                Console.WriteLine($"DEBUG: Buscando proyecto con ID: {_idProyectoActual}");
                _proyectoActual = await _proyectoRepository.ObtenerProyectoPorIdAsync(_idProyectoActual);

                if (_proyectoActual == null)
                {
                    Console.WriteLine($"ERROR: No se encontró proyecto con ID: {_idProyectoActual} o repo devolvió null.");
                    MessageBox.Show($"No se encontró el proyecto con ID {_idProyectoActual} o hubo un error al cargarlo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                _esArchivado = _proyectoActual.IsArchived;
                Console.WriteLine($"DEBUG: Proyecto Cargado OK -> ID={_proyectoActual.id_proyecto}, Nombre='{_proyectoActual.nombre}', Codigo='{_proyectoActual.codigo}', Archived={_esArchivado}");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show($"Error fatal al cargar los datos del proyecto:\n{ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR Fatal Load Proyecto Detalles: {ex}");
                this.Close();
                return;
            }

            // --- 2. Llenar Información Básica (CON DEBUG ANTES Y DESPUÉS) ---
            try
            {
                Console.WriteLine($"DEBUG: Intentando asignar Nombre: '{_proyectoActual.nombre}' a lblNombreProyecto ('{lblNombreProyecto.Name}')");
                lblNombreProyecto.Text = _proyectoActual.nombre;
                Console.WriteLine($"DEBUG: lblNombreProyecto.Text AHORA ES: '{lblNombreProyecto.Text}'");

                string codigoTexto = $"(Código: {_proyectoActual.codigo})";
                Console.WriteLine($"DEBUG: Intentando asignar Código: '{codigoTexto}' a lblCodigoProyecto ('{lblCodigoProyecto.Name}')");
                lblCodigoProyecto.Text = codigoTexto;
                Console.WriteLine($"DEBUG: lblCodigoProyecto.Text AHORA ES: '{lblCodigoProyecto.Text}'");

                string fechaInicioTexto = _proyectoActual.fecha_inicio.ToString("dd/MM/yyyy");
                Console.WriteLine($"DEBUG: Intentando asignar Fecha Inicio: '{fechaInicioTexto}' a lblFechaInicio ('{lblFechaInicio.Name}')");
                lblFechaInicio.Text = fechaInicioTexto;
                Console.WriteLine($"DEBUG: lblFechaInicio.Text AHORA ES: '{lblFechaInicio.Text}'");

                string fechaFinTexto = _proyectoActual.fecha_fin.HasValue ? _proyectoActual.fecha_fin.Value.ToString("dd/MM/yyyy") : "Activo";
                Console.WriteLine($"DEBUG: Intentando asignar Fecha Fin: '{fechaFinTexto}' a lblFechaFin ('{lblFechaFin.Name}')");
                lblFechaFin.Text = fechaFinTexto;
                Console.WriteLine($"DEBUG: lblFechaFin.Text AHORA ES: '{lblFechaFin.Text}'");
            }
            catch (Exception exLabel)
            {
                // Error al asignar a los labels (raro, pero posible si un control no existe)
                Console.WriteLine($"ERROR asignando texto a Labels básicos: {exLabel.Message}");
                MessageBox.Show($"Ocurrió un error al mostrar la información básica del proyecto:\n{exLabel.Message}", "Error de UI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // --- FIN DEBUG LABELS ---


            // 3. Ajustar UI si está Archivado (CON DEBUG)
            Console.WriteLine($"DEBUG: Ajustando UI para Archivado = {_esArchivado}");
            if (_esArchivado)
            {
                lblEstadoArchivado.Visible = true;
                lblFechaFin.ForeColor = Color.OrangeRed;
            }
            else
            {
                lblEstadoArchivado.Visible = false;
                lblFechaFin.ForeColor = SystemColors.ControlText;
            }
            Console.WriteLine($"DEBUG: lblEstadoArchivado.Visible = {lblEstadoArchivado.Visible}");


            // 4. Cargar Datos de Grids y Estadísticas Asíncronamente
            Console.WriteLine("DEBUG: Iniciando carga de Estadisticas y Grids...");
            await CargarEstadisticasAsync();
            await CargarPacientesAsync();
            await CargarMuestrasAsync();
            await CargarExamenesAsync();
            Console.WriteLine("DEBUG: Carga de Estadisticas y Grids Terminada.");

            ConfigurarColumnasGrids();

            this.Cursor = Cursors.Default;
            Console.WriteLine("--- wVerDetallesProyecto_Load FIN ---");
        }   
        #region Cargar Datos de Grids
        private async Task CargarEstadisticasAsync()
        {
            Console.WriteLine("DEBUG: Iniciando CargarEstadisticasAsync...");
            // Mostrar estado inicial
            lblTotalPacientes.Text = "Calculando...";
            lblTotalMuestras.Text = "Calculando...";
            lblTotalExamenesProc.Text = "Calculando...";

            try
            {
                // Lanzar las 3 tareas de conteo en paralelo
                Task<int> taskCountPac = _pacienteRepository.ContarPorProyectoIdAsync(_idProyectoActual);
                Task<int> taskCountMue = _muestraRepository.ContarPorProyectoIdAsync(_idProyectoActual);
                Task<int> taskCountExa = _examenRepository.ContarProcesadosPorProyectoIdAsync(_idProyectoActual);

                // Esperar a que todas terminen
                await Task.WhenAll(taskCountPac, taskCountMue, taskCountExa);

                // Obtener los resultados y actualizar los labels
                // Accedemos a .Result DESPUÉS de que Task.WhenAll haya terminado
                lblTotalPacientes.Text = taskCountPac.Result.ToString();
                lblTotalMuestras.Text = taskCountMue.Result.ToString();
                lblTotalExamenesProc.Text = taskCountExa.Result.ToString();

                Console.WriteLine($"DEBUG: Estadísticas Cargadas - Pac: {taskCountPac.Result}, Mue: {taskCountMue.Result}, ExaProc: {taskCountExa.Result}");
            }
            catch (Exception ex)
            {
                // Si alguna de las tareas falló, mostrar Error
                lblTotalPacientes.Text = "Error";
                lblTotalMuestras.Text = "Error";
                lblTotalExamenesProc.Text = "Error";
                Console.WriteLine($"ERROR CargarEstadisticasAsync: {ex}");
                // Opcional: Mostrar un MessageBox, aunque quizás sea demasiado si falla a menudo
                // MessageBox.Show($"Error al cargar las estadísticas:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Console.WriteLine("DEBUG: CargarEstadisticasAsync Finalizado.");
        }
        private async Task CargarPacientesAsync()
        {
            dgvPacientes.DataSource = null; // Limpiar antes de cargar
            Console.WriteLine($"DEBUG: Iniciando CargarPacientesAsync para Proyecto ID: {_idProyectoActual}");
            try
            {
                // Llamar al método del repositorio que ya creamos
                List<pacientes> listaPacientes = await _pacienteRepository.ObtenerPorProyectoIdAsync(_idProyectoActual); // O List<pacientes> si tu clase se llama así

                if (listaPacientes != null && listaPacientes.Any())
                {
                    Console.WriteLine($"DEBUG: Se obtuvieron {listaPacientes.Count} pacientes.");

                    dgvPacientes.AutoGenerateColumns = false; // Evita que cree columnas extras
                                                              

                    // Asignar la lista como origen de datos
                    dgvPacientes.DataSource = listaPacientes;

                    // Configurar qué propiedad va en qué columna 
                    ConfigurarColumnasPacientes();
                }
                else
                {
                    Console.WriteLine("DEBUG: No se encontraron pacientes para este proyecto.");
                    // Opcional: Mostrar un mensaje en el grid o un label si está vacío
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la lista de pacientes:\n{ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine($"Error CargarPacientesAsync: {ex}");
                dgvPacientes.DataSource = null; // Asegurar que quede vacío en caso de error
            }
            finally
            {
                // Ajustar modo de AutoSize después de cargar datos para mejor rendimiento
                AjustarAutoSizeGrid(dgvPacientes);
                Console.WriteLine("DEBUG: CargarPacientesAsync Finalizado.");
            }
        }
        private async Task CargarMuestrasAsync()
        {
            dgvMuestras.DataSource = null; // Limpiar antes de cargar
            Console.WriteLine($"DEBUG: Iniciando CargarMuestrasAsync para Proyecto ID: {_idProyectoActual}");
            try
            {
                // Llamar al nuevo método del repositorio
                List<MuestraDetalleViewModel> listaMuestras = await _muestraRepository.ObtenerDetallesMuestraPorProyectoIdAsync(_idProyectoActual);

                if (listaMuestras != null && listaMuestras.Any())
                {
                    Console.WriteLine($"DEBUG: Se obtuvieron {listaMuestras.Count} muestras.");
                    // IMPORTANTE: Evitar que se auto-generen columnas
                    dgvMuestras.AutoGenerateColumns = false;
                    // Asignar la lista como origen de datos
                    dgvMuestras.DataSource = listaMuestras;
                    // Configurar qué propiedad del ViewModel va en qué columna del grid
                    ConfigurarColumnasMuestras(); // Llamar al helper para mapear columnas
                }
                else
                {
                    Console.WriteLine("DEBUG: No se encontraron muestras para este proyecto.");
                    // Podrías mostrar un mensaje aquí si lo deseas
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la lista de muestras:\n{ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine($"Error CargarMuestrasAsync: {ex}");
                dgvMuestras.DataSource = null; // Asegurar que quede vacío en caso de error
            }
            finally
            {
                // Ajustar modo de AutoSize después de cargar datos
                AjustarAutoSizeGrid(dgvMuestras);
                Console.WriteLine("DEBUG: CargarMuestrasAsync Finalizado.");
            }
        }
        private async Task CargarExamenesAsync()
        {
            dgvExamenes.DataSource = null;
            Console.WriteLine($"DEBUG: Iniciando CargarExamenesAsync para Proyecto ID: {_idProyectoActual}");
            try
            {
                List<ExamenDetalleViewModel> listaExamenes = await _examenRepository.ObtenerProcesadosPorProyectoIdAsync(_idProyectoActual);

                if (listaExamenes != null && listaExamenes.Any())
                {
                    Console.WriteLine($"DEBUG: Se obtuvieron {listaExamenes.Count} exámenes procesados.");
                    dgvExamenes.AutoGenerateColumns = false;
                    dgvExamenes.DataSource = listaExamenes;
                    ConfigurarColumnasExamenes(); // Necesitamos crear este método
                }
                else { Console.WriteLine("DEBUG: No se encontraron exámenes procesados para este proyecto."); }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la lista de exámenes:\n{ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine($"Error CargarExamenesAsync: {ex}");
                dgvExamenes.DataSource = null;
            }
            finally
            {
                AjustarAutoSizeGrid(dgvExamenes);
                Console.WriteLine("DEBUG: CargarExamenesAsync Finalizado.");
            }
        }
        #endregion
        #region Configuración de Columnas

        // ---  Para configurar columnas ---
        private void ConfigurarColumnasGrids()
        {
            dgvPacientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvMuestras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvExamenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
        private void ConfigurarColumnasPacientes()
        {
            // *** OJO: Asegúrate que los nombres de columna ("colCodigoPaciente", etc.)
            // *** coincidan con los 'Name' que les diste en el Diseñador,
            // *** y que los DataPropertyName coincidan con los nombres de las propiedades
            // *** en tu clase Paciente (o pacientes).

            if (dgvPacientes.Columns["colCodigoPaciente"] != null)
                dgvPacientes.Columns["colCodigoPaciente"].DataPropertyName = "codigo_beneficiario";

            if (dgvPacientes.Columns["colNombres"] != null)
                dgvPacientes.Columns["colNombres"].DataPropertyName = "nombres";

            if (dgvPacientes.Columns["colApellidos"] != null)
                dgvPacientes.Columns["colApellidos"].DataPropertyName = "apellidos";

            if (dgvPacientes.Columns["colGenero"] != null)
                dgvPacientes.Columns["colGenero"].DataPropertyName = "genero";

            if (dgvPacientes.Columns["colEdad"] != null)
                dgvPacientes.Columns["colEdad"].DataPropertyName = "edad";

            if (dgvPacientes.Columns["colFechaNacimiento"] != null)
            {
                dgvPacientes.Columns["colFechaNacimiento"].DataPropertyName = "fecha_nacimiento";
                dgvPacientes.Columns["colFechaNacimiento"].DefaultCellStyle.Format = "dd/MM/yyyy"; // Formato fecha
            }

            // La columna del botón (colVerPacienteDetalle) no necesita DataPropertyName
        }
        private void ConfigurarColumnasMuestras()
        {
            // *** OJO: Asegúrate que los nombres de columna ("colNumMuestra", etc.)
            // *** coincidan con los 'Name' que les diste en el Diseñador,
            // *** y que los DataPropertyName coincidan con los nombres de las propiedades
            // *** en tu clase MuestraDetalleViewModel.

            try // Añadir try-catch por si alguna columna no existe
            {
                if (dgvMuestras.Columns["colNumMuestra"] != null)
                    dgvMuestras.Columns["colNumMuestra"].DataPropertyName = nameof(MuestraDetalleViewModel.NumeroMuestra);

                if (dgvMuestras.Columns["colCodigoPacienteMuestra"] != null)
                    dgvMuestras.Columns["colCodigoPacienteMuestra"].DataPropertyName = nameof(MuestraDetalleViewModel.CodigoPaciente);

                if (dgvMuestras.Columns["colNombrePacienteMuestra"] != null)
                    dgvMuestras.Columns["colNombrePacienteMuestra"].DataPropertyName = nameof(MuestraDetalleViewModel.NombrePaciente);

                if (dgvMuestras.Columns["colFechaRecepcion"] != null)
                {
                    dgvMuestras.Columns["colFechaRecepcion"].DataPropertyName = nameof(MuestraDetalleViewModel.FechaRecepcion);
                    dgvMuestras.Columns["colFechaRecepcion"].DefaultCellStyle.Format = "dd/MM/yyyy"; // Formato fecha
                }

                if (dgvMuestras.Columns["colExamenesSolicitados"] != null)
                    dgvMuestras.Columns["colExamenesSolicitados"].DataPropertyName = nameof(MuestraDetalleViewModel.ExamenesSolicitados);

                if (dgvMuestras.Columns["colEstadoMuestra"] != null)
                    dgvMuestras.Columns["colEstadoMuestra"].DataPropertyName = nameof(MuestraDetalleViewModel.EstadoMuestra);

                // Ocultar la columna ID si no la quieres ver (asumiendo que no la añadiste al diseñador)
                // if(dgvMuestras.Columns[nameof(MuestraDetalleViewModel.IdMuestra)] != null)
                //    dgvMuestras.Columns[nameof(MuestraDetalleViewModel.IdMuestra)].Visible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error configurando columnas de Muestras: {ex.Message}");
                // No mostrar MessageBox aquí, podría ser molesto si falla a menudo
            }
        }
        private void ConfigurarColumnasExamenes()
        {
            // *** OJO: Asegúrate que los nombres de columna ("colNumMuestraExamen", etc.)
            // *** coincidan con los 'Name' del Diseñador, y los DataPropertyName
            // *** con las propiedades de ExamenDetalleViewModel.

            try
            {
                if (dgvExamenes.Columns["colNumMuestraExamen"] != null)
                    dgvExamenes.Columns["colNumMuestraExamen"].DataPropertyName = nameof(ExamenDetalleViewModel.NumeroMuestra);

                if (dgvExamenes.Columns["colPacienteExamen"] != null)
                    dgvExamenes.Columns["colPacienteExamen"].DataPropertyName = nameof(ExamenDetalleViewModel.NombrePaciente);

                if (dgvExamenes.Columns["colTipoExamen"] != null)
                    dgvExamenes.Columns["colTipoExamen"].DataPropertyName = nameof(ExamenDetalleViewModel.TipoExamen);

                if (dgvExamenes.Columns["colFechaProcesamiento"] != null)
                {
                    dgvExamenes.Columns["colFechaProcesamiento"].DataPropertyName = nameof(ExamenDetalleViewModel.FechaProcesamiento);
                    dgvExamenes.Columns["colFechaProcesamiento"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"; // Incluir hora
                }

                if (dgvExamenes.Columns["colEstadoExamen"] != null)
                    dgvExamenes.Columns["colEstadoExamen"].DataPropertyName = nameof(ExamenDetalleViewModel.Estado);

                // TODO: Añadir columna botón "Ver Detalles Examen" en el diseñador si se quiere
                // if(dgvExamenes.Columns["colVerExamenDetalle"] != null) { ... }

            }
            catch (Exception ex) { Console.WriteLine($"Error configurando columnas de Exámenes: {ex.Message}"); }
        }
        // ---  Para ajustar AutoSize después de cargar ---
        private void AjustarAutoSizeGrid(DataGridView dgv)
        {
            try
            {
                dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                // Opcional: Poner alguna columna en modo Fill después del ajuste inicial
                // if(dgv.Columns["colNombrePacienteMuestra"] != null) {
                //    dgv.Columns["colNombrePacienteMuestra"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                // }
            }
            catch (Exception ex) { Console.WriteLine($"Error ajustando AutoSize para {dgv.Name}: {ex.Message}"); }
        }
        #endregion
        #region Eventos de UI
        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close(); // Simplemente cierra el formulario
        }
        private void pnlInfoBasica_Paint(object sender, PaintEventArgs e)
        {

        }
        private void dgvExamenes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Asegurarse que no sea el header y que sea la columna del botón nuevo
            if (e.RowIndex >= 0 && dgvExamenes.Columns[e.ColumnIndex].Name == "colVerExamenDetalle")
            {
                // Obtener el ViewModel de la fila clickeada
                if (dgvExamenes.Rows[e.RowIndex].DataBoundItem is ExamenDetalleViewModel examenSeleccionado)
                {
                    // Obtenemos el ID de la MUESTRA asociada a este examen
                    int idMuestra = examenSeleccionado.IdMuestra;
                    Console.WriteLine($"Clic en Ver Detalles para Examen ID: {examenSeleccionado.IdExamen}, Muestra ID: {idMuestra}");

                    // --- Abrir wProcesarResultados en modo VISTA ---
                    try
                    {
                        // *** OJO: Asumimos que wProcesarResultados puede funcionar
                        // *** principalmente con idMuestra y el flag modoVista.
                        // *** Puede que necesites ajustar el constructor/Load de wProcesarResultados
                        // *** para que busque los detalles de paciente/fecha/etc. usando el idMuestra.
                        using (var formResultados = new wProcesarResultados(
     examenSeleccionado.IdMuestra,
     examenSeleccionado.NumeroMuestra,
     examenSeleccionado.FechaRecepcion, // Ya está disponible en el ViewModel
     examenSeleccionado.NombrePaciente, // Ya está disponible
     modoVista: true
     ))
                        // --- FIN LÍNEA CORREGIDA ---
                        {
                            formResultados.ShowDialog(this);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al intentar abrir los detalles del examen:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine($"Error abriendo wProcesarResultados: {ex}");
                    }
                    // --- Fin Abrir ---
                }
                else
                {
                    MessageBox.Show("No se pudo obtener la información del examen seleccionado de la fila.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion
    } // Fin clase wVerDetallesProyecto
} // Fin namespace