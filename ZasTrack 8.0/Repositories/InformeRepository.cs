using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using ZasTrack.Models;
using ZasTrack.Models.ExamenModel;
using ZasTrack.Models.Informes;

namespace ZasTrack.Repositories
{
    public class InformeRepository
    {
        private ExamenRepository _examenRepository;

        public InformeRepository()
        {
            _examenRepository = new ExamenRepository();
        }

        // --- BuscarMuestrasParaInformePorProyecto (Sin cambios respecto a la versión anterior) ---
        public List<MuestraInformeViewModel> BuscarMuestrasParaInformePorProyecto(int idProyecto)
        {
            var listaResultados = new List<MuestraInformeViewModel>();
            string query = @"
            SELECT DISTINCT
                m.id_muestra, m.fecha_recepcion, p.codigo_beneficiario,
                p.nombres, p.apellidos, p.genero, p.edad,
                COALESCE(pr.nombre, 'N/A') AS NombreProyecto,
                COALESCE(
                    (SELECT STRING_AGG(DISTINCT te.nombre, ', ' ORDER BY te.nombre)
                     FROM examen ex_agg
                     JOIN tipo_examen te ON ex_agg.id_tipo_examen = te.id_tipo_examen
                     WHERE ex_agg.id_muestra = m.id_muestra),
                    'Ninguno'
                ) AS ExamenesRealizados
            FROM muestra m
            INNER JOIN pacientes p ON m.id_paciente = p.id_paciente
            LEFT JOIN proyecto pr ON m.id_proyecto = pr.id_proyecto
            WHERE m.id_proyecto = @idProyecto
              AND EXISTS (
                  SELECT 1 FROM examen ex
                  LEFT JOIN examen_orina eo ON ex.id_examen = eo.id_examen AND eo.procesado = TRUE
                  LEFT JOIN examen_heces eh ON ex.id_examen = eh.id_examen AND eh.procesado = TRUE
                  LEFT JOIN examen_sangre es ON ex.id_examen = es.id_examen AND es.procesado = TRUE
                  WHERE ex.id_muestra = m.id_muestra
                    AND (eo.procesado = TRUE OR eh.procesado = TRUE OR es.procesado = TRUE)
              )
            ORDER BY m.fecha_recepcion DESC, p.apellidos, p.nombres;";

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        { /* Mapeo igual que antes */
                            listaResultados.Add(new MuestraInformeViewModel
                            {
                                id_Muestra = reader.GetInt32(reader.GetOrdinal("id_muestra")),
                                FechaTomaRecepcion = reader.GetDateTime(reader.GetOrdinal("fecha_recepcion")),
                                CodigoPaciente = reader.GetString(reader.GetOrdinal("codigo_beneficiario")),
                                NombrePaciente = reader.GetString(reader.GetOrdinal("nombres")),
                                ApellidoPaciente = reader.GetString(reader.GetOrdinal("apellidos")),
                                GeneroPaciente = reader.GetString(reader.GetOrdinal("genero")),
                                EdadPaciente = reader.GetInt32(reader.GetOrdinal("edad")),
                                NombreProyecto = reader.GetString(reader.GetOrdinal("NombreProyecto")),
                                ExamenesRealizados = reader.GetString(reader.GetOrdinal("ExamenesRealizados"))
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"ERROR en InformeRepository.BuscarMuestrasParaInformePorProyecto: {ex}"); throw; }
            return listaResultados;
        }

        // --- ObtenerDatosCompletosInforme (Ahora usa la propiedad IdMuestra correcta) ---
        public InformeCompletoViewModel? ObtenerDatosCompletosInforme(int idMuestra)
        {
            // Usa la propiedad corregida IdMuestra
            var informeVM = new InformeCompletoViewModel { IdMuestra = idMuestra };
            bool infoBasicaCargada = false;

            try
            {
                // 1. Obtener datos Muestra, Paciente, Proyecto (Igual que antes)
                using (var conn = DatabaseConnection.GetConnection())
                using (var cmdInfo = new NpgsqlCommand())
                { /* ... Código igual ... */
                    conn.Open();
                    cmdInfo.Connection = conn;
                    cmdInfo.CommandText = @"
SELECT m.fecha_recepcion, COALESCE(m.observacion_general,'') AS ObservacionGeneral,
       p.nombres, p.apellidos, p.edad, p.genero, p.codigo_beneficiario, p.fecha_nacimiento,
       COALESCE(pr.nombre, 'N/A') AS NombreProyecto
FROM muestra m
JOIN pacientes p ON m.id_paciente = p.id_paciente
LEFT JOIN proyecto pr ON m.id_proyecto = pr.id_proyecto
WHERE m.id_muestra = @idMuestra";// Misma query
                    cmdInfo.Parameters.AddWithValue("@idMuestra", idMuestra);
                    using (var readerInfo = cmdInfo.ExecuteReader())
                    {
                        if (readerInfo.Read())
                        {
                            // Llenamos la información básica en el ViewModel
                            informeVM.FechaToma = readerInfo.GetDateTime(readerInfo.GetOrdinal("fecha_recepcion"));
                            informeVM.FechaInforme = DateTime.Now;
                            informeVM.ObservacionesGenerales = readerInfo.GetString(readerInfo.GetOrdinal("ObservacionGeneral"));
                            informeVM.NombrePaciente = readerInfo.GetString(readerInfo.GetOrdinal("nombres"));
                            informeVM.ApellidoPaciente = readerInfo.GetString(readerInfo.GetOrdinal("apellidos"));
                            informeVM.EdadPaciente = readerInfo.GetInt32(readerInfo.GetOrdinal("edad"));
                            informeVM.GeneroPaciente = readerInfo.GetString(readerInfo.GetOrdinal("genero"));
                            informeVM.CodigoBeneficiario = readerInfo.GetString(readerInfo.GetOrdinal("codigo_beneficiario"));
                            informeVM.NombreProyecto = readerInfo.GetString(readerInfo.GetOrdinal("NombreProyecto"));
                            informeVM.NombreLaboratorio = "Laboratorio Clínico ZasTrack"; // O leer de config
                            informeVM.DireccionLaboratorio = "Dirección Ejemplo, Ciudad"; // O leer de config
                            informeVM.ContactoLaboratorio = "Tel: 123-4567"; // O leer de config

                            infoBasicaCargada = true;
                            Console.WriteLine($"DEBUG: Info básica cargada para Muestra {idMuestra}");
                        }
                        else { Console.WriteLine($"WARN: No se encontró info básica para Muestra ID {idMuestra}"); return null; }
                    }
                }

                if (!infoBasicaCargada) return null;

                // 2. Obtener Resultados llamando a ExamenRepository (Igual que antes)
                examen_sangre? resSangre = _examenRepository.ObtenerResultadoSangre(idMuestra);
                if (resSangre != null) { informeVM.ResultadosBHC = MapearResultadosSangre(resSangre); }
                else { Console.WriteLine($"DEBUG: NO se encontraron resultados Sangre para Muestra {idMuestra}"); }

                examen_orina? resOrina = _examenRepository.ObtenerResultadoOrina(idMuestra);
                if (resOrina != null)
                {
                    informeVM.ResultadosOrinaFQ = MapearResultadosOrinaFQ(resOrina);
                    informeVM.ResultadosOrinaMicro = MapearResultadosOrinaMicro(resOrina);
                }
                else { Console.WriteLine($"DEBUG: NO se encontraron resultados Orina para Muestra {idMuestra}"); }

                examen_heces? resHeces = _examenRepository.ObtenerResultadoHeces(idMuestra);
                if (resHeces != null) { informeVM.ResultadosHeces = MapearResultadosHeces(resHeces); }
                else { Console.WriteLine($"DEBUG: NO se encontraron resultados Heces para Muestra {idMuestra}"); }

            }
            catch (Npgsql.PostgresException pgEx) // Captura errores específicos de PostgreSQL
            {
                // Construimos un mensaje detallado para el MessageBox
                string errorMsg = $@"¡Error de Base de Datos! (Muestra ID: {idMuestra})

                    SQLSTATE: {pgEx.SqlState}

Mensaje: {pgEx.Message}

Detalle: {pgEx.Detail}
Pista: {pgEx.Hint}
Posición: {pgEx.Position}

StackTrace Completo:
{pgEx.ToString()}";

                // Mostramos el MessageBox
                MessageBox.Show(errorMsg, "Error PostgreSQL (InformeRepository)", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Debugger.Break(); // Puedes descomentar para forzar una pausa aquí
                return null; // Devuelve null igual
            }
            catch (Exception ex) // Captura cualquier otro error general
            {
                // Mensaje para errores generales
                string errorMsg = $@"¡Error General! (Muestra ID: {idMuestra})

Tipo: {ex.GetType().Name}

Mensaje: {ex.Message}

StackTrace Completo:
{ex.ToString()}";

                // Mostramos el MessageBox
                MessageBox.Show(errorMsg, "Error General (InformeRepository)", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
            return informeVM;
        }


        // --- MÉTODOS HELPER DE MAPEO (Ahora funcionan con decimal? si modificaste los modelos) ---

        private List<ResultadoParametroVm> MapearResultadosSangre(examen_sangre res)
        {
            var lista = new List<ResultadoParametroVm>();
            if (res == null) return lista;
            // El operador ?. funciona ahora porque asumimos que res.globulos_rojos es decimal?
            lista.Add(new ResultadoParametroVm { Parametro = "Glóbulos Rojos", Resultado = res.globulos_rojos.ToString("N2") ?? "-", Unidad = "x10^6/µL", Referencia = "4.5-5.5" });
            lista.Add(new ResultadoParametroVm { Parametro = "Hemoglobina", Resultado = res.hemoglobina.ToString("N1") ?? "-", Unidad = "g/dL", Referencia = "12-16" });
            lista.Add(new ResultadoParametroVm { Parametro = "Hematocrito", Resultado = res.hematocrito.ToString("N1") ?? "-", Unidad = "%", Referencia = "36-48" });
            lista.Add(new ResultadoParametroVm { Parametro = "Leucocitos", Resultado = res.leucocitos.ToString("N1") ?? "-", Unidad = "x10^3/µL", Referencia = "4.5-11.0" });
            lista.Add(new ResultadoParametroVm { Parametro = "VCM (MCV)", Resultado = res.mcv.ToString("N1") ?? "-", Unidad = "fL", Referencia = "80-100" });
            lista.Add(new ResultadoParametroVm { Parametro = "HCM (MCH)", Resultado = res.mch.ToString("N1") ?? "-", Unidad = "pg", Referencia = "27-32" });
            lista.Add(new ResultadoParametroVm { Parametro = "CHCM (MCHC)", Resultado = res.mchc.ToString("N1") ?? "-", Unidad = "g/dL", Referencia = "32-36" });
            lista.Add(new ResultadoParametroVm { Parametro = "Neutrófilos", Resultado = res.neutrofilos.ToString("N1") ?? "-", Unidad = "%", Referencia = "40-75" });
            lista.Add(new ResultadoParametroVm { Parametro = "Linfocitos", Resultado = res.linfocitos.ToString("N1") ?? "-", Unidad = "%", Referencia = "20-45" });
            lista.Add(new ResultadoParametroVm { Parametro = "Monocitos", Resultado = res.monocitos.ToString("N1") ?? "-", Unidad = "%", Referencia = "2-10" });
            lista.Add(new ResultadoParametroVm { Parametro = "Eosinófilos", Resultado = res.eosinofilos.ToString("N1") ?? "-", Unidad = "%", Referencia = "1-6" });
            lista.Add(new ResultadoParametroVm { Parametro = "Basófilos", Resultado = res.basofilos.ToString("N1") ?? "-", Unidad = "%", Referencia = "0-1" });
            return lista;
        }

        private List<ResultadoParametroVm> MapearResultadosOrinaFQ(examen_orina res)
        {
            var lista = new List<ResultadoParametroVm>();
            if (res == null) return lista; // Si el examen de orina completo no se encontró

            // Para los strings, mantenemos '?? "-"' por si acaso
            lista.Add(new ResultadoParametroVm { Parametro = "Color", Resultado = res.color ?? "-", Unidad = "", Referencia = "Amarillo" });
            lista.Add(new ResultadoParametroVm { Parametro = "Aspecto", Resultado = res.aspecto ?? "-", Unidad = "", Referencia = "Claro" });

            // Para decimales (ph, densidad) - Quitamos el '?.'
            // Asumimos que res.ph y res.densidad NUNCA serán null si 'res' no es null.
            lista.Add(new ResultadoParametroVm { Parametro = "pH", Resultado = res.ph.ToString("N1"), Unidad = "", Referencia = "5.0-8.0" });
            lista.Add(new ResultadoParametroVm { Parametro = "Densidad", Resultado = res.densidad.ToString("0.000"), Unidad = "", Referencia = "1.005-1.030" });

            // Para los strings (tiras reactivas)
            lista.Add(new ResultadoParametroVm { Parametro = "Leucocitos (Tira)", Resultado = res.leucocitos ?? "-", Unidad = "", Referencia = "Negativo" });
            lista.Add(new ResultadoParametroVm { Parametro = "Nitritos", Resultado = res.nitritos ?? "-", Unidad = "", Referencia = "Negativo" });
            lista.Add(new ResultadoParametroVm { Parametro = "Proteínas", Resultado = res.proteina ?? "-", Unidad = "", Referencia = "Negativo" });
            lista.Add(new ResultadoParametroVm { Parametro = "Glucosa", Resultado = res.glucosa ?? "-", Unidad = "", Referencia = "Negativo" });
            lista.Add(new ResultadoParametroVm { Parametro = "Cetonas", Resultado = res.cetonas ?? "-", Unidad = "", Referencia = "Negativo" });
            lista.Add(new ResultadoParametroVm { Parametro = "Urobilinógeno", Resultado = res.urobilinogeno ?? "-", Unidad = "", Referencia = "Normal" });
            lista.Add(new ResultadoParametroVm { Parametro = "Bilirrubina", Resultado = res.bilirrubinas ?? "-", Unidad = "", Referencia = "Negativo" });
            lista.Add(new ResultadoParametroVm { Parametro = "Sangre (Tira)", Resultado = res.hemoglobina ?? "-", Unidad = "", Referencia = "Negativo" });
            return lista;
        }


        private List<ResultadoParametroVm> MapearResultadosOrinaMicro(examen_orina res)
        {
            // (Sin cambios aquí ya que no usaba ?. en decimales)
            var lista = new List<ResultadoParametroVm>();
            if (res == null) return lista;
            lista.Add(new ResultadoParametroVm { Parametro = "Células Epiteliales", Resultado = res.celulas_epiteliales ?? "-", Unidad = "x c.", Referencia = "Escasas" });
            lista.Add(new ResultadoParametroVm { Parametro = "Bacterias", Resultado = res.bacterias ?? "-", Unidad = "", Referencia = "Escasas" });
            lista.Add(new ResultadoParametroVm { Parametro = "Leucocitos", Resultado = res.leucocitos_micro ?? "-", Unidad = "x c.", Referencia = "0-5" });
            lista.Add(new ResultadoParametroVm { Parametro = "Eritrocitos", Resultado = res.eritrocitos ?? "-", Unidad = "x c.", Referencia = "0-2" });
            lista.Add(new ResultadoParametroVm { Parametro = "Cilindros", Resultado = res.cilindros ?? "-", Unidad = "x c.", Referencia = "Negativo" });
            lista.Add(new ResultadoParametroVm { Parametro = "Cristales", Resultado = res.cristales ?? "-", Unidad = "", Referencia = "Negativo" });
            return lista;
        }

        private List<ResultadoParametroVm> MapearResultadosHeces(examen_heces res)
        {
            // (Sin cambios aquí)
            var lista = new List<ResultadoParametroVm>();
            if (res == null) return lista;
            lista.Add(new ResultadoParametroVm { Parametro = "Color", Resultado = res.color ?? "-", Unidad = "", Referencia = "" });
            lista.Add(new ResultadoParametroVm { Parametro = "Consistencia", Resultado = res.consistencia ?? "-", Unidad = "", Referencia = "" });
            lista.Add(new ResultadoParametroVm { Parametro = "Parásitos", Resultado = res.parasitos ?? "No se observan", Unidad = "", Referencia = "No se observan" });
            return lista;
        }

    } // Fin clase InformeRepository
} // Fin namespace