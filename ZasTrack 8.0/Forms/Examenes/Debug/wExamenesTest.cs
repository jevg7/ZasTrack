using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZasTrack.Models;
using ZasTrack.Repositories;

namespace ZasTrack.Forms.Examenes
{
    public partial class wExamenesTest : Form
    {
        private ProyectoRepository proyectoRepository;
        private ExamenRepository examenRepository;
        private int ultimoProyectoSeleccionado = -1;

        public wExamenesTest()
        {
            InitializeComponent();
            proyectoRepository = new ProyectoRepository();
            examenRepository = new ExamenRepository();

            CargarProyectos();
        }

        private void CargarProyectos()
        {
            var proyectos = proyectoRepository.ObtenerProyectos();
            cmbProyecto.DataSource = proyectos;
            cmbProyecto.DisplayMember = "nombre";
            cmbProyecto.ValueMember = "id_proyecto";
            cmbProyecto.SelectedIndex = -1;
        }

        private void cmbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProyecto.SelectedItem is Proyecto p)
            {
                ultimoProyectoSeleccionado = p.id_proyecto;
                CargarPacientesPorProyecto(p.id_proyecto);
            }
        }

        private void CargarPacientesPorProyecto(int idProyecto)
        {
            pnlPacientes.Controls.Clear();

            // Títulos
            var titlePanel = new Panel
            {
                Height = 30,
                Dock = DockStyle.Top,
                BackColor = Color.LightGray
            };
            titlePanel.Controls.Add(CreateLabel("Muestra", 10));
            titlePanel.Controls.Add(CreateLabel("Paciente", 120));
            titlePanel.Controls.Add(CreateLabel("Género", 270));
            titlePanel.Controls.Add(CreateLabel("Edad", 360));
            titlePanel.Controls.Add(CreateLabel("Exámenes Pendientes", 420));
            titlePanel.Controls.Add(CreateLabel("Fecha Recepción", 600));
            titlePanel.Controls.Add(CreateLabel("Acciones", 750));
            pnlPacientes.Controls.Add(titlePanel);

            var pacientes = examenRepository.ObtenerPacientesPorProyecto(idProyecto);
            if (!pacientes.Any())
            {
                pnlPacientes.Controls.Add(new Label
                {
                    Text = "No se encontró ningúna muestra en el proyecto seleccionado.",
                    Location = new Point(10, 40),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.Red
                });
                return;
            }

            // Para cada muestra, creamos un panel
            foreach (var pac in pacientes)
            {
                var panel = new Panel
                {
                    Height = 60,
                    Dock = DockStyle.Top,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle
                };

                panel.Controls.Add(CreateLabel($"#{pac.NumeroMuestra}", 10));
                panel.Controls.Add(CreateLabel(pac.Paciente, 120));
                panel.Controls.Add(CreateLabel(pac.Genero, 270));
                panel.Controls.Add(CreateLabel(pac.Edad.ToString(), 360));

                // *** ¡La parte simplificada! ***
                // Usa directamente la cadena de pendientes del ViewModel
                panel.Controls.Add(CreateLabel(pac.ExamenesPendientesStr, 420)); // Ya no necesitas el placeholder "..."

                panel.Controls.Add(CreateLabel(pac.FechaRecepcion.ToShortDateString(), 600));

                var btn = new Button
                {
                    Text = "Procesar",
                    Location = new Point(750, 15),
                    Size = new Size(80, 30),
                    Tag = pac.IdMuestra
                };
                btn.Click += BtnAccion_Click;
                panel.Controls.Add(btn);

                pnlPacientes.Controls.Add(panel);
                pnlPacientes.Controls.SetChildIndex(panel, 0); // Mantiene el orden de agregado

                // *** ¡Ya NO necesitas llamar a ObtenerExamenesPendientesPorMuestra aquí! ***
                // var lblPend = panel.Controls...
                // var pendientes = examenRepository.ObtenerExamenesPendientesPorMuestra(...);
                // lblPend.Text = ...;
                // --> ¡Todo ese bloque se elimina! <--
            }
        }

        private Label CreateLabel(string text, int x)
            => new Label
            {
                Text = text,
                Location = new Point(x, 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };

        private void BtnAccion_Click(object sender, EventArgs e)
        {
            int idMuestra = (int)((Button)sender).Tag;
            MessageBox.Show($"Procesar muestra #{idMuestra}");
            // Aquí abres tu modal de procesamiento...
        }
        private void pnlPacientes_Paint(object sender, PaintEventArgs e)
        {
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
