using System;
using System.IO; // Necesario para manejar archivos y rutas
using System.Windows.Forms;
using QuestPDF.Drawing; // Necesario para FontManager
using QuestPDF.Infrastructure;
using ZasTrack.Forms;
using ZasTrack.Forms.Examenes;
using ZasTrack.Forms.Examenes.ExamWrite;
using ZasTrack.Forms.Muestras;
using ZasTrack.Forms.wProyectos;

namespace ZasTrack
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configuración de QuestPDF
            QuestPDF.Settings.License = LicenseType.Community;
            QuestPDF.Settings.EnableDebugging = true;

            // --- INICIO DEL PARCHE: REGISTRO DE FUENTES LATO ---
            try
            {
                // Busca la carpeta "LatoFont" en el directorio donde corre el .exe
                string fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LatoFont");

                if (Directory.Exists(fontPath))
                {
                    // Registra las variantes que usas (Regular y Bold)
                    // Asegúrate de que los nombres de archivo coincidan con los que tienes en la carpeta
                    var regularFont = Path.Combine(fontPath, "Lato-Regular.ttf");
                    var boldFont = Path.Combine(fontPath, "Lato-Bold.ttf");

                    if (File.Exists(regularFont))
                        FontManager.RegisterFont(File.OpenRead(regularFont));

                    if (File.Exists(boldFont))
                        FontManager.RegisterFont(File.OpenRead(boldFont));

                    // Si usas Italic en algún lado, descomenta esto:
                    // var italicFont = Path.Combine(fontPath, "Lato-Italic.ttf");
                    // if (File.Exists(italicFont)) FontManager.RegisterFont(File.OpenRead(italicFont));
                }
            }
            catch (Exception ex)
            {
                // Si falla la carga de fuentes, mostramos un aviso pero dejamos que el programa continúe
                // Usará la fuente por defecto del sistema si esto falla.
                MessageBox.Show($"Advertencia: No se pudieron cargar las fuentes Lato para los informes.\nError: {ex.Message}", "Aviso de Fuente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // --- FIN DEL PARCHE ---

            ApplicationConfiguration.Initialize();
            Application.Run(new wMain());
        }
    }
}