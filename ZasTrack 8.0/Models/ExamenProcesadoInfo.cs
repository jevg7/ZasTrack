using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasTrack.Models // O el namespace que uses para tus modelos/viewmodels
{
    public class ExamenProcesadoInfo
    {
        public int NumeroMuestra { get; set; }
        public string Paciente { get; set; }
        public string TipoExamen { get; set; }
        public DateTime FechaProcesamiento { get; set; }
    }
}