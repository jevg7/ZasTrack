using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ZasTrack.Models;
using ZasTrack.Repositories;

// --- Cambiar Namespace si es necesario ---
namespace ZasTrack.Forms.Estudiantes // O Forms.Estudiantes si lo prefieres mantener así
{
    public partial class wEditarEliminarPaciente : Form
    {
        // --- Repositorios como miembros ---
        private PacienteRepository pacienteRepository;
        private ProyectoRepository proyectoRepository;

        // --- Variables para el paciente actual ---
        private int pacienteIdActual; // Guarda el ID del paciente a editar/eliminar
        private pacientes pacienteActual = null; // Guarda el objeto paciente cargado

        // --- Constructor para Editar/Eliminar (Recibe ID) ---
        public wEditarEliminarPaciente(int idPaciente)
        {
            InitializeComponent();
            pacienteRepository = new PacienteRepository();
            proyectoRepository = new ProyectoRepository(); // Inicializar repo de proyectos también

            this.pacienteIdActual = idPaciente; // Guardar el ID

            // Asignar handlers a eventos principales
            this.Load += wEditarEliminarPaciente_Load;
            btnEditar.Click += btnEditar_Click;     // Botón para HABILITAR edición
            btnGuardar.Click += btnGuardar_Click;   // Botón para GUARDAR CAMBIOS
            btnCancelar.Click += btnCancelar_Click; // Botón para CANCELAR edición
            btnEliminar.Click += btnEliminar_Click; // Botón para ELIMINAR paciente
            dtpFechaNac.ValueChanged += dtpFechaNac_ValueChanged; // Para actualizar edad visual
        }

        // --- Evento Load: Carga filtros y datos del paciente ---
        private void wEditarEliminarPaciente_Load(object sender, EventArgs e)
        {
            // Configurar límites de fecha (igual que en Agregar)
            dtpFechaNac.MaxDate = DateTime.Today;
            dtpFechaNac.MinDate = new DateTime(1900, 1, 1);

            // Cargar ComboBox de Género
            cmbGenero.Items.Clear(); // Limpiar por si acaso
            cmbGenero.Items.Add("Masculino");
            cmbGenero.Items.Add("Femenino");

            // Cargar ComboBox de Proyectos (Solo Activos)
            CargarProyectos();

            // Cargar los datos del paciente específico
            CargarDatosPaciente();

            // Configurar estado inicial (Modo Vista - no editable)
            PonerModoVista();
        }

        // --- Carga los Proyectos ACTIVOS en el ComboBox ---
        private void CargarProyectos()
        {
            if (proyectoRepository == null) proyectoRepository = new ProyectoRepository();
            try
            {
                List<Proyecto> proyectosActivos = proyectoRepository.ObtenerProyectos(incluirArchivados: false);
                cmbProyecto.DataSource = null;
                cmbProyecto.DataSource = proyectosActivos;
                cmbProyecto.DisplayMember = "nombre";
                cmbProyecto.ValueMember = "id_proyecto";
                cmbProyecto.SelectedIndex = -1; // Deseleccionar inicialmente
            }
            catch (Exception ex) { MessageBox.Show($"Error cargando proyectos: {ex.Message}"); cmbProyecto.Enabled = false; }
        }

