using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasTrack.Models
{
    public class MuestraDetalleViewModel
    {
        public int IdMuestra { get; set; }        // ID interno
        public int NumeroMuestra { get; set; }    // Para colNumMuestra
        public string CodigoPaciente { get; set; } // Para colCodigoPacienteMuestra
        public string NombrePaciente { get; set; } // Para colNombrePacienteMuestra
        public DateTime FechaRecepcion { get; set; } // Para colFechaRecepcion
        public string ExamenesSolicitados { get; set; } // Para colExamenesSolicitados (Texto generado por SQL)
        public string EstadoMuestra { get; set; }     // Para colEstadoMuestra (Texto generado por SQL: "Pendiente", "Procesada", etc.)
    }
}
