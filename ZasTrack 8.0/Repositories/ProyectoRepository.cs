﻿using Npgsql;
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

        public List<Proyecto> ObtenerProyectos()
        {
            var proyectos = new List<Proyecto>();
            string query = "SELECT id_proyecto, nombre, fecha_inicio, fecha_fin FROM proyecto";

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
                                    id_proyecto = reader.GetInt32(0),
                                    nombre = reader.GetString(1),
                                    fecha_inicio = reader.GetDateTime(2),
                                    fecha_fin = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)
                                });
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Error de PostgreSQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
            }

            return proyectos;
        }
    }
}