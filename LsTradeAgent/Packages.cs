using System;
using System.Collections.Generic;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using System.Threading;

namespace LsTradeAgent
{
    class Packages
    {
        private static Boolean StatusParse = true;
        public static Connector connector = new Connector();
        private static List<string> ar = new List<string>();

        public static void parse(string p_id, int com, string msg)
        {
            switch (p_id)
            {
                case "LS":
                    switch (com)
                    {
                        case 0:
                            break;
                        case 1:
                            Color.WriteLineColor(msg, ConsoleColor.Cyan);
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                    }
                    break;
                case "DR":
                    switch (com)
                    {
                        case 0:
                            Color.WriteLineColor("Принято: " + msg, ConsoleColor.Green);

                            CheckSailOff(msg);

                            Color.WriteLineColor("Пакет обработан!", ConsoleColor.Green);

                            break;
                        case 1:
                            Color.WriteLineColor("Принято: " + msg, ConsoleColor.Green);
                            ar.Add(msg);
                            break;
                        case 2:
                            Thread th = new Thread(delegate()
                                {
                                    Color.WriteLineColor("[Thread] Поток парсер создан", ConsoleColor.Yellow);

                                    List<string> ar_copy = new List<string>();

                                    foreach (string str in ar)
                                    {
                                        ar_copy.Add(str);
                                    }

                                    ar.Clear();
                                    ar = null;
                                    GC.Collect();

                                    Color.WriteLineColor("Сделана копия массива", ConsoleColor.Blue);

                                    foreach (string str in ar_copy)
                                    {
                                        string[] split_data = str.Split(new Char[] { ';' });

                                        string priority_id = split_data[0];
                                        string barcode = split_data[1];
                                        string date = split_data[2];

                                        CheckSendPrice(priority_id, barcode, date);
                                    }

                                    ar_copy = null;
                                    GC.Collect();

                                    Color.WriteLineColor("[Thread] Поток парсер завершен", ConsoleColor.Yellow);
                                });
                                th.Name = "Проверка общая";
                                th.Start();

                            break;
                    }
                    break;
            }
        }

