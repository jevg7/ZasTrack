using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZasTrack.Forms.Examenes
{
    public partial class wBHC : Form
    {
        public wBHC()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(txtGlobulosRojos.Text)) || (string.IsNullOrEmpty(txtHematocrito.Text)) || (string.IsNullOrEmpty(txtHemoglobina.Text))
                || (string.IsNullOrEmpty(txtLeucocitos.Text)) || (string.IsNullOrEmpty(txtMCV.Text)) || (string.IsNullOrEmpty(txtMCH.Text))
                || (string.IsNullOrEmpty(txtMCHC.Text)) || (string.IsNullOrEmpty(txtNeutrofilos.Text)) || (string.IsNullOrEmpty(txtLinfocitos.Text))
                || (string.IsNullOrEmpty(txtMonocitos.Text)) || (string.IsNullOrEmpty(txtEosinofilos.Text) || (string.IsNullOrEmpty(txtBasofilos.Text))))
            {
                MessageBox.Show("Por favor, llene todos los campos.");
                return;
            }

            else
            {
                MessageBox.Show("Examen guardado correctamente.");
            }




        }
    }
}
