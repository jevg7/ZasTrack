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

}