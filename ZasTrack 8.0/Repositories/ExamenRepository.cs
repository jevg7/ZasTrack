using Npgsql;
using ZasTrack.Models;
using ZasTrack;
using System.Globalization;
using System.Collections.Generic;

public class ExamenRepository
{
    // Cambia el tipo de retorno a List<MuestraInfoViewModel>
    public List<MuestraInfoViewModel> ObtenerPacientesPorProyecto(int idProyecto, DateTime fecha)
    {
        // Pega la NUEVA consulta SQL aquí
        string query = @"
            SELECT
                m.id_muestra,
                m.numero_muestra,
                p.nombres || ' ' || p.apellidos AS paciente,
                p.genero,
                p.edad,
                m.fecha_recepcion,
                COALESCE(
                    STRING_AGG(DISTINCT te.nombre, ', ' ORDER BY te.nombre) FILTER (WHERE
                         e.id_examen IS NULL OR
                         (
                           (te.id_tipo_examen = 1 AND eo.procesado IS DISTINCT FROM TRUE) OR
                           (te.id_tipo_examen = 2 AND eh.procesado IS DISTINCT FROM TRUE) OR
                           (te.id_tipo_examen = 3 AND es.procesado IS DISTINCT FROM TRUE)
                         )
                    ),
                    'Sin pendientes'
                ) AS examenes_pendientes_str
            FROM
                muestra m
            INNER JOIN pacientes p ON m.id_paciente = p.id_paciente
            LEFT JOIN muestra_examen me ON m.id_muestra = me.id_muestra
            LEFT JOIN tipo_examen te ON me.id_tipo_examen = te.id_tipo_examen
            LEFT JOIN examen e ON e.id_muestra = me.id_muestra AND e.id_tipo_examen = me.id_tipo_examen
            LEFT JOIN examen_orina eo ON te.id_tipo_examen = 1 AND eo.id_examen = e.id_examen
            LEFT JOIN examen_heces eh ON te.id_tipo_examen = 2 AND eh.id_examen = e.id_examen
            LEFT JOIN examen_sangre es ON te.id_tipo_examen = 3 AND es.id_examen = e.id_examen
            WHERE
                m.id_proyecto = @idProyecto
                -- ***** PASO 2: Añadir filtro por fecha en SQL *****
                AND m.fecha_recepcion = @fechaRecepcion
            GROUP BY
                m.id_muestra, m.numero_muestra, paciente, p.genero, p.edad, m.fecha_recepcion
            ORDER BY
                 m.numero_muestra;
        ";

        var resultados = new List<MuestraInfoViewModel>(); // Cambia el tipo de la lista
        try
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    // ***** PASO 3: Añadir el parámetro de fecha al comando *****
                    cmd.Parameters.AddWithValue("@fechaRecepcion", fecha.Date);

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
                                ExamenesPendientesStr = reader.GetString(reader.GetOrdinal("examenes_pendientes_str")) // Lee directo gracias a COALESCE
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener pacientes y pendientes: {ex.Message}");
            // Considera un mejor manejo de errores/logging aquí
            throw;
        }
        return resultados;
    }

    // Ya NO necesitas este método si no lo usas en otro lado:
    // public List<string> ObtenerExamenesPendientesPorMuestra(int idMuestra) { ... }
}