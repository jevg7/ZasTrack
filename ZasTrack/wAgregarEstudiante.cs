using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        
    }
}
