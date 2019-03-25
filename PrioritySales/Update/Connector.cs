using MySql.Data.MySqlClient;

namespace Update
{
    class Connector
    {
        private string IpCashServer = Config.GetParametr("IpCashServer");
        private string UpdateServerBase = Config.GetParametr("UpdateServerBase");
        private int CommandTimeout = int.Parse(Config.GetParametr("CommandTimeout"));
        private int ConnectTimeout = int.Parse(Config.GetParametr("ConnectTimeout"));

        public bool ExecuteNonQuery(string str)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=" + ConnectTimeout + ";", IpCashServer, "PrioritySail", "", UpdateServerBase)))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(str, conn);

                    cmd.CommandTimeout = CommandTimeout;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex) { Log.ExcWrite("[GetNameProgramUpdate] " + ex.Message); return false; }

            return true;
        }
    }
}
