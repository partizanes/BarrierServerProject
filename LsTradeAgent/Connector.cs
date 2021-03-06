﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LsTradeAgent
{
    class Connector
    {
        public static string BarrierDataBase = Config.GetParametr("BarrierDataBase");
        public static string UkmDataBase = Config.GetParametr("UkmDataBase");
        public static string IpCashServer = Config.GetParametr("IpCashServer");
        private static int CommandTimeout = int.Parse(Config.GetParametr("CommandTimeout"));
        public static int ConnectTimeout = int.Parse(Config.GetParametr("ConnectTimeout"));
        public static string BarrierStringConnecting = string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=" + ConnectTimeout + ";", IpCashServer, "", "", BarrierDataBase);
        public static string UkmStringConnecting = string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60" + ConnectTimeout + ";", IpCashServer, "", "", UkmDataBase);


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
                Color.WriteLineColor("При запросе с базы данных произошло исключение. " + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[Connector][ExecuteNonQuery]" + str);
                Log.ExcWrite("[Connector][ExecuteNonQuery]" + ex.Message);
            }

            return true;
        }
    }
}
