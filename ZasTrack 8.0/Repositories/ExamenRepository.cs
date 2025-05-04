using Microsoft.VisualBasic.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using ZasTrack;
using ZasTrack.Forms.Examenes.Debug;
using ZasTrack.Forms.Muestras;
using ZasTrack.Models;
using ZasTrack.Models.ExamenModel;
public class ExamenRepository
{
    public List<MuestraInfoViewModel> ObtenerPacientesPorProyecto(int idProyecto, DateTime fecha, List<int> tiposRequeridos, string textoBusqueda)
{
    string queryBase = @"
        SELECT
            m.id_muestra, m.numero_muestra, p.nombres || ' ' || p.apellidos AS paciente,
            p.genero, p.edad, m.fecha_recepcion,
            COALESCE(
                STRING_AGG(DISTINCT te.nombre, ', ' ORDER BY te.nombre) FILTER (WHERE
                    te.activo = TRUE AND (
                         e.id_examen IS NULL OR
                         (te.id_tipo_examen = 1 AND eo.procesado IS DISTINCT FROM TRUE) OR
                         (te.id_tipo_examen = 2 AND eh.procesado IS DISTINCT FROM TRUE) OR
                         (te.id_tipo_examen = 3 AND es.procesado IS DISTINCT FROM TRUE)
                    )
                ), '[COMPLETADO - NO DEBERIA SALIR]' -- Mensaje si algo falla al filtrar
            ) AS examenes_pendientes_str -- Sigue siendo la lista de PENDIENTES
        FROM muestra m
        INNER JOIN pacientes p ON m.id_paciente = p.id_paciente
        LEFT JOIN muestra_examen me ON m.id_muestra = me.id_muestra
        LEFT JOIN tipo_examen te ON me.id_tipo_examen = te.id_tipo_examen
        LEFT JOIN examen e ON e.id_muestra = me.id_muestra AND e.id_tipo_examen = te.id_tipo_examen
        LEFT JOIN examen_orina eo ON te.id_tipo_examen = 1 AND eo.id_examen = e.id_examen
        LEFT JOIN examen_heces eh ON te.id_tipo_examen = 2 AND eh.id_examen = e.id_examen
        LEFT JOIN examen_sangre es ON te.id_tipo_examen = 3 AND es.id_examen = e.id_examen
        ";

        var whereClauses = new List<string>();
        whereClauses.Add("m.id_proyecto = @idProyecto");
        whereClauses.Add("m.fecha_recepcion = @fechaRecepcion");

        if (tiposRequeridos != null && tiposRequeridos.Any())
        {
            whereClauses.Add(@"EXISTS (
                                SELECT 1 FROM muestra_examen me_f
                                JOIN tipo_examen te_f ON me_f.id_tipo_examen = te_f.id_tipo_examen
                                WHERE me_f.id_muestra = m.id_muestra
                                AND te_f.activo = TRUE
                                AND me_f.id_tipo_examen = ANY(@p_tipos)
                               )");
        }

        if (!string.IsNullOrWhiteSpace(textoBusqueda))
        {
            whereClauses.Add("(p.nombres ILIKE @p_texto OR p.apellidos ILIKE @p_texto OR p.codigo_beneficiario ILIKE @p_texto OR CAST(m.numero_muestra AS TEXT) ILIKE @p_texto)");
        }

        //  condición para asegurar que solo se incluyan muestras
        // para las cuales SÍ existe al menos un examen todavía pendiente.
        whereClauses.Add(@"
        EXISTS (
            SELECT 1 FROM muestra_examen me_pend
            JOIN tipo_examen te_pend ON me_pend.id_tipo_examen = te_pend.id_tipo_examen
            LEFT JOIN examen e_pend ON e_pend.id_muestra = me_pend.id_muestra AND e_pend.id_tipo_examen = me_pend.id_tipo_examen
            LEFT JOIN examen_orina eo_pend ON te_pend.id_tipo_examen = 1 AND eo_pend.id_examen = e_pend.id_examen
            LEFT JOIN examen_heces eh_pend ON te_pend.id_tipo_examen = 2 AND eh_pend.id_examen = e_pend.id_examen
            LEFT JOIN examen_sangre es_pend ON te_pend.id_tipo_examen = 3 AND es_pend.id_examen = e_pend.id_examen
            WHERE me_pend.id_muestra = m.id_muestra AND te_pend.activo = TRUE
              AND ( e_pend.id_examen IS NULL OR
                    (te_pend.id_tipo_examen = 1 AND eo_pend.procesado IS DISTINCT FROM TRUE) OR
                    (te_pend.id_tipo_examen = 2 AND eh_pend.procesado IS DISTINCT FROM TRUE) OR
                    (te_pend.id_tipo_examen = 3 AND es_pend.procesado IS DISTINCT FROM TRUE) )
        )
    ");


        // Construye la query final
        string queryFinal = queryBase + " WHERE " + string.Join(" AND ", whereClauses) +
                            " GROUP BY m.id_muestra, m.numero_muestra, paciente, p.genero, p.edad, m.fecha_recepcion" +
                            " ORDER BY m.numero_muestra;";

        var resultados = new List<MuestraInfoViewModel>();
        try
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(queryFinal, conn))
                {
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    cmd.Parameters.AddWithValue("@fechaRecepcion", fecha.Date);
                    if (tiposRequeridos != null && tiposRequeridos.Any())
                    {
                        cmd.Parameters.AddWithValue("@p_tipos", tiposRequeridos);
                    }
                    if (!string.IsNullOrWhiteSpace(textoBusqueda))
                    {
                        cmd.Parameters.AddWithValue("@p_texto", $"%{textoBusqueda}%");
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Llenado del ViewModel (sin cambios)
                            // La columna examenes_pendientes_str ahora NUNCA debería ser null
                            // porque filtramos las muestras completadas con el EXISTS.
                            resultados.Add(new MuestraInfoViewModel
                            {
                                id_Muestra = reader.GetInt32(reader.GetOrdinal("id_muestra")),
                                NumeroMuestra = reader.GetInt32(reader.GetOrdinal("numero_muestra")),
                                Paciente = reader.GetString(reader.GetOrdinal("paciente")),
                                Genero = reader.GetString(reader.GetOrdinal("genero")),
                                Edad = reader.GetInt32(reader.GetOrdinal("edad")),
                                FechaRecepcion = reader.GetDateTime(reader.GetOrdinal("fecha_recepcion")),
                                ExamenesPendientesStr = reader.GetString(reader.GetOrdinal("examenes_pendientes_str")) // Leer directo
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en ObtenerPacientesPorProyecto MODIFICADO: {ex.Message}\nQuery: {queryFinal}"); // Loguea la query para depurar
            throw;
        }
        return resultados;
    }
  
    public List<MuestraInfoViewModel> ObtenerPacientesProcesados(int idProyecto, DateTime fecha, List<int> tiposRequeridos, string textoBusqueda)
    {
        // La base de la consulta incluye el STRING_AGG para COMPLETADOS
        string queryBase = @"
    SELECT
        m.id_muestra, m.numero_muestra, p.nombres || ' ' || p.apellidos AS paciente,
        p.genero, p.edad, m.fecha_recepcion,
        COALESCE(
            STRING_AGG(DISTINCT te.nombre, ', ' ORDER BY te.nombre) FILTER (WHERE
                te.activo = TRUE AND (
                     (te.id_tipo_examen = 1 AND eo.procesado = TRUE) OR
                     (te.id_tipo_examen = 2 AND eh.procesado = TRUE) OR
                     (te.id_tipo_examen = 3 AND es.procesado = TRUE)
                )
            ), '[Ninguno?]'
        ) AS examenes_completados_str
    FROM muestra m
    INNER JOIN pacientes p ON m.id_paciente = p.id_paciente
    LEFT JOIN muestra_examen me ON m.id_muestra = me.id_muestra
    LEFT JOIN tipo_examen te ON me.id_tipo_examen = te.id_tipo_examen
    LEFT JOIN examen e ON e.id_muestra = m.id_muestra AND e.id_tipo_examen = te.id_tipo_examen
    LEFT JOIN examen_orina eo ON te.id_tipo_examen = 1 AND eo.id_examen = e.id_examen
    LEFT JOIN examen_heces eh ON te.id_tipo_examen = 2 AND eh.id_examen = e.id_examen
    LEFT JOIN examen_sangre es ON te.id_tipo_examen = 3 AND es.id_examen = e.id_examen
    ";

        var whereClauses = new List<string>();
        // Filtros estándar
        whereClauses.Add("m.id_proyecto = @idProyecto");
        whereClauses.Add("m.fecha_recepcion = @fechaRecepcion");

        // Filtro por tipo REQUERIDO (si se marca un checkbox, muestra procesados que lo incluían)
        if (tiposRequeridos != null && tiposRequeridos.Any())
        {
            whereClauses.Add(@"EXISTS (
                            SELECT 1 FROM muestra_examen me_f
                            JOIN tipo_examen te_f ON me_f.id_tipo_examen = te_f.id_tipo_examen
                            WHERE me_f.id_muestra = m.id_muestra
                            AND te_f.activo = TRUE
                            AND me_f.id_tipo_examen = ANY(@p_tipos)
                           )");
        }

        // Filtro por texto de búsqueda
        if (!string.IsNullOrWhiteSpace(textoBusqueda))
        {
            whereClauses.Add("(p.nombres ILIKE @p_texto OR p.apellidos ILIKE @p_texto OR p.codigo_beneficiario ILIKE @p_texto OR CAST(m.numero_muestra AS TEXT) ILIKE @p_texto)");
        }

        // ***** CONDICIÓN CLAVE PARA ESTA VISTA *****
        // Incluir si EXISTE al menos UN examen PROCESADO
        whereClauses.Add(@"
        EXISTS (
            SELECT 1
            FROM examen e_proc -- Busca directamente en examen (que implica que hay resultado)
            LEFT JOIN examen_orina eo_proc ON e_proc.id_tipo_examen = 1 AND eo_proc.id_examen = e_proc.id_examen
            LEFT JOIN examen_heces eh_proc ON e_proc.id_tipo_examen = 2 AND eh_proc.id_examen = e_proc.id_examen
            LEFT JOIN examen_sangre es_proc ON e_proc.id_tipo_examen = 3 AND es_proc.id_examen = e_proc.id_examen
            WHERE e_proc.id_muestra = m.id_muestra -- Vincula con la muestra externa 'm'
              AND ( -- Condición de PROCESADO
                    (e_proc.id_tipo_examen = 1 AND eo_proc.procesado = TRUE) OR
                    (e_proc.id_tipo_examen = 2 AND eh_proc.procesado = TRUE) OR
                    (e_proc.id_tipo_examen = 3 AND es_proc.procesado = TRUE)
                  )
        )
    ");
        // ***** FIN CONDICIÓN CLAVE *****

        // ¡Ya NO necesitamos la condición NOT EXISTS (pending) aquí!

        // Construcción final (CON GROUP BY porque usamos STRING_AGG)
        string queryFinal = queryBase + " WHERE " + string.Join(" AND ", whereClauses) +
                            " GROUP BY m.id_muestra, m.numero_muestra, paciente, p.genero, p.edad, m.fecha_recepcion" +
                            " ORDER BY m.numero_muestra;";

        var resultados = new List<MuestraInfoViewModel>();
        try
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(queryFinal, conn))
                {
                    // Parámetros (igual que antes)
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    cmd.Parameters.AddWithValue("@fechaRecepcion", fecha.Date);
                    if (tiposRequeridos != null && tiposRequeridos.Any()) { cmd.Parameters.AddWithValue("@p_tipos", tiposRequeridos); }
                    if (!string.IsNullOrWhiteSpace(textoBusqueda)) { cmd.Parameters.AddWithValue("@p_texto", $"%{textoBusqueda}%"); }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Llenado del ViewModel
                            resultados.Add(new MuestraInfoViewModel
                            {
                                id_Muestra = reader.GetInt32(reader.GetOrdinal("id_muestra")),
                                NumeroMuestra = reader.GetInt32(reader.GetOrdinal("numero_muestra")),
                                Paciente = reader.GetString(reader.GetOrdinal("paciente")),
                                Genero = reader.GetString(reader.GetOrdinal("genero")),
                                Edad = reader.GetInt32(reader.GetOrdinal("edad")),
                                FechaRecepcion = reader.GetDateTime(reader.GetOrdinal("fecha_recepcion")),
                                // Lee la columna con la lista de completados
                                ExamenesCompletadosStr = reader.IsDBNull(reader.GetOrdinal("examenes_completados_str")) ? "" : reader.GetString(reader.GetOrdinal("examenes_completados_str")),
                                // ExamenesPendientesStr se puede omitir o poner vacío
                                ExamenesPendientesStr = "" // O null, según tu ViewModel
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex) { Console.WriteLine($"Error en ObtenerPacientesProcesados MODIFICADO: {ex.Message}\nQuery: {queryFinal}"); throw; }
        return resultados;
    }
    public int? ObtenerIdPacientePorMuestra(int id_Muestra)
    {
        string query = "SELECT id_paciente FROM muestra WHERE id_muestra = @id_Muestra LIMIT 1;";
        try
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_Muestra", id_Muestra);
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener idPaciente para muestra {id_Muestra}: {ex.Message}");
            // Podrías lanzar una excepción o devolver null
        }
        return null; // Devuelve null si no se encuentra o hay error
    }
    public List<tipo_examen> ObtenerTiposExamenPendientesPorMuestra(int id_Muestra)
    {
        var tiposPendientes = new List<tipo_examen>();
        // Esta consulta busca los tipos de examen asociados a la muestra
        // para los cuales AÚN NO existe un resultado procesado.
        string query = @"
            SELECT DISTINCT te.id_tipo_examen, te.nombre, te.activo
            FROM muestra_examen me
            JOIN tipo_examen te ON me.id_tipo_examen = te.id_tipo_examen
            LEFT JOIN examen e ON e.id_muestra = me.id_muestra AND e.id_tipo_examen = me.id_tipo_examen
            LEFT JOIN examen_orina eo ON te.id_tipo_examen = 1 AND eo.id_examen = e.id_examen
            LEFT JOIN examen_heces eh ON te.id_tipo_examen = 2 AND eh.id_examen = e.id_examen
            LEFT JOIN examen_sangre es ON te.id_tipo_examen = 3 AND es.id_examen = e.id_examen
            WHERE me.id_muestra = @id_Muestra
              AND te.activo = TRUE -- Solo consideramos tipos de examen activos
              AND ( -- Condición para estar PENDIENTE
                    e.id_examen IS NULL OR
                    (te.id_tipo_examen = 1 AND eo.procesado IS DISTINCT FROM TRUE) OR
                    (te.id_tipo_examen = 2 AND eh.procesado IS DISTINCT FROM TRUE) OR
                    (te.id_tipo_examen = 3 AND es.procesado IS DISTINCT FROM TRUE)
                  )
            ORDER BY te.id_tipo_examen; -- Opcional: ordenar pestañas
        ";

        try
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_Muestra", id_Muestra);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tiposPendientes.Add(new tipo_examen
                            {
                                id_tipo_examen = reader.GetInt32(reader.GetOrdinal("id_tipo_examen")),
                                nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                activo = reader.GetBoolean(reader.GetOrdinal("activo"))
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener tipos pendientes para muestra {id_Muestra}: {ex.Message}");
            // Podrías relanzar o manejar el error como prefieras
            throw;
        }
        return tiposPendientes;
    }     
    public List<tipo_examen> ObtenerTiposExamenProcesadosPorMuestra(int id_Muestra)
    {
        var tiposProcesados = new List<tipo_examen>();
        // Seleccionamos los tipos de examen que tienen una entrada correspondiente
        // en la tabla 'examen' Y cuyo estado 'procesado' asociado es TRUE.
        string query = @"
            SELECT DISTINCT te.id_tipo_examen, te.nombre, te.activo
            FROM examen e
            INNER JOIN tipo_examen te ON e.id_tipo_examen = te.id_tipo_examen
            LEFT JOIN examen_orina eo ON te.id_tipo_examen = 1 AND eo.id_examen = e.id_examen
            LEFT JOIN examen_heces eh ON te.id_tipo_examen = 2 AND eh.id_examen = e.id_examen
            LEFT JOIN examen_sangre es ON te.id_tipo_examen = 3 AND es.id_examen = e.id_examen
            WHERE e.id_muestra = @id_Muestra
              AND te.activo = TRUE
              AND ( -- Condición para estar PROCESADO
                    (te.id_tipo_examen = 1 AND eo.procesado = TRUE) OR
                    (te.id_tipo_examen = 2 AND eh.procesado = TRUE) OR
                    (te.id_tipo_examen = 3 AND es.procesado = TRUE)
                  )
            ORDER BY te.id_tipo_examen;
        ";

        try
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_Muestra", id_Muestra);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tiposProcesados.Add(new tipo_examen // Usa el nombre correcto de tu clase
                            {
                                id_tipo_examen = reader.GetInt32(reader.GetOrdinal("id_tipo_examen")),
                                nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                activo = reader.GetBoolean(reader.GetOrdinal("activo"))
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener tipos procesados para muestra {id_Muestra}: {ex.Message}");
            throw; // Relanza para que la capa superior se entere
        }
        return tiposProcesados;
    }
    public async Task<Dictionary<string, int>> CountPendientesByTypeByProjectAsync(int idProyecto)
    {
        var counts = new Dictionary<string, int>();
        // La query es compleja, asumimos que es la misma que tenías
        string query = @"
        SELECT te.nombre, COUNT(DISTINCT me.id_muestra)
        FROM tipo_examen te
        INNER JOIN muestra_examen me ON te.id_tipo_examen = me.id_tipo_examen
        INNER JOIN muestra m ON me.id_muestra = m.id_muestra
        LEFT JOIN examen e ON e.id_muestra = me.id_muestra AND e.id_tipo_examen = te.id_tipo_examen
        LEFT JOIN examen_orina eo ON te.id_tipo_examen = 1 AND eo.id_examen = e.id_examen
        LEFT JOIN examen_heces eh ON te.id_tipo_examen = 2 AND eh.id_examen = e.id_examen
        LEFT JOIN examen_sangre es ON te.id_tipo_examen = 3 AND es.id_examen = e.id_examen
        WHERE m.id_proyecto = @idProyecto AND te.activo = TRUE AND (
              e.id_examen IS NULL OR
              (te.id_tipo_examen = 1 AND eo.procesado IS DISTINCT FROM TRUE) OR
              (te.id_tipo_examen = 2 AND eh.procesado IS DISTINCT FROM TRUE) OR
              (te.id_tipo_examen = 3 AND es.procesado IS DISTINCT FROM TRUE) )
        GROUP BY te.nombre ORDER BY te.nombre;";
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
                        counts.Add(reader.GetString(0), Convert.ToInt32(reader.GetInt64(1)));
                    }
                }
            }
        }
        catch (Exception ex) { Console.WriteLine($"Error en CountPendientesByTypeByProjectAsync: {ex}"); throw; }
        return counts;
    }
    public async Task<int> CountProcesadosByProjectAndDateAsync(int idProyecto, DateTime fecha)
    {
        // Query igual a la versión síncrona
        string query = @"
        SELECT COUNT(e.id_examen) FROM examen e
        INNER JOIN muestra m ON e.id_muestra = m.id_muestra
        INNER JOIN tipo_examen te ON e.id_tipo_examen = te.id_tipo_examen
        LEFT JOIN examen_orina eo ON te.id_tipo_examen = 1 AND eo.id_examen = e.id_examen
        LEFT JOIN examen_heces eh ON te.id_tipo_examen = 2 AND eh.id_examen = e.id_examen
        LEFT JOIN examen_sangre es ON te.id_tipo_examen = 3 AND es.id_examen = e.id_examen
        WHERE m.id_proyecto = @idProyecto AND e.fecha_procesamiento::date = @fecha
          AND te.activo = TRUE AND (
             (te.id_tipo_examen = 1 AND eo.procesado = TRUE) OR
             (te.id_tipo_examen = 2 AND eh.procesado = TRUE) OR
             (te.id_tipo_examen = 3 AND es.procesado = TRUE) );";
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
        catch (Exception ex) { Console.WriteLine($"Error en CountProcesadosByProjectAndDateAsync: {ex}"); throw; }
        return count;
    }
    
