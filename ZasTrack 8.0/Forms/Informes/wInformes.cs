using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Models;         // Acceso a Proyecto
using ZasTrack.Models.Informes; // Acceso a ViewModels de Informe
using ZasTrack.Repositories;    // Acceso a Repositorios
// using ZasTrack.Models; // <- REDUNDANTE, QUITAR
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Threading;
using Npgsql;
// Quita usings de System.Drawing no necesarios si QuestPDF lo maneja todo

namespace ZasTrack.Forms.Informes // O tu namespace preferido
{
    public partial class wInformes : Form
    {
        // --- Repositorios ---
        private ProyectoRepository proyectoRepository;
        private InformeRepository informeRepository;
        public wInformes()
        {
            InitializeComponent();
            proyectoRepository = new ProyectoRepository();
            // --- CAMBIO: Instanciar InformeRepository ---
            informeRepository = new InformeRepository();

            // Asignar Handlers (sin cambios)
            this.Load += wInformes_Load;
            cmbProyectoInforme.SelectedIndexChanged += cmbProyectoInforme_SelectedIndexChanged;
            dgvListaInformes.CellContentClick += dgvListaInformes_CellContentClick;
            btnExportarTodoPdf.Click += btnExportarTodoPdf_Click;
        }

        // --- Evento Load: Carga inicial ---
        private void wInformes_Load(object sender, EventArgs e)
        {
            ConfigurarGridInformes();
            CargarProyectosInformes();
            dgvListaInformes.DataSource = null;
            btnExportarTodoPdf.Enabled = false;
        }

