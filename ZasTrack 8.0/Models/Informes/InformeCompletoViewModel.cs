using System;
using System.Collections.Generic;
using System.Linq; // Necesario para .Any()
using ZasTrack.Models; // Necesitas acceso a los modelos de resultados? Revisa si es necesario.

namespace ZasTrack.Models.Informes // O el namespace que prefieras
{
    // --- Clases Auxiliares para Resultados Detallados ---
    public class ResultadoParametroVm
    {
        public string Parametro { get; set; } = string.Empty;
        public string Resultado { get; set; } = string.Empty;
        public string Unidad { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        // public bool FueraDeRango { get; set; }
    }

    // Comentadas ya que usas ResultadoParametroVm para todo
    // public class ResultadoOrinaFqVm { /* ... */ }
    // public class ResultadoOrinaMicroVm { /* ... */ }
    // public class ResultadoHecesVm { /* ... */ }


    // --- Clase Principal para los Datos del Informe ---
    public class InformeCompletoViewModel
    {
        // Información General y del Paciente
        public int IdMuestra { get; set; } // <-- PROPIEDAD CORRECTA

        public string NombreLaboratorio { get; set; } = "Zas! Medical"; // O leer de config
        public string DireccionLaboratorio { get; set; } = "Bo. San Luis..."; // O leer de config
        public string ContactoLaboratorio { get; set; } = "Telefono: ... Email: ..."; // O leer de config
        // public int id_Muestra { get; set; } // <-- ELIMINADA (duplicada)
        public DateTime FechaToma { get; set; }
        public DateTime FechaInforme { get; set; } = DateTime.Now; // Fecha de generación
        public string CodigoMuestraCompleto { get; set; } = string.Empty; // Podría ser ID Muestra o uno más complejo

        public string NombrePaciente { get; set; } = string.Empty;
        public string ApellidoPaciente { get; set; } = string.Empty;
        public int EdadPaciente { get; set; } // O string formateado "N años (X meses)"
        public string GeneroPaciente { get; set; } = string.Empty;
        public string CodigoBeneficiario { get; set; } = string.Empty;
        public string NombreProyecto { get; set; } = string.Empty;


        // Listas con los resultados detallados de cada examen REALIZADO
        public List<ResultadoParametroVm> ResultadosBHC { get; set; } = new List<ResultadoParametroVm>();
        public List<ResultadoParametroVm> ResultadosOrinaFQ { get; set; } = new List<ResultadoParametroVm>();
        public List<ResultadoParametroVm> ResultadosOrinaMicro { get; set; } = new List<ResultadoParametroVm>();
        public List<ResultadoParametroVm> ResultadosHeces { get; set; } = new List<ResultadoParametroVm>();

        // Observaciones generales
        public string ObservacionesGenerales { get; set; } = string.Empty;

        // Propiedad para saber si se realizó un tipo de examen
        // Se corrigió la lógica para chequear null Y Any()
        public bool SeRealizoBHC => ResultadosBHC != null && ResultadosBHC.Any();
        public bool SeRealizoOrinaFQ => ResultadosOrinaFQ != null && ResultadosOrinaFQ.Any();
        public bool SeRealizoOrinaMicro => ResultadosOrinaMicro != null && ResultadosOrinaMicro.Any();
        public bool SeRealizoHeces => ResultadosHeces != null && ResultadosHeces.Any();

        // Constructor vacío
        public InformeCompletoViewModel() { }
    }
}