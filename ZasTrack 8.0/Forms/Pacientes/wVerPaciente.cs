using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Models;       // Asegurar acceso a Proyecto y pacientes
using ZasTrack.Models.ExamenModel;
using ZasTrack.Repositories;    // Asegurar acceso a repositorios
// using ZasTrack.ViewModels; // Si pones PacienteViewModel en otro namespace

namespace ZasTrack.Forms.Estudiantes // O tu namespace correcto (ej: Estudiantes)
{
    public partial class wVerPaciente : Form
    {
        private PacienteRepository pacienteRepository;
        private ProyectoRepository proyectoRepository;
        private const int ID_TODOS_PROYECTOS = -1; // Valor especial

        public wVerPaciente()
        {
            InitializeComponent();
            pacienteRepository = new PacienteRepository();
            proyectoRepository = new ProyectoRepository();

            // Asignar Handlers
            this.Load += wVerPaciente_Load;
            cmbProyectoVer.SelectedIndexChanged += FiltroCambiado;
            cmbFiltroGenero.SelectedIndexChanged += FiltroCambiado;
            chkFiltroConMuestras.CheckedChanged += FiltroCambiado; // Llama al mismo método
            chkFiltroConExamenes.CheckedChanged += FiltroCambiado; // Llama al mismo método
                                                                   // ------------------------------------
            btnBuscar.Click += btnBuscar_Click;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            dgvPacientes.CellContentClick += dgvPacientes_CellContentClick;
        }
        private void wVerPaciente_Load(object sender, EventArgs e)
        {
            CargarFiltrosIniciales();
            ConfigurarGridViaCodigo(); // Configurar columnas por código
            // Carga inicial opcional (ej: todos los pacientes)
        }

