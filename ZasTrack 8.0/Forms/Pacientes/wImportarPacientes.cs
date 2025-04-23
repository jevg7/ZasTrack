using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader; // NuGet: ExcelDataReader, ExcelDataReader.DataSet
using Npgsql; // Para NpgsqlException si la usas en el repo
using ZasTrack.Models;
using ZasTrack.Repositories;

// Necesario para ExcelDataReader en .NET Core / .NET 5+
// Añadir paquete NuGet: System.Text.Encoding.CodePages
// Y añadir esta línea UNA SOLA VEZ al inicio de tu aplicación (ej. en Program.cs o constructor principal)
// System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
// Si ya la tienes en otro lado, no la repitas. La pongo en ProcesarArchivoExcel por si acaso.

namespace ZasTrack.Forms.Pacientes
{
    public partial class wImportarPacientes : Form
    {
        // --- Repositorios ---
        private Repositories.PacienteRepository pacienteRepository;
        private Repositories.ProyectoRepository proyectoRepository;

        // --- Constructor ---
        public wImportarPacientes()
        {
            InitializeComponent();
            // Inicializar repositorios
            pacienteRepository = new PacienteRepository();
            proyectoRepository = new ProyectoRepository();

            // Conectar Handlers (si no se hizo en diseñador)
            this.Load += wImportarPacientes_Load;
            btnSeleccionarArchivo.Click += btnSeleccionarArchivo_Click;
            btnImportar.Click += btnImportar_Click;
            cmbHojas.SelectedIndexChanged += cmbHojas_SelectedIndexChanged;
            cmbProyecto.SelectedIndexChanged += cmbProyecto_SelectedIndexChanged;
        }

        // --- Evento Load ---
        private void wImportarPacientes_Load(object sender, EventArgs e)
        {
            CargarProyectos(); // Cargar proyectos activos al inicio

            // Estado inicial UI
            progressBarImportacion.Visible = false;
            lblProgreso.Text = "";
            lblProgreso.Visible = true;
            txtRutaArchivo.Text = "";
            cmbHojas.DataSource = null;
            cmbHojas.Enabled = false; // Habilitar solo después de seleccionar archivo
            chkTieneEncabezado.Checked = true; // Marcar por defecto
            dgvResultados.DataSource = null;

            // Habilitar/Deshabilitar botones basado en estado inicial
            ActualizarEstadoBotonImportar(); // Esto lo deshabilitará inicialmente
        }

        // --- Cargar Proyectos Activos ---
        private void CargarProyectos()
        {
            // Usar el repositorio miembro
            if (proyectoRepository == null) proyectoRepository = new ProyectoRepository();
            List<Proyecto> proyectosActivos = null;
            try
            {
                Console.WriteLine("DEBUG: [CargarProyectos] Iniciando...");
                proyectosActivos = proyectoRepository.ObtenerProyectos(incluirArchivados: false);
                Console.WriteLine($"DEBUG: [CargarProyectos] ObtenerProyectos devolvió: {proyectosActivos?.Count ?? -1} proyectos.");

                cmbProyecto.DataSource = null;
                cmbProyecto.Items.Clear();

                if (proyectosActivos != null && proyectosActivos.Any())
                {
                    Console.WriteLine($"DEBUG: [CargarProyectos] {proyectosActivos.Count} proyectos encontrados. Asignando...");
                    cmbProyecto.DataSource = proyectosActivos; // Asignar PRIMERO
                    cmbProyecto.DisplayMember = "nombre";
                    cmbProyecto.ValueMember = "id_proyecto";
                    Console.WriteLine($"DEBUG: [CargarProyectos] DataSource asignado. Items.Count = {cmbProyecto.Items.Count}");
                    cmbProyecto.SelectedIndex = -1;
                    cmbProyecto.Text = "Seleccione proyecto destino...";
                    cmbProyecto.Enabled = true;
                    btnSeleccionarArchivo.Enabled = true; // Habilitar selección de archivo
                }
                else
                {
                    Console.WriteLine("DEBUG: [CargarProyectos] No se encontraron proyectos activos.");
                    cmbProyecto.Text = "No hay proyectos activos";
                    cmbProyecto.Enabled = false;
                    btnSeleccionarArchivo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: [CargarProyectos] EXCEPCIÓN: {ex.ToString()}");
                MessageBox.Show($"Error crítico al cargar proyectos activos:\n{ex.Message}", "Error Carga Proyectos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbProyecto.Enabled = false;
                btnSeleccionarArchivo.Enabled = false;
            }
            finally
            {
                ActualizarEstadoBotonImportar(); // Actualizar estado del botón importar
                Console.WriteLine("DEBUG: [CargarProyectos] Finalizado.");
            }
        }

        // --- Seleccionar Archivo Excel ---
        private void btnSeleccionarArchivo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Seleccionar archivo Excel con pacientes";
                openFileDialog.Filter = "Archivos Excel (*.xlsx;*.xls)|*.xlsx;*.xls"; // Solo Excel
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtRutaArchivo.Text = openFileDialog.FileName;
                    CargarNombresDeHojas(openFileDialog.FileName); // Cargar hojas al seleccionar
                }
                // No limpiamos si cancela, para que pueda reintentar importar si ya tenía archivo
                ActualizarEstadoBotonImportar(); // Actualizar estado botón importar
            }
        }

