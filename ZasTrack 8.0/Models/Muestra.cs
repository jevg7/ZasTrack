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
        public int IdProyecto { get; set; }
        public int IdPaciente { get; set; }
        public int IdTipoExamen { get; set; }
        public int NumeroMuestra { get; set; }
        public DateTime FechaRecepcion { get; set; }

        // Constructor sin parámetros
        public Muestra() { }

        // Constructor con parámetros
        public Muestra(int idProyecto, int idPaciente, int idTipoExamen, int numeroMuestra, DateTime fechaRecepcion)
        {
            IdProyecto = idProyecto;
            IdPaciente = idPaciente;
            IdTipoExamen = idTipoExamen;
            NumeroMuestra = numeroMuestra;
            FechaRecepcion = fechaRecepcion;
        }
    }
}
