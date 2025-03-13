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

namespace ZasTrack
{
    public partial class wMain : Form
    {
        private Button currentBtn;
        public wMain()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        #region ConfigBotones

        private struct Colores
        {
            public static Color color1 = Color.FromArgb(255, 255, 255);
        }
        private void actButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                dsbButton(); // Desactiva el botón actualmente activo.
                             //Btn
                currentBtn = (Button)senderBtn; // Establece el botón actualmente clicado como el botón activo.
                currentBtn.BackColor = Color.FromArgb(40, 40, 40); // Establece el color de fondo del botón activo.
                currentBtn.ForeColor = color; // Establece el color del texto del botón activo.
                currentBtn.TextAlign = ContentAlignment.MiddleCenter; // Establece la alineación del texto en el botón activo.
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage; // Establece la relación entre el texto y la imagen en el botón activo.
                currentBtn.ImageAlign = ContentAlignment.MiddleRight; // Establece la alineación de la imagen en el botón activo.
            }
        }
        private void dsbButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(27, 27, 27); // Establece el color de fondo del botón inactivo.
                currentBtn.ForeColor = Color.White; // Establece el color del texto del botón inactivo.
                currentBtn.TextAlign = ContentAlignment.MiddleLeft; // Establece la alineación del texto en el botón inactivo.
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText; // Establece la relación entre el texto y la imagen en el botón inactivo.
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft; // Establece la alineación de la imagen en el botón inactivo.
            }

        }
        #endregion


        private void btnEstudiantes_Click(object sender, EventArgs e)
        {
            actButton(sender, Colores.color1);
            Abrir_Form(new wEstudiantes());
        }

        private void btnExamenes_Click(object sender, EventArgs e)
        {
            actButton(sender, Colores.color1);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            actButton(sender, Colores.color1);
        }

        private void btnProyecto_Click(object sender, EventArgs e)
        {
            actButton(sender, Colores.color1);
        }



        private void Abrir_Form(object formhijo)
        {

            if (this.pnlContenedor.Controls.Count > 0)
                this.pnlContenedor.Controls.RemoveAt(0); // Elimina cualquier control existente del panel contenedor.

            Form fh = formhijo as Form; // Convierte el objeto de entrada en un formulario.
            fh.TopLevel = false; // Establece la propiedad TopLevel del formulario como false.
            fh.Dock = DockStyle.Fill; // Establece la propiedad Dock del formulario para que ocupe todo el espacio del panel contenedor.
            this.pnlContenedor.Controls.Add(fh); // Agrega el formulario al panel contenedor.
            this.pnlContenedor.Tag = fh; // Establece la propiedad Tag del panel contenedor como el formulario.
            fh.Show(); // Muestra el formulario.
        }

       

    }

}