        // --- Cargar Nombres de Hojas del Excel ---
        private void CargarNombresDeHojas(string rutaArchivo)
        {
            List<string> nombresHojas = new List<string>();
            cmbHojas.DataSource = null; // Limpiar primero
            cmbHojas.Enabled = false; // Deshabilitar mientras carga

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // Necesario
                using (var stream = File.Open(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do { nombresHojas.Add(reader.Name); } while (reader.NextResult());
                }

                if (nombresHojas.Any())
                {
                    cmbHojas.DataSource = nombresHojas;
                    cmbHojas.SelectedIndex = 0;
                    cmbHojas.Enabled = true;
                }
                else
                {
                    MessageBox.Show("El archivo Excel seleccionado no contiene hojas.", "Archivo Vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRutaArchivo.Text = ""; // Limpiar ruta si no hay hojas
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"No se pudo leer el archivo. Asegúrese de que no esté abierto en Excel.\nError: {ioEx.Message}", "Error al leer archivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRutaArchivo.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado al leer las hojas del archivo Excel:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRutaArchivo.Text = "";
            }
            finally
            {
                ActualizarEstadoBotonImportar(); // Actualizar botón importar al final
            }
        }

        // --- Eventos para actualizar estado del botón Importar ---
        private void cmbHojas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotonImportar();
        }

        private void cmbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotonImportar();
        }

        // --- Habilitar/Deshabilitar Botón Importar ---
        private void ActualizarEstadoBotonImportar()
        {
            bool archivoOk = !string.IsNullOrEmpty(txtRutaArchivo.Text) && File.Exists(txtRutaArchivo.Text);
            bool hojaOk = cmbHojas.SelectedItem != null;
            bool proyectoOk = cmbProyecto.SelectedValue != null && (cmbProyecto.SelectedValue is int);

            btnImportar.Enabled = archivoOk && hojaOk && proyectoOk;
        }

