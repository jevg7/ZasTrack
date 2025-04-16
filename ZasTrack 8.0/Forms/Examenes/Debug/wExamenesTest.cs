using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
            dtpFechaRecepcion.ValueChanged += dtpFechaRecepcion_ValueChanged;

            dtpFechaRecepcion.Value = DateTime.Today;

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
                CargarPacientesPorProyecto(p.id_proyecto, dtpFechaRecepcion.Value.Date);
                // --------------------------
            }
            else
            {
                ultimoProyectoSeleccionado = -1;
                pnlPacientes.Controls.Clear();
            }
        }


        private void CargarPacientesPorProyecto(int idProyecto, DateTime fecha)
        {
            pnlPacientes.Controls.Clear();

            // Títulos (sin cambios)
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

            // Llamada al repositorio (asume que el repositorio ya está modificado para aceptar fecha)
            var pacientes = examenRepository.ObtenerPacientesPorProyecto(idProyecto, fecha);

            if (!pacientes.Any())
            {
                pnlPacientes.Controls.Add(new Label
                {
                    // Ajusta el texto si quieres ser más específico sobre la fecha
                    Text = "No se encontró ninguna muestra para el proyecto y fecha seleccionados.",
                    Location = new Point(10, 40),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.DarkOrange // Cambié el color para diferenciarlo de un error
                });
                return;
            }

            // Define la cultura una vez antes del bucle
            CultureInfo culturaEsp = new CultureInfo("es-NI");

            // Para cada muestra, creamos un panel
            foreach (var pac in pacientes) // pac es de tipo MuestraInfoViewModel
            {
                var panel = new Panel
                {
                    Height = 60,
                    Dock = DockStyle.Top,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle
                };

                // ----- INICIO: Modificación para la primera etiqueta -----
                // Obtiene el nombre del día de la semana en español
                string diaSemana = pac.FechaRecepcion.ToString("dddd", culturaEsp);
                // Capitaliza la primera letra
                if (!string.IsNullOrEmpty(diaSemana))
                {
                    diaSemana = char.ToUpper(diaSemana[0]) + diaSemana.Substring(1);
                }
                // Crea la etiqueta con el formato "Dia #Num"
                panel.Controls.Add(CreateLabel($"{diaSemana} #{pac.NumeroMuestra}", 10));
                // ----- FIN: Modificación para la primera etiqueta -----

                // El resto de las etiquetas usan los datos del ViewModel
                panel.Controls.Add(CreateLabel(pac.Paciente, 120));
                panel.Controls.Add(CreateLabel(pac.Genero, 270));
                panel.Controls.Add(CreateLabel(pac.Edad.ToString(), 360));
                // Usa la cadena de pendientes directamente (viene del repo con STRING_AGG)
                panel.Controls.Add(CreateLabel(pac.ExamenesPendientesStr, 420));
                panel.Controls.Add(CreateLabel(pac.FechaRecepcion.ToShortDateString(), 600));
                

                // Botón Procesar (sin cambios)
                var btn = new Button
                {
                    Text = "Procesar",
                    Location = new Point(750, 15),
                    Size = new Size(80, 30),
                    Tag = pac.IdMuestra // Pasamos el IdMuestra real
                };
                btn.Click += BtnAccion_Click;
                panel.Controls.Add(btn);

                // Añade el panel completo
                pnlPacientes.Controls.Add(panel);
                // Asegura el orden visual correcto al añadir desde arriba
                pnlPacientes.Controls.SetChildIndex(panel, 0);
            }
        }

        // El método CreateLabel (sin cambios)
        private Label CreateLabel(string text, int x)
            => new Label
            {
                Text = text,
                Location = new Point(x, 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };

        // El método BtnAccion_Click (sin cambios)
        private void BtnAccion_Click(object sender, EventArgs e)
        {
            int idMuestra = (int)((Button)sender).Tag;
            MessageBox.Show($"Procesar muestra ID: {idMuestra}");
            // Cambié un poco el mensaje para claridad
                                                                 
            // Aquí abres tu modal de procesamiento...
                                                                  
            // Ejemplo:
                                                                  
            // using (var modalForm = new wProcesarResultados(idMuestra))
                                                                  
            // {
                                                                  
            //     modalForm.ShowDialog();
                                                                  
            //     // Después de cerrar el modal, refresca la lista
                                                                  
            //     if(ultimoProyectoSeleccionado != -1)
                                                                 
            //     {
                                                                  
            //          CargarPacientesPorProyecto(ultimoProyectoSeleccionado, dtpFechaRecepcion.Value.Date);
                                                                  
            //     }
                                                                  
            // }
        }
        private void dtpFechaRecepcion_ValueChanged(object sender, EventArgs e)
        {
            if (ultimoProyectoSeleccionado != -1)
            {
                CargarPacientesPorProyecto(ultimoProyectoSeleccionado, dtpFechaRecepcion.Value.Date);
            }
        }
        // ------------------------------------
        private void pnlPacientes_Paint(object sender, PaintEventArgs e)
        {
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
