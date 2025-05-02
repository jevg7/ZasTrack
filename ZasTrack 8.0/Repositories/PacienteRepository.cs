using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using ZasTrack.Models;
using ZasTrack.Models.ExamenModel;
using static ZasTrack.Forms.Estudiantes.wVerPaciente;

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
        public async Task<int> obtTotalPacientesAsync(int idProyecto)
        {
            string query = "SELECT COUNT(*) FROM pacientes WHERE id_proyecto = @idProyecto";
            int count = 0;
            try
            {
                // Usar using para asegurar disposición de conexión y comando
                using (var conn = DatabaseConnection.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    await conn.OpenAsync(); // Abrir conexión asíncrona
                    var result = await cmd.ExecuteScalarAsync(); // Ejecutar escalar asíncrono
                    count = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en obtTotalPacientesAsync (Proyecto: {idProyecto}): {ex}");
              
            }
            return count;
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
        public bool ActualizarPaciente(pacientes paciente)
        {
            // Recalcular edad por si cambió la fecha de nacimiento
            int edadDb = DateTime.Today.Year - paciente.fecha_nacimiento.Year;
            if (DateTime.Today < paciente.fecha_nacimiento.AddYears(edadDb)) edadDb--;
            if (edadDb < 0) edadDb = 0;

            // Query para actualizar usando el ID del paciente
            string query = @"UPDATE pacientes SET
                                nombres = @nombres,
                                apellidos = @apellidos,
                                edad = @edad,           -- Actualizar edad calculada
                                genero = @genero,
                                codigo_beneficiario = @codigo, -- Permitir cambiar código? Cuidado con duplicados!
                                fecha_nacimiento = @fechaNac,
                                id_proyecto = @idProy,
                                observacion = @obs
                            WHERE id_paciente = @idPaciente"; // <-- WHERE por ID (PK) es más seguro

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        // Pasar todos los parámetros
                        cmd.Parameters.AddWithValue("@nombres", paciente.nombres);
                        cmd.Parameters.AddWithValue("@apellidos", paciente.apellidos);
                        cmd.Parameters.AddWithValue("@edad", edadDb); // Edad recalculada
                        cmd.Parameters.AddWithValue("@genero", paciente.genero);
                        cmd.Parameters.AddWithValue("@codigo", paciente.codigo_beneficiario);
                        cmd.Parameters.AddWithValue("@fechaNac", paciente.fecha_nacimiento);
                        cmd.Parameters.AddWithValue("@idProy", paciente.id_proyecto);
                        cmd.Parameters.AddWithValue("@obs", (object?)paciente.observacion ?? DBNull.Value); // Manejar posible null
                        cmd.Parameters.AddWithValue("@idPaciente", paciente.id_paciente); // El ID para el WHERE

                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0; // Devuelve true si se actualizó 1 fila
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ActualizarPaciente (ID: {paciente.id_paciente}): {ex.ToString()}");
                // throw; // Opcional: relanzar
                return false; // Indicar que falló
            }
        }

        // --- Método para Eliminar un Paciente por su ID ---
        public bool EliminarPaciente(int idPaciente)
        {
            string query = "DELETE FROM pacientes WHERE id_paciente = @id";
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idPaciente);
                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0; // True si se borró 1 fila
                    }
                }
            }
            catch (NpgsqlException ex) // Captura específica de PG para FK
            {
                Console.WriteLine($"Error PostgreSQL en EliminarPaciente (ID: {idPaciente}): {ex.Message} (SQLState: {ex.SqlState})");
                if (ex.SqlState == "23503")
                { // Foreign Key Violation
                    throw new InvalidOperationException("No se puede eliminar este paciente porque tiene datos asociados (muestras, exámenes, etc.).", ex);
                }
                throw; // Relanzar otros errores de BD
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general en EliminarPaciente (ID: {idPaciente}): {ex.ToString()}");
                throw; // Relanzar otros errores
            }
        }
        public pacientes? ObtenerPacientePorId(int idPaciente) // Usa 'pacientes?' para indicar que puede devolver null
        {
            pacientes? paciente = null; // Inicializar a null
            string query = "SELECT id_paciente, nombres, apellidos, edad, genero, codigo_beneficiario, fecha_nacimiento, id_proyecto, observacion FROM pacientes WHERE id_paciente = @id";

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idPaciente);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Si encuentra una fila
                            {
                                paciente = new pacientes
                                {
                                    id_paciente = reader.GetInt32(reader.GetOrdinal("id_paciente")),
                                    nombres = reader.GetString(reader.GetOrdinal("nombres")),
                                    apellidos = reader.GetString(reader.GetOrdinal("apellidos")),
                                    edad = reader.GetInt32(reader.GetOrdinal("edad")), // Lee la edad guardada
                                    genero = reader.GetString(reader.GetOrdinal("genero")),
                                    codigo_beneficiario = reader.GetString(reader.GetOrdinal("codigo_beneficiario")),
                                    fecha_nacimiento = reader.GetDateTime(reader.GetOrdinal("fecha_nacimiento")),
                                    id_proyecto = reader.GetInt32(reader.GetOrdinal("id_proyecto")),
                                    observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? null : reader.GetString(reader.GetOrdinal("observacion"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerPacientePorId (ID: {idPaciente}): {ex.ToString()}");
                // Puedes relanzar o devolver null según tu manejo de errores
                throw;
            }
            return paciente; // Devuelve el paciente encontrado o null
        }
        public List<PacienteViewModel> BuscarPacientesCompleto(string? criterio, int? idProyecto, string? filtroGenero, bool? filtroConMuestras, bool? filtroConExamenes)
        {
            var pacientesVM = new List<PacienteViewModel>();
            var sqlBuilder = new StringBuilder();

            // --- ¡¡REVISA Y ADAPTA ESTA SQL A TU BASE DE DATOS!! ---
            sqlBuilder.Append(@"
                SELECT
                    p.id_paciente, p.codigo_beneficiario, p.nombres, p.apellidos,
                    p.fecha_nacimiento, p.genero, p.edad,
                    COALESCE(pr.nombre, '') AS NombreProyecto,

                    -- Resumen de Muestras (EJEMPLO: Concatena ID o Tipo de muestra)
                    -- CAMBIA 'm.id_muestra' o añade JOIN a 'tipo_muestra' si es necesario
                    COALESCE(
                        (SELECT STRING_AGG(CAST(m.id_muestra AS TEXT), ', ' ORDER BY m.id_muestra)
                         FROM muestra m WHERE m.id_paciente = p.id_paciente),
                        'Ninguna'
                    ) AS ResumenMuestras,

                    -- Resumen de Exámenes (EJEMPLO: Concatena ID o Nombre de examen)
                    -- CAMBIA 'ex.id_examen' o añade JOIN a 'tipo_examen' si es necesario
                     COALESCE(
                         (SELECT STRING_AGG(CAST(ex.id_examen AS TEXT), ', ' ORDER BY ex.id_examen)
                          FROM examen ex
                          JOIN muestra m ON ex.id_muestra = m.id_muestra
                          WHERE m.id_paciente = p.id_paciente),
                         'Ninguno'
                     ) AS ResumenExamenes

                FROM
                    pacientes p
                LEFT JOIN
                    proyecto pr ON p.id_proyecto = pr.id_proyecto
                WHERE 1=1 "); // Cláusula base

            // --- Añadir Filtros Condicionales ---
            if (idProyecto.HasValue) sqlBuilder.Append(" AND p.id_proyecto = @idProyecto ");
            if (!string.IsNullOrWhiteSpace(criterio)) sqlBuilder.Append(@" AND (LOWER(p.nombres) LIKE LOWER(@criterioLike) OR LOWER(p.apellidos) LIKE LOWER(@criterioLike) OR LOWER(p.codigo_beneficiario) LIKE LOWER(@criterioLike)) ");
            if (!string.IsNullOrWhiteSpace(filtroGenero)) sqlBuilder.Append(" AND p.genero = @filtroGenero ");
            if (filtroConMuestras.HasValue) sqlBuilder.Append(filtroConMuestras.Value ? " AND EXISTS (SELECT 1 FROM muestra m WHERE m.id_paciente = p.id_paciente) " : " AND NOT EXISTS (SELECT 1 FROM muestra m WHERE m.id_paciente = p.id_paciente) ");
            if (filtroConExamenes.HasValue) sqlBuilder.Append(filtroConExamenes.Value ? " AND EXISTS (SELECT 1 FROM examen e JOIN muestra m ON e.id_muestra = m.id_muestra WHERE m.id_paciente = p.id_paciente) " : " AND NOT EXISTS (SELECT 1 FROM examen e JOIN muestra m ON e.id_muestra = m.id_muestra WHERE m.id_paciente = p.id_paciente) ");

            sqlBuilder.Append(" ORDER BY p.apellidos, p.nombres;");
            // --- Fin SQL ---

            string query = sqlBuilder.ToString();
            Console.WriteLine($"DEBUG SQL BuscarPacientesCompleto:\n{query}");

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    // Añadir parámetros (importante manejar nulls para los opcionales)
                    if (idProyecto.HasValue) cmd.Parameters.AddWithValue("@idProyecto", idProyecto.Value);
                    if (!string.IsNullOrWhiteSpace(criterio)) cmd.Parameters.AddWithValue("@criterioLike", $"%{criterio}%");
                    if (!string.IsNullOrWhiteSpace(filtroGenero)) cmd.Parameters.AddWithValue("@filtroGenero", filtroGenero);
                    // Los parámetros booleanos para EXISTS/NOT EXISTS no son necesarios si se construyen en la SQL

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pacientesVM.Add(new PacienteViewModel
                            { // Crear ViewModel
                                id_paciente = reader.GetInt32(reader.GetOrdinal("id_paciente")),
                                codigo_beneficiario = reader.GetString(reader.GetOrdinal("codigo_beneficiario")),
                                nombres = reader.GetString(reader.GetOrdinal("nombres")),
                                apellidos = reader.GetString(reader.GetOrdinal("apellidos")),
                                fecha_nacimiento = reader.GetDateTime(reader.GetOrdinal("fecha_nacimiento")),
                                genero = reader.GetString(reader.GetOrdinal("genero")),
                                edad = reader.GetInt32(reader.GetOrdinal("edad")),
                                NombreProyecto = reader.GetString(reader.GetOrdinal("NombreProyecto")),
                                ResumenMuestras = reader.GetString(reader.GetOrdinal("ResumenMuestras")),
                                ResumenExamenes = reader.GetString(reader.GetOrdinal("ResumenExamenes"))
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"ERROR en BuscarPacientesCompleto: {ex.ToString()}"); throw; }
            return pacientesVM;
        }
        public async Task<List<pacientes>> ObtenerPorProyectoIdAsync(int idProyecto)
        {
            var listaPacientes = new List<pacientes>();
            // Asegúrate que los nombres de columnas y tabla sean correctos
            string query = @"SELECT id_paciente, nombres, apellidos, edad, genero,
                                codigo_beneficiario, fecha_nacimiento, observacion
                         FROM pacientes
                         WHERE id_proyecto = @idProyecto
                         ORDER BY apellidos, nombres;"; // Ordenar alfabéticamente

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            listaPacientes.Add(new pacientes
                            {
                                id_paciente = reader.GetInt32(reader.GetOrdinal("id_paciente")),
                                nombres = reader.GetString(reader.GetOrdinal("nombres")),
                                apellidos = reader.GetString(reader.GetOrdinal("apellidos")),
                                edad = reader.IsDBNull(reader.GetOrdinal("edad")) ? 0 : reader.GetInt32(reader.GetOrdinal("edad")), // Manejar posible NULL en edad
                                genero = reader.IsDBNull(reader.GetOrdinal("genero")) ? "" : reader.GetString(reader.GetOrdinal("genero")), // Manejar NULL
                                codigo_beneficiario = reader.IsDBNull(reader.GetOrdinal("codigo_beneficiario")) ? "" : reader.GetString(reader.GetOrdinal("codigo_beneficiario")), // Manejar NULL
                                fecha_nacimiento = reader.GetDateTime(reader.GetOrdinal("fecha_nacimiento")), // Leer directamente
                                observacion = reader.IsDBNull(reader.GetOrdinal("observacion")) ? null : reader.GetString(reader.GetOrdinal("observacion")), // Manejar NULL
                                id_proyecto = idProyecto // Ya sabemos el id del proyecto
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en PacienteRepository.ObtenerPorProyectoIdAsync (ID: {idProyecto}): {ex}");
                // Relanzar para que la UI maneje el error
                throw;
            }
            return listaPacientes;
        }
        public async Task<int> ContarPorProyectoIdAsync(int idProyecto)
        {
            string query = "SELECT COUNT(*) FROM pacientes WHERE id_proyecto = @idProyecto";
            int count = 0;
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    await conn.OpenAsync();
                    // ExecuteScalarAsync devuelve object, hacemos cast seguro a long y luego a int
                    var result = await cmd.ExecuteScalarAsync();
                    count = Convert.ToInt32(result ?? 0L); // Convertir a long (0L) si es null, luego a int
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en PacienteRepository.ContarPorProyectoIdAsync (ID: {idProyecto}): {ex}");
                // Relanzar para que la UI maneje el error, o devolver -1 si prefieres
                throw;
            }
            return count;
        }

    }


}