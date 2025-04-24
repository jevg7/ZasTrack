using System;
using System.Collections.Generic; // Para List<>
using System.Data; // Para DataTable (si lo usaras)
using System.Drawing; // Para FontStyle, Color
using System.Linq; // Necesario para .Any()
using System.Windows.Forms;
using ZasTrack.Models; // Asegúrate que el namespace de tus modelos sea correcto
// Si ResultadoImportacion y ErrorImportacion están en otro namespace, añade el using correspondiente

namespace ZasTrack.Forms.Pacientes // O tu namespace correcto
{
    public partial class wResultadosImportacion : Form
    {
        // Variable para guardar los resultados recibidos
        private ResultadoImportacion resultadosImportacion;

        // --- Constructor: Recibe el objeto con los resultados ---
        // --- Asegúrate que ResultadoImportacion sea el tipo correcto (definido FUERA de las clases de formulario) ---
        public wResultadosImportacion(ResultadoImportacion resultados)
        {
            InitializeComponent(); // Carga los controles del diseñador
            this.resultadosImportacion = resultados; // Guarda los resultados

            // Asignar eventos (si no se hizo en diseñador)
            this.Load += wResultadosImportacion_Load;
            btnCerrar.Click += BtnCerrar_Click;
            // dgvExitosos.CellContentClick += dgvExitosos_CellContentClick; // Si necesitas manejar clics en la tabla
        }

        // --- Evento Load: Se ejecuta cuando el formulario se carga ---
        private void wResultadosImportacion_Load(object sender, EventArgs e)
        {
            // Verificación de seguridad
            if (resultadosImportacion == null)
            {
                lblResumen.Text = "No se recibieron resultados.";
                return;
            }

            // 1. Mostrar Resumen en el Label (lblResumen)
            // Usa la propiedad calculada 'Exitosos' o el Count de la lista
            lblResumen.Text = $"Pacientes importados exitosamente: {resultadosImportacion.Exitosos}\n" +
                              $"Filas con errores: {resultadosImportacion.Errores.Count}";

            // 2. Mostrar Pacientes Exitosos en el DataGridView (dgvExitosos)
            dgvExitosos.DataSource = null; // Limpiar enlace anterior por si acaso

            dgvExitosos.AutoGenerateColumns = false;
            // ----------------------------------------------------

            if (resultadosImportacion.PacientesGuardados != null && resultadosImportacion.PacientesGuardados.Any())
            {
                // Asignar la lista de pacientes guardados como origen de datos
                // Como AutoGenerateColumns es False, SOLO se llenarán las columnas
                // que tú definiste en el diseñador Y cuyo 'DataPropertyName'
                // coincide con una propiedad de la clase 'pacientes'.
                dgvExitosos.DataSource = resultadosImportacion.PacientesGuardados;

                // --- Configuración Opcional de Columnas  ---
                // Puedes ajustar títulos, visibilidad, formato aquí después de asignar DataSource
                /*
                try {
                    if (dgvExitosos.Columns["id_paciente"] != null) dgvExitosos.Columns["id_paciente"].Visible = false;
                    if (dgvExitosos.Columns["id_proyecto"] != null) dgvExitosos.Columns["id_proyecto"].Visible = false;
                    if (dgvExitosos.Columns["observacion"] != null) dgvExitosos.Columns["observacion"].Visible = false;
                    if (dgvExitosos.Columns["edad"] != null) dgvExitosos.Columns["edad"].HeaderText = "Edad (Años)";

                    if (dgvExitosos.Columns["codigo_beneficiario"] != null) dgvExitosos.Columns["codigo_beneficiario"].HeaderText = "Código Benef.";
                    if (dgvExitosos.Columns["nombres"] != null) dgvExitosos.Columns["nombres"].HeaderText = "Nombres";
                    if (dgvExitosos.Columns["apellidos"] != null) {
                        dgvExitosos.Columns["apellidos"].HeaderText = "Apellidos";
                        dgvExitosos.Columns["apellidos"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Ocupar espacio extra
                    }
                    if (dgvExitosos.Columns["fecha_nacimiento"] != null) {
                         dgvExitosos.Columns["fecha_nacimiento"].HeaderText = "Fecha Nac.";
                         dgvExitosos.Columns["fecha_nacimiento"].DefaultCellStyle.Format = "dd/MM/yyyy"; // Formatear fecha
                    }
                    if (dgvExitosos.Columns["genero"] != null) dgvExitosos.Columns["genero"].HeaderText = "Género";
                } catch (Exception exGrid) {
                    Console.WriteLine($"Advertencia: No se pudieron configurar todas las columnas de dgvExitosos: {exGrid.Message}");
                }
                */

            }
            else
            {
                Console.WriteLine("No hay pacientes exitosos para mostrar en dgvExitosos.");
                // Opcional: Mostrar un mensaje en el grid si está vacío
                // Por ejemplo, creando una tabla dummy con un mensaje
            }

            // 3. Opcional: Mostrar Errores
            // Si decides mostrar errores aquí también (en otro DataGridView 'dgvErrores'):
            // if (resultadosImportacion.Errores.Any()) {
            //    dgvErrores.AutoGenerateColumns = false; // Asumiendo columnas definidas
            //    dgvErrores.DataSource = resultadosImportacion.Errores;
            // }
        }

        // --- Evento Click del Botón Cerrar ---
        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra este formulario
        }

        // --- Handler Opcional para Clics en la Tabla ---
        private void dgvExitosos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Podrías añadir lógica aquí si necesitaras hacer algo al hacer clic
            // en una celda específica (ej: un botón dentro de la celda)
        }

    } // Fin clase wResultadosImportacion
} // Fin namespace