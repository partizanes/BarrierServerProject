using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace BarrierServerProject
{
    class Connector
    {
        private MySqlCommand cmd;
        private MySqlConnection serverConn;
        private string connStr;

        public Boolean ExecuteNonQuery(string str)
        {
            try
            {
                connStr = string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServer", "***REMOVED***", "barrierserver");

                serverConn = new MySqlConnection(connStr);

                serverConn.Open();

                cmd = new MySqlCommand(str,serverConn);

                cmd.CommandTimeout = 0;

                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor("При запросе с базы данных произошло исключение. " + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[Connector][ExecuteNonQuery]" + str);
                Log.ExcWrite("[Connector][ExecuteNonQuery]" + ex.Message);
                return false;
            }
            finally
            {
                if (serverConn.State != ConnectionState.Closed)
                    serverConn.Close();
            }

            return true;
        }
    }
}
