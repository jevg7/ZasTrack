using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZasTrack.Models;
using ZasTrack.Repositories;

namespace ZasTrack.Forms.Examenes
{
    public partial class wExamenesTest : Form
    {
        private ProyectoRepository proyectoRepository;
        private MuestraRepository muestraRepository;
        private ExamenRepository examenRepository; // Agregado ExamenRepository
        private int ultimoProyectoSeleccionado = -1;

        public wExamenesTest()
        {
            InitializeComponent();
            proyectoRepository = new ProyectoRepository();
            muestraRepository = new MuestraRepository();
            examenRepository = new ExamenRepository(); // Inicialización de ExamenRepository
            CargarProyectos();
        }

        // Cargar los proyectos en el ComboBox
        private void CargarProyectos()
        {
            List<Proyecto> proyectos = proyectoRepository.ObtenerProyectos();
            cmbProyecto.DataSource = proyectos;
            cmbProyecto.DisplayMember = "nombre";
            cmbProyecto.ValueMember = "id_proyecto";
            cmbProyecto.SelectedIndex = -1;  // Dejar el primer valor vacío
        }

        // Cuando se selecciona un proyecto en el ComboBox
        private void cmbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProyecto.SelectedValue != null)
            {
                int idProyecto = (cmbProyecto.SelectedItem as Proyecto)?.id_proyecto ?? -1;
                ultimoProyectoSeleccionado = idProyecto;

                // Ahora que se seleccionó un proyecto, cargar los pacientes asociados
                CargarPacientesPorProyecto(idProyecto);
            }
        }

        // Método para cargar los pacientes por proyecto seleccionado
        private void CargarPacientesPorProyecto(int idProyecto)
        {
            pnlPacientes.Controls.Clear(); // Limpiar los controles anteriores en pnlPacientes

            // Agregar los títulos de las columnas
            Panel titlePanel = new Panel
            {
                Height = 30,
                Dock = DockStyle.Top,
                BackColor = Color.LightGray
            };

            titlePanel.Controls.Add(CreateLabel("Muestra", 10));
            titlePanel.Controls.Add(CreateLabel("Paciente", 120));
            titlePanel.Controls.Add(CreateLabel("Genero", 270));
            titlePanel.Controls.Add(CreateLabel("Edad", 360));
            titlePanel.Controls.Add(CreateLabel("Examenes Pendientes", 420));
            titlePanel.Controls.Add(CreateLabel("Fecha Recepcion", 560));
            titlePanel.Controls.Add(CreateLabel("ACCIONES", 700));

            pnlPacientes.Controls.Add(titlePanel);

            // Llamar al método del repository para obtener los pacientes
            var pacientes = examenRepository.ObtenerPacientesPorProyecto(idProyecto);

            if (pacientes.Count == 0)
            {
                Label lblNoPacientes = new Label
                {
                    Text = "No se encontró ningún paciente en el proyecto seleccionado.",
                    Location = new Point(10, 40),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.Red
                };
                pnlPacientes.Controls.Add(lblNoPacientes);
            }
            else
            {
                // Agregar un panel por cada paciente
                foreach (var paciente in pacientes)
                {
                    Panel panel = new Panel
                    {
                        Height = 60,
                        Dock = DockStyle.Top,
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    panel.Controls.Add(CreateLabel($"Muestra #{paciente.NumeroMuestra}", 10));
                    panel.Controls.Add(CreateLabel(paciente.Paciente, 120));
                    panel.Controls.Add(CreateLabel(paciente.Genero, 270));
                    panel.Controls.Add(CreateLabel(paciente.Edad.ToString(), 360));
                    panel.Controls.Add(CreateLabel(paciente.ExamenesPendientes.ToString(), 420));
                    panel.Controls.Add(CreateLabel(paciente.FechaRecepcion.ToShortDateString(), 500));

                    Button btnAccion = new Button
                    {
                        Text = "Procesar",
                        Location = new Point(630, 15),
                        Size = new Size(80, 30),
                        Tag = paciente.IdMuestra
                    };
                    btnAccion.Click += BtnAccion_Click;
                    panel.Controls.Add(btnAccion);

                    pnlPacientes.Controls.Add(panel); // Agregar el panel al contenedor
                    pnlPacientes.Controls.SetChildIndex(panel, 0); // Para que lo último agregado quede arriba
                }
            }
        }

        // Crear las etiquetas que se mostrarán para cada paciente
        private Label CreateLabel(string text, int x)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
        }

        private void BtnAccion_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int idMuestra = (int)btn.Tag;

            MessageBox.Show($"Procesar muestra #{idMuestra}");
            // Aquí podrías abrir tu modal de procesamiento
        }
        private void pnlPacientes_Paint(object sender, PaintEventArgs e)
        {
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
