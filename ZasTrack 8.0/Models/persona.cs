public class persona
{
    public int id_persona { get; set; } // PK
    public string nombres { get; set; }
    public string apellidos { get; set; }
    public string cedula { get; set; }
    public int id_proyecto { get; set; } // FK
}