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
        private Form? activeForm = null;
        public wMain()
        {
            InitializeComponent();
            // Establecer el color de fondo del panel contenedor
            actButton(null, Color.Turquoise); // Cambia el color del botón activo a Turquoise
            ShowChildForm(new Forms.Dashboard.wDashboard(this));
        }
        // Metodo para mostrar formularios hijos
        public void ShowChildForm(Form childForm)
        {
            // 1. No hacer nada si se intenta abrir el mismo tipo de form que ya está activo
            if (activeForm?.GetType() == childForm.GetType())
            {
                childForm.Dispose(); // Liberar la instancia recién creada que no se usará
                return;
            }

            // 2. Cerrar y liberar el formulario activo anterior (si existe)
            if (activeForm != null)
            {
                activeForm.Close(); // Llama a Close() que a su vez debería llamar a Dispose()
                                    // Alternativamente, puedes llamar a Dispose() directamente si Close() no es suficiente:
                                    // activeForm.Dispose();
            }

            // 3. Configurar y mostrar el nuevo formulario
            try
            {
                activeForm = childForm; // Guardar referencia al nuevo formulario activo
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                this.pnlContenedor.Controls.Add(childForm);
                this.pnlContenedor.Tag = childForm; // Opcional, podrías usar activeForm en su lugar
                childForm.BringToFront();
                childForm.Show();
            }
            catch (Exception ex)
            {
                // Manejo básico de errores al mostrar el form
                MessageBox.Show($"Error al intentar abrir la sección:\n{ex.Message}",
                                "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR ShowChildForm: {ex}"); // Mantener log para depuración
                if (childForm != null)
                {
                    childForm.Dispose(); // Asegurarse de liberar recursos si falla
                }
                activeForm = null; // Resetear el formulario activo si falló
            }
        }
        #region Eventos de los botones

        // Formulario Dashboard
        private void btnDashBoard_Click(object sender, EventArgs e)
        {
            actButton(sender, Color.Turquoise);
            ShowChildForm(new Forms.Dashboard.wDashboard(this));
        }     

        // Formulario Proyectos
        private void btnProyecto_Click(object sender, EventArgs e)
        {
            actButton(sender, Color.Turquoise);
            ShowChildForm(new ZasTrack.Forms.wProyectos.wProyectos());
        }

        // Formulario Pacientes
        private void btnEstudiantes_Click(object sender, EventArgs e)
        {
            actButton(sender, Color.Turquoise);
            ShowChildForm(new ZasTrack.wPaciente());
        }

        // Formulario Muestras
        private void btnMuestras_Click(object sender, EventArgs e)
        {
            actButton(sender, Color.Turquoise);
            ShowChildForm(new Forms.Muestras.wMuestras());

        }
       
        // Formulario Examenes
        private void btnExamenes_Click(object sender, EventArgs e)
        {
            actButton(sender, Color.Turquoise);
            ShowChildForm(new wExamenes());
        }

        // Formulario Reportes
        private void btnReportes_Click(object sender, EventArgs e)
        {
            actButton(sender, Color.Turquoise);
            ShowChildForm(new Forms.Informes.wInformes());
        }
        #endregion
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
                currentBtn = (Button)senderBtn;
                currentBtn.ForeColor = Color.Gray; // Color activo
                currentBtn.BackColor = Color.LightGray;

                currentBtn.ForeColor = Color.Black;     //  Establece el color del texto del botón activo.
                currentBtn.TextAlign = ContentAlignment.MiddleCenter; // Establece la alineación del texto en el botón activo.
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage; // Establece la relación entre el texto y la imagen en el botón activo.
                currentBtn.ImageAlign = ContentAlignment.MiddleRight; // Establece la alineación de la imagen en el botón activo.
            }
        }
        private void dsbButton()
        {
            if (currentBtn != null)
            {
                 currentBtn.BackColor = Color.White; // Establece el color de fondo del botón inactivo.
                currentBtn.ForeColor = Color.Black; // Establece el color del texto del botón inactivo.
                currentBtn.TextAlign = ContentAlignment.MiddleLeft; // Establece la alineación del texto en el botón inactivo.
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText; // Establece la relación entre el texto y la imagen en el botón inactivo.
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft; // Establece la alineación de la imagen en el botón inactivo.
            }

        }
        #endregion
        #region Windows Form Designer generated code
        private void pnlContenedor_Paint(object sender, PaintEventArgs e)
        {
        }

       
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        #endregion

       
    }

}