        private void CargarFiltrosIniciales()
        {
            const int ID_TODOS_PROYECTOS_CMB = 0; // ID especial para "(Todos los Proyectos)" en el ComboBox

            var listaParaCombo = new List<Proyecto>();
            bool proyectosCargadosOk = false;

            cmbProyectoVer.SelectedIndexChanged -= FiltroCambiado; // Desconectar evento temporalmente
            cmbProyectoVer.DataSource = null;
            cmbProyectoVer.Items.Clear();

            try
            {
                if (proyectoRepository == null) proyectoRepository = new ProyectoRepository();
                Console.WriteLine("DEBUG: [CargarFiltrosIniciales] Iniciando...");

                // 1. Añadir opción "(Todos los Proyectos)" con ID especial 0
                listaParaCombo.Add(new Proyecto { id_proyecto = ID_TODOS_PROYECTOS_CMB, nombre = "(Todos los Proyectos)" });

                // 2. Obtener y añadir proyectos activos (sus IDs reales de la BD)
                var proyectosActivos = proyectoRepository.ObtenerProyectos(incluirArchivados: false);
                if (proyectosActivos != null)
                {
                    listaParaCombo.AddRange(proyectosActivos);
                }

                // Asignar al ComboBox
                cmbProyectoVer.DataSource = listaParaCombo;
                cmbProyectoVer.DisplayMember = "nombre";
                cmbProyectoVer.ValueMember = "id_proyecto";

                // --- INICIO: Lógica para Placeholder ---
                cmbProyectoVer.SelectedIndex = -1; // No seleccionar nada por defecto
                cmbProyectoVer.Text = "Seleccione un proyecto..."; // Mostrar este texto como placeholder
                                                                   // --- FIN: Lógica para Placeholder ---

                cmbProyectoVer.Enabled = true;
                proyectosCargadosOk = true;
                Console.WriteLine($"DEBUG: [CargarFiltrosIniciales] Proyectos cargados. Placeholder: '{cmbProyectoVer.Text}'");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error crítico al cargar la lista de proyectos:\n{ex.Message}", "Error Carga Proyectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR CargarFiltrosIniciales (Proyectos): {ex.ToString()}");
                // Configurar ComboBox en estado de error
                cmbProyectoVer.DataSource = null;
                cmbProyectoVer.Items.Clear();
                cmbProyectoVer.Items.Add("Error al cargar");
                cmbProyectoVer.SelectedIndex = 0;
                cmbProyectoVer.Enabled = false;
                proyectosCargadosOk = false; // Marcar como fallo
            }
            finally
            {
                cmbProyectoVer.SelectedIndexChanged += FiltroCambiado; // Reconectar evento
                                                                       // NO llamamos a RealizarBusqueda aquí, esperamos selección del usuario
                dgvPacientes.DataSource = null; // Asegurar que el grid esté vacío inicialmente
                ActualizarVisibilidadColumnasCondicionales(null); // Ocultar columnas condicionales
            }

            // --- Cargar Género ---
            try
            {
                cmbFiltroGenero.Items.Clear();
                cmbFiltroGenero.Items.Add("Todos");
                cmbFiltroGenero.Items.Add("Masculino");
                cmbFiltroGenero.Items.Add("Femenino");
                cmbFiltroGenero.SelectedIndex = 0; // Todos por defecto
                cmbFiltroGenero.Enabled = true;
            }
            catch (Exception ex) { Console.WriteLine($"ERROR configurando cmbFiltroGenero: {ex.ToString()}"); cmbFiltroGenero.Enabled = false; }

            // --- Configurar CheckBoxes Muestras/Exámenes ---
            try
            {
                chkFiltroConMuestras.Checked = false; // Desmarcado por defecto
                chkFiltroConExamenes.Checked = false;
                chkFiltroConMuestras.Enabled = true;
                chkFiltroConExamenes.Enabled = true;
            }
            catch (Exception ex) { Console.WriteLine($"ERROR configurando CheckBoxes de filtro: {ex.ToString()}"); chkFiltroConMuestras.Enabled = false; chkFiltroConExamenes.Enabled = false; }

            // --- Habilitar controles de búsqueda ---
            // Habilitar búsqueda general si al menos los filtros se cargaron
            bool puedeBuscar = cmbProyectoVer.Enabled;
            txtBuscar.Enabled = puedeBuscar;
            btnBuscar.Enabled = puedeBuscar;


            Console.WriteLine("DEBUG: [CargarFiltrosIniciales] Finalizado.");
        }
        private void ConfigurarGridViaCodigo()
        {
            try
            {
                dgvPacientes.Columns.Clear(); // Limpiar columnas pre-existentes
                dgvPacientes.AutoGenerateColumns = false; // MUY IMPORTANTE
                dgvPacientes.AllowUserToAddRows = false;
                dgvPacientes.AllowUserToDeleteRows = false;
                dgvPacientes.ReadOnly = true; // Celdas no editables directamente
                dgvPacientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPacientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dgvPacientes.ColumnHeadersDefaultCellStyle.Font = new Font(dgvPacientes.Font, FontStyle.Bold); // Cabecera en negrita

                // --- Definir Columnas ---

                // ID Oculto (útil para eventos)
                var colIdPaciente = new DataGridViewTextBoxColumn
                {
                    Name = "colIdPaciente",
                    DataPropertyName = "id_paciente",
                    HeaderText = "ID",
                    Visible = false
                };
                dgvPacientes.Columns.Add(colIdPaciente);

                // Código Beneficiario
                var colCodigo = new DataGridViewTextBoxColumn
                {
                    Name = "colCodigo",
                    HeaderText = "Código",
                    DataPropertyName = "codigo_beneficiario",
                    Width = 120
                };
                dgvPacientes.Columns.Add(colCodigo);

                // Nombres
                var colNombres = new DataGridViewTextBoxColumn
                {
                    Name = "colNombres",
                    HeaderText = "Nombres",
                    DataPropertyName = "nombres",
                    Width = 150
                };
                dgvPacientes.Columns.Add(colNombres);

                // Apellidos
                var colApellidos = new DataGridViewTextBoxColumn
                {
                    Name = "colApellidos",
                    HeaderText = "Apellidos",
                    DataPropertyName = "apellidos",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill // Ocupar espacio restante
                };
                dgvPacientes.Columns.Add(colApellidos);

                // Género
                var colGenero = new DataGridViewTextBoxColumn
                {
                    Name = "colGenero",
                    HeaderText = "Género",
                    DataPropertyName = "genero",
                    Width = 90
                };
                dgvPacientes.Columns.Add(colGenero);

                // Fecha Nacimiento
                var colFechaNac = new DataGridViewTextBoxColumn
                {
                    Name = "colFechaNac",
                    HeaderText = "Fecha Nac.",
                    DataPropertyName = "fecha_nacimiento",
                    Width = 100
                };
                colFechaNac.DefaultCellStyle.Format = "dd/MM/yyyy"; // Formato de fecha
                dgvPacientes.Columns.Add(colFechaNac);

                // Resumen Muestras (Inicialmente Oculta)
                var colResumenMuestras = new DataGridViewTextBoxColumn
                {
                    Name = "colResumenMuestras",
                    HeaderText = "Muestras",
                    DataPropertyName = "ResumenMuestras",
                    Width = 150,
                    Visible = false
                };
                dgvPacientes.Columns.Add(colResumenMuestras);

                // Resumen Exámenes (Inicialmente Oculta)
                var colResumenExamenes = new DataGridViewTextBoxColumn
                {
                    Name = "colResumenExamenes",
                    HeaderText = "Exámenes",
                    DataPropertyName = "ResumenExamenes",
                    Width = 150,
                    Visible = false
                };
                dgvPacientes.Columns.Add(colResumenExamenes);

                // Proyecto (Inicialmente Oculta)
                var colProyecto = new DataGridViewTextBoxColumn
                {
                    Name = "colProyecto",
                    HeaderText = "Proyecto",
                    DataPropertyName = "NombreProyecto",
                    Width = 150,
                    Visible = false
                };
                dgvPacientes.Columns.Add(colProyecto);

                // Botón Editar/Ver
                var colEditar = new DataGridViewButtonColumn
                {
                    Name = "colEditar",
                    HeaderText = "Acción",
                    Text = "Editar/Ver",
                    UseColumnTextForButtonValue = true,
                    Width = 80,
                    FlatStyle = FlatStyle.Flat
                };
                colEditar.DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255); // Azul claro suave
                colEditar.DefaultCellStyle.ForeColor = Color.Black;
                dgvPacientes.Columns.Add(colEditar);

                // Botón Eliminar NO se añade aquí según tu última petición

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configurando la tabla de pacientes: {ex.Message}", "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR ConfigurarGridViaCodigo: {ex.ToString()}");
            }
        }
        private void FiltroCambiado(object sender, EventArgs e)
        {
            if (cmbProyectoVer.SelectedIndex != -1 || sender != cmbProyectoVer) // Si cambió un filtro que no es el combo, o si el combo tiene algo seleccionado
            {
                RealizarBusqueda();
            }
            else if (cmbProyectoVer.SelectedIndex == -1 && sender == cmbProyectoVer)
            {
                // Si el ComboBox de Proyecto se pone en "Seleccione..." (placeholder), limpiar el grid
                dgvPacientes.DataSource = null;
                ActualizarVisibilidadColumnasCondicionales(null); // Ocultar columnas opcionales
                Console.WriteLine("DEBUG: Placeholder seleccionado en cmbProyectoVer, grid limpiado.");
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e) { RealizarBusqueda(); }
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { RealizarBusqueda(); e.SuppressKeyPress = true; } }

