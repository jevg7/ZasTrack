using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using ZasTrack.Models;
using ZasTrack.Repositories;

namespace ZasTrack.Forms
{
    public partial class wAñadirProyecto : Form
    {
        private ProyectoRepository proyectoRepository;

        public wAñadirProyecto()
        {
            InitializeComponent();
            proyectoRepository = new ProyectoRepository();
          ;
        }


        // --- Evento Load (YA NO PONE MinDate) ---
        private void wAñadirProyecto_Load(object sender, EventArgs e)
        {
           
            dtpFechaInicio.Value = DateTime.Today;
            Console.WriteLine("DEBUG: wAñadirProyecto cargado. Fecha inicio permite cualquier fecha.");
        }
        private void btnGuardarProyecto_Click(object sender, EventArgs e)
        {
            // 1. Validar Campos Vacíos
            string nombreProyecto = txtNombreProyecto.Text.Trim();
            string codigoProyecto = txtCodigo.Text.Trim(); // Asume que el TextBox se llama txtCodigo

            if (string.IsNullOrEmpty(nombreProyecto))
            {
                MessageBox.Show("El nombre del proyecto es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreProyecto.Focus(); return; // Detener y enfocar
            }
            if (string.IsNullOrEmpty(codigoProyecto))
            {
                MessageBox.Show("El código del proyecto es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus(); return; // Detener y enfocar
            }

         

            // 3. Validar Nombre Duplicado (llamando al repositorio)
            try
            {
                if (ExisteNombreProyecto(nombreProyecto))
                {
                    MessageBox.Show($"Ya existe un proyecto con el nombre '{nombreProyecto}'.\nPor favor, elija otro nombre.", "Nombre Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombreProyecto.Focus(); return; // Detener
                }
            }
            catch (Exception exValNombre)
            {
                MessageBox.Show($"Error al validar el nombre del proyecto:\n{exValNombre.Message}", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Detener si falla la validación
            }


            // 4. Validar Código Duplicado (llamando al repositorio)
            try
            {
                if (ExisteCodigoProyecto(codigoProyecto))
                {
                    MessageBox.Show($"Ya existe un proyecto con el código '{codigoProyecto}'.\nPor favor, elija otro código.", "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodigo.Focus(); return; // Detener
                }
            }
            catch (Exception exValCodigo)
            {
                MessageBox.Show($"Error al validar el código del proyecto:\n{exValCodigo.Message}", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Detener si falla la validación
            }


            // --- Si todas las validaciones pasan, proceder a guardar ---
            var proyecto = new Proyecto
            {
                // Usar valores limpios (Trim)
                nombre = nombreProyecto,
                fecha_inicio = dtpFechaInicio.Value.Date, // Guardar solo la fecha
                codigo = codigoProyecto,
                // IsArchived será false por defecto al crear uno nuevo
            };

            try
            {
                // Usar la instancia miembro del repositorio
                proyectoRepository.GuardarProyecto(proyecto);
                MessageBox.Show("Proyecto guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK; // Indicar éxito al cerrar
                this.Close();
            }
            catch (NpgsqlException exDb) // Capturar error específico de DB
            {
                Console.WriteLine($"Error de PostgreSQL al guardar: {exDb.Message} (SQLState: {exDb.SqlState})");
                // Mostrar mensaje más específico si es posible (ej. violación de constraint UNIQUE si falló la validación previa)
                MessageBox.Show($"No se pudo guardar el proyecto debido a un error de base de datos.\nError: {exDb.Message}", "Error Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // NO relanzar aquí normalmente en un evento de UI
            }
            catch (Exception ex) // Capturar cualquier otro error
            {
                Console.WriteLine($"Error general al guardar proyecto: {ex.ToString()}");
                MessageBox.Show($"Ocurrió un error inesperado al guardar:\n{ex.Message}", "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // NO relanzar aquí normalmente
            }
        }
        public bool ExisteNombreProyecto(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return false; // Nombre vacío no "existe"

            // Usamos LOWER() en ambos lados para comparación insensible a mayúsculas/minúsculas
            string query = "SELECT COUNT(*) FROM proyecto WHERE LOWER(nombre) = LOWER(@nombre)";
            long count = 0;
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre.Trim());
                        count = (long)cmd.ExecuteScalar(); // Obtiene el resultado del COUNT
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar nombre de proyecto: {ex.Message}");
                // Considera lanzar una excepción aquí si fallar la validación es crítico
                // throw new Exception($"Error al verificar nombre '{nombre}'.", ex);
                return true; // Por seguridad, si no podemos verificar, asumimos que podría existir
            }
            return count > 0; // Devuelve true si el contador es mayor a 0 (ya existe)
        }

        // Verifica si ya existe un proyecto con ese código (ignorando mayús/minús)
        // Busca en TODOS los proyectos, incluyendo archivados.
        public bool ExisteCodigoProyecto(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo)) return false; // Código vacío no "existe"

            string query = "SELECT COUNT(*) FROM proyecto WHERE LOWER(codigo) = LOWER(@codigo)";
            long count = 0;
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", codigo.Trim());
                        count = (long)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar código de proyecto: {ex.Message}");
                // throw new Exception($"Error al verificar código '{codigo}'.", ex);
                return true; // Por seguridad, si no podemos verificar, asumimos que podría existir
            }
            return count > 0; // Devuelve true si ya existe
        }
        #region windows forms generated
      
      
      
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCodigoProyecto_Click(object sender, EventArgs e)
        {

        }

        private void dtpFechaFin_ValueChanged(object sender, EventArgs e)
        {

        }
        #endregion


    }
}
