using Npgsql;
using System;
using System.Collections.Generic;

namespace ZasTrack.Repositories
{
    public class ExamenRepository
    {
        public List<dynamic> ObtenerPacientesPorProyecto(int idProyecto)
        {
            string query = @"
                SELECT 
                    m.id_muestra,
                    m.numero_muestra,
                    p.nombres || ' ' || p.apellidos AS paciente,
                    p.genero,
                    p.edad,
                    m.fecha_recepcion,
                    COUNT(CASE WHEN e.fecha_procesamiento IS NULL THEN 1 END) AS examenes_pendientes
                FROM muestra m
                INNER JOIN pacientes p ON m.id_paciente = p.id_paciente
                LEFT JOIN examen e ON e.id_muestra = m.id_muestra
                WHERE m.id_proyecto = @idProyecto
                GROUP BY m.id_muestra, m.numero_muestra, p.nombres, p.apellidos, p.genero, p.edad, m.fecha_recepcion
                ORDER BY m.numero_muestra;
            ";

            List<dynamic> pacientes = new List<dynamic>();

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idProyecto", idProyecto);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var paciente = new
                                {
                                    IdMuestra = reader.GetInt32(0),
                                    NumeroMuestra = reader.GetInt32(1),
                                    Paciente = reader.GetString(2),
                                    Genero = reader.GetString(3),
                                    Edad = reader.GetInt32(4),
                                    FechaRecepcion = reader.GetDateTime(5),
                                    ExamenesPendientes = reader.GetInt32(6)
                                };

                                pacientes.Add(paciente);
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error de PostgreSQL: {ex.Message}");
                Console.WriteLine($"Código de error: {ex.SqlState}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }

            return pacientes;
        }
    }
}