        private static void CheckSendPrice(string priority_id, string barcode, string date)
        {
            string kass = @"К3";
            string cena = @"Ц2";

            try
            {
                using (OleDbConnection OleDbconn = new OleDbConnection(Dbf.ConnectingString))
                {
                    OleDbconn.Open();

                    //TODO k_dev in config;
                    OleDbCommand OleDbcmd = new OleDbCommand(@"select n_cenu,kod_isp,p_time,k_dev from dvkinpr where k_grup = '" + barcode + "' AND p_time > {^ " + date + " } AND k_dev IN('" + kass + "','" + cena + "')");

                    OleDbcmd.Connection = OleDbconn;

                    OleDbcmd.CommandTimeout = 0;

                    using (OleDbDataReader OleDbDr = OleDbcmd.ExecuteReader())
                    {
                        if (OleDbDr == null || !OleDbDr.HasRows)
                        {
                            Color.WriteLineColor("Отсутствуют данные отправки на кассу по штрихкоду: " + barcode, ConsoleColor.Yellow);
                            return;
                        }

                        using (MySqlConnection MySqlconn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "PrioritySailR", "***REMOVED***", Config.GetParametr("BarrierDataBase"))))
                        {
                            MySqlCommand MySqlCmd = new MySqlCommand();


                            Color.WriteLineColor("Открытие соединения для добавления данных отправки на кассу...", ConsoleColor.Green);

                            MySqlconn.Open();

                            MySqlCmd.Connection = MySqlconn;

                            MySqlCmd.CommandText = "INSERT IGNORE INTO `sendPOS` VALUES(@id, @price, @kod_isp, @datetime, @action)";
                            MySqlCmd.Prepare();

                            MySqlCmd.Parameters.AddWithValue("@id", 1);
                            MySqlCmd.Parameters.AddWithValue("@price", 8500);
                            MySqlCmd.Parameters.AddWithValue("@kod_isp", 22);
                            MySqlCmd.Parameters.AddWithValue("@datetime", "2012-01-01 00:00:00");
                            MySqlCmd.Parameters.AddWithValue("@action", "text");

                            while (OleDbDr.Read())
                            {
                                MySqlCmd.Parameters["@id"].Value = priority_id;
                                MySqlCmd.Parameters["@price"].Value = OleDbDr.GetValue(0);
                                MySqlCmd.Parameters["@kod_isp"].Value = OleDbDr.GetValue(1);
                                MySqlCmd.Parameters["@datetime"].Value = OleDbDr.GetDateTime(2).ToString("yyyy-MM-dd,HH:mm:ss");
                                MySqlCmd.Parameters["@action"].Value = OleDbDr.GetValue(3);

                                MySqlCmd.ExecuteNonQuery();

                                Color.WriteLineColor("[sendPOS] Добавлена строка " + priority_id, ConsoleColor.DarkYellow);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor("[CheckSendPrice]" + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[CheckSendPrice]" + ex.Message);
            }
        }

        private static void CheckSailOff(string msg)
        {
            try
            {
                StatusParse = true;

                string[] split_data = msg.Replace("\0", "").Split(new Char[] { ';' });

                int id = int.Parse(split_data[0].Replace(" ", ""));
                string barcode = split_data[1].Replace(" ", "");

                using (OleDbConnection conn = new OleDbConnection(string.Format("Provider=vfpoledb.1;Data Source=" + Config.GetParametr("LsTradeDir") + ";Mode=Read;Collating Sequence=MACHINE;CODEPAGE=1251")))
                {
                    conn.Open();

                    OleDbCommand cmd = new OleDbCommand(@"SELECT k_mat,k_op,SUM(n_mat),n_cenu FROM dvmat WHERE k_mat IN(SELECT k_mat FROM sprmat WHERE k_grup  ='" + barcode + "' AND p_time > {^" + split_data[3] + "}) AND k_op IN ('53', '61','62','72','93') AND p_time > {^" + split_data[3] + "} GROUP BY k_mat,k_op,n_cenu");

                    cmd.Connection = conn;

                    cmd.CommandTimeout = 0;

                    using (OleDbDataReader DbfDataReader = cmd.ExecuteReader())
                    {
                        connector.ExecuteNonQuery("DELETE FROM `operations` WHERE `id` = " + id + " AND `operation` IN ('53', '61','62','72','93')", Config.GetParametr("BarrierDataBase"));

                        if (DbfDataReader == null)
                        {
                            Color.WriteLineColor("Отсутствуют партии расхода по баркоду: " + barcode, ConsoleColor.Yellow);
                            return;
                        }

                        while (DbfDataReader.Read())
                        {
                            string count = DbfDataReader.GetValue(2).ToString().Replace(",", ".");
                            string oper = DbfDataReader.GetString(1);
                            string price = DbfDataReader.GetValue(3).ToString().Replace(",", ".");

                            if (connector.ExecuteNonQuery("INSERT INTO `operations`(`id`,`operation`,`count`,`price`,`inactive`) VALUES ( '" + id + "','" + oper + "','" + count + "','" + price + "','0');", Config.GetParametr("BarrierDataBase")))
                                Color.WriteLineColor("Добавлен код операции: " + oper + " Количество: " + count + " Цена: " + price, ConsoleColor.Yellow);
                            else
                                Color.WriteLineColor("Отклонен код операции: " + oper + " Количество: " + count + " Цена: " + price, ConsoleColor.Red);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor("[CheckSailOff] " + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[CheckSailOff] " + ex.Message);

                StatusParse = false;
            }

            finally
            {
                Server.Sender("LsTradeAgent", 8, "Пакет успешно обработан!");

                if (StatusParse && Program.Debug)
                {
                    Color.WriteLineColor("[DEBUG] Пакет успешно обработан!", ConsoleColor.Cyan);
                }

                StatusParse = false;
            }
        }
    }
}
