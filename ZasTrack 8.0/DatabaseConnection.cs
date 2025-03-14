using Npgsql;

namespace ZasTrack
{
    public static class DatabaseConnection
    {
        private static string connectionString = "Host=aws-0-us-east-2.pooler.supabase.com;Username=postgres.qhvzvrxcuwipnwrnbwxd;Password=0uOCajlsEsiYdD1i;Database=postgres;Port=6543;SSL Mode=Require;Trust Server Certificate=true;Timeout=30";

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
            
        }
    }


}