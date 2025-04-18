using System;
using System.Drawing;
using System.Globalization; // Necesario para NumberStyles y CultureInfo
using System.Linq;          // Necesario para OfType<>
using System.Windows.Forms;
using ZasTrack.Models;      // Asegúrate que examen_sangre esté aquí

namespace ZasTrack.Forms.Examenes.ExamWrite // Revisa el namespace
{
    public partial class BHCControl : UserControl
    {
        // Bandera para evitar cálculos recursivos
        private bool _isCalculating = false;

        public BHCControl()
        {
            InitializeComponent();
            // Usar TextChanged para cálculo en tiempo real
            txtGlobulosRojos.TextChanged += CamposClave_TextChanged;
            txtHematocrito.TextChanged += CamposClave_TextChanged;
            txtHemoglobina.TextChanged += CamposClave_TextChanged;
        }

        /// <summary>
        /// Handler unificado para TextChanged en Hct, Hb, GR. Dispara los cálculos.
        /// </summary>
        private void CamposClave_TextChanged(object sender, EventArgs e)
        {
            CalcularIndices();
        }

        /// <summary>
        /// Calcula Hb, GR, MCV, MCH, MCHC basado en Hct (prioridad), trunca a 2 decimales y actualiza TextBoxes.
        /// </summary>
        private void CalcularIndices()
        {
            if (_isCalculating) return;
            _isCalculating = true;

            CultureInfo culture = CultureInfo.InvariantCulture;
            NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;

            // Variables para valores parseados
            decimal? hto = null, hb_parsed = null, gr_parsed = null; // Hb/GR leídos directamente
                                                                     // Variables para valores calculados (precisión completa)
            decimal? calcHb = null, calcGr = null;
            // ***** Variables para valores INTERMEDIOS TRUNCADOS *****
            decimal? truncHb = null, truncGr = null;
            // Variables para índices calculados (precisión completa)
            decimal? finalMCV = null, finalMCH = null, finalMCHC = null;
            // Variables para índices TRUNCADOS (para mostrar)
            decimal? truncMCV = null, truncMCH = null, truncMCHC = null;
            // Variables para texto final
            string hbText = "", grText = "", mcvText = "", mchText = "", mchcText = "";

            try
            {
                // 1. Parsear Inputs actuales
                if (decimal.TryParse(txtHematocrito.Text, style, culture, out decimal htoVal)) hto = htoVal;
                if (decimal.TryParse(txtHemoglobina.Text, style, culture, out decimal hbPVal)) hb_parsed = hbPVal;
                if (decimal.TryParse(txtGlobulosRojos.Text, style, culture, out decimal grPVal)) gr_parsed = grPVal;

                // 2. Cascada Hct -> Hb -> GR (con precisión completa)
                if (hto.HasValue)
                {
                    calcHb = hto.Value / 3m;
                    calcGr = (calcHb.HasValue && calcHb.Value != 0m) ? (calcHb.Value / 3m) : 0m;
                }
                else
                {
                    // Si Hct es inválido, usamos los valores parseados como base
                    calcHb = hb_parsed;
                    calcGr = gr_parsed;
                }

                // ***** PASO CLAVE: TRUNCAR los Hb y GR calculados/leídos ANTES de usarlos para índices *****
                truncHb = TruncateDecimal(calcHb, 2); // Trunca el Hb que usaremos
                truncGr = TruncateDecimal(calcGr, 2); // Trunca el GR que usaremos
                                                      // ***** FIN PASO CLAVE *****

                // 3. Calcular Índices usando Hct y los Hb/GR YA TRUNCADOS
                if (hto.HasValue && truncGr.HasValue && truncGr.Value != 0m)
                {
                    finalMCV = (hto.Value * 10m) / truncGr.Value; // Usa truncGr!
                }
                if (truncHb.HasValue && truncGr.HasValue && truncGr.Value != 0m)
                {
                    finalMCH = (truncHb.Value * 10m) / truncGr.Value; // Usa truncHb y truncGr!
                }
                if (truncHb.HasValue && hto.HasValue && hto.Value != 0m)
                {
                    finalMCHC = (truncHb.Value * 100m) / hto.Value; // Usa truncHb!
                }

                // 4. Truncar los índices resultantes también
                truncMCV = TruncateDecimal(finalMCV, 2);
                truncMCH = TruncateDecimal(finalMCH, 2);
                truncMCHC = TruncateDecimal(finalMCHC, 2);

                // 5. Preparar Texto de Salida (usando valores TRUNCADOS)
                hbText = truncHb?.ToString("F2", culture) ?? "";
                grText = truncGr?.ToString("F2", culture) ?? "";
                mcvText = truncMCV?.ToString("F2", culture) ?? "";
                mchText = truncMCH?.ToString("F2", culture) ?? "";
                mchcText = truncMCHC?.ToString("F2", culture) ?? "";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR during calculation: {ex.Message}");
                hbText = ""; grText = ""; mcvText = ""; mchText = ""; mchcText = "";
            }
            finally
            {
                // 6. Actualizar TextBoxes
                try
                {
                    // Actualiza SIEMPRE para reflejar el cálculo truncado
                    txtHemoglobina.Text = hbText;
                    txtGlobulosRojos.Text = grText;
                    txtMCV.Text = mcvText;
                    txtMCH.Text = mchText;
                    txtMCHC.Text = mchcText;
                }
                catch (Exception updateEx) { Console.WriteLine($"ERROR updating TextBoxes: {updateEx.Message}"); }
                finally { _isCalculating = false; }
            }
        }



