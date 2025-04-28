namespace ZasTrack.Models
{

    public class Examen
    {
        public int IdExamen { get; set; } // PK
        public int id_Muestra { get; set; } // FK
        public int IdTipoExamen { get; set; } // FK
        public int IdPaciente { get; set; } // FK
        public DateTime FechaProcesamiento { get; set; } // FK
    }
}