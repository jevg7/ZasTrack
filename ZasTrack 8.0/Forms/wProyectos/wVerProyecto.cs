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

namespace ZasTrack.Forms.wProyectos
{
    public partial class wVerProyecto : Form
    {
        private ProyectoRepository proyectoRepository;

        public wVerProyecto()
        {
            InitializeComponent();

            proyectoRepository = new ProyectoRepository();
            CargarProyectosAsync();

        }

        #region Metodos
        private async void CargarProyectosAsync()
        {
            MostrarCargando(true);

            try
            {
                Console.WriteLine("Obteniendo proyectos...");
                List<Proyecto> proyectos = await Task.Run(() => proyectoRepository.ObtenerProyectos());
                Console.WriteLine($"Proyectos obtenidos: {proyectos?.Count ?? 0}");

                if (proyectos == null || proyectos.Count == 0)
                {
                    MessageBox.Show("No se encontraron proyectos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Console.WriteLine("Limpiando FlowLayoutPanel...");
                flpProyList.Controls.Clear();
                    
                Console.WriteLine("Agregando proyectos al FlowLayoutPanel...");
                foreach (Proyecto proyecto in proyectos)
                {
                    Console.WriteLine($"Agregando proyecto: {proyecto.nombre}");
                    Panel pnlProyecto = new Panel
                    {
                        Size = new Size(300, 150),
                        BackColor = SystemColors.ControlLight,
                        Margin = new Padding(10),
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    Label lblNombre = new Label
                    {
                        Text = proyecto.nombre,
                        AutoSize = false,
                        Size = new Size(180, 50),
                        Location = new Point(5, 10),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        BorderStyle = BorderStyle.Fixed3D,
                    };

                    Label lblFechaInicio = new Label
                    {
                        Text = $"Inicio: {proyecto.fecha_inicio.ToShortDateString()}",
                        AutoSize = false,
                        Size = new Size(180, 20),
                        Location = new Point(10, 60),
                        TextAlign = ContentAlignment.MiddleLeft,
                        Font = new Font("Segoe UI", 9)
                    };

                    Label lblFechaFin = new Label
                    {
                        Text = proyecto.fecha_fin.HasValue ? $"Fin: {proyecto.fecha_fin.Value.ToShortDateString()}" : "Fin: No definido",
                        AutoSize = false,
                        Size = new Size(180, 20),
                        Location = new Point(10, 80),
                        TextAlign = ContentAlignment.MiddleLeft,
                        Font = new Font("Segoe UI", 9)
                    };
                    Button btnDetalles = new Button
                    {
                        Text = "Detalles",
                        Size = new Size(80, 30),
                        Location = new Point(10, 100),
                        BackColor = Color.LightBlue,
                        FlatStyle = FlatStyle.Flat
                    };

                    pnlProyecto.Controls.Add(lblNombre);
                    pnlProyecto.Controls.Add(lblFechaInicio);
                    pnlProyecto.Controls.Add(lblFechaFin);
                    pnlProyecto.Controls.Add(btnDetalles);

                    flpProyList.Controls.Add(pnlProyecto);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en CargarProyectosAsync: " + ex.Message);
                MessageBox.Show("Error al cargar los proyectos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Console.WriteLine("Finalizando carga...");
                MostrarCargando(false);
            }
        }

        private void MostrarCargando(bool mostrar)
        {
            // Definir el nombre del Label de carga
            string nombreLabelCargando = "lblCargando";

            if (mostrar)
            {
                // Mostrar un mensaje de carga
                Label lblCargando = new Label
                {
                    Name = nombreLabelCargando, // Asignar un nombre único
                    Text = "Cargando proyectos...",
                    AutoSize = true,
                    Location = new Point(10, 10),
                    Font = new Font("Segoe UI", 12)
                };
                pnlCargando.Controls.Add(lblCargando);
            }
            else
            {
                // Ocultar el mensaje de carga (eliminar solo el Label de carga)
                Control lblCargando = pnlCargando.Controls.Find(nombreLabelCargando, true).FirstOrDefault();
                if (lblCargando != null)
                {
                    pnlCargando.Controls.Remove(lblCargando);
                }
            }
        }
        #endregion

        #region Windows Form Designer generated code
        private void flpProyList_Paint(object sender, PaintEventArgs e)
        {
                                
        }
        #endregion

    }
}
