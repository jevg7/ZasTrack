using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasTrack.Models.Informes
{
    public class MuestraInformeViewModel
    {
        public int id_Muestra { get; set; } // Para identificar la muestra/informe
        public DateTime FechaTomaRecepcion { get; set; } // La fecha relevante
        public string CodigoPaciente { get; set; } = string.Empty;
        public string NombrePaciente { get; set; } = string.Empty;
        public string ApellidoPaciente { get; set; } = string.Empty;
        public string GeneroPaciente { get; set; } = string.Empty;
        public int EdadPaciente { get; set; } // Edad en el momento (o actual)
        public string NombreProyecto { get; set; } = string.Empty;
        public string ExamenesRealizados { get; set; } = "Ninguno"; // El resumen de texto

        // Propiedad calculada opcional para mostrar nombre completo
        public string NombreCompletoPaciente => $"{NombrePaciente} {ApellidoPaciente}".Trim();
    }
}
