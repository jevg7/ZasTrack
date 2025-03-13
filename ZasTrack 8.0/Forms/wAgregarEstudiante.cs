using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Class;

namespace ZasTrack
{
    public partial class wAgregarEstudiante : Form
    {
        public wAgregarEstudiante()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void wAgregarEstudiante_Load(object sender, EventArgs e)
        {
            cmbAgenero.Items.Add("Masculino");
            cmbAgenero.Items.Add("Femenino");
            cmbAgenero.SelectedIndex = 0;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string fecha = "00/00/0000";
            fecha = string.Format("{0: yyyy-MM-dd}", dateTimePicker1.Value);
            DateTime fechaNacimiento = DateTime.Parse(fecha);

            int edad = DateTime.Today.Year - fechaNacimiento.Year;

            if (DateTime.Today < fechaNacimiento.AddYears(edad))
                --edad;

            txtaEdad.Text = edad.ToString();
        }

        CConexion conexion = new CConexion();
        private void btnGuardarPaciente_Click(object sender, EventArgs e)
        {
            if (txtAnombreApellido.Text == "" || txtAcodigo.Text == "")
            {
                MessageBox.Show("Por favor, llene todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dr = MessageBox.Show("¿Está seguro de que desea guardar los datos del estudiante?", "Guardar datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question); ;
                if (dr == DialogResult.Yes)
                {
                    string codigo = txtAcodigo.Text;
                    string nombre = txtAnombreApellido.Text;
                    string genero = cmbAgenero.SelectedText;
                    DateTime fechanac = dateTimePicker1.Value;
                    int edad = 0;

                    try
                    {
                        edad = int.Parse(txtaEdad.Text);
                    }
                    catch (Exception ex)
                    {
                    }

                    string observaciones = txtAobservacion.Text;

                   


                }
            }
        }

        private void txtAcodigo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
