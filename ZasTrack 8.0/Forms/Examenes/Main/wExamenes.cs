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
            List<Proyecto> proyectos = null;
            try
            {
                // --- CAMBIO AQUÍ ---
                proyectos = proyectoRepository.ObtenerProyectos(incluirArchivados: false); // <-- Añadir 'false'
                                                                                           // ------------------

                cmbProyecto.DataSource = proyectos;
                cmbProyecto.DisplayMember = "nombre";
                cmbProyecto.ValueMember = "id_proyecto";
                cmbProyecto.SelectedIndex = -1;
                cmbProyecto.Text = "Seleccione un proyecto..."; // Placeholder
            }
            catch (Exception ex)
            {
                // Mantener manejo de error si falla la carga
                MessageBox.Show($"Error al cargar la lista de proyectos activos:\n{ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR CargarProyectos (wExamenes): {ex.ToString()}");
                cmbProyecto.DataSource = null;
                cmbProyecto.Items.Clear();
                cmbProyecto.Items.Add("Error al cargar");
                cmbProyecto.SelectedIndex = 0;
                cmbProyecto.Enabled = false;
            }
            finally
            {
                // Limpiar la lista de muestras si cambiamos de proyecto (o al cargar inicialmente)
                LimpiarTarjetasDeMuestras(); // O usa flpListaMuestras.Controls.Clear() si no has creado el helper
                CrearOActualizarEncabezado(); // Asegura que el encabezado esté presente/actualizado
            }
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
                    flpListaMuestras.Controls.Clear();
                }
            }            
            
            private void RecargarListaSiEsNecesario()
            {
                CrearOActualizarEncabezado();
                
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
                            pacientes = examenRepository.ObtenerPacientesProcesados(ultimoProyectoSeleccionado, fechaSeleccionada, tiposSeleccionados, textoBusqueda);
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
                LimpiarTarjetasDeMuestras();

            }
        }
            private void LimpiarTarjetasDeMuestras()
            {
                // Elimina todos los controles EXCEPTO el panel de encabezado
                var controlesAEliminar = flpListaMuestras.Controls.Cast<Control>()
                                           .Where(c => c.Name != "pnlEncabezadoDinamico") // No borrar el encabezado
                                           .ToList();
                if (controlesAEliminar.Any())
                {
                    flpListaMuestras.SuspendLayout();
                    foreach (var c in controlesAEliminar)
                    {
                        flpListaMuestras.Controls.Remove(c);
                        c.Dispose();
                    }
                    flpListaMuestras.ResumeLayout(true);
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
            private void MostrarListaMuestras(List<MuestraInfoViewModel> muestras, bool esVistaRecepcionados)
        {
            // Guardar control con foco (si aplica)
            Control focusedControl = this.ActiveControl;
            // Suspender layout del FlowLayoutPanel principal
            flpListaMuestras.SuspendLayout(); // *** USA EL NOMBRE DEL NUEVO FlowLayoutPanel ***
                                              // Limpiar controles anteriores (excepto el panel de carga si está aquí)
            LimpiarTarjetasDeMuestras();

            Console.WriteLine($"DEBUG: Mostrando vista: {(esVistaRecepcionados ? "Recepcionados" : "Procesados")}");

            try
            {
                if (muestras == null || !muestras.Any())
                {
                    // Mostrar mensaje si no hay resultados
                    Label lblNoResultados = new Label
                    {
                        Text = esVistaRecepcionados ? "No hay muestras pendientes con los filtros aplicados." : "No hay muestras procesadas con los filtros aplicados.",
                        AutoSize = true,
                        Margin = new Padding(10),
                        Font = new Font("Segoe UI", 10),
                        ForeColor = SystemColors.GrayText
                    };
                    flpListaMuestras.Controls.Add(lblNoResultados);
                }
                else
                {
                    CultureInfo culturaEsp = new CultureInfo("es-NI");

                    foreach (var muestraInfo in muestras)
                    {
                        // --- 1. Crear Panel Exterior (Tarjeta) ---
                        Panel pnlTarjeta = new Panel
                        {
                            Width = flpListaMuestras.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10, // Mantenemos el ancho calculado (o quítalo si probaste antes)
                                                                                                                       // AutoSize = true,                       // <-- COMENTADO
                                                                                                                       // AutoSizeMode = AutoSizeMode.GrowAndShrink, // <-- COMENTADO
                            Height = 80,                           // <-- AÑADIDO: Altura fija para probar
                            Margin = new Padding(3, 3, 3, 6),
                            BorderStyle = BorderStyle.FixedSingle,
                            Tag = muestraInfo
                        };

                        // Asignar evento Click al panel exterior para selección (si aún lo quieres)
                        // pnlTarjeta.Click += PnlProyecto_Click; // Necesitarías un PnlMuestra_Click similar

                        // --- 2. Crear TableLayoutPanel Interior ---
                        TableLayoutPanel tlpContenido = new TableLayoutPanel
                        {
                            ColumnCount = 7, // Una columna para cada dato/acción
                            RowCount = 1,
                            Dock = DockStyle.Fill, // Llenar el panel exterior
                            BackColor = Color.White, // Fondo blanco para el contenido
                            Padding = new Padding(5), // Padding interno
                            MinimumSize = new Size(0, 50) // Altura mínima para que no colapse
                        };

                        // --- 3. Definir Estilos de Columna ---
                        // Ajusta porcentajes/tamaños según necesites
                        tlpContenido.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // 0: Muestra (#)
                        tlpContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F)); // 1: Paciente (Ancho)
                        tlpContenido.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // 2: Género
                        tlpContenido.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // 3: Edad
                        tlpContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F)); // 4: Exámenes (Ancho)
                        tlpContenido.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // 5: Fecha Rec.
                        tlpContenido.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // 6: Acción (Botón)

                        // --- 4. Definir Estilo de Fila (AutoSize) ---
                        tlpContenido.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // La fila crece con el contenido

                        // --- 5. Crear y Añadir Controles a las Celdas ---
                        string diaSemana = muestraInfo.FechaRecepcion.ToString("dddd", culturaEsp);
                        if (!string.IsNullOrEmpty(diaSemana)) { diaSemana = char.ToUpper(diaSemana[0]) + diaSemana.Substring(1); }

                        // Col 0: Muestra
                        Label lblMuestra = new Label { Text = $"{diaSemana} #{muestraInfo.NumeroMuestra}", AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(0, 0, 5, 0) };
                        tlpContenido.Controls.Add(lblMuestra, 0, 0);

                        // Col 1: Paciente
                        Label lblPaciente = new Label { Text = muestraInfo.Paciente, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Margin = new Padding(0, 0, 5, 0) };
                        tlpContenido.Controls.Add(lblPaciente, 1, 0);

                        // Col 2: Género
                        Label lblGenero = new Label { Text = muestraInfo.Genero, AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(0, 0, 5, 0) };
                        tlpContenido.Controls.Add(lblGenero, 2, 0);

                        // Col 3: Edad
                        Label lblEdad = new Label { Text = muestraInfo.Edad.ToString(), AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(0, 0, 5, 0) };
                        tlpContenido.Controls.Add(lblEdad, 3, 0);

                        // Col 4: Exámenes (Pendientes o Realizados) -> *** CON DOCK FILL ***
                        string estadoExamenesTexto = esVistaRecepcionados ? muestraInfo.ExamenesPendientesStr : muestraInfo.ExamenesCompletadosStr;
                        if (string.IsNullOrEmpty(estadoExamenesTexto)) estadoExamenesTexto = esVistaRecepcionados ? "[Ninguno Pendiente]" : "[Ninguno Realizado]";
                        Label lblExamenes = new Label
                        {
                            Text = estadoExamenesTexto,
                            AutoSize = true, // Permite crecer verticalmente
                            Dock = DockStyle.Fill, // Usa todo el ancho de la celda y ajusta texto (wrap)
                            Margin = new Padding(0, 0, 5, 0),
                            TextAlign = ContentAlignment.MiddleLeft // Alinear arriba si hay varias líneas
                        };
                        tlpContenido.Controls.Add(lblExamenes, 4, 0);

                        // Col 5: Fecha Recepción
                        Label lblFechaRec = new Label { Text = muestraInfo.FechaRecepcion.ToString("dd/MM/yyyy"), AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(0, 0, 5, 0) };
                        tlpContenido.Controls.Add(lblFechaRec, 5, 0);

                        // Col 6: Botón Acción
                        Button btnAccion = new Button
                        {
                            Text = esVistaRecepcionados ? "Procesar" : "Ver Detalles",
                            Tag = muestraInfo, // Guarda el objeto MuestraInfoViewModel completo
                            Size = new Size(90, 28), // Tamaño ajustado
                            Anchor = AnchorStyles.None, // Centrar el botón en la celda
                            MinimumSize = new Size(90, 28) // Evitar que se encoja demasiado
                        };
                        btnAccion.Click += BtnAccion_Click; // El mismo handler que ya tenías
                        tlpContenido.Controls.Add(btnAccion, 6, 0);


                        // --- 6. Añadir TLP al Panel Exterior ---
                        pnlTarjeta.Controls.Add(tlpContenido);

                        // --- 7. Añadir Panel Exterior al FlowLayoutPanel Principal ---
                        flpListaMuestras.Controls.Add(pnlTarjeta); // *** USA EL NOMBRE DEL NUEVO FlowLayoutPanel ***

                    } // Fin foreach
                } // Fin else (si hay muestras)
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar las muestras: {ex.Message}", "Error de Interfaz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine($"ERROR MostrarListaMuestras: {ex}");
            }
            finally
            {
                // Reanudar layout del FLP principal
                flpListaMuestras.ResumeLayout(true); // *** USA EL NOMBRE DEL NUEVO FlowLayoutPanel ***
                                                     // Devolver foco si se guardó
                                                     // focusedControl?.Focus();
            }
        }
            private void CrearOActualizarEncabezado()
            {
                string nombrePanelEncabezado = "pnlEncabezadoDinamico";
                // Busca si ya existe un panel con ese nombre DENTRO de flpListaMuestras
                Panel? panelEncabezado = flpListaMuestras.Controls.Find(nombrePanelEncabezado, false).FirstOrDefault() as Panel;

                // Si no existe, lo creamos por primera vez
                if (panelEncabezado == null)
                {
                    Console.WriteLine("DEBUG: Creando panel de encabezado...");
                    panelEncabezado = new Panel
                    {
                        Name = nombrePanelEncabezado,
                        // Calcular Ancho inicial basado en el FlowLayoutPanel padre
                        // Restamos espacio para posible barra de scroll y un pequeño margen
                        Width = flpListaMuestras.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10,
                        Height = 35, // <-- Altura FIJA (ajusta si es necesario)
                                     // Ya no usamos AutoSize para el panel exterior
                        Margin = new Padding(3, 3, 3, 0), // Margen arriba/lados, sin margen inferior
                        BackColor = SystemColors.ControlDark, // Color para distinguirlo
                                                              // Anclar a los lados y arriba para que el ancho se ajuste si flpListaMuestras cambia
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                    };

                    // Crear el TableLayoutPanel interior
                    TableLayoutPanel tlpEncabezado = new TableLayoutPanel
                    {
                        ColumnCount = 7, // Mismo número de columnas que las tarjetas de datos
                        RowCount = 1,
                        Dock = DockStyle.Fill, // Llenar el panelEncabezado
                        BackColor = Color.Transparent,
                        Padding = new Padding(5),
                        MinimumSize = new Size(0, 25) // Altura mínima interna
                    };

                    // ***** ¡¡CRUCIAL!! Define los estilos de columna EXACTAMENTE IGUAL *****
                    // ***** que los definidos para 'tlpContenido' en 'MostrarListaMuestras' *****
                    tlpEncabezado.ColumnStyles.Clear(); // Limpiar estilos por si acaso
                    tlpEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));         // 0: Muestra (#)
                    tlpEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));     // 1: Paciente
                    tlpEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));         // 2: Género
                    tlpEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));         // 3: Edad
                    tlpEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));     // 4: Exámenes
                    tlpEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));         // 5: Fecha Rec.
                    tlpEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));         // 6: Acción
                                                                                                // ***** FIN ESTILOS COLUMNA *****

                    // Definir estilo de fila (que ocupe el alto disponible del panel)
                    tlpEncabezado.RowStyles.Clear();
                    tlpEncabezado.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Ocupar alto fijo

                    // Crear y añadir Labels de título (negrita)
                    Font boldFont = new Font("Segoe UI", 9, FontStyle.Bold);
                    var middleLeftAnchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom; // Para centrar verticalmente

                    tlpEncabezado.Controls.Add(new Label { Text = "Muestra", Font = boldFont, AutoSize = true, Anchor = middleLeftAnchor, TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
                    tlpEncabezado.Controls.Add(new Label { Text = "Paciente", Font = boldFont, AutoSize = true, Anchor = middleLeftAnchor, TextAlign = ContentAlignment.MiddleLeft }, 1, 0);
                    tlpEncabezado.Controls.Add(new Label { Text = "Género", Font = boldFont, AutoSize = true, Anchor = middleLeftAnchor, TextAlign = ContentAlignment.MiddleLeft }, 2, 0);
                    tlpEncabezado.Controls.Add(new Label { Text = "Edad", Font = boldFont, AutoSize = true, Anchor = middleLeftAnchor, TextAlign = ContentAlignment.MiddleLeft }, 3, 0);
                    // Label dinámico para título de columna exámenes
                    Label lblTituloEstado = new Label { Name = "lblTituloColumnaEstado", Text = "Exámenes...", Font = boldFont, AutoSize = false, Anchor = AnchorStyles.Left | AnchorStyles.Right, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
                    tlpEncabezado.Controls.Add(lblTituloEstado, 4, 0);
                    tlpEncabezado.Controls.Add(new Label { Text = "Fecha Rec.", Font = boldFont, AutoSize = true, Anchor = middleLeftAnchor, TextAlign = ContentAlignment.MiddleLeft }, 5, 0);
                    tlpEncabezado.Controls.Add(new Label { Text = "Acción", Font = boldFont, AutoSize = true, Anchor = AnchorStyles.None, TextAlign = ContentAlignment.MiddleCenter }, 6, 0);

                    // Añadir TLP al panel y panel al FLP
                    panelEncabezado.Controls.Add(tlpEncabezado);
                    flpListaMuestras.Controls.Add(panelEncabezado);
                    flpListaMuestras.Controls.SetChildIndex(panelEncabezado, 0); // Asegura que esté al principio

                    // Log de depuración (opcional ahora que debería funcionar)
                    Console.WriteLine($"DEBUG: Panel Encabezado CREADO -> Name: {panelEncabezado.Name}, Visible: {panelEncabezado.Visible}, Size: {panelEncabezado.Size}, Location: {panelEncabezado.Location}, Parent: {panelEncabezado.Parent?.Name}");
                }

                // Siempre actualizamos el texto del título dinámico según la vista
                Label? tituloEstado = panelEncabezado?.Controls.OfType<TableLayoutPanel>().FirstOrDefault()
                                       ?.Controls.Find("lblTituloColumnaEstado", false).FirstOrDefault() as Label;
                if (tituloEstado != null)
                {
                    tituloEstado.Text = mostrandoRecepcionados ? "Exámenes Pendientes" : "Exámenes Realizados";
                }

                // Opcional: Reajustar ancho si el FLP cambió (Anchor debería manejarlo)
                if (panelEncabezado != null && panelEncabezado.Width != flpListaMuestras.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10)
                {
                    // panelEncabezado.Width = flpListaMuestras.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10;
                }
            }
        #region Botones con Metodos
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            RecargarListaSiEsNecesario();
        }
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
        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            RecargarListaSiEsNecesario(); // Usa el método centralizado

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
            flpListaMuestras.Controls.Clear();


        }
        private async void txtSearch_KeyDown(object sender, KeyEventArgs e) // Marcar como async void
        {
            // Verificar si la tecla presionada fue Enter
            if (e.KeyCode == Keys.Enter)
            {
                Console.WriteLine("DEBUG: Enter presionado en txtSearch."); // Log para confirmar

                // Llamar al método que recarga la lista con todos los filtros
                RecargarListaSiEsNecesario(); // Llamamos al método principal de recarga

                // Indicar que hemos manejado la tecla para evitar otros comportamientos (como el sonido 'ding')
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
        #endregion
        #region RecargarLista Windows Metodos
        private void dtpFechaRecepcion_ValueChanged(object sender, EventArgs e)
                {
                    RecargarListaSiEsNecesario(); // Usa el método centralizado
                }
        private void FiltroTipoExamen_CheckedChanged(object sender, EventArgs e)
            {
                RecargarListaSiEsNecesario(); // Recarga al cambiar checkbox
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
            private void btnVerProcesados_Click_1(object sender, EventArgs e)
            {

            }
            private void tlpBotones_Paint(object sender, PaintEventArgs e)
            {

            }

        #endregion


        }
    }
