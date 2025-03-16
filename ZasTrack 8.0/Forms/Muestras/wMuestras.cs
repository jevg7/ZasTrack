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
using ZasTrack.Models;
using ZasTrack.Repositories;

namespace ZasTrack.Forms.Muestras
{
    public partial class wMuestras : Form
    {
        private PacienteRepository pacienteRepository;

        public wMuestras()
        {
            InitializeComponent();
            CargarProyectos();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFecha.Enabled = false;
            pacienteRepository = new PacienteRepository();
        }

        private void wMuestras_Load(object sender, EventArgs e)
        {
            mostrarNumMuestra();
        }

        private void pnlProyecto_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarMuestra();


        }

        private void txtFecha_TextChanged(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFecha.Enabled = false;
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
        private bool validarNombre()
        {
            if (string.IsNullOrWhiteSpace(txtIdPaciente.Text))
            {
                MessageBox.Show("El nombre del paciente es obligatorio.");
                return false;
            }
            return true;
        }
        private List<int> obtTipoExaSelect()
        {
            List<int> tiposExamen = new List<int>();

            // Recorrer los ítems seleccionados en el CheckedListBox
            foreach (int index in cklExamenes.CheckedIndices)
            {
                // Mapear el índice al ID correspondiente
                switch (index)
                {
                    case 0: // EGH (Heces)
                        tiposExamen.Add(1);
                        break;
                    case 1: // EGO (Orina)
                        tiposExamen.Add(2);
                        break;
                    case 2: // BCC (Sangre)
                        tiposExamen.Add(3);
                        break;
                }
            }

            return tiposExamen;
        }
        private void GuardarMuestra()
        {
            if (!validarNombre() || !validarExamenes())
                return;

            int idProyecto = (int)cmbProyecto.SelectedValue;
            DateTime fechaMuestra = DateTime.Now;
            int numeroMuestra = obtSigMuestra(idProyecto, fechaMuestra);
            string nombrePaciente = txtIdPaciente.Text;
            List<int> tiposExamen = obtTipoExaSelect();

            using (NpgsqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insertar la muestra
                        string queryMuestra = "INSERT INTO muestra (id_proyecto, numero_muestra, fecha_muestra, nombre_paciente) VALUES (@IdProyecto, @NumeroMuestra, @FechaMuestra, @NombrePaciente) RETURNING id_muestra";
                        NpgsqlCommand commandMuestra = new NpgsqlCommand(queryMuestra, connection, transaction);
                        commandMuestra.Parameters.AddWithValue("@IdProyecto", idProyecto);
                        commandMuestra.Parameters.AddWithValue("@NumeroMuestra", numeroMuestra);
                        commandMuestra.Parameters.AddWithValue("@FechaMuestra", fechaMuestra);
                        commandMuestra.Parameters.AddWithValue("@NombrePaciente", nombrePaciente);
                        int idMuestra = Convert.ToInt32(commandMuestra.ExecuteScalar());

                        // Insertar los tipos de examen seleccionados en muestra_examen
                        foreach (int idTipoExamen in tiposExamen)
                        {
                            string queryExamen = "INSERT INTO muestra_examen (id_muestra, id_tipo_examen) VALUES (@IdMuestra, @IdTipoExamen)";
                            NpgsqlCommand commandExamen = new NpgsqlCommand(queryExamen, connection, transaction);
                            commandExamen.Parameters.AddWithValue("@IdMuestra", idMuestra);
                            commandExamen.Parameters.AddWithValue("@IdTipoExamen", idTipoExamen);
                            commandExamen.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Muestra guardada correctamente.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error al guardar la muestra: " + ex.Message);
                    }
                }
            }
        }
        private void CargarTiposExamen()
        {
            using (NpgsqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT id_tipo_examen, nombre_examen FROM tipo_examen WHERE activo = true";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                cklExamenes.DisplayMember = "nombre_examen";
                cklExamenes.ValueMember = "id_tipo_examen";
                cklExamenes.DataSource = dt;
            }
        }
        private int obtSigMuestra(int idProyecto, DateTime fecha)
        {
            using (NpgsqlConnection connection = DatabaseConnection.GetConnection())
            {
                string query = "SELECT COALESCE(MAX(numero_muestra), 0) + 1 FROM muestra WHERE id_proyecto = @IdProyecto AND fecha_muestra = @FechaMuestra";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdProyecto", idProyecto);
                command.Parameters.AddWithValue("@FechaMuestra", fecha);

                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        private bool validarExamenes()
        {
            if (cklExamenes.CheckedItems.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un tipo de examen.");
                return false;
            }
            return true;
        }
        private void mostrarNumMuestra()
        {
            // Validar que se haya seleccionado un proyecto
            if (cmbProyecto.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un proyecto para generar el número de muestra.");
                return;
            }

            int idProyecto = (int)cmbProyecto.SelectedValue;
            DateTime fechaMuestra = DateTime.Now;

            // Obtener el siguiente número de muestra
            int numeroMuestra = obtSigMuestra(idProyecto, fechaMuestra);

            // Asignar el número de muestra al campo txtMuestrasId
            txtMuestrasId.Text = numeroMuestra.ToString();
        }

        private void cklExamenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            mostrarNumMuestra();
        }
    }
}
