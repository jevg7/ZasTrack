﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Forms.Examenes.Debug;
using ZasTrack.Models;
using ZasTrack.Repositories;

namespace ZasTrack.Forms.Examenes
{
    public partial class wExamenesTest : Form
    {
        public wExamenesTest()
        {
            InitializeComponent();
            CargarProyectos();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Abrir_Form(object formhijo)
        {

            if (this.pnlContenedor.Controls.Count > 0)
                this.pnlContenedor.Controls.RemoveAt(0); // Elimina cualquier control existente del panel contenedor.

            Form fh = formhijo as Form; // Convierte el objeto de entrada en un formulario.
            fh.TopLevel = false; // Establece la propiedad TopLevel del formulario como false.
            fh.Dock = DockStyle.Fill; // Establece la propiedad Dock del formulario para que ocupe todo el espacio del panel contenedor.
            this.pnlContenedor.Controls.Add(fh); // Agrega el formulario al panel contenedor.
            this.pnlContenedor.Tag = fh; // Establece la propiedad Tag del panel contenedor como el formulario.
            fh.Show(); // Muestra el formulario.
        }

        private void procesadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir_Form(new wProcesados());
        }

        private void noProcesadosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Abrir_Form(new wNoProcesados());
        }
        private void cmbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
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
    }
}
