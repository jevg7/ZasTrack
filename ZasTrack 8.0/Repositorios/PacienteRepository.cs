using Npgsql;
using System;
using System.Collections.Generic;
using ZasTrack.Models;

namespace ZasTrack.Repositories
{
    public class PacienteRepository
    {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        public List<pacientes> ObtenerPacientes()
        {
            var pacientes = new List<pacientes>();
            string query = "SELECT id_paciente, nombres, apellidos, edad, genero, codigo_beneficiario, fecha_nacimiento, observacion FROM pacientes";

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
                                observacion = reader.IsDBNull(7) ? null : reader.GetString(7)
                            });
                        }
                    }
                }
            }

            return pacientes;
        }
        public void BuscarPacientePorCodigo(string codigo)
        {
            string query = "SELECT * FROM pacientes WHERE codigo_beneficiario = @codigo";

            using (var conn = DatabaseConnection.GetConnection())
            {
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pacientes pacientes = new pacientes
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

            using (var conexion = DatabaseConnection.GetConnection())
            {
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


    }
}