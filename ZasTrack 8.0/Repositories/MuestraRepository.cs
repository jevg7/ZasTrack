using Npgsql;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ZasTrack.Models;
using ZasTrack.Models.ExamenModel;
using ZasTrack.Models.Informes;

namespace ZasTrack.Repositories
{
    public class MuestraRepository
    {
        private ExamenRepository _examenRepository;
        // --- Constructor de la clase ---
        public MuestraRepository() // O InformeRepository()
        {
            _examenRepository = new ExamenRepository();
            
        }
        public int GuardarMuestras(Muestra muestra)
        {
            string query = @"
        INSERT INTO muestra (fecha_recepcion, id_proyecto, id_paciente, numero_muestra)
        VALUES (@FechaRecepcion, @IdProyecto, @IdPaciente, @NumeroMuestra)
        RETURNING id_muestra";

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FechaRecepcion", muestra.FechaRecepcion);
                        cmd.Parameters.AddWithValue("@IdProyecto", muestra.IdProyecto);
                        cmd.Parameters.AddWithValue("@IdPaciente", muestra.IdPaciente);
                        cmd.Parameters.AddWithValue("@NumeroMuestra", muestra.NumeroMuestra);
                        return Convert.ToInt32(cmd.ExecuteScalar());


                    }
                }            
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error de PostgreSQL: {ex.Message}");
                Console.WriteLine($"Código de error: {ex.SqlState}");
                throw; // Relanza la excepción para que el llamador pueda manejarla
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw; // Relanza la excepción para que el llamador pueda manejarla
            }

        }
        public int ObtenerUltimaMuestra(int idProyecto, DateTime fecha)
        {
            int ultimaMuestra = 0;
            string query = @"
                SELECT COALESCE(MAX(numero_muestra), 0) 
                FROM muestra 
                WHERE id_proyecto = @idProyecto 
                AND fecha_recepcion::DATE = @fecha";

            try
            {
                // Asegúrate de que la conexión se inicialice correctamente
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                        cmd.Parameters.AddWithValue("@fecha", fecha.Date);

                        // Ejecutamos el comando y obtenemos el resultado
                        var result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            ultimaMuestra = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error de PostgreSQL: {ex.Message}");
                Console.WriteLine($"Código de error: {ex.SqlState}");
                throw; // Relanza la excepción para que el llamador pueda manejarla
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw; // Relanza la excepción para que el llamador pueda manejarla
            }
            return ultimaMuestra;
        }

        public async Task<int> CountByProjectAndDateAsync(int idProyecto, DateTime fecha)
        {
            string query = "SELECT COUNT(id_muestra) FROM muestra WHERE id_proyecto = @idProyecto AND fecha_recepcion::date = @fecha";
            int count = 0;
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    cmd.Parameters.AddWithValue("@fecha", fecha.Date);
                    await conn.OpenAsync();
                    var result = await cmd.ExecuteScalarAsync();
                    count = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CountByProjectAndDateAsync (Proyecto: {idProyecto}, Fecha: {fecha.Date}): {ex}");
                // throw;
            }
            return count;
        }

        public async Task<List<MuestraInfoViewModel>> GetUltimasMuestrasPorProyectoAsync(int idProyecto, int limite = 5)
        {
            var ultimasMuestras = new List<MuestraInfoViewModel>();
            string query = @"
        SELECT m.id_muestra, m.numero_muestra, p.nombres || ' ' || p.apellidos AS paciente, m.fecha_recepcion
        FROM muestra m
        INNER JOIN pacientes p ON m.id_paciente = p.id_paciente
        WHERE m.id_proyecto = @idProyecto
        ORDER BY m.fecha_recepcion DESC, m.id_muestra DESC
        LIMIT @limite;";
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    cmd.Parameters.AddWithValue("@limite", limite);
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync()) // Usar ExecuteReaderAsync
                    {
                        while (await reader.ReadAsync()) // Usar ReadAsync
                        {
                            ultimasMuestras.Add(new MuestraInfoViewModel
                            {
                                id_Muestra = reader.GetInt32(reader.GetOrdinal("id_muestra")),
                                NumeroMuestra = reader.GetInt32(reader.GetOrdinal("numero_muestra")),
                                Paciente = reader.GetString(reader.GetOrdinal("paciente")),
                                FechaRecepcion = reader.GetDateTime(reader.GetOrdinal("fecha_recepcion")),
                                // Otros campos vacíos...
                                Genero = "",
                                Edad = 0,
                                ExamenesPendientesStr = "",
                                ExamenesCompletadosStr = ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetUltimasMuestrasPorProyectoAsync (Proyecto: {idProyecto}): {ex}");
                // throw; // O retorna lista vacía
            }
            return ultimasMuestras;
        }
        public async Task<int> ContarPorProyectoIdAsync(int idProyecto)
        {
            string query = "SELECT COUNT(*) FROM muestra WHERE id_proyecto = @idProyecto";
            int count = 0;
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    await conn.OpenAsync();
                    var result = await cmd.ExecuteScalarAsync();
                    count = Convert.ToInt32(result ?? 0L);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en MuestraRepository.ContarPorProyectoIdAsync (ID: {idProyecto}): {ex}");
                throw;
            }
            return count;
        }

        public async Task<List<MuestraDetalleViewModel>> ObtenerDetallesMuestraPorProyectoIdAsync(int idProyecto)
        {
            var listaMuestras = new List<MuestraDetalleViewModel>();
            string query = @"
            SELECT
                m.id_muestra,
                m.numero_muestra,
                m.fecha_recepcion,
                p.codigo_beneficiario,
                p.nombres || ' ' || p.apellidos AS nombre_paciente,

                -- Lógica para Examenes Solicitados (EJEMPLO con STRING_AGG):
                COALESCE( (SELECT STRING_AGG(te.nombre, ', ' ORDER BY te.nombre)
                          FROM muestra_examen me
                          JOIN tipo_examen te ON me.id_tipo_examen = te.id_tipo_examen
                          WHERE me.id_muestra = m.id_muestra), 'Ninguno') AS examenes_solicitados_str,

                -- Lógica para Estado Muestra (EJEMPLO con CASE - ¡NECESITA REFINARSE!):
                -- Este CASE es una simplificación. Una lógica real necesitaría contar
                -- cuántos exámenes se solicitaron vs cuántos tienen procesado=true.
                CASE
                    WHEN NOT EXISTS (SELECT 1 FROM muestra_examen me WHERE me.id_muestra = m.id_muestra) THEN 'Sin Solicitud' -- Raro si se registraron muestras
                    WHEN EXISTS (SELECT 1 FROM examen ex JOIN examen_orina eo ON ex.id_examen = eo.id_examen WHERE ex.id_muestra = m.id_muestra AND eo.procesado = FALSE)
                      OR EXISTS (SELECT 1 FROM examen ex JOIN examen_heces eh ON ex.id_examen = eh.id_examen WHERE ex.id_muestra = m.id_muestra AND eh.procesado = FALSE)
                      OR EXISTS (SELECT 1 FROM examen ex JOIN examen_sangre es ON ex.id_examen = es.id_examen WHERE ex.id_muestra = m.id_muestra AND es.procesado = FALSE)
                      OR NOT EXISTS (SELECT 1 FROM examen ex_proc WHERE ex_proc.id_muestra = m.id_muestra) -- Si no hay ningún examen procesado aún
                      THEN 'Pendiente' -- O 'Parcial' si algunos están procesados?
                    ELSE 'Procesada' -- Asume procesada si no hay pendientes (simplificación)
                END AS estado_muestra_str

            FROM muestra m
            INNER JOIN pacientes p ON m.id_paciente = p.id_paciente
            WHERE m.id_proyecto = @idProyecto
            ORDER BY m.fecha_recepcion DESC, m.numero_muestra DESC;";
            // --- *** FIN CONSULTA SQL IMPORTANTE *** ---

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            listaMuestras.Add(new MuestraDetalleViewModel
                            {
                                IdMuestra = reader.GetInt32(reader.GetOrdinal("id_muestra")),
                                NumeroMuestra = reader.GetInt32(reader.GetOrdinal("numero_muestra")),
                                FechaRecepcion = reader.GetDateTime(reader.GetOrdinal("fecha_recepcion")),
                                CodigoPaciente = reader.GetString(reader.GetOrdinal("codigo_beneficiario")),
                                NombrePaciente = reader.GetString(reader.GetOrdinal("nombre_paciente")),
                                ExamenesSolicitados = reader.GetString(reader.GetOrdinal("examenes_solicitados_str")),
                                EstadoMuestra = reader.GetString(reader.GetOrdinal("estado_muestra_str"))
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en MuestraRepository.ObtenerDetallesMuestraPorProyectoIdAsync (ID: {idProyecto}): {ex}");
                throw; // Relanzar para que la UI sepa
            }
            return listaMuestras;
        }
        public async Task<int> GuardarMuestraCompletaAsync(Muestra muestra, List<int> idsTiposExamen)
        {
            int newMuestraId = 0; // 0 o -1 indicará fallo

            // Abre conexión una sola vez
            await using var conn = DatabaseConnection.GetConnection();
            await conn.OpenAsync();

            // Inicia transacción
            await using var transaction = await conn.BeginTransactionAsync();
            Console.WriteLine("DEBUG: Transacción iniciada para guardar muestra completa.");

            try
            {
                // --- 1. Guardar la Muestra ---
                string queryMuestra = @"
                INSERT INTO muestra (fecha_recepcion, id_proyecto, id_paciente, numero_muestra)
                VALUES (@FechaRecepcion, @IdProyecto, @IdPaciente, @NumeroMuestra)
                RETURNING id_muestra";

                await using (var cmdMuestra = new NpgsqlCommand(queryMuestra, conn, transaction)) // Usa la transacción
                {
                    cmdMuestra.Parameters.AddWithValue("@FechaRecepcion", muestra.FechaRecepcion);
                    cmdMuestra.Parameters.AddWithValue("@IdProyecto", muestra.IdProyecto);
                    cmdMuestra.Parameters.AddWithValue("@IdPaciente", muestra.IdPaciente);
                    cmdMuestra.Parameters.AddWithValue("@NumeroMuestra", muestra.NumeroMuestra);

                    // Ejecutar y obtener el ID generado
                    var result = await cmdMuestra.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        newMuestraId = Convert.ToInt32(result);
                        Console.WriteLine($"DEBUG: Muestra guardada con ID: {newMuestraId}");
                    }
                    else
                    {
                        throw new Exception("No se pudo obtener el ID de la nueva muestra después de insertarla.");
                    }
                }

                // --- 2. Guardar los Vínculos Muestra-Examen (Batch Insert) ---
                if (idsTiposExamen != null && idsTiposExamen.Any())
                {
                    Console.WriteLine($"DEBUG: Intentando vincular {idsTiposExamen.Count} tipos de examen.");
                    // Construir la parte VALUES de la consulta dinámicamente
                    var sqlValues = new List<string>();
                    var parameters = new List<NpgsqlParameter>();
                    parameters.Add(new NpgsqlParameter("@idMuestra", newMuestraId)); // Parámetro común

                    for (int i = 0; i < idsTiposExamen.Count; i++)
                    {
                        string paramName = $"@idTipoExamen{i}";
                        sqlValues.Add($"(@idMuestra, {paramName})"); // Añade (idMuestra, @idTipoExamenN)
                        parameters.Add(new NpgsqlParameter(paramName, idsTiposExamen[i]));
                    }

                    // Construir la consulta completa de batch insert
                    string queryExamenes = $@"
                    INSERT INTO muestra_examen (id_muestra, id_tipo_examen)
                    VALUES {string.Join(", ", sqlValues)}
                    ON CONFLICT (id_muestra, id_tipo_examen) DO NOTHING"; // Ignorar si ya existe el vínculo

                    await using (var cmdExamenes = new NpgsqlCommand(queryExamenes, conn, transaction)) // Usa la transacción
                    {
                        cmdExamenes.Parameters.AddRange(parameters.ToArray());
                        int rowsAffected = await cmdExamenes.ExecuteNonQueryAsync();
                        Console.WriteLine($"DEBUG: Vínculos muestra_examen afectados: {rowsAffected}");
                    }
                }
                else
                {
                    Console.WriteLine("WARN: No se seleccionaron tipos de examen para vincular.");
                    // No es un error necesariamente, pero es bueno saberlo.
                }

                // --- 3. Si todo fue bien, confirmar la transacción ---
                await transaction.CommitAsync();
                Console.WriteLine("DEBUG: Transacción completada (Commit).");
                return newMuestraId; // Devuelve el ID de la muestra guardada
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR GuardarMuestraCompletaAsync: {ex.ToString()}");
                try
                {
                    // Si algo falló, deshacer la transacción
                    await transaction.RollbackAsync();
                    Console.WriteLine("DEBUG: Transacción deshecha (Rollback).");
                }
                catch (Exception exRollback)
                {
                    Console.WriteLine($"ERROR ¡FALLÓ EL ROLLBACK!: {exRollback.ToString()}");
                }
                // Devolver 0 o -1 para indicar fallo, o relanzar la excepción original
                // throw; // Opcional: relanzar si quieres manejarlo más arriba
                return 0; // Indica fallo
            }
            // El using se encarga de cerrar la conexión
        }

    }

}


