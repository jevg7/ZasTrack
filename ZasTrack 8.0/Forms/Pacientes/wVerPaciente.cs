using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Models;
using ZasTrack.Repositories;
namespace ZasTrack.Forms.Estudiantes
{
    public partial class wVerPaciente : Form
    {
        private PacienteRepository pacienteRepository;
        public wVerPaciente()
        {
            InitializeComponent();
            pacienteRepository = new PacienteRepository();
            ConfigurarDataGridView();
        }
        #region Windows Form Designer generated code
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarPacientes();

        }

        #endregion

        #region Metodos

        private void CargarPacientes()
        {
            string criterio = txtBuscar.Text.Trim(); // Obtiene el texto de búsqueda

            if (string.IsNullOrEmpty(criterio))
            {
                MessageBox.Show("Por favor, ingrese un criterio de búsqueda.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<pacientes> resultados = BuscarPacientes(criterio); // Busca pacientes

            if (resultados.Count == 0)
            {
                MessageBox.Show("No se encontraron pacientes con el criterio proporcionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvPacientes.Rows.Clear(); // Limpia el DataGridView si no hay resultados
                return;
            }

            MostrarResultadosEnDGV(resultados); // Muestra los resultados en el DataGridView
        }

        private void MostrarResultadosEnDGV(List<pacientes> pacientes)
        {
            dgvPacientes.Rows.Clear(); // Limpia el DataGridView

            foreach (var paciente in pacientes)
            {
                dgvPacientes.Rows.Add(
                    paciente.id_paciente,
                    paciente.nombres,
                    paciente.apellidos,
                    paciente.edad,
                    paciente.genero,
                    paciente.codigo_beneficiario,
                    paciente.fecha_nacimiento.ToShortDateString(),
                    paciente.id_proyecto,
                    paciente.observacion
                );
            }
        }

        private List<pacientes> BuscarPacientes(string criterio)
        {
            List<pacientes> resultados = new List<pacientes>();

            // Intenta buscar por ID de paciente (si el criterio es un número)
            if (int.TryParse(criterio, out int idPaciente))
            {
                var paciente = pacienteRepository.BuscarPacientePorId(idPaciente);
                if (paciente != null)
                {
                    resultados.Add(paciente);
                    Console.WriteLine($"Paciente encontrado por ID: {paciente.nombres} {paciente.apellidos}");
                }
                else
                {
                    Console.WriteLine("No se encontró ningún paciente con el ID proporcionado.");
                }
            }

            // Busca por código de beneficiario
            var pacientePorCodigo = pacienteRepository.BuscarPacientePorCodigo(criterio);
            if (pacientePorCodigo != null)
            {
                resultados.Add(pacientePorCodigo);
                Console.WriteLine($"Paciente encontrado por código: {pacientePorCodigo.nombres} {pacientePorCodigo.apellidos}");
            }
            else
            {
                Console.WriteLine("No se encontró ningún paciente con el código proporcionado.");
            }

            // Busca por nombre o apellido
            var pacientesPorNombre = pacienteRepository.BuscarPacientesPorNombre(criterio);
            if (pacientesPorNombre.Count > 0)
            {
                resultados.AddRange(pacientesPorNombre);
                Console.WriteLine($"Pacientes encontrados por nombre/apellido: {pacientesPorNombre.Count}");
            }
            else
            {
                Console.WriteLine("No se encontraron pacientes con el nombre/apellido proporcionado.");
            }

            return resultados;
        }
        private void ConfigurarDataGridView()
        {
            dgvPacientes.Columns.Clear(); // Limpia las columnas existentes

            // Agrega columnas manualmente
            dgvPacientes.Columns.Add("id_paciente", "ID Paciente");
            dgvPacientes.Columns.Add("nombres", "Nombres");
            dgvPacientes.Columns.Add("apellidos", "Apellidos");
            dgvPacientes.Columns.Add("edad", "Edad");
            dgvPacientes.Columns.Add("genero", "Género");
            dgvPacientes.Columns.Add("codigo_beneficiario", "Código Beneficiario");
            dgvPacientes.Columns.Add("fecha_nacimiento", "Fecha Nacimiento");
            dgvPacientes.Columns.Add("id_proyecto", "ID Proyecto");
            dgvPacientes.Columns.Add("observacion", "Observación");
        }

        #endregion

        private void dgvPacientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
    

