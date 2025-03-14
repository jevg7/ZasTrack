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
using ZasTrack.Models;
using ZasTrack.Repositories;

namespace ZasTrack.Forms.wProyectos
{
    public partial class wProyectos : Form
    {
        private ProyectoRepository proyectoRepository; // Declarar la variable

        public wProyectos()
        {
            InitializeComponent();
            proyectoRepository = new ProyectoRepository(); // Inicializar el repositorio
            CargarProyectosAsync(); // Cargar y mostrar los proyectos al iniciar el formulario
        }

        private void tsmiAñadirProyectos_Click(object sender, EventArgs e)
        {

        }

        private void wProyectos_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pnlProyectos_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnAggProyect_Click1(object sender, EventArgs e)
        {
            // Abrir el formulario wAñadirProyecto como un diálogo
            using (var form = new wAñadirProyecto()) ;
           
        }

        private void splitContainer1_Panel1_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlAggProy_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblAdmProyec_Click(object sender, EventArgs e)
        {

        }

        private void Abrir_Form(object formhijo)
        {
            if (this.pnlProyChildren.Controls.Count > 0)
                this.pnlProyChildren.Controls.RemoveAt(0); // Eliminar cualquier control existente

            Form fh = formhijo as Form; // Convertir el objeto a un formulario
            fh.TopLevel = false; // Establecer como formulario secundario
            fh.Dock = DockStyle.Fill; // Ajustar al tamaño del panel contenedor
            this.pnlProyChildren.Controls.Add(fh); // Agregar al panel contenedor
            this.pnlProyChildren.Tag = fh; // Establecer referencia
            fh.Show(); // Mostrar el formulario
        }
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
                        Size = new Size(200, 100),
                        BackColor = SystemColors.ControlLight,
                        Margin = new Padding(10),
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    Label lblNombre = new Label
                    {
                        Text = proyecto.nombre,
                        AutoSize = false,
                        Size = new Size(180, 40),
                        Location = new Point(10, 10),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 12, FontStyle.Bold)
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

                    pnlProyecto.Controls.Add(lblNombre);
                    pnlProyecto.Controls.Add(lblFechaInicio);
                    pnlProyecto.Controls.Add(lblFechaFin);

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
                flpProyList.Controls.Add(lblCargando);
            }
            else
            {
                // Ocultar el mensaje de carga (eliminar solo el Label de carga)
                Control lblCargando = flpProyList.Controls.Find(nombreLabelCargando, true).FirstOrDefault();
                if (lblCargando != null)
                {
                    flpProyList.Controls.Remove(lblCargando);
                }
            }
        }




    }

}


    



