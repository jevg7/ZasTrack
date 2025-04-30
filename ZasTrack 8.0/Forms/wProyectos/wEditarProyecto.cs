using System;
using System.Windows.Forms;
using Npgsql;
using ZasTrack.Models; // Asegúrate que el namespace de Proyecto sea accesible
using ZasTrack.Repositories; // Necesitaremos esto LUEGO para guardar

namespace ZasTrack.Forms.wProyectos // O tu namespace correcto
{
    public partial class wEditarProyecto : Form
    {
        // Campo privado para guardar el proyecto que estamos editando
        private readonly Proyecto _proyectoActual;
        // Repositorio (lo necesitaremos para guardar y validar después)
        private readonly ProyectoRepository _proyectoRepository;

        // --- CONSTRUCTOR MODIFICADO ---
        // Ahora acepta un objeto Proyecto
        public wEditarProyecto(Proyecto proyectoAEditar)
        {
            InitializeComponent();

            // Guardamos una referencia al proyecto que nos pasaron
            // Usamos 'ArgumentNullException.ThrowIfNull' para asegurar que no sea null (buena práctica)
            ArgumentNullException.ThrowIfNull(proyectoAEditar, nameof(proyectoAEditar));
            _proyectoActual = proyectoAEditar;

            // Inicializamos el repositorio
            _proyectoRepository = new ProyectoRepository();
        }

        // --- EVENTO LOAD: Llenar los campos con datos existentes ---
        private void wEditarProyecto_Load(object sender, EventArgs e)
        {
            if (_proyectoActual != null)
            {
                this.Text = $"Editar Proyecto: {_proyectoActual.nombre}"; // Actualiza título de ventana
                txtNombreProyecto.Text = _proyectoActual.nombre;
                txtCodigo.Text = _proyectoActual.codigo;
                dtpFechaInicio.Value = _proyectoActual.fecha_inicio.Date;
            }
            else
            {
                // Esto no debería pasar si se llama correctamente, pero por seguridad:
                MessageBox.Show("Error: No se recibió información del proyecto a editar.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); // Cerrar si no hay proyecto
            }
        }

        // En wEditarProyecto.cs

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            // 1. Validar Campos Vacíos (igual que en añadir)
            string nombreProyecto = txtNombreProyecto.Text.Trim();
            string codigoProyecto = txtCodigo.Text.Trim(); // Asume que el TextBox se llama txtCodigo

            if (string.IsNullOrEmpty(nombreProyecto))
            {
                MessageBox.Show("El nombre del proyecto es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreProyecto.Focus(); return;
            }
            if (string.IsNullOrEmpty(codigoProyecto))
            {
                MessageBox.Show("El código del proyecto es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus(); return;
            }

            // --- Validación de Duplicados (MODIFICADA para Editar) ---
            // Pasamos el ID del proyecto actual para excluirlo de la verificación

            // 2. Validar Nombre Duplicado (excluyendo el actual)
            try
            {
                // Llama al método del repositorio pasando el ID actual para excluirlo
                if (_proyectoRepository.NombreExiste(nombreProyecto, _proyectoActual.id_proyecto))
                {
                    MessageBox.Show($"Ya existe OTRO proyecto con el nombre '{nombreProyecto}'.\nPor favor, elija otro nombre.", "Nombre Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombreProyecto.Focus(); return;
                }
            }
            catch (Exception exValNombre)
            {
                MessageBox.Show($"Error al validar el nombre del proyecto:\n{exValNombre.Message}", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Validar Código Duplicado (excluyendo el actual)
            try
            {
                // Llama al método del repositorio pasando el ID actual para excluirlo
                if (_proyectoRepository.CodigoExiste(codigoProyecto, _proyectoActual.id_proyecto))
                {
                    MessageBox.Show($"Ya existe OTRO proyecto con el código '{codigoProyecto}'.\nPor favor, elija otro código.", "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodigo.Focus(); return;
                }
            }
            catch (Exception exValCodigo)
            {
                MessageBox.Show($"Error al validar el código del proyecto:\n{exValCodigo.Message}", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // --- Si todas las validaciones pasan, actualizar y guardar ---

            // 4. Actualizar el objeto _proyectoActual con los nuevos valores
            _proyectoActual.nombre = nombreProyecto;
            _proyectoActual.codigo = codigoProyecto;
            _proyectoActual.fecha_inicio = dtpFechaInicio.Value.Date; // Guardar solo fecha

            // 5. Llamar al repositorio para guardar los cambios
            try
            {
                bool exito = _proyectoRepository.ActualizarProyecto(_proyectoActual); // Llamar al nuevo método

                if (exito)
                {
                    MessageBox.Show("Proyecto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Indicar éxito al cerrar
                    this.Close(); // Cerrar el formulario
                }
                else
                {
                    // Esto podría pasar si el UPDATE no afectó filas (ej: el ID no existía)
                    MessageBox.Show("No se pudo actualizar el proyecto. Es posible que haya sido eliminado.", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (NpgsqlException exDb)
            {
                Console.WriteLine($"Error de PostgreSQL al actualizar: {exDb.Message} (SQLState: {exDb.SqlState})");
                MessageBox.Show($"No se pudo actualizar el proyecto debido a un error de base de datos.\nError: {exDb.Message}", "Error Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general al actualizar proyecto: {ex.ToString()}");
                MessageBox.Show($"Ocurrió un error inesperado al actualizar:\n{ex.Message}", "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- BOTÓN CANCELAR ---
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Simplemente cierra el formulario sin guardar nada
            this.DialogResult = DialogResult.Cancel; // Opcional, útil si se abre con ShowDialog()
            this.Close();
        }

        // --- BORRAR MANEJADORES VACÍOS ---
        // Borra los métodos vacíos como txtCodigo_TextChanged, lblCodigoProyecto_Click, etc.
        // que el diseñador pudo haber creado si hiciste doble clic por error.
        // Recuerda desconectarlos primero en el diseñador (Propiedades -> Eventos ⚡).

    } // Fin clase wEditarProyecto
} // Fin namespace