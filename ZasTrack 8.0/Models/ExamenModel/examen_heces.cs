﻿    namespace ZasTrack.Models
    {
        public class examen_heces
        {
            public int id_examen { get; set; } // PK y FK  
            public string color { get; set; }
            public string consistencia { get; set; }
        public string parasitos { get; set; }
        public bool procesado { get; set; }
        public string observacion { get; set; }
        }
    }