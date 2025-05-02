using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasTrack.Models
{
    public class ExamenDetalleViewModel
    {
        public int IdExamen { get; set; } // ID interno útil
        public int NumeroMuestra { get; set; }
        public string NombrePaciente { get; set; } // Nombre completo
        public string TipoExamen { get; set; }
        public DateTime FechaProcesamiento { get; set; }
        public string Estado { get; set; } = "Procesado"; // Asumiendo que solo listamos procesados aquí
                                                          // Podrías añadir IdPaciente, IdMuestra si los necesitas para el botón "Ver Detalles"
        public int IdPaciente { get; set; }
        public int IdMuestra { get; set; }
        public DateTime FechaRecepcion { get; set; }
    }
}
