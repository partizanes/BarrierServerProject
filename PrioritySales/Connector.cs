using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows;

namespace PrioritySales
{

    class Connector
    {
        public static string BarrierDataBase = Config.GetParametr("BarrierDataBase");
        public static string UkmDataBase = Config.GetParametr("UkmDataBase");
        public static string IpCashServer = Config.GetParametr("IpCashServer");
        public static int CommandTimeout = int.Parse(Config.GetParametr("CommandTimeout"));
        public static int ConnectTimeout = int.Parse(Config.GetParametr("ConnectTimeout"));
        public static string BarrierStringConnecting = string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=" + ConnectTimeout + ";", IpCashServer, "BarrierServerR", "***REMOVED***", BarrierDataBase);
        public static string UkmStringConnecting = string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60" + ConnectTimeout + ";", IpCashServer, "BarrierServerR", "***REMOVED***", UkmDataBase);

        public static Boolean ExecuteNonQuery(string str)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(str, conn);

                    cmd.CommandTimeout = CommandTimeout;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("[Connector][ExecuteNonQuery]" + ex.Message);

                Log.ExcWrite("[Connector][ExecuteNonQuery]" + str);
                Log.ExcWrite("[Connector][ExecuteNonQuery]" + ex.Message);
            }

            return true;
        }
    }
}
