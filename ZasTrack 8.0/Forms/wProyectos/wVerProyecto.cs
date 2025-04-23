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
                        // currentY += lblFechaFin.Height + spacing; // Actualizar si algo fuera debajo

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

                        if (vistaActual == EstadoVista.Activos)
                        {
                            btnAccion.Text = "Finalizar";
                            btnAccion.Name = $"btnFinalizar_{proyecto.id_proyecto}";
                            btnAccion.BackColor = Color.OrangeRed;
                            // Asegúrate que el handler BtnFinalizar_Click exista
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
                        flpBotonesInterno.Controls.Add(btnAccion);

                        // Añadir el panel de botones al panel principal del proyecto
                        // (Añadir al final para que DockStyle.Bottom funcione bien)
                        pnlProyecto.Controls.Add(flpBotonesInterno);

                        // --- Añadir el Panel completo del proyecto a la lista principal ---
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

        // --- NUEVO: Método para actualizar estilo de botones de vista ---
        private void ActualizarEstiloBotonesVista()
        {
            // Asigna estilos a los botones btnVerActivos y btnVerArchivados
            // según el valor de 'vistaActual'
            // (Puedes copiar el código que te di en la respuesta anterior)
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
        // ---------------------------------------------------------


        private void PnlProyecto_Click(object sender, EventArgs e)
        {
            // Lógica para resaltar panel clickeado y guardar 'proyectoSeleccionado'
            // (Copiar el código que te di en la respuesta anterior)
            if (sender is Panel pnlClickeado && pnlClickeado.Tag is Proyecto proy)
            {
                foreach (Control ctrl in flpProyList.Controls) { if (ctrl is Panel pnl) { pnl.BackColor = SystemColors.Control; } }
                pnlClickeado.BackColor = SystemColors.Highlight;
                proyectoSeleccionado = proy;
                Console.WriteLine($"Seleccionado: {proyectoSeleccionado.nombre}");
                // TODO: Habilitar/Deshabilitar botones Edit/Delete aquí si Modo Avanzado está activo
            }
        }
        // --- REEMPLAZAR: Método MostrarCargando actualizado ---
        // Asegúrate de que pnlCargando SÍ exista en el diseñador y NO esté dentro de flpProyList
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
        // ---------------------------------------------------------

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

        // --- BtnFinalizar_Click (sin cambios respecto a tu código original) ---
        private void BtnFinalizar_Click(object sender, EventArgs e)
        {
            // (Tu código existente para finalizar/archivar)
            // ... igual que lo tenías ...
            if (sender is Button btn && btn.Tag is Proyecto proyectoParaArchivar)
            {
                var confirmResult = MessageBox.Show($"¿Está seguro de que desea finalizar y archivar el proyecto '{proyectoParaArchivar.nombre}'?\n\nEsta acción establecerá la fecha de fin a hoy ({DateTime.Today:dd/MM/yyyy}) y lo ocultará de las listas activas.",
                                                      "Confirmar Finalizar Proyecto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    DateTime fechaFin = DateTime.Today;
                    bool exito = false;
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        // Asumiendo que ArchivarProyecto hace SET is_archived=true, fecha_fin=fechaFin
                        exito = proyectoRepository.ArchivarProyecto(proyectoParaArchivar.id_proyecto, fechaFin);
                    }
                    catch (Exception ex)
                    { /* ... manejo error ... */ this.Cursor = Cursors.Default; MessageBox.Show($"Error: {ex.Message}"); return; }
                    finally { this.Cursor = Cursors.Default; }

                    if (exito)
                    { /* ... mensaje éxito y CargarProyectosAsync() ... */ MessageBox.Show("Éxito"); CargarProyectosAsync(); }
                    else { /* ... mensaje error ... */ MessageBox.Show("Error al finalizar"); }
                }
            }
            else { /* ... mensaje error Tag ... */ MessageBox.Show("Error Tag"); }
        }


        #endregion

        #region Handlers Pendientes / Nuevos

        // --- AÑADIR: Esqueleto para BtnReactivar_Click ---
        // Este método se llamará desde los botones "Reactivar" que se crean
        // cuando vistaActual es EstadoVista.Archivados
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

        private void flpProyList_Paint(object sender, PaintEventArgs e)
        {
        }

        private void wVerProyecto_Load(object sender, EventArgs e)
        {

        }
    }
}