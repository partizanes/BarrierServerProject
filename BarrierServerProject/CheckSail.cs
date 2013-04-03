using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace BarrierServerProject
{
    public static class CheckSail
    {
        public static void CheckAll(bool one)
        {
            while (true)
            {
                Thread th = new Thread(delegate()
                {
                    Color.WriteLineColor("[Thread]CheckAll запущен", ConsoleColor.DarkYellow);

                    Color.WriteLineColor("Проерка цен продаж...", ConsoleColor.Cyan);
                    CheckErrorSailPrice();

                    Color.WriteLineColor("Проерка цен на кассе...", ConsoleColor.Cyan);
                    CheckCurrentPrices();

                    Color.WriteLineColor("Проерка статуса очередности...", ConsoleColor.Cyan);
                    CurrentStatusRemains();

                    Color.WriteLineColor("[Thread]CheckAll остановлен", ConsoleColor.DarkYellow);
                }); ;
                th.Name = "CheckAll";
                th.Start();

                if (one)
                    return;

                Thread.Sleep(3600000);
            }
        }

        public static void CheckErrorSailPrice()
        {
            using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", Config.GetParametr("BarrierDataBase"))))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(@"select p.id,p.turn_price,o.price from priority as p,operations as o where o.id = p.id AND p.turn_price != o.price AND o.inactive = 0", conn);

                cmd.CommandTimeout = 0;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr == null) { return; }

                    if (!dr.HasRows) { return; }

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int turn_price = dr.GetInt32(1);
                        int sailed_price = dr.GetInt32(2);

                        if (turn_price > sailed_price)
                            TasksAdd(id, 2, " Продажи дешевле цены очередности [" + sailed_price + "]", 5);
                        else
                            TasksAdd(id, 1, " Продажи дороже цены очередности [" + sailed_price + "]", 6);
                    }
                }
            }
        }

        public static void CheckCurrentPrices()
        {
            string b = Config.GetParametr("BarrierDataBase");
            string u = Config.GetParametr("BdName");

            using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", Config.GetParametr("BarrierDataBase"))))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(@"SELECT p.id,p.turn_price,b.price FROM " + b + ".priority p," + u + ".trm_in_var C LEFT JOIN " + u + ".trm_in_items A ON A.id=C.item LEFT JOIN " + u + ".trm_in_pricelist_items B ON B.item=c.item WHERE C.item = p.bar AND p.turn_price != b.price AND b.pricelist_id= 1", conn);

                cmd.CommandTimeout = 0;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr == null) { return; }

                    if (!dr.HasRows) { return; }

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int turn_price = Convert.ToInt32(dr.GetValue(1));
                        int sail_price = Convert.ToInt32(dr.GetValue(2));

                        if (turn_price > sail_price)
                            TasksAdd(id, 1, " Цена на кассе меньше цены очередности [" + sail_price + "]", 8);
                        else
                            TasksAdd(id, 1, " Цена на кассе больше цены очередности [" + sail_price + "]", 7);
                    }
                }
            }
        }

        private static void CurrentStatusRemains()
        {
            string b = Config.GetParametr("BarrierDataBase");


            using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", Config.GetParametr("BarrierDataBase"))))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(@"SELECT p.id,p.count,SUM(o.count) FROM operations o,priority p WHERE o.id = p.id GROUP BY o.id", conn);

                cmd.CommandTimeout = 0;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr == null) { return; }

                    if (!dr.HasRows) { return; }

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);

                        double turn_count = dr.GetDouble(1);
                        double sail_price = dr.GetDouble(2);

                        if(turn_count < sail_price)
                        {
                            TasksAdd(id,1," Нужна прогрузка цены на касску",3);
                            TasksAdd(id,2," Продано больше чем нужно",3);
                            continue;
                        }

                        if (turn_count == sail_price)
                        {
                            TasksAdd(id, 1, " Нужна прогрузка цены на касску", 2);
                            continue;
                        }

                        if (turn_count < (sail_price + 1))
                        {
                            //TODO товар на подходе; 
                        }

                        if (turn_count > sail_price)
                        {
                            //Все ок!
                        }
                    }
                }
            }
        }

        private static void TasksAdd(int id,int user_group, string task_text, int priority)
        {
            string b = Config.GetParametr("BarrierDataBase");

            string query = @"INSERT INTO `tasks`(`tasks_id`,`priority_id`,`user_group`,`task_text`,`user_id`,`priority`,`date`,`inactive`) VALUES ( NULL,'" + id + "','" + user_group + "','" + task_text + "','0','" + priority + "','" + DateTime.Now.ToString("yyyy-MM-dd,HH:mm:ss") + "','0')";

            Log.LogWriteDebug(query);

            using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", Config.GetParametr("BarrierDataBase"))))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM `tasks` WHERE priority_id = " + id + " AND task_text = '" + task_text + "' AND priority =" + priority, conn);

                cmd.CommandTimeout = 0;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr == null) { Packages.connector.ExecuteNonQuery(query); return; }

                    if (!dr.HasRows) { Packages.connector.ExecuteNonQuery(query); return; }

                    while (dr.Read())
                    {
                        Color.WriteLineColor("Обнаружена идентичная задача.Записано в лог.Пропущено.", ConsoleColor.Yellow);
                        Log.log_write(cmd.CommandText,"1","IDENTICAL");
                        Log.log_write(query, "2", "IDENTICAL");
                    }
                }
            }
        }

