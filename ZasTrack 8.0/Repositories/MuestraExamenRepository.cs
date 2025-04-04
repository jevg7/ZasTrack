using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasTrack.Models;


namespace ZasTrack.Repositories
{
    public class MuestraExamenRepository
    {

        private string connectionString;
        public MuestraExamenRepository()
        {
        }
        public MuestraExamenRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void InsertMuestraExamen(MuestraExamen muestraExamen)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    string query = @"
                    INSERT INTO muestra_examen (id_muestra, id_tipo_examen, id_proyecto)
                    VALUES (@IdMuestra, @IdTipoExamen)";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdMuestra", muestraExamen.IdMuestra);
                        command.Parameters.AddWithValue("@IdTipoExamen", muestraExamen.IdTipoExamen);
                        command.Parameters.AddWithValue("@IdProyecto", muestraExamen.IdProyecto);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones en caso de error
                MessageBox.Show($"Ocurrió un error al guardar la muestra examen: {ex.Message}");
            }
        }
    }
}
