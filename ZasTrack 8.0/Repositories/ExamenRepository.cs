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
    public bool GuardarResultadoHeces(examen_heces datosHeces, int idMuestra, int idPaciente)
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
                    string checkQuery = "SELECT id_examen FROM examen WHERE id_muestra = @idMuestra AND id_tipo_examen = @idTipoExamen";
                    int? examenExistenteId = null;
                    using (var checkCmd = new NpgsqlCommand(checkQuery, conn, transaction))
                    {
                        checkCmd.Parameters.AddWithValue("@idMuestra", idMuestra);
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
                                                     VALUES (@idPaciente, @idTipoExamen, @idMuestra, @fechaProc, (SELECT fecha_recepcion FROM muestra WHERE id_muestra = @idMuestra LIMIT 1))
                                                     RETURNING id_examen;";
                        using (var insertCmd = new NpgsqlCommand(insertExamenQuery, conn, transaction))
                        {
                            insertCmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                            insertCmd.Parameters.AddWithValue("@idTipoExamen", ID_TIPO_EXAMEN_HECES); // <- Usa ID Heces
                            insertCmd.Parameters.AddWithValue("@idMuestra", idMuestra);
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
    // ***** FIN MÉTODO HECES *****
    public bool GuardarResultadoSangre(examen_sangre datosSangre, int idMuestra, int idPaciente)
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
                    string checkQuery = "SELECT id_examen FROM examen WHERE id_muestra = @idMuestra AND id_tipo_examen = @idTipoExamen";
                    int? examenExistenteId = null;
                    using (var checkCmd = new NpgsqlCommand(checkQuery, conn, transaction))
                    {
                        checkCmd.Parameters.AddWithValue("@idMuestra", idMuestra);
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
                                                     VALUES (@idPaciente, @idTipoExamen, @idMuestra, @fechaProc, (SELECT fecha_recepcion FROM muestra WHERE id_muestra = @idMuestra LIMIT 1))
                                                     RETURNING id_examen;";
                        using (var insertCmd = new NpgsqlCommand(insertExamenQuery, conn, transaction))
                        {
                            insertCmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                            insertCmd.Parameters.AddWithValue("@idTipoExamen", ID_TIPO_EXAMEN_SANGRE); // <- Usa ID Sangre
                            insertCmd.Parameters.AddWithValue("@idMuestra", idMuestra);
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
    // ***** FIN MÉTODO SANGRE *****

    public bool GuardarResultadoOrina(examen_orina datosOrina, int idMuestra, int idPaciente)
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
                    string checkQuery = "SELECT id_examen FROM examen WHERE id_muestra = @idMuestra AND id_tipo_examen = @idTipoExamen";
                    int? examenExistenteId = null;
                    using (var checkCmd = new NpgsqlCommand(checkQuery, conn, transaction)) // Ejecuta dentro de la transacción
                    {
                        checkCmd.Parameters.AddWithValue("@idMuestra", idMuestra);
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
                            VALUES (@idPaciente, @idTipoExamen, @idMuestra, @fechaProc, (SELECT fecha_recepcion FROM muestra WHERE id_muestra = @idMuestra LIMIT 1))
                            RETURNING id_examen;"; // RETURNING nos devuelve el ID recién creado
                        using (var insertCmd = new NpgsqlCommand(insertExamenQuery, conn, transaction))
                        {
                            insertCmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                            insertCmd.Parameters.AddWithValue("@idTipoExamen", ID_TIPO_EXAMEN_ORINA);
                            insertCmd.Parameters.AddWithValue("@idMuestra", idMuestra);
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
    // ***** FIN NUEVO MÉTODO *****
    // En ExamenRepository.cs
    public int? ObtenerIdPacientePorMuestra(int idMuestra)
    {
        string query = "SELECT id_paciente FROM muestra WHERE id_muestra = @idMuestra LIMIT 1;";
        try
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idMuestra", idMuestra);
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
            Console.WriteLine($"Error al obtener idPaciente para muestra {idMuestra}: {ex.Message}");
            // Podrías lanzar una excepción o devolver null
        }
        return null; // Devuelve null si no se encuentra o hay error
    }

}

