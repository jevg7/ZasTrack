namespace ZasTrack.Models
{

    public class Examen
    {
        public int IdExamen { get; set; } // PK
        public int IdMuestra { get; set; } // FK
        public int IdTipoExamen { get; set; } // FK
        public int IdPaciente { get; set; } // FK
        public DateTime FechaRecepcion { get; set; } // FK
    }
}