using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ZasTrack.Models;
using ZasTrack.Repositories;

namespace ZasTrack
{
    public partial class wAgregarPaciente : Form
    {
        private PacienteRepository pacienteRepository;
        private ProyectoRepository proyectoRepository;
        public wAgregarPaciente()
        {
            InitializeComponent();
            pacienteRepository = new PacienteRepository();
            proyectoRepository = new ProyectoRepository();
        }
        
        private void wAgregarPaciente_Load(object sender, EventArgs e) // Nombre actualizado
        {
            // --- Validación de Fecha ---
            dtpFechaNac.MaxDate = DateTime.Today; // No permitir fechas futuras
                                                  // No permitir fechas demasiado antiguas (ej. antes de 1900)
            dtpFechaNac.MinDate = new DateTime(1900, 1, 1);
            // Establecer un valor inicial razonable (opcional, ej: hace 20 años)
            // dtpFechaNac.Value = DateTime.Today.AddYears(-20);
            // ---------------------------

            // Cargar Género
            cmbGenero.Items.Add("Masculino");
            cmbGenero.Items.Add("Femenino");
            cmbGenero.SelectedIndex = 0; // O -1 para ninguno seleccionado

            // Cargar Proyectos (¡Importante el filtro!)
            CargarProyectos();

            // Calcular edad inicial basada en la fecha por defecto del picker
            dateTimePicker1_ValueChanged(dtpFechaNac, EventArgs.Empty);
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaNacimiento = dtpFechaNac.Value;
            DateTime hoy = DateTime.Today;
            string textoEdad = "";

            if (fechaNacimiento > hoy)
            {
                textoEdad = "Fecha inválida";
            }
            else
            {
                // Calcular diferencia total
                TimeSpan diferenciaTotal = hoy - fechaNacimiento;
                int diasTotales = (int)diferenciaTotal.TotalDays;

                // Calcular años completos
                int anios = hoy.Year - fechaNacimiento.Year;
                if (hoy < fechaNacimiento.AddYears(anios))
                {
                    anios--;
                }
                if (anios < 0) anios = 0; // Por si nació hoy

                // Calcular meses completos desde el último cumpleaños
                DateTime ultimoCumple = fechaNacimiento.AddYears(anios);
                int meses = 0;
                while (ultimoCumple.AddMonths(meses + 1) <= hoy)
                {
                    meses++;
                }

                // Calcular días desde el último mes cumplido (aproximado)
                int dias = (hoy - ultimoCumple.AddMonths(meses)).Days;


                // --- Formatear Salida ---
                if (anios > 0)
                {
                    // Si tiene 1 año o más, mostrar Años y Meses
                    textoEdad = $"{anios} año{(anios != 1 ? "s" : "")}";
                    if (meses > 0) // Añadir meses solo si son más de 0
                    {
                        textoEdad += $" ({meses} mes{(meses != 1 ? "es" : "")})";
                    }
                }
                else if (meses > 0)
                {
                    // Si tiene 0 años pero sí meses, mostrar solo Meses y Días
                    textoEdad = $"{meses} mes{(meses != 1 ? "es" : "")}";
                    // Opcional: Añadir días si quieres más precisión para bebés
                    if (dias > 0) textoEdad += $" ({dias} día{(dias != 1 ? "s" : "")})";
                }
                else
                {
                    // Si tiene 0 años y 0 meses, mostrar Días
                    textoEdad = $"{dias} día{(dias != 1 ? "s" : "")}";
                    // Podrías poner "Recién nacido" si días es muy pequeño, ej: < 7
                    if (dias == 0) textoEdad = "Recién nacido";
                }
            }

            // Mostrar en el TextBox (asegúrate que sea ReadOnly)
            txtEdad.Text = textoEdad;
        }
        private void btnGuardarPaciente_Click(object sender, EventArgs e)
        {
            guardarPaciente();
        }      

