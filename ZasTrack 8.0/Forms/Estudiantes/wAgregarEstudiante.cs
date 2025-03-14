using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Class;
using ZasTrack.Models;
using ZasTrack.Repositories;

namespace ZasTrack
{
    public partial class wAgregarEstudiante : Form
    {
        private PacienteRepository pacienteRepository;

        public wAgregarEstudiante()
        {
            InitializeComponent();
            pacienteRepository = new PacienteRepository();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void wAgregarEstudiante_Load(object sender, EventArgs e)
        {
            cmbGenero.Items.Add("Masculino");
            cmbGenero.Items.Add("Femenino");
            cmbGenero.SelectedIndex = 0;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string fecha = "00/00/0000";
            fecha = string.Format("{0: yyyy-MM-dd}", dtpFechaNac.Value);
            DateTime fechaNacimiento = DateTime.Parse(fecha);

            int edad = DateTime.Today.Year - fechaNacimiento.Year;

            if (DateTime.Today < fechaNacimiento.AddYears(edad))
                --edad;

            txtEdad.Text = edad.ToString();
        }

        private void btnGuardarPaciente_Click(object sender, EventArgs e)
        {
           guardarPaciente();
        }
        

        private void txtAcodigo_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtAnombreApellido_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtAobservacion_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtEdad_TextChanged(object sender, EventArgs e)
        {

        }
        private void cmbGenero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #region Metodos
        private void LimpiarCampos()
        {
            txtNombres.Clear();
            txtApellidos.Clear();
            txtCodigoBen.Clear();
            txtEdad.Clear();
            cmbGenero.SelectedIndex = 0;
            dtpFechaNac.Value = DateTime.Now;
            txtObservacion.Clear();
        }
        private void guardarPaciente()
        {
            if (string.IsNullOrEmpty(txtCodigoBen.Text) ||
                string.IsNullOrEmpty(txtNombres.Text) ||
                string.IsNullOrEmpty(txtEdad.Text) ||
                string.IsNullOrEmpty(txtApellidos.Text) ||
                cmbGenero.SelectedItem == null)
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            pacientes nuevoPaciente = new pacientes
            {
                nombres = CapitalizarTexto(txtNombres.Text),
                apellidos = CapitalizarTexto(txtApellidos.Text),
                edad = int.Parse(txtEdad.Text),
                genero = cmbGenero.SelectedItem.ToString(),
                codigo_beneficiario = txtCodigoBen.Text,
                fecha_nacimiento = dtpFechaNac.Value,
                observacion = txtObservacion.Text
            };

            try
            {
                PacienteRepository pacienteRepository = new PacienteRepository();

                pacienteRepository.GuardarPaciente(nuevoPaciente);
                MessageBox.Show("Paciente guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el paciente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }   
        private string CapitalizarTexto(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return texto;

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(texto.ToLower());
        }
        #endregion
    }
}
