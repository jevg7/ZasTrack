namespace ZasTrack.Models
{

    public class examen
    {
        public int id_examen { get; set; } // PK
        public int id_persona { get; set; } // FK
        public int id_tipo_examen { get; set; } // FK
        public DateTime fecha_examen { get; set; }
        public DateTime fecha_recepcion { get; set; }
        public DateTime fecha_registro { get; set; }
    }
}