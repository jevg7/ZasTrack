using Microsoft.VisualBasic.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZasTrack.Forms.Examenes.ExamWrite;
using ZasTrack.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZasTrack.Forms.Examenes.Debug
{
    public partial class wProcesarResultados : Form
    {
        private int _idMuestra;
        private int _numeroMuestra;
        private DateTime _fechaRecepcion;
        private string _nombrePaciente;
        private bool _esModoVistaOEditar; // Para saber si es nuevo o ver/editar
        private ExamenRepository _examenRepository;
        public wProcesarResultados(int idMuestra, int numeroMuestra, DateTime fechaRecepcion, string nombrePaciente, bool modoVista = false)
        {
            InitializeComponent();
            _idMuestra = idMuestra;
            // Guarda los datos extras
            _numeroMuestra = numeroMuestra;
            _fechaRecepcion = fechaRecepcion;
            _nombrePaciente = nombrePaciente;
            _esModoVistaOEditar = modoVista; // Guarda el modo

            _examenRepository = new ExamenRepository();
            this.Load += WProcesarResultados_Load;

        }


        // --- Evento Load: Se ejecuta cuando el formulario se carga ---
        private void WProcesarResultados_Load(object sender, EventArgs e)
        {
            // --- Construye el título ---
            CultureInfo culturaEsp = new CultureInfo("es-NI");
            string diaSemana = _fechaRecepcion.ToString("dddd", culturaEsp);
            if (!string.IsNullOrEmpty(diaSemana)) { diaSemana = char.ToUpper(diaSemana[0]) + diaSemana.Substring(1); }
            this.Text = $"Resultados: {diaSemana} #{_numeroMuestra} - {_nombrePaciente}";
            // --- Fin Título ---

            CargarPestanasExamenes(); // Llama a la carga de pestañas
        }

        // --- Método para Cargar Pestañas (A IMPLEMENTAR) ---
        // En wProcesarResultados.cs

        // --- Método para Cargar Pestañas Dinámicamente ---
        private void CargarPestanasExamenes()
        {
            tabControlExamenes.TabPages.Clear(); // Limpia siempre

            // Determina si habilitar controles o no (por defecto, sí para procesar)
            bool habilitarControles = true;

            try
            {
                List<tipo_examen> examenesParaMostrar;

                if (_esModoVistaOEditar) // --- LÓGICA PARA MODO VER/EDITAR ---
                {
                    habilitarControles = false; // Por defecto, en modo vista es solo lectura
                                                // Podrías añadir un botón "Habilitar Edición" si quieres permitirla después

                    // 1. Obtiene la lista de tipos YA PROCESADOS para esta muestra
                    examenesParaMostrar = _examenRepository.ObtenerTiposExamenProcesadosPorMuestra(_idMuestra);

                    if (!examenesParaMostrar.Any())
                    {
                        MessageBox.Show("No se encontraron exámenes procesados para esta muestra.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.BeginInvoke(new Action(() => this.Close()));
                        return;
                    }
                }
                else // --- LÓGICA PARA MODO PROCESAR (PENDIENTES) ---
                {
                    // 1. Obtiene la lista de tipos PENDIENTES (como antes)
                    examenesParaMostrar = _examenRepository.ObtenerTiposExamenPendientesPorMuestra(_idMuestra);

                    if (!examenesParaMostrar.Any())
                    {
                        MessageBox.Show("Esta muestra no tiene exámenes pendientes de procesar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.BeginInvoke(new Action(() => this.Close()));
                        return;
                    }
                }

                // 2. Itera sobre la lista de exámenes (pendientes o procesados)
                foreach (var tipoExamen in examenesParaMostrar)
                {
                    // 3a. Crea la TabPage
                    TabPage nuevaPestana = new TabPage(tipoExamen.nombre);
                    nuevaPestana.Name = "tab" + tipoExamen.nombre.Replace(" ", "");
                    nuevaPestana.Tag = tipoExamen.id_tipo_examen;

                    // 3b. Crea el UserControl correspondiente
                    UserControl controlExamen = null;
                    object datosGuardados = null; // Variable para guardar los datos recuperados

                    switch (tipoExamen.id_tipo_examen)
                    {
                        case 1: // Orina
                            controlExamen = new EGOControl();
                            if (_esModoVistaOEditar) // Si es modo vista, busca los datos
                            {
                                datosGuardados = _examenRepository.ObtenerResultadoOrina(_idMuestra);
                            }
                            break;
                        case 2: // Heces
                            controlExamen = new EGHControl();
                            if (_esModoVistaOEditar)
                            {
                                datosGuardados = _examenRepository.ObtenerResultadoHeces(_idMuestra);
                            }
                            break;
                        case 3: // Sangre
                            controlExamen = new BHCControl();
                            if (_esModoVistaOEditar)
                            {
                                datosGuardados = _examenRepository.ObtenerResultadoSangre(_idMuestra);
                            }
                            break;
                        default:
                            controlExamen = new UserControl(); // Placeholder
                            controlExamen.Controls.Add(new Label { Text = $"Controles para '{tipoExamen.nombre}' no implementados.", Dock = DockStyle.Fill, ForeColor = Color.Red });
                            break;
                    }

                    // 3c. Añade y configura el UserControl
                    if (controlExamen != null)
                    {
                        controlExamen.Dock = DockStyle.Fill;
                        nuevaPestana.Controls.Add(controlExamen);

                        // 3d. Si estamos en modo Ver/Editar Y se encontraron datos, CARGA los datos
                        if (_esModoVistaOEditar && datosGuardados != null)
                        {
                            // Llama al método CargarDatos específico del UserControl
                            if (controlExamen is EGOControl ego && datosGuardados is examen_orina dataOrina) { ego.CargarDatos(dataOrina); }
                            else if (controlExamen is EGHControl egh && datosGuardados is examen_heces dataHeces) { egh.CargarDatos(dataHeces); }
                            else if (controlExamen is BHCControl bhc && datosGuardados is examen_sangre dataSangre) { bhc.CargarDatos(dataSangre); }
                        }
                        // Si es modo nuevo (Procesar), CargarDatos del UserControl pondrá los defaults si lo implementaste así
                        else if (!_esModoVistaOEditar)
                        {
                            // Asegurarse de que se inicialicen los defaults llamando a CargarDatos(null)
                            // si no lo hiciste en el constructor del UserControl
                            if (controlExamen is EGOControl ego) ego.CargarDatos(null);
                            else if (controlExamen is EGHControl egh) egh.CargarDatos(null);
                            else if (controlExamen is BHCControl bhc) bhc.CargarDatos(null);
                        }


                        // 3e. Hacer controles solo lectura si estamos en modo Vista
                        if (!habilitarControles) // Si habilitarControles es false (Modo Vista)
                        {
                            HacerControlesSoloLectura(controlExamen);
                        }
                    }

                    // 3f. Añade la nueva TabPage al TabControl
                    tabControlExamenes.TabPages.Add(nuevaPestana);
                } // Fin foreach

                // 4. Ajustar botones según el modo
                if (!habilitarControles) // Si es modo solo lectura
                {
                    btnGuardarActual.Enabled = false; // Deshabilita guardar
                    btnGuardarActual.Visible = false; // Oculta guardar
                    btnCancelar.Text = "&Cerrar";        // Cambia texto de Cancelar a Cerrar
                }
                else
                {
                    btnGuardarActual.Enabled = true;
                    btnGuardarActual.Visible = true;
                    btnCancelar.Text = "&Cancelar";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las pestañas de exámenes: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.BeginInvoke(new Action(() => this.Close()));
            }
        }
        // --- Fin CargarPestanasExamenes ---

        private void HacerControlesSoloLectura(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox txt)
                {
                    txt.ReadOnly = true;
                    txt.BackColor = SystemColors.Control; // Fondo gris para indicar que no se edita
                }
                else if (c.HasChildren) // Si es un contenedor (Panel, GroupBox), revisa dentro
                {
                    HacerControlesSoloLectura(c);
                }
                // Podrías añadir lógica para deshabilitar ComboBox, CheckBox si los tuvieras
                // else if (c is ComboBox combo) { combo.Enabled = false; }
            }
        }
        // --- Click Guardar (A IMPLEMENTAR LÓGICA DE GUARDADO) ---


        // En wProcesarResultados.cs
        // REEMPLAZA COMPLETAMENTE el método btnGuardarResultados_Click

        private void btnGuardarActual_Click(object sender, EventArgs e)
        {
            // 1. Obtener la pestaña activa y el UserControl asociado
            if (tabControlExamenes.SelectedTab == null)
            {
                MessageBox.Show("No hay ninguna pestaña de examen seleccionada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TabPage tabActiva = tabControlExamenes.SelectedTab;
            UserControl controlExamenActivo = null;

            if (tabActiva.Controls.Count > 0 && tabActiva.Controls[0] is UserControl uc)
            {
                controlExamenActivo = uc;
            }
            else
            {
                MessageBox.Show($"No se encontró el control de examen dentro de la pestaña '{tabActiva.Text}'.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Validar y Obtener Datos del UserControl activo
            object datosObtenidos = null;
            bool validacionOk = false;

            try
            {
                if (controlExamenActivo is EGOControl egoCtrl) { datosObtenidos = egoCtrl.ObtenerDatos(); }
                else if (controlExamenActivo is EGHControl eghCtrl) { datosObtenidos = eghCtrl.ObtenerDatos(); }
                else if (controlExamenActivo is BHCControl bhcCtrl) { datosObtenidos = bhcCtrl.ObtenerDatos(); }
                // Añade 'else if' para otros tipos de UserControl si los tienes

                validacionOk = (datosObtenidos != null);
            }
            catch (NotImplementedException exNIE)
            {
                MessageBox.Show($"La función para obtener/validar datos de '{tabActiva.Text}' aún no está implementada en su UserControl.\nDetalle: {exNIE.Message}", "Función Faltante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                validacionOk = false;
            }
            catch (Exception exVal)
            {
                MessageBox.Show($"Ocurrió un error inesperado durante la validación de '{tabActiva.Text}':\n{exVal.Message}", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                validacionOk = false;
                Console.WriteLine($"ERROR en ObtenerDatos para {tabActiva.Text}: {exVal.ToString()}");
            }

            if (!validacionOk)
            {
                tabControlExamenes.SelectedTab = tabActiva;
                return;
            }

            // 3. Obtener idPaciente
            int? idPacienteNullable = null;
            try
            {
                idPacienteNullable = _examenRepository.ObtenerIdPacientePorMuestra(_idMuestra);
            }
            catch (Exception exRepo)
            {
                MessageBox.Show($"Error al consultar la base de datos para obtener el paciente:\n{exRepo.Message}", "Error de Conexión/Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR en ObtenerIdPacientePorMuestra: {exRepo.ToString()}");
                return;
            }

            if (!idPacienteNullable.HasValue)
            {
                MessageBox.Show("Error Crítico: No se pudo encontrar el ID del paciente asociado a esta muestra en la base de datos.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int idPaciente = idPacienteNullable.Value;

            // 4. Proceder a guardar los datos validados
            bool guardadoExitoso = false;
            string nombreExamenActual = tabActiva.Text;

            btnGuardarActual.Enabled = false;
            if (!_esModoVistaOEditar) btnGuardarResultados.Enabled = false;
            btnCancelar.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (datosObtenidos is examen_orina dataOrina)
                {
                    guardadoExitoso = _examenRepository.GuardarResultadoOrina(dataOrina, _idMuestra, idPaciente);
                }
                else if (datosObtenidos is examen_heces dataHeces)
                {
                    guardadoExitoso = _examenRepository.GuardarResultadoHeces(dataHeces, _idMuestra, idPaciente);
                }
                else if (datosObtenidos is examen_sangre dataSangre)
                {
                    guardadoExitoso = _examenRepository.GuardarResultadoSangre(dataSangre, _idMuestra, idPaciente);
                }
                // Añade 'else if' para otros tipos si los tienes
            }
            catch (Exception exGuardado)
            {
                guardadoExitoso = false;
                Console.WriteLine($"ERROR guardando {nombreExamenActual}: {exGuardado.ToString()}");
                MessageBox.Show($"Ocurrió un error inesperado al intentar guardar los resultados de '{nombreExamenActual}':\n{exGuardado.Message}",
                                "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGuardarActual.Enabled = true;
                if (!_esModoVistaOEditar) { btnGuardarResultados.Enabled = true; }
                btnCancelar.Enabled = true;
                this.Cursor = Cursors.Default;
            }

            // 5. Mostrar Mensaje Final y actualizar color de pestaña
            if (guardadoExitoso)
            {
                MessageBox.Show($"Los resultados del examen de '{nombreExamenActual}' se han guardado correctamente.",
                                "Guardado Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // --- INICIO: Marcar pestaña como guardada ---
                tabActiva.BackColor = Color.LightGreen; // Cambia el color de fondo
                                                        // --- FIN: Marcar pestaña como guardada ---
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Si asignaste DialogResult=Cancel en diseñador, esta línea no es estrictamente necesaria
            // pero no hace daño.
            this.DialogResult = DialogResult.Cancel;
            this.Close(); // Cierra el modal
        }
        private void tabControlExamenes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void btnGuardarResultados_Click(object sender, EventArgs e)
        {
            // --- 1. Obtener idPaciente ---
            int? idPacienteNullable = _examenRepository.ObtenerIdPacientePorMuestra(_idMuestra);
            if (!idPacienteNullable.HasValue)
            {
                MessageBox.Show("Error Crítico: No se pudo obtener la información del paciente.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int idPaciente = idPacienteNullable.Value;

            // --- 2. Validar TODAS las pestañas VISIBLES ---
            bool validacionGeneralOK = true;
            var datosValidosPorPestana = new List<(TabPage Tab, int TipoExamenId, object Data)>();
            List<string> examenesConErrorValidacion = new List<string>();

            // Deshabilita UI mientras valida
            //btnGuardarActual.Enabled = false; // Asegúrate que este botón exista si lo vas a deshabilitar
            btnGuardarResultados.Enabled = false;
            btnCancelar.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            // Primero, valida todo lo visible
            foreach (TabPage tabPage in tabControlExamenes.TabPages)
            {
                if (tabPage.Controls.Count > 0 && tabPage.Controls[0] is UserControl controlExamen)
                {
                    object datosObtenidos = null;
                    int tipoExamenId = (tabPage.Tag is int id) ? id : 0;

                    // Llama a ObtenerDatos (que valida internamente)
                    if (controlExamen is EGOControl egoCtrl) { datosObtenidos = egoCtrl.ObtenerDatos(); }
                    else if (controlExamen is EGHControl eghCtrl) { datosObtenidos = eghCtrl.ObtenerDatos(); }
                    else if (controlExamen is BHCControl bhcCtrl) { datosObtenidos = bhcCtrl.ObtenerDatos(); }
                    // ... otros tipos ...

                    if (datosObtenidos == null) // Validación falló para esta pestaña
                    {
                        validacionGeneralOK = false;
                        examenesConErrorValidacion.Add(tabPage.Text);
                    }
                    else
                    {
                        if (tipoExamenId != 0)
                        {
                            datosValidosPorPestana.Add((tabPage, tipoExamenId, datosObtenidos));
                        }
                    }
                }
            }

            // --- 3. Si ALGUNA validación falló, informar TODOS los errores y detener ---
            if (!validacionGeneralOK)
            {
                // Intenta enfocar la primera pestaña con error
                var primeraTabConError = tabControlExamenes.TabPages.Cast<TabPage>()
                                            .FirstOrDefault(t => examenesConErrorValidacion.Contains(t.Text));
                if (primeraTabConError != null)
                {
                    tabControlExamenes.SelectedTab = primeraTabConError;
                }

                MessageBox.Show($"Corrija los errores de validación en: {string.Join(", ", examenesConErrorValidacion)}.\nNo se guardó ningún resultado.",
                                "Validación Fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Rehabilita UI y sale
                //if (btnGuardarActual != null) btnGuardarActual.Enabled = true; // Si existe
                btnGuardarResultados.Enabled = true; btnCancelar.Enabled = true; this.Cursor = Cursors.Default;
                return;
            }

            // --- 4. Si no hay nada válido para guardar ---
            if (datosValidosPorPestana.Count == 0)
            {
                MessageBox.Show("No se encontraron datos válidos para guardar en las pestañas visibles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Rehabilita UI y sale
                //if (btnGuardarActual != null) btnGuardarActual.Enabled = true; // Si existe
                btnGuardarResultados.Enabled = true; btnCancelar.Enabled = true; this.Cursor = Cursors.Default;
                return;
            }

            // --- 5. Confirmación del Usuario ANTES de guardar ---
            bool guardarConfirmado = true;
            if (datosValidosPorPestana.Count > 1) // Pregunta si hay más de uno
            {
                string nombresExamenesValidos = string.Join(", ", datosValidosPorPestana.Select(d => d.Tab.Text));
                var confirmResult = MessageBox.Show($"Se validaron correctamente los resultados para: {nombresExamenesValidos}.\n\n¿Desea guardar estos {datosValidosPorPestana.Count} resultado(s)?",
                                                    "Confirmar Guardado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                guardarConfirmado = (confirmResult == DialogResult.Yes);
            }
            else // Si solo hay uno, pregunta por ese
            {
                var confirmResult = MessageBox.Show($"Se validaron correctamente los resultados para: {datosValidosPorPestana[0].Tab.Text}.\n\n¿Desea guardar este resultado?",
                                                    "Confirmar Guardado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                guardarConfirmado = (confirmResult == DialogResult.Yes);
            }

            if (!guardarConfirmado)
            {
                // Usuario canceló, rehabilita UI y sale
                //if (btnGuardarActual != null) btnGuardarActual.Enabled = true; // Si existe
                btnGuardarResultados.Enabled = true; btnCancelar.Enabled = true; this.Cursor = Cursors.Default;
                return;
            }

            // --- 6. Si usuario confirma, GUARDAR los datos validados ---
            bool guardadoGeneralExitoso = true;
            int examenesGuardados = 0;
            List<string> examenesConErrorGuardado = new List<string>();

            try // Envuelve TODO el bloque de guardado
            {
                // Itera SOLO sobre los datos que validaron y que el usuario confirmó guardar
                foreach (var itemGuardar in datosValidosPorPestana)
                {
                    bool guardadoOkEsteExamen = false;
                    string nombreExamenActual = itemGuardar.Tab.Text;

                    try // Intenta guardar este examen individualmente
                    {
                        if (itemGuardar.Data is examen_orina dataOrina) { guardadoOkEsteExamen = /*await*/ _examenRepository.GuardarResultadoOrina(dataOrina, _idMuestra, idPaciente); }
                        else if (itemGuardar.Data is examen_heces dataHeces) { guardadoOkEsteExamen = /*await*/ _examenRepository.GuardarResultadoHeces(dataHeces, _idMuestra, idPaciente); }
                        else if (itemGuardar.Data is examen_sangre dataSangre) { guardadoOkEsteExamen = /*await*/ _examenRepository.GuardarResultadoSangre(dataSangre, _idMuestra, idPaciente); }
                        // ... otros tipos ...

                        if (!guardadoOkEsteExamen) // Si el repositorio devolvió false
                        {
                            guardadoGeneralExitoso = false;
                            examenesConErrorGuardado.Add(nombreExamenActual);
                        }
                        else
                        {
                            examenesGuardados++;
                        }
                    }
                    catch (Exception exGuardado) // Captura error de BD al guardar ESTE examen
                    {
                        guardadoGeneralExitoso = false; examenesConErrorGuardado.Add(nombreExamenActual);
                        Console.WriteLine($"ERROR guardando {nombreExamenActual}: {exGuardado.Message}");
                        // Podrías añadir más detalles al log aquí si es necesario
                    }
                } // Fin foreach datosParaGuardar
            }
            catch (Exception exGeneral) // Captura error inesperado fuera del loop de guardado (poco probable)
            {
                guardadoGeneralExitoso = false;
                MessageBox.Show($"Ocurrió un error general inesperado antes de poder guardar todos los resultados:\n{exGeneral.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR GENERAL en btnGuardarResultados_Click: {exGeneral.ToString()}");
            }
            finally
            {
                // Rehabilita UI INDEPENDIENTEMENTE del resultado
                //if (btnGuardarActual != null) btnGuardarActual.Enabled = true; // Si existe
                // Solo rehabilita si no estamos en modo vista (donde ya estaba deshabilitado)
                if (!_esModoVistaOEditar) { btnGuardarResultados.Enabled = true; }
                btnCancelar.Enabled = true;
                this.Cursor = Cursors.Default;
            }

            // --- 7. Mensajes Finales y Cierre ---
            if (!guardadoGeneralExitoso) // Si hubo error al guardar AL MENOS UNO de los confirmados
            {
                MessageBox.Show($"Se intentó guardar los exámenes validados. Ocurrió un error al guardar: {string.Join(", ", examenesConErrorGuardado)}.\n{(examenesGuardados > 0 ? $"Los otros {examenesGuardados} resultados válidos sí pudieron guardarse." : "Ningún resultado fue guardado.")}",
                                "Error Parcial de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // NO CERRAMOS el modal para que el usuario vea qué falló o intente de nuevo.
            }
            else if (examenesGuardados > 0) // Si no hubo errores Y se guardó al menos uno (todos los confirmados)
            {
                MessageBox.Show($"Se guardaron exitosamente los resultados de {examenesGuardados} examen(es).",
                                "Guardado Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Indica éxito para refrescar la lista anterior
                this.Close(); // Cierra el formulario
            }
            else // No hubo errores, pero no se guardó nada (quizás el usuario canceló?)
            {
                // Este caso es cubierto por la confirmación previa, pero por si acaso.
                MessageBox.Show("No se guardó ningún resultado en esta operación.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // No cerramos
            }
        }
    }

} // Fin clase wProcesarResultados

  

