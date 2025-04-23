using Npgsql;
using System;
using System.Collections.Generic;
using ZasTrack.Models;

namespace ZasTrack.Repositories
{
    public class ProyectoRepository
    {
        public void GuardarProyecto(Proyecto proyecto)
        {
            string query = @"
                INSERT INTO proyecto (nombre, fecha_inicio, codigo)
                VALUES (@nombre, @fecha_inicio, @codigo)";

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("nombre", proyecto.nombre);
                        cmd.Parameters.AddWithValue("fecha_inicio", proyecto.fecha_inicio);
                        cmd.Parameters.AddWithValue("codigo", proyecto.codigo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error de PostgreSQL: {ex.Message}");
                Console.WriteLine($"Código de error: {ex.SqlState}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        public List<Proyecto> ObtenerProyectos(bool incluirArchivados = false) // Parámetro opcional
        {
            var proyectos = new List<Proyecto>();
            // Modifica la consulta para filtrar por is_archived
            // ¡Asegúrate de que los nombres de columna 'is_archived' y 'codigo' sean correctos!
            string query = "SELECT id_proyecto, nombre, fecha_inicio, fecha_fin, is_archived, codigo FROM proyecto";

            if (!incluirArchivados)
            {
                // Añade el filtro WHERE si NO se incluyen archivados
                // Usamos IS DISTINCT FROM TRUE para incluir los NULL (que no están archivados)
                query += " WHERE is_archived IS DISTINCT FROM TRUE";
            }
            // Opcional: Añadir ordenamiento
            query += " ORDER BY nombre;";

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                proyectos.Add(new Proyecto
                                {
                                    // Uso de GetOrdinal: ¡Muy bien! Hace el código más robusto.
                                    id_proyecto = reader.GetInt32(reader.GetOrdinal("id_proyecto")),
                                    nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    fecha_inicio = reader.GetDateTime(reader.GetOrdinal("fecha_inicio")),
                                    fecha_fin = reader.IsDBNull(reader.GetOrdinal("fecha_fin")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fecha_fin")),
                                    // Lee el estado archivado, manejando NULLs (asume NULL = false)
                                    IsArchived = reader.IsDBNull(reader.GetOrdinal("is_archived")) ? false : reader.GetBoolean(reader.GetOrdinal("is_archived")),
                                    // Lee el código, manejando NULLs
                                    codigo = reader.IsDBNull(reader.GetOrdinal("codigo")) ? null : reader.GetString(reader.GetOrdinal("codigo"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex) // Captura genérica Exception, considera NpgsqlException si quieres ser más específico
            {
                // Es buena idea registrar el error completo, no solo el mensaje
                Console.WriteLine($"Error en ObtenerProyectos: {ex.ToString()}");
                // Re-lanzar la excepción para que capas superiores puedan manejarla si es necesario
                throw;
            }
            return proyectos;
        }
        public bool ArchivarProyecto(int idProyecto, DateTime fechaFin)
        {
            // Actualiza la fecha de fin y marca como archivado
            string query = @"UPDATE proyecto 
                     SET fecha_fin = @fechaFin, 
                         is_archived = TRUE 
                     WHERE id_proyecto = @idProyecto;";
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                        cmd.Parameters.AddWithValue("@fechaFin", fechaFin.Date); // Guarda solo la fecha
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; // Devuelve true si se actualizó al menos una fila
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ArchivarProyecto: {ex.Message}");
                // Considera loguear ex.ToString()
                return false; // Indica que hubo un error
            }
        }
    }
}