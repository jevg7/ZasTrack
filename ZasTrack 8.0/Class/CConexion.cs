using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace ZasTrack.Class
{
    internal class CConexion
    {
        private string cadena = "Host=aws-0-us-east-2.pooler.supabase.com;Username=postgres.qhvzvrxcuwipnwrnbwxd;Password=0uOCajlsEsiYdD1i;Database=postgres;Port=6543;SSL Mode=Require;Trust Server Certificate=true;";
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;

        public CConexion()
        {
            conn = new NpgsqlConnection(cadena);
        }

        public bool multiple(String query)
        {
            conn.Open();
            cmd = new NpgsqlCommand();

            cmd.CommandText = query;
            cmd.Connection = conn;
            int i = cmd.ExecuteNonQuery();

            conn.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable tablas(string tabla, string sql)
        {
            DataSet ds = new DataSet();
            NpgsqlDataAdapter da;
            DataTable dt = new DataTable();
            da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds, tabla);
            dt = ds.Tables[tabla];
            return (dt);
        }
        
           




        /* public DataTable ExecuteQuery(string query)
         {
             DataTable dataTable = new DataTable();

             try
             {
                 conn.Open();
                 cmd = new NpgsqlCommand(query, conn);
                 NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                 adapter.Fill(dataTable);
             }
             catch (Exception ex)
             {
                 // Handle any exceptions
             }
             finally
             {
                 conn.Close();
             }

             return dataTable;
         }*/
    }
}
