using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader; // NuGet: ExcelDataReader, ExcelDataReader.DataSet
using Npgsql; // Para NpgsqlException si la usas en el repo
using ZasTrack.Models;
using ZasTrack.Repositories;
using static System.Net.WebRequestMethods;

using static System.Runtime.InteropServices.JavaScript.JSType;

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
            pacienteRepository = new PacienteRepository();
            proyectoRepository = new ProyectoRepository();

            // --- ASEGÚRATE QUE ESTÉN AQUÍ (Y NO EN EL DISEÑADOR) ---
            this.Load += wImportarPacientes_Load;
            btnSeleccionarArchivo.Click += btnSeleccionarArchivo_Click;
            this.btnImportar.Click += btnImportar_Click;
            cmbHojas.SelectedIndexChanged += cmbHojas_SelectedIndexChanged;
            cmbProyecto.SelectedIndexChanged += cmbProyecto_SelectedIndexChanged;
            // -------------------------------------------------------
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
                using (var stream = System.IO.File.Open(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) // Añade System.IO.
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
            bool archivoOk = !string.IsNullOrEmpty(txtRutaArchivo.Text) && System.IO.File.Exists(txtRutaArchivo.Text);
            bool hojaOk = cmbHojas.SelectedItem != null;
            bool proyectoOk = cmbProyecto.SelectedValue != null && (cmbProyecto.SelectedValue is int);

            btnImportar.Enabled = archivoOk && hojaOk && proyectoOk;
        }

        // --- BOTÓN IMPORTAR: Inicia el proceso ---
        private async void btnImportar_Click(object sender, EventArgs e)
        {
            // --- 1. Validaciones Iniciales ---
            string rutaArchivo = txtRutaArchivo.Text;
            if (string.IsNullOrEmpty(rutaArchivo) || !System.IO.File.Exists(rutaArchivo))
            {
                MessageBox.Show("Seleccione archivo Excel válido.", "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbHojas.SelectedItem == null)
            {
                MessageBox.Show("Seleccione la hoja de cálculo.", "Hoja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbProyecto.SelectedValue == null || !(cmbProyecto.SelectedValue is int))
            {
                MessageBox.Show("Seleccione un proyecto destino.", "Proyecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombreHoja = cmbHojas.SelectedItem.ToString();
            int idProyectoSeleccionado = (int)cmbProyecto.SelectedValue;
            bool tieneEncabezado = chkTieneEncabezado.Checked;

            // --- 2. Preparar UI para Importación ---
            btnImportar.Enabled = false;
            btnSeleccionarArchivo.Enabled = false;
            cmbProyecto.Enabled = false;
            cmbHojas.Enabled = false;
            chkTieneEncabezado.Enabled = false;
            progressBarImportacion.Value = 0;
            progressBarImportacion.Visible = true;
            lblProgreso.Text = "Iniciando importación...";
            lblProgreso.Visible = true;
            dgvResultados.DataSource = null;

            // --- 3. Iniciar Tarea Asíncrona ---
            ResultadoImportacion resultados = null;
            try
            {
                resultados = await Task.Run(() => ProcesarArchivoExcel(rutaArchivo, nombreHoja, tieneEncabezado, idProyectoSeleccionado));

                // --- 4. Mostrar Resultados en Nueva Ventana (UI Thread) ---
                if (resultados != null) // Verificar que el proceso devolvió algo
                {
                    // Actualizar label local con resumen simple
                    // Usamos resultados.PacientesGuardados.Count en lugar de resultados.Exitosos para estar seguros
                    lblProgreso.Text = $"Proceso finalizado. {resultados.PacientesGuardados.Count} guardados, {resultados.Errores.Count} filas con errores.";


                    // ** OPCIONAL: Mostrar errores en el grid local **
                    // Si quieres mantener los errores aquí además de en la ventana nueva:
                    if (resultados.Errores.Any())
                    {
                        dgvResultados.DataSource = null;
                        dgvResultados.DataSource = resultados.Errores;
                        try { /* ... configurar columnas dgvResultados ... */ } catch { /*...*/ }
                        Console.WriteLine("Mostrando errores en dgvResultados local.");
                    }
                    else
                    {
                        dgvResultados.DataSource = null; // Limpiar si no hay errores
                    }

                    // *** Mostrar la nueva ventana de resultados ***
                    // Solo si hubo algún paciente guardado O algún error que mostrar
                    if (resultados.PacientesGuardados.Any() || resultados.Errores.Any())
                    {
                        try
                        {
                            // Crear instancia del formulario de resultados pasándole el objeto ResultadoImportacion
                            wResultadosImportacion formResultados = new wResultadosImportacion(resultados);
                            // Mostrarla como diálogo modal (el usuario debe cerrarla para continuar)
                            formResultados.ShowDialog(this);
                        }
                        catch (Exception exDialog)
                        {
                            MessageBox.Show($"Error al mostrar ventana de resultados: {exDialog.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Mensaje si el archivo estaba vacío o todas las filas fueron inválidas/omitidas
                        MessageBox.Show("No se encontraron pacientes válidos para importar en el archivo.", "Importación Vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    lblProgreso.Text = "El proceso de importación no devolvió resultados.";
                    MessageBox.Show("El proceso de importación finalizó inesperadamente sin resultados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblProgreso.Text = "Error durante la importación.";
                MessageBox.Show($"Ocurrió un error grave durante la importación:\n{ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR IMPORTACIÓN (btnImportar_Click): {ex}");
            }
            finally
            {
                // --- 5. Reactivar UI ---
                progressBarImportacion.Visible = false;
                btnImportar.Enabled = true;
                btnSeleccionarArchivo.Enabled = true;
                cmbProyecto.Enabled = cmbProyecto.Items.Count > 0;
                cmbHojas.Enabled = !string.IsNullOrEmpty(txtRutaArchivo.Text);
                chkTieneEncabezado.Enabled = true;
                ActualizarEstadoBotonImportar();
            }
        }

        // --- MÉTODO ACTUALIZADO: ProcesarArchivoExcel con Validaciones y Guardado 1x1 ---
        private ResultadoImportacion ProcesarArchivoExcel(string rutaArchivo, string nombreHoja, bool tieneEncabezado, int idProyecto)
        {
            ResultadoImportacion resultado = new ResultadoImportacion();
            int filaExcelActual = tieneEncabezado ? 1 : 0; // Inicia antes de la primera fila a leer
            int filasProcesadas = 0;

            if (pacienteRepository == null) pacienteRepository = new PacienteRepository();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            try
            {
                using (var stream = System.IO.File.Open(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataset = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = tieneEncabezado
                        }
                    });

                    DataTable tabla = dataset.Tables[nombreHoja] ?? dataset.Tables[0];
                    if (tabla == null) throw new Exception("No se encontró la hoja especificada o el archivo Excel está vacío.");

                    foreach (DataRow fila in tabla.Rows)
                    {
                        filaExcelActual++;
                        filasProcesadas++;

                        if (tieneEncabezado && filasProcesadas == 1) continue;

                        string codigo = fila.Field<string>(1)?.Trim();
                        string nombreCompleto = fila.Field<string>(2)?.Trim();
                        object fechaObj = fila[3];
                        string generoChar = fila.Field<string>(4)?.Trim().ToUpper();

                        if (string.IsNullOrWhiteSpace(codigo) && string.IsNullOrWhiteSpace(nombreCompleto)) continue;

                        bool esFilaValida = true;
                        List<string> erroresFila = new List<string>();
                        DateTime fechaNac = DateTime.MinValue;

                        if (string.IsNullOrWhiteSpace(codigo))
                        {
                            esFilaValida = false;
                            erroresFila.Add("Código vacío.");
                        }
                        else
                        {
                            try
                            {
                                if (pacienteRepository.ExisteCodigoBeneficiario(codigo))
                                {
                                    esFilaValida = false;
                                    erroresFila.Add($"Código '{codigo}' ya existe.");
                                }
                            }
                            catch
                            {
                                esFilaValida = false;
                                erroresFila.Add("Error validando código.");
                            }
                        }

                        if (string.IsNullOrWhiteSpace(nombreCompleto))
                        {
                            esFilaValida = false;
                            erroresFila.Add("Nombre vacío.");
                        }

                        if (fechaObj == null || string.IsNullOrWhiteSpace(fechaObj.ToString()))
                        {
                            esFilaValida = false;
                            erroresFila.Add("Fecha Nac. vacía.");
                        }
                        else
                        {
                            if (!DateTime.TryParse(fechaObj.ToString(), out fechaNac) || fechaNac > DateTime.Today || fechaNac < new DateTime(1900, 1, 1))
                            {
                                esFilaValida = false;
                                erroresFila.Add("Fecha Nac. inválida.");
                            }
                        }

                        if (generoChar != "F" && generoChar != "M")
                        {
                            esFilaValida = false;
                            erroresFila.Add("Género inválido (F/M).");
                        }

                        if (esFilaValida)
                        {
                            string nombres = "", apellidos = "";
                            int firstSpace = nombreCompleto.IndexOf(' ');
                            if (firstSpace > 0)
                            {
                                nombres = nombreCompleto.Substring(0, firstSpace).Trim();
                                apellidos = nombreCompleto.Substring(firstSpace + 1).Trim();
                            }
                            else
                            {
                                nombres = nombreCompleto.Trim();
                            }

                            pacientes nuevoPaciente = new pacientes
                            {
                                nombres = CapitalizarTexto(nombres),
                                apellidos = CapitalizarTexto(apellidos),
                                edad = DateTime.Today.Year - fechaNac.Year,
                                genero = generoChar == "F" ? "Femenino" : "Masculino",
                                codigo_beneficiario = codigo,
                                fecha_nacimiento = fechaNac,
                                id_proyecto = idProyecto,
                                observacion = ""
                            };

                            try
                            {
                                pacienteRepository.GuardarPaciente(nuevoPaciente);
                                resultado.PacientesGuardados.Add(nuevoPaciente);
                            }
                            catch (Exception exSave)
                            {
                                Console.WriteLine($"ERROR al guardar Cod={codigo}, FilaExcel={filaExcelActual}: {exSave.Message}");

                                resultado.Errores.Add(new ErrorImportacion
                                {
                                    Fila = filaExcelActual,
                                    Mensaje = $"Error al guardar BD: {exSave.Message}",
                                    CodigoIntentado = codigo,
                                    NombreIntentado = nombreCompleto
                                });

                            }
                        }
                        else
                        {
                            resultado.Errores.Add(new ErrorImportacion
                            {
                                Fila = filaExcelActual,
                                Mensaje = string.Join("; ", erroresFila),
                                CodigoIntentado = codigo ?? "",
                                NombreIntentado = nombreCompleto ?? ""
                            });
                        }
                    }
                }
            }
            catch (IOException ioEx)
            {
                resultado.Errores.Add(new ErrorImportacion { Fila = 0, Mensaje = $"Error IO: {ioEx.Message}" });
            }
            catch (Exception ex)
            {
                resultado.Errores.Add(new ErrorImportacion { Fila = filaExcelActual, Mensaje = $"Error General: {ex.Message}" });
            }

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
        // FUERA de la clase wImportarPacientes, DENTRO del namespace

       
        // --- Handlers vacíos (puedes quitarlos si no los usas) ---
        private void cmbProyectoImportar_SelectedIndexChanged(object sender, EventArgs e)
        { /* No necesita lógica si ActualizarEstado lo maneja todo */ }

        private void wImportarPacientes_Load_1
            (object sender, EventArgs e)
        { /* Probablemente redundante, usa el otro Load */ }
    } // Fin clase wImportarPacientes
    public class ResultadoImportacion
    {
        public List<pacientes> PacientesGuardados { get; set; }
        public List<ErrorImportacion> Errores { get; set; }
        public int Exitosos => PacientesGuardados?.Count ?? 0;

        public ResultadoImportacion()
        {
            PacientesGuardados = new List<pacientes>();
            Errores = new List<ErrorImportacion>();
        }
    }

    public class ErrorImportacion
    {
        public int Fila { get; set; }
        public string Mensaje { get; set; }
        public string CodigoIntentado { get; set; }
        public string NombreIntentado { get; set; }
    }
} // Fin namespace