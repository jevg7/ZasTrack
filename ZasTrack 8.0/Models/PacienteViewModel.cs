using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasTrack.Models
{
    public class PacienteViewModel
    {
        // Propiedades directas del paciente
        public int id_paciente { get; set; }
        public string codigo_beneficiario { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string genero { get; set; }
        public int edad { get; set; } // Edad en años (calculada en repo o aquí)

        // Propiedad añadida desde el JOIN con Proyecto
        public string NombreProyecto { get; set; } = string.Empty; // Inicializar

        // --- NUEVAS PROPIEDADES ---
        public bool TieneMuestras { get; set; } = false; // Inicializar
        public bool TieneExamenes { get; set; } = false; // Inicializar
        public string ResumenMuestras { get; set; } = "Ninguna"; // Valor por defecto
        public string ResumenExamenes { get; set; } = "Ninguno"; // Valor por defecto
    }
}
