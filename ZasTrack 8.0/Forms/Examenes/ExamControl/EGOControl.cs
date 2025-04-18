using System;
using System.Drawing;        // Necesario para Font, Point, Color etc.
using System.Globalization;  // Necesario para CultureInfo
using System.Windows.Forms;
using ZasTrack.Models;      // Asegúrate que examen_orina esté en este namespace

namespace ZasTrack.Forms.Examenes.ExamWrite // Asegúrate que este sea el namespace correcto
{
    // Asegúrate que herede de UserControl y sea 'partial'
    public partial class EGOControl : UserControl
    {
        // Constructor (normalmente solo llama a InitializeComponent)
        public EGOControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Rellena los controles del UserControl con datos de un objeto examen_orina.
        /// Se usa para mostrar resultados existentes (Modo Ver/Editar).
        /// </summary>
        /// <param name="datos">El objeto examen_orina con los datos a mostrar, o null.</param>
        public void CargarDatos(examen_orina datos)
        {
            // Si no se pasaron datos (ej. es un examen nuevo), limpia los campos
            if (datos == null)
            {
                txtColor.Text = "";
                txtAspecto.Text = "";
                txtPh.Text = "";
                txtDensidad.Text = "";
                txtLeucocitos.Text = "";
                txtNitritos.Text = "";
                txtProteina.Text = "";
                txtGlucosa.Text = "";
                txtCetonas.Text = "";
                txtUrobilinogeno.Text = "";
                txtBilirrubinas.Text = "";
                txtHemoglobina.Text = "";
                txtCelulasEpiteliales.Text = "";
                txtLeucocitosMicro.Text = "";
                txtEritrocitos.Text = ""; // Asegúrate que este TextBox exista
                txtBacterias.Text = "";
                txtCristales.Text = "";
                txtCilindros.Text = "";
                // txtObservacionesOrina.Text = ""; // Si añades TextBox para observaciones específicas de orina
                return;
            }

            // Asigna valores desde el objeto 'datos' a los TextBoxes
            // Usamos ?? "" para manejar posibles nulos en strings que vienen de la BD/Modelo
            txtColor.Text = datos.color ?? "";
            txtAspecto.Text = datos.aspecto ?? "";
            // Usa CultureInfo.InvariantCulture para asegurar que el punto decimal funcione bien
            txtPh.Text = datos.ph.ToString(CultureInfo.InvariantCulture);
            txtDensidad.Text = datos.densidad.ToString(CultureInfo.InvariantCulture);
            txtLeucocitos.Text = datos.leucocitos ?? "";
            txtNitritos.Text = datos.nitritos ?? "";
            txtProteina.Text = datos.proteina ?? "";
            txtGlucosa.Text = datos.glucosa ?? "";
            txtCetonas.Text = datos.cetonas ?? "";
            txtUrobilinogeno.Text = datos.urobilinogeno ?? "";
            txtBilirrubinas.Text = datos.bilirrubinas ?? "";
            txtHemoglobina.Text = datos.hemoglobina ?? "";

            // Examen Microscópico
            txtCelulasEpiteliales.Text = datos.celulas_epiteliales ?? "";
            txtLeucocitosMicro.Text = datos.leucocitos_micro ?? "";
            txtEritrocitos.Text = datos.eritrocitos ?? ""; // Asumiendo que añadiste 'eritrocitos' al modelo
            txtBacterias.Text = datos.bacterias ?? "";
            txtCristales.Text = datos.cristales ?? "";
            txtCilindros.Text = datos.cilindros ?? "";
            // txtObservacionesOrina.Text = datos.observaciones ?? ""; // Si añades TextBox para observaciones
        }

        /// <summary>
        /// Lee los datos de los TextBoxes, los valida, y devuelve un objeto examen_orina.
        /// Devuelve null si la validación falla. Se usa al presionar "Guardar Resultados".
        /// </summary>
        /// <returns>Un objeto examen_orina con los datos o null si falla la validación.</returns>
        public examen_orina ObtenerDatos()
        {
            // 1. Validar las entradas primero
            if (!ValidarEntradasEGO())
            {
                return null; // Validación fallida, no se puede continuar
            }

            // 2. Si la validación es exitosa, crea el objeto y lee los datos
            var datos = new examen_orina();
            try
            {
                datos.color = txtColor.Text;
                datos.aspecto = txtAspecto.Text;
                datos.ph = decimal.TryParse(txtPh.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal phVal) ? phVal : 0m; // Asigna 0 si falla
                datos.densidad = decimal.TryParse(txtDensidad.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal densVal) ? densVal : 0m; // Asigna 0 si falla
                datos.leucocitos = txtLeucocitos.Text;
                datos.nitritos = txtNitritos.Text;
                datos.proteina = txtProteina.Text;
                datos.glucosa = txtGlucosa.Text;
                datos.cetonas = txtCetonas.Text;
                datos.urobilinogeno = txtUrobilinogeno.Text;
                datos.bilirrubinas = txtBilirrubinas.Text;
                datos.hemoglobina = txtHemoglobina.Text;
                datos.celulas_epiteliales = txtCelulasEpiteliales.Text;
                datos.leucocitos_micro = txtLeucocitosMicro.Text;
                datos.eritrocitos = txtEritrocitos.Text; // Asume que existe txtEritrocitos
                datos.bacterias = txtBacterias.Text;
                datos.cristales = txtCristales.Text;
                datos.cilindros = txtCilindros.Text;
                // datos.observaciones = txtObservacionesOrina.Text; // Si tuvieras este TextBox
                // datos.procesado se asigna en el repositorio
                return datos;
            }
            catch (Exception ex)
            {
                // Captura cualquier error inesperado durante la lectura/conversión
                MessageBox.Show($"Error al procesar los datos ingresados para Orina: {ex.Message}", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Devuelve null indicando que hubo un error
            }
        }

        /// <summary>
        /// Valida los datos ingresados en los controles del examen de orina.
        /// Muestra mensajes al usuario si encuentra errores y devuelve false.
        /// </summary>
        /// <returns>True si todos los datos son válidos, False en caso contrario.</returns>
        private bool ValidarEntradasEGO()
        {
              if (!string.IsNullOrWhiteSpace(txtPh.Text) && !decimal.TryParse(txtPh.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _))
            {
                MessageBox.Show("El valor de pH ingresado no es un número válido...", "Validación Orina", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPh.Focus(); txtPh.SelectAll(); return false;
            }
            // Validación para Densidad (numérico si se ingresa)
            if (!string.IsNullOrWhiteSpace(txtDensidad.Text) && !decimal.TryParse(txtDensidad.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _))
            {
                MessageBox.Show("El valor de Densidad ingresado no es un número válido...", "Validación Orina", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDensidad.Focus(); txtDensidad.SelectAll(); return false;
            }
        
            return true;
        }

        // Aquí puedes añadir más lógica o eventos privados si son necesarios
        // específicamente para este UserControl.
        private void EGOControl_Load(object sender, EventArgs e)
        {

        }
    } // Fin clase EGOControl
} // Fin namespace