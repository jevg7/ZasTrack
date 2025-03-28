﻿using System;
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

namespace ZasTrack.Forms
{
    public partial class wAñadirProyecto : Form
    {
        public wAñadirProyecto()
        {
            InitializeComponent();
        }

        private void wAñadirProyecto_Load(object sender, EventArgs e)
        {

        }
        private void btnGuardarProyecto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreProyecto.Text))
            {
                MessageBox.Show("El nombre del proyecto es requerido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var proyecto = new Proyecto
            {
                nombre = txtNombreProyecto.Text,
                fecha_inicio = dtpFechaInicio.Value,
                fecha_fin = dtpFechaFin.Checked ? dtpFechaFin.Value : (DateTime?)null
            };

            var proyectoRepository = new ProyectoRepository();
            try
            {
                proyectoRepository.GuardarProyecto(proyecto);
                MessageBox.Show("Proyecto guardado correctamente.");

                // Cerrar el formulario después de guardar
                this.DialogResult = DialogResult.OK; // Indica que el formulario se cerró correctamente
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el proyecto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }
    }
}
