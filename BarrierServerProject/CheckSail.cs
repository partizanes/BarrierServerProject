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
            try
            {
                connStr = string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "pricechecker", "***REMOVED***", Config.GetParametr("BdName"));

                serverConn = new MySqlConnection(connStr);

                serverConn.Open();

                CleanDbf(Environment.CurrentDirectory + "\\data\\");

                if (File.Exists(Environment.CurrentDirectory + "\\data\\" + "balance.dbf"))
                {
                    Dbf dbf = new Dbf();

                    OleDbDataReader dr = dbf.ExecuteReader("SELECT * FROM balance.dbf");

                    if (dr == null)
                        return;

                    if (!dr.HasRows)
                        Color.WriteLineColor("База очередности пустая!", ConsoleColor.Red);

                    while (dr.Read())
                    {
                        string bar = dr.GetString(0).Replace(" ", "");
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

                        cmd = new MySqlCommand(@"SELECT SUM(IF(h.type IN (0,5), 1, -1) * i.quantity) quantity,i.price
												FROM trm_in_pos c INNER JOIN 
												trm_out_receipt_header h ON h.cash_id = c.cash_id INNER JOIN 
												trm_out_receipt_item i ON i.cash_id = h.cash_id AND i.receipt_header = h.id  LEFT JOIN 
												trm_out_receipt_item i2 ON (h.cash_id = i2.cash_id AND h.id = i2.receipt_header AND i2.link_item = i.id) INNER JOIN 
												trm_out_receipt_footer f ON f.cash_id = h.cash_id AND f.id = h.id 
												WHERE i2.link_item IS NULL AND i.type = 0 AND 
												h.type IN (0,5,1,4) AND f.result IN (0) AND 1001 = c.store_id AND i.item LIKE '" + bar + "%' AND f.date >= '" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "' GROUP BY i.price", serverConn);

                        cmd.CommandTimeout = 0;

                        MySqlDataReader reader;

                        reader = cmd.ExecuteReader();

                        if (!reader.HasRows)
                        {
                            Color.WriteLineColor("Реализация по товару: " + bar + " не обнаружена с " + date, ConsoleColor.Yellow);
                        }

                        while (reader.Read())
                        {
                            object counturm = reader.GetValue(0);
                            object priceurm = reader.GetValue(1);

                            if (priceurm == price && Convert.ToInt64(counturm) >= Convert.ToInt64(count))
                            {
                                Color.WriteLineColor("Товар: " + bar + " по цене: " + priceurm + " продан в количестве: " + counturm + " по цене очередности", ConsoleColor.Cyan);
                            }

                            if (priceurm == price && Convert.ToInt64(counturm) < Convert.ToInt64(count))
                            {
                                Color.WriteLineColor("Товар: " + bar + " по цене: " + priceurm + " продан в количестве: " + counturm + " на текущий момент продается по правильной цене и есть остаток", ConsoleColor.Yellow);
                            }

                            if (priceurm != price)
                            {
                                Color.WriteLineColor("Товар " + bar + " продается по неправильной цене! ", ConsoleColor.Red);
                                Color.WriteLineColor("Цена на кассе: " + Convert.ToInt64(priceurm) + " Цена очередности: " + Convert.ToInt64(price), ConsoleColor.Red);
                                Color.WriteLineColor("Продается количество по неверной цене: " + Convert.ToInt64(counturm), ConsoleColor.Red);

                                dbf.ExecuteNonQuery("INSERT INTO action.dbf (barcode,status,msg) VALUES ('" + bar + "','6'," + "'Товар продается по неверной цене!')");
                            }
                        }

                        if (!reader.IsClosed)
                            reader.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor(ex.Message, ConsoleColor.Red);
            }
            finally
            {
//                 if (serverConn.State != ConnectionState.Closed)
//                     serverConn.Close();
            }
           
        }
    }
}
