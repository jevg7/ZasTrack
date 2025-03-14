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
        INSERT INTO pacientes (nombres, apellidos, edad, genero, codigo_beneficiario, fecha_nacimiento, observacion)
        VALUES (@nombres, @apellidos, @edad, @genero, @codigo_beneficiario, @fecha_nacimiento, @observacion)";

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
                        cmd.Parameters.AddWithValue("observacion", paciente.observacion ?? (object)DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el paciente: " + ex.Message, ex);
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
    }
}