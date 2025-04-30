using System;
using System.Drawing; // Necesario para Point, Color, etc. si se usaran (FontStyle no es de aquí)
using System.Windows.Forms; // Namespace principal de WinForms
// using ZasTrack.Models; // No parece usarse directamente aquí
// using ZasTrack.Forms; // No parece usarse directamente aquí
using ZasTrack.Repositories; // Necesario para ProyectoRepository si se usara aquí (actualmente no)

// Asegúrate que el namespace sea el correcto para TU estructura
namespace ZasTrack.Forms.wProyectos
{
    public partial class wProyectos : Form
    {
        // --- Campos ---
        // Ya no necesitamos el repositorio aquí si solo navegamos
        // private ProyectoRepository proyectoRepository;
        private Form? activeProyectosForm = null; // Guarda referencia al form hijo activo EN ESTE PANEL

        public wProyectos()
        {
            InitializeComponent();
            // Ya no necesitamos inicializar el repositorio aquí
            // proyectoRepository = new ProyectoRepository();
        }

        private void wProyectos_Load(object sender, EventArgs e)
        {
            // Carga el formulario de ver proyectos por defecto al iniciar
            // Usamos el nuevo método ShowChildForm
            ShowChildForm(new ZasTrack.Forms.wProyectos.wVerProyecto()); // Ajusta namespace si es diferente
        }

      
        private void ShowChildForm(Form childForm)
        {
            // 1. No hacer nada si se intenta abrir el mismo tipo de form que ya está activo
            if (activeProyectosForm?.GetType() == childForm.GetType())
            {
                childForm.Dispose(); // Liberar la instancia recién creada que no se usará
                return;
            }

            // 2. Cerrar y liberar el formulario activo anterior (si existe)
            if (activeProyectosForm != null)
            {
                activeProyectosForm.Close(); // Esto debería llamar a Dispose internamente
                // activeProyectosForm.Dispose(); // Alternativa más directa si Close no basta
            }

            // 3. Configurar y mostrar el nuevo formulario
            try
            {
                activeProyectosForm = childForm; // Guardar referencia
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                // 'pnlContenedor' es el nombre del Panel dentro de wProyectos (verifica en tu Designer)
                this.pnlContenedor.Controls.Add(childForm);
                this.pnlContenedor.Tag = childForm; // Tag es opcional
                childForm.BringToFront();
                childForm.Show();
                Console.WriteLine($"DEBUG: Cargado {childForm.GetType().Name} en pnlContenedor de wProyectos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar abrir la sección:\n{ex.Message}",
                                "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR ShowChildForm (wProyectos): {ex}");
                if (childForm != null) { childForm.Dispose(); } // Liberar si falla
                activeProyectosForm = null; // Resetear
            }
        }

        // --- Eventos del MenuStrip ---
        private void agregarProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Llama al nuevo método con el formulario correspondiente
            ShowChildForm(new ZasTrack.Forms.wProyectos.wAñadirProyecto()); // Ajusta namespace si es diferente
        }

        private void verProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Llama al nuevo método con el formulario correspondiente
            ShowChildForm(new ZasTrack.Forms.wProyectos.wVerProyecto()); // Ajusta namespace si es diferente
        }

        // Los handlers para editar/eliminar (aún vacíos)
        private void editarProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Implementar lógica para abrir wEditarProyecto (probablemente necesite el ID del proyecto seleccionado en wVerProyecto)
            MessageBox.Show("Funcionalidad Editar Proyecto - Pendiente de implementar.");
        }

        private void eliminarProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Implementar lógica para eliminar proyecto (necesita ID, confirmación y llamada al repo)
            MessageBox.Show("Funcionalidad Eliminar Proyecto - Pendiente de implementar.");
        }
    } // Fin clase wProyectos
} // Fin namespace