using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using ZasTrack.Models;
using ZasTrack.Repositories;

namespace ZasTrack.Forms.wProyectos
{
    public partial class wVerProyecto : Form
    {
        private ProyectoRepository proyectoRepository;
        private Proyecto proyectoSeleccionado = null; // Para guardar la selección

        // --- NUEVO: Enum y variable para el estado de la vista ---
        private enum EstadoVista { Activos, Archivados }
        private EstadoVista vistaActual = EstadoVista.Activos; // Inicia en Activos
                                                               // -------------------------------------------------------

        // --- MODIFICADO: Constructor ---
        public wVerProyecto()
        {
            InitializeComponent();
            proyectoRepository = new ProyectoRepository();

            // --- ¡AÑADIR ESTAS LÍNEAS! ---
            btnVerActivos.Click += btnVerActivos_Click;
            btnVerArchivados.Click += btnVerArchivados_Click;
            // -----------------------------

            ActualizarEstiloBotonesVista();
            CargarProyectosAsync();
        }

        #region Manejadores de Eventos Botones Vista

        // --- NUEVO: Eventos Click para los botones de vista ---
        // --- AÑADIR: Eventos Click para los botones de vista ---
        private void btnVerActivos_Click(object sender, EventArgs e)
        {
            if (vistaActual != EstadoVista.Activos)
            {
                vistaActual = EstadoVista.Activos;
                proyectoSeleccionado = null;
                ActualizarEstiloBotonesVista();
                ActualizarEstadoBotonesAdmin(); 
                CargarProyectosAsync();
            }
        }

        private void btnVerArchivados_Click(object sender, EventArgs e)
        {
            if (vistaActual != EstadoVista.Archivados)
            {
                vistaActual = EstadoVista.Archivados;
                proyectoSeleccionado = null;
                ActualizarEstiloBotonesVista();
                CargarProyectosAsync();
            }
        }
        // ----------------------------------------------------

        #endregion

        #region Metodos Principales (Carga y UI)


