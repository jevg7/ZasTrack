using System;
using System.Collections.Generic;
using System.Linq;
using QuestPDF.Drawing; // NuGet: QuestPDF
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ZasTrack.Models.Informes;
using ZasTrack.Models; // O donde esté InformeCompletoViewModel y ResultadoParametroVm

namespace ZasTrack.Models.Informes // O tu namespace preferido
{
    public class InformeDocument : IDocument
    {
        // Guarda el ViewModel con todos los datos del informe
        public InformeCompletoViewModel Model { get; }

        // Constructor que recibe los datos
        public InformeDocument(InformeCompletoViewModel model)
        {
            Model = model;
        }

        // Metadata del documento (opcional)
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        // --- AQUÍ SE DEFINE TODA LA ESTRUCTURA DEL PDF ---
        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    // Configuración de la página (Tamaño A4 por defecto)
                    page.Margin(30); // Márgenes en puntos (1 pulgada = 72 puntos)
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(ts => ts.FontSize(10).FontFamily(Fonts.Calibri)); // Fuente por defecto

                    // Cabecera de la página (repetida en cada página si hay más de una)
                    page.Header().Element(ComposeHeader);

                    // Contenido principal de la página
                    page.Content().Element(ComposeContent);

                    // Pie de página (repetido en cada página)
                    // Se elimina numeración de páginas en el pie
                    // No se define footer para que esté vacío
                });
        }

        // --- MÉTODO PARA DIBUJAR LA CABECERA ---
        void ComposeHeader(IContainer container)
        {
            // --- ¡¡ADAPTA ESTO CON TU INFO!! ---
            container.Row(row =>
            {
                // Logo (si tienes la ruta o los bytes)
                // row.ConstantItem(100).Image("ruta/a/tu/logo.png"); // O .Image(byte[])

                row.RelativeItem().Column(col =>
                {
                    col.Item().Text(Model.NombreLaboratorio).Bold().FontSize(14);
                    col.Item().Text(Model.DireccionLaboratorio).FontSize(9);
                    col.Item().Text(Model.ContactoLaboratorio).FontSize(9);
                });

                // Espacio si no hay logo
                // row.ConstantItem(100);

                // Podrías añadir un código de barras o QR aquí a la derecha si lo usas
                row.ConstantItem(100).Placeholder("Código Barras"); // Espacio para código barras
            });
            container.PaddingTop(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten1); // Línea separadora
        }

        // --- MÉTODO PARA DIBUJAR EL CONTENIDO PRINCIPAL ---
        void ComposeContent(IContainer container)
        {
            container.Column(col =>
            {
                col.Spacing(15); // Espacio entre elementos principales

                // Sección de Datos del Paciente y Muestra
                col.Item().Element(ComposePatientInfo);

                // --- Secciones de Resultados ---
                // Para cada tipo de examen, incluimos el título y luego
                // la tabla de resultados O el texto "NO SE REALIZO"

                // Hematología / BHC
                col.Item().Element(c => ComposeExamSection(c, "HEMATOLOGÍA COMPLETA (BHC)", Model.SeRealizoBHC, Model.ResultadosBHC));

                // Orina Físico-Químico
                col.Item().Element(c => ComposeExamSection(c, "EXAMEN GENERAL DE ORINA (EGO) - FÍSICO QUÍMICO", Model.SeRealizoOrinaFQ, Model.ResultadosOrinaFQ));

                // Orina Microscópico
                col.Item().Element(c => ComposeExamSection(c, "EXAMEN GENERAL DE ORINA (EGO) - MICROSCÓPICO", Model.SeRealizoOrinaMicro, Model.ResultadosOrinaMicro));

                // Heces
                col.Item().Element(c => ComposeExamSection(c, "EXAMEN GENERAL DE HECES (EGH)", Model.SeRealizoHeces, Model.ResultadosHeces));

                // Añadir más secciones para otros tipos de examen si los tienes...

                // Observaciones Generales
                col.Item().Element(ComposeObservations);

                // Línea y Firma al final (dentro del contenido o en un footer más complejo)
                col.Item().PaddingTop(30).LineHorizontal(0.5f);
                col.Item().AlignCenter().PaddingTop(5).Text("_________________________").FontSize(10);
                col.Item().AlignCenter().Text("Bioanalista Clínico").Bold().FontSize(10);


            });
        }

        // --- MÉTODO PARA DIBUJAR LA INFO DEL PACIENTE ---
        void ComposePatientInfo(IContainer container)
        {
            container.ShowOnce().Column(col => // ShowOnce para que no se repita en multi-página si el contenido es largo
            {
                col.Item().Text("Datos del Paciente").Bold().Underline();
                col.Spacing(2);
                // Usar tabla o grid para alinear bien los datos
                col.Item().Grid(grid => {
                    // Ajuste: usar 12 columnas para permitir spans 2/10 y 2/4/2/4 por fila
                    grid.Columns(12);
                    grid.Item(2).Text("Paciente:").FontSize(9).Bold();
                    grid.Item(10).Text($"{Model.NombrePaciente} {Model.ApellidoPaciente}").FontSize(9); // Ocupa 10 columnas para extenderse

                    grid.Item(2).Text("Edad:").FontSize(9).Bold();
                    grid.Item(4).Text($"{Model.EdadPaciente} años").FontSize(9); // O usa tu string formateado
                    grid.Item(2).Text("Fecha Muestra:").FontSize(9).Bold();
                    grid.Item(4).Text(Model.FechaToma.ToString("dd/MM/yyyy HH:mm")).FontSize(9); // Incluir hora si la tienes

                    grid.Item(2).Text("Género:").FontSize(9).Bold();
                    grid.Item(4).Text(Model.GeneroPaciente).FontSize(9);
                    grid.Item(2).Text("Fecha Informe:").FontSize(9).Bold();
                    grid.Item(4).Text(Model.FechaInforme.ToString("dd/MM/yyyy HH:mm")).FontSize(9);

                    grid.Item(2).Text("Código:").FontSize(9).Bold();
                    grid.Item(4).Text(Model.CodigoBeneficiario).FontSize(9);
                    grid.Item(2).Text("Proyecto:").FontSize(9).Bold();
                    grid.Item(4).Text(Model.NombreProyecto).FontSize(9);

                });
            });
        }

        // --- MÉTODO REUTILIZABLE PARA DIBUJAR UNA SECCIÓN DE EXAMEN ---
        void ComposeExamSection(IContainer container, string tituloSeccion, bool seRealizo, List<ResultadoParametroVm> resultados)
        {
            container.Column(col => {
                col.Spacing(5);
                
                // Determinar si debe tener fondo gris y cuántas columnas usar
                bool tieneFondoGris = tituloSeccion.Contains("HEMATOLOGÍA") || tituloSeccion.Contains("FÍSICO QUÍMICO") || tituloSeccion.Contains("HECES");
                bool usar4Columnas = tituloSeccion.Contains("HEMATOLOGÍA");
                
                // Título de la Sección
                if (tieneFondoGris)
                {
                    col.Item().Background(Colors.Grey.Lighten3).PaddingLeft(5).PaddingVertical(2).Text(tituloSeccion).Bold();
                }
                else
                {
                    col.Item().PaddingLeft(5).PaddingVertical(2).Text(tituloSeccion).Bold();
                }

                // Contenido: Tabla de resultados O el texto "NO SE REALIZO"
                if (seRealizo && resultados != null && resultados.Any())
                {
                    // Dibuja la tabla si hay resultados
                    col.Item().PaddingLeft(5).Table(table => {
                        if (usar4Columnas)
                        {
                            // Definir columnas: Parámetro, Resultado, Valores normales, Unidad
                            table.ColumnsDefinition(columns => {
                                columns.RelativeColumn(1); // Parámetro (compacto)
                                columns.RelativeColumn(1); // Resultado
                                columns.RelativeColumn(1); // Valores normales (compacto)
                                columns.RelativeColumn(1); // Unidad (compacto)
                            });

                            table.Header(header => {
                                header.Cell().Text("Nombre del examen").Bold();
                                header.Cell().AlignRight().Text("Resultado").Bold();
                                header.Cell().Text("Valores normales").Bold();
                                header.Cell().Text("Unidad").Bold();
                            });

                            // Filas de datos
                            foreach (var item in resultados)
                            {
                                table.Cell().PaddingRight(5).Text(item.Parametro).FontSize(9);
                                table.Cell().AlignRight().Text(item.Resultado).FontSize(9);
                                table.Cell().Text(item.Referencia).FontSize(9);
                                table.Cell().Text(item.Unidad).FontSize(9);
                            }
                        }
                        else
                        {
                            // Definir 2 columnas: Parámetro, Resultado (para microscópico y heces)
                            table.ColumnsDefinition(columns => {
                                columns.RelativeColumn(1); // Parámetro (compacto)
                                columns.RelativeColumn(1); // Resultado (pegado al parámetro)
                            });

                            table.Header(header => {
                                header.Cell().Text("Parámetro").Bold();
                                header.Cell().AlignRight().Text("Resultado").Bold();
                            });

                            // Filas de datos
                            foreach (var item in resultados)
                            {
                                table.Cell().PaddingRight(5).Text(item.Parametro).FontSize(9);
                                table.Cell().AlignRight().Text(item.Resultado).FontSize(9);
                            }
                        }
                    });
                }
                else
                {
                    // Mostrar "NO SE REALIZO" si no hay resultados
                    col.Item().PaddingLeft(5).Text("NO SE REALIZO").Italic().FontSize(9);
                }
            });
        }

        // --- MÉTODO PARA DIBUJAR OBSERVACIONES ---
        void ComposeObservations(IContainer container)
        {
            container.Column(col => {
                col.Item().Text("Observación:").Bold();
                col.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten2) // Línea debajo de "Observación:"
                       .PaddingBottom(2).Text(string.IsNullOrWhiteSpace(Model.ObservacionesGenerales) ? "Ninguna." : Model.ObservacionesGenerales).FontSize(9);

            });
        }

    } // Fin clase InformeDocument
} // Fin namespace