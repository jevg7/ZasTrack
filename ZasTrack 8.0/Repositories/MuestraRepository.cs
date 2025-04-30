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


    }
}