        // --- Carga Proyectos ACTIVOS en el ComboBox (SIN "Todos") ---
        private void CargarProyectosInformes()
        {
            List<Proyecto> proyectosActivos = null;
            try
            {
                Console.WriteLine("DEBUG: [CargarProyectosInformes] Cargando proyectos...");
                proyectosActivos = proyectoRepository.ObtenerProyectos(incluirArchivados: false);

                cmbProyectoInforme.DataSource = null;
                cmbProyectoInforme.Items.Clear();

                if (proyectosActivos != null && proyectosActivos.Any())
                {
                    cmbProyectoInforme.DataSource = proyectosActivos;
                    cmbProyectoInforme.DisplayMember = "nombre";
                    cmbProyectoInforme.ValueMember = "id_proyecto";
                    cmbProyectoInforme.SelectedIndex = -1; // Empezar sin selección
                    cmbProyectoInforme.Text = "Seleccione un Proyecto...";
                    cmbProyectoInforme.Enabled = true;
                    Console.WriteLine($"DEBUG: {proyectosActivos.Count} proyectos activos cargados.");
                }
                else
                {
                    Console.WriteLine("DEBUG: No se encontraron proyectos activos.");
                    cmbProyectoInforme.Items.Add("No hay proyectos activos");
                    cmbProyectoInforme.SelectedIndex = 0;
                    cmbProyectoInforme.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar proyectos:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR CargarProyectosInformes: {ex.ToString()}");
                cmbProyectoInforme.Items.Add("Error al cargar");
                cmbProyectoInforme.SelectedIndex = 0;
                cmbProyectoInforme.Enabled = false;
            }
        }

        // --- Configura las Columnas del DataGridView por Código ---
        private void ConfigurarGridInformes()
        {
            // ... (código sin cambios, usa MuestraInformeViewModel) ...
            try
            {
                dgvListaInformes.Columns.Clear();
                dgvListaInformes.AutoGenerateColumns = false; // No generar columnas automáticamente
                dgvListaInformes.AllowUserToAddRows = false;
                dgvListaInformes.AllowUserToDeleteRows = false;
                dgvListaInformes.ReadOnly = true; // Solo lectura (botones sí funcionan)
                dgvListaInformes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvListaInformes.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                dgvListaInformes.ColumnHeadersDefaultCellStyle.Font = new Font(dgvListaInformes.Font, FontStyle.Bold);

                // --- Definir Columnas según MuestraInformeViewModel ---

                // ID Muestra (Oculto) - Útil para obtener ID al hacer clic en Imprimir
                var colid_Muestra = new DataGridViewTextBoxColumn
                {
                    Name = "colid_Muestra",
                    DataPropertyName = "id_Muestra", // Coincide con MuestraInformeViewModel.id_Muestra
                    Visible = false
                };
                dgvListaInformes.Columns.Add(colid_Muestra);

                // Fecha
                var colFecha = new DataGridViewTextBoxColumn
                {
                    Name = "colFecha",
                    HeaderText = "Fecha Proc.",
                    DataPropertyName = "FechaTomaRecepcion", // Coincide con MuestraInformeViewModel
                    Width = 100
                };
                colFecha.DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvListaInformes.Columns.Add(colFecha);

                // Código Paciente
                var colCodigoPac = new DataGridViewTextBoxColumn
                {
                    Name = "colCodigoPac",
                    HeaderText = "Código Pac.",
                    DataPropertyName = "CodigoPaciente", // Coincide con MuestraInformeViewModel
                    Width = 110
                };
                dgvListaInformes.Columns.Add(colCodigoPac);

                // Nombres Paciente
                var colNombresPac = new DataGridViewTextBoxColumn
                {
                    Name = "colNombresPac",
                    HeaderText = "Nombres",
                    DataPropertyName = "NombrePaciente", // Coincide con MuestraInformeViewModel
                    Width = 150
                };
                dgvListaInformes.Columns.Add(colNombresPac);

                // Apellidos Paciente
                var colApellidosPac = new DataGridViewTextBoxColumn
                {
                    Name = "colApellidosPac",
                    HeaderText = "Apellidos",
                    DataPropertyName = "ApellidoPaciente", // Coincide con MuestraInformeViewModel
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                };
                dgvListaInformes.Columns.Add(colApellidosPac);

                // Género Paciente
                var colGeneroPac = new DataGridViewTextBoxColumn
                {
                    Name = "colGeneroPac",
                    HeaderText = "Género",
                    DataPropertyName = "GeneroPaciente", // Coincide con MuestraInformeViewModel
                    Width = 90
                };
                dgvListaInformes.Columns.Add(colGeneroPac);

                // Edad Paciente
                var colEdadPac = new DataGridViewTextBoxColumn
                {
                    Name = "colEdadPac",
                    HeaderText = "Edad",
                    DataPropertyName = "EdadPaciente", // Coincide con MuestraInformeViewModel
                    Width = 50 // Ajusta si es necesario
                };
                dgvListaInformes.Columns.Add(colEdadPac);


                // Exámenes Realizados (Resumen de texto)
                var colExamenes = new DataGridViewTextBoxColumn
                {
                    Name = "colExamenes",
                    HeaderText = "Exámenes Realizados",
                    DataPropertyName = "ExamenesRealizados", // Coincide con MuestraInformeViewModel
                    Width = 160
                };
                dgvListaInformes.Columns.Add(colExamenes);

                // Botón Imprimir
                var colImprimir = new DataGridViewButtonColumn
                {
                    Name = "colImprimir",
                    HeaderText = "Acción",
                    Text = "Imprimir",
                    UseColumnTextForButtonValue = true,
                    Width = 80,
                    FlatStyle = FlatStyle.Flat
                };
                colImprimir.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(192, 255, 192); // Verde claro
                colImprimir.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                dgvListaInformes.Columns.Add(colImprimir);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configurando la tabla de informes: {ex.Message}", "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR ConfigurarGridInformes: {ex.ToString()}");
            }
        }

        // --- Evento cuando cambia el proyecto seleccionado ---
        private async void cmbProyectoInforme_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvListaInformes.DataSource = null; // Limpiar grid al cambiar proyecto
            btnExportarTodoPdf.Enabled = false; // Deshabilitar botón

            if (cmbProyectoInforme.SelectedValue is int idProyectoSeleccionado && idProyectoSeleccionado > 0)
            {
                Console.WriteLine($"DEBUG: Proyecto seleccionado ID: {idProyectoSeleccionado}");
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    // --- CAMBIO: Llamar a informeRepository ---
                    List<MuestraInformeViewModel> muestras = await Task.Run(() =>
                        informeRepository.BuscarMuestrasParaInformePorProyecto(idProyectoSeleccionado)
                    );

                    dgvListaInformes.DataSource = muestras;
                    btnExportarTodoPdf.Enabled = muestras.Any();

                    Console.WriteLine($"DEBUG: Se encontraron {muestras?.Count ?? 0} muestras/informes para el proyecto.");
                    // ... (resto sin cambios) ...
                    if (!muestras.Any())
                    {
                        // Opcional: Mostrar mensaje si no hay muestras para ese proyecto
                        // MessageBox.Show("No se encontraron muestras/informes para el proyecto seleccionado.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar muestras para el informe:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"ERROR buscando muestras informe: {ex.ToString()}");
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                Console.WriteLine("DEBUG: No hay proyecto seleccionado o el ID es inválido.");
            }
        }

        // --- Handlers para los botones del grid y Exportar PDF ---
        private void dgvListaInformes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvListaInformes.Columns[e.ColumnIndex].Name == "colImprimir")
            {
                if (dgvListaInformes.Rows[e.RowIndex].DataBoundItem is MuestraInformeViewModel item)
                {
                    int id_Muestra = item.id_Muestra;
                    Console.WriteLine($"Clic en Imprimir para Muestra ID: {id_Muestra}");
                    // La llamada a GenerarEImprimirInformeIndividual usará informeRepository internamente
                    GenerarEImprimirInformeIndividual(id_Muestra);
                }
            }
        }
        private async void btnExportarTodoPdf_Click(object sender, EventArgs e)
        {
            var listaParaExportar = dgvListaInformes.DataSource as List<MuestraInformeViewModel>;

            if (listaParaExportar != null && listaParaExportar.Any())
            {
                Console.WriteLine($"Clic en Exportar Todo PDF para {listaParaExportar.Count} informes.");
                // La llamada a GenerarPdfMultiplesInformes usará informeRepository internamente
                await GenerarPdfMultiplesInformes(listaParaExportar);
            }
            else
            {
                MessageBox.Show("No hay informes en la lista para exportar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // --- MÉTODO PARA GENERAR PDF MÚLTIPLE (Usa informeRepository) ---
        private async Task GenerarPdfMultiplesInformes(List<MuestraInformeViewModel> lista)
        {
            // ... (Validaciones iniciales sin cambios) ...
            if (lista == null || !lista.Any())
            {
                MessageBox.Show("No hay informes en la lista para exportar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar Todos los Informes en PDF";
                saveFileDialog.FileName = $"Informes_{DateTime.Now:yyyyMMdd}.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaGuardar = saveFileDialog.FileName;
                    Console.WriteLine($"DEBUG: Se exportarán {lista.Count} informes a: {rutaGuardar}");

                    this.Cursor = Cursors.WaitCursor;
                    // Application.DoEvents(); // Considera quitarlo si async/await es suficiente

                    try
                    {
                        // --- CAMBIO (Potencial): Obtener todos los ViewModels ANTES ---
                        //    (Esto es opcional pero mejora eficiencia si son muchos)
                        // List<InformeCompletoViewModel> viewModelsCompletos = new List<InformeCompletoViewModel>();
                        // foreach(var item in lista) {
                        //    var vm = await Task.Run(() => informeRepository.ObtenerDatosCompletosInforme(item.id_Muestra));
                        //    if (vm != null) viewModelsCompletos.Add(vm);
                        //    else Console.WriteLine($"WARN: No se pudieron obtener datos para Muestra ID: {item.id_Muestra}. Se omitirá.");
                        // }
                        // Console.WriteLine($"DEBUG: Datos completos obtenidos para {viewModelsCompletos.Count} informes.");

                        // Crear el documento PDF compuesto
                        var document = Document.Create(container =>
                        {
                            // Si obtuviste los VMs antes, itera sobre viewModelsCompletos
                            // foreach (var vmInforme in viewModelsCompletos)

                            // Si mantienes la obtención dentro, itera sobre la lista original
                            for (int i = 0; i < lista.Count; i++)
                            {
                                var item = lista[i];
                                int informeNum = i + 1;
                                Console.WriteLine($"DEBUG: Obteniendo datos para informe {informeNum}/{lista.Count} (Muestra ID: {item.id_Muestra})");

                                // --- CAMBIO: Llamar a informeRepository ---
                                // (Nota: Esta llamada es síncrona aquí dentro, considera la optimización comentada arriba)
                                InformeCompletoViewModel? vmInforme = informeRepository.ObtenerDatosCompletosInforme(item.id_Muestra);

                                if (vmInforme != null)
                                {
                                    container.Page(page =>
                                    {
                                        page.Margin(30);
                                        page.DefaultTextStyle(ts => ts.FontSize(10).FontFamily(Fonts.Calibri));
                                        page.Header().Element(c => ComposeHeader(c, vmInforme));
                                        page.Content().Element(c => ComposeContent(c, vmInforme));
                                        page.Footer().AlignCenter().Text(text =>
                                        {
                                            // Actualiza el texto del footer si quieres
                                            text.Span($"Informe {informeNum} / {lista.Count} - Página ");
                                            text.CurrentPageNumber();
                                        });
                                    });
                                    Console.WriteLine($"DEBUG: Contenido añadido para Muestra ID: {item.id_Muestra}");
                                }
                                else
                                {
                                    Console.WriteLine($"WARN: No se pudieron obtener datos para Muestra ID: {item.id_Muestra}. Se omitirá.");
                                    // Podrías añadir una página de error al PDF aquí si lo deseas
                                }
                            }
                        });

                        // Generar y guardar el PDF (sin cambios)
                        Console.WriteLine("DEBUG: Generando archivo PDF final...");
                        await Task.Run(() => document.GeneratePdf(rutaGuardar));
                        Console.WriteLine("DEBUG: PDF generado.");
                        MessageBox.Show($"Se generó el archivo PDF con los informes en:\n{rutaGuardar}", "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine($"Error de PostgreSQL al guardar paciente: {ex.Message} (SQLState: {ex.SqlState})");
                        // Mostrar un mensaje más amigable al usuario
                        MessageBox.Show($"No se pudo guardar el paciente debido a un error de base de datos.\nDetalle técnico: {ex.SqlState}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // No relanzar (throw) aquí usualmente, manejar el error mostrando mensaje.
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error general al guardar paciente: {ex.ToString()}");
                        MessageBox.Show($"Ocurrió un error inesperado al guardar el paciente:\n{ex.Message}", "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // No relanzar (throw) aquí usualmente.
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        // --- MÉTODO PARA GENERAR/IMPRIMIR UN Informe (Usa informeRepository) ---
        private async void GenerarEImprimirInformeIndividual(int id_Muestra)
        {
            Console.WriteLine($"DEBUG: Iniciando generación de informe para Muestra ID: {id_Muestra}");
            this.Cursor = Cursors.WaitCursor;
            InformeCompletoViewModel? viewModel = null;

            // 1. Obtener datos completos (sin cambios)
            try
            {
                viewModel = await Task.Run(() => informeRepository.ObtenerDatosCompletosInforme(id_Muestra));
            }
            catch (Exception exRepo)
            {
                MessageBox.Show($"Error al obtener datos del informe: {exRepo.Message}", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR Repositorio ObtenerDatosCompletosInforme: {exRepo.ToString()}");
                this.Cursor = Cursors.Default;
                return;
            }

            if (viewModel == null)
            {
                MessageBox.Show($"No se encontraron datos para el informe (Muestra ID: {id_Muestra}).", "Sin Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Cursor = Cursors.Default;
                return;
            }

            // --- CAMBIO 1: Usar SaveFileDialog en lugar de una ruta temporal ---
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string nombreCompleto = $"{viewModel.NombrePaciente} {viewModel.ApellidoPaciente}".Trim();
                string fechaArchivo = viewModel.FechaInforme.ToString("yyyy-MM-dd");

                // --- CAMBIO: El nombre del archivo ahora dice "Examen" ---
                saveFileDialog.FileName = $"Examen - {nombreCompleto} - {fechaArchivo}.pdf";

                saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar Examen PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaGuardar = saveFileDialog.FileName;

                    try
                    {
                        var document = Document.Create(container =>
                        {
                            container.Page(page =>
                            {
                                page.Margin(30);
                                page.DefaultTextStyle(ts => ts.FontSize(10).FontFamily(Fonts.Calibri));
                                page.Header().Element(c => ComposeHeader(c, viewModel));
                                page.Content().Element(c => ComposeContent(c, viewModel));
                                page.Footer().AlignCenter().Text(text => { text.Span("Página "); text.CurrentPageNumber(); });
                            });
                        });

                        await Task.Run(() => document.GeneratePdf(rutaGuardar));
                        Console.WriteLine($"DEBUG: PDF guardado en: {rutaGuardar}");
                        MessageBox.Show($"El examen se ha guardado correctamente en:\n{rutaGuardar}", "Guardado Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(new ProcessStartInfo(rutaGuardar) { UseShellExecute = true });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error al generar o guardar el PDF:\n{ex.Message}", "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine($"ERROR generando PDF individual: {ex.ToString()}");
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        // --- HELPERS DE QUESTPDF (ComposeHeader, ComposeContent, etc.) ---
        //     (Sin cambios, permanecen en wInformes.cs ya que son parte de la UI/Presentación)
        void ComposeHeader(QuestPDF.Infrastructure.IContainer container, InformeCompletoViewModel model)
        {
            byte[]? logoData = null;

            // Carga el logo como un recurso incrustado para que funcione después de instalar la app.
            try
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();

                // El nombre del recurso es: "TuNamespace.NombreDeLaCarpeta.NombreDelArchivo"
                // Asegúrate de que "ZasTrack" es tu namespace principal.
                string resourceName = "ZasTrack.Resources.Zas-log.ico";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            stream.CopyTo(ms);
                            logoData = ms.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Si falla, se mostrará un error en la consola de depuración.
                Console.WriteLine($"ERROR: No se pudo cargar el recurso del logo. {ex.Message}");
            }

            // Se envuelve todo en una columna principal para evitar el error de "múltiples hijos".
            container.Column(col =>
            {
                // El primer elemento de la columna es una fila que contiene todo el diseño.
                col.Item().Row(row =>
                {
                    // Columna 1: Logo (ocupa un ancho fijo de 80)
                    // Columna 1: Logo
                    row.ConstantItem(80).Column(logoCol =>
                    {
                        if (logoData != null)
                            // CAMBIO A .FitArea() PARA ASEGURAR QUE LA IMAGEN QUEPA
                            logoCol.Item().Image(logoData).FitArea();
                        else
                            logoCol.Item().Height(40).Placeholder("Logo");
                    });

                    // Columna 2: Información del Laboratorio (ocupa 4 partes del espacio restante)
                    row.RelativeItem(4).Shrink().PaddingLeft(10).Column(labCol =>
                    {
                        labCol.Item().Text("Barrio San Luis, Centro de Salud Francisco Bultrago, 1c. al Norte. Managua, Nicaragua. CP: 11097").FontSize(7);
                        labCol.Item().Text("Tel: +505 8660 2341 | Correo: info@grupo-zas.com | Web: grupo-zas.com").FontSize(7);
                    });

                    // Columna 3: Información del Paciente (ocupa 3 partes del espacio restante)
                    row.RelativeItem(3).Shrink().PaddingLeft(10).AlignRight().Column(pacienteCol =>
                    {
                        pacienteCol.Item().Text(text =>
                        {
                            text.Span("Paciente: ").FontSize(8).Bold();
                            text.Span($"{model.NombrePaciente} {model.ApellidoPaciente}").FontSize(8);
                        });

                        // Muestra la fecha de nacimiento si está disponible, si no, solo la edad.
                        if (model.FechaNacimiento != default(DateTime))
                        {
                            pacienteCol.Item().Text(text =>
                            {
                                text.Span("Fecha Nacimiento: ").FontSize(8).Bold();
                                text.Span($"{model.FechaNacimiento:dd/MM/yyyy} ({model.EdadPaciente} años)").FontSize(8);
                            });
                        }
                        else
                        {
                            pacienteCol.Item().Text(text =>
                            {
                                text.Span("Edad: ").FontSize(8).Bold();
                                text.Span($"{model.EdadPaciente} años").FontSize(8);
                            });
                        }

                        pacienteCol.Item().Text(text =>
                        {
                            text.Span("Género: ").FontSize(8).Bold();
                            text.Span(model.GeneroPaciente).FontSize(8);
                        });
                        pacienteCol.Item().Text(text =>
                        {
                            text.Span("Fecha Muestra: ").FontSize(8).Bold();
                            text.Span(model.FechaToma.ToString("dd/MM/yyyy HH:mm")).FontSize(8);
                        });
                        pacienteCol.Item().Text(text =>
                        {
                            text.Span("Fecha Informe: ").FontSize(8).Bold();
                            text.Span(model.FechaInforme.ToString("dd/MM/yyyy HH:mm")).FontSize(8);
                        });
                    });
                });

                // El segundo elemento de la columna es la línea separadora.
                col.Item().PaddingTop(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
            });
        }
        void ComposeContent(QuestPDF.Infrastructure.IContainer container, InformeCompletoViewModel model)
        {
            container.Column(col =>
            {
                col.Spacing(15);
                // --- CAMBIO: Se eliminó la línea que llamaba a ComposePatientInfo ---
                // col.Item().Element(c => ComposePatientInfo(c, model)); // ¡Eliminar esta línea!

                // Secciones de Resultados (Usa las listas del ViewModel)
                if (model.SeRealizoBHC)
                    col.Item().Element(c => ComposeExamSection(c, "HEMATOLOGÍA COMPLETA (BHC)", model.ResultadosBHC));

                if (model.SeRealizoOrinaFQ)
                    col.Item().Element(c => ComposeExamSection(c, "EXAMEN GENERAL DE ORINA (EGO) - FÍSICO QUÍMICO", model.ResultadosOrinaFQ));

                if (model.SeRealizoOrinaMicro)
                    col.Item().Element(c => ComposeExamSection(c, "EXAMEN GENERAL DE ORINA (EGO) - MICROSCÓPICO", model.ResultadosOrinaMicro));

                if (model.SeRealizoHeces)
                    col.Item().Element(c => ComposeExamSection(c, "EXAMEN GENERAL DE HECES (EGH)", model.ResultadosHeces));

                // Añadir más secciones con la misma lógica 'if'...

                col.Item().Element(c => ComposeObservations(c, model));
                col.Item().PaddingTop(30).Text("_________________________").AlignCenter();
                col.Item().Text("Firma y Sello").AlignCenter();
            });
        }
        // Dibuja la Información del Paciente
        // Dibuja la Información del Paciente
        void ComposePatientInfo(QuestPDF.Infrastructure.IContainer container, InformeCompletoViewModel model)
        {
            // --- CAMBIO: Se usa el compositor de texto para combinar negrita y texto normal ---
            container.Grid(grid =>
            {
                grid.Columns(12); // Dividir en 12 columnas

                // Fila 1
                grid.Item(12).Text(text =>
                {
                    text.Span("Paciente: ").FontSize(9).Bold();
                    text.Span($"{model.NombrePaciente} {model.ApellidoPaciente}").FontSize(9);
                });

                // Fila 2
                // ASUMO que agregaste 'FechaNacimiento' a tu 'InformeCompletoViewModel'
                // Si no lo has hecho, debes agregarlo para que esto funcione.
                grid.Item(6).Text(text =>
                {
                    text.Span("Fecha de Nacimiento: ").FontSize(9).Bold();
                    text.Span($"{model.FechaNacimiento:dd/MM/yyyy} ({model.EdadPaciente} años)").FontSize(9);
                });
                grid.Item(6).Text(text =>
                {
                    text.Span("Género: ").FontSize(9).Bold();
                    text.Span(model.GeneroPaciente).FontSize(9);
                });

                // Fila 3
                grid.Item(6).Text(text =>
                {
                    text.Span("Fecha Muestra: ").FontSize(9).Bold();
                    text.Span(model.FechaToma.ToString("dd/MM/yyyy HH:mm")).FontSize(9);
                });
                grid.Item(6).Text(text =>
                {
                    text.Span("Fecha Informe: ").FontSize(9).Bold();
                    text.Span(model.FechaInforme.ToString("dd/MM/yyyy HH:mm")).FontSize(9);
                });

                // --- CAMBIO: Se eliminaron las líneas de Código y Proyecto ---
            });
        }

        // Dibuja UNA Sección de Examen (Tabla de resultados o "NO SE REALIZO")
        // El parámetro 'seRealizo' se eliminó porque la verificación ahora se hace antes de llamar a este método
        void ComposeExamSection(QuestPDF.Infrastructure.IContainer container, string tituloSeccion, List<ResultadoParametroVm> resultados)
        {
            container.Column(col =>
            {
                col.Spacing(5);
                col.Item().Background(Colors.Grey.Lighten3).PaddingLeft(5).PaddingVertical(2).Text(tituloSeccion).Bold();

                // Ya sabemos que se realizó, solo verificamos que haya resultados para dibujar la tabla
                if (resultados != null && resultados.Any())
                {
                    col.Item().PaddingLeft(5).Table(table =>
                    {
                        // ... el resto de la lógica de la tabla no cambia ...
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3); columns.RelativeColumn(2);
                        });
                        table.Header(header =>
                        {
                            header.Cell().Text("Parámetro").Bold().FontSize(9);
                            header.Cell().Text("Resultado").Bold().FontSize(9);
                        });
                        foreach (var item in resultados)
                        {
                            table.Cell().PaddingRight(5).Text(item.Parametro).FontSize(9);
                            table.Cell().Text(item.Resultado).FontSize(9);
                        }
                    });
                }
                else
                {
                    // Opcional: si la lista está vacía, puedes poner un mensaje o dejarlo en blanco.
                    col.Item().PaddingLeft(5).PaddingTop(2).Text("Resultados no disponibles.").Italic().FontSize(9);
                }
            });
        }

        // Dibuja las Observaciones
        void ComposeObservations(QuestPDF.Infrastructure.IContainer container, InformeCompletoViewModel model)
        {
            container.PaddingTop(10).Column(col =>
            { // Añadido PaddingTop
                col.Item().Text("Observación General:").Bold().FontSize(9); // Ajustado texto y tamaño
                col.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                    .PaddingBottom(2).Text(string.IsNullOrWhiteSpace(model.ObservacionesGenerales) ? "Ninguna." : model.ObservacionesGenerales).FontSize(9);
            });
        }

        private void btnExportarTodoPdf_Click_1(object sender, EventArgs e)
        {

        }
    } // Fin clase wInformes
} // Fin namespace