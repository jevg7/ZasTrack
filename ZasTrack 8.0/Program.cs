using QuestPDF.Infrastructure;
using ZasTrack.Forms;
using ZasTrack.Forms.Examenes;
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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            QuestPDF.Settings.License = LicenseType.Community;

            ApplicationConfiguration.Initialize();
            Application.Run(new wMain());
        }
    }
}