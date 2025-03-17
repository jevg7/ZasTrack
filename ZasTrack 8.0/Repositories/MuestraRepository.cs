using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasTrack.Models;

namespace ZasTrack.Repositories
{
    public class MuestraRepository
    {
        private string connectionString;

        public MuestraRepository()
        {
        }

        public MuestraRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void InsertMuestra(Muestra muestra)
        {
            try
            {
                // Verifica que la cadena de conexión esté definida correctamente
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    string query = @"
                INSERT INTO muestra (id_proyecto, id_paciente, id_tipo_examen, numero_muestra, fecha_recepcion)
                VALUES (@IdProyecto, @IdPaciente, @IdTipoExamen, @NumeroMuestra, @FechaRecepcion)";

                    // Comando para ejecutar la consulta
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        // Agrega los parámetros de la consulta
                        command.Parameters.AddWithValue("@IdProyecto", muestra.IdProyecto);
                        command.Parameters.AddWithValue("@IdPaciente", muestra.IdPaciente);
                        command.Parameters.AddWithValue("@IdTipoExamen", muestra.IdTipoExamen);
                        command.Parameters.AddWithValue("@NumeroMuestra", muestra.NumeroMuestra);
                        command.Parameters.AddWithValue("@FechaRecepcion", muestra.FechaRecepcion);

                        // Abre la conexión y ejecuta la consulta
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                // Mensaje de éxito
                MessageBox.Show("Muestra guardada correctamente.");
            }
            catch (Exception ex)
            {
                // Manejo de excepciones en caso de error
                MessageBox.Show($"Ocurrió un error al guardar la muestra: {ex.Message}");
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
            catch (Exception ex)
            {
                // Manejo de excepciones para detectar problemas de conexión
                Console.WriteLine($"Error al obtener la última muestra: {ex.Message}");
                throw;  // Relanza la excepción para manejarla en otro nivel si es necesario
            }

            return ultimaMuestra;
        }




    }
}