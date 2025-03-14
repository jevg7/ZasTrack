using Npgsql;
using System;
using System.Collections.Generic;
using ZasTrack.Models;

namespace ZasTrack.Repositories
{
    public class ProyectoRepository
    {
        public void GuardarProyecto(Poyecto proyecto)
        {
            string query = @"
                INSERT INTO proyecto (nombre, fecha_inicio, fecha_fin)
                VALUES (@nombre, @fecha_inicio, @fecha_fin)";

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("nombre", proyecto.nombre);
                    cmd.Parameters.AddWithValue("fecha_inicio", proyecto.fecha_inicio);
                    cmd.Parameters.AddWithValue("fecha_fin", proyecto.fecha_fin ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Poyecto> ObtenerProyectos()
        {
            var proyectos = new List<Poyecto>();
            string query = "SELECT id_proyecto, nombre, fecha_inicio, fecha_fin FROM proyecto";

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            proyectos.Add(new Poyecto
                            {
                                id_proyecto = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                fecha_inicio = reader.GetDateTime(2),
                                fecha_fin = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)
                            });
                        }
                    }
                }
            }

            return proyectos;
        }
    }
}