        #region Metodos
        private void LimpiarCampos()
        {
            txtNombres.Clear();
            txtApellidos.Clear();
            txtCodigoBen.Clear();
            txtEdad.Clear();
            cmbGenero.SelectedIndex = 0;
            dtpFechaNac.Value = DateTime.Today;
            txtObservacion.Clear();
        }
        // Dentro de wAgregarPaciente.cs
        private void guardarPaciente()
        {
            // Validación de campos vacíos (mejorada para quitar txtEdad de aquí)
            if (string.IsNullOrEmpty(txtCodigoBen.Text) ||
                string.IsNullOrEmpty(txtNombres.Text) ||
                // string.IsNullOrEmpty(txtEdad.Text) || // <-- QUITAR ESTA VALIDACIÓN DEL TEXTO VISUAL
                string.IsNullOrEmpty(txtApellidos.Text) ||
                cmbGenero.SelectedItem == null ||
                cmbProyecto.SelectedValue == null) // <-- AÑADIR VALIDACIÓN PARA PROYECTO
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios (Nombres, Apellidos, Código, Género, Proyecto).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validación de Fecha (extra, por si acaso)
            if (dtpFechaNac.Value > DateTime.Today || dtpFechaNac.Value < dtpFechaNac.MinDate)
            {
                MessageBox.Show("La fecha de nacimiento seleccionada no es válida.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // --- Calcular la Edad en AÑOS para la Base de Datos ---
            DateTime fechaNac = dtpFechaNac.Value;
            int edadEnAniosParaDB = DateTime.Today.Year - fechaNac.Year;
            if (DateTime.Today < fechaNac.AddYears(edadEnAniosParaDB))
            {
                edadEnAniosParaDB--;
            }
            // Asegurarse que no sea negativa si justo nació hoy
            if (edadEnAniosParaDB < 0) edadEnAniosParaDB = 0;
            // -----------------------------------------------------

            pacientes nuevoPaciente = new pacientes
            {
                nombres = CapitalizarTexto(txtNombres.Text),
                apellidos = CapitalizarTexto(txtApellidos.Text),
                edad = edadEnAniosParaDB, // <-- USAR EDAD CALCULADA (int)
                genero = cmbGenero.SelectedItem.ToString(),
                codigo_beneficiario = txtCodigoBen.Text,
                fecha_nacimiento = fechaNac, // <-- Guardar la fecha correcta
                                             // Asegúrate que el SelectedValue sea realmente un int, puede necesitar casteo seguro
                id_proyecto = Convert.ToInt32(cmbProyecto.SelectedValue),
                observacion = txtObservacion.Text,
            };

            try
            {
                // Usar la instancia del repositorio MIEMBRO de la clase
                this.pacienteRepository.GuardarPaciente(nuevoPaciente);
                MessageBox.Show("Paciente guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos DESPUÉS de guardar exitosamente (movido desde el Click)
                LimpiarCampos();

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error de PostgreSQL al guardar paciente: {ex.Message} (SQLState: {ex.SqlState})");
                // Mostrar un mensaje más amigable al usuario
                MessageBox.Show($"No se pudo guardar el paciente debido a un error de base de datos.\nDetalle técnico: {ex.SqlState}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // No relanzar (throw) aquí usualmente, manejar el error mostrando mensaje.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general al guardar paciente: {ex.ToString()}");
                MessageBox.Show($"Ocurrió un error inesperado al guardar el paciente:\n{ex.Message}", "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // No relanzar (throw) aquí usualmente.
            }
            // QUITAR LimpiarCampos() de aquí, ya se llama en el Click o después del éxito.
        }
        private string CapitalizarTexto(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return texto;

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(texto.ToLower());
        }
        private void CargarProyectos()
        {
            // Usar el repositorio miembro si ya lo tienes, si no, asegúrate que esté instanciado.
            // Si ProyectoRepository no es miembro, considera hacerlo.
            if (proyectoRepository == null) proyectoRepository = new ProyectoRepository(); // Asegurar instancia

            try
            {
                // *** LLAMAR CON filtro incluirArchivados: false ***
                List<Proyecto> proyectos = proyectoRepository.ObtenerProyectos(incluirArchivados: false);
                // *************************************************

                cmbProyecto.DataSource = null; // Limpiar antes de asignar nuevo origen
                cmbProyecto.DataSource = proyectos;
                cmbProyecto.DisplayMember = "nombre";
                cmbProyecto.ValueMember = "id_proyecto";
                cmbProyecto.SelectedIndex = -1; // Ninguno seleccionado por defecto
                cmbProyecto.Text = "Seleccione un proyecto..."; // Texto placeholder opcional
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar proyectos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Considera deshabilitar el ComboBox o manejar el error de otra forma
            }
        }
        #endregion
        #region Windows Form Designer generated code

        private void cmbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void txtAcodigo_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtAnombreApellido_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtAobservacion_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtEdad_TextChanged(object sender, EventArgs e)
        {

        }
        private void cmbGenero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
