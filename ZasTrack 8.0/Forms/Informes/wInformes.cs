using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Models;
using ZasTrack.Models.Informes;
using ZasTrack.Repositories;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Threading;
using Npgsql;

namespace ZasTrack.Forms.Informes
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
            informeRepository = new InformeRepository();

            this.Load += wInformes_Load;
            cmbProyectoInforme.SelectedIndexChanged += cmbProyectoInforme_SelectedIndexChanged;
            dgvListaInformes.CellContentClick += dgvListaInformes_CellContentClick;
            btnExportarTodoPdf.Click += btnExportarTodoPdf_Click;
        }

        private void wInformes_Load(object sender, EventArgs e)
        {
            ConfigurarGridInformes();
            CargarProyectosInformes();
            dgvListaInformes.DataSource = null;
            btnExportarTodoPdf.Enabled = false;
        }

        private void CargarProyectosInformes()
        {
            List<Proyecto> proyectosActivos = null;
            try
            {
                proyectosActivos = proyectoRepository.ObtenerProyectos(incluirArchivados: false);

                cmbProyectoInforme.DataSource = null;
                cmbProyectoInforme.Items.Clear();

                if (proyectosActivos != null && proyectosActivos.Any())
                {
                    cmbProyectoInforme.DataSource = proyectosActivos;
                    cmbProyectoInforme.DisplayMember = "nombre";
                    cmbProyectoInforme.ValueMember = "id_proyecto";
                    cmbProyectoInforme.SelectedIndex = -1;
                    cmbProyectoInforme.Text = "Seleccione un Proyecto...";
                    cmbProyectoInforme.Enabled = true;
                }
                else
                {
                    cmbProyectoInforme.Items.Add("No hay proyectos activos");
                    cmbProyectoInforme.SelectedIndex = 0;
                    cmbProyectoInforme.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar proyectos:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbProyectoInforme.Items.Add("Error al cargar");
                cmbProyectoInforme.SelectedIndex = 0;
                cmbProyectoInforme.Enabled = false;
            }
        }

        private void ConfigurarGridInformes()
        {
            try
            {
                dgvListaInformes.Columns.Clear();
                dgvListaInformes.AutoGenerateColumns = false;
                dgvListaInformes.AllowUserToAddRows = false;
                dgvListaInformes.AllowUserToDeleteRows = false;
                dgvListaInformes.ReadOnly = true;
                dgvListaInformes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvListaInformes.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                dgvListaInformes.ColumnHeadersDefaultCellStyle.Font = new Font(dgvListaInformes.Font, FontStyle.Bold);

                var colid_Muestra = new DataGridViewTextBoxColumn
                {
                    Name = "colid_Muestra",
                    DataPropertyName = "id_Muestra",
                    Visible = false
                };
                dgvListaInformes.Columns.Add(colid_Muestra);

                var colFecha = new DataGridViewTextBoxColumn
                {
                    Name = "colFecha",
                    HeaderText = "Fecha Proc.",
                    DataPropertyName = "FechaTomaRecepcion",
                    Width = 100
                };
                colFecha.DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvListaInformes.Columns.Add(colFecha);

                var colCodigoPac = new DataGridViewTextBoxColumn
                {
                    Name = "colCodigoPac",
                    HeaderText = "Código Pac.",
                    DataPropertyName = "CodigoPaciente",
                    Width = 110
                };
                dgvListaInformes.Columns.Add(colCodigoPac);

                var colNombresPac = new DataGridViewTextBoxColumn
                {
                    Name = "colNombresPac",
                    HeaderText = "Nombres",
                    DataPropertyName = "NombrePaciente",
                    Width = 150
                };
                dgvListaInformes.Columns.Add(colNombresPac);

                var colApellidosPac = new DataGridViewTextBoxColumn
                {
                    Name = "colApellidosPac",
                    HeaderText = "Apellidos",
                    DataPropertyName = "ApellidoPaciente",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                };
                dgvListaInformes.Columns.Add(colApellidosPac);

                var colGeneroPac = new DataGridViewTextBoxColumn
                {
                    Name = "colGeneroPac",
                    HeaderText = "Género",
                    DataPropertyName = "GeneroPaciente",
                    Width = 90
                };
                dgvListaInformes.Columns.Add(colGeneroPac);

                var colEdadPac = new DataGridViewTextBoxColumn
                {
                    Name = "colEdadPac",
                    HeaderText = "Edad",
                    DataPropertyName = "EdadPaciente",
                    Width = 50
                };
                dgvListaInformes.Columns.Add(colEdadPac);

                var colExamenes = new DataGridViewTextBoxColumn
                {
                    Name = "colExamenes",
                    HeaderText = "Exámenes Realizados",
                    DataPropertyName = "ExamenesRealizados",
                    Width = 160
                };
                dgvListaInformes.Columns.Add(colExamenes);

                var colImprimir = new DataGridViewButtonColumn
                {
                    Name = "colImprimir",
                    HeaderText = "Acción",
                    Text = "Imprimir",
                    UseColumnTextForButtonValue = true,
                    Width = 80,
                    FlatStyle = FlatStyle.Flat
                };
                colImprimir.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                colImprimir.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                dgvListaInformes.Columns.Add(colImprimir);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configurando la tabla de informes: {ex.Message}", "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void cmbProyectoInforme_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvListaInformes.DataSource = null;
            btnExportarTodoPdf.Enabled = false;

            if (cmbProyectoInforme.SelectedValue is int idProyectoSeleccionado && idProyectoSeleccionado > 0)
            {
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    List<MuestraInformeViewModel> muestras = await Task.Run(() =>
                        informeRepository.BuscarMuestrasParaInformePorProyecto(idProyectoSeleccionado)
                    );

                    dgvListaInformes.DataSource = muestras;
                    btnExportarTodoPdf.Enabled = muestras.Any();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar muestras para el informe:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void dgvListaInformes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvListaInformes.Columns[e.ColumnIndex].Name == "colImprimir")
            {
                if (dgvListaInformes.Rows[e.RowIndex].DataBoundItem is MuestraInformeViewModel item)
                {
                    GenerarEImprimirInformeIndividual(item.id_Muestra);
                }
            }
        }
        private async void btnExportarTodoPdf_Click(object sender, EventArgs e)
        {
            var listaParaExportar = dgvListaInformes.DataSource as List<MuestraInformeViewModel>;

            if (listaParaExportar != null && listaParaExportar.Any())
            {
                await GenerarPdfMultiplesInformes(listaParaExportar);
            }
            else
            {
                MessageBox.Show("No hay informes en la lista para exportar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // --- MÉTODO PARA GENERAR PDF MÚLTIPLE ---
        private async Task GenerarPdfMultiplesInformes(List<MuestraInformeViewModel> lista)
        {
            if (lista == null || !lista.Any()) return;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar Todos los Informes en PDF";
                saveFileDialog.FileName = $"Informes_{DateTime.Now:yyyyMMdd}.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaGuardar = saveFileDialog.FileName;
                    this.Cursor = Cursors.WaitCursor;

                    try
                    {
                        var document = Document.Create(container =>
                        {
                            for (int i = 0; i < lista.Count; i++)
                            {
                                var item = lista[i];
                                InformeCompletoViewModel? vmInforme = informeRepository.ObtenerDatosCompletosInforme(item.id_Muestra);

                                if (vmInforme != null)
                                {
                                    container.Page(page =>
                                    {
                                        // --- CONFIGURACIÓN DE MÁRGENES MODIFICADA ---
                                        // Margen izquierdo reducido a 15 (aprox 0.5 cm menos que 30)
                                        page.MarginVertical(30);
                                        page.MarginRight(30);
                                        page.MarginLeft(15);

                                        // Fuente base aumentada a 11
                                        page.DefaultTextStyle(ts => ts.FontSize(11).FontFamily("Lato"));

                                        page.Header().Element(c => ComposeHeader(c, vmInforme));
                                        page.Content().Element(c => ComposeContent(c, vmInforme));
                                    });
                                }
                            }
                        });

                        await Task.Run(() => document.GeneratePdf(rutaGuardar));
                        MessageBox.Show($"Se generó el archivo PDF con los informes en:\n{rutaGuardar}", "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error inesperado al guardar:\n{ex.Message}", "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        // --- MÉTODO PARA GENERAR/IMPRIMIR UN INFORME INDIVIDUAL ---
        private async void GenerarEImprimirInformeIndividual(int id_Muestra)
        {
            this.Cursor = Cursors.WaitCursor;
            InformeCompletoViewModel? viewModel = null;

            try
            {
                viewModel = await Task.Run(() => informeRepository.ObtenerDatosCompletosInforme(id_Muestra));
            }
            catch (Exception exRepo)
            {
                MessageBox.Show($"Error al obtener datos: {exRepo.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                return;
            }

            if (viewModel == null)
            {
                MessageBox.Show($"No se encontraron datos para el informe.", "Sin Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Cursor = Cursors.Default;
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string nombreCompleto = $"{viewModel.NombrePaciente} {viewModel.ApellidoPaciente}".Trim();
                string fechaArchivo = viewModel.FechaInforme.ToString("yyyy-MM-dd");
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
                                // --- CONFIGURACIÓN DE MÁRGENES MODIFICADA ---
                                // Margen izquierdo reducido a 15
                                page.MarginVertical(30);
                                page.MarginRight(30);
                                page.MarginLeft(15);

                                // Fuente base aumentada a 11
                                page.DefaultTextStyle(ts => ts.FontSize(11).FontFamily("Lato"));

                                page.Header().Element(c => ComposeHeader(c, viewModel));
                                page.Content().Element(c => ComposeContent(c, viewModel));
                            });
                        });

                        await Task.Run(() => document.GeneratePdf(rutaGuardar));
                        MessageBox.Show($"El examen se ha guardado correctamente en:\n{rutaGuardar}", "Guardado Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        try { Process.Start(new ProcessStartInfo(rutaGuardar) { UseShellExecute = true }); } catch { }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error al generar el PDF:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        // --- HELPERS DE DIBUJO ---

        void ComposeHeader(QuestPDF.Infrastructure.IContainer container, InformeCompletoViewModel model)
        {
            byte[]? logoData = null;
            try
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
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
            catch { }

            container.Column(col =>
            {
                col.Item().Row(row =>
                {
                    // Logo
                    row.ConstantItem(80).Column(logoCol =>
                    {
                        if (logoData != null)
                            logoCol.Item().Image(logoData).FitArea();
                        else
                            logoCol.Item().Height(40).Placeholder("Logo");
                    });

                    // Info Laboratorio - Aumentado a 10
                    row.RelativeItem(4).Shrink().PaddingLeft(10).Column(labCol =>
                    {
                        labCol.Item().Text("Barrio San Luis, Centro de Salud Francisco Bultrago, 1c. al Norte. Managua, Nicaragua. CP: 11097").FontSize(10);
                        labCol.Item().Text("Tel: +505 8660 2341 | Correo: info@grupo-zas.com | Web: grupo-zas.com").FontSize(10);
                    });

                    // Info Paciente - Aumentado a 11
                    row.RelativeItem(3).Shrink().PaddingLeft(10).AlignRight().Column(pacienteCol =>
                    {
                        pacienteCol.Item().Text(text =>
                        {
                            text.Span("Paciente: ").FontSize(11).Bold();
                            text.Span($"{model.NombrePaciente} {model.ApellidoPaciente}").FontSize(11);
                        });

                        if (model.FechaNacimiento != default(DateTime))
                        {
                            pacienteCol.Item().Text(text =>
                            {
                                text.Span("Fecha Nacimiento: ").FontSize(11).Bold();
                                text.Span($"{model.FechaNacimiento:dd/MM/yyyy} ({model.EdadPaciente} años)").FontSize(11);
                            });
                        }
                        else
                        {
                            pacienteCol.Item().Text(text =>
                            {
                                text.Span("Edad: ").FontSize(11).Bold();
                                text.Span($"{model.EdadPaciente} años").FontSize(11);
                            });
                        }

                        pacienteCol.Item().Text(text =>
                        {
                            text.Span("Género: ").FontSize(11).Bold();
                            text.Span(model.GeneroPaciente).FontSize(11);
                        });
                        pacienteCol.Item().Text(text =>
                        {
                            text.Span("Fecha Muestra: ").FontSize(11).Bold();
                            text.Span(model.FechaToma.ToString("dd/MM/yyyy HH:mm")).FontSize(11);
                        });
                        pacienteCol.Item().Text(text =>
                        {
                            text.Span("Fecha Informe: ").FontSize(11).Bold();
                            text.Span(model.FechaInforme.ToString("dd/MM/yyyy HH:mm")).FontSize(11);
                        });
                    });
                });

                col.Item().PaddingTop(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
            });
        }

        void ComposeContent(QuestPDF.Infrastructure.IContainer container, InformeCompletoViewModel model)
        {
            container.Column(col =>
            {
                col.Spacing(5);

                // Secciones de Resultados
                if (model.SeRealizoBHC)
                    col.Item().Element(c => ComposeExamSection(c, "HEMATOLOGÍA COMPLETA (BHC)", model.ResultadosBHC));

                if (model.SeRealizoOrinaFQ)
                    col.Item().Element(c => ComposeExamSection(c, "EXAMEN GENERAL DE ORINA (EGO) - FÍSICO QUÍMICO", model.ResultadosOrinaFQ));

                if (model.SeRealizoOrinaMicro)
                    col.Item().Element(c => ComposeExamSection(c, "EXAMEN GENERAL DE ORINA (EGO) - MICROSCÓPICO", model.ResultadosOrinaMicro));

                if (model.SeRealizoHeces)
                    col.Item().Element(c => ComposeExamSection(c, "EXAMEN GENERAL DE HECES (EGH)", model.ResultadosHeces));

                col.Item().Element(c => ComposeObservations(c, model));

                // Firma - Aumentado a 11
                col.Item().PaddingTop(10).Text("_________________________").FontSize(11).AlignCenter();
                col.Item().Text("Firma y Sello").Bold().FontSize(11).AlignCenter();
            });
        }

        void ComposeExamSection(QuestPDF.Infrastructure.IContainer container, string tituloSeccion, List<ResultadoParametroVm> resultados)
        {
            container.Column(col =>
            {
                col.Spacing(5);

                // Determinar si es Biometría (Hematología)
                bool esBiometria = tituloSeccion.Contains("HEMATOLOGÍA");
                bool tieneFondoGris = esBiometria || tituloSeccion.Contains("FÍSICO QUÍMICO") || tituloSeccion.Contains("HECES");

                // Título de Sección - Aumentado a 12
                var titleContainer = col.Item();
                if (tieneFondoGris) titleContainer = titleContainer.Background(Colors.Grey.Lighten3);

                titleContainer.PaddingLeft(5).PaddingVertical(2).Text(tituloSeccion).Bold().FontSize(12);

                if (resultados != null && resultados.Any())
                {
                    col.Item().PaddingLeft(5).Table(table =>
                    {
                        // --- DEFINICIÓN DINÁMICA DE COLUMNAS ---
                        table.ColumnsDefinition(columns =>
                        {
                            if (esBiometria)
                            {
                                // 4 Columnas para BHC
                                columns.RelativeColumn(4);      // Parámetro
                                columns.RelativeColumn(2.5f);   // Resultado
                                columns.RelativeColumn(2.5f);   // Valores normales
                                columns.RelativeColumn(1);      // Unidad
                            }
                            else
                            {
                                // 2 Columnas para el resto (ahorra espacio y se ve mejor)
                                columns.RelativeColumn(4);      // Parámetro
                                columns.RelativeColumn(2);      // Resultado (más ancho)
                            }
                        });

                        // --- ENCABEZADOS DINÁMICOS ---
                        table.Header(header =>
                        {
                            string primerHeader = esBiometria ? "Nombre del examen" : "Parámetro";

                            // Encabezados aumentados a 11
                            header.Cell().Text(primerHeader).Bold().FontSize(11);
                            header.Cell().AlignRight().Text("Resultado").Bold().FontSize(11);

                            if (esBiometria)
                            {
                                // Solo mostrar estos encabezados si es Biometría
                                header.Cell().PaddingLeft(10).Text("Valores normales").Bold().FontSize(11);
                                header.Cell().Text("Unidad").Bold().FontSize(11);
                            }
                        });

                        // --- FILAS DE DATOS ---
                        foreach (var item in resultados)
                        {
                            // Celdas aumentadas a 11
                            table.Cell().PaddingRight(5).Text(item.Parametro).FontSize(11);
                            table.Cell().AlignRight().Text(item.Resultado).FontSize(11);

                            if (esBiometria)
                            {
                                // Solo llenar estas celdas si es Biometría
                                table.Cell().PaddingLeft(10).Text(item.Referencia).FontSize(11);
                                table.Cell().Text(item.Unidad).FontSize(11);
                            }
                            // Si NO es biometría, no hacemos nada más, porque solo definimos 2 columnas.
                        }
                    });
                }
                else
                {
                    col.Item().PaddingLeft(5).PaddingTop(2).Text("Resultados no disponibles.").Italic().FontSize(11);
                }
            });
        }

        void ComposeObservations(QuestPDF.Infrastructure.IContainer container, InformeCompletoViewModel model)
        {
            container.PaddingTop(2).Column(col =>
            {
                // Texto aumentado a 11
                col.Item().Text("Observación General:").Bold().FontSize(11);
                col.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                    .PaddingBottom(2).Text(string.IsNullOrWhiteSpace(model.ObservacionesGenerales) ? "Ninguna." : model.ObservacionesGenerales).FontSize(11);
            });
        }

        private void btnExportarTodoPdf_Click_1(object sender, EventArgs e)
        {
        }
    }
}