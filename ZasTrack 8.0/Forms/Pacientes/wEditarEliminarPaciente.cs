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
using ZasTrack.Models;
using ZasTrack.Repositories;

namespace ZasTrack.Forms.Estudiantes
{
    public partial class wEditarEliminarPaciente : Form
    {
        private PacienteRepository pacienteRepository;
        public wEditarEliminarPaciente()
        {
            InitializeComponent();
            pacienteRepository = new PacienteRepository();



        }



        #region Metodos

        private void editarPaciente()
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

            pacientes pacienteEditado = new pacientes
            {
                nombres = CapitalizarTexto(txtNombres.Text),
                apellidos = CapitalizarTexto(txtApellidos.Text),
                edad = int.Parse(txtEdad.Text),
                genero = cmbGenero.SelectedItem.ToString(),
                codigo_beneficiario = txtCodigoBen.Text,
                fecha_nacimiento = dtpFechaNac.Value,
                id_proyecto = (int)cmbProyecto.SelectedValue,
                observacion = txtObservacion.Text,
            };

            try
            {
                // Verifica si el paciente existe
                pacientes pacienteExistente = pacienteRepository.BuscarPacientePorCodigo(txtCodigoBen.Text);

                if (pacienteExistente != null)
                {
                    // Si existe, actualiza la información del paciente
                    pacienteEditado.id_paciente = pacienteExistente.id_paciente;
                    pacienteRepository.EditarPaciente(pacienteEditado);
                    MessageBox.Show("Paciente actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontró el paciente para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el paciente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProyectos()
        {
            ProyectoRepository proyectoRepository = new ProyectoRepository();
            List<Proyecto> proyectos = proyectoRepository.ObtenerProyectos();
            cmbProyecto.DataSource = proyectos;
            cmbProyecto.DisplayMember = "nombre";
            cmbProyecto.ValueMember = "id_proyecto";
            cmbProyecto.SelectedIndex = -1;
        }




        private string CapitalizarTexto(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return texto;

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(texto.ToLower());
        }
        #endregion
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnEliminar.Visible = true;
            btnCancelar.Visible = false;
            btnGuardar.Visible = false;
            txtCodigoBen.Enabled = false;
            txtNombres.Enabled = false;
            txtApellidos.Enabled = false;
            txtObservacion.Enabled = false;
            dtpFechaNac.Enabled = false;
            txtGenero.Show();
            cmbGenero.Hide();


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEliminar.Visible = false;
            btnCancelar.Visible = true;
            btnGuardar.Visible = true;
            txtCodigoBen.Enabled = true;
            txtNombres.Enabled = true;
            txtApellidos.Enabled = true;
            txtObservacion.Enabled = true;
            dtpFechaNac.Enabled = true;
            txtGenero.Hide();
            cmbGenero.Show();

        }

        private void cmbGenero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void wEditarEliminarEstudiante_Load(object sender, EventArgs e)
        {
            cmbGenero.Items.Add("Masculino");
            cmbGenero.Items.Add("Femenino");
            cmbGenero.SelectedIndex = 0;
            CargarProyectos();
        }

        private void dtpFechaNac_ValueChanged(object sender, EventArgs e)
        {
            string fecha = "00/00/0000";
            fecha = string.Format("{0: yyyy-MM-dd}", dtpFechaNac.Value);
            DateTime fechaNacimiento = DateTime.Parse(fecha);

            int edad = DateTime.Today.Year - fechaNacimiento.Year;

            if (DateTime.Today < fechaNacimiento.AddYears(edad))
                --edad;

            txtEdad.Text = edad.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            editarPaciente();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string codigo = txtBusqueda.Text; // oe jairo sos baboso, habias puesto el txt de abajo no el de arriba, con razon no servia

            pacientes pacienteEncontrado = pacienteRepository.BuscarPacientePorCodigo(codigo);

            if (pacienteEncontrado != null)
            {
                txtNombres.Text = pacienteEncontrado.nombres;
                txtApellidos.Text = pacienteEncontrado.apellidos;
                txtEdad.Text = pacienteEncontrado.edad.ToString();
                cmbGenero.SelectedItem = pacienteEncontrado.genero;
                dtpFechaNac.Value = pacienteEncontrado.fecha_nacimiento;
                txtObservacion.Text = pacienteEncontrado.observacion;

                MessageBox.Show("Paciente encontrado. Listo para editar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Paciente no encontrado. Ingrese los datos para registrarlo.", "Nuevo Paciente", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
