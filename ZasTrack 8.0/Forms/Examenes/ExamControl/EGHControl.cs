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
        private string DefaultColor = "Café";
        private string DefaultConsistencia = "Sólida";
        private string DefaultParasito = "No se observan";
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
                                                                          // Replace this line:
                                                                          // InitializeDefaultValue((TextBox)txtParasito, DefaultParasito); // Cast ComboBox to TextBox

            // With this:
            if (txtParasito is ComboBox comboBoxParasito)
            {
                comboBoxParasito.Text = DefaultParasito;
                comboBoxParasito.ForeColor = DefaultForeColor;
                comboBoxParasito.Tag = true;
            }
            // Asignar Event Handlers para efecto Gris/Negro
            txtColor.Enter += TextBox_Enter;
            txtColor.Leave += TextBox_Leave;
            txtConsistencia.Enter += TextBox_Enter; // Nombre Corregido
            txtConsistencia.Leave += TextBox_Leave; // Nombre Corregido
                                                    // AÑADE ESTAS LÍNEAS
            txtParasito.Enter += ComboBox_Enter;
            txtParasito.Leave += ComboBox_Leave;
            txtParasito.TextChanged += ComboBox_TextChanged; // O también puedes usar .SelectionChangeCommitted

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
            {
                string defaultValue = "";
                if (txt == txtColor) defaultValue = DefaultColor;
                else if (txt == txtConsistencia) defaultValue = DefaultConsistencia;
                else if (txt.Name == txtParasito.Name) defaultValue = DefaultParasito; // Fixed comparison
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
                    // Replace this line:
                    // InitializeDefaultValue((TextBox)txtParasito, DefaultParasito); // Cast ComboBox to TextBox

                    // With this:
                    if (txtParasito is ComboBox comboBoxParasito)
                    {
                        comboBoxParasito.Text = DefaultParasito;
                        comboBoxParasito.ForeColor = DefaultForeColor;
                        comboBoxParasito.Tag = true;
                    }
                }
                else
                {
                    // Si los datos de la BD son null, mostrar valores por defecto
                    if (string.IsNullOrWhiteSpace(datos.color))
                    {
                        InitializeDefaultValue(txtColor, DefaultColor);
                    }
                    else
                    {
                        txtColor.Text = datos.color;
                        SetTextBoxState(txtColor, DefaultColor);
                    }

                    if (string.IsNullOrWhiteSpace(datos.consistencia))
                    {
                        InitializeDefaultValue(txtConsistencia, DefaultConsistencia);
                    }
                    else
                    {
                        txtConsistencia.Text = datos.consistencia;
                        SetTextBoxState(txtConsistencia, DefaultConsistencia);
                    }

                    if (txtParasito is ComboBox comboBoxParasito)
                    {
                        if (string.IsNullOrWhiteSpace(datos.parasitos))
                        {
                            comboBoxParasito.Text = DefaultParasito;
                            comboBoxParasito.ForeColor = DefaultForeColor;
                            comboBoxParasito.Tag = true;
                        }
                        else
                        {
                            comboBoxParasito.Text = datos.parasitos;
                            comboBoxParasito.ForeColor = NormalForeColor;
                            comboBoxParasito.Tag = false;
                        }
                    }
                }
            }
            catch { /* Manejo de error si es necesario */ }
            // Importante: Resetea _isDirty DESPUÉS de asignar textos programáticamente
            _isDirty = false;
        }


        // --- ObtenerDatos y ValidarEntradasEGH (sin cambios lógicos aquí) ---
        public examen_heces ObtenerDatos()
        {
            if (!ValidarEntradasEGH()) { return null; }

            var datos = new examen_heces();
            try
            {
                // Si el campo está en su estado por defecto (Tag=true), guardar null, sino, guardar el texto.
                datos.color = (txtColor.Tag is bool && (bool)txtColor.Tag == true)
                    ? null
                    : txtColor.Text;

                datos.consistencia = (txtConsistencia.Tag is bool && (bool)txtConsistencia.Tag == true)
                    ? null
                    : txtConsistencia.Text;

                if (txtParasito is ComboBox comboBoxParasito)
                {
                    // <<-- LÍNEA CORREGIDA: Se usa .Text en lugar de .SelectedItem
                    datos.parasitos = (comboBoxParasito.Tag is bool && (bool)comboBoxParasito.Tag == true)
                        ? null
                        : (string.IsNullOrWhiteSpace(comboBoxParasito.Text) ? null : comboBoxParasito.Text);
                }
                else
                {
                    datos.parasitos = null;
                }

                return datos;
            }
            catch (Exception ex)
            {
                // Manejar o registrar el error si es necesario
                return null;
            }
        }
        private bool ValidarEntradasEGH()
        {
            // Mantenemos validación simple por ahora, permitiendo defaults
            // Podrías requerir que si IsDirty = true, entonces los campos obligatorios no pueden volver a quedar vacíos.
            return true;
        }
        // --- Fin ObtenerDatos y Validar ---
        // --- NUEVOS MÉTODOS EXCLUSIVOS PARA EL COMBOBOX ---

        private void ComboBox_Enter(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            // Si el ComboBox está en estado "por defecto" (Tag=true)...
            if (cmb != null && cmb.Tag is bool && (bool)cmb.Tag == true)
            {
                // ...límpialo y prepáralo para recibir un nuevo valor.
                cmb.Text = "";
                cmb.ForeColor = NormalForeColor;
                cmb.Tag = false;
            }
        }

        private void ComboBox_Leave(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            // Si el usuario deja el ComboBox vacío...
            if (cmb != null && string.IsNullOrWhiteSpace(cmb.Text))
            {
                // ...restaura el valor por defecto.
                cmb.Text = DefaultParasito;
                cmb.ForeColor = DefaultForeColor;
                cmb.Tag = true;
            }
        }

        // Este evento se dispara cuando el usuario SELECCIONA un ítem o cambia el texto.
        private void ComboBox_TextChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            // Si el control no está en su estado por defecto, marca que hay cambios.
            if (cmb != null && !(cmb.Tag is bool && (bool)cmb.Tag == true))
            {
                _isDirty = true;
            }
        }

    } // Fin clase
} // Fin namespace