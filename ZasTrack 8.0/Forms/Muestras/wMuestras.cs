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
                string connectionString  = "Host=aws-0-us-east-2.pooler.supabase.com;Username=postgres.qhvzvrxcuwipnwrnbwxd;Password=0uOCajlsEsiYdD1i;Database=postgres;Port=5432;SSL Mode=Require;Trust Server Certificate=true;Timeout=30;Include Error Detail=true";
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
            txtMuestrasId.Text = "";  
            txtIdPaciente.Text = ""; // <-- Limpiar ID paciente 
            txtPaciente.Text = "";   // <-- Limpiar nombre paciente 
            txtPaciente.Enabled = true; // <-- Habilitar para búsqueda
            txtBuscar.Text = "";      // <-- Limpiar búsqueda
        }

        #region Eventos  
            private void cmbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProyecto.SelectedItem is Proyecto proyectoSeleccionado && proyectoSeleccionado.id_proyecto > 0) // Asegura que sea un proyecto válido
            {
                int idProyecto = proyectoSeleccionado.id_proyecto;
                ultimoProyectoSeleccionado = idProyecto;
                try
                {
                    // Obtener y mostrar el siguiente número de muestra SOLO SI HAY PROYECTO VÁLIDO
                    int ultimoNum = muestraRepository.ObtenerUltimaMuestra(idProyecto, DateTime.Today); // Usar Today para consistencia diaria
                    txtMuestrasId.Text = (ultimoNum + 1).ToString();
                    txtMuestrasId.Enabled = false; // Asegurar que no se pueda editar
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al obtener el número de muestra para el proyecto:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMuestrasId.Text = "Error"; // Indicar error
                }
                // Habilitar búsqueda de paciente ahora que hay proyecto
                txtBuscar.Enabled = true;
                btnBuscar.Enabled = true;
            }
            else // Si no hay proyecto seleccionado o no es válido
            {
                ultimoProyectoSeleccionado = -1;
                txtMuestrasId.Text = ""; // <-- LIMPIAR el número de muestra
                txtPaciente.Text = ""; // Limpiar paciente
                txtIdPaciente.Text = "";
                txtPaciente.Enabled = true;
                txtBuscar.Enabled = false; // Deshabilitar búsqueda sin proyecto
                btnBuscar.Enabled = false;
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
            private void dgvResultadosBusqueda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Asegurarse que no sea el header
            if (e.RowIndex >= 0)
            {
                // Obtener el paciente seleccionado de la fila donde se hizo doble clic
                if (dgvResultadosBusqueda.Rows[e.RowIndex].DataBoundItem is pacientes pacienteSeleccionado) // Usa tu clase
                {
                    // Rellenar los campos principales con el paciente seleccionado
                    txtPaciente.Text = pacienteSeleccionado.nombres + " " + pacienteSeleccionado.apellidos;
                    txtIdPaciente.Text = pacienteSeleccionado.id_paciente.ToString();
                    txtPaciente.Enabled = false; // Deshabilitar

                    // Ocultar el grid de resultados
                    dgvResultadosBusqueda.DataSource = null;
                    dgvResultadosBusqueda.Visible = false;

                    // Mover el foco al siguiente paso lógico
                    chkHeces.Focus(); // O al primer CheckBox

                    Console.WriteLine($"DEBUG: Paciente seleccionado desde grid: {txtPaciente.Text} (ID: {txtIdPaciente.Text})");
                }
            }
        }
            private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    await BuscarPacAsync(); // Llama a la búsqueda asíncrona
                    e.SuppressKeyPress = true; // Evita el sonido 'ding'
                                               // *** PODRÍAMOS AÑADIR ESTO TAMBIÉN ***
                    e.Handled = true;          // Indica que ya manejamos la tecla
                }
            }

        #endregion
        #region Botones con Eventos
        private async void btnBuscar_Click(object sender, EventArgs e) 
                {
                    await BuscarPacAsync(); // Llamada asíncrona
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

        #endregion
        #region Metodos
            private async Task BuscarPacAsync() 
            {
                string criterio = txtBuscar.Text.Trim();
                int idProyecto = ultimoProyectoSeleccionado; // Usa el ID ya guardado

                if (idProyecto <= 0)
                {
                    MessageBox.Show("Por favor, seleccione un proyecto válido primero.", "Proyecto Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbProyecto.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(criterio))
                {
                    MessageBox.Show("Por favor, ingrese un nombre, apellido o código para buscar.", "Criterio Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBuscar.Focus();
                    return;
                }


                this.Cursor = Cursors.WaitCursor;
                txtPaciente.Text = "Buscando...";
                txtIdPaciente.Text = "";
                dgvResultadosBusqueda.Visible = false; // Ocultar grid al iniciar búsqueda
                dgvResultadosBusqueda.DataSource = null; // Limpiar datos anteriores
                List<pacientes> pacientes = null;

                try
                {
                    pacientes = await pacienteRepository.BuscarPacientesAsync(criterio, idProyecto);
                }
                catch (Exception ex)
                {
                    // ... (manejo de error igual que antes) ...
                    txtPaciente.Text = "[Error en búsqueda]";
                    this.Cursor = Cursors.Default;
                    return;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

                // Lógica para manejar resultados (igual que antes)
                if (pacientes == null || !pacientes.Any())
                {
                    txtPaciente.Text = ""; // Limpiar si no hay resultados
                    MessageBox.Show("No se encontraron pacientes...", "Sin Resultados");
                    // Asegurar que el grid siga oculto
                    dgvResultadosBusqueda.Visible = false;
                    txtBuscar.Focus();
                    txtBuscar.SelectAll();
                }
                else if (pacientes.Count > 1)
                {
                    // *** MÚLTIPLES RESULTADOS: Mostrar el Grid ***
                    txtPaciente.Text = "[Seleccione de la lista de abajo]"; // Indicar acción
                    txtIdPaciente.Text = "";
                    txtPaciente.Enabled = true; // Mantener habilitado para posible nueva búsqueda

                    // Configurar y mostrar el grid
                    dgvResultadosBusqueda.AutoGenerateColumns = false; // Asegurar que no autogenere
                    ConfigurarColumnasResultadosBusqueda(); // Mapear columnas a propiedades
                    dgvResultadosBusqueda.DataSource = pacientes; // Asignar datos
                    dgvResultadosBusqueda.Visible = true; // <-- HACER VISIBLE EL GRID
                    dgvResultadosBusqueda.BringToFront();
                }
                else // Exactamente un resultado
                {
                    dgvResultadosBusqueda.Visible = false; // Asegurar que esté oculto

                    txtPaciente.Text = pacientes[0].nombres + " " + pacientes[0].apellidos;
                    txtIdPaciente.Text = pacientes[0].id_paciente.ToString();
                    txtPaciente.Enabled = false; // Deshabilitar edición directa
                    Console.WriteLine($"DEBUG: Paciente seleccionado: {txtPaciente.Text} (ID: {txtIdPaciente.Text})");
                    chkHeces.Focus(); // Mover foco
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
            private async void guardarMuestra() 
            {
                // --- Validaciones iniciales (igual que antes) ---
                if (cmbProyecto.SelectedItem == null || !(cmbProyecto.SelectedItem is Proyecto proyectoSeleccionado) || proyectoSeleccionado.id_proyecto <= 0)
                {
                    MessageBox.Show("Debe seleccionar un proyecto válido.", "Proyecto no Seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning); cmbProyecto.Focus(); return;
                }
                if (string.IsNullOrWhiteSpace(txtIdPaciente.Text) || !int.TryParse(txtIdPaciente.Text, out int pacienteId) || pacienteId <= 0)
                {
                    MessageBox.Show("Debe buscar y seleccionar un paciente válido antes de guardar.", "Paciente no Seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtBuscar.Focus(); return;
                }
                if (proyectoSeleccionado.IsArchived)
                {
                    MessageBox.Show($"El proyecto '{proyectoSeleccionado.nombre}' está archivado...", "Proyecto Archivado", MessageBoxButtons.OK, MessageBoxIcon.Stop); return;
                }
                // Recopilar IDs de tipos de examen seleccionados
                var idsTiposExamenSeleccionados = new List<int>();
                if (chkOrina.Checked) idsTiposExamenSeleccionados.Add(1); // ID Orina = 1
                if (chkHeces.Checked) idsTiposExamenSeleccionados.Add(2); // ID Heces = 2
                if (chkSangre.Checked) idsTiposExamenSeleccionados.Add(3); // ID Sangre = 3

                if (!idsTiposExamenSeleccionados.Any())
                {
                    MessageBox.Show("Debe seleccionar al menos un tipo de examen.", "Tipo Examen Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                }
                // --- Fin Validaciones ---


                // --- Preparar datos ---
                int idProyecto = proyectoSeleccionado.id_proyecto;
                DateTime fechaActual = DateTime.Today; // Usar Today consistente con ObtenerUltimaMuestra
                int numeroMuestra = 0;

                this.Cursor = Cursors.WaitCursor; // Mostrar espera

                try
                {
                    // Obtener siguiente número de muestra (podría hacerse async también)
                    numeroMuestra = muestraRepository.ObtenerUltimaMuestra(idProyecto, fechaActual) + 1;
                    txtMuestrasId.Text = numeroMuestra.ToString(); // Actualizar UI por si acaso

                    Muestra muestra = new Muestra()
                    {
                        IdProyecto = idProyecto,
                        IdPaciente = pacienteId,
                        NumeroMuestra = numeroMuestra,
                        FechaRecepcion = fechaActual
                    };

                    Console.WriteLine($"DEBUG: Llamando a GuardarMuestraCompletaAsync para Muestra #{numeroMuestra} con {idsTiposExamenSeleccionados.Count} exámenes.");
                    int idNuevaMuestra = await muestraRepository.GuardarMuestraCompletaAsync(muestra, idsTiposExamenSeleccionados);
                    // --------------------------------------------

                    if (idNuevaMuestra > 0)
                    {
                        MessageBox.Show($"Muestra #{numeroMuestra} guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos(false); // Limpiar para siguiente muestra, manteniendo proyecto
                        txtBuscar.Clear();
                        txtBuscar.Focus();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo guardar la muestra debido a un error interno o de base de datos. Verifique la consola de depuración.", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Captura cualquier error inesperado durante el proceso
                    MessageBox.Show($"Ocurrió un error inesperado:\n{ex.Message}", "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"ERROR en guardarMuestra: {ex.ToString()}");
                }
                finally
                {
                    this.Cursor = Cursors.Default; // Restaurar cursor
                }
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
            private void ConfigurarColumnasResultadosBusqueda()
            {
                // *** OJO: Asegúrate que los nombres de columna ('colCodigoResultado', etc.)
                // *** coincidan con los 'Name' que les diste en el Diseñador para dgvResultadosBusqueda
                // *** y los DataPropertyName con tu clase 'pacientes' ***
                try
                {
                    if (dgvResultadosBusqueda.Columns["colCodigoResultado"] != null)
                        dgvResultadosBusqueda.Columns["colCodigoResultado"].DataPropertyName = "codigo_beneficiario";

                    if (dgvResultadosBusqueda.Columns["colNombresResultado"] != null)
                        dgvResultadosBusqueda.Columns["colNombresResultado"].DataPropertyName = "nombres";

                    if (dgvResultadosBusqueda.Columns["colApellidosResultado"] != null)
                        dgvResultadosBusqueda.Columns["colApellidosResultado"].DataPropertyName = "apellidos";

                    if (dgvResultadosBusqueda.Columns["colFechaNacResultado"] != null)
                    {
                        dgvResultadosBusqueda.Columns["colFechaNacResultado"].DataPropertyName = "fecha_nacimiento";
                        dgvResultadosBusqueda.Columns["colFechaNacResultado"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
                    // Ajustar AutoSize
                    dgvResultadosBusqueda.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                }
                catch (Exception ex) { Console.WriteLine($"Error configurando columnas dgvResultadosBusqueda: {ex.Message}"); }
            }
        #endregion
        #region Windows Form Designer generated code
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