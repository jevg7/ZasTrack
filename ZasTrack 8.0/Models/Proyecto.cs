namespace ZasTrack.Models
{
    public class Proyecto
    {
        public int IdProyecto { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }        
        public bool IsArchived { get; set; } 
        public string Codigo { get; set; }   
    }
}