        // --- Carga los datos del paciente por su ID ---
        private void CargarDatosPaciente()
        {
            if (pacienteIdActual <= 0) return; // No hacer nada si no hay ID válido

            try
            {
                // --- ¡NECESITAS ESTE MÉTODO EN PacienteRepository! ---
                pacienteActual = pacienteRepository.ObtenerPacientePorId(pacienteIdActual);
                // -----------------------------------------------------

                if (pacienteActual != null)
                {
                    // Llenar los controles con los datos del paciente
                    this.Text = $"Editar/Eliminar: {pacienteActual.nombres} {pacienteActual.apellidos}"; // Título ventana
                    txtNombres.Text = pacienteActual.nombres;
                    txtApellidos.Text = pacienteActual.apellidos;
                    txtCodigoBen.Text = pacienteActual.codigo_beneficiario;
                    dtpFechaNac.Value = pacienteActual.fecha_nacimiento; // Carga la fecha
                                                                         // Seleccionar Género en ComboBox (si está visible) o mostrar en TextBox
                    cmbGenero.SelectedItem = pacienteActual.genero;
                    txtGenero.Text = pacienteActual.genero; // Mostrar en el TextBox de solo lectura inicial
                                                            // Seleccionar Proyecto en ComboBox
                    cmbProyecto.SelectedValue = pacienteActual.id_proyecto;
                    txtObservacion.Text = pacienteActual.observacion;

                    // Calcular y mostrar edad formateada (usando el evento ValueChanged)
                    dtpFechaNac_ValueChanged(dtpFechaNac, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show($"No se encontró el paciente con ID {pacienteIdActual}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Deshabilitar botones principales si no se carga paciente
                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos del paciente:\n{ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }

        // --- Calcula y Muestra Edad (Años y Meses) ---
        // --- REEMPLAZA TU VERSIÓN ANTERIOR ---
        private void dtpFechaNac_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaNacimiento = dtpFechaNac.Value;
            DateTime hoy = DateTime.Today;
            string textoEdad = ""; // Variable local para el texto

            if (fechaNacimiento > hoy || fechaNacimiento < dtpFechaNac.MinDate)
            {
                textoEdad = "Fecha Inválida";
            }
            else
            {
                int anios = hoy.Year - fechaNacimiento.Year;
                if (hoy < fechaNacimiento.AddYears(anios)) anios--;
                if (anios < 0) anios = 0;

                DateTime ultimoCumple = fechaNacimiento.AddYears(anios);
                int meses = 0;
                while (ultimoCumple.AddMonths(meses + 1) <= hoy) meses++;

                int dias = (hoy - ultimoCumple.AddMonths(meses)).Days;

                if (anios > 0)
                {
                    textoEdad = $"{anios} año{(anios != 1 ? "s" : "")}";
                    if (meses > 0) textoEdad += $" ({meses} mes{(meses != 1 ? "es" : "")})";
                }
                else if (meses > 0)
                {
                    textoEdad = $"{meses} mes{(meses != 1 ? "es" : "")}";
                    if (dias > 0) textoEdad += $" ({dias} día{(dias != 1 ? "s" : "")})";
                }
                else
                {
                    textoEdad = $"{dias} día{(dias != 1 ? "s" : "")}";
                    if (dias == 0) textoEdad = "Recién nacido";
                }
            }
            // Asignar a un Label (recomendado) o a txtEdad si es ReadOnly
            lblEdadCalculada.Text = textoEdad; // Asegúrate que lblEdadCalculada exista
                                               // O, si insistes en usar txtEdad, asegúrate que sea ReadOnly=true en el diseñador
                                               // txtEdad.Text = textoEdad;
        }

        // --- Botón Editar: Habilita los campos para edición ---
        private void btnEditar_Click(object sender, EventArgs e)
        {
            PonerModoEdicion();
        }

        // --- Botón Cancelar: Deshabilita campos y restaura datos originales ---
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            PonerModoVista();
            // Recargar datos originales por si se modificaron campos
            CargarDatosPaciente();
        }

        // --- Botón Guardar: Guarda los cambios ---
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarCambiosPaciente();
        }

        // --- Botón Eliminar: Inicia el proceso de eliminación ---
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (pacienteActual == null) return; // No hay paciente cargado

            // Confirmación MUY clara
            var confirm = MessageBox.Show($"¿Está COMPLETAMENTE SEGURO de que desea eliminar PERMANENTEMENTE al paciente '{pacienteActual.nombres} {pacienteActual.apellidos}' (ID: {pacienteActual.id_paciente})?\n\n¡ESTA ACCIÓN NO SE PUEDE DESHACER!",
                                           "Confirmar Eliminación PERMANENTE",
                                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // --- ¡NECESITAS ESTE MÉTODO EN PacienteRepository! ---
                    bool exito = pacienteRepository.EliminarPaciente(pacienteActual.id_paciente);
                    // ---------------------------------------------------

                    if (exito)
                    {
                        MessageBox.Show("Paciente eliminado correctamente.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.Yes; // Indicar que algo cambió (se eliminó)
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el paciente. Verifique si tiene datos asociados (muestras, etc.) o inténtelo de nuevo.", "Error al Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception exDel)
                {
                    // Capturar error específico de FK si es posible (ej. si tiene muestras)
                    if (exDel is NpgsqlException npgEx && npgEx.SqlState == "23503") // Código común para Foreign Key Violation
                    {
                        MessageBox.Show("No se puede eliminar este paciente porque tiene muestras u otros datos asociados.", "Error de Dependencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Error inesperado al eliminar el paciente:\n{exDel.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Console.WriteLine($"ERROR Eliminando Paciente ID {pacienteActual.id_paciente}: {exDel.ToString()}");
                }
            }
        }


        // --- Método para Guardar Cambios (Actualizar) ---
        // --- REEMPLAZA tu método editarPaciente() ---
        private void GuardarCambiosPaciente()
        {
            // Validar campos obligatorios (similar a Agregar, pero sin validar código duplicado si no se puede cambiar)
            string nombre = txtNombres.Text.Trim();
            string apellido = txtApellidos.Text.Trim();
            string codigoBen = txtCodigoBen.Text.Trim(); // ¿Se puede editar el código? Asumo que sí por ahora.

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(codigoBen) || cmbGenero.SelectedItem == null || cmbProyecto.SelectedValue == null)
            {
                MessageBox.Show("Complete todos los campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (dtpFechaNac.Value > DateTime.Today || dtpFechaNac.Value < dtpFechaNac.MinDate)
            {
                MessageBox.Show("La fecha de nacimiento no es válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            // TODO: Validar si el CÓDIGO BENEFICIARIO fue cambiado Y si el NUEVO código ya existe para OTRO paciente.
            // Esto requiere una modificación en ExisteCodigoBeneficiario o un método nuevo.
            // if (codigoBen != pacienteActual.codigo_beneficiario && pacienteRepository.ExisteCodigoBeneficiario(codigoBen)) {
            //     MessageBox.Show($"El nuevo código '{codigoBen}' ya está asignado a otro paciente.", "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            // }


            // Calcular edad en años para DB (si la guardas)
            DateTime fechaNac = dtpFechaNac.Value;
            int edadDb = DateTime.Today.Year - fechaNac.Year;
            if (DateTime.Today < fechaNac.AddYears(edadDb)) edadDb--;
            if (edadDb < 0) edadDb = 0;

            // Crear objeto con datos actualizados
            pacientes pacienteEditado = new pacientes
            {
                id_paciente = this.pacienteIdActual, // <<< Usar el ID guardado
                nombres = CapitalizarTexto(nombre),
                apellidos = CapitalizarTexto(apellido),
                edad = edadDb, // Edad calculada en años
                genero = cmbGenero.SelectedItem.ToString(),
                codigo_beneficiario = codigoBen,
                fecha_nacimiento = fechaNac,
                id_proyecto = Convert.ToInt32(cmbProyecto.SelectedValue), // Asignar proyecto
                observacion = txtObservacion.Text.Trim()
            };

            try
            {
                // --- ¡NECESITAS ESTE MÉTODO EN PacienteRepository! ---
                bool exito = pacienteRepository.ActualizarPaciente(pacienteEditado);
                // ---------------------------------------------------

                if (exito)
                {
                    MessageBox.Show("Paciente actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Indicar que se guardó algo
                    PonerModoVista(); // Volver a modo vista después de guardar
                    CargarDatosPaciente(); // Recargar por si acaso
                                           // Opcional: Cerrar el formulario this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el paciente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el paciente:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR ActualizarPaciente ID {this.pacienteIdActual}: {ex.ToString()}");
            }
        }

        // --- Métodos para cambiar entre modo Vista y Edición ---
        private void PonerModoEdicion()
        {
            this.Text = $"Editando: {pacienteActual?.nombres} {pacienteActual?.apellidos}";
            btnEditar.Visible = false; // Ocultar Editar
            btnEliminar.Visible = false; // Ocultar Eliminar mientras edita
            btnGuardar.Visible = true;   // Mostrar Guardar
            btnCancelar.Visible = true;  // Mostrar Cancelar

            // Habilitar campos editables
            txtNombres.ReadOnly = false; // Asumiendo que son ReadOnly por defecto
            txtApellidos.ReadOnly = false;
            txtCodigoBen.ReadOnly = false; // ¿Permitir editar código? Cuidado con duplicados
            dtpFechaNac.Enabled = true;
            cmbGenero.Visible = true; // Mostrar ComboBox
            txtGenero.Visible = false; // Ocultar TextBox de solo lectura
            cmbProyecto.Enabled = true;
            txtObservacion.ReadOnly = false;

            txtNombres.Focus(); // Poner foco en el primer campo
        }

        private void PonerModoVista()
        {
            this.Text = $"Ver: {pacienteActual?.nombres} {pacienteActual?.apellidos}";
            btnEditar.Visible = true;   // Mostrar Editar
            btnEliminar.Visible = true;  // Mostrar Eliminar
            btnGuardar.Visible = false;  // Ocultar Guardar
            btnCancelar.Visible = false; // Ocultar Cancelar

            // Deshabilitar/Poner ReadOnly campos
            txtNombres.ReadOnly = true;
            txtApellidos.ReadOnly = true;
            txtCodigoBen.ReadOnly = true;
            dtpFechaNac.Enabled = false;
            cmbGenero.Visible = false; // Ocultar ComboBox
            txtGenero.Visible = true; // Mostrar TextBox (que se llenó en CargarDatosPaciente)
            cmbProyecto.Enabled = false;
            txtObservacion.ReadOnly = true;
            lblEdadCalculada.Visible = true; // Asegurar que label de edad sea visible
        }


        // --- Método auxiliar ---
        private string CapitalizarTexto(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return texto;
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(texto.ToLower());
        }

        #region Windows Form Designer generated code
        private void btnBuscar_Click(object sender, EventArgs e)
        {
        }
        
        private void wEditarEliminarEstudiante_Load(object sender, EventArgs e)
        {
        }
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
     
        private void lblNombres_Click(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void cmbGenero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

    } // Fin clase wEditarEliminarPaciente
}