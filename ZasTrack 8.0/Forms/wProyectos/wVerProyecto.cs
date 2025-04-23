using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        public wVerProyecto()
        {
            InitializeComponent();

            proyectoRepository = new ProyectoRepository();
            CargarProyectosAsync();

        }

        #region Metodos

        private async void CargarProyectosAsync()
        {
            // Muestra indicador visual de carga
            MostrarCargando(true);
            // Limpia los controles previos del FlowLayoutPanel
            flpProyList.Controls.Clear();

            try
            {
                // Obtiene la lista de proyectos activos (IsArchived = false o NULL)
                // Usamos Task.Run para no bloquear el Hilo UI durante el acceso a BD
                List<Proyecto> proyectos = await Task.Run(() => proyectoRepository.ObtenerProyectos(incluirArchivados: false));

                if (proyectos == null || !proyectos.Any())
                {
                    // Si no hay proyectos, muestra un mensaje informativo
                    Label lblNoProyectos = new Label
                    {
                        Text = "No se encontraron proyectos activos.",
                        AutoSize = true,
                        Margin = new Padding(10),
                        Font = new Font("Segoe UI", 10)
                    };
                    flpProyList.Controls.Add(lblNoProyectos);
                }
                else
                {
                    // Si hay proyectos, crea un panel para cada uno
                    Console.WriteLine($"Creando paneles para {proyectos.Count} proyectos...");
                    foreach (Proyecto proyecto in proyectos)
                    {
                        // Ya no es necesario el if(proyecto.IsArchived) porque ObtenerProyectos ya los filtra

                        // --- Panel Principal para el Proyecto ---
                        Panel pnlProyecto = new Panel
                        {
                            // **IMPORTANTE: Define un tamaño para el panel**
                            // Ajusta el ancho (Width) basado en el FlowLayoutPanel contenedor.
                            // -25 puede ser para dar espacio a un scrollbar vertical.
                            // Ajusta el alto (Height) según tus necesidades (150 es un ejemplo).
                            Size = new Size(flpProyList.ClientSize.Width - 25, 150),
                            BorderStyle = BorderStyle.FixedSingle, // Borde para visualizar el panel
                            Margin = new Padding(5), // Espacio entre paneles
                            Tag = proyecto // Guardamos el objeto completo para usarlo después
                        };

                        // --- Controles Dentro del Panel ---

                        // Nombre del Proyecto
                        Label lblNombre = new Label
                        {
                            Text = proyecto.nombre,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            AutoSize = true, // Ajusta tamaño al texto
                            Location = new Point(10, 10), // Posición dentro del pnlProyecto
                            // Opcional: Limitar ancho si los nombres son muy largos
                            // MaximumSize = new Size(pnlProyecto.Width - 20, 0),
                            Tag = proyecto // También puedes guardar aquí si lo necesitas
                        };

                        // Fecha de Inicio
                        Label lblFechaInicio = new Label
                        {
                            Text = $"Inicio: {proyecto.fecha_inicio:dd/MM/yyyy}",
                            Font = new Font("Segoe UI", 9),
                            AutoSize = true,
                            Location = new Point(10, lblNombre.Bottom + 5) // Debajo del nombre
                        };

                        // Fecha de Fin (o "Activo")
                        Label lblFechaFin = new Label
                        {
                            Text = proyecto.fecha_fin.HasValue ? $"Fin: {proyecto.fecha_fin.Value:dd/MM/yyyy}" : "Fin: Activo",
                            Font = new Font("Segoe UI", 9),
                            AutoSize = true,
                            Location = new Point(10, lblFechaInicio.Bottom + 5) // Debajo de fecha inicio
                        };

                        // Botón Detalles
                        Button btnDetalles = new Button
                        {
                            Text = "Detalles",
                            Size = new Size(80, 30),
                            Location = new Point(10, pnlProyecto.Height - 40), // Abajo a la izquierda
                            Tag = proyecto.id_proyecto // Guardamos solo el ID para este botón (o el objeto si prefieres)
                            // Puedes añadir más propiedades de estilo (BackColor, FlatStyle, etc.)
                        };
                        btnDetalles.Click += BtnDetalles_Click;

                        // Botón Finalizar (Archivar)
                        Button btnFinalizar = new Button
                        {
                            Text = "Finalizar",
                            Name = $"btnFinalizar_{proyecto.id_proyecto}", // Nombre único
                            Size = new Size(80, 30),
                            Location = new Point(btnDetalles.Right + 10, btnDetalles.Top), // Al lado de Detalles
                            BackColor = Color.OrangeRed,
                            FlatStyle = FlatStyle.Flat,
                            ForeColor = Color.White,
                            Tag = proyecto // Guardamos el objeto Proyecto completo
                        };
                        btnFinalizar.FlatAppearance.BorderSize = 0;
                        btnFinalizar.Click += BtnFinalizar_Click; // Asigna el handler correcto

                        // Añadir controles al panel del proyecto
                        pnlProyecto.Controls.Add(lblNombre);
                        pnlProyecto.Controls.Add(lblFechaInicio);
                        pnlProyecto.Controls.Add(lblFechaFin);
                        pnlProyecto.Controls.Add(btnDetalles);
                        pnlProyecto.Controls.Add(btnFinalizar);

                        // Añadir el panel del proyecto al FlowLayoutPanel
                        flpProyList.Controls.Add(pnlProyecto);
                    }
                }
            }
            catch (NpgsqlException ex) // Error específico de PostgreSQL
            {
                Console.WriteLine($"Error de PostgreSQL al cargar proyectos: {ex.Message} (SQLState: {ex.SqlState})");
                MessageBox.Show($"Error de base de datos al cargar proyectos.\nDetalle: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) // Otros errores inesperados
            {
                Console.WriteLine($"Error general al cargar proyectos: {ex.ToString()}");
                MessageBox.Show($"Ocurrió un error inesperado al cargar los proyectos.\nDetalle: {ex.Message}", "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Asegura que el indicador de carga se oculte siempre
                Console.WriteLine("Finalizando carga de proyectos...");
                MostrarCargando(false);
            }
        }
        private void BtnFinalizar_Click(object sender, EventArgs e)
        {
            // Recuperamos el objeto Proyecto completo que guardamos en el Tag del botón
            if (sender is Button btn && btn.Tag is Proyecto proyectoParaArchivar)
            {
                // Pide confirmación al usuario
                var confirmResult = MessageBox.Show($"¿Está seguro de que desea finalizar y archivar el proyecto '{proyectoParaArchivar.nombre}'?\n\nEsta acción establecerá la fecha de fin a hoy ({DateTime.Today:dd/MM/yyyy}) y lo ocultará de las listas activas.",
                                                      "Confirmar Finalizar Proyecto",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    DateTime fechaFin = DateTime.Today; // Fecha de finalización = hoy
                    bool exito = false;
                    this.Cursor = Cursors.WaitCursor; // Cambia el cursor mientras se procesa

                    try
                    {
                        // Llama al método del repositorio para archivar (actualizar fecha_fin y is_archived)
                        exito = proyectoRepository.ArchivarProyecto(proyectoParaArchivar.id_proyecto, fechaFin);
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores durante el archivado
                        this.Cursor = Cursors.Default; // Restaura el cursor
                        MessageBox.Show($"Ocurrió un error al intentar finalizar el proyecto:\n{ex.Message}", "Error al Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine($"ERROR BtnFinalizar_Click DB: {ex.ToString()}");
                        return; // Sale del método si hay error
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default; // Asegura restaurar el cursor normal
                    }

                    // Informa al usuario y refresca la lista si tuvo éxito
                    if (exito)
                    {
                        MessageBox.Show($"Proyecto '{proyectoParaArchivar.nombre}' finalizado y archivado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarProyectosAsync(); // Vuelve a cargar la lista para que desaparezca el proyecto archivado
                    }
                    else
                    {
                        MessageBox.Show("No se pudo finalizar el proyecto. Verifique la conexión o si el proyecto aún existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } // Fin if (confirmResult == DialogResult.Yes)
            }
            else
            {
                // Error si no se pudo obtener el objeto Proyecto del Tag
                MessageBox.Show("No se pudo obtener la información del proyecto desde el botón.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        } // Fin BtnFinalizar_Click

        private void BtnDetalles_Click(object sender, EventArgs e)
        {
            // Recupera el ID del proyecto (o el objeto completo si lo prefieres guardar en el Tag)
            if (sender is Button btn && btn.Tag is int idProyecto)
            {
                // Aquí iría la lógica para abrir otra ventana o mostrar más detalles
                MessageBox.Show($"Funcionalidad 'Detalles' para proyecto ID: {idProyecto} pendiente de implementar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
            }
            else if (sender is Button btn2 && btn2.Tag is Proyecto proyectoCompleto) // Si guardaste el objeto completo
            {
                MessageBox.Show($"Funcionalidad 'Detalles' para proyecto: {proyectoCompleto.nombre} (ID: {proyectoCompleto.id_proyecto}) pendiente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Lógica usando proyectoCompleto...
            }
        }

        // --- HANDLER PARA BOTÓN ARCHIVAR (COMPLETO) ---

        private void MostrarCargando(bool mostrar)
        {
            // Asume que tienes un Panel llamado 'pnlCargando' en el diseñador
            // y que este panel está configurado para aparecer donde quieres el mensaje.
            string nombreLabelCargando = "lblCargandoDinamic"; // Nombre único para el label dinámico

            if (mostrar)
            {
                // Oculta el FlowLayoutPanel mientras carga para evitar parpadeos
                flpProyList.Visible = false;
                // Muestra el panel de carga
                pnlCargando.Visible = true;

                // Si ya existe un label de carga anterior, lo quita por si acaso
                Control lblExistente = pnlCargando.Controls.Find(nombreLabelCargando, false).FirstOrDefault();
                if (lblExistente != null)
                {
                    pnlCargando.Controls.Remove(lblExistente);
                    lblExistente.Dispose(); // Libera recursos
                }

                // Crea y añade el nuevo label de carga
                Label lblCargando = new Label
                {
                    Name = nombreLabelCargando, // Asigna el nombre
                    Text = "Cargando proyectos...",
                    AutoSize = true,
                    Location = new Point(10, 10), // Ajusta posición dentro de pnlCargando
                    Font = new Font("Segoe UI", 12)
                };
                // Centrar el Label en el Panel pnlCargando (opcional)
                // lblCargando.Location = new Point(
                //    (pnlCargando.Width - lblCargando.Width) / 2,
                //    (pnlCargando.Height - lblCargando.Height) / 2);
                // lblCargando.Anchor = AnchorStyles.None; // Para que se mantenga centrado si pnlCargando cambia tamaño

                pnlCargando.Controls.Add(lblCargando);
            }
            else
            {
                // Oculta el panel de carga
                pnlCargando.Visible = false;
                // Muestra el FlowLayoutPanel con los resultados
                flpProyList.Visible = true;

                // Busca y elimina el label de carga que creamos
                Control lblCargando = pnlCargando.Controls.Find(nombreLabelCargando, false).FirstOrDefault();
                if (lblCargando != null)
                {
                    pnlCargando.Controls.Remove(lblCargando);
                    lblCargando.Dispose(); // Importante liberar recursos del control dinámico
                }
            }
        }
        #endregion

        #region Windows Form Designer generated code
        private void flpProyList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
        #endregion

    }
}
