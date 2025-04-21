using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ZasTrack.Models; // Asegúrate que el namespace sea correcto

namespace ZasTrack.Forms.Examenes.ExamWrite // Revisa el namespace
{
    public partial class EGHControl : UserControl
    {
        // Valores por defecto
        private const string DefaultColor = "Cafe";
        private const string DefaultConsistencia = "Solida";
        private const string DefaultParasito = "No se observo";
        private Color DefaultForeColor = SystemColors.GrayText;
        private Color NormalForeColor = SystemColors.WindowText;

        // ***** NUEVO: Bandera para saber si el usuario modificó algo *****
        private bool _isDirty = false; // Inicia como 'limpio'
        /// <summary>
        /// Indica si el usuario ha modificado algún campo desde la última carga/guardado.
        /// </summary>
        public bool IsDirty => _isDirty;
        // ***** FIN NUEVO *****

        public EGHControl()
        {
            InitializeComponent();

            // Asignar Valores por Defecto y Estado Inicial
            InitializeDefaultValue(txtColor, DefaultColor);
            InitializeDefaultValue(txtConsistencia, DefaultConsistencia); // Nombre Corregido
            InitializeDefaultValue(txtParasito, DefaultParasito);       // Nombre Corregido

            // Asignar Event Handlers para efecto Gris/Negro
            txtColor.Enter += TextBox_Enter;
            txtColor.Leave += TextBox_Leave;
            txtConsistencia.Enter += TextBox_Enter; // Nombre Corregido
            txtConsistencia.Leave += TextBox_Leave; // Nombre Corregido
            txtParasito.Enter += TextBox_Enter;     // Nombre Corregido
            txtParasito.Leave += TextBox_Leave;     // Nombre Corregido

            // ***** NUEVO: Asignar TextChanged para marcar como modificado *****
            // Usamos el mismo handler para todos los campos de entrada
            txtColor.TextChanged += InputTextBox_TextChanged;
            txtConsistencia.TextChanged += InputTextBox_TextChanged; // Nombre Corregido
            txtParasito.TextChanged += InputTextBox_TextChanged;     // Nombre Corregido
            // ***** FIN NUEVO *****
        }

        // --- Métodos para manejar Defaults y Texto Gris/Negro (sin cambios) ---
        private void InitializeDefaultValue(TextBox txt, string defaultValue)
        {
            txt.Text = defaultValue;
            txt.ForeColor = DefaultForeColor;
            txt.Tag = true;
        }
        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null && txt.Tag is bool && (bool)txt.Tag == true) { /* ... limpia y pone negro ... */ txt.Text = ""; txt.ForeColor = NormalForeColor; txt.Tag = false; }
        }
        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null && string.IsNullOrWhiteSpace(txt.Text))
            { /* ... restaura default ... */
                string defaultValue = "";
                if (txt == txtColor) defaultValue = DefaultColor;
                else if (txt == txtConsistencia) defaultValue = DefaultConsistencia;
                else if (txt == txtParasito) defaultValue = DefaultParasito;
                InitializeDefaultValue(txt, defaultValue);
            }
        }
        private void SetTextBoxState(TextBox txt, string defaultValue)
        { /* ... igual que antes ... */
            if (txt.Text == defaultValue) { txt.ForeColor = DefaultForeColor; txt.Tag = true; }
            else { txt.ForeColor = NormalForeColor; txt.Tag = false; }
        }
        // --- Fin Métodos Defaults ---


        private void InputTextBox_TextChanged(object sender, EventArgs e)
        {
           
            TextBox txt = sender as TextBox;
          
            if (txt != null && !(txt.Tag is bool && (bool)txt.Tag == true))
            {
                _isDirty = true;
             
            }

        }

        private void EGHControl_Load(object sender, EventArgs e)
        {
       
        }
        /// <summary>
        /// Carga datos existentes o establece valores por defecto. Resetea el estado Dirty.
        /// </summary>
        public void CargarDatos(examen_heces datos)
        {
            _isDirty = false; // Asegura empezar como 'limpio' al cargar
            try
            {
                if (datos == null)
                {
                    InitializeDefaultValue(txtColor, DefaultColor);
                    InitializeDefaultValue(txtConsistencia, DefaultConsistencia);
                    InitializeDefaultValue(txtParasito, DefaultParasito);
                }
                else
                {
                    txtColor.Text = datos.color ?? DefaultColor;
                    txtConsistencia.Text = datos.consistencia ?? DefaultConsistencia;
                    txtParasito.Text = datos.parasitos ?? DefaultParasito;
                    SetTextBoxState(txtColor, DefaultColor);
                    SetTextBoxState(txtConsistencia, DefaultConsistencia);
                    SetTextBoxState(txtParasito, DefaultParasito);
                }
            }
            catch { /* Manejo de error si es necesario */ }
            // Importante: Resetea _isDirty DESPUÉS de asignar textos programáticamente
            _isDirty = false;
        }


        // --- ObtenerDatos y ValidarEntradasEGH (sin cambios lógicos aquí) ---
        public examen_heces ObtenerDatos()
        {
            // La validación ahora solo se llama si IsDirty es true (desde el botón Guardar)
            // Pero la dejamos aquí por si se llama directamente.
            if (!ValidarEntradasEGH()) { return null; }
            var datos = new examen_heces();
            try
            {
                datos.color = (txtColor.Tag is bool && (bool)txtColor.Tag == true) ? DefaultColor : txtColor.Text;
                datos.consistencia = (txtConsistencia.Tag is bool && (bool)txtConsistencia.Tag == true) ? DefaultConsistencia : txtConsistencia.Text;
                datos.parasitos = (txtParasito.Tag is bool && (bool)txtParasito.Tag == true) ? DefaultParasito : txtParasito.Text;
                return datos;
            }
            catch (Exception ex) { /* ... */ return null; }
        }

        private bool ValidarEntradasEGH()
        {
            // Mantenemos validación simple por ahora, permitiendo defaults
            // Podrías requerir que si IsDirty = true, entonces los campos obligatorios no pueden volver a quedar vacíos.
            return true;
        }
        // --- Fin ObtenerDatos y Validar ---

    } // Fin clase
} // Fin namespace