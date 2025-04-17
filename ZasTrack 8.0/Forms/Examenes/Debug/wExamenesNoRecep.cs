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
    public partial class wExamenesNoRecep : Form
    {
        private ProyectoRepository proyectoRepository;
        private ExamenRepository examenRepository;
        private int ultimoProyectoSeleccionado = -1;
        private Button btnClearFilters => btnLimpiarFiltros; 

        public wExamenesNoRecep()
        {
            InitializeComponent(); 
            proyectoRepository = new ProyectoRepository();
            examenRepository = new ExamenRepository();

            // --- Asignación de Eventos ---
            cmbProyecto.SelectedIndexChanged += cmbProyecto_SelectedIndexChanged;
            dtpFechaRecepcion.ValueChanged += dtpFechaRecepcion_ValueChanged;
            chkFiltroOrina.CheckedChanged += FiltroTipoExamen_CheckedChanged;
            chkFiltroHeces.CheckedChanged += FiltroTipoExamen_CheckedChanged;
            chkFiltroSangre.CheckedChanged += FiltroTipoExamen_CheckedChanged;
            txtSearch.KeyDown += txtSearch_KeyDown;         // Evento para buscar con Enter
            btnActualizar.Click += btnActualizar_Click;     // Evento para botón Actualizar
            btnClearFilters.Click += btnLimpiarFiltros_Click; // Evento para botón Limpiar

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
            pnlPacientes.Controls.Clear(); 
        }

        private void cmbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProyecto.SelectedItem is Proyecto p)
            {
                ultimoProyectoSeleccionado = p.id_proyecto;
                RecargarListaSiEsNecesario(); 
            }
            else
            {
                ultimoProyectoSeleccionado = -1;
                pnlPacientes.Controls.Clear();
            }
        }
        private void AddTitlePanel()
        {
            if (pnlPacientes.Controls.ContainsKey("titlePanel")) return;

            var titlePanel = new Panel
            {
                Height = 30,
                Dock = DockStyle.Top,
                BackColor = Color.LightGray,
                Name = "titlePanel"
            };
            
            titlePanel.Controls.Add(CreateLabel("Muestra", 10));
            titlePanel.Controls.Add(CreateLabel("Paciente", 120));
            titlePanel.Controls.Add(CreateLabel("Género", 270));
            titlePanel.Controls.Add(CreateLabel("Edad", 360));
            titlePanel.Controls.Add(CreateLabel("Exámenes Pendientes", 420));
            titlePanel.Controls.Add(CreateLabel("Fecha Recepción", 600));
            titlePanel.Controls.Add(CreateLabel("Acciones", 750));
           
            pnlPacientes.Controls.Add(titlePanel);
        }

   
        private void CargarPacientesPorProyecto(int idProyecto, DateTime fecha, List<int> tiposFiltro, string textoBusqueda)
        {
            Control focusedControl = this.ActiveControl;

            pnlPacientes.SuspendLayout();

            pnlPacientes.Controls.Clear();
            AddTitlePanel(); 

            List<Panel> panelesData = new List<Panel>();

            try
            {
                var pacientes = examenRepository.ObtenerPacientesPorProyecto(idProyecto, fecha, tiposFiltro, textoBusqueda);

                if (pacientes.Any()) // Solo si hay pacientes
                {
                    CultureInfo culturaEsp = new CultureInfo("es-NI");
                    foreach (var pac in pacientes) 
                    {
                        var panel = new Panel
                        {
                            Height = 60,
                            Dock = DockStyle.Top,
                            BackColor = Color.White,
                            BorderStyle = BorderStyle.FixedSingle
                        };

                        string diaSemana = pac.FechaRecepcion.ToString("dddd", culturaEsp);
                        if (!string.IsNullOrEmpty(diaSemana)) { diaSemana = char.ToUpper(diaSemana[0]) + diaSemana.Substring(1); }
                        panel.Controls.Add(CreateLabel($"{diaSemana} #{pac.NumeroMuestra}", 10));
                        Label lblPaciente = CreateLabel(pac.Paciente, 120);
                        panel.Controls.Add(lblPaciente);
                        panel.Controls.Add(CreateLabel(pac.Genero, 270));
                        panel.Controls.Add(CreateLabel(pac.Edad.ToString(), 360));
                        panel.Controls.Add(CreateLabel(pac.ExamenesPendientesStr, 420));
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
                 
                    
                        lblPaciente.BackColor = panel.BackColor; 
                        if (!string.IsNullOrWhiteSpace(textoBusqueda))
                        {
                            bool coincidenciaEncontrada = (pac.Paciente.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                           pac.NumeroMuestra.ToString().Contains(textoBusqueda));
                            if (coincidenciaEncontrada) { lblPaciente.BackColor = Color.Yellow; }
                        }

                        pnlPacientes.Controls.Add(panel);

                       

                        pnlPacientes.Controls.SetChildIndex(panel, 0);

                    }
                }
                else // Si no hay pacientes
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
                }

               
                if (panelesData.Any())
                {
                    pnlPacientes.Controls.AddRange(panelesData.ToArray());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las muestras: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                pnlPacientes.ResumeLayout(true); 
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            RecargarListaSiEsNecesario();
        }

        private void BtnAccion_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is int idMuestra)
            {
                MessageBox.Show($"Procesar muestra ID: {idMuestra}"); 

              
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


   
        private void RecargarListaSiEsNecesario()
        {
            if (ultimoProyectoSeleccionado != -1)
            {
                List<int> tiposSeleccionados = ObtenerTiposExamenSeleccionados();
                DateTime fechaSeleccionada = dtpFechaRecepcion.Value.Date;
                string textoBusqueda = txtSearch.Text.Trim();

                CargarPacientesPorProyecto(ultimoProyectoSeleccionado, fechaSeleccionada, tiposSeleccionados, textoBusqueda);
            }
            else
            {
                pnlPacientes.Controls.Clear();
            }
        }
        private List<int> ObtenerTiposExamenSeleccionados()
        {
            var tipos = new List<int>();
            if (chkFiltroOrina.Checked) tipos.Add(1); // ID Orina = 1
            if (chkFiltroHeces.Checked) tipos.Add(2); // ID Heces = 2
            if (chkFiltroSangre.Checked) tipos.Add(3); // ID Sangre = 3
            return tipos;
        }          

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            // Guarda el estado actual para evitar recargas innecesarias por eventos
            bool projectWasSelected = (ultimoProyectoSeleccionado != -1);

            // Desactivar eventos temporalmente (opcional pero más limpio)
            dtpFechaRecepcion.ValueChanged -= dtpFechaRecepcion_ValueChanged;
            chkFiltroOrina.CheckedChanged -= FiltroTipoExamen_CheckedChanged;
            chkFiltroHeces.CheckedChanged -= FiltroTipoExamen_CheckedChanged;
            chkFiltroSangre.CheckedChanged -= FiltroTipoExamen_CheckedChanged;

            // Resetear controles
            cmbProyecto.SelectedIndex = -1;
            dtpFechaRecepcion.Value = DateTime.Today;
            chkFiltroOrina.Checked = false;
            chkFiltroHeces.Checked = false;
            chkFiltroSangre.Checked = false;
            txtSearch.Text = "";

            // Reactivar eventos
            dtpFechaRecepcion.ValueChanged += dtpFechaRecepcion_ValueChanged;
            chkFiltroOrina.CheckedChanged += FiltroTipoExamen_CheckedChanged;
            chkFiltroHeces.CheckedChanged += FiltroTipoExamen_CheckedChanged;
            chkFiltroSangre.CheckedChanged += FiltroTipoExamen_CheckedChanged;

            // Actualizar estado y limpiar panel
            ultimoProyectoSeleccionado = -1;
            pnlPacientes.Controls.Clear();
          

        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter en el cuadro
            if (e.KeyCode == Keys.Enter)
            {
                RecargarListaSiEsNecesario(); 
                e.SuppressKeyPress = true; 
            }
        }
        #region RecargarLista Windows Metodos
        private void dtpFechaRecepcion_ValueChanged(object sender, EventArgs e)
        {
            RecargarListaSiEsNecesario(); // Usa el método centralizado
        }
      
        private void FiltroTipoExamen_CheckedChanged(object sender, EventArgs e)
        {
            RecargarListaSiEsNecesario(); // Recarga al cambiar checkbox
        }
        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            RecargarListaSiEsNecesario(); // Usa el método centralizado

        }
        #endregion
        #region Windows Form Designer generated code
        private void dtpFechaRecepcion_ValueChanged_1(object sender, EventArgs e)
        {

        }
        private void pnlPacientes_Paint(object sender, PaintEventArgs e)
        {
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion

    }
}
