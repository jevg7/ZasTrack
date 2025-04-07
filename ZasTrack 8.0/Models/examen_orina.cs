namespace ZasTrack.Models
{
    public class examen_orina
    {
        public int id_examen { get; set; } // PK y FK
        public string color { get; set; }
        public decimal ph { get; set; }
        public string aspecto { get; set; }
        public decimal densidad { get; set; }
        public string leucocitos { get; set; }
        public string hemoglobina { get; set; }
        public string nitritos { get; set; }
        public string cetonas { get; set; }
        public string urobilinogeno { get; set; }
        public string bilirrubinas { get; set; }
        public string proteina { get; set; }
        public string glucosa { get; set; }
        public string celulas_epiteliales { get; set; }
        public string bacterias { get; set; }
        public string cristales { get; set; }
        public string cilindros { get; set; }
        public string leucocitos_micro { get; set; }
        public string observaciones { get; set; }

    }
}