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
        #region Metodos
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
      
        private void ActualizarEstadoBotonImportar()
        {
            bool archivoOk = !string.IsNullOrEmpty(txtRutaArchivo.Text) && System.IO.File.Exists(txtRutaArchivo.Text);
            bool hojaOk = cmbHojas.SelectedItem != null;
            bool proyectoOk = cmbProyecto.SelectedValue != null && (cmbProyecto.SelectedValue is int);

            btnImportar.Enabled = archivoOk && hojaOk && proyectoOk;
        }

       
        private ResultadoImportacion ProcesarArchivoExcel(string rutaArchivo, string nombreHoja, bool tieneEncabezado, int idProyecto)
        {
          
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            ResultadoImportacion resultado = new ResultadoImportacion();
            int filaExcelActual = 0; // Inicia en 0, la primera fila leída será la 1 (o 2 si hay encabezado)
            int filasLeidasDelExcel = 0; // Contador de filas realmente leídas del DataRow

            // Asegurar que el repositorio esté instanciado (ya lo haces en el constructor, está bien)
            if (pacienteRepository == null) pacienteRepository = new PacienteRepository();

            try
            {
                using (var stream = File.Open(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) // Usar System.IO.File
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataset = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = tieneEncabezado // Usa el valor del CheckBox
                        }
                    });

                    DataTable tabla;
                    if (dataset.Tables.Contains(nombreHoja))
                    {
                        tabla = dataset.Tables[nombreHoja];
                    }
                    else if (dataset.Tables.Count > 0)
                    {
                        // Si no encuentra la hoja por nombre, toma la primera (podrías avisar al usuario)
                        tabla = dataset.Tables[0];
                        Console.WriteLine($"WARN: Hoja '{nombreHoja}' no encontrada. Usando primera hoja: '{tabla.TableName}'.");
                        // Podrías incluso actualizar el cmbHojas aquí si quisieras.
                    }
                    else
                    {
                        throw new Exception("El archivo Excel no contiene hojas o no se pudo leer ninguna.");
                    }


                    foreach (DataRow fila in tabla.Rows)
                    {
                        filasLeidasDelExcel++; // Este contador es para las filas leídas del DataTable 'tabla'
                        filaExcelActual = tieneEncabezado ? filasLeidasDelExcel + 1 : filasLeidasDelExcel; // Número de fila en el archivo Excel

                        // --- AÑADIR ESTA VALIDACIÓN PARA OMITIR ENCABEZADO ---
                        // Si el usuario marcó "Tiene Encabezado" Y esta es la primera fila que estamos leyendo
                        // del DataTable (que ExcelDataReader pudo haber incluido erróneamente), la saltamos.
                        if (tieneEncabezado && filasLeidasDelExcel == 1)
                        {
                            Console.WriteLine($"DEBUG: Omitiendo fila de encabezado detectada en DataTable (Fila Excel: {filaExcelActual})");
                            continue; // Saltar al siguiente DataRow
                        }

                        string codigo = fila.Field<string>(1)?.Trim();         // Código de la columna B
                        string nombreCompleto = fila.Field<string>(2)?.Trim(); // Nombre de la columna C
                        object fechaObj = fila[3];                             // Fecha de la columna D
                        string generoChar = fila.Field<string>(4)?.Trim().ToUpper(); // Género de la columna E


                        // Omitir filas completamente vacías (basado en código y nombre)
                        if (string.IsNullOrWhiteSpace(codigo) && string.IsNullOrWhiteSpace(nombreCompleto))
                        {
                            Console.WriteLine($"DEBUG: Fila Excel {filaExcelActual} omitida por estar vacía (código y nombre).");
                            continue; // Saltar a la siguiente fila
                        }

                        bool esFilaValida = true;
                        List<string> erroresFila = new List<string>();
                        DateTime fechaNac = DateTime.MinValue;

                        // --- Validaciones ---
                        // Código
                        if (string.IsNullOrWhiteSpace(codigo))
                        {
                            esFilaValida = false;
                            erroresFila.Add("Código de beneficiario está vacío.");
                        }
                        else
                        {
                            try
                            {
                                // *** OJO: Asumimos que tu PacienteRepository tiene el método ExisteCodigoBeneficiario ***
                                if (pacienteRepository.ExisteCodigoBeneficiario(codigo)) // Valida contra la BD
                                {
                                    esFilaValida = false;
                                    erroresFila.Add($"El código de beneficiario '{codigo}' ya existe en la base de datos.");
                                }
                            }
                            catch (Exception exValCod)
                            {
                                esFilaValida = false;
                                erroresFila.Add($"Error al validar código contra BD: {exValCod.Message}");
                                Console.WriteLine($"ERROR validando código {codigo}: {exValCod}");
                            }
                        }

                        // Nombre Completo
                        string nombres = "";
                        string apellidos = "";
                        if (string.IsNullOrWhiteSpace(nombreCompleto))
                        {
                            esFilaValida = false;
                            erroresFila.Add("Nombre completo está vacío.");
                        }
                        else
                        {
                            // --- LÓGICA DE DIVISIÓN DE NOMBRE MEJORADA ---
                            string[] palabras = nombreCompleto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (palabras.Length == 1)
                            {
                                nombres = palabras[0];
                                apellidos = ""; // O marcar como error si se requieren apellidos
                                erroresFila.Add("Solo se encontró una palabra en Nombre Completo (asignado a Nombres).");
                            }
                            else if (palabras.Length == 2)
                            {
                                nombres = palabras[0];
                                apellidos = palabras[1];
                            }
                            else if (palabras.Length > 2)
                            {
                                apellidos = $"{palabras[palabras.Length - 2]} {palabras[palabras.Length - 1]}";
                                nombres = string.Join(" ", palabras.Take(palabras.Length - 2));
                            }
                            // --- FIN LÓGICA DE DIVISIÓN ---

                            if (string.IsNullOrWhiteSpace(nombres)) { esFilaValida = false; erroresFila.Add("Nombres (después de dividir) está vacío."); }
                            if (string.IsNullOrWhiteSpace(apellidos) && palabras.Length > 1) { esFilaValida = false; erroresFila.Add("Apellidos (después de dividir) está vacío."); }
                        }


                        // Fecha Nacimiento
                        if (fechaObj == null || string.IsNullOrWhiteSpace(fechaObj.ToString()))
                        {
                            esFilaValida = false;
                            erroresFila.Add("Fecha de Nacimiento está vacía.");
                        }
                        else
                        {
                            // ExcelDataReader a veces devuelve fechas como double (OADate) o ya como DateTime
                            if (fechaObj is double d) { fechaNac = DateTime.FromOADate(d); }
                            else if (fechaObj is DateTime dt) { fechaNac = dt; }
                            else if (!DateTime.TryParse(fechaObj.ToString(), out fechaNac))
                            {
                                // Intenta otros formatos comunes si el TryParse directo falla
                                string[] formatosFecha = { "dd/MM/yyyy", "d/M/yy", "d-M-yyyy", "yyyy-MM-dd" }; // Añade más si es necesario
                                if (!DateTime.TryParseExact(fechaObj.ToString(), formatosFecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaNac))
                                {
                                    esFilaValida = false;
                                    erroresFila.Add($"Fecha Nac. '{fechaObj}' formato inválido.");
                                }
                            }

                            if (esFilaValida && (fechaNac > DateTime.Today.AddYears(1) || fechaNac < new DateTime(1900, 1, 1))) // +1 año por si es un bebé recién nacido
                            {
                                esFilaValida = false;
                                erroresFila.Add("Fecha Nac. fuera de rango (1900 - Hoy+1año).");
                            }
                        }

                        // Género
                        if (string.IsNullOrWhiteSpace(generoChar) || (generoChar != "F" && generoChar != "M"))
                        {
                            esFilaValida = false;
                            erroresFila.Add("Género inválido (debe ser F o M).");
                        }

                        // --- Fin Validaciones ---

                        if (esFilaValida)
                        {
                            int edadCalculada = DateTime.Today.Year - fechaNac.Year;
                            if (fechaNac.Date > DateTime.Today.AddYears(-edadCalculada)) edadCalculada--; // Ajustar si aún no cumple años este año

                            pacientes nuevoPaciente = new pacientes // *** OJO: usa el nombre de tu clase (pacientes o Paciente) ***
                            {
                                nombres = CapitalizarTexto(nombres),
                                apellidos = CapitalizarTexto(apellidos),
                                edad = edadCalculada,
                                genero = generoChar == "F" ? "Femenino" : "Masculino",
                                codigo_beneficiario = codigo.ToUpper(), // Guardar código en mayúsculas quizás
                                fecha_nacimiento = fechaNac.Date, // Guardar solo la fecha
                                id_proyecto = idProyecto,
                                observacion = "" // Opcional: tomarlo del Excel si hay columna
                            };

                            try
                            {
                                // *** OJO: Asumimos que tu PacienteRepository tiene el método GuardarPaciente ***
                                pacienteRepository.GuardarPaciente(nuevoPaciente);
                                resultado.PacientesGuardados.Add(nuevoPaciente);
                            }
                            catch (Exception exSave)
                            {
                                Console.WriteLine($"ERROR al guardar Paciente con Código={codigo} de FilaExcel={filaExcelActual}: {exSave.Message}");
                                resultado.Errores.Add(new ErrorImportacion
                                {
                                    Fila = filaExcelActual,
                                    Mensaje = $"Error BD: {exSave.Message}",
                                    CodigoIntentado = codigo,
                                    NombreIntentado = nombreCompleto
                                });
                            }
                        }
                        else // Si la fila no es válida
                        {
                            resultado.Errores.Add(new ErrorImportacion
                            {
                                Fila = filaExcelActual,
                                Mensaje = string.Join("; ", erroresFila),
                                CodigoIntentado = codigo ?? "",
                                NombreIntentado = nombreCompleto ?? ""
                            });
                        }

                        // Actualizar progreso (ejemplo simple)
                        // Necesitarías una forma de comunicar esto a la UI si ProcesarArchivoExcel
                        // se ejecuta en un hilo diferente de verdad (con Task.Run como lo tenías antes).
                        // Por ahora, lo pongo directo pero no actualizará la UI si este método
                        // se llama desde un Task.Run sin un IProgress<T>.
                        // int progreso = (int)(((double)filasLeidasDelExcel / tabla.Rows.Count) * 100);
                        // this.Invoke((MethodInvoker)delegate {
                        //     progressBarImportacion.Value = Math.Min(progreso, 100);
                        //     lblProgreso.Text = $"Procesando fila {filasLeidasDelExcel} de {tabla.Rows.Count}... {progreso}%";
                        // });

                    } // Fin foreach
                } // Fin using reader
            } // Fin using stream
            catch (IOException ioEx)
            {
                Console.WriteLine($"ERROR IO ProcesarArchivoExcel: {ioEx.ToString()}");
                resultado.Errores.Add(new ErrorImportacion { Fila = 0, Mensaje = $"Error IO: {ioEx.Message}. Asegúrese de que el archivo no esté en uso." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR General ProcesarArchivoExcel: {ex.ToString()}");
                resultado.Errores.Add(new ErrorImportacion { Fila = filasLeidasDelExcel, Mensaje = $"Error General al procesar: {ex.Message}" });
            }

            return resultado;
        }

        private string CapitalizarTexto(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return texto;
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(texto.ToLower());
        }
        #endregion


        #region Eventos
        private void lblUbicacionArc_Click(object sender, EventArgs e)
        {

        }
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
        private void cmbHojas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotonImportar();
        }
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
        private void cmbProyecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotonImportar();
        }

        #endregion

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