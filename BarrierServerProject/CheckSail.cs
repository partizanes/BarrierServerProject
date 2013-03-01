using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Threading;
using MySql.Data.MySqlClient;

namespace BarrierServerProject
{
    public static class CheckSail
    {
        public static bool CheckSend = false;

        private static MySqlCommand cmd;
        private static MySqlConnection serverConn;
        private static string connStr;

        public static void StartCheck()
        {
            while(!File.Exists(Environment.CurrentDirectory + "\\data\\" + "balance.dbf"))
            {
                Color.WriteLineColor("Для проверки очередности продаж в папке программы 'data'\n должен находиться файл базы данных balance.dbf.\n\nПовторная проверка наличия файла через 10 секунд!", ConsoleColor.Red);
                Thread.Sleep(10000);
            }

            Thread.Sleep(5000);

            CheckInfo();

            CheckFinal();
        }

        private static void CleanDbf(string namedbf)
        {
            string connectionString = @"Provider=VFPOLEDB.1;Data Source=" + namedbf;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand scriptCommand = connection.CreateCommand())
                {
                    bool stat = true;

                    try
                    {
                        connection.Open();

                        string vfpScript = @"SET EXCLUSIVE ON
                                DELETE FROM operation
                                PACK";

                        scriptCommand.CommandType = CommandType.StoredProcedure;
                        scriptCommand.CommandText = "ExecScript";
                        scriptCommand.Parameters.Add("myScript", OleDbType.Char).Value = vfpScript;
                        scriptCommand.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        stat = false;
                    }
                    finally
                    {
                        if (stat)
                            Color.WriteLineColor("Очистка и упаковка таблицы operation завершена успешно.",ConsoleColor.Green);
                        else
                            Color.WriteLineColor("Очистка и упаковка таблицы operation неудачна.", ConsoleColor.Red);
                    }
                }
            }
        }

        public static void CheckInfo()
        {
            CleanDbf(Environment.CurrentDirectory + "\\data\\");

            if (File.Exists(Environment.CurrentDirectory + "\\data\\" + "balance.dbf"))
            {
                Dbf dbf = new Dbf();

                OleDbDataReader dr = dbf.ExecuteReader("SELECT * FROM balance.dbf");

                if (dr == null)
                    return;

                if (!dr.HasRows)
                    Color.WriteLineColor("База очередности пустая!",ConsoleColor.Red);

                while (dr.Read())
                {
                    string bar = dr.GetString(0).Replace(" ","");
                    object price = dr.GetValue(1);
                    object count = dr.GetValue(2);
                    DateTime date = dr.GetDateTime(3);

                    Color.WriteLineColor("[DEBUG] " + bar + ";" + price + ";" + count + ";" + date.ToString("yyyy-MM-dd,HH:mm:ss"), ConsoleColor.Gray);

                    Msg.SendUser("LsTradeAgent", "DR", 0, bar + ";" + price + ";" + count + ";" + date.ToString("yyyy-MM-dd,HH:mm:ss"));

                    while (!CheckSend)
                    {
                        Thread.Sleep(500);
                    }

                    CheckSend = false;
                }
            }
        }

        public static void CheckFinal()
        {
            MySqlDataReader reader;

            connStr = string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "pricechecker", "***REMOVED***", "ukmserver");

            serverConn = new MySqlConnection(connStr);

            try
            {
                serverConn.Open();

                cmd = new MySqlCommand("", serverConn);

                cmd.CommandTimeout = 0;

                reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {

                }

                while (reader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                Log.log_write(ex.Message, "Exception", "Exception");
                Color.WriteLineColor(ex.Message, ConsoleColor.Red);
            }
            finally
            {
                if (serverConn.State == ConnectionState.Open)
                {
                    serverConn.Close();
                }
            }
        }
    }
}
