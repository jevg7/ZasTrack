using System;
using System.Drawing; // Para Color
using System.Globalization;
using System.Windows.Forms;
using ZasTrack.Models;

namespace ZasTrack.Forms.Examenes.ExamWrite
{
    public partial class EGHControl : UserControl
    {
        // Define los valores por defecto como constantes para reutilizarlos
        private const string DefaultColor = "Cafe";
        private const string DefaultConsistencia = "Solida";
        private const string DefaultParasito = "No se observo";
        private Color DefaultForeColor = SystemColors.GrayText; // Color para texto por defecto
        private Color NormalForeColor = SystemColors.WindowText; // Color normal (negro)

        public EGHControl()
        {
            InitializeComponent();

            // --- Asignar Valores por Defecto y Estado Inicial ---
            InitializeDefaultValue(txtColor, DefaultColor);
            InitializeDefaultValue(txtConsistencia, DefaultConsistencia); // Usa nombre corregido
            InitializeDefaultValue(txtParasito, DefaultParasito);       // Usa nombre corregido

            // --- Asignar Event Handlers para efecto Gris/Negro ---
            txtColor.Enter += TextBox_Enter;
            txtColor.Leave += TextBox_Leave;
            txtConsistencia.Enter += TextBox_Enter; // Usa nombre corregido
            txtConsistencia.Leave += TextBox_Leave; // Usa nombre corregido
            txtParasito.Enter += TextBox_Enter;     // Usa nombre corregido
            txtParasito.Leave += TextBox_Leave;     // Usa nombre corregido
        }
        private void EGHControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Inicializa un TextBox con su valor y apariencia por defecto.
        /// </summary>
        private void InitializeDefaultValue(TextBox txt, string defaultValue)
        {
            txt.Text = defaultValue;
            txt.ForeColor = DefaultForeColor;
            txt.Tag = true; // Usamos Tag para marcar si está mostrando el default (true) o no (false)
        }

        /// <summary>
        /// Se ejecuta cuando el usuario ENTRA a un TextBox.
        /// Si está mostrando el valor por defecto (gris), lo limpia y pone el color normal.
        /// </summary>
        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null && txt.Tag is bool && (bool)txt.Tag == true)
            {
                txt.Text = "";
                txt.ForeColor = NormalForeColor;
                txt.Tag = false; // Marca que ya no tiene el valor por defecto
            }
        }

        /// <summary>
        /// Se ejecuta cuando el usuario SALE de un TextBox.
        /// Si lo dejó vacío, restaura el valor por defecto en gris.
        /// </summary>
        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null && string.IsNullOrWhiteSpace(txt.Text))
            {
                // Restaura el default correspondiente
                string defaultValue = "";
                if (txt == txtColor) defaultValue = DefaultColor;
                else if (txt == txtConsistencia) defaultValue = DefaultConsistencia; // Nombre corregido
                else if (txt == txtParasito) defaultValue = DefaultParasito;     // Nombre corregido

                InitializeDefaultValue(txt, defaultValue);
            }
            // Si tiene texto real, Tag ya es 'false' y ForeColor es 'NormalForeColor'
            // y se quedan así.
        }


        /// <summary>
        /// Rellena los controles con datos existentes (sobrescribe defaults).
        /// </summary>
        public void CargarDatos(examen_heces datos)
        {
            // Si se pasan datos existentes, sobrescriben los defaults y se pone color normal
            if (datos != null)
            {
                txtColor.Text = datos.color ?? DefaultColor; // Si es null en BD, usa default
                txtConsistencia.Text = datos.consistencia ?? DefaultConsistencia; // Nombre corregido
                txtParasito.Text = datos.parasitos ?? DefaultParasito; // Nombre corregido

                // Asegura que el color sea normal y el Tag sea false si cargamos datos
                SetTextBoxState(txtColor, DefaultColor);
                SetTextBoxState(txtConsistencia, DefaultConsistencia); // Nombre corregido
                SetTextBoxState(txtParasito, DefaultParasito);     // Nombre corregido
            }
            // Si datos es null, el constructor ya puso los valores/colores por defecto
        }

        /// <summary>
        /// Establece el estado visual (color/tag) de un TextBox después de CargarDatos.
        /// </summary>
        private void SetTextBoxState(TextBox txt, string defaultValue)
        {
            if (txt.Text == defaultValue) // Si terminó con el valor por defecto
            {
                txt.ForeColor = DefaultForeColor;
                txt.Tag = true;
            }
            else // Si tiene un valor diferente al default
            {
                txt.ForeColor = NormalForeColor;
                txt.Tag = false;
            }
        }


        /// <summary>
        /// Lee, valida y devuelve un objeto examen_heces con los datos ingresados.
        /// ¡IMPORTANTE! Si el campo tiene el valor por defecto (gris), lo trata como si estuviera vacío
        /// para la validación de obligatorios, pero SÍ lo incluye en el objeto devuelto si pasa la validación.
        /// </summary>
        public examen_heces ObtenerDatos()
        {
            if (!ValidarEntradasEGH())
            {
                return null;
            }

            var datos = new examen_heces();
            try
            {
                // Si el Tag es true, significa que aún tiene el valor por defecto (gris).
                // Guardamos ese valor por defecto. Si Tag es false, guardamos lo que escribió el usuario.
                datos.color = (txtColor.Tag is bool && (bool)txtColor.Tag == true) ? DefaultColor : txtColor.Text;
                datos.consistencia = (txtConsistencia.Tag is bool && (bool)txtConsistencia.Tag == true) ? DefaultConsistencia : txtConsistencia.Text; // Nombre corregido
                datos.parasitos = (txtParasito.Tag is bool && (bool)txtParasito.Tag == true) ? DefaultParasito : txtParasito.Text;             // Nombre corregido

                return datos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar los datos de Heces: {ex.Message}", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Valida las entradas de Heces. Considera los campos con valor por defecto (gris) como NO llenados
        /// si son requeridos.
        /// </summary>
        private bool ValidarEntradasEGH()
        {            
            return true;
        }
    } // Fin clase
} // Fin namespace