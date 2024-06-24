using MySql.Data.MySqlClient;

namespace QLMamNon.Facade
{
    public static class MySqlFacade
    {
        public static void BackupDb(string connectionString, string outputFile)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(outputFile);
                        conn.Close();
                    }
                }
            }
        }
    }
}
