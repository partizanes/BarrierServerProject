using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace BarrierServerProject
{
    class Connector
    {
        private static MySqlCommand cmd;
        private static MySqlConnection serverConn;
        private static string connStr;

        public static Boolean ExecuteNonQuery(string str)
        {
            try
            {
                connStr = string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "pricechecker", "***REMOVED***", "barrierserver");

                serverConn = new MySqlConnection(connStr);

                serverConn.Open();

                cmd = new MySqlCommand(str,serverConn);

                cmd.CommandTimeout = 0;

                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor("При запросе с базы данных произошло исключение. " + ex.Message, ConsoleColor.Red);
                return false;
            }

            return true;
        }

        public static  MySqlDataReader ExecuteReader(string str)
        {
            MySqlDataReader reader = null;

            try
            {
                connStr = string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "pricechecker", "***REMOVED***", "barrierserver");

                serverConn = new MySqlConnection(connStr);

                serverConn.Open();

                cmd = new MySqlCommand(str, serverConn);

                cmd.CommandTimeout = 0;

                reader = cmd.ExecuteReader();
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor("При запросе с базы данных произошло исключение. " + ex.Message, ConsoleColor.Red);
                return reader;
            }

            return reader;
        }

    }
}