        // --- REEMPLAZAR: Método CargarProyectosAsync completo y actualizado ---
        private async void CargarProyectosAsync()
        {
            // Intenta mostrar el indicador de carga (suponiendo que pnlCargando está corregido)
            MostrarCargando(true);
            // Limpia la lista anterior y la selección
            flpProyList.Controls.Clear();
            proyectoSeleccionado = null;
            // TODO: Aquí podrías deshabilitar botones de Modo Avanzado si existieran

            List<Proyecto> proyectos = null; // Inicializar

            try
            {
                Console.WriteLine($"DEBUG: Iniciando carga para vista: {vistaActual}"); // Log para saber qué se carga

                // Obtener datos del repositorio según la vista actual
                if (vistaActual == EstadoVista.Activos)
                {
                    this.Text = "Administrar Proyectos - Activos";
                    proyectos = await Task.Run(() => proyectoRepository.ObtenerProyectos(incluirArchivados: false));
                }
                else // Vista Archivados
                {
                    this.Text = "Administrar Proyectos - Archivados";
                    // Asegúrate que este método exista en ProyectoRepository
                    proyectos = await Task.Run(() => proyectoRepository.ObtenerProyectosSoloArchivados());
                }

                Console.WriteLine($"DEBUG: Proyectos obtenidos del repo: {proyectos?.Count ?? 0}");

                // Verificar si hay proyectos para mostrar
                if (proyectos == null || !proyectos.Any())
                {
                    Label lblNoProyectos = new Label
                    {
                        Text = (vistaActual == EstadoVista.Activos) ? "No se encontraron proyectos activos." : "No se encontraron proyectos archivados.",
                        AutoSize = true,
                        Margin = new Padding(20), // Más margen para que se vea mejor
                        Font = new Font("Segoe UI", 11),
                        ForeColor = SystemColors.GrayText // Color discreto
                    };
                    // Centrar el label (puede requerir ajustar Location después de añadir)
                    flpProyList.Controls.Add(lblNoProyectos);
                    // Para centrar mejor un único elemento en FLP, a veces ayuda ponerlo en un Panel
                    // o calcular Location después de añadirlo si no se centra bien solo.
                }
                else
                {
                    // Si hay proyectos, crear los paneles
                    Console.WriteLine($"DEBUG: Creando paneles para {proyectos.Count} proyectos ({vistaActual})...");
                    flpProyList.SuspendLayout(); // Optimización: suspender layout

                    foreach (Proyecto proyecto in proyectos)
                    {
                        // --- Crear Panel Principal para el Proyecto ---
                        int panelHeight = 150; // Altura fija para cada 'tarjeta' de proyecto
                        int panelWidth = flpProyList.ClientSize.Width - 30; // Ancho ajustado (menos ~30px para scrollbar V y márgenes)
                                                                            // Asegúrate que flpProyList.ClientSize.Width sea > 30
                        if (panelWidth < 200) panelWidth = 200; // Un ancho mínimo por si acaso

                        Panel pnlProyecto = new Panel
                        {
                            Size = new Size(panelWidth, panelHeight),
                            BorderStyle = BorderStyle.FixedSingle, // Borde visible
                            Margin = new Padding(5),               // Espacio entre paneles
                            Tag = proyecto                         // Guardar el objeto proyecto
                        };
                        pnlProyecto.Click += PnlProyecto_Click; // Asignar handler para selección

                        // --- Añadir Labels (con posicionamiento acumulado) ---
                        int currentY = 10;
                        int spacing = 5;
                        int leftMargin = 10;
                        int panelContentWidth = pnlProyecto.ClientSize.Width - (leftMargin * 2);

                        Label lblNombre = new Label
                        {
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            Location = new Point(leftMargin, currentY),
                            AutoSize = true, // Permitir que determine su alto
                            MaximumSize = new Size(panelContentWidth, 0) // Limitar ancho
                        };
                        lblNombre.Text = proyecto.nombre;
                        pnlProyecto.Controls.Add(lblNombre);
                        currentY += lblNombre.Height + spacing;

                        Label lblFechaInicio = new Label
                        {
                            Font = new Font("Segoe UI", 9),
                            Location = new Point(leftMargin, currentY),
                            AutoSize = true
                        };
                        lblFechaInicio.Text = $"Inicio: {proyecto.fecha_inicio:dd/MM/yyyy}";
                        pnlProyecto.Controls.Add(lblFechaInicio);
                        currentY += lblFechaInicio.Height + spacing;

                        Label lblFechaFin = new Label
                        {
                            Font = new Font("Segoe UI", 9),
                            Location = new Point(leftMargin, currentY),
                            AutoSize = true
                        };
                        lblFechaFin.Text = proyecto.fecha_fin.HasValue ? $"Fin: {proyecto.fecha_fin.Value:dd/MM/yyyy}" : "Fin: Activo";
                        pnlProyecto.Controls.Add(lblFechaFin);

                        // --- Crear Panel para Botones (FlowLayoutPanel anidado) ---
                        FlowLayoutPanel flpBotonesInterno = new FlowLayoutPanel
                        {
                            FlowDirection = FlowDirection.LeftToRight, // Botones en horizontal
                            WrapContents = true,        // Permitir que bajen si no caben
                            Dock = DockStyle.Bottom,   // Anclar abajo
                            Height = 35,               // Altura fija para botones
                            BackColor = Color.Transparent,
                            Padding = new Padding(leftMargin - 3, 0, 0, 5) // Ajustar padding izquierdo/inferior
                        };

                        // Botón Detalles
                        Button btnDetalles = new Button
                        {
                            Text = "Detalles",
                            Size = new Size(80, 30),
                            Tag = proyecto, // Guardar objeto completo
                            Margin = new Padding(0, 0, spacing, 0), // Margen derecho
                            FlatStyle = FlatStyle.System // Estilo estándar
                        };
                        // Asegúrate que el handler BtnDetalles_Click exista
                        btnDetalles.Click += BtnDetalles_Click;
                        flpBotonesInterno.Controls.Add(btnDetalles);

                        // Botón Condicional (Finalizar o Reactivar)
                        Button btnAccion = new Button
                        {
                            Size = new Size(80, 30),
                            Tag = proyecto, // Guardar objeto completo
                            Margin = new Padding(0, 0, spacing, 0),
                            FlatStyle = FlatStyle.Flat,
                            ForeColor = Color.White,
                        };
                        btnAccion.FlatAppearance.BorderSize = 0;
                        flpBotonesInterno.Controls.Add(btnAccion);
                        Button btnEditar = new Button
                        {
                            Text = "Editar",
                            Name = $"btnEditar_{proyecto.id_proyecto}", // Nombre único por si acaso
                            Size = new Size(80, 30),
                            Tag = proyecto,
                            Margin = new Padding(spacing, 0, spacing, 0), // Añadir margen izquierdo y derecho
                            FlatStyle = FlatStyle.System,
                            Visible = false, // <-- INVISIBLE por defecto
                            Enabled = false  // <-- DESHABILITADO por defecto
                        };
                        btnEditar.Click += BtnEditar_Click; // Asigna el evento (crearemos el método luego)
                        flpBotonesInterno.Controls.Add(btnEditar); // Añade al panel de botones

                        Button btnEliminar = new Button
                        {
                            Text = "Eliminar",
                            Name = $"btnEliminar_{proyecto.id_proyecto}", // Nombre único
                            Size = new Size(80, 30),
                            Tag = proyecto,
                            Margin = new Padding(0, 0, spacing, 0), // Margen derecho
                            FlatStyle = FlatStyle.Flat,
                            BackColor = Color.DarkRed, // Color distintivo peligroso
                            ForeColor = Color.White,
                            Visible = false, // <-- INVISIBLE por defecto
                            Enabled = false  // <-- DESHABILITADO por defecto
                        };
                        btnEliminar.FlatAppearance.BorderSize = 0;
                        btnEliminar.Click += BtnEliminar_Click;
                        if (vistaActual == EstadoVista.Activos)
                        {
                            btnAccion.Text = "Finalizar";
                            btnAccion.Name = $"btnFinalizar_{proyecto.id_proyecto}";
                            btnAccion.BackColor = Color.OrangeRed;
                            btnAccion.Click += BtnFinalizar_Click;
                        }
                        else
                        { // Vista Archivados
                            btnAccion.Text = "Reactivar";
                            btnAccion.Name = $"btnReactivar_{proyecto.id_proyecto}";
                            btnAccion.BackColor = Color.ForestGreen;
                            // Asegúrate que el handler BtnReactivar_Click exista
                            btnAccion.Click += BtnReactivar_Click;
                        }
                       // Asigna el evento (crearemos el método luego)
                        flpBotonesInterno.Controls.Add(btnEliminar); // Añade al panel de botones

                        // --- FIN AÑADIR BOTONES EDITAR Y ELIMINAR ---


                        // Añadir el panel de botones al panel principal del proyecto
                        pnlProyecto.Controls.Add(flpBotonesInterno);

                        // Añadir el Panel completo del proyecto a la lista principal
                        flpProyList.Controls.Add(pnlProyecto);

                    } // --- FIN foreach ---

                    flpProyList.ResumeLayout(true); // Optimización: aplicar layout

                    // --- DEBUG Scroll después de añadir ---
                    flpProyList.PerformLayout(); // Forzar layout
                    await Task.Delay(50);    // Pausa breve
                    Console.WriteLine($"DEBUG: Fin Carga - VScroll Visible: {flpProyList.VerticalScroll.Visible}, HScroll Visible: {flpProyList.HorizontalScroll.Visible}");
                    Console.WriteLine($"DEBUG: Fin Carga - flpProyList ClientSize: {flpProyList.ClientSize}");
                    // --- FIN DEBUG ---

                } // Fin del else (cuando sí hay proyectos)
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error de PostgreSQL al cargar proyectos: {ex.Message} (SQLState: {ex.SqlState})");
                MessageBox.Show($"Error de base de datos al cargar proyectos.\nDetalle: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general al cargar proyectos: {ex.ToString()}");
                MessageBox.Show($"Ocurrió un error inesperado al cargar los proyectos.\nDetalle: {ex.Message}", "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MostrarCargando(false); // Ocultar indicador de carga
            }
        }
        // En wVerProyecto.cs
        private void ActualizarEstadoBotonesAdmin()
        {
            Console.WriteLine("--- ActualizarEstadoBotonesAdmin INICIO ---");
            bool modoAdmin = chkModoAdmin.Checked;
            bool haySeleccion = proyectoSeleccionado != null;
            bool habilitar = modoAdmin && haySeleccion;

            Console.WriteLine($"ModoAdmin={modoAdmin}, HaySeleccion={haySeleccion}, Habilitar={habilitar}");
            Console.WriteLine($"Proyecto Seleccionado: {proyectoSeleccionado?.nombre ?? "NULL"} (ID: {proyectoSeleccionado?.id_proyecto.ToString() ?? "N/A"})");

            // Deshabilitar y resetear TODOS los botones de admin primero
            foreach (Control panelControl in flpProyList.Controls.OfType<Panel>())
            {
                FlowLayoutPanel flpBotones = panelControl.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
                if (flpBotones != null)
                {
                    Button btnEd = flpBotones.Controls.OfType<Button>().FirstOrDefault(b => b.Name.StartsWith("btnEditar_"));
                    Button btnDel = flpBotones.Controls.OfType<Button>().FirstOrDefault(b => b.Name.StartsWith("btnEliminar_"));
                    if (btnEd != null) { btnEd.Enabled = false; btnEd.Text = "Editar"; } // Resetea texto
                    if (btnDel != null) { btnDel.Enabled = false; }
                }
            }

            // Ahora, intentar habilitar y cambiar texto SOLO en el seleccionado si aplica
            if (habilitar) // Solo si modo admin está ON Y hay selección
            {
                // Construimos los nombres EXACTOS de los botones que buscamos
                string editButtonName = $"btnEditar_{proyectoSeleccionado.id_proyecto}";
                string deleteButtonName = $"btnEliminar_{proyectoSeleccionado.id_proyecto}";

                // Usamos Controls.Find(key, searchAllChildren) buscando DENTRO de flpProyList
                // Devuelve una colección, tomamos el primero (debería ser único)
                Button btnEdit = flpProyList.Controls.Find(editButtonName, true).FirstOrDefault() as Button;
                Button btnDelete = flpProyList.Controls.Find(deleteButtonName, true).FirstOrDefault() as Button;

                // Verificamos si se encontraron y aplicamos estado/texto
                if (btnEdit != null)
                {
                    Console.WriteLine($"Botón Editar encontrado ({btnEdit.Name}) por Find. Habilitar={habilitar}");
                    btnEdit.Enabled = habilitar;
                }
                else { Console.WriteLine($"Botón Editar con nombre '{editButtonName}' NO encontrado en flpProyList."); }

                if (btnDelete != null)
                {
                    Console.WriteLine($"Botón Eliminar encontrado ({btnDelete.Name}) por Find. Habilitar={habilitar}");
                    btnDelete.Enabled = habilitar;
                }
                else { Console.WriteLine($"Botón Eliminar con nombre '{deleteButtonName}' NO encontrado en flpProyList."); }
            }
            else { Console.WriteLine("Condición para habilitar no cumplida."); }

            Console.WriteLine("--- ActualizarEstadoBotonesAdmin FIN ---");
        }
        private void ActualizarEstiloBotonesVista()
        {
            // Asigna estilos a los botones btnVerActivos y btnVerArchivados
            // según el valor de 'vistaActual'
            if (vistaActual == EstadoVista.Activos)
            {
                // Estilo para botón activo
                btnVerActivos.BackColor = Color.DodgerBlue; // Puedes elegir otros colores
                btnVerActivos.ForeColor = Color.White;
                btnVerActivos.FlatStyle = FlatStyle.Flat;
                btnVerActivos.FlatAppearance.BorderColor = Color.DodgerBlue;
                btnVerActivos.FlatAppearance.BorderSize = 1;


                // Estilo para botón inactivo
                btnVerArchivados.BackColor = SystemColors.Control;
                btnVerArchivados.ForeColor = SystemColors.ControlText;
                btnVerArchivados.FlatStyle = FlatStyle.Standard;
                btnVerArchivados.FlatAppearance.BorderColor = SystemColors.ControlDark; // Resetear borde
                btnVerArchivados.FlatAppearance.BorderSize = 1; // O el valor por defecto

            }
            else // Vista Archivados
            {
                // Estilo para botón inactivo
                btnVerActivos.BackColor = SystemColors.Control;
                btnVerActivos.ForeColor = SystemColors.ControlText;
                btnVerActivos.FlatStyle = FlatStyle.Standard;
                btnVerActivos.FlatAppearance.BorderColor = SystemColors.ControlDark;
                btnVerActivos.FlatAppearance.BorderSize = 1;


                // Estilo para botón activo
                btnVerArchivados.BackColor = Color.DodgerBlue;
                btnVerArchivados.ForeColor = Color.White;
                btnVerArchivados.FlatStyle = FlatStyle.Flat;
                btnVerArchivados.FlatAppearance.BorderColor = Color.DodgerBlue;
                btnVerArchivados.FlatAppearance.BorderSize = 1;

            }
        }
        private void PnlProyecto_Click(object sender, EventArgs e)
        {
        
            if (sender is Panel pnlClickeado && pnlClickeado.Tag is Proyecto proy)
            {
                foreach (Control ctrl in flpProyList.Controls) { if (ctrl is Panel pnl) { pnl.BackColor = SystemColors.Control; } }
                pnlClickeado.BackColor = SystemColors.Highlight;
                proyectoSeleccionado = proy;
                Console.WriteLine($"Seleccionado: {proyectoSeleccionado.nombre}");
                ActualizarEstadoBotonesAdmin();
            }
        }
        private void MostrarCargando(bool mostrar)
        {
            string nombreLabelCargando = "lblCargandoDinamic";

            if (mostrar)
            {
                // Oculta la lista y muestra el panel de carga
                flpProyList.Visible = false;
                pnlCargando.Visible = true; // Asegúrate que pnlCargando sea visible
                pnlCargando.BringToFront(); // Ponlo encima de la lista

                // Limpia cualquier label de carga anterior en pnlCargando
                Control lblExistente = pnlCargando.Controls.Find(nombreLabelCargando, true).FirstOrDefault(); // Busca recursivamente
                if (lblExistente != null)
                {
                    pnlCargando.Controls.Remove(lblExistente);
                    lblExistente.Dispose();
                }

                // Crea y añade el nuevo label de carga a pnlCargando
                Label lblCargando = new Label
                {
                    Name = nombreLabelCargando,
                    Text = "Cargando proyectos...",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 12),

                    Anchor = AnchorStyles.None, // <-- CAMBIAR A None
                };
                // Calcular posición centrada después de crear el label
                lblCargando.Location = new Point(
                    (pnlCargando.ClientSize.Width - lblCargando.Width) / 2,
                    (pnlCargando.ClientSize.Height - lblCargando.Height) / 2);

                pnlCargando.Controls.Add(lblCargando);
            }
            else
            {
                // Oculta el panel de carga y muestra la lista
                pnlCargando.Visible = false;
                flpProyList.Visible = true; // Muestra la lista con los resultados

                // Busca y elimina el label de carga de pnlCargando
                Control lblCargando = pnlCargando.Controls.Find(nombreLabelCargando, true).FirstOrDefault(); // Busca recursivamente
                if (lblCargando != null)
                {
                    pnlCargando.Controls.Remove(lblCargando);
                    lblCargando.Dispose();
                }
            }
        }
        private void BtnDetalles_Click(object sender, EventArgs e)
        {
            // Obtener el objeto Proyecto completo y el flag esArchivado, luego abrir detalles
            // (Copiar el código actualizado que te di en la respuesta anterior)
            if (sender is Button btn && btn.Tag is Proyecto proy)
            {
                bool esArchivado = proy.IsArchived;
                Console.WriteLine($"Abriendo detalles para {proy.nombre}, Archivado: {esArchivado}");
                // wVerDetallesProyecto formDetalles = new wVerDetallesProyecto(proy, esArchivado);
                // formDetalles.ShowDialog(this);
                MessageBox.Show($"Detalles para: {proy.nombre}\nEstado: {(esArchivado ? "Archivado" : "Activo")}\n(Formulario pendiente)", "Detalles", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo obtener info del proyecto.", "Error Tag", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Corregido a Warning
            }
        }
        private void BtnFinalizar_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is Proyecto proyectoParaArchivar)
            {
                DateTime fechaFinParaGuardar; // Variable para guardar la fecha decidida

                // Verificar si ya existe una fecha de fin previa
                if (proyectoParaArchivar.fecha_fin.HasValue)
                {
                    // --- Lógica para proyecto previamente archivado ---
                    string fechaExistenteStr = proyectoParaArchivar.fecha_fin.Value.ToString("dd/MM/yyyy");
                    string fechaHoyStr = DateTime.Today.ToString("dd/MM/yyyy");

                    string mensaje = $"El proyecto '{proyectoParaArchivar.nombre}' ya tiene una fecha de finalización registrada: {fechaExistenteStr}.\n\n" +
                                     $"Al finalizarlo de nuevo, ¿qué fecha desea usar?\n\n" +
                                     $"- Pulse [Sí] para MANTENER la fecha original ({fechaExistenteStr}).\n" +
                                     $"- Pulse [No] para USAR la fecha de HOY ({fechaHoyStr}).\n" +
                                     $"- Pulse [Cancelar] para no hacer nada.";

                    DialogResult resultFecha = MessageBox.Show(mensaje,
                                                           "Elegir Fecha de Finalización",
                                                           MessageBoxButtons.YesNoCancel, // Da las tres opciones
                                                           MessageBoxIcon.Question);

                    if (resultFecha == DialogResult.Cancel)
                    {
                        return; // El usuario canceló
                    }
                    else if (resultFecha == DialogResult.Yes)
                    {
                        // El usuario quiere mantener la fecha existente
                        fechaFinParaGuardar = proyectoParaArchivar.fecha_fin.Value;
                        Console.WriteLine($"DEBUG: Finalizar manteniendo fecha existente: {fechaFinParaGuardar:dd/MM/yyyy}");
                    }
                    else // resultFecha == DialogResult.No
                    {
                        // El usuario quiere usar la fecha de hoy
                        fechaFinParaGuardar = DateTime.Today;
                        Console.WriteLine($"DEBUG: Finalizar usando fecha de hoy: {fechaFinParaGuardar:dd/MM/yyyy}");
                    }
                }
                else
                {
                    // --- Lógica original para proyecto nunca antes archivado ---
                    var confirmResult = MessageBox.Show($"¿Está seguro de que desea finalizar y archivar el proyecto '{proyectoParaArchivar.nombre}'?\n\n" +
                                                      $"Esta acción establecerá la fecha de fin a hoy ({DateTime.Today:dd/MM/yyyy}) y lo ocultará de las listas activas.",
                                                      "Confirmar Finalizar Proyecto",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                    if (confirmResult == DialogResult.No)
                    {
                        return; // El usuario canceló
                    }
                    // Si confirma, usa la fecha de hoy
                    fechaFinParaGuardar = DateTime.Today;
                    Console.WriteLine($"DEBUG: Finalizar por primera vez usando fecha de hoy: {fechaFinParaGuardar:dd/MM/yyyy}");
                }

                // --- Proceder a Archivar con la fecha determinada ---
                bool exito = false;
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    // Llamar al repositorio pasando la fecha decidida
                    exito = proyectoRepository.ArchivarProyecto(proyectoParaArchivar.id_proyecto, fechaFinParaGuardar);
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show($"Error al archivar el proyecto: {ex.Message}", "Error Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"ERROR ArchivarProyecto: {ex.ToString()}");
                    return;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

                if (exito)
                {
                    MessageBox.Show($"Proyecto '{proyectoParaArchivar.nombre}' finalizado y archivado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Recargar la vista actual (que es la de activos)
                    // El proyecto archivado ya no debería aparecer aquí.
                    CargarProyectosAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo archivar el proyecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo obtener la información del proyecto desde el botón Finalizar.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            // 1. Obtener el proyecto desde el Tag del botón presionado
            if (sender is Button btnEditar && btnEditar.Tag is Proyecto proyectoParaEditar)
            {
                Console.WriteLine($"DEBUG: Abriendo editor para proyecto ID: {proyectoParaEditar.id_proyecto}, Nombre: {proyectoParaEditar.nombre}");

                // 2. Crear una instancia del formulario de edición, pasándole el proyecto
                //    Usamos 'using' para asegurarnos que se liberen los recursos del formulario de edición al cerrarse
                using (wEditarProyecto formEditar = new wEditarProyecto(proyectoParaEditar))
                {
                    // 3. Mostrar el formulario de edición como un DIÁLOGO MODAL.
                    //    Esto detiene la ejecución aquí hasta que el usuario cierre wEditarProyecto.
                    DialogResult resultado = formEditar.ShowDialog(this); // 'this' lo hace hijo de wVerProyecto

                    // 4. (Opcional pero recomendado) Verificar si se guardaron cambios y refrescar
                    //    Para que esto funcione bien, necesitaremos que btnGuardarCambios_Click en wEditarProyecto
                    //    establezca this.DialogResult = DialogResult.OK; si el guardado fue exitoso.
                    //    Por ahora, simplemente recargaremos la lista si no se canceló.
                    if (resultado != DialogResult.Cancel)
                    {
                        Console.WriteLine("DEBUG: El formulario de edición se cerró (posiblemente guardó). Recargando lista de proyectos...");
                        CargarProyectosAsync(); // Recargar la lista para reflejar posibles cambios
                    }
                    else
                    {
                        Console.WriteLine("DEBUG: El formulario de edición se cerró con Cancelar.");
                    }
                } // El 'using' llama a formEditar.Dispose() aquí automáticamente
            }
            else
            {
                // Error si no se pudo obtener el proyecto del botón
                MessageBox.Show("No se pudo obtener la información del proyecto para editar.",
                                "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is Proyecto proyectoParaEliminar)
            {
                Console.WriteLine($"Clic en Eliminar para: {proyectoParaEliminar.nombre}");
                // TODO: Mostrar confirmación MUY clara. Si confirma, llamar a repo y recargar.
                MessageBox.Show($"Eliminar proyecto: {proyectoParaEliminar.nombre}\n(Funcionalidad pendiente)", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void chkModoAdmin_CheckedChanged(object sender, EventArgs e)
        {
            bool modoAdminActivado = chkModoAdmin.Checked;
            Console.WriteLine($"Modo Admin cambiado a: {modoAdminActivado}");

            // Recorre todos los paneles de proyecto en el FlowLayoutPanel
            foreach (Control controlPnlProyecto in flpProyList.Controls)
            {
                if (controlPnlProyecto is Panel pnlProyecto) // Asegura que sea un Panel
                {
                    // Busca el FlowLayoutPanel anidado que contiene los botones
                    FlowLayoutPanel flpBotones = pnlProyecto.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
                    if (flpBotones != null)
                    {
                        // Busca los botones Editar y Eliminar DENTRO del FLP de botones
                        Button btnEdit = flpBotones.Controls.OfType<Button>().FirstOrDefault(b => b.Name.StartsWith("btnEditar_"));
                        Button btnDelete = flpBotones.Controls.OfType<Button>().FirstOrDefault(b => b.Name.StartsWith("btnEliminar_"));

                        // Cambia su visibilidad según el modo admin
                        if (btnEdit != null) btnEdit.Visible = modoAdminActivado;
                        if (btnDelete != null) btnDelete.Visible = modoAdminActivado;
                    }
                }
            }

            // Después de cambiar la visibilidad, actualiza el estado Enabled (basado en si hay selección)
            ActualizarEstadoBotonesAdmin();
        }
        #endregion

        #region Handlers Pendientes / Nuevos


        private void BtnReactivar_Click(object sender, EventArgs e)
        {
            if (sender is Button btnReactivar && btnReactivar.Tag is Proyecto proyectoParaReactivar)
            {
                Console.WriteLine($"Clic en Reactivar para: {proyectoParaReactivar.nombre}");

                string fechaFinOriginalStr = proyectoParaReactivar.fecha_fin.HasValue
                                             ? proyectoParaReactivar.fecha_fin.Value.ToString("dd/MM/yyyy")
                                             : "[Ninguna]";

                string mensaje = $"Va a reactivar el proyecto '{proyectoParaReactivar.nombre}'.\n\n" +
                                 $"Originalmente finalizó el: {fechaFinOriginalStr}\n\n" +
                                 "¿Desea limpiar esta fecha de finalización (Sí - Recomendado)?\n" +
                                 "¿O mantenerla (No)?";

                DialogResult confirmResult = MessageBox.Show(mensaje,
                                                             "Confirmar Reactivación",
                                                             MessageBoxButtons.YesNoCancel,
                                                             MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Cancel) { return; } // Cancelado

                bool limpiarFecha = (confirmResult == DialogResult.Yes);

                // Llamada al repositorio (¡Asegúrate que exista en ProyectoRepository!)
                bool exito = false;
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    exito = proyectoRepository.ReactivarProyecto(proyectoParaReactivar.id_proyecto, limpiarFecha);
                }
                catch (Exception ex)
                { this.Cursor = Cursors.Default; MessageBox.Show($"Error al reactivar: {ex.Message}"); return; }
                finally { this.Cursor = Cursors.Default; }


                if (exito)
                {
                    MessageBox.Show("Proyecto reactivado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Importante: Recarga la vista actual (que es la de archivados)
                    // El proyecto reactivado ya no debería aparecer aquí.
                    CargarProyectosAsync();
                }
                else { MessageBox.Show("No se pudo reactivar el proyecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else
            {
                MessageBox.Show("No se pudo obtener la información del proyecto desde el botón Reactivar.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Corregido a Warning
            }
        }
        // ---------------------------

        #endregion
    }
}