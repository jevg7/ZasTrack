using Npgsql;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ZasTrack.Models;

namespace ZasTrack.Repositories
{
    public class MuestraRepository
    {
        public void GuardarMuestras(Muestra muestra)
        {
            string query = @"
                INSERT INTO muestra (fecha_recepcion, id_proyecto, id_paciente, id_tipo_examen, id_muestra, numero_muestra)
                VALUES (@FechaRecepcion, @IdProyecto, @IdPaciente, @IdTipoExamen, @IdMuestra, @NumeroMuestra)";

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
                        cmd.Parameters.AddWithValue("@IdTipoExamen", muestra.IdTipoExamen);
                        cmd.Parameters.AddWithValue("@IdMuestra", muestra.IdMuestra);
                        cmd.Parameters.AddWithValue("@NumeroMuestra", muestra.NumeroMuestra);

                        cmd.ExecuteNonQuery();


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
    }
}