    public async Task<int> ContarProcesadosPorProyectoIdAsync(int idProyecto)
    {
        // Contamos los registros en la tabla 'examen' que pertenecen al proyecto
        // Y tienen una entrada correspondiente en alguna tabla examen_* con procesado = TRUE
        string query = @"
            SELECT COUNT(DISTINCT e.id_examen)
            FROM examen e
            INNER JOIN muestra m ON e.id_muestra = m.id_muestra
            LEFT JOIN examen_orina eo ON e.id_tipo_examen = 1 AND eo.id_examen = e.id_examen
            LEFT JOIN examen_heces eh ON e.id_tipo_examen = 2 AND eh.id_examen = e.id_examen
            LEFT JOIN examen_sangre es ON e.id_tipo_examen = 3 AND es.id_examen = e.id_examen
            WHERE m.id_proyecto = @idProyecto
              AND ( (e.id_tipo_examen = 1 AND eo.procesado = TRUE) OR
                    (e.id_tipo_examen = 2 AND eh.procesado = TRUE) OR
                    (e.id_tipo_examen = 3 AND es.procesado = TRUE) );";
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
            Console.WriteLine($"Error en ExamenRepository.ContarProcesadosPorProyectoIdAsync (ID: {idProyecto}): {ex}");
            throw;
        }
        return count;
    }
    public async Task<List<ExamenProcesadoInfo>> GetUltimosExamenesProcesadosPorProyectoAsync(int idProyecto, int limite = 5)
    {
        var ultimosExamenes = new List<ExamenProcesadoInfo>();
        // Query igual a la versión síncrona
        string query = @"
        SELECT m.numero_muestra, p.nombres || ' ' || p.apellidos AS paciente,
               te.nombre AS tipo_examen, e.fecha_procesamiento
        FROM examen e
        INNER JOIN muestra m ON e.id_muestra = m.id_muestra
        INNER JOIN pacientes p ON m.id_paciente = p.id_paciente
        INNER JOIN tipo_examen te ON e.id_tipo_examen = te.id_tipo_examen
        LEFT JOIN examen_orina eo ON te.id_tipo_examen = 1 AND eo.id_examen = e.id_examen
        LEFT JOIN examen_heces eh ON te.id_tipo_examen = 2 AND eh.id_examen = e.id_examen
        LEFT JOIN examen_sangre es ON te.id_tipo_examen = 3 AND es.id_examen = e.id_examen
        WHERE m.id_proyecto = @idProyecto AND (
             (te.id_tipo_examen = 1 AND eo.procesado = TRUE) OR
             (te.id_tipo_examen = 2 AND eh.procesado = TRUE) OR
             (te.id_tipo_examen = 3 AND es.procesado = TRUE) )
        ORDER BY e.fecha_procesamiento DESC, e.id_examen DESC LIMIT @limite;";
        try
        {
            using (var conn = DatabaseConnection.GetConnection())
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                cmd.Parameters.AddWithValue("@limite", limite);
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ultimosExamenes.Add(new ExamenProcesadoInfo
                        {
                            NumeroMuestra = reader.GetInt32(reader.GetOrdinal("numero_muestra")),
                            Paciente = reader.GetString(reader.GetOrdinal("paciente")),
                            TipoExamen = reader.GetString(reader.GetOrdinal("tipo_examen")),
                            FechaProcesamiento = reader.GetDateTime(reader.GetOrdinal("fecha_procesamiento"))
                        });
                    }
                }
            }
        }
        catch (Exception ex) { Console.WriteLine($"Error en GetUltimosExamenesProcesadosPorProyectoAsync: {ex}"); throw; }
        return ultimosExamenes;
    }   
    public async Task<List<ExamenDetalleViewModel>> ObtenerProcesadosPorProyectoIdAsync(int idProyecto)
    {
        var listaExamenes = new List<ExamenDetalleViewModel>();

        // --- QUERY CORREGIDA ---
        string query = @"
        SELECT
            e.id_examen, e.id_paciente, e.id_muestra,
            m.numero_muestra, m.fecha_recepcion,
            p.nombres || ' ' || p.apellidos AS nombre_paciente,
            te.nombre AS tipo_examen,
            e.fecha_procesamiento
        FROM examen e
        INNER JOIN muestra m ON e.id_muestra = m.id_muestra
        INNER JOIN pacientes p ON e.id_paciente = p.id_paciente
        INNER JOIN tipo_examen te ON e.id_tipo_examen = te.id_tipo_examen
        -- Condiciones ON corregidas para los LEFT JOIN:
        LEFT JOIN examen_orina eo ON e.id_tipo_examen = 1 AND eo.id_examen = e.id_examen
        LEFT JOIN examen_heces eh ON e.id_tipo_examen = 2 AND eh.id_examen = e.id_examen
        LEFT JOIN examen_sangre es ON e.id_tipo_examen = 3 AND es.id_examen = e.id_examen
        WHERE m.id_proyecto = @idProyecto
          AND ( (e.id_tipo_examen = 1 AND eo.procesado = TRUE) OR
                (e.id_tipo_examen = 2 AND eh.procesado = TRUE) OR
                (e.id_tipo_examen = 3 AND es.procesado = TRUE) )
        ORDER BY e.fecha_procesamiento DESC;";
        // --- FIN QUERY CORREGIDA ---

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
                        listaExamenes.Add(new ExamenDetalleViewModel
                        {
                            IdExamen = reader.GetInt32(reader.GetOrdinal("id_examen")),
                            IdMuestra = reader.GetInt32(reader.GetOrdinal("id_muestra")),
                            IdPaciente = reader.GetInt32(reader.GetOrdinal("id_paciente")),
                            NumeroMuestra = reader.GetInt32(reader.GetOrdinal("numero_muestra")),
                            NombrePaciente = reader.GetString(reader.GetOrdinal("nombre_paciente")),
                            TipoExamen = reader.GetString(reader.GetOrdinal("tipo_examen")),
                            FechaProcesamiento = reader.GetDateTime(reader.GetOrdinal("fecha_procesamiento")),
                            FechaRecepcion = reader.GetDateTime(reader.GetOrdinal("fecha_recepcion")),
                            Estado = "Procesado"
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en ExamenRepository.ObtenerProcesadosPorProyectoIdAsync (ID: {idProyecto}): {ex}");
            throw;
        }
        return listaExamenes;
    }

    public async Task<List<MuestraInfoViewModel>> ObtenerMuestrasConExamenesCompletadosPorProyectoIdAsync(int idProyecto)
    {
        var resultados = new List<MuestraInfoViewModel>();
        
        string query = @"
            SELECT
                m.id_muestra, m.numero_muestra, p.nombres || ' ' || p.apellidos AS paciente,
                p.genero, p.edad, m.fecha_recepcion,
                COALESCE(
                    STRING_AGG(DISTINCT te.nombre, ', ' ORDER BY te.nombre) FILTER (WHERE
                        te.activo = TRUE AND (
                             (te.id_tipo_examen = 1 AND eo.procesado = TRUE) OR
                             (te.id_tipo_examen = 2 AND eh.procesado = TRUE) OR
                             (te.id_tipo_examen = 3 AND es.procesado = TRUE)
                        )
                    ), '[Ninguno Procesado]' -- Mensaje si no hay ninguno procesado para esta muestra
                ) AS examenes_completados_str
            FROM muestra m
            INNER JOIN pacientes p ON m.id_paciente = p.id_paciente
            LEFT JOIN muestra_examen me ON m.id_muestra = me.id_muestra
            LEFT JOIN tipo_examen te ON me.id_tipo_examen = te.id_tipo_examen
            LEFT JOIN examen e ON e.id_muestra = m.id_muestra AND e.id_tipo_examen = te.id_tipo_examen
            LEFT JOIN examen_orina eo ON te.id_tipo_examen = 1 AND eo.id_examen = e.id_examen
            LEFT JOIN examen_heces eh ON te.id_tipo_examen = 2 AND eh.id_examen = e.id_examen
            LEFT JOIN examen_sangre es ON te.id_tipo_examen = 3 AND es.id_examen = e.id_examen
            WHERE
                m.id_proyecto = @idProyecto
                AND EXISTS ( -- Asegura que al menos UNO esté procesado
                    SELECT 1
                    FROM examen e_proc
                    LEFT JOIN examen_orina eo_proc ON e_proc.id_tipo_examen = 1 AND eo_proc.id_examen = e_proc.id_examen
                    LEFT JOIN examen_heces eh_proc ON e_proc.id_tipo_examen = 2 AND eh_proc.id_examen = e_proc.id_examen
                    LEFT JOIN examen_sangre es_proc ON e_proc.id_tipo_examen = 3 AND es_proc.id_examen = e_proc.id_examen
                    WHERE e_proc.id_muestra = m.id_muestra
                      AND ( (e_proc.id_tipo_examen = 1 AND eo_proc.procesado = TRUE) OR
                            (e_proc.id_tipo_examen = 2 AND eh_proc.procesado = TRUE) OR
                            (e_proc.id_tipo_examen = 3 AND es_proc.procesado = TRUE) )
                )
            GROUP BY m.id_muestra, m.numero_muestra, paciente, p.genero, p.edad, m.fecha_recepcion
            ORDER BY m.fecha_recepcion DESC, m.numero_muestra DESC;"; // Ordenar por fecha o número

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
                        resultados.Add(new MuestraInfoViewModel
                        {
                            id_Muestra = reader.GetInt32(reader.GetOrdinal("id_muestra")),
                            NumeroMuestra = reader.GetInt32(reader.GetOrdinal("numero_muestra")),
                            Paciente = reader.GetString(reader.GetOrdinal("paciente")),
                            Genero = reader.GetString(reader.GetOrdinal("genero")),
                            Edad = reader.GetInt32(reader.GetOrdinal("edad")),
                            FechaRecepcion = reader.GetDateTime(reader.GetOrdinal("fecha_recepcion")),
                            ExamenesCompletadosStr = reader.GetString(reader.GetOrdinal("examenes_completados_str")),
                            ExamenesPendientesStr = "" // No aplica en esta vista
                        });
                    }
                }
            }
        }
        catch (Exception ex) { Console.WriteLine($"Error en ObtenerMuestrasConExamenesCompletadosPorProyectoIdAsync: {ex}"); throw; }
        return resultados;
    }

    #region Guardar Resultados
    public bool GuardarResultadoHeces(examen_heces datosHeces, int id_Muestra, int idPaciente)
    {
        int idExamen;
        const int ID_TIPO_EXAMEN_HECES = 2; // Asumiendo ID 2 = Heces

        using (var conn = DatabaseConnection.GetConnection())
        {
            conn.Open();
            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    // PASO 1: Buscar o Crear cabecera 'examen' (Idéntico a Orina, solo cambia ID_TIPO_EXAMEN)
                    string checkQuery = "SELECT id_examen FROM examen WHERE id_muestra = @id_Muestra AND id_tipo_examen = @idTipoExamen";
                    int? examenExistenteId = null;
                    using (var checkCmd = new NpgsqlCommand(checkQuery, conn, transaction))
                    {
                        checkCmd.Parameters.AddWithValue("@id_Muestra", id_Muestra);
                        checkCmd.Parameters.AddWithValue("@idTipoExamen", ID_TIPO_EXAMEN_HECES); // <- Usa ID Heces
                        var result = checkCmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value) examenExistenteId = Convert.ToInt32(result);
                    }

                    if (examenExistenteId.HasValue)
                    {
                        idExamen = examenExistenteId.Value;
                        string updateExamenQuery = "UPDATE examen SET fecha_procesamiento = @fechaProc WHERE id_examen = @idExamen";
                        using (var updateCmd = new NpgsqlCommand(updateExamenQuery, conn, transaction))
                        {
                            updateCmd.Parameters.AddWithValue("@fechaProc", DateTime.Now);
                            updateCmd.Parameters.AddWithValue("@idExamen", idExamen);
                            updateCmd.ExecuteNonQuery();
                        }
                        Console.WriteLine($"DEBUG: Registro examen {idExamen} actualizado (Heces).");
                    }
                    else
                    {
                        string insertExamenQuery = @"INSERT INTO examen (id_paciente, id_tipo_examen, id_muestra, fecha_procesamiento, fecha_recepcion)
                                                     VALUES (@idPaciente, @idTipoExamen, @id_Muestra, @fechaProc, (SELECT fecha_recepcion FROM muestra WHERE id_muestra = @id_Muestra LIMIT 1))
                                                     RETURNING id_examen;";
                        using (var insertCmd = new NpgsqlCommand(insertExamenQuery, conn, transaction))
                        {
                            insertCmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                            insertCmd.Parameters.AddWithValue("@idTipoExamen", ID_TIPO_EXAMEN_HECES); // <- Usa ID Heces
                            insertCmd.Parameters.AddWithValue("@id_Muestra", id_Muestra);
                            insertCmd.Parameters.AddWithValue("@fechaProc", DateTime.Now);
                            idExamen = Convert.ToInt32(insertCmd.ExecuteScalar());
                        }
                        Console.WriteLine($"DEBUG: Registro examen {idExamen} creado (Heces).");
                    }

                    // PASO 2: Insertar o Actualizar resultados en 'examen_heces'
                    string insertHecesQuery = @"
                        INSERT INTO examen_heces (
                            id_examen, color, consistencia, parasitos, observacion, procesado
                        ) VALUES (
                            @idExamen, @color, @consistencia, @parasitos, @observacion, TRUE
                        )
                        ON CONFLICT (id_examen) DO UPDATE SET
                            color = EXCLUDED.color, consistencia = EXCLUDED.consistencia,
                            parasitos = EXCLUDED.parasitos, observacion = EXCLUDED.observacion,
                            procesado = TRUE; -- Asegura que quede procesado
                        ";

                    using (var cmdHeces = new NpgsqlCommand(insertHecesQuery, conn, transaction))
                    {
                        cmdHeces.Parameters.AddWithValue("@idExamen", idExamen);
                        cmdHeces.Parameters.AddWithValue("@color", (object)datosHeces.color ?? DBNull.Value);
                        cmdHeces.Parameters.AddWithValue("@consistencia", (object)datosHeces.consistencia ?? DBNull.Value);
                        cmdHeces.Parameters.AddWithValue("@parasitos", (object)datosHeces.parasitos ?? DBNull.Value); // Ahora es string
                        cmdHeces.Parameters.AddWithValue("@observacion", (object)datosHeces.observacion ?? DBNull.Value);
                        // procesado = TRUE está en la query

                        cmdHeces.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    Console.WriteLine($"DEBUG: Resultados Heces para examen {idExamen} guardados.");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR al guardar resultado de Heces: {ex.Message}");
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
    public bool GuardarResultadoSangre(examen_sangre datosSangre, int id_Muestra, int idPaciente)
    {
        int idExamen;
        const int ID_TIPO_EXAMEN_SANGRE = 3; // Asumiendo ID 3 = Sangre

        using (var conn = DatabaseConnection.GetConnection())
        {
            conn.Open();
            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    // PASO 1: Buscar o Crear cabecera 'examen' (Idéntico a Orina, solo cambia ID_TIPO_EXAMEN)
                    string checkQuery = "SELECT id_examen FROM examen WHERE id_muestra = @id_Muestra AND id_tipo_examen = @idTipoExamen";
                    int? examenExistenteId = null;
                    using (var checkCmd = new NpgsqlCommand(checkQuery, conn, transaction))
                    {
                        checkCmd.Parameters.AddWithValue("@id_Muestra", id_Muestra);
                        checkCmd.Parameters.AddWithValue("@idTipoExamen", ID_TIPO_EXAMEN_SANGRE); // <- Usa ID Sangre
                        var result = checkCmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value) examenExistenteId = Convert.ToInt32(result);
                    }

                    if (examenExistenteId.HasValue)
                    {
                        idExamen = examenExistenteId.Value;
                        string updateExamenQuery = "UPDATE examen SET fecha_procesamiento = @fechaProc WHERE id_examen = @idExamen";
                        using (var updateCmd = new NpgsqlCommand(updateExamenQuery, conn, transaction))
                        {
                            updateCmd.Parameters.AddWithValue("@fechaProc", DateTime.Now);
                            updateCmd.Parameters.AddWithValue("@idExamen", idExamen);
                            updateCmd.ExecuteNonQuery();
                        }
                        Console.WriteLine($"DEBUG: Registro examen {idExamen} actualizado (Sangre).");
                    }
                    else
                    {
                        string insertExamenQuery = @"INSERT INTO examen (id_paciente, id_tipo_examen, id_muestra, fecha_procesamiento, fecha_recepcion)
                                                     VALUES (@idPaciente, @idTipoExamen, @id_Muestra, @fechaProc, (SELECT fecha_recepcion FROM muestra WHERE id_muestra = @id_Muestra LIMIT 1))
                                                     RETURNING id_examen;";
                        using (var insertCmd = new NpgsqlCommand(insertExamenQuery, conn, transaction))
                        {
                            insertCmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                            insertCmd.Parameters.AddWithValue("@idTipoExamen", ID_TIPO_EXAMEN_SANGRE); // <- Usa ID Sangre
                            insertCmd.Parameters.AddWithValue("@id_Muestra", id_Muestra);
                            insertCmd.Parameters.AddWithValue("@fechaProc", DateTime.Now);
                            idExamen = Convert.ToInt32(insertCmd.ExecuteScalar());
                        }
                        Console.WriteLine($"DEBUG: Registro examen {idExamen} creado (Sangre).");
                    }

                    // PASO 2: Insertar o Actualizar resultados en 'examen_sangre'
                    string insertSangreQuery = @"
                        INSERT INTO examen_sangre (
                            id_examen, globulos_rojos, hematocrito, hemoglobina, leucocitos,
                            mcv, mch, mchc, neutrofilos, linfocitos, monocitos, eosinofilos,
                            basofilos, observacion, procesado
                        ) VALUES (
                            @idExamen, @gr, @hto, @hb, @leuco, @mcv, @mch, @mchc,
                            @neu, @lin, @mono, @eos, @bas, @obs, TRUE
                        )
                        ON CONFLICT (id_examen) DO UPDATE SET
                            globulos_rojos = EXCLUDED.globulos_rojos, hematocrito = EXCLUDED.hematocrito,
                            hemoglobina = EXCLUDED.hemoglobina, leucocitos = EXCLUDED.leucocitos,
                            mcv = EXCLUDED.mcv, mch = EXCLUDED.mch, mchc = EXCLUDED.mchc,
                            neutrofilos = EXCLUDED.neutrofilos, linfocitos = EXCLUDED.linfocitos,
                            monocitos = EXCLUDED.monocitos, eosinofilos = EXCLUDED.eosinofilos,
                            basofilos = EXCLUDED.basofilos, observacion = EXCLUDED.observacion,
                            procesado = TRUE; -- Asegura que quede procesado
                        ";

                    using (var cmdSangre = new NpgsqlCommand(insertSangreQuery, conn, transaction))
                    {
                        // Añadir todos los parámetros desde el objeto datosSangre
                        cmdSangre.Parameters.AddWithValue("@idExamen", idExamen);
                        cmdSangre.Parameters.AddWithValue("@gr", datosSangre.globulos_rojos);
                        cmdSangre.Parameters.AddWithValue("@hto", datosSangre.hematocrito);
                        cmdSangre.Parameters.AddWithValue("@hb", datosSangre.hemoglobina);
                        cmdSangre.Parameters.AddWithValue("@leuco", datosSangre.leucocitos);
                        cmdSangre.Parameters.AddWithValue("@mcv", datosSangre.mcv);
                        cmdSangre.Parameters.AddWithValue("@mch", datosSangre.mch);
                        cmdSangre.Parameters.AddWithValue("@mchc", datosSangre.mchc);
                        cmdSangre.Parameters.AddWithValue("@neu", datosSangre.neutrofilos);
                        cmdSangre.Parameters.AddWithValue("@lin", datosSangre.linfocitos);
                        cmdSangre.Parameters.AddWithValue("@mono", datosSangre.monocitos);
                        cmdSangre.Parameters.AddWithValue("@eos", datosSangre.eosinofilos);
                        cmdSangre.Parameters.AddWithValue("@bas", datosSangre.basofilos);
                        cmdSangre.Parameters.AddWithValue("@obs", (object)datosSangre.observacion ?? DBNull.Value);
                        // procesado = TRUE está en la query

                        cmdSangre.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    Console.WriteLine($"DEBUG: Resultados Sangre para examen {idExamen} guardados.");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR al guardar resultado de Sangre: {ex.Message}");
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
    public bool GuardarResultadoOrina(examen_orina datosOrina, int id_Muestra, int idPaciente)
    {
        int idExamen; // Guardará el ID del registro 'examen' (nuevo o existente)
        const int ID_TIPO_EXAMEN_ORINA = 1; // Asumiendo que 1 es Orina

        // Usamos una transacción para asegurar que todo se guarde o nada se guarde
        using (var conn = DatabaseConnection.GetConnection())
        {
            conn.Open();
            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    // PASO 1: Buscar o Crear el registro cabecera en la tabla 'examen'
                    //         Asociado a esta muestra Y este tipo de examen.
                    string checkQuery = "SELECT id_examen FROM examen WHERE id_muestra = @id_Muestra AND id_tipo_examen = @idTipoExamen";
                    int? examenExistenteId = null;
                    using (var checkCmd = new NpgsqlCommand(checkQuery, conn, transaction)) // Ejecuta dentro de la transacción
                    {
                        checkCmd.Parameters.AddWithValue("@id_Muestra", id_Muestra);
                        checkCmd.Parameters.AddWithValue("@idTipoExamen", ID_TIPO_EXAMEN_ORINA);
                        var result = checkCmd.ExecuteScalar(); // Devuelve la primera columna de la primera fila, o null si no hay filas
                        if (result != null && result != DBNull.Value)
                        {
                            examenExistenteId = Convert.ToInt32(result);
                        }
                    }

                    if (examenExistenteId.HasValue)
                    {
                        // Si ya existe, usamos su ID y podríamos actualizar la fecha de procesamiento
                        idExamen = examenExistenteId.Value;
                        string updateExamenQuery = "UPDATE examen SET fecha_procesamiento = @fechaProc WHERE id_examen = @idExamen";
                        using (var updateCmd = new NpgsqlCommand(updateExamenQuery, conn, transaction))
                        {
                            updateCmd.Parameters.AddWithValue("@fechaProc", DateTime.Now);
                            updateCmd.Parameters.AddWithValue("@idExamen", idExamen);
                            updateCmd.ExecuteNonQuery();
                        }
                        Console.WriteLine($"DEBUG: Registro examen {idExamen} actualizado.");
                    }
                    else
                    {
                        // Si no existe, lo insertamos y obtenemos el nuevo ID
                        string insertExamenQuery = @"
                            INSERT INTO examen (id_paciente, id_tipo_examen, id_muestra, fecha_procesamiento, fecha_recepcion)
                            VALUES (@idPaciente, @idTipoExamen, @id_Muestra, @fechaProc, (SELECT fecha_recepcion FROM muestra WHERE id_muestra = @id_Muestra LIMIT 1))
                            RETURNING id_examen;"; // RETURNING nos devuelve el ID recién creado
                        using (var insertCmd = new NpgsqlCommand(insertExamenQuery, conn, transaction))
                        {
                            insertCmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                            insertCmd.Parameters.AddWithValue("@idTipoExamen", ID_TIPO_EXAMEN_ORINA);
                            insertCmd.Parameters.AddWithValue("@id_Muestra", id_Muestra);
                            insertCmd.Parameters.AddWithValue("@fechaProc", DateTime.Now);
                            // La fecha de recepción se toma de la tabla muestra en la misma query
                            idExamen = Convert.ToInt32(insertCmd.ExecuteScalar()); // Obtiene el ID nuevo
                        }
                        Console.WriteLine($"DEBUG: Registro examen {idExamen} creado.");
                    }

                    // PASO 2: Insertar o Actualizar los resultados específicos en 'examen_orina'
                    //         y poner procesado = true.
                    //         (Este ejemplo asume INSERT, si permites editar, necesitarías lógica UPDATE)

                    string insertOrinaQuery = @"
    INSERT INTO examen_orina (
        id_examen, color, ph, aspecto, densidad, leucocitos, hemoglobina,
        nitritos, cetonas, urobilinogeno, bilirrubinas, proteina, glucosa,
        celulas_epiteliales, bacterias, cristales, cilindros, eritrocitos, /* <-- Campo añadido al modelo */
        leucocitos_micro, observaciones, procesado /* <-- Campo añadido al modelo */
    ) VALUES (
        @idExamen, @color, @ph, @aspecto, @densidad, @leucocitos, @hemoglobina,
        @nitritos, @cetonas, @urobilinogeno, @bilirrubinas, @proteina, @glucosa,
        @celulas, @bacterias, @cristales, @cilindros, @eritrocitos, /* <-- Parámetro añadido */
        @leucoMicro, @obs, TRUE
    )
  ON CONFLICT (id_examen) DO UPDATE SET
    color = EXCLUDED.color, ph = EXCLUDED.ph, aspecto = EXCLUDED.aspecto,
    densidad = EXCLUDED.densidad, leucocitos = EXCLUDED.leucocitos, hemoglobina = EXCLUDED.hemoglobina,
    nitritos = EXCLUDED.nitritos, cetonas = EXCLUDED.cetonas, urobilinogeno = EXCLUDED.urobilinogeno,
    bilirrubinas = EXCLUDED.bilirrubinas, proteina = EXCLUDED.proteina, glucosa = EXCLUDED.glucosa,
    celulas_epiteliales = EXCLUDED.celulas_epiteliales, -- <-- CORREGIDO
    bacterias = EXCLUDED.bacterias, cristales = EXCLUDED.cristales,
    cilindros = EXCLUDED.cilindros, eritrocitos = EXCLUDED.eritrocitos,
    leucocitos_micro = EXCLUDED.leucocitos_micro, -- <-- CORREGIDO
    observaciones = EXCLUDED.observaciones, -- <-- CORREGIDO
    procesado = TRUE; -- Asegura que quede procesado
    ";

                    using (var cmdOrina = new NpgsqlCommand(insertOrinaQuery, conn, transaction))
                    {
                        // Añadir todos los parámetros desde el objeto datosOrina
                        cmdOrina.Parameters.AddWithValue("@idExamen", idExamen);
                        cmdOrina.Parameters.AddWithValue("@color", (object)datosOrina.color ?? DBNull.Value);
                        cmdOrina.Parameters.AddWithValue("@ph", datosOrina.ph);
                        cmdOrina.Parameters.AddWithValue("@aspecto", (object)datosOrina.aspecto ?? DBNull.Value);
                        cmdOrina.Parameters.AddWithValue("@densidad", datosOrina.densidad);
                        cmdOrina.Parameters.AddWithValue("@leucocitos", (object)datosOrina.leucocitos ?? DBNull.Value);
                        cmdOrina.Parameters.AddWithValue("@hemoglobina", (object)datosOrina.hemoglobina ?? DBNull.Value);
                        cmdOrina.Parameters.AddWithValue("@nitritos", (object)datosOrina.nitritos ?? DBNull.Value);
                        cmdOrina.Parameters.AddWithValue("@cetonas", (object)datosOrina.cetonas ?? DBNull.Value);
                        cmdOrina.Parameters.AddWithValue("@urobilinogeno", (object)datosOrina.urobilinogeno ?? DBNull.Value);
                        cmdOrina.Parameters.AddWithValue("@bilirrubinas", (object)datosOrina.bilirrubinas ?? DBNull.Value);
                        cmdOrina.Parameters.AddWithValue("@proteina", (object)datosOrina.proteina ?? DBNull.Value);
                        cmdOrina.Parameters.AddWithValue("@glucosa", (object)datosOrina.glucosa ?? DBNull.Value);
                        cmdOrina.Parameters.AddWithValue("@celulas", (object)datosOrina.celulas_epiteliales ?? DBNull.Value); // Ojo nombre parámetro
                        cmdOrina.Parameters.AddWithValue("@bacterias", (object)datosOrina.bacterias ?? DBNull.Value);
                        cmdOrina.Parameters.AddWithValue("@cristales", (object)datosOrina.cristales ?? DBNull.Value);
                        cmdOrina.Parameters.AddWithValue("@cilindros", (object)datosOrina.cilindros ?? DBNull.Value);
                        cmdOrina.Parameters.AddWithValue("@eritrocitos", (object)datosOrina.eritrocitos ?? DBNull.Value); // <-- Parámetro añadido
                        cmdOrina.Parameters.AddWithValue("@leucoMicro", (object)datosOrina.leucocitos_micro ?? DBNull.Value);

                        cmdOrina.Parameters.AddWithValue("@obs", (object)datosOrina.observaciones ?? DBNull.Value); // <-- Parámetro añadido

                        cmdOrina.ExecuteNonQuery();

                        // Si todo fue bien, confirma la transacción
                        transaction.Commit();
                        Console.WriteLine($"DEBUG: Resultados Orina para examen {idExamen} guardados.");
                        return true; // Indica éxito
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
            } // La conexión se cierra automáticamente aquí
        }
    }
    #endregion
    #region Obtener Resultados Examen
    public examen_orina ObtenerResultadoOrina(int id_Muestra)
    {
        examen_orina resultado = null;
        const int ID_TIPO_EXAMEN_ORINA = 1;
        // Busca en examen_orina a través de la tabla examen usando id_muestra y id_tipo_examen
        string query = @"
            SELECT eo.*
            FROM examen_orina eo
            JOIN examen e ON eo.id_examen = e.id_examen
            WHERE e.id_muestra = @id_Muestra AND e.id_tipo_examen = @idTipoExamen
            LIMIT 1;
        "; // LIMIT 1 por si acaso, aunque solo debería haber uno

        try
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_Muestra", id_Muestra);
                    cmd.Parameters.AddWithValue("@idTipoExamen", ID_TIPO_EXAMEN_ORINA);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Si encontró un resultado
                        {
                            resultado = new examen_orina
                            {
                                // Mapea todas las columnas de examen_orina a las propiedades
                                id_examen = reader.GetInt32(reader.GetOrdinal("id_examen")),
                                color = reader.IsDBNull(reader.GetOrdinal("color")) ? null : reader.GetString(reader.GetOrdinal("color")),
                                ph = reader.GetDecimal(reader.GetOrdinal("ph")), // Asume que no es null en BD? O usa reader.IsDBNull
                                aspecto = reader.IsDBNull(reader.GetOrdinal("aspecto")) ? null : reader.GetString(reader.GetOrdinal("aspecto")),
                                densidad = reader.GetDecimal(reader.GetOrdinal("densidad")),
                                leucocitos = reader.IsDBNull(reader.GetOrdinal("leucocitos")) ? null : reader.GetString(reader.GetOrdinal("leucocitos")),
                                hemoglobina = reader.IsDBNull(reader.GetOrdinal("hemoglobina")) ? null : reader.GetString(reader.GetOrdinal("hemoglobina")),
                                nitritos = reader.IsDBNull(reader.GetOrdinal("nitritos")) ? null : reader.GetString(reader.GetOrdinal("nitritos")),
                                cetonas = reader.IsDBNull(reader.GetOrdinal("cetonas")) ? null : reader.GetString(reader.GetOrdinal("cetonas")),
                                urobilinogeno = reader.IsDBNull(reader.GetOrdinal("urobilinogeno")) ? null : reader.GetString(reader.GetOrdinal("urobilinogeno")),
                                bilirrubinas = reader.IsDBNull(reader.GetOrdinal("bilirrubinas")) ? null : reader.GetString(reader.GetOrdinal("bilirrubinas")),
                                proteina = reader.IsDBNull(reader.GetOrdinal("proteina")) ? null : reader.GetString(reader.GetOrdinal("proteina")),
                                glucosa = reader.IsDBNull(reader.GetOrdinal("glucosa")) ? null : reader.GetString(reader.GetOrdinal("glucosa")),
                                celulas_epiteliales = reader.IsDBNull(reader.GetOrdinal("celulas_epiteliales")) ? null : reader.GetString(reader.GetOrdinal("celulas_epiteliales")),
                                bacterias = reader.IsDBNull(reader.GetOrdinal("bacterias")) ? null : reader.GetString(reader.GetOrdinal("bacterias")),
                                cristales = reader.IsDBNull(reader.GetOrdinal("cristales")) ? null : reader.GetString(reader.GetOrdinal("cristales")),
                                cilindros = reader.IsDBNull(reader.GetOrdinal("cilindros")) ? null : reader.GetString(reader.GetOrdinal("cilindros")),
                                eritrocitos = reader.IsDBNull(reader.GetOrdinal("eritrocitos")) ? null : reader.GetString(reader.GetOrdinal("eritrocitos")),
                                leucocitos_micro = reader.IsDBNull(reader.GetOrdinal("leucocitos_micro")) ? null : reader.GetString(reader.GetOrdinal("leucocitos_micro")),
                                observaciones = reader.IsDBNull(reader.GetOrdinal("observaciones")) ? null : reader.GetString(reader.GetOrdinal("observaciones")),
                                procesado = reader.GetBoolean(reader.GetOrdinal("procesado"))
                            };
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener resultado de Orina para muestra {id_Muestra}: {ex.Message}");
            throw; // Relanza para manejo superior
        }
        return resultado;
    }
    public examen_heces ObtenerResultadoHeces(int id_Muestra)
    {
        examen_heces resultado = null;
        const int ID_TIPO_EXAMEN_HECES = 2;
        string query = @"
            SELECT eh.*
            FROM examen_heces eh
            JOIN examen e ON eh.id_examen = e.id_examen
            WHERE e.id_muestra = @id_Muestra AND e.id_tipo_examen = @idTipoExamen
            LIMIT 1;
        ";

        try
        {
            using (var conn = DatabaseConnection.GetConnection())
            { /* ... (similar a Orina, abre conexión, crea comando con parámetros) ... */
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_Muestra", id_Muestra);
                    cmd.Parameters.AddWithValue("@idTipoExamen", ID_TIPO_EXAMEN_HECES);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            resultado = new examen_heces
                            {
                                id_examen = reader.GetInt32(reader.GetOrdinal("id_examen")),
                                color = reader.IsDBNull(reader.GetOrdinal("color")) ? null : reader.GetString(reader.GetOrdinal("color")),
                                consistencia = reader.IsDBNull(reader.GetOrdinal("consistencia")) ? null : reader.GetString(reader.GetOrdinal("consistencia")),
                                parasitos = reader.IsDBNull(reader.GetOrdinal("parasitos")) ? null : reader.GetString(reader.GetOrdinal("parasitos")), // Asegúrate que sea string en modelo
                                observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? null : reader.GetString(reader.GetOrdinal("observacion")),
                                procesado = reader.GetBoolean(reader.GetOrdinal("procesado"))
                            };
                        }
                    }
                }
            }
        }
        catch (Exception ex) { /* ... (Manejo de error similar a Orina) ... */ throw; }
        return resultado;
    }
    public examen_sangre ObtenerResultadoSangre(int id_Muestra)
    {
        examen_sangre resultado = null;
        const int ID_TIPO_EXAMEN_SANGRE = 3;
        string query = @"
            SELECT es.*
            FROM examen_sangre es
            JOIN examen e ON es.id_examen = e.id_examen
            WHERE e.id_muestra = @id_Muestra AND e.id_tipo_examen = @idTipoExamen
            LIMIT 1;
        ";

        try
        {
            using (var conn = DatabaseConnection.GetConnection())
            { /* ... (similar a Orina, abre conexión, crea comando con parámetros) ... */
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_Muestra", id_Muestra);
                    cmd.Parameters.AddWithValue("@idTipoExamen", ID_TIPO_EXAMEN_SANGRE);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Mapea TODOS los campos numéricos y de texto de examen_sangre
                            resultado = new examen_sangre
                            {
                                id_examen = reader.GetInt32(reader.GetOrdinal("id_examen")),
                                globulos_rojos = reader.GetDecimal(reader.GetOrdinal("globulos_rojos")), // Asume que no son null? O usa IsDBNull
                                hematocrito = reader.GetDecimal(reader.GetOrdinal("hematocrito")),
                                hemoglobina = reader.GetDecimal(reader.GetOrdinal("hemoglobina")),
                                leucocitos = reader.GetDecimal(reader.GetOrdinal("leucocitos")),
                                mcv = reader.GetDecimal(reader.GetOrdinal("mcv")),
                                mch = reader.GetDecimal(reader.GetOrdinal("mch")),
                                mchc = reader.GetDecimal(reader.GetOrdinal("mchc")),
                                neutrofilos = reader.GetDecimal(reader.GetOrdinal("neutrofilos")),
                                linfocitos = reader.GetDecimal(reader.GetOrdinal("linfocitos")),
                                monocitos = reader.GetDecimal(reader.GetOrdinal("monocitos")),
                                eosinofilos = reader.GetDecimal(reader.GetOrdinal("eosinofilos")),
                                basofilos = reader.GetDecimal(reader.GetOrdinal("basofilos")),
                                observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? null : reader.GetString(reader.GetOrdinal("observacion")),
                                procesado = reader.GetBoolean(reader.GetOrdinal("procesado"))
                            };
                        }
                    }
                }
            }
        }
        catch (Exception ex) { /* ... (Manejo de error similar a Orina) ... */ throw; }
        return resultado;
    }
    #endregion


}

