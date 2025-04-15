namespace ZasTrack.Models
{
    public class Proyecto
    {
        public int id_proyecto { get; set; }
        public string nombre { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime? fecha_fin { get; set; }        
        public bool IsArchived { get; set; } 
        public string codigo { get; set; }   
    }
}