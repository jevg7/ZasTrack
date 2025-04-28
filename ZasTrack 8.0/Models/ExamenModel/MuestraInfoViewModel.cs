// En MuestraInfoViewModel.cs
namespace ZasTrack.Models.ExamenModel
{
    public class MuestraInfoViewModel
    {
        public int id_Muestra { get; set; }
        public int NumeroMuestra { get; set; }
        public string Paciente { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string ExamenesPendientesStr { get; set; }

        public string ExamenesCompletadosStr { get; set; }

    }
}