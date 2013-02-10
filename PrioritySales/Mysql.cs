using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace PrioritySales
{
    class Mysql
    {
        private MySqlCommand cmd;
        private MySqlConnection serverConn;
        private string connStr;

        public Boolean ExecuteNonQuery(string str)
        {
            connStr = string.Format("server={0};uid={1};pwd={2};database={3};", "192.168.1.100", "pricechecker", "***REMOVED***", "ukmserver");

            serverConn = new MySqlConnection(connStr);

            try
            {
                serverConn.Open();

                cmd = new MySqlCommand(str, serverConn);

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (System.Exception ex)
            {
                Log.log_write(ex.Message, "Exception", "Exception");
                return false;
            }
            finally
            {
                if (serverConn.State == ConnectionState.Open)
                    serverConn.Close();
            }
        }

        public MySqlDataReader ExecuteReader(string str)
        {
            MySqlDataReader reader;

            connStr = string.Format("server={0};uid={1};pwd={2};database={3};", "192.168.1.3", "pricechecker", "***REMOVED***", "ukmserver");

            serverConn = new MySqlConnection(connStr);

            try
            {
                serverConn.Open();

                cmd = new MySqlCommand(str, serverConn);

                reader = cmd.ExecuteReader();

                return reader;
            }
            catch (System.Exception ex)
            {
                Log.log_write(ex.Message, "Exception", "Exception");
                return null;
            }
        }
    }
}
