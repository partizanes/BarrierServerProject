using System;
using System.Collections.Generic;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

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
                            foreach (string str in ar)
                            {
                                string[] split_data = str.Split(new Char[] { ';' });

                                string id = split_data[0];
                                string barcode = split_data[1];
                                string date = split_data[2];

                                CheckSendPrice(id, barcode, date);
                            }

                            ar.Clear();

                            break;
                    }
                    break;
            }
        }

        private static void CheckSendPrice(string id,string barcode,string date)
        {
            OleDbDataReader dr = null;

            //TODO k_dev in config;
            dr = Dbf.dbf_read("select n_cenu,kod_isp,p_time,k_dev from dvkinpr where k_grup = '" + barcode + "' AND p_time > {^ " + date + " } AND k_dev IN('К3','Ц2')");

            if (dr == null)
            {
                Color.WriteLineColor("Отсутствуют данные отправки на кассу по штрихкоду: " + barcode, ConsoleColor.Yellow);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "PrioritySailR", "***REMOVED***", Config.GetParametr("BarrierDataBase"))))
            {
                MySqlCommand cmd = new MySqlCommand();

                try
                {
                    conn.Open();

                    cmd.Connection = conn;

                    cmd.CommandText = "INSERT IGNORE INTO `sendPOS` VALUES(@id, @price, @kod_isp, @datetime, @action)";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@id", 1);
                    cmd.Parameters.AddWithValue("@price", 8500);
                    cmd.Parameters.AddWithValue("@kod_isp", 22);
                    cmd.Parameters.AddWithValue("@datetime", "2012-01-01 00:00:00" );
                    cmd.Parameters.AddWithValue("@action", "text");

                    while (dr.Read())
                    {
                        cmd.Parameters["@id"].Value = id;
                        cmd.Parameters["@price"].Value = dr.GetValue(0);
                        cmd.Parameters["@kod_isp"].Value = dr.GetValue(1);
                        cmd.Parameters["@datetime"].Value = dr.GetDateTime(2).ToString("yyyy-MM-dd,HH:mm:ss");
                        cmd.Parameters["@action"].Value = dr.GetValue(3);

                        cmd.ExecuteNonQuery();
                    }

                }
                catch (System.Exception ex)
                {
                    Color.WriteLineColor(ex.Message, ConsoleColor.Red);
                }
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
                Color.WriteLineColor("[GetLsTradeSail] " + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[GetLsTradeSail] " + ex.Message);

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
