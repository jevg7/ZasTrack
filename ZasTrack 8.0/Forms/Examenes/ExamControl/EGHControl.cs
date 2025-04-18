using System;
using System.Drawing;
using System.Globalization; // Necesario si validas/conviertes números
using System.Windows.Forms;
using ZasTrack.Models; // Asegúrate que examen_heces esté aquí

namespace ZasTrack.Forms.Examenes.ExamWrite // Revisa el namespace
{
    public partial class EGHControl : UserControl
    {
        public EGHControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Rellena los controles con datos de un objeto examen_heces.
        /// </summary>
        public void CargarDatos(examen_heces datos)
        {
            if (datos == null)
            {
                txtColor.Text = "";
                txtConsistencia.Text = ""; // Usa el nombre corregido del TextBox
                txtParasito.Text = "";     // Usa el nombre corregido del TextBox
                return;
            }

            txtColor.Text = datos.color ?? "";
            txtConsistencia.Text = datos.consistencia ?? ""; // Usa el nombre corregido
            txtParasito.Text = datos.parasitos ?? "";         // Usa el nombre corregido
        }

        /// <summary>
        /// Lee, valida y devuelve un objeto examen_heces con los datos ingresados.
        /// Devuelve null si la validación falla.
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
                datos.color = txtColor.Text;
                datos.consistencia = txtConsistencia.Text; // Usa el nombre corregido
                datos.parasitos = txtParasito.Text;         // Usa el nombre corregido

                // id_examen y procesado se manejan al guardar en el repositorio
                return datos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar los datos de Heces: {ex.Message}", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Valida los datos ingresados en los controles de Heces.
        /// </summary>
        private bool ValidarEntradasEGH()
        {
            // Ejemplo: Validar que Color no esté vacío (si es obligatorio)
            // if (string.IsNullOrWhiteSpace(txtColor.Text))
            // {
            //     MessageBox.Show("El campo 'Color' es obligatorio.", "Validación Heces", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     txtColor.Focus();
            //     return false;
            // }

            // Añade más validaciones si son necesarias...

            return true; // Pasa validación
        }

        private void EGHControl_Load(object sender, EventArgs e)
        {
        }
    }
}