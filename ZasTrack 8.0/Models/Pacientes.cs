namespace ZasTrack.Models
{
    public class pacientes
    {
        public int id_paciente { get; set; } // PK
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public int edad { get; set; }
        public string genero { get; set; }
        public string codigo_beneficiario { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public int id_proyecto { get; set; } // FK

        public string observacion { get; set; }
    }
}