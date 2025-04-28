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

        // --- Carga inicial de ComboBoxes de Filtro ---
        private void CargarFiltrosIniciales()
        {
            // Definir IDs especiales según tu nueva propuesta
            const int ID_SELECCIONE = 0;  // Valor para "Seleccione..."
            const int ID_TODOS = 1;       // Valor para "(Todos)"
                                          // Los IDs reales de la DB deben ser > 1

            var listaParaCombo = new List<Proyecto>();
            bool proyectosCargadosOk = false;

            try
            {
                if (proyectoRepository == null) proyectoRepository = new ProyectoRepository();
                Console.WriteLine("DEBUG: [CargarFiltrosIniciales] Cargando proyectos...");

                // 1. Añadir opción "Seleccione..." con ID 0
                listaParaCombo.Add(new Proyecto { id_proyecto = ID_SELECCIONE, nombre = "Seleccione proyecto..." });
                // 2. Añadir opción "(Todos)" con ID 1
                listaParaCombo.Add(new Proyecto { id_proyecto = ID_TODOS, nombre = "(Todos los Proyectos)" });

                // 3. Obtener y añadir proyectos activos (cuyos IDs deben ser > 1)
                var proyectos = proyectoRepository.ObtenerProyectos(incluirArchivados: false);
                listaParaCombo.AddRange(proyectos);

                // Asignar al ComboBox (Desconectando/Reconectando evento para seguridad)
                cmbProyectoVer.SelectedIndexChanged -= FiltroCambiado;
                cmbProyectoVer.DataSource = null;
                cmbProyectoVer.Items.Clear();
                cmbProyectoVer.DataSource = listaParaCombo;
                cmbProyectoVer.DisplayMember = "nombre";
                cmbProyectoVer.ValueMember = "id_proyecto";

                // 4. Seleccionar "Seleccione..." (ID=0) por defecto
                cmbProyectoVer.SelectedValue = ID_SELECCIONE;

                cmbProyectoVer.Enabled = true;
                proyectosCargadosOk = true;
                Console.WriteLine($"DEBUG: [CargarFiltrosIniciales] Proyectos cargados. Default: {cmbProyectoVer.Text}");
                cmbProyectoVer.SelectedIndexChanged += FiltroCambiado; // Reconectar

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
        // --- Configuración del DataGridView mediante Código ---
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

        // --- Event Handlers que llaman a la búsqueda ---
        private void FiltroCambiado(object sender, EventArgs e) { RealizarBusqueda(); }
        private void btnBuscar_Click(object sender, EventArgs e) { RealizarBusqueda(); }
        private void txtBuscar_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { RealizarBusqueda(); e.SuppressKeyPress = true; } }

        // --- Método Central de Búsqueda ---
        // --- MÉTODO ACTUALIZADO: RealizarBusqueda con chequeo inicial ---
        // --- MÉTODO ACTUALIZADO: RealizarBusqueda con chequeo inicial CORREGIDO ---
        // --- MÉTODO ACTUALIZADO: RealizarBusqueda (SIN chequeo inicial) ---
        private async void RealizarBusqueda()
        {
            Console.WriteLine($"DEBUG: ***** RealizarBusqueda() llamada a las {DateTime.Now:HH:mm:ss.fff} *****");

            // Definir IDs especiales (igual que en CargarFiltrosIniciales)
            const int ID_SELECCIONE = 0;
            const int ID_TODOS = 1;

            // --- Leer filtros actuales ---
            int? idProyectoFiltrar = null; // Null significa buscar en TODOS
            int selectedValue = ID_SELECCIONE; // Default a "Seleccione"
            if (cmbProyectoVer.SelectedValue is int val)
            {
                selectedValue = val;
            }

            // *** NUEVA LÓGICA DE FILTRO PROYECTO ***
            if (selectedValue == ID_SELECCIONE)
            {
                Console.WriteLine("DEBUG: [RealizarBusqueda] 'Seleccione proyecto...' elegido. Limpiando grid.");
                dgvPacientes.DataSource = null; // Limpiar tabla
                                                // Actualizar visibilidad de columnas por si acaso
                if (dgvPacientes.Columns["colProyecto"] != null) dgvPacientes.Columns["colProyecto"].Visible = false;
                if (dgvPacientes.Columns["colResumenMuestras"] != null) dgvPacientes.Columns["colResumenMuestras"].Visible = false;
                if (dgvPacientes.Columns["colResumenExamenes"] != null) dgvPacientes.Columns["colResumenExamenes"].Visible = false;
                return; // No hacer nada más
            }
            else if (selectedValue == ID_TODOS)
            {
                idProyectoFiltrar = null; // Buscar en todos
                Console.WriteLine("DEBUG: [RealizarBusqueda] Buscando en (Todos los Proyectos).");
            }
            else if (selectedValue > ID_TODOS)
            { // Mayor que 1 (ID real de proyecto)
                idProyectoFiltrar = selectedValue; // Buscar en proyecto específico
                Console.WriteLine($"DEBUG: [RealizarBusqueda] Buscando en Proyecto ID: {idProyectoFiltrar}.");
            }
            // **************************************

            // Leer otros filtros (igual que antes)
            string? filtroGenero = (cmbFiltroGenero.SelectedIndex > 0) ? cmbFiltroGenero.SelectedItem.ToString() : null;
            bool? filtroConMuestras = chkFiltroConMuestras.Checked ? true : (bool?)null;
            bool? filtroConExamenes = chkFiltroConExamenes.Checked ? true : (bool?)null;
            string? criterio = string.IsNullOrWhiteSpace(txtBuscar.Text) ? null : txtBuscar.Text.Trim();

            // --- UI de carga (igual) ---
            this.Cursor = Cursors.WaitCursor;
            dgvPacientes.DataSource = null;

            List<PacienteViewModel> resultados = new List<PacienteViewModel>();

            try
            {
                // Llamada al Repositorio (la firma no cambia, solo el valor de idProyectoFiltrar)
                Console.WriteLine($"DEBUG: Buscando con criterio='{criterio}', idProyecto={idProyectoFiltrar}, genero={filtroGenero}, conMuestras={filtroConMuestras}, conExamenes={filtroConExamenes}");
                if (pacienteRepository == null) pacienteRepository = new PacienteRepository();

                // >>> ¡¡¡ ASEGÚRATE QUE EL MÉTODO DEL REPO ESTÉ IMPLEMENTADO !!! <<<
                resultados = await Task.Run(() =>
                    pacienteRepository.BuscarPacientesCompleto(criterio, idProyectoFiltrar, filtroGenero, filtroConMuestras, filtroConExamenes)
                );
                // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                dgvPacientes.DataSource = resultados;

                // Actualizar Visibilidad Columnas (La lógica no cambia)
                bool mostrarColumnaProyecto = (idProyectoFiltrar == null); // Mostrar si se buscaron TODOS
                bool mostrarColumnaMuestras = chkFiltroConMuestras.Checked;
                bool mostrarColumnaExamenes = chkFiltroConExamenes.Checked;
                // ... (código para poner .Visible = ... a las columnas) ...
                if (dgvPacientes.Columns["colProyecto"] != null) dgvPacientes.Columns["colProyecto"].Visible = mostrarColumnaProyecto;
                if (dgvPacientes.Columns["colResumenMuestras"] != null) dgvPacientes.Columns["colResumenMuestras"].Visible = mostrarColumnaMuestras;
                if (dgvPacientes.Columns["colResumenExamenes"] != null) dgvPacientes.Columns["colResumenExamenes"].Visible = mostrarColumnaExamenes;

                Console.WriteLine($"DEBUG: Búsqueda completada. {resultados?.Count ?? 0} resultados encontrados.");
            }
            catch (Exception ex) { /* ... manejo error ... */ MessageBox.Show($"Error buscando: {ex.Message}"); }
            finally { this.Cursor = Cursors.Default; /* ... mensaje si no hay resultados ... */ }
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
        // --- Definición del ViewModel (Poner fuera de la clase wVerPaciente) ---
       
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