        private async void RealizarBusqueda()
        {
            Console.WriteLine($"DEBUG: ***** RealizarBusqueda() llamada a las {DateTime.Now:HH:mm:ss.fff} *****");
            const int ID_TODOS_PROYECTOS_CMB = 0; // Debe coincidir con CargarFiltrosIniciales

            // --- Leer filtros actuales ---
            int? idProyectoFiltrar = null;

            // Verificar si hay algo seleccionado
            if (cmbProyectoVer.SelectedIndex == -1 || cmbProyectoVer.SelectedValue == null)
            {
                Console.WriteLine("DEBUG: [RealizarBusqueda] No hay proyecto seleccionado (placeholder activo). No se buscará.");
                dgvPacientes.DataSource = null; // Limpiar tabla
                ActualizarVisibilidadColumnasCondicionales(null);
                return; // No hacer nada si está el placeholder
            }

            // Obtener el valor seleccionado
            if (cmbProyectoVer.SelectedValue is int selectedValue)
            {
                if (selectedValue == ID_TODOS_PROYECTOS_CMB) // Si es "Todos los Proyectos"
                {
                    idProyectoFiltrar = null; // Null significa buscar en TODOS los proyectos ACTIVOS
                    Console.WriteLine("DEBUG: [RealizarBusqueda] Buscando en (Todos los Proyectos Activos).");
                }
                else // Es un ID de proyecto específico (debe ser > 0)
                {
                    idProyectoFiltrar = selectedValue;
                    Console.WriteLine($"DEBUG: [RealizarBusqueda] Buscando en Proyecto ID: {idProyectoFiltrar}.");
                }
            }
            else
            {
                // Caso inesperado, tratar como si no hubiera selección
                Console.WriteLine("DEBUG: [RealizarBusqueda] SelectedValue no es un int. Limpiando grid.");
                dgvPacientes.DataSource = null;
                ActualizarVisibilidadColumnasCondicionales(null);
                return;
            }

            // Leer otros filtros
            string? filtroGenero = (cmbFiltroGenero.SelectedIndex > 0) ? cmbFiltroGenero.SelectedItem.ToString() : null;
            bool? filtroConMuestras = chkFiltroConMuestras.Checked ? true : (bool?)null;
            bool? filtroConExamenes = chkFiltroConExamenes.Checked ? true : (bool?)null;
            string? criterio = string.IsNullOrWhiteSpace(txtBuscar.Text) ? null : txtBuscar.Text.Trim();

            // --- UI de carga ---
            this.Cursor = Cursors.WaitCursor;
            dgvPacientes.DataSource = null;
            List<PacienteViewModel> resultados = new List<PacienteViewModel>();

            try
            {
                Console.WriteLine($"DEBUG: Buscando con criterio='{criterio}', idProyecto={idProyectoFiltrar?.ToString() ?? "NULL"}, genero={filtroGenero}, conMuestras={filtroConMuestras}, conExamenes={filtroConExamenes}");
                if (pacienteRepository == null) pacienteRepository = new PacienteRepository();

                // Llamada al repositorio
                resultados = await Task.Run(() =>
                    pacienteRepository.BuscarPacientesCompleto(criterio, idProyectoFiltrar, filtroGenero, filtroConMuestras, filtroConExamenes)
                );

                dgvPacientes.DataSource = resultados;
                ActualizarVisibilidadColumnasCondicionales(idProyectoFiltrar); // Pasa el id para lógica de columnas
                Console.WriteLine($"DEBUG: Búsqueda completada. {resultados?.Count ?? 0} resultados encontrados.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error buscando: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR RealizarBusqueda: {ex}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        // En wVerPaciente.cs

        private void ActualizarVisibilidadColumnasCondicionales(int? idProyectoSeleccionado)
        {
            try
            {
                // Columna Proyecto: Visible solo si se seleccionó "(Todos los Proyectos)"
                if (dgvPacientes.Columns["colProyecto"] != null)
                {
                    // idProyectoSeleccionado será null cuando "(Todos los Proyectos)" (ID 0) esté elegido.
                    dgvPacientes.Columns["colProyecto"].Visible = (idProyectoSeleccionado == null);
                    Console.WriteLine($"DEBUG: colProyecto.Visible = {(idProyectoSeleccionado == null)}");
                }
                else { Console.WriteLine("WARN: Columna 'colProyecto' no encontrada en dgvPacientes."); }

                // Columna Resumen Muestras: Visible si el CheckBox está marcado
                if (dgvPacientes.Columns["colResumenMuestras"] != null)
                {
                    dgvPacientes.Columns["colResumenMuestras"].Visible = chkFiltroConMuestras.Checked;
                    Console.WriteLine($"DEBUG: colResumenMuestras.Visible = {chkFiltroConMuestras.Checked}");
                }
                else { Console.WriteLine("WARN: Columna 'colResumenMuestras' no encontrada en dgvPacientes."); }


                // Columna Resumen Exámenes: Visible si el CheckBox está marcado
                if (dgvPacientes.Columns["colResumenExamenes"] != null)
                {
                    dgvPacientes.Columns["colResumenExamenes"].Visible = chkFiltroConExamenes.Checked;
                    Console.WriteLine($"DEBUG: colResumenExamenes.Visible = {chkFiltroConExamenes.Checked}");
                }
                else { Console.WriteLine("WARN: Columna 'colResumenExamenes' no encontrada en dgvPacientes."); }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ActualizarVisibilidadColumnasCondicionales: {ex.Message}");
                // No mostrar MessageBox aquí para no ser intrusivo
            }
        }
        private void dgvPacientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignorar cabecera

            // Verificar si se hizo clic en la columna del botón Editar
            if (dgvPacientes.Columns[e.ColumnIndex].Name == "colEditar")
            {
                // Obtener el ViewModel de la fila clickeada
                if (dgvPacientes.Rows[e.RowIndex].DataBoundItem is PacienteViewModel pacienteVM)
                {
                    int idPaciente = pacienteVM.id_paciente;
                    Console.WriteLine($"Clic en Editar/Ver para Paciente ID: {idPaciente}");

                    // --- Abrir formulario wEditarEliminarPaciente ---
                    // Asumiendo que ese formulario tiene un constructor que acepta el ID
                    // y carga los datos del paciente para permitir editar O eliminar desde ahí.
                    try
                    {
                        // Asegúrate que este namespace y clase existan
                        Forms.Estudiantes.wEditarEliminarPaciente formEditarEliminar = new Forms.Estudiantes.wEditarEliminarPaciente(idPaciente);
                        // Mostrar como diálogo para que espere a que se cierre
                        DialogResult result = formEditarEliminar.ShowDialog(this);

                        // Si el formulario de edición/eliminación indicó que algo cambió (ej: se guardó o eliminó)
                        // volvemos a cargar la búsqueda actual para reflejar los cambios.
                        if (result == DialogResult.OK || result == DialogResult.Yes) // O cualquier lógica que uses para indicar cambio
                        {
                            Console.WriteLine("Actualizando lista después de Editar/Eliminar...");
                            RealizarBusqueda(); // Recargar el grid
                        }
                    }
                    catch (Exception exForm)
                    {
                        MessageBox.Show($"Error al abrir el formulario de edición/eliminación:\n{exForm.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // -------------------------------------------------
                }
            }
            // --- La lógica para la columna "colEliminar" se quitó ---
            // else if (dgvPacientes.Columns[e.ColumnIndex].Name == "colEliminar") { ... }
        }

        #region Windows Form Designer generated code


        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion
    } // Fin clase wVerPaciente

} // Fin namespace