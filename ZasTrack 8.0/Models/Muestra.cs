using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasTrack.Models
{
    public class Muestra
    {
        public int IdMuestra { get; set; }
        public int IdPaciente { get; set; }
        public int IdTipoExamen { get; set; } // Nueva propiedad
        public int NumeroMuestra { get; set; }
        public DateTime FechaMuestra { get; set; }
        public DateTime? FechaInforme { get; set; } // Nullable porque puede no estar presente

        // Constructor sin parámetros
        public Muestra() { }

        // Constructor con parámetros
        public Muestra(int idPaciente, int idTipoExamen, int numeroMuestra, DateTime fechaMuestra, DateTime? fechaInforme)
        {
            IdPaciente = idPaciente;
            IdTipoExamen = idTipoExamen;
            NumeroMuestra = numeroMuestra;
            FechaMuestra = fechaMuestra;
            FechaInforme = fechaInforme;
        }
    }
}