        /// <summary>
        /// Rellena los controles con datos de un objeto examen_sangre o limpia/pone defaults.
        /// </summary>
        public void CargarDatos(examen_sangre datos)
        {
            bool calculationState = _isCalculating; // Guarda estado actual
            _isCalculating = true; // Desactiva cálculos durante la carga
            try
            {
                if (datos == null) // Poner valores por defecto o limpiar
                {
                    txtGlobulosRojos.Text = "0.00"; // Pon tus defaults si son diferentes
                    txtHematocrito.Text = "0.0";
                    txtHemoglobina.Text = "0.0";
                    txtLeucocitos.Text = "0.0";
                    txtMCV.Text = ""; // Calculados se limpian
                    txtMCH.Text = "";
                    txtMCHC.Text = "";
                    txtNeutrofilos.Text = "0.0";
                    txtLinfocitos.Text = "0.0";
                    txtMonocitos.Text = "0.0";
                    txtEosinofilos.Text = "0.0";
                    txtBasofilos.Text = "0.0";
                }
                else // Cargando datos existentes
                {
                    CultureInfo culture = CultureInfo.InvariantCulture;
                    txtGlobulosRojos.Text = datos.globulos_rojos.ToString("F2", culture);
                    txtHematocrito.Text = datos.hematocrito.ToString("F2", culture); // Cambiado a F2
                    txtHemoglobina.Text = datos.hemoglobina.ToString("F2", culture); // Cambiado a F2
                    txtLeucocitos.Text = datos.leucocitos.ToString("F2", culture); // Cambiado a F2
                    txtMCV.Text = datos.mcv.ToString("F2", culture); // Cambiado a F2
                    txtMCH.Text = datos.mch.ToString("F2", culture); // Cambiado a F2
                    txtMCHC.Text = datos.mchc.ToString("F2", culture); // Cambiado a F2
                    txtNeutrofilos.Text = datos.neutrofilos.ToString("F2", culture); // Cambiado a F2
                    txtLinfocitos.Text = datos.linfocitos.ToString("F2", culture); // Cambiado a F2
                    txtMonocitos.Text = datos.monocitos.ToString("F2", culture); // Cambiado a F2
                    txtEosinofilos.Text = datos.eosinofilos.ToString("F2", culture); // Cambiado a F2
                    txtBasofilos.Text = datos.basofilos.ToString("F2", culture); // Cambiado a F2
                }
            }
            finally
            {
                _isCalculating = calculationState; // Restaura estado anterior
                                                   // Si cargamos datos existentes, forzamos un cálculo final por si acaso
                                                   // if (datos != null) CalcularIndices(); // Opcional: recalcular al cargar
            }
        }

