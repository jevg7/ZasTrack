    namespace ZasTrack.Models
    {
        public class examen_sangre
        {
            public int id_examen { get; set; } // PK y FK
            public decimal globulos_rojos { get; set; }
            public decimal hematocrito { get; set; }
            public decimal hemoglobina { get; set; }
            public decimal leucocitos { get; set; }
            public decimal mcv { get; set; }
            public decimal mch { get; set; }
            public decimal mchc { get; set; }

            public decimal neutrofilos { get; set; }
            public decimal linfocitos { get; set; }
            public decimal monocitos { get; set; }
            public decimal eosinofilos { get; set; }
            public decimal basofilos { get; set; }
        public bool procesado { get; set; }
        public string observacion { get; set; }
        }
    }