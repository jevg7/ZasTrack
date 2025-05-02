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
            string query = "UPDATE proyecto SET is_archived = TRUE, fecha_fin = @fechaFin WHERE id_proyecto = @id";
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idProyecto);
                        cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ArchivarProyecto: {ex.Message}");
                return false;
            }
        }
        

        public List<Proyecto> ObtenerProyectosSoloArchivados()
        {
            var proyectos = new List<Proyecto>();
            // Asegúrate que los nombres de columna (is_archived, etc.) sean correctos
            // Consulta para obtener solo los archivados (is_archived = TRUE)
            string query = "SELECT id_proyecto, nombre, fecha_inicio, fecha_fin, is_archived, codigo FROM proyecto WHERE is_archived = TRUE ORDER BY nombre;";

            try
            {
                using (var conn = DatabaseConnection.GetConnection()) // Usa tu método de conexión
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
                                    id_proyecto = reader.GetInt32(reader.GetOrdinal("id_proyecto")),
                                    nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    fecha_inicio = reader.GetDateTime(reader.GetOrdinal("fecha_inicio")),
                                    fecha_fin = reader.IsDBNull(reader.GetOrdinal("fecha_fin")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fecha_fin")),
                                    // Aquí is_archived debería ser siempre true, pero lo leemos igual
                                    IsArchived = reader.IsDBNull(reader.GetOrdinal("is_archived")) ? false : reader.GetBoolean(reader.GetOrdinal("is_archived")),
                                    codigo = reader.IsDBNull(reader.GetOrdinal("codigo")) ? null : reader.GetString(reader.GetOrdinal("codigo"))
                                });
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error de PostgreSQL en ObtenerProyectosSoloArchivados: {ex.Message} (SQLState: {ex.SqlState})");
                // Considera lanzar la excepción o devolver lista vacía/null según tu manejo de errores
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general en ObtenerProyectosSoloArchivados: {ex.ToString()}");
                throw;
            }
            return proyectos;
        }

        public bool ActualizarProyecto(Proyecto proyecto)
        {
            // Validar que el objeto y su ID sean válidos
            if (proyecto == null || proyecto.id_proyecto <= 0)
            {
                throw new ArgumentException("Datos del proyecto inválidos para actualizar.");
            }

            // Sentencia UPDATE
            // Asegúrate que los nombres de columna (nombre, codigo, fecha_inicio)
            // y tabla (proyecto) coincidan exactamente con tu base de datos.
            string query = @"UPDATE proyecto
                     SET nombre = @nombre,
                         codigo = @codigo,
                         fecha_inicio = @fechaInicio
                         -- No actualizamos is_archived ni fecha_fin aquí
                     WHERE id_proyecto = @idProyecto;";

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    // Asignar parámetros desde el objeto proyecto
                    cmd.Parameters.AddWithValue("@nombre", proyecto.nombre.Trim());
                    cmd.Parameters.AddWithValue("@codigo", proyecto.codigo.Trim());
                    // Asegurarse que fecha_inicio no sea null (aunque no debería serlo)
                    cmd.Parameters.AddWithValue("@fechaInicio", proyecto.fecha_inicio);
                    cmd.Parameters.AddWithValue("@idProyecto", proyecto.id_proyecto);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery(); // ExecuteNonQuery devuelve el número de filas afectadas

                    // Devolver true si se actualizó al menos una fila (debería ser 1)
                    return rowsAffected > 0;
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error DB en ActualizarProyecto (ID: {proyecto.id_proyecto}): {ex}");
                // Relanzar para que la capa superior (el formulario) sepa del error
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error General en ActualizarProyecto (ID: {proyecto.id_proyecto}): {ex}");
                // Relanzar
                throw;
            }
        }
        // --- NUEVO MÉTODO: Reactivar Proyecto ---
        public bool ReactivarProyecto(int idProyecto, bool limpiarFechaFin)
        {
            // Prepara la consulta SQL condicionalmente
            string query;
            if (limpiarFechaFin)
            {
                // Pone IsArchived = false Y fecha_fin = NULL
                query = "UPDATE proyecto SET is_archived = FALSE, fecha_fin = NULL WHERE id_proyecto = @id";
            }
            else
            {
                // Solo pone IsArchived = false, deja fecha_fin como estaba
                query = "UPDATE proyecto SET is_archived = FALSE WHERE id_proyecto = @id";
            }

            try
            {
                using (var conn = DatabaseConnection.GetConnection()) // Usa tu método de conexión
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        // Añade el parámetro ID de forma segura
                        cmd.Parameters.AddWithValue("@id", idProyecto);
                        // Ejecuta el comando y verifica si alguna fila fue afectada
                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0; // Devuelve true si se actualizó al menos una fila
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error de PostgreSQL en ReactivarProyecto: {ex.Message} (SQLState: {ex.SqlState})");
                return false; // Devuelve false en caso de error de BD
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general en ReactivarProyecto: {ex.ToString()}");
                return false; // Devuelve false en caso de error general
            }
        }
        // En ProyectoRepository.cs
        public bool NombreExiste(string nombre, int idExcluir = 0) // idExcluir es útil para editar
        {
            // Compara sin distinguir mayúsculas/minúsculas
            // Excluye el ID actual si se proporciona (para el modo edición)
            string query = "SELECT COUNT(*) FROM proyecto WHERE LOWER(nombre) = LOWER(@nombre) AND id_proyecto != @idExcluir";
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre.Trim());
                    cmd.Parameters.AddWithValue("@idExcluir", idExcluir);
                    conn.Open();
                    return (long)cmd.ExecuteScalar() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en NombreExiste: {ex}");
                throw; // Relanzar para que la UI sepa que falló la validación
            }
        }

        public bool CodigoExiste(string codigo, int idExcluir = 0) // idExcluir es útil para editar
        {
            // Compara sin distinguir mayúsculas/minúsculas
            // Excluye el ID actual si se proporciona (para el modo edición)
            string query = "SELECT COUNT(*) FROM proyecto WHERE LOWER(codigo) = LOWER(@codigo) AND id_proyecto != @idExcluir";
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo.Trim());
                    cmd.Parameters.AddWithValue("@idExcluir", idExcluir);
                    conn.Open();
                    return (long)cmd.ExecuteScalar() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CodigoExiste: {ex}");
                throw; // Relanzar para que la UI sepa que falló la validación
            }
        }
        // En ProyectoRepository.cs

        public async Task<Proyecto?> ObtenerProyectoPorIdAsync(int idProyecto)
        {
            Proyecto? proyecto = null;
            // --- CAMBIO AQUÍ: Usar @idProyecto en la query ---
            string query = "SELECT id_proyecto, nombre, codigo, fecha_inicio, fecha_fin, is_archived " +
                           "FROM proyecto WHERE id_proyecto = @idProyecto LIMIT 1;";
            // (También es buena práctica listar las columnas explícitamente en lugar de SELECT *)
            // --- FIN CAMBIO ---

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    // El nombre del parámetro aquí SÍ coincide con la query ahora
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            proyecto = new Proyecto
                            {
                                id_proyecto = reader.GetInt32(reader.GetOrdinal("id_proyecto")),
                                nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                codigo = reader.GetString(reader.GetOrdinal("codigo")),
                                fecha_inicio = reader.GetDateTime(reader.GetOrdinal("fecha_inicio")),
                                fecha_fin = reader.IsDBNull(reader.GetOrdinal("fecha_fin")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fecha_fin")),
                                IsArchived = reader.GetBoolean(reader.GetOrdinal("is_archived")) // Asumiendo que IsArchived se mapea a is_archived en DB
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerProyectoPorIdAsync (ID: {idProyecto}): {ex}");
                // throw; // Opcional: relanzar
            }
            return proyecto;
        }
    }
}