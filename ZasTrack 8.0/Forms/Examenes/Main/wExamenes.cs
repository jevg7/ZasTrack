    using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ZasTrack.Forms.Examenes.Debug;
using ZasTrack.Models;
using ZasTrack.Models.ExamenModel;
using ZasTrack.Repositories;

namespace ZasTrack.Forms.Examenes
{
    public partial class wExamenes : Form
    {
        private ProyectoRepository proyectoRepository;
        private ExamenRepository examenRepository;
        private int ultimoProyectoSeleccionado = -1;
        private Button btnClearFilters => btnLimpiarFiltros;
        private bool mostrandoRecepcionados = true; // Inicia mostrando Recepcionados


        public wExamenes()
        {
            InitializeComponent();
            proyectoRepository = new ProyectoRepository();
            examenRepository = new ExamenRepository();

            // --- Asignación de Eventos ---
            cmbProyecto.SelectedIndexChanged += cmbProyecto_SelectedIndexChanged;
            dtpFechaRecepcion.ValueChanged += dtpFechaRecepcion_ValueChanged;
            chkFiltroOrina.CheckedChanged += FiltroTipoExamen_CheckedChanged;
            chkFiltroHeces.CheckedChanged += FiltroTipoExamen_CheckedChanged;
            chkFiltroSangre.CheckedChanged += FiltroTipoExamen_CheckedChanged;
            txtSearch.KeyDown += txtSearch_KeyDown;         // Evento para buscar con Enter
            btnActualizar.Click += btnActualizar_Click;     // Evento para botón Actualizar
            btnClearFilters.Click += btnLimpiarFiltros_Click; // Evento para botón Limpiar
            btnVerRecepcionados.Click += btnVerRecepcionados_Click; // <- Verifica nombre btnVerRecepcionados
            btnVerProcesados.Click += btnVerProcesados_Click;   // <- Verifica nombre btnVerProcesados


            dtpFechaRecepcion.Value = DateTime.Today;
            CargarProyectos();
            ActualizarAparienciaBotonesVista();

        }

        private void CargarProyectos()
        {
            var proyectos = proyectoRepository.ObtenerProyectos();
            cmbProyecto.DataSource = proyectos;
            cmbProyecto.DisplayMember = "nombre";
            cmbProyecto.ValueMember = "id_proyecto";
            cmbProyecto.SelectedIndex = -1;
            pnlPacientes.Controls.Clear();
        }

