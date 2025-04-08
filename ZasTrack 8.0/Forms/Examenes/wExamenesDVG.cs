using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using ZasTrack.Repositories;

namespace ZasTrack.Forms.Examenes
{
    public partial class wExamenesDVG : Form
    {
        private PacienteRepository pacienteRepository;
        private ExamenRepository examenRepository;
        private MuestraRepository muestrasRepository;

        public wExamenesDVG()
        {
            InitializeComponent();
         
            pacienteRepository = new PacienteRepository();
            examenRepository = new ExamenRepository();
            muestrasRepository = new MuestraRepository();
            CargarPacientesDesdeDB();
        }

   
        private void FormExamenes_Load(object sender, EventArgs e)
        {
            CargarPacientesDesdeDB();
        }

        #region Metodos
        private void CargarPacientesDesdeDB()
        {
            // 1. Limpiar controles existentes
            flpPacientes.Controls.Clear();

            // 2. Obtener datos de la DB
            string query = "SELECT id_paciente, nombres, apellidos FROM pacientes";
            using (var conn = DatabaseConnection.GetConnection())

            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // 3. Crear control dinámico por cada paciente
                        var panelPaciente = new Panel
                        {
                            Width = 300,
                            Height = 50,
                            Margin = new Padding(5),
                            BorderStyle = BorderStyle.FixedSingle
                        };

                        var lblNombre = new Label
                        {
                            Text = $"{reader["nombres"]} {reader["apellidos"]}",
                            Location = new Point(10, 10),
                            Font = new Font("Arial", 10, FontStyle.Bold),
                            AutoSize = true
                        };

                        var lblDetalle = new Label
                        {
                            Text = "No hay grupos · 0 minutos", // Ejemplo estático
                            Location = new Point(10, 30),
                            ForeColor = Color.Gray,
                            AutoSize = true
                        };

                        // 4. Agregar controles al panel del paciente
                        panelPaciente.Controls.Add(lblNombre);
                        panelPaciente.Controls.Add(lblDetalle);

                        // 5. Añadir al FlowLayoutPanel
                        flpPacientes.Controls.Add(panelPaciente);
                    }
                }
            }
        }
        #endregion
        #region Windows Form Designer generated code
        private void flpPacientes_Paint(object sender, PaintEventArgs e)
        {

        }
        private void pnlContenedorDvg_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion
    }
}
