using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Forms;
using ZasTrack.Forms.Examenes;

namespace ZasTrack
{
    public partial class wMain : Form
    {
        private Button currentBtn;
        public wMain()
        {
            InitializeComponent();
            LoadChildForm(new Forms.Dashboard.wDashboard(this), this.pnlContenedor); // Pass 'this' as the required 'mainForm' parameter
        }
        public void Abrir_Form(object formhijo)
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

        #region Eventos de los botones

        private void btnEstudiantes_Click(object sender, EventArgs e)
        {
            Abrir_Form(new ZasTrack.wPaciente());
        }


        private void btnProyecto_Click(object sender, EventArgs e)
        {
            Abrir_Form(new ZasTrack.Forms.wProyectos.wProyectos());
        }
        private void btnAgregarProyecto_Click(object sender, EventArgs e)
        {
            Abrir_Form(new Forms.wAñadirProyecto());
        }

        private void btnMuestras_Click(object sender, EventArgs e)
        {
            Abrir_Form(new Forms.Muestras.wMuestras());

        }

        private void btnDashBoard_Click(object sender, EventArgs e)
        {
            Abrir_Form(new Forms.Dashboard.wDashboard(this));
        }
        private void btnExamenes_Click(object sender, EventArgs e)
        {
            Abrir_Form(new wExamenes());
        }
        private void btnReportes_Click(object sender, EventArgs e)
        {
            Abrir_Form(new Forms.Informes.wInformes());
        }
        #endregion
        private void LoadChildForm(Form childForm, Panel targetPanel)
        {
            
            if (targetPanel.Controls.Count > 0)
            {
                if (targetPanel.Controls[0] is Form currentForm && currentForm.GetType() == childForm.GetType())
                {
                    childForm.Dispose(); // Evita abrir el mismo tipo de nuevo
                    return;
                }
                targetPanel.Controls.RemoveAt(0);
            }

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            targetPanel.Controls.Add(childForm);
            targetPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        /// ni idea si se usan
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
        #region metodos sin uso
        private void pnlContenedor_Paint(object sender, PaintEventArgs e)
        {
        }

       
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        #endregion

       
    }

}
