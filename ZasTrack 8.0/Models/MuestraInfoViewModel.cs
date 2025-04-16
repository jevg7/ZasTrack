// Puedes poner esto en tu carpeta Models o en un lugar adecuado
namespace ZasTrack.Models // O el namespace que uses para ViewModels/DTOs
{
    public class MuestraInfoViewModel
    {
        public int IdMuestra { get; set; }
        public int NumeroMuestra { get; set; }
        public string Paciente { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string ExamenesPendientesStr { get; set; } // Aquí irá la cadena agregada
    }
}