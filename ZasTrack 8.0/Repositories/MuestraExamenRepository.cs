// MuestraExamenRepository.cs
using Npgsql;
using System;
using System.Collections.Generic;
using ZasTrack.Models;

namespace ZasTrack.Repositories
{
    public class MuestraExamenRepository
    {
        public void VincularExamen(MuestraExamen muestraExamen)
        {
            string query = @"
                INSERT INTO muestra_examen (id_muestra, id_tipo_examen)
                VALUES (@id_Muestra, @IdTipoExamen)
                ON CONFLICT (id_muestra, id_tipo_examen) DO NOTHING";

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_Muestra", muestraExamen.id_Muestra);
                        cmd.Parameters.AddWithValue("@IdTipoExamen", muestraExamen.IdTipoExamen);
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

        public List<int> GetExamenesPorMuestra(int id_Muestra)
        {

           
            var examenes = new List<int>();
            string query = "SELECT id_tipo_examen FROM muestra_examen WHERE id_muestra = @id_Muestra";
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
                            examenes.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            return examenes;
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
    }
}