﻿public class proyecto
{
    public int id_proyecto { get; set; } // PK
    public string nombre { get; set; }
    public DateTime fecha_inicio { get; set; }
    public DateTime? fecha_fin { get; set; }
}