        /// <summary>
        /// Obtiene y valida los datos de BHC. Devuelve null si falla.
        /// </summary>
        public examen_sangre ObtenerDatos()
        {
            if (!ValidarEntradasBHC()) { return null; } // Valida primero

            var datos = new examen_sangre();
            try
            {
                CultureInfo culture = CultureInfo.InvariantCulture;
                NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;

                // Usa Parse porque ValidarEntradasBHC ya confirmó que son números
                datos.globulos_rojos = decimal.Parse(txtGlobulosRojos.Text, style, culture);
                datos.hematocrito = decimal.Parse(txtHematocrito.Text, style, culture);
                datos.hemoglobina = decimal.Parse(txtHemoglobina.Text, style, culture);
                datos.leucocitos = decimal.Parse(txtLeucocitos.Text, style, culture);
                datos.mcv = decimal.Parse(txtMCV.Text, style, culture);
                datos.mch = decimal.Parse(txtMCH.Text, style, culture);
                datos.mchc = decimal.Parse(txtMCHC.Text, style, culture);
                datos.neutrofilos = decimal.Parse(txtNeutrofilos.Text, style, culture);
                datos.linfocitos = decimal.Parse(txtLinfocitos.Text, style, culture);
                datos.monocitos = decimal.Parse(txtMonocitos.Text, style, culture);
                datos.eosinofilos = decimal.Parse(txtEosinofilos.Text, style, culture);
                datos.basofilos = decimal.Parse(txtBasofilos.Text, style, culture);
                // datos.observacion = ...
                // datos.procesado = ... (se pone en repo)

                return datos;
            }
            catch (Exception ex) { /* ... Error handling ... */ return null; }
        }

        /// <summary>
        /// Valida las entradas del formulario BHC. Revisa obligatorios, formato numérico.
        /// (Quitamos validación de rango de aquí, se haría al mostrar referencia)
        /// </summary>
        private bool ValidarEntradasBHC()
        {
            TextBox[] controlesNumericos = { /* ... lista igual que antes ... */
                 txtGlobulosRojos, txtHematocrito, txtHemoglobina, txtLeucocitos,
                 txtMCV, txtMCH, txtMCHC, txtNeutrofilos, txtLinfocitos,
                 txtMonocitos, txtEosinofilos, txtBasofilos
             };
            CultureInfo culture = CultureInfo.InvariantCulture;
            NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;

            foreach (TextBox txt in controlesNumericos)
            {
                string nombreCampo = EncontrarLabelPara(txt)?.Text ?? txt.Name.Substring(3);

                // 1. Obligatorio
                if (string.IsNullOrWhiteSpace(txt.Text))
                { MessageBox.Show($"El campo '{nombreCampo}' no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); txt.Focus(); return false; }

                // 2. Numérico
                if (!decimal.TryParse(txt.Text, style, culture, out _)) // Solo valida que sea número
                { MessageBox.Show($"El valor en '{nombreCampo}' no es un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); txt.Focus(); txt.SelectAll(); return false; }

                // 3. Rango (QUITADO DE AQUÍ - se haría al mostrar referencias o como regla de negocio aparte)
            }

            // 4. Suma Diferencial (Mantenemos esta validación)
            decimal sumaDiferencial = 0m;
            TextBox[] controlesDiferencial = { txtNeutrofilos, txtLinfocitos, txtMonocitos, txtEosinofilos, txtBasofilos };
            foreach (TextBox txtDiff in controlesDiferencial) { sumaDiferencial += decimal.Parse(txtDiff.Text, style, culture); } // Parse es seguro aquí
            if (Math.Abs(sumaDiferencial - 100m) > 1m) // Margen de +/- 1%
            { MessageBox.Show("La suma del diferencial no es 100% (±1%).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNeutrofilos.Focus(); return false; }


            return true; // Todo válido
        }


        /// <summary>
        /// Trunca un valor decimal a un número específico de decimales sin redondear.
        /// </summary>
        private decimal? TruncateDecimal(decimal? value, int decimalPlaces)
        {
            if (!value.HasValue) return null;
            if (decimalPlaces < 0) throw new ArgumentOutOfRangeException(nameof(decimalPlaces));
            decimal factor = (decimal)Math.Pow(10, decimalPlaces);
            decimal truncatedValue = Math.Truncate(value.Value * factor) / factor;
            return truncatedValue;
        }

        /// <summary>
        /// Pequeña función auxiliar para encontrar el Label asociado.
        /// </summary>
        private Label EncontrarLabelPara(TextBox txt)
        {
            string nombreLabel = "lbl" + txt.Name.Substring(3);
            return this.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Name.Equals(nombreLabel, StringComparison.OrdinalIgnoreCase));
        }

    } // Fin clase BHCControl
} // Fin namespace