//         public static bool CheckSend = false;
// 
//         private static bool CheckInfoStatus = false;
// 
//         public static void StartCheck()
//         {
//             while(!File.Exists(Environment.CurrentDirectory + "\\data\\" + "balance.dbf"))
//             {
//                 Color.WriteLineColor("Для проверки очередности продаж в папке программы 'data'\n должен находиться файл базы данных balance.dbf.\n\nПовторная проверка наличия файла через 10 секунд!", ConsoleColor.Red);
// 
//                 Thread.Sleep(10000);
//             }
// 
//             Thread.Sleep(5000);
// 
//             while (true)
//             {
//                 CheckInfo();
// 
//                 CleanBase();
// 
//                 TasksUpdate();
// 
//                 UpdateStateBase();
// 
//                 Thread.Sleep(1800000);
//             }
//         }
// 
//         private static void TasksUpdate()
//         {
//             try
//             {
//                 Color.WriteLineColor("Возвращаем задания пользователей из временной таблицы.", ConsoleColor.Green);
// 
//                 using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", Config.GetParametr("BarrierDataBase"))))
//                 {
//                     conn.Open();
// 
//                     MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM `tmp_tasks`", conn);
//                     cmd.CommandTimeout = 0;
// 
//                     using (MySqlDataReader dr = cmd.ExecuteReader())
//                     {
//                         if (dr == null)
//                             return;
// 
//                         if (!dr.HasRows)
//                         {
//                             return;
//                         }
// 
//                         while (dr.Read())
//                         {
//                             Packages.connector.ExecuteNonQuery("UPDATE .`tasks` SET `user_id`='" + dr.GetValue(3) + "' WHERE `barcode` = '" + dr.GetValue(0) + "' AND `sailprice` = '" + dr.GetValue(1) + "' AND `group` = '" + dr.GetValue(2) + "' AND `priority`='" + dr.GetValue(4) + "' AND `date`='" + dr.GetDateTime(5) + "';");
//                         }
// 
//                         Packages.connector.ExecuteNonQuery("DELETE FROM `tmp_tasks`");
//                     }
//                 }
//             }
//             catch (System.Exception ex)
//             {
//                 Color.WriteLineColor("[TasksUpdate] " + ex.Message,ConsoleColor.Red);
//             }
//         }
// 
//         private static void CleanDbf(string namedbf)
//         {
//             string connectionString = @"Provider=VFPOLEDB.1;Data Source=" + Environment.CurrentDirectory + "\\data\\";
// 
//             using (OleDbConnection connection = new OleDbConnection(connectionString))
//             {
//                 using (OleDbCommand scriptCommand = connection.CreateCommand())
//                 {
//                     bool stat = true;
// 
//                     try
//                     {
//                         connection.Open();
// 
//                         string vfpScript = "SET EXCLUSIVE ON\r\n                                 DELETE FROM " + namedbf + "\r\n                                PACK\r\n                                 SET EXCLUSIVE OFf";
// 
//                         scriptCommand.CommandType = CommandType.StoredProcedure;
//                         scriptCommand.CommandText = "ExecScript";
//                         scriptCommand.Parameters.Add("myScript", OleDbType.Char).Value = vfpScript;
//                         scriptCommand.ExecuteNonQuery();
//                     }
//                     catch (System.Exception ex)
//                     {
//                         Console.WriteLine(ex.Message);
//                         stat = false;
//                     }
//                     finally
//                     {
//                         if (stat)
//                             Color.WriteLineColor("Очистка и упаковка таблицы " + namedbf + " завершена успешно.", ConsoleColor.Green);
//                         else
//                         {
//                             Color.WriteLineColor("Очистка и упаковка таблицы " + namedbf + " неудачна.", ConsoleColor.Red);
// 
//                             Dbf.ExecuteNonQuery("DELETE FROM " + namedbf);
//                         }
//                     }
//                 }
//             }
//         }
// 
//         private static void CheckInfo()
//         {
//             try
//             {
//                 if (CheckInfoStatus)
//                 {
//                     Color.WriteLineColor("На текущий момент производится добавление данных...Ожидание завершения процесса..", ConsoleColor.Cyan);
// 
//                     while (CheckInfoStatus)
//                         Thread.Sleep(10000);
//                 }
// 
//                 CheckInfoStatus = true;
// 
//                 Color.WriteLineColor("CheckInfoStatus: Проиходит обновление бд ,таблицы заблокированы.", ConsoleColor.Red);
// 
//                 CleanDbf("operation");
// 
//                 Color.WriteLineColor("Копируем во временную таблицу задания пользователей", ConsoleColor.Green);
// 
//                 Packages.connector.ExecuteNonQuery("INSERT INTO `tmp_tasks` (SELECT `tasks`.barcode, `tasks`.sailprice, `tasks`.GROUP, `tasks`.user_id, `tasks`.priority, `tasks`.DATE FROM `tasks` WHERE `tasks`.`user_id` > 0)");
// 
//                 Packages.connector.ExecuteNonQuery("DELETE FROM tasks");
// 
//                 if (File.Exists(Environment.CurrentDirectory + "\\data\\" + "balance.dbf"))
//                 {
//                     OleDbDataReader dr = Dbf.ExecuteReader("SELECT * FROM balance.dbf");
// 
//                     if (dr == null)
//                         return;
// 
//                     if (!dr.HasRows)
//                         Color.WriteLineColor("База очередности пустая!", ConsoleColor.Red);
// 
//                     while (dr.Read())
//                     {
//                         string bar = dr.GetString(0).Replace(" ", "");
//                         Int32 price = Convert.ToInt32(dr.GetValue(1));
//                         double count = dr.GetDouble(2);
//                         DateTime date = dr.GetDateTime(3);
// 
//                         GetSail(bar, date, price, count);
//                     }
//                 }
//             }
//             catch (System.Exception ex)
//             {
//                 Color.WriteLineColor(ex.Message, ConsoleColor.Red);
//             }
//             finally
//             {
//                 CheckInfoStatus = false;
//                 Color.WriteLineColor("CheckInfoStatus: Обновление завершено ,таблицы разблокированы.", ConsoleColor.Green);
//                 Color.WriteLineColor("Перебор завершен.", ConsoleColor.Cyan);
//             }
//            
//         }
// 
//         private static void GetSail(string bar, DateTime date, int price, double count)
//         {
//             Color.WriteLineColor("[DEBUG] " + bar + ";" + price + ";" + count + ";" + date.ToString("yyyy-MM-dd,HH:mm:ss"), ConsoleColor.Gray);
// 
//             Msg.SendUser("LsTradeAgent", "DR", 0, bar + ";" + price + ";" + count + ";" + date.ToString("yyyy-MM-dd,HH:mm:ss"));
// 
//             DateTime datetime = DateTime.Now;
// 
//             while (!CheckSend)
//             {
//                 Thread.Sleep(500);
// 
//                 if ((DateTime.Now - datetime).TotalSeconds > 60)
//                 {
//                     CheckSend = true;
//                     Color.WriteLineColor("LsTradeAgent не ответил вовремя.Операция отменена.", ConsoleColor.Red);
//                 }
//             }
// 
//             CheckSend = false;
// 
//             Color.WriteLineColor("Запрос на кассовый сервер...", ConsoleColor.Cyan);
// 
//             using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", Config.GetParametr("BdName"))))
//             {
//                 conn.Open();
// 
//                 MySqlCommand cmd = new MySqlCommand(@"SELECT SUM(IF(h.type IN (0,5), 1, -1) * i.quantity) quantity,i.price
// 												FROM trm_in_pos c INNER JOIN 
// 												trm_out_receipt_header h ON h.cash_id = c.cash_id INNER JOIN 
// 												trm_out_receipt_item i ON i.cash_id = h.cash_id AND i.receipt_header = h.id  LEFT JOIN 
// 												trm_out_receipt_item i2 ON (h.cash_id = i2.cash_id AND h.id = i2.receipt_header AND i2.link_item = i.id) INNER JOIN 
// 												trm_out_receipt_footer f ON f.cash_id = h.cash_id AND f.id = h.id 
// 												WHERE i2.link_item IS NULL AND i.type = 0 AND 
// 												h.type IN (0,5,1,4) AND f.result IN (0) AND 1001 = c.store_id AND i.item LIKE '" + bar + "%' AND f.date >= '" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "' GROUP BY i.price", conn);
//                 cmd.CommandTimeout = 0;
// 
//                 using (MySqlDataReader dr = cmd.ExecuteReader())
//                 {
//                     if (dr == null)
//                         return;
// 
//                     if (!dr.HasRows)
//                     {
//                         NoSales(bar, price, date);
//                     }
// 
//                     while (dr.Read())
//                     {
//                         if (TermsCheck(bar, price, count, dr.GetInt32(1), dr.GetDouble(0), date))
//                             continue;
//                     }
// 
//                     if (!dr.IsClosed)
//                         dr.Close();
//                 }
//             }
//         }
// 
//         public static void CheckOne(string bar, string operation, double count, Int32 price, DateTime date)
//         {
//             if (CheckInfoStatus)
//             {
//                 Color.WriteLineColor("На текущий момент производится переформирование базы данных...Ожидание завершения процесса..", ConsoleColor.Cyan);
// 
//                 while(CheckInfoStatus)
//                     Thread.Sleep(10000);
//             }
// 
//             CheckInfoStatus = true;
//             Color.WriteLineColor("CheckInfoStatus " + CheckInfoStatus, ConsoleColor.Red);
// 
//             try
//             {
//                 if (File.Exists(Environment.CurrentDirectory + "\\data\\" + "balance.dbf"))
//                 {
//                     //TODO DEBUG parametrs
// 
//                     GetSail(bar, date, price, count);
//                 }
//             }
//             catch (System.Exception ex)
//             {
//                 Color.WriteLineColor(ex.Message, ConsoleColor.Red);
//             }
//             finally
//             {
//                 Color.WriteLineColor("Добавление завершено.", ConsoleColor.Cyan);
// 
//                 //TODO optimal logic not remove all data from mysql, add only one this barcode
//                 CleanBase();
//                 UpdateStateBase();
// 
//                 CheckInfoStatus = false;
// 
//                 Color.WriteLineColor("CheckInfoStatus " + CheckInfoStatus, ConsoleColor.Red);
// 
//                 foreach (System.Collections.DictionaryEntry de in Server.clients)
//                 {
//                     Msg.SendUser((de.Value).ToString(), "PrioritySale", 9, Packages.StatusString + ";" + DateTime.Now);
//                 }
//             }
//         }
// 
//         private static void NoSales(string bar, int price, DateTime date)
//         {
//             Color.WriteLineColor("Реализация товара: " + bar + " не обнаружена с " + date, ConsoleColor.Yellow);
// 
//             int TotalDay = Convert.ToInt32((DateTime.Now - date).TotalSeconds) / 86400;
// 
//             if (TotalDay > 2)
//             {
//                 Color.WriteLineColor("Товар " + bar + " не продается долгое время.Число дней " + TotalDay, ConsoleColor.Red);
// 
//                 Packages.connector.ExecuteNonQuery("INSERT INTO `tasks`(`id`,`barcode`,`sailprice`,`group`,`text`,`user_id`,`priority`,`date`) VALUES ( NULL," + bar + ",'0','1','долго не продается.Число дней: " + TotalDay + "','0','1','" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "')");
//             }
// 
//             Dbf.ExecuteNonQuery("INSERT INTO operation.dbf (barcode,operation,count,price,dt) VALUES ('" + bar + "',51,0,0,{^" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "})");
//         }
// 
//         private static bool TermsCheck(string bar, int price, double count, int priceukm, double countukm,DateTime date)
//         {
//             Int32 _CurrentPrice = 0;
// 
//             if (priceukm == price && countukm > count)
//             {
//                 Color.WriteLineColor("Товар: " + bar + " по цене: " + priceukm + " продан больше чем было в очередности, количество: " + countukm + " по цене очередности", ConsoleColor.Cyan);
// 
//                 Packages.connector.ExecuteNonQuery("INSERT INTO `tasks`(`id`,`barcode`,`sailprice`,`group`,`text`,`user_id`,`priority`,`date`) VALUES ( NULL," + bar + ",'" + priceukm + "','2','Продано больше чем было в очередности,необходима прогрузка цены на кассу','0','3','" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "')");
// 
//                 Dbf.ExecuteNonQuery("INSERT INTO operation.dbf (barcode,operation,count,price,dt) VALUES ('" + bar + "',51," + countukm.ToString().Replace(",", ".") + "," + priceukm + ",{^" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "})");
//                 return true;
//             }
// 
//             if (priceukm == price && countukm == count)
//             {
//                 Color.WriteLineColor("Товар: " + bar + " по цене: " + priceukm + " продан в количестве: " + countukm + " по цене очередности", ConsoleColor.Cyan);
// 
//                 Packages.connector.ExecuteNonQuery("INSERT INTO `tasks`(`id`,`barcode`,`sailprice`,`group`,`text`,`user_id`,`priority`,`date`) VALUES ( NULL," + bar + ",'" + priceukm + "','1','продано по цене очереди,необходимо прогрузить новую цену на кассу','0','2','" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "')");
// 
//                 Dbf.ExecuteNonQuery("INSERT INTO operation.dbf (barcode,operation,count,price,dt) VALUES ('" + bar + "',51," + countukm.ToString().Replace(",", ".") + "," + priceukm + ",{^" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "})");
//                 return true;
//             }
// 
//             if (priceukm == price && countukm < count)
//             {
//                 Color.WriteLineColor("Товар: " + bar + " по цене: " + priceukm + " продан в количестве: " + countukm + " на текущий момент продается по правильной цене и есть остаток", ConsoleColor.Yellow);
// 
//                 //Packages.connector.ExecuteNonQuery("INSERT INTO `tasks`(`id`,`barcode`,`group`,`text`,`user_id`,`priority`,`date`) VALUES ( NULL," + bar + ",'1','Продается по правильной цене и есть остаток!','0','9','" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "')");
// 
//                 Dbf.ExecuteNonQuery("INSERT INTO operation.dbf (barcode,operation,count,price,dt) VALUES ('" + bar + "',51," + countukm.ToString().Replace(",", ".") + "," + priceukm + ",{^" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "})");
//                 return true;
//             }
// 
//             if (priceukm < price)
//             {
//                 using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", Config.GetParametr("BdName"))))
//                 {
//                     conn.Open();
// 
//                     MySqlCommand cmd = new MySqlCommand(@"SELECT b.price FROM trm_in_var C LEFT JOIN trm_in_items A ON A.id=C.item LEFT JOIN trm_in_pricelist_items B ON B.item=c.item WHERE C.item= '" + bar + "' AND b.pricelist_id= 1", conn);
//                     cmd.CommandTimeout = 0;
// 
//                     using (MySqlDataReader dr = cmd.ExecuteReader())
//                     {
//                         if (dr == null)
//                             return false;
// 
//                         if (!dr.HasRows) { return false; }
// 
//                         while (dr.Read())
//                         {
//                             _CurrentPrice = dr.GetInt32(0);
//                         }
//                     }
//                 }
// 
//                 if (price <= _CurrentPrice)
//                 {
//                     Color.WriteLineColor("Товар " + bar + " был продан по неправильной цене! ", ConsoleColor.Red);
//                     Color.WriteLineColor("Цена продажи: " + priceukm + " Цена очередности: " + price + " Текущая цена на кассе: " + _CurrentPrice, ConsoleColor.Red);
//                     Color.WriteLineColor("Проданое количество по дешевой цене: " + countukm, ConsoleColor.Red);
// 
//                     Dbf.ExecuteNonQuery("INSERT INTO operation.dbf (barcode,operation,count,price,dt) VALUES ('" + bar + "',51," + countukm.ToString().Replace(",", ".") + "," + priceukm + ",{^" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "})");
// 
//                     Packages.connector.ExecuteNonQuery("INSERT INTO `tasks`(`id`,`barcode`,`sailprice`,`group`,`text`,`user_id`,`priority`,`date`) VALUES ( NULL," + bar + ",'" + priceukm + "','1','были продажи дешевле цены очерёдности!','0','5','" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "')");
//                 }
// 
//                 if (price > _CurrentPrice)
//                 {
//                     Color.WriteLineColor("Товар " + bar + " был продан по неправильной цене! ", ConsoleColor.Red);
//                     Color.WriteLineColor("Цена продажи: " + priceukm + " Цена очередности: " + price + " Текущая цена на кассе: " + _CurrentPrice, ConsoleColor.Red);
//                     Color.WriteLineColor("Проданое количество по дешевой цене: " + countukm, ConsoleColor.Red);
// 
//                     Dbf.ExecuteNonQuery("INSERT INTO operation.dbf (barcode,operation,count,price,dt) VALUES ('" + bar + "',51," + countukm.ToString().Replace(",", ".") + "," + priceukm + ",{^" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "})");
// 
//                     Packages.connector.ExecuteNonQuery("INSERT INTO `tasks`(`id`,`barcode`,`sailprice`,`group`,`text`,`user_id`,`priority`,`date`) VALUES ( NULL," + bar + ",'" + priceukm + "','1','были продажи дешевле цены очерёдности ,неверная цена на кассе!','0','4','" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "')");
//                 }
// 
//                 return true;
//             }
// 
//             if (priceukm > price)
//             {
//                 Color.WriteLineColor("Товар " + bar + " завышение цены! ", ConsoleColor.Red);
//                 Color.WriteLineColor("Цена на кассе: " + priceukm + " Цена очередности: " + price , ConsoleColor.Red);
//                 Color.WriteLineColor("Проданое количество по дорогой цене: " + countukm, ConsoleColor.Red);
// 
//                 Dbf.ExecuteNonQuery("INSERT INTO operation.dbf (barcode,operation,count,price,dt) VALUES ('" + bar + "',51," + countukm.ToString().Replace(",", ".") + "," + priceukm + ",{^" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "})");
// 
//                 Packages.connector.ExecuteNonQuery("INSERT INTO `tasks`(`id`,`barcode`,`sailprice`,`group`,`text`,`user_id`,`priority`,`date`) VALUES ( NULL," + bar + ",'" + priceukm + "','1','продается дороже цены очерёдности!','0','6','" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "')");
//                 return true;
//             }
// 
//             return false;
//         }
// 
//         private static void CleanBase()
//         {
//             Packages.connector.ExecuteNonQuery("DELETE FROM state;DELETE FROM sendprice");
//         }
// 
//         private static void UpdateStateBase()
//         {
//             OleDbDataReader reader = Dbf.ExecuteReader("SELECT balance.barcode,balance.item,balance.price,balance.count,operation.count,operation.price,balance.date FROM balance,operation where (balance.barcode == operation.barcode) AND (balance.date == operation.dt)");
// 
//             if (reader == null)
//                 return;
// 
//             while (reader.Read())
//             {
//                 string barcode = reader.GetString(0);
//                 string item = reader.GetString(1);
//                 Int32 price = Convert.ToInt32(reader.GetValue(2));
//                 double count = Convert.ToDouble(reader.GetValue(3));
//                 double sail = Convert.ToDouble(reader.GetValue(4));
//                 double pricesail = Convert.ToDouble(reader.GetValue(5));
//                 Int32 status = 0;
// 
//                 DateTime dt = reader.GetDateTime(6);
// 
//                 using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", Config.GetParametr("BarrierDataBase"))))
//                 {
//                     conn.Open();
// 
//                     MySqlCommand cmd = new MySqlCommand("SELECT priority FROM tasks WHERE `barcode` = '" + barcode + "' AND `sailprice` = " + pricesail + " AND `date` = '" + dt.ToString("yyyy-MM-dd,HH:mm:ss") + "'", conn);
//                     cmd.CommandTimeout = 0;
// 
//                     using (MySqlDataReader dr = cmd.ExecuteReader())
//                     {
//                         while (dr.Read())
//                         {
//                             if (!dr.IsDBNull(0))
//                                 status = Convert.ToInt32(dr.GetValue(0));
//                         }
//                     }
//                 }
// 
//                 Packages.connector.ExecuteNonQuery("INSERT INTO `barrierserver`.`state`(`barcode`,`name`,`price`,`sailprice`,`count`,`sailed`,`status`,`date`) VALUES ( '" + barcode + "','" + item + "','" + price + "','" + pricesail + "','" + count.ToString().Replace(",", ".") + "','" + sail.ToString().Replace(",", ".") + "'," + status + ",'" + dt.ToString("yyyy-MM-dd,HH:mm:ss") + "')");
//             }
// 
// 
//             using (MD5 md5Hash = MD5.Create())
//             {
//                 Random rnd = new Random();
// 
//                 Packages.StatusString = Packages.GetMd5Hash(md5Hash, (Packages.GetMd5Hash(md5Hash, rnd.Next(100000).ToString())));
// 
//                 Color.WriteLineColor("Версия базы:" + Packages.StatusString,ConsoleColor.Cyan);
//             }
// 
//             foreach (System.Collections.DictionaryEntry de in Server.clients)
//             {
//                 Msg.SendUser((de.Value).ToString(), "PrioritySale", 9, Packages.StatusString + ";" + DateTime.Now);
//             }
//         }
    }
}
