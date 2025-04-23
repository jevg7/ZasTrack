using Npgsql;
using System;
using System.Collections.Generic;
using ZasTrack.Models;

namespace ZasTrack.Repositories
{
    public class PacienteRepository
    {
        public List<pacientes> ObtenerPacientes()
        {
            var pacientes = new List<pacientes>();
            string query = "SELECT id_paciente, nombres, apellidos, edad, genero, codigo_beneficiario, fecha_nacimiento, id_proyecto, observacion FROM pacientes";

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pacientes.Add(new pacientes
                            {
                                id_paciente = reader.GetInt32(0),
                                nombres = reader.GetString(1),
                                apellidos = reader.GetString(2),
                                edad = reader.GetInt32(3),
                                genero = reader.GetString(4),
                                codigo_beneficiario = reader.GetString(5),
                                fecha_nacimiento = reader.GetDateTime(6),
                                id_proyecto = reader.GetInt32(7),
                                observacion = reader.IsDBNull(8) ? null : reader.GetString(8)
                            });
                        }
                    }
                }
            }

            return pacientes;
        }
        public void GuardarPaciente(pacientes paciente)
        {
            string query = @"
        INSERT INTO pacientes (nombres, apellidos, edad, genero, codigo_beneficiario, fecha_nacimiento, id_proyecto, observacion)
        VALUES (@nombres, @apellidos, @edad, @genero, @codigo_beneficiario, @fecha_nacimiento, @id_proyecto, @observacion)";

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("nombres", paciente.nombres);
                        cmd.Parameters.AddWithValue("apellidos", paciente.apellidos);
                        cmd.Parameters.AddWithValue("edad", paciente.edad);
                        cmd.Parameters.AddWithValue("genero", paciente.genero);
                        cmd.Parameters.AddWithValue("codigo_beneficiario", paciente.codigo_beneficiario);
                        cmd.Parameters.AddWithValue("fecha_nacimiento", paciente.fecha_nacimiento);
                        cmd.Parameters.AddWithValue("id_proyecto", paciente.id_proyecto);
                        cmd.Parameters.AddWithValue("observacion", paciente.observacion);

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
        public void EditarPaciente(pacientes paciente)
        {
            string query = @"UPDATE pacientes SET 
                    nombres = @nombres,
                    apellidos = @apellidos,
                    edad = @edad,
                    genero = @genero,
                    fecha_nacimiento = @fecha_nacimiento,
                    id_proyecto = @id_proyecto,
                    observacion = @observacion
                 WHERE codigo_beneficiario = @codigo_beneficiario";

            try
            {
                using (var conexion = DatabaseConnection.GetConnection())
                {
                    conexion.Open();
                    using (var cmd = new NpgsqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@nombres", paciente.nombres);
                        cmd.Parameters.AddWithValue("@apellidos", paciente.apellidos);
                        cmd.Parameters.AddWithValue("@edad", paciente.edad);
                        cmd.Parameters.AddWithValue("@genero", paciente.genero);
                        cmd.Parameters.AddWithValue("@fecha_nacimiento", paciente.fecha_nacimiento);
                        cmd.Parameters.AddWithValue("@id_proyecto", paciente.id_proyecto);
                        cmd.Parameters.AddWithValue("@observacion", paciente.observacion ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@codigo_beneficiario", paciente.codigo_beneficiario);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas == 0)
                        {
                            throw new Exception("No se encontró el paciente a editar.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al editar el paciente: {ex.Message}");
                throw; // Relanza la excepción para que el llamador pueda manejarla
            }
        }
        public pacientes BuscarPacientePorCodigo(string codigo)
        {
            string query = "SELECT * FROM pacientes WHERE codigo_beneficiario = @codigo";

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new pacientes
                            {
                                id_paciente = reader.GetInt32(reader.GetOrdinal("id_paciente")),
                                nombres = reader.GetString(reader.GetOrdinal("nombres")),
                                apellidos = reader.GetString(reader.GetOrdinal("apellidos")),
                                edad = reader.GetInt32(reader.GetOrdinal("edad")),
                                genero = reader.GetString(reader.GetOrdinal("genero")),
                                codigo_beneficiario = reader.GetString(reader.GetOrdinal("codigo_beneficiario")),
                                fecha_nacimiento = reader.GetDateTime(reader.GetOrdinal("fecha_nacimiento")),
                                id_proyecto = reader.GetInt32(reader.GetOrdinal("id_proyecto")),
                                observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? "" : reader.GetString(reader.GetOrdinal("observacion"))
                            };
                        }
                    }
                }
            }

            return null; // Si no se encuentra el paciente, retorna null
        }        
        public pacientes BuscarPacientePorId(int idPaciente)
        {
            string query = "SELECT * FROM pacientes WHERE id_paciente = @idPaciente";

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new pacientes
                            {
                                id_paciente = reader.GetInt32(reader.GetOrdinal("id_paciente")),
                                nombres = reader.GetString(reader.GetOrdinal("nombres")),
                                apellidos = reader.GetString(reader.GetOrdinal("apellidos")),
                                edad = reader.GetInt32(reader.GetOrdinal("edad")),
                                genero = reader.GetString(reader.GetOrdinal("genero")),
                                codigo_beneficiario = reader.GetString(reader.GetOrdinal("codigo_beneficiario")),
                                fecha_nacimiento = reader.GetDateTime(reader.GetOrdinal("fecha_nacimiento")),
                                id_proyecto = reader.GetInt32(reader.GetOrdinal("id_proyecto")),
                                observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? "" : reader.GetString(reader.GetOrdinal("observacion"))
                            };
                        }
                    }
                }
            }

            return null; // Si no se encuentra el paciente, retorna null
        }
        public List<pacientes> BuscarPacientesPorNombre(string nombre)
        {
            var pacientes = new List<pacientes>();
            string query = @"
        SELECT * FROM pacientes 
        WHERE LOWER(nombres) LIKE LOWER(@nombre) 
        OR LOWER(apellidos) LIKE LOWER(@nombre) 
        OR LOWER(CONCAT(nombres, ' ', apellidos)) LIKE LOWER(@nombre)";

            Console.WriteLine($"Buscando pacientes con nombre/apellido: {nombre}");

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", $"%{nombre.ToLower()}%"); // Búsqueda parcial y normalizada
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pacientes.Add(new pacientes
                            {
                                id_paciente = reader.GetInt32(reader.GetOrdinal("id_paciente")),
                                nombres = reader.GetString(reader.GetOrdinal("nombres")),
                                apellidos = reader.GetString(reader.GetOrdinal("apellidos")),
                                edad = reader.GetInt32(reader.GetOrdinal("edad")),
                                genero = reader.GetString(reader.GetOrdinal("genero")),
                                codigo_beneficiario = reader.GetString(reader.GetOrdinal("codigo_beneficiario")),
                                fecha_nacimiento = reader.GetDateTime(reader.GetOrdinal("fecha_nacimiento")),
                                id_proyecto = reader.GetInt32(reader.GetOrdinal("id_proyecto")),
                                observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? "" : reader.GetString(reader.GetOrdinal("observacion"))
                            });
                        }
                    }
                }
            }

            Console.WriteLine($"Pacientes encontrados: {pacientes.Count}");
            return pacientes;
        }
        public int obtTotalPacientes(int idProyecto)
        {
            string query = "SELECT COUNT(*) FROM pacientes WHERE id_proyecto = @idProyecto";

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    return Convert.ToInt32(cmd.ExecuteScalar()); // Devuelve el total de pacientes
                }
            }
        }

        public List<pacientes> BuscarPacientes(string criterio, int idProyecto)
        {
            var pacientes = new List<pacientes>();
            string query = @"
        SELECT * FROM pacientes 
        WHERE id_proyecto = @idProyecto 
        AND (
            CAST(id_paciente AS TEXT) LIKE @criterio 
            OR LOWER(nombres) LIKE LOWER(@criterio) 
            OR LOWER(apellidos) LIKE LOWER(@criterio) 
            OR LOWER(CONCAT(nombres, ' ', apellidos)) LIKE LOWER(@criterio) 
            OR LOWER(codigo_beneficiario) LIKE LOWER(@criterio)
        )";

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@criterio", $"%{criterio.ToLower()}%");
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pacientes.Add(new pacientes
                            {
                                id_paciente = reader.GetInt32(reader.GetOrdinal("id_paciente")),
                                nombres = reader.GetString(reader.GetOrdinal("nombres")),
                                apellidos = reader.GetString(reader.GetOrdinal("apellidos")),
                                edad = reader.GetInt32(reader.GetOrdinal("edad")),
                                genero = reader.GetString(reader.GetOrdinal("genero")),
                                codigo_beneficiario = reader.GetString(reader.GetOrdinal("codigo_beneficiario")),
                                fecha_nacimiento = reader.GetDateTime(reader.GetOrdinal("fecha_nacimiento")),
                                id_proyecto = reader.GetInt32(reader.GetOrdinal("id_proyecto")),
                                observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? "" : reader.GetString(reader.GetOrdinal("observacion"))
                            });
                        }
                    }
                }
            }

            return pacientes;
        }

        // --- Añadir a PacienteRepository.cs ---

        public bool ExisteCodigoBeneficiario(string codigoBeneficiario)
        {
            // Consulta para contar cuántos pacientes tienen ese código
            string query = "SELECT COUNT(*) FROM pacientes WHERE codigo_beneficiario = @codigo";
            bool existe = false;

            if (string.IsNullOrWhiteSpace(codigoBeneficiario))
            {
                return false; // Un código vacío no "existe" para evitar errores
            }

            try
            {
                using (var conn = DatabaseConnection.GetConnection()) // Usa tu método de conexión
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", codigoBeneficiario.Trim()); // Usa Trim por si acaso
                                                                                           // ExecuteScalar devuelve la primera columna de la primera fila (en este caso, el COUNT)
                        long count = (long)cmd.ExecuteScalar();
                        existe = (count > 0);
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error de PostgreSQL al verificar código beneficiario: {ex.Message}");
                // Decide cómo manejar el error. Lanzar excepción o devolver 'false' (asumiendo que no existe si hay error)
                // Lanzar la excepción es más seguro para saber que hubo un problema.
                throw new Exception($"Error al verificar existencia del código '{codigoBeneficiario}'.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general al verificar código beneficiario: {ex.ToString()}");
                throw new Exception($"Error al verificar existencia del código '{codigoBeneficiario}'.", ex);
            }
            return existe;
        }

        // --- OPCIONAL PERO RECOMENDADO: Guardar en Lote ---
        // public bool GuardarPacientesBatch(List<pacientes> listaPacientes)
        // {
        //     if (listaPacientes == null || !listaPacientes.Any()) return true; // Nada que guardar
        //
        //     using (var conn = DatabaseConnection.GetConnection())
        //     {
        //         conn.Open();
        //         // Usar una transacción para asegurar que todo se guarde o nada
        //         using (var transaction = conn.BeginTransaction())
        //         {
        //             try
        //             {
        //                 // Construir un comando INSERT grande o usar COPY de PostgreSQL (más avanzado)
        //                 // Ejemplo con múltiples INSERTs en una transacción (menos eficiente que COPY):
        //                  string query = "INSERT INTO pacientes (nombres, apellidos, edad, genero, codigo_beneficiario, fecha_nacimiento, id_proyecto, observacion) VALUES (@nombres, @apellidos, @edad, @genero, @codigo, @fechaNac, @idProy, @obs);";
        //                  foreach (var p in listaPacientes) {
        //                      using (var cmd = new NpgsqlCommand(query, conn, transaction)) { // Asociar comando a transacción
        //                           cmd.Parameters.AddWithValue("@nombres", p.nombres);
        //                           cmd.Parameters.AddWithValue("@apellidos", p.apellidos);
        //                           cmd.Parameters.AddWithValue("@edad", p.edad);
        //                           cmd.Parameters.AddWithValue("@genero", p.genero);
        //                           cmd.Parameters.AddWithValue("@codigo", p.codigo_beneficiario);
        //                           cmd.Parameters.AddWithValue("@fechaNac", p.fecha_nacimiento);
        //                           cmd.Parameters.AddWithValue("@idProy", p.id_proyecto);
        //                           cmd.Parameters.AddWithValue("@obs", (object)p.observacion ?? DBNull.Value); // Manejar posible null
        //                           cmd.ExecuteNonQuery();
        //                      }
        //                  }
        //
        //                 transaction.Commit(); // Si todo fue bien, confirma los cambios
        //                 return true;
        //             }
        //             catch (Exception ex)
        //             {
        //                 Console.WriteLine($"ERROR en GuardarPacientesBatch: {ex.ToString()}");
        //                 transaction.Rollback(); // Deshacer cambios si algo falló
        //                 return false;
        //             }
        //         } // Fin using transaction
        //     } // Fin using connection
        // }
    }

}