        private void cmbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProyecto.SelectedItem is Proyecto p)
            {
                ultimoProyectoSeleccionado = p.id_proyecto;
                RecargarListaSiEsNecesario();
            }
            else
            {
                ultimoProyectoSeleccionado = -1;
                pnlPacientes.Controls.Clear();
            }
        }
        private void AddTitlePanel()
        {
            // Verifica si ya existe por nombre para evitar duplicados
            if (pnlPacientes.Controls.ContainsKey("titlePanel")) return;

            var titlePanel = new Panel
            {
                Height = 30,
                Dock = DockStyle.Top,
                BackColor = Color.LightGray,
                Name = "titlePanel" // Le damos nombre al panel contenedor
            };

            // Añade los otros labels como antes...
            titlePanel.Controls.Add(CreateLabel("Muestra", 10));
            titlePanel.Controls.Add(CreateLabel("Paciente", 120));
            titlePanel.Controls.Add(CreateLabel("Género", 270));
            titlePanel.Controls.Add(CreateLabel("Edad", 360));

            Label labelEstado = CreateLabel("Exámenes Pendientes", 420); // Texto inicial
            labelEstado.Name = "lblTituloColumnaEstado"; // Nombre único
            titlePanel.Controls.Add(labelEstado); // Añádelo al panel
                                                  // ***** FIN CAMBIO *****

            titlePanel.Controls.Add(CreateLabel("Fecha Recepción", 600));
            titlePanel.Controls.Add(CreateLabel("Acciones", 750));

            pnlPacientes.Controls.Add(titlePanel);
            // No es necesario SetChildIndex si es el primero añadido después de Clear()
        }


        // En wExamenesNoRecep.cs

        // ***** MÉTODO RENOMBRADO Y ADAPTADO PARA MOSTRAR LA LISTA *****
        // Recibe la lista de pacientes/muestras y un booleano indicando la vista actual
        private void MostrarListaMuestras(List<MuestraInfoViewModel> pacientes, bool esVistaRecepcionados)
        {
            Control focusedControl = this.ActiveControl;
            pnlPacientes.SuspendLayout();
            pnlPacientes.Controls.Clear();
            AddTitlePanel(); // Añade títulos (ahora el label de estado tiene nombre)

            // --- CÓDIGO CORREGIDO: Ajustar título de la columna de estado ---
            // 1. Busca el panel de títulos por su nombre dentro de pnlPacientes
            Panel panelDeTitulos = pnlPacientes.Controls.Find("titlePanel", false).FirstOrDefault() as Panel;

            if (panelDeTitulos != null) // Si se encontró el panel de títulos...
            {
                // 2. Busca el Label específico por su nombre DENTRO del panel de títulos
                Label lblTituloColumnaEstado = panelDeTitulos.Controls.Find("lblTituloColumnaEstado", false).FirstOrDefault() as Label;

                if (lblTituloColumnaEstado != null) // Si se encontró el label...
                {
                    // 3. Cambia el texto según la vista
                    lblTituloColumnaEstado.Text = esVistaRecepcionados ? "Exámenes Pendientes" : "Exámenes Realizados";
                }
                else { Console.WriteLine("WARN: No se encontró lblTituloColumnaEstado dentro de titlePanel."); }
            }
            else { Console.WriteLine("WARN: No se encontró titlePanel dentro de pnlPacientes."); }
            // --- FIN CÓDIGO CORREGIDO ---

            try
            {
                // Verifica si la lista de pacientes que se recibió está vacía
                if (!pacientes.Any())
                {
                    // Define el mensaje apropiado según la vista activa
                    string textoNoEncontrado = esVistaRecepcionados
                        ? "No se encontraron muestras pendientes con los filtros aplicados."
                        : "No se encontraron muestras procesadas con los filtros aplicados.";

                    // Crea y añade una etiqueta para informar al usuario
                    pnlPacientes.Controls.Add(new Label
                    {
                        Text = textoNoEncontrado,
                        Location = new Point(10, 40), // Posición debajo de la fila de títulos
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        ForeColor = Color.DarkOrange
                    });
                }
                else // Si la lista SÍ tiene pacientes/muestras
                {
                    CultureInfo culturaEsp = new CultureInfo("es-NI"); // Para formato de fecha/día

                    // Itera sobre cada elemento en la lista de pacientes/muestras
                    foreach (var pac in pacientes)
                    {
                        // Crea un nuevo Panel para representar esta fila
                        var panel = new Panel
                        {
                            Height = 60,
                            Dock = DockStyle.Top, // Se apilará debajo del anterior
                            BackColor = Color.White,
                            BorderStyle = BorderStyle.FixedSingle
                        };

                        // --- Creación de etiquetas y botón para esta fila ---

                        // 1. Etiqueta Muestra ("Dia #Num")
                        string diaSemana = pac.FechaRecepcion.ToString("dddd", culturaEsp);
                        if (!string.IsNullOrEmpty(diaSemana))
                        {
                            diaSemana = char.ToUpper(diaSemana[0]) + diaSemana.Substring(1);
                        }
                        panel.Controls.Add(CreateLabel($"{diaSemana} #{pac.NumeroMuestra}", 10));

                        // 2. Etiqueta Paciente (guardamos referencia para resaltado)
                        Label lblPaciente = CreateLabel(pac.Paciente, 120);
                        panel.Controls.Add(lblPaciente);

                        // 3. Etiquetas Género y Edad
                        panel.Controls.Add(CreateLabel(pac.Genero, 270));
                        panel.Controls.Add(CreateLabel(pac.Edad.ToString(), 360));

                        // 4. Etiqueta Estado / Exámenes Pendientes (CONTENIDO CONDICIONAL)
                        //    (Opcional) Cambiar el título de la columna una sola vez fuera del loop si es necesario
                        string estadoExamenesTexto = esVistaRecepcionados ? pac.ExamenesPendientesStr : pac.ExamenesCompletadosStr;

                        // Fallback por si acaso viene vacío/null
                        if (!esVistaRecepcionados && string.IsNullOrEmpty(estadoExamenesTexto))
                        {
                            estadoExamenesTexto = "[N/A]";
                        }
                        panel.Controls.Add(CreateLabel(estadoExamenesTexto, 420));

                        // 5. Etiqueta Fecha Recepción
                        panel.Controls.Add(CreateLabel(pac.FechaRecepcion.ToShortDateString(), 600));

                        // 6. Botón Acción (TEXTO CONDICIONAL)
                        // Dentro del foreach en MostrarListaMuestras

                        var btn = new Button
                        {
                            Text = esVistaRecepcionados ? "Procesar" : "Ver Detalles",
                            Location = new Point(750, 15),
                            Size = new Size(80, 30),
                            // ***** CAMBIO AQUÍ: Guarda el objeto 'pac' COMPLETO en el Tag *****
                            Tag = pac
                            // ***** FIN CAMBIO *****
                        };
                        btn.Click += BtnAccion_Click;
                        panel.Controls.Add(btn);
                        // --- Fin Creación ---

                        // --- Lógica de Resaltado (Corregida) ---
                        lblPaciente.BackColor = panel.BackColor; // Color normal por defecto
                        string textoBusqueda = txtSearch.Text.Trim(); // Necesita el texto actual
                        if (!string.IsNullOrWhiteSpace(textoBusqueda))
                        {
                            bool coincidencia = (pac.Paciente.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                 pac.NumeroMuestra.ToString().Contains(textoBusqueda));
                            if (coincidencia)
                            {
                                lblPaciente.BackColor = Color.Yellow; // Resalta si coincide
                            }
                        }
                        // --- Fin Resaltado ---

                        // --- Añadir y Ordenar Panel (Tu método que funciona) ---
                        pnlPacientes.Controls.Add(panel); // Añade el panel
                        pnlPacientes.Controls.SetChildIndex(panel, 0); // Mueve al índice 0 para tu layout
                                                                       // --- Fin Añadir y Ordenar ---

                    } // Fin foreach
                } // Fin else (si hay pacientes)
            }
            catch (Exception ex)
            {
                // Muestra un mensaje de error si algo falla al mostrar los datos
                MessageBox.Show($"Error al mostrar las muestras: {ex.Message}", "Error de Interfaz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                // Reanuda el layout del panel para que se apliquen todos los cambios visuales
                pnlPacientes.ResumeLayout(true);

                // Opcional: Intenta devolver el foco al control que lo tenía antes de recargar
                // focusedControl?.Focus();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            RecargarListaSiEsNecesario();
        }

        // En wExamenesNoRecep.cs

        // En wExamenes.cs

        private void BtnAccion_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is MuestraInfoViewModel pac)
            {
                // Variable para guardar el resultado del dialogo
                DialogResult result = DialogResult.None;

                if (mostrandoRecepcionados) // Botón "Procesar"
                {
                    using (var modalForm = new wProcesarResultados(pac.id_Muestra, pac.NumeroMuestra, pac.FechaRecepcion, pac.Paciente))
                    {
                        // Muestra el formulario modal
                        result = modalForm.ShowDialog(this);
                    } // El 'using' asegura que se liberen recursos aquí
                }
                else // Botón "Ver Detalles"
                {
                    using (var modalForm = new wProcesarResultados(pac.id_Muestra, pac.NumeroMuestra, pac.FechaRecepcion, pac.Paciente, modoVista: true))
                    {
                        // Muestra el formulario modal en modo vista
                        result = modalForm.ShowDialog(this);
                    } // El 'using' asegura que se liberen recursos aquí
                }

                // Recargar SIEMPRE después de cerrar el modal
                RecargarListaSiEsNecesario();

            }
            else
            {
                MessageBox.Show("Error: No se pudo obtener la información de la muestra desde el botón.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private Label CreateLabel(string text, int x)
             => new Label
             {
                 Text = text,
                 Location = new Point(x, 5),
                 AutoSize = true,
                 Font = new Font("Segoe UI", 9)
             };



        private void RecargarListaSiEsNecesario()
        {
            if (ultimoProyectoSeleccionado != -1)
            {
                // 1. Recopila TODOS los filtros actuales
                List<int> tiposSeleccionados = ObtenerTiposExamenSeleccionados();
                DateTime fechaSeleccionada = dtpFechaRecepcion.Value.Date;
                string textoBusqueda = txtSearch.Text.Trim(); // Asume que txtSearch es el nombre correcto

                List<MuestraInfoViewModel> pacientes = new List<MuestraInfoViewModel>(); // Inicializa lista

                try
                {
                    // --- Decide qué método del repositorio llamar ---
                    if (mostrandoRecepcionados)
                    {
                        // Llama al método existente para pendientes/recepcionados
                        pacientes = examenRepository.ObtenerPacientesPorProyecto(ultimoProyectoSeleccionado, fechaSeleccionada, tiposSeleccionados, textoBusqueda);
                    }
                    else // Estamos mostrando los Procesados
                    {
                        // Llama al NUEVO método para procesados (¡asegúrate que exista en ExamenRepository!)
                        pacientes = examenRepository.ObtenerPacientesProcesados(ultimoProyectoSeleccionado, fechaSeleccionada, tiposSeleccionados, textoBusqueda);
                        // ¡¡La línea que asignaba una lista vacía aquí FUE BORRADA!!
                    }
                    // --- Fin decisión ---
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al obtener la lista de muestras: {ex.Message}", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pacientes = new List<MuestraInfoViewModel>(); // Asegura lista vacía en caso de error
                }

                // 4. Llama al método que DIBUJA la lista, pasando los datos y el modo
                MostrarListaMuestras(pacientes, mostrandoRecepcionados); // Llama al método renombrado
            }
            else
            {
                // Si no hay proyecto, limpia y muestra títulos
                pnlPacientes.Controls.Clear();
                AddTitlePanel();
            }
        }
        private List<int> ObtenerTiposExamenSeleccionados()
        {
            var tipos = new List<int>();
            if (chkFiltroOrina.Checked) tipos.Add(1); // ID Orina = 1
            if (chkFiltroHeces.Checked) tipos.Add(2); // ID Heces = 2
            if (chkFiltroSangre.Checked) tipos.Add(3); // ID Sangre = 3
            return tipos;
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            // Guarda el estado actual para evitar recargas innecesarias por eventos
            bool projectWasSelected = (ultimoProyectoSeleccionado != -1);

            // Desactivar eventos temporalmente (opcional pero más limpio)
            dtpFechaRecepcion.ValueChanged -= dtpFechaRecepcion_ValueChanged;
            chkFiltroOrina.CheckedChanged -= FiltroTipoExamen_CheckedChanged;
            chkFiltroHeces.CheckedChanged -= FiltroTipoExamen_CheckedChanged;
            chkFiltroSangre.CheckedChanged -= FiltroTipoExamen_CheckedChanged;

            // Resetear controles
            cmbProyecto.SelectedIndex = -1;
            dtpFechaRecepcion.Value = DateTime.Today;
            chkFiltroOrina.Checked = false;
            chkFiltroHeces.Checked = false;
            chkFiltroSangre.Checked = false;
            txtSearch.Text = "";

            // Reactivar eventos
            dtpFechaRecepcion.ValueChanged += dtpFechaRecepcion_ValueChanged;
            chkFiltroOrina.CheckedChanged += FiltroTipoExamen_CheckedChanged;
            chkFiltroHeces.CheckedChanged += FiltroTipoExamen_CheckedChanged;
            chkFiltroSangre.CheckedChanged += FiltroTipoExamen_CheckedChanged;

            // Actualizar estado y limpiar panel
            ultimoProyectoSeleccionado = -1;
            pnlPacientes.Controls.Clear();


        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter en el cuadro
            if (e.KeyCode == Keys.Enter)
            {
                RecargarListaSiEsNecesario();
                e.SuppressKeyPress = true;
            }
        }
        private void btnVerRecepcionados_Click(object sender, EventArgs e)
        {
            if (!mostrandoRecepcionados) // Solo recarga si no estaba ya activa
            {
                mostrandoRecepcionados = true;
                ActualizarAparienciaBotonesVista(); // Actualiza qué botón se ve activo
                RecargarListaSiEsNecesario();    // Recarga la lista para la nueva vista
            }
        }

        private void btnVerProcesados_Click(object sender, EventArgs e)
        {
            if (mostrandoRecepcionados) // Solo recarga si no estaba ya activa
            {
                mostrandoRecepcionados = false;
                ActualizarAparienciaBotonesVista(); // Actualiza qué botón se ve activo
                RecargarListaSiEsNecesario();    // Recarga la lista para la nueva vista
            }
        }


        private void ActualizarAparienciaBotonesVista()
        {
            // Asume que tus botones se llaman btnVerRecepcionados y btnVerProcesados
            // Puedes ajustar los colores o usar FlatStyle, etc.
            if (mostrandoRecepcionados)
            {
                btnVerRecepcionados.BackColor = Color.DodgerBlue; // Color activo
                btnVerRecepcionados.ForeColor = Color.White;
                btnVerProcesados.BackColor = SystemColors.Control; // Color inactivo
                btnVerProcesados.ForeColor = SystemColors.ControlText;
            }
            else
            {
                btnVerRecepcionados.BackColor = SystemColors.Control; // Color inactivo
                btnVerRecepcionados.ForeColor = SystemColors.ControlText;
                btnVerProcesados.BackColor = Color.MediumSeaGreen; // Color activo
                btnVerProcesados.ForeColor = Color.White;
            }
        }



        #region RecargarLista Windows Metodos
        private void dtpFechaRecepcion_ValueChanged(object sender, EventArgs e)
        {
            RecargarListaSiEsNecesario(); // Usa el método centralizado
        }

        private void FiltroTipoExamen_CheckedChanged(object sender, EventArgs e)
        {
            RecargarListaSiEsNecesario(); // Recarga al cambiar checkbox
        }
        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            RecargarListaSiEsNecesario(); // Usa el método centralizado

        }
        #endregion
        #region Windows Form Designer generated code
        private void dtpFechaRecepcion_ValueChanged_1(object sender, EventArgs e)
        {

        }
        private void pnlPacientes_Paint(object sender, PaintEventArgs e)
        {
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion

        private void btnVerProcesados_Click_1(object sender, EventArgs e)
        {

        }
    }
}