        // --- BOTÓN IMPORTAR: Inicia el proceso ---
        private async void btnImportar_Click(object sender, EventArgs e)
        {
            // Validaciones Iniciales (ya hechas por ActualizarEstadoBotonImportar, pero doble chequeo)
            string rutaArchivo = txtRutaArchivo.Text;
            if (string.IsNullOrEmpty(rutaArchivo) || !File.Exists(rutaArchivo)) { MessageBox.Show("Seleccione archivo."); return; }
            if (cmbHojas.SelectedItem == null) { MessageBox.Show("Seleccione hoja."); return; }
            if (cmbProyecto.SelectedValue == null || !(cmbProyecto.SelectedValue is int)) { MessageBox.Show("Seleccione proyecto."); return; }

            string nombreHoja = cmbHojas.SelectedItem.ToString();
            int idProyectoSeleccionado = (int)cmbProyecto.SelectedValue;
            bool tieneEncabezado = chkTieneEncabezado.Checked;

            // Preparar UI
            btnImportar.Enabled = false; btnSeleccionarArchivo.Enabled = false;
            cmbProyecto.Enabled = false; cmbHojas.Enabled = false; chkTieneEncabezado.Enabled = false;
            progressBarImportacion.Value = 0; progressBarImportacion.Visible = true;
            lblProgreso.Text = "Iniciando importación..."; lblProgreso.Visible = true;
            dgvResultados.DataSource = null;

            // Iniciar Tarea Asíncrona
            ResultadoImportacion resultados = null;
            try
            {
                resultados = await Task.Run(() => ProcesarArchivoExcel(rutaArchivo, nombreHoja, tieneEncabezado, idProyectoSeleccionado));

                // Mostrar Resumen Final
                lblProgreso.Text = $"Proceso finalizado. {resultados.Exitosos} pacientes guardados, {resultados.Errores.Count} filas con errores.";
                MessageBox.Show($"Importación Finalizada.\n\nPacientes guardados: {resultados.Exitosos}\nFilas con errores: {resultados.Errores.Count}",
                                "Proceso Terminado", MessageBoxButtons.OK,
                                resultados.Errores.Any() ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                // Mostrar errores detallados
                if (resultados.Errores.Any())
                {
                    dgvResultados.DataSource = resultados.Errores;
                    // Configurar columnas (hacerlo idealmente en el diseñador)
                    try
                    { // Puede fallar si las columnas no existen o AutoGenerate está off
                        if (dgvResultados.Columns["Fila"] != null) dgvResultados.Columns["Fila"].HeaderText = "Fila Excel";
                        if (dgvResultados.Columns["Mensaje"] != null) { dgvResultados.Columns["Mensaje"].HeaderText = "Motivo del Error"; dgvResultados.Columns["Mensaje"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; }
                        if (dgvResultados.Columns["CodigoIntentado"] != null) dgvResultados.Columns["CodigoIntentado"].HeaderText = "Código Leído";
                        if (dgvResultados.Columns["NombreIntentado"] != null) dgvResultados.Columns["NombreIntentado"].HeaderText = "Nombre Leído";
                    }
                    catch (Exception exGrid) { Console.WriteLine($"Error configurando DGV: {exGrid.Message}"); }
                }
            }
            catch (Exception ex)
            {
                lblProgreso.Text = "Error durante la importación.";
                MessageBox.Show($"Ocurrió un error grave durante la importación:\n{ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR IMPORTACIÓN (btnImportar_Click): {ex.ToString()}");
            }
            finally
            {
                // Reactivar UI
                progressBarImportacion.Visible = false;
                btnImportar.Enabled = true; // Habilitar de nuevo por si quiere reintentar
                btnSeleccionarArchivo.Enabled = true;
                cmbProyecto.Enabled = true; // Habilitar de nuevo si hay proyectos
                cmbHojas.Enabled = !string.IsNullOrEmpty(txtRutaArchivo.Text); // Habilitar si hay ruta
                chkTieneEncabezado.Enabled = true;
                ActualizarEstadoBotonImportar(); // Re-evaluar estado final del botón importar
            }
        }


        // --- MÉTODO ACTUALIZADO: ProcesarArchivoExcel con Validaciones y Guardado 1x1 ---
        private ResultadoImportacion ProcesarArchivoExcel(string rutaArchivo, string nombreHoja, bool tieneEncabezado, int idProyecto)
        {
            ResultadoImportacion resultado = new ResultadoImportacion();
            int filaActualExcel = tieneEncabezado ? 1 : 0;
            int filasProcesadas = 0;

            if (pacienteRepository == null) pacienteRepository = new PacienteRepository();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            try
            {
                using (var stream = File.Open(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataset = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = tieneEncabezado }
                    });

                    DataTable tabla = dataset.Tables[nombreHoja] ?? dataset.Tables[0];
                    if (tabla == null) throw new Exception("No se encontró la hoja especificada o el archivo está vacío.");

                    int totalFilas = tabla.Rows.Count;

                    // Actualizar UI inicial (Progreso)
                    this.Invoke((MethodInvoker)delegate { progressBarImportacion.Maximum = totalFilas; progressBarImportacion.Value = 0; lblProgreso.Text = $"Procesando {totalFilas} filas..."; });

                    // --- Iterar Filas ---
                    foreach (DataRow fila in tabla.Rows)
                    {
                        filaActualExcel++; filasProcesadas++;

                        // Actualizar Progreso
                        if (filasProcesadas % 5 == 0 || filasProcesadas == totalFilas)
                        {
                            this.Invoke((MethodInvoker)delegate { progressBarImportacion.Value = filasProcesadas; lblProgreso.Text = $"Procesando fila {filasProcesadas} de {totalFilas}..."; });
                        }

                        // Extraer datos
                        string codigo = fila.Field<string>(1)?.Trim();
                        string nombreCompleto = fila.Field<string>(2)?.Trim();
                        object fechaObj = fila[3];
                        string generoChar = fila.Field<string>(4)?.Trim().ToUpper();

                        // Filtrar filas vacías
                        if (string.IsNullOrWhiteSpace(codigo) && string.IsNullOrWhiteSpace(nombreCompleto)) continue;

                        // Validar Datos
                        bool esFilaValida = true; List<string> erroresFila = new List<string>(); DateTime fechaNac = DateTime.MinValue;

                        // 1. Código
                        if (string.IsNullOrWhiteSpace(codigo)) { esFilaValida = false; erroresFila.Add("Código vacío."); }
                        else { try { if (pacienteRepository.ExisteCodigoBeneficiario(codigo)) { esFilaValida = false; erroresFila.Add($"Código '{codigo}' ya existe."); } } catch (Exception exRepo) { esFilaValida = false; erroresFila.Add($"Error DB validando código."); Console.WriteLine(exRepo.Message); } }
                        // 2. Nombre
                        if (string.IsNullOrWhiteSpace(nombreCompleto)) { esFilaValida = false; erroresFila.Add("Nombre vacío."); }
                        // 3. Fecha Nacimiento
                        if (fechaObj == null || string.IsNullOrWhiteSpace(fechaObj.ToString())) { esFilaValida = false; erroresFila.Add("Fecha Nac. vacía."); }
                        else
                        {
                            bool fechaParseada = false;
                            if (fechaObj is DateTime dt) { fechaNac = dt; fechaParseada = true; }
                            else if (fechaObj is double oaDate) { try { fechaNac = DateTime.FromOADate(oaDate); fechaParseada = true; } catch { /* Ignorar error de OADate */ } }
                            if (!fechaParseada && DateTime.TryParseExact(fechaObj.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaNac)) { fechaParseada = true; }
                            if (!fechaParseada && DateTime.TryParse(fechaObj.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaNac)) { fechaParseada = true; }

                            if (!fechaParseada) { esFilaValida = false; erroresFila.Add($"Formato Fecha Nac. inválido ('{fechaObj}')."); }
                            else if (fechaNac > DateTime.Today || fechaNac < new DateTime(1900, 1, 1)) { esFilaValida = false; erroresFila.Add("Fecha Nac. fuera de rango."); }
                        }
                        // 4. Género
                        if (generoChar != "F" && generoChar != "M") { esFilaValida = false; erroresFila.Add("Género inválido (F/M)."); }

                        // --- Procesar / Guardar si es Válido ---
                        if (esFilaValida)
                        {
                            string nombres = ""; string apellidos = "";
                            int firstSpace = nombreCompleto.IndexOf(' ');
                            if (firstSpace > 0) { nombres = nombreCompleto.Substring(0, firstSpace); apellidos = nombreCompleto.Substring(firstSpace + 1); } else { nombres = nombreCompleto; }
                            string generoDb = (generoChar == "F") ? "Femenino" : "Masculino";
                            int edadDb = DateTime.Today.Year - fechaNac.Year; if (DateTime.Today < fechaNac.AddYears(edadDb)) edadDb--; if (edadDb < 0) edadDb = 0;

                            pacientes nuevoPaciente = new pacientes
                            {
                                nombres = CapitalizarTexto(nombres),
                                apellidos = CapitalizarTexto(apellidos),
                                edad = edadDb,
                                genero = generoDb,
                                codigo_beneficiario = codigo,
                                fecha_nacimiento = fechaNac,
                                id_proyecto = idProyecto,
                                observacion = ""
                            };

                            // --- Guardar Paciente Individualmente ---
                            try
                            {
                                pacienteRepository.GuardarPaciente(nuevoPaciente);
                                resultado.Exitosos++; // Incrementar éxito
                            }
                            catch (Exception exSave)
                            {
                                Console.WriteLine($"ERROR al guardar paciente Cod={codigo}: {exSave.Message}");
                                // Registrar error si falla guardado
                                resultado.Errores.Add(new ErrorImportacion { Fila = filaActualExcel, Mensaje = $"Error al guardar en BD: {exSave.Message}", CodigoIntentado = codigo, NombreIntentado = nombreCompleto });
                            }
                        }
                        else
                        {
                            // Registrar error de validación
                            resultado.Errores.Add(new ErrorImportacion { Fila = filaActualExcel, Mensaje = string.Join("; ", erroresFila), CodigoIntentado = codigo ?? "", NombreIntentado = nombreCompleto ?? "" });
                        }
                    } // Fin foreach fila
                } // Fin using reader
            } // Fin using stream
        
        catch (IOException ioEx) { Console.WriteLine($"Error de IO: {ioEx.Message}"); resultado.Errores.Add(new ErrorImportacion { Fila = 0, Mensaje = $"Error IO: {ioEx.Message}" }); }
        catch (Exception ex) { Console.WriteLine($"Error procesando Excel: {ex.ToString()}"); resultado.Errores.Add(new ErrorImportacion { Fila = filaActualExcel, Mensaje = $"Error General Fila ~{filaActualExcel}: {ex.Message}" }); }

        // Actualizar progreso final
        this.Invoke((MethodInvoker)delegate { if (progressBarImportacion.Maximum > 0) progressBarImportacion.Value = progressBarImportacion.Maximum; });
            return resultado;
                }

                // --- Método auxiliar para capitalizar ---
                private string CapitalizarTexto(string texto)
            {
                if (string.IsNullOrWhiteSpace(texto)) return texto;
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                return textInfo.ToTitleCase(texto.ToLower());
            }

            // --- Clases auxiliares para resultado y error ---
            public class ResultadoImportacion
            {
                public int Exitosos { get; set; } = 0;
                public List<ErrorImportacion> Errores { get; set; } = new List<ErrorImportacion>();
            }
            public class ErrorImportacion
            {
                public int Fila { get; set; }
                public string Mensaje { get; set; }
                public string CodigoIntentado { get; set; }
                public string NombreIntentado { get; set; }
            }

            // --- Handlers vacíos (puedes quitarlos si no los usas) ---
            private void cmbProyectoImportar_SelectedIndexChanged(object sender, EventArgs e) 
            { /* No necesita lógica si ActualizarEstado lo maneja todo */ }
            
            private void wImportarPacientes_Load_1
                (object sender, EventArgs e) { /* Probablemente redundante, usa el otro Load */ }

    } // Fin clase wImportarPacientes
} // Fin namespace