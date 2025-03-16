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

        public MuestraRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void InsertMuestra(Muestra muestra)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "INSERT INTO muestra (id_paciente, id_tipo_examen, numero_muestra, fecha_muestra, fecha_informe) VALUES (@IdPaciente, @IdTipoExamen, @NumeroMuestra, @FechaMuestra, @FechaInforme)";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdPaciente", muestra.IdPaciente);
                command.Parameters.AddWithValue("@IdTipoExamen", muestra.IdTipoExamen);
                command.Parameters.AddWithValue("@NumeroMuestra", muestra.NumeroMuestra);
                command.Parameters.AddWithValue("@FechaMuestra", muestra.FechaMuestra);
                command.Parameters.AddWithValue("@FechaInforme", (object)muestra.FechaInforme ?? DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
       

    }
}