using Npgsql;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ZasTrack.Models;
using ZasTrack.Models.ExamenModel;
using ZasTrack.Models.Informes;

namespace ZasTrack.Repositories
{
    public class MuestraRepository
    {
        private ExamenRepository _examenRepository;
        // --- Constructor de la clase ---
        public MuestraRepository() // O InformeRepository()
        {
            _examenRepository = new ExamenRepository();
            
        }
        public int GuardarMuestras(Muestra muestra)
        {
            string query = @"
        INSERT INTO muestra (fecha_recepcion, id_proyecto, id_paciente, numero_muestra)
        VALUES (@FechaRecepcion, @IdProyecto, @IdPaciente, @NumeroMuestra)
        RETURNING id_muestra";

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
                        cmd.Parameters.AddWithValue("@NumeroMuestra", muestra.NumeroMuestra);
                        return Convert.ToInt32(cmd.ExecuteScalar());


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
        public int CountByProjectAndDate(int idProyecto, DateTime fecha)
        {
            // Compara solo la parte de la fecha
            string query = "SELECT COUNT(id_muestra) FROM muestra WHERE id_proyecto = @idProyecto AND fecha_recepcion::date = @fecha";
            int count = 0;
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open(); // Considerar OpenAsync si el repo es async
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                        cmd.Parameters.AddWithValue("@fecha", fecha.Date); // Asegura comparar solo fecha
                        var result = cmd.ExecuteScalar(); // Puede ser null si no hay filas
                        count = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CountByProjectAndDate: {ex.Message}");
                throw; // O manejar el error como prefieras
            }
            return count;
        }
        public List<MuestraInfoViewModel> GetUltimasMuestrasPorProyecto(int idProyecto, int limite = 5) // limite=5 por defecto
        {
            var ultimasMuestras = new List<MuestraInfoViewModel>();
            // Unimos muestra con pacientes, filtramos por proyecto, ordenamos descendente por fecha/ID y limitamos
            string query = @"
        SELECT 
            m.id_muestra, m.numero_muestra, p.nombres || ' ' || p.apellidos AS paciente, 
            m.fecha_recepcion 
        FROM muestra m
        INNER JOIN pacientes p ON m.id_paciente = p.id_paciente
        WHERE m.id_proyecto = @idProyecto
        ORDER BY m.fecha_recepcion DESC, m.id_muestra DESC 
        LIMIT @limite;
    ";
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                        cmd.Parameters.AddWithValue("@limite", limite);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Creamos un ViewModel simplificado o reusamos MuestraInfoViewModel
                                // Asegúrate que MuestraInfoViewModel tenga las propiedades necesarias
                                ultimasMuestras.Add(new MuestraInfoViewModel
                                {
                                    id_Muestra = reader.GetInt32(reader.GetOrdinal("id_muestra")),
                                    NumeroMuestra = reader.GetInt32(reader.GetOrdinal("numero_muestra")),
                                    Paciente = reader.GetString(reader.GetOrdinal("paciente")),
                                    FechaRecepcion = reader.GetDateTime(reader.GetOrdinal("fecha_recepcion")),
                                    // Otras propiedades del ViewModel pueden quedar null o vacías
                                    Genero = "",
                                    Edad = 0,
                                    ExamenesPendientesStr = "",
                                    ExamenesCompletadosStr = ""
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetUltimasMuestrasPorProyecto: {ex.Message}");
                throw; // O retorna lista vacía
            }
            return ultimasMuestras;
        }
       
        

    }
}

