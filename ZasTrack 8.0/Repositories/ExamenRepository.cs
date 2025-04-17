using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using ZasTrack;
using ZasTrack.Models; 
public class ExamenRepository
{
    // MeTODO COMPLETO CON TODOS LOS FILTROS
    public List<MuestraInfoViewModel> ObtenerPacientesPorProyecto(int idProyecto, DateTime fecha, List<int> tiposRequeridos, string textoBusqueda)
    {
        string queryBase = @"
            SELECT
                m.id_muestra, m.numero_muestra, p.nombres || ' ' || p.apellidos AS paciente,
                p.genero, p.edad, m.fecha_recepcion,
                COALESCE(
                    STRING_AGG(DISTINCT te.nombre, ', ' ORDER BY te.nombre) FILTER (WHERE
                         e.id_examen IS NULL OR
                         ( (te.id_tipo_examen = 1 AND eo.procesado IS DISTINCT FROM TRUE) OR
                           (te.id_tipo_examen = 2 AND eh.procesado IS DISTINCT FROM TRUE) OR
                           (te.id_tipo_examen = 3 AND es.procesado IS DISTINCT FROM TRUE) )
                    ), 'Sin pendientes'
                ) AS examenes_pendientes_str
            FROM muestra m
            INNER JOIN pacientes p ON m.id_paciente = p.id_paciente
            LEFT JOIN muestra_examen me ON m.id_muestra = me.id_muestra
            LEFT JOIN tipo_examen te ON me.id_tipo_examen = te.id_tipo_examen
            LEFT JOIN examen e ON e.id_muestra = me.id_muestra AND e.id_tipo_examen = me.id_tipo_examen
            LEFT JOIN examen_orina eo ON te.id_tipo_examen = 1 AND eo.id_examen = e.id_examen
            LEFT JOIN examen_heces eh ON te.id_tipo_examen = 2 AND eh.id_examen = e.id_examen
            LEFT JOIN examen_sangre es ON te.id_tipo_examen = 3 AND es.id_examen = e.id_examen
            ";

        var whereClauses = new List<string>();
        whereClauses.Add("m.id_proyecto = @idProyecto");
        whereClauses.Add("m.fecha_recepcion = @fechaRecepcion");

        if (tiposRequeridos != null && tiposRequeridos.Any())
        {
            whereClauses.Add("EXISTS (SELECT 1 FROM muestra_examen me_f WHERE me_f.id_muestra = m.id_muestra AND me_f.id_tipo_examen = ANY(@p_tipos))");
        }

        if (!string.IsNullOrWhiteSpace(textoBusqueda))
        {
            // Busca en nombres, apellidos, código y número de muestra (como texto)
            whereClauses.Add("(p.nombres ILIKE @p_texto OR p.apellidos ILIKE @p_texto OR p.codigo_beneficiario ILIKE @p_texto OR CAST(m.numero_muestra AS TEXT) ILIKE @p_texto)");
        }


        string queryFinal = queryBase + " WHERE " + string.Join(" AND ", whereClauses) +
                           " GROUP BY m.id_muestra, m.numero_muestra, paciente, p.genero, p.edad, m.fecha_recepcion" +
                           " ORDER BY m.numero_muestra;"; // Orden final

        var resultados = new List<MuestraInfoViewModel>();
        try
        {
            using (var conn = DatabaseConnection.GetConnection()) 
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(queryFinal, conn))
                {
                    // Parámetros base
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    cmd.Parameters.AddWithValue("@fechaRecepcion", fecha.Date);

                    // Parámetro tipos (condicional)
                    if (tiposRequeridos != null && tiposRequeridos.Any())
                    {
                        cmd.Parameters.AddWithValue("@p_tipos", tiposRequeridos);
                    }

                    // Parámetro para la búsqueda (condicional)
                    if (!string.IsNullOrWhiteSpace(textoBusqueda))
                    {
                        // Añade comodines % para buscar coincidencias parciales
                        cmd.Parameters.AddWithValue("@p_texto", $"%{textoBusqueda}%");
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Llenado del ViewModel (sin cambios) REFERENCIA A MODEL TEMPORAL DE MUESTRAINFOVIEWMODEL
                            resultados.Add(new MuestraInfoViewModel
                            {
                                IdMuestra = reader.GetInt32(reader.GetOrdinal("id_muestra")),
                                NumeroMuestra = reader.GetInt32(reader.GetOrdinal("numero_muestra")),
                                Paciente = reader.GetString(reader.GetOrdinal("paciente")),
                                Genero = reader.GetString(reader.GetOrdinal("genero")),
                                Edad = reader.GetInt32(reader.GetOrdinal("edad")),
                                FechaRecepcion = reader.GetDateTime(reader.GetOrdinal("fecha_recepcion")),
                                ExamenesPendientesStr = reader.GetString(reader.GetOrdinal("examenes_pendientes_str"))
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener pacientes filtrados: {ex.Message}");
            throw;
        }
        return resultados;
    }
    // ***** NUEVO MÉTODO PARA OBTENER PROCESADOS *****
    public List<MuestraInfoViewModel> ObtenerPacientesProcesados(int idProyecto, DateTime fecha, List<int> tiposRequeridos, string textoBusqueda)
    {
        // Selecciona la información base de la muestra y paciente
        string queryBase = @"
             SELECT
                 m.id_muestra, m.numero_muestra, p.nombres || ' ' || p.apellidos AS paciente,
                 p.genero, p.edad, m.fecha_recepcion
             FROM muestra m
             INNER JOIN pacientes p ON m.id_paciente = p.id_paciente
             ";

        // Construcción dinámica del WHERE
        var whereClauses = new List<string>();
        whereClauses.Add("m.id_proyecto = @idProyecto");
        whereClauses.Add("m.fecha_recepcion = @fechaRecepcion");

        // Filtro por tipo REQUERIDO (opcional, pero puede ser útil para ver
        // muestras procesadas que INCLUYERON ciertos exámenes)
        if (tiposRequeridos != null && tiposRequeridos.Any())
        {
            whereClauses.Add("EXISTS (SELECT 1 FROM muestra_examen me_f WHERE me_f.id_muestra = m.id_muestra AND me_f.id_tipo_examen = ANY(@p_tipos))");
        }

        // Filtro por texto de búsqueda (igual que en el otro método)
        if (!string.IsNullOrWhiteSpace(textoBusqueda))
        {
            whereClauses.Add("(p.nombres ILIKE @p_texto OR p.apellidos ILIKE @p_texto OR p.codigo_beneficiario ILIKE @p_texto OR CAST(m.numero_muestra AS TEXT) ILIKE @p_texto)");
        }

        // ***** LÓGICA CLAVE: Asegurar que NO existan exámenes PENDIENTES *****
        whereClauses.Add(@"
             NOT EXISTS (
                 SELECT 1
                 FROM muestra_examen me_pend
                 JOIN tipo_examen te_pend ON me_pend.id_tipo_examen = te_pend.id_tipo_examen
                 LEFT JOIN examen e_pend ON e_pend.id_muestra = me_pend.id_muestra AND e_pend.id_tipo_examen = me_pend.id_tipo_examen
                 LEFT JOIN examen_orina eo_pend ON te_pend.id_tipo_examen = 1 AND eo_pend.id_examen = e_pend.id_examen
                 LEFT JOIN examen_heces eh_pend ON te_pend.id_tipo_examen = 2 AND eh_pend.id_examen = e_pend.id_examen
                 LEFT JOIN examen_sangre es_pend ON te_pend.id_tipo_examen = 3 AND es_pend.id_examen = e_pend.id_examen
                 WHERE me_pend.id_muestra = m.id_muestra -- Para la muestra actual 'm'
                   AND ( -- Condición para estar PENDIENTE (igual que en el STRING_AGG FILTER)
                         e_pend.id_examen IS NULL OR
                         (te_pend.id_tipo_examen = 1 AND eo_pend.procesado IS DISTINCT FROM TRUE) OR
                         (te_pend.id_tipo_examen = 2 AND eh_pend.procesado IS DISTINCT FROM TRUE) OR
                         (te_pend.id_tipo_examen = 3 AND es_pend.procesado IS DISTINCT FROM TRUE)
                       )
             )
         ");
        // ***** FIN LÓGICA CLAVE *****

        // Asegurar que la muestra tenga al menos un examen asignado originalmente
        // para no mostrar muestras vacías que nunca necesitaron procesamiento.
        whereClauses.Add("EXISTS (SELECT 1 FROM muestra_examen me_any WHERE me_any.id_muestra = m.id_muestra)");


        // Construir la consulta final (NO necesitamos GROUP BY aquí porque solo seleccionamos de muestra y paciente)
        string queryFinal = queryBase + " WHERE " + string.Join(" AND ", whereClauses) +
                           " ORDER BY m.numero_muestra;"; // Orden ascendente

        var resultados = new List<MuestraInfoViewModel>();
        try
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(queryFinal, conn))
                {
                    // Parámetros (igual que en el otro método)
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
                            resultados.Add(new MuestraInfoViewModel
                            {
                                IdMuestra = reader.GetInt32(reader.GetOrdinal("id_muestra")),
                                NumeroMuestra = reader.GetInt32(reader.GetOrdinal("numero_muestra")),
                                Paciente = reader.GetString(reader.GetOrdinal("paciente")),
                                Genero = reader.GetString(reader.GetOrdinal("genero")),
                                Edad = reader.GetInt32(reader.GetOrdinal("edad")),
                                FechaRecepcion = reader.GetDateTime(reader.GetOrdinal("fecha_recepcion")),
                                // ***** CAMBIO: Para procesados, el estado es "Completado" *****
                                ExamenesPendientesStr = "Completado"
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener pacientes procesados: {ex.Message}");
            // Considerar mejor logging/manejo de errores
            throw;
        }
        return resultados;
    }
    // ***** FIN NUEVO MÉTODO *****
    public List<tipo_examen> ObtenerTiposExamenPendientesPorMuestra(int idMuestra)
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
            WHERE me.id_muestra = @idMuestra
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
                    cmd.Parameters.AddWithValue("@idMuestra", idMuestra);
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
            Console.WriteLine($"Error al obtener tipos pendientes para muestra {idMuestra}: {ex.Message}");
            // Podrías relanzar o manejar el error como prefieras
            throw;
        }
        return tiposPendientes;
    }
    // ***** FIN NUEVO MÉTODO *****


}

