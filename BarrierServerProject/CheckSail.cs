using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace BarrierServerProject
{
    public static class CheckSail
    {
        public static bool CheckSend = false;

        private static MySqlCommand cmd;
        private static MySqlConnection serverConn;
        private static string connStr;
        private static bool CheckInfoStatus = false;

        public static void StartCheck()
        {
            while(!File.Exists(Environment.CurrentDirectory + "\\data\\" + "balance.dbf"))
            {
                Color.WriteLineColor("Для проверки очередности продаж в папке программы 'data'\n должен находиться файл базы данных balance.dbf.\n\nПовторная проверка наличия файла через 10 секунд!", ConsoleColor.Red);

                //TODO query to create table
                //dbf.ExecuteNonQuery("CREATE TABLE " + Environment.CurrentDirectory + "\\data\\" + "balance.dbf" + " (barcode C(13,0) NOT NULL,price N(10,0) NOT NULL,count N(10,0) NOT NULL,date T(8,0) NOT NULL)");

                Thread.Sleep(10000);
            }

            Thread.Sleep(5000);

            CheckInfo();

            CleanBase();

            UpdateStateBase();

            //Thread.Sleep(1800000);

            //StartCheck();
        }

        private static void CleanDbf(string namedbf)
        {
            string connectionString = @"Provider=VFPOLEDB.1;Data Source=" + Environment.CurrentDirectory + "\\data\\";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand scriptCommand = connection.CreateCommand())
                {
                    bool stat = true;

                    try
                    {
                        connection.Open();

                        string vfpScript = "SET EXCLUSIVE ON\r\n                                 DELETE FROM " + namedbf + "\r\n                                PACK";

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
                            Color.WriteLineColor("Очистка и упаковка таблицы " + namedbf + " завершена успешно.", ConsoleColor.Green);
                        else
                        {
                            Color.WriteLineColor("Очистка и упаковка таблицы " + namedbf + " неудачна.", ConsoleColor.Red);

                            Dbf.ExecuteNonQuery("DELETE FROM " + namedbf);
                        }
                    }
                }
            }
        }

        private static void CheckInfo()
        {
            try
            {
                if (CheckInfoStatus)
                {
                    Color.WriteLineColor("На текущий момент производится добавление данных...Ожидание завершения процесса..", ConsoleColor.Cyan);

                    while (CheckInfoStatus)
                        Thread.Sleep(10000);
                }

                CheckInfoStatus = true;
                Color.WriteLineColor("CheckInfoStatus " + CheckInfoStatus, ConsoleColor.Red);

                connStr = string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "pricechecker", "***REMOVED***", Config.GetParametr("BdName"));

                serverConn = new MySqlConnection(connStr);

                serverConn.Open();

                CleanDbf("operation");
                Connector.ExecuteNonQuery("DELETE FROM tasks");

                if (File.Exists(Environment.CurrentDirectory + "\\data\\" + "balance.dbf"))
                {
                    OleDbDataReader dr = Dbf.ExecuteReader("SELECT * FROM balance.dbf");

                    if (dr == null)
                        return;

                    if (!dr.HasRows)
                        Color.WriteLineColor("База очередности пустая!", ConsoleColor.Red);

                    while (dr.Read())
                    {
                        string bar = dr.GetString(0).Replace(" ", "");
                        Int32 price = Convert.ToInt32(dr.GetValue(1));
                        double count = dr.GetDouble(2);
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

                        Color.WriteLineColor("Запрос на кассовый сервер...", ConsoleColor.Cyan);

                        reader = cmd.ExecuteReader();

                        if (!reader.HasRows)
                        {
                            NoSales(bar, price, date);
                        }

                        while (reader.Read())
                        {
                            if (TermsCheck(bar, price, count, reader.GetInt32(1), reader.GetDouble(0), date))
                                continue;
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
//                  if (serverConn.State != ConnectionState.Closed)
//                      serverConn.Close();

                CheckInfoStatus = false;
                Color.WriteLineColor("CheckInfoStatus " + CheckInfoStatus, ConsoleColor.Red);
                Color.WriteLineColor("Перебор завершен.", ConsoleColor.Cyan);
            }
           
        }

        public static void CheckOne(string bar, string operation, double count, Int32 price, DateTime date)
        {
            if (CheckInfoStatus)
            {
                Color.WriteLineColor("На текущий момент производится переформирование базы данных...Ожидание завершения процесса..", ConsoleColor.Cyan);

                while(CheckInfoStatus)
                    Thread.Sleep(10000);
            }

            CheckInfoStatus = true;
            Color.WriteLineColor("CheckInfoStatus " + CheckInfoStatus, ConsoleColor.Red);

            try
            {
                connStr = string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "pricechecker", "***REMOVED***", Config.GetParametr("BdName"));

                serverConn = new MySqlConnection(connStr);

                serverConn.Open();

                if (File.Exists(Environment.CurrentDirectory + "\\data\\" + "balance.dbf"))
                {
                    //TODO DEBUG parametrs
                    Color.WriteLineColor("[DEBUG] " + bar + ";" + price + ";" + count + ";" + date.ToString("yyyy-MM-dd,HH:mm:ss"), ConsoleColor.Gray);

                    Msg.SendUser("LsTradeAgent", "DR", 0, bar + ";" + price + ";" + count + ";" + date.ToString("yyyy-MM-dd,HH:mm:ss"));


                    //TODO CHECK TIME OPERATION EXECUTION , ABORT HANG OPERATION
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

                    Color.WriteLineColor("Запрос на кассовый сервер...", ConsoleColor.Cyan);

                    reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        NoSales(bar, price, date);
                    }

                    while (reader.Read())
                    {
                        if (TermsCheck(bar, price, count, reader.GetInt32(1), reader.GetDouble(0), date))
                            continue;
                    }

                    if (!reader.IsClosed)
                        reader.Close();
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

                Color.WriteLineColor("Добавление завершено.", ConsoleColor.Cyan);

                //TODO optimal logic not remove all data from mysql, add only one this barcode
                CleanBase();
                UpdateStateBase();

                CheckInfoStatus = false;

                Color.WriteLineColor("CheckInfoStatus " + CheckInfoStatus, ConsoleColor.Red);

                foreach (System.Collections.DictionaryEntry de in Server.clients)
                {
                    Msg.SendUser((de.Value).ToString(), "PrioritySale", 9, Packages.StatusString + ";" + DateTime.Now);
                }
            }
        }

        private static void NoSales(string bar, int price, DateTime date)
        {
            Color.WriteLineColor("Реализация товара: " + bar + " не обнаружена с " + date, ConsoleColor.Yellow);

            int TotalDay = Convert.ToInt32((DateTime.Now - date).TotalSeconds) / 86400;

            if (TotalDay > 2)
            {
                Color.WriteLineColor("Товар " + bar + " не продается долгое время.Число дней " + TotalDay, ConsoleColor.Red);

                Connector.ExecuteNonQuery("INSERT INTO `tasks`(`id`,`barcode`,`group`,`text`,`user_id`,`priority`) VALUES ( NULL," + bar + ",'1','Товар долго не продается.Число дней: " + TotalDay + "','0','1')");
            }

            Dbf.ExecuteNonQuery("INSERT INTO operation.dbf (barcode,operation,count,price,dt) VALUES ('" + bar + "',51,0," + price + ",{^" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "})");
        }

        private static bool TermsCheck(string bar, int price, double count, int priceukm, double countukm,DateTime date)
        {
            if (priceukm == price && countukm > count)
            {
                Color.WriteLineColor("Товар: " + bar + " по цене: " + priceukm + " продан больше чем было в очередности, количество: " + countukm + " по цене очередности", ConsoleColor.Cyan);

                Connector.ExecuteNonQuery("INSERT INTO `tasks`(`id`,`barcode`,`group`,`text`,`user_id`,`priority`) VALUES ( NULL," + bar + ",'2','Продано больше чем было в очередности,необходима прогрузка цены на кассу','0','3')");

                Dbf.ExecuteNonQuery("INSERT INTO operation.dbf (barcode,operation,count,price,dt) VALUES ('" + bar + "',51," + countukm.ToString().Replace(",", ".") + "," + price + ",{^" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "})");
                return true;
            }

            if (priceukm == price && countukm == count)
            {
                Color.WriteLineColor("Товар: " + bar + " по цене: " + priceukm + " продан в количестве: " + countukm + " по цене очередности", ConsoleColor.Cyan);

                Connector.ExecuteNonQuery("INSERT INTO `tasks`(`id`,`barcode`,`group`,`text`,`user_id`,`priority`) VALUES ( NULL," + bar + ",'1','Все продано по цене очереди,необходимо прогрузить новую цену на кассу','0','2')");

                Dbf.ExecuteNonQuery("INSERT INTO operation.dbf (barcode,operation,count,price,dt) VALUES ('" + bar + "',51," + countukm.ToString().Replace(",", ".") + "," + price + ",{^" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "})");
                return true;
            }

            if (priceukm == price && countukm < count)
            {
                Color.WriteLineColor("Товар: " + bar + " по цене: " + priceukm + " продан в количестве: " + countukm + " на текущий момент продается по правильной цене и есть остаток", ConsoleColor.Yellow);

                Connector.ExecuteNonQuery("INSERT INTO `tasks`(`id`,`barcode`,`group`,`text`,`user_id`,`priority`) VALUES ( NULL," + bar + ",'1','Продается по правильной цене и есть остаток!','0','9')");

                Dbf.ExecuteNonQuery("INSERT INTO operation.dbf (barcode,operation,count,price,dt) VALUES ('" + bar + "',51," + countukm.ToString().Replace(",", ".") + "," + price + ",{^" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "})");
                return true;
            }

            if (priceukm < price)
            {
                Color.WriteLineColor("Товар " + bar + " продается по неправильно цене! ", ConsoleColor.Red);
                Color.WriteLineColor("Цена на кассе: " + Convert.ToDouble(priceukm) + " Цена очередности: " + Convert.ToInt64(price), ConsoleColor.Red);
                Color.WriteLineColor("Проданое количество по дешевой цене: " + Convert.ToDouble(countukm), ConsoleColor.Red);

                Dbf.ExecuteNonQuery("INSERT INTO operation.dbf (barcode,operation,count,price,dt) VALUES ('" + bar + "',51," + countukm.ToString().Replace(",", ".") + "," + price + ",{^" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "})");

                Connector.ExecuteNonQuery("INSERT INTO `tasks`(`id`,`barcode`,`group`,`text`,`user_id`,`priority`) VALUES ( NULL," + bar + ",'1','Товар продается дешевле цены очерёдности!','0','4')");

                return true;
            }

            if (priceukm > price)
            {
                Color.WriteLineColor("Товар " + bar + " завышение цены! ", ConsoleColor.Red);
                Color.WriteLineColor("Цена на кассе: " + Convert.ToInt64(priceukm) + " Цена очередности: " + Convert.ToInt64(price), ConsoleColor.Red);
                Color.WriteLineColor("Проданое количество по дорогой цене: " + Convert.ToInt64(countukm), ConsoleColor.Red);

                Dbf.ExecuteNonQuery("INSERT INTO operation.dbf (barcode,operation,count,price,dt) VALUES ('" + bar + "',51," + countukm.ToString().Replace(",", ".") + "," + price + ",{^" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "})");

                Connector.ExecuteNonQuery("INSERT INTO `tasks`(`id`,`barcode`,`group`,`text`,`user_id`,`priority`) VALUES ( NULL," + bar + ",'1','Товар продается дороже цены очерёдности!','0','5')");
                return true;
            }

            return false;
        }


        private static void CleanBase()
        {
            Connector.ExecuteNonQuery("DELETE FROM state");
        }

        private static void UpdateStateBase()
        {
            OleDbDataReader reader = Dbf.ExecuteReader("SELECT balance.barcode,balance.item,balance.price,balance.count,operation.count,balance.date FROM balance,operation where (balance.barcode == operation.barcode) AND (balance.date == operation.dt)");            

            while (reader.Read())
            {
                string barcode = reader.GetString(0);
                string item = reader.GetString(1);
                Int32 price = Convert.ToInt32(reader.GetValue(2));
                Int32 count = Convert.ToInt32(reader.GetValue(3));
                decimal sail = reader.GetDecimal(4);
                Int32 status = 0;
                Int32 flag = 0;
                DateTime dt = reader.GetDateTime(5);

                //TODO CHECK FLAG

                Connector.ExecuteNonQuery("INSERT INTO `barrierserver`.`state`(`barcode`,`name`,`price`,`count`,`sailed`,`status`,`date`,`flag`) VALUES ( '" + barcode + "','" + item + "','" + price + "','" + count + "','" + sail.ToString().Replace(",", ".") + "','" + status + "','" + dt.ToString("yyyy-MM-dd,HH:mm:ss") + "','" + flag + "')");
            }

            using (MD5 md5Hash = MD5.Create())
            {
                Random rnd = new Random();

                Packages.StatusString = Packages.GetMd5Hash(md5Hash, (Packages.GetMd5Hash(md5Hash, rnd.Next(100000).ToString())));

                Color.WriteLineColor("Версия базы:" + Packages.StatusString,ConsoleColor.Cyan);
            }
        }
    }
}
