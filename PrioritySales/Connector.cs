using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;

namespace PrioritySales
{

    class Connector
    {
        private MySqlCommand cmd;
        private MySqlConnection serverConn;
        private string connStr;

        public Boolean ExecuteNonQuery(string str,string bdname)
        {
            try
            {
                connStr = string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "PrioritySail", "***REMOVED***", bdname);

                serverConn = new MySqlConnection(connStr);

                serverConn.Open();

                cmd = new MySqlCommand(str,serverConn);

                cmd.CommandTimeout = 0;

                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("При запросе произошло исключение. " + ex.Message);
                return false;
            }
            finally
            {
                if(serverConn.State != ConnectionState.Closed)
                    serverConn.Close();
            }

            return true;
        }
    }
}
