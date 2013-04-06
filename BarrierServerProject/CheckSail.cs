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
        public static void CheckAll()
        {
                Thread th = new Thread(delegate()
                {
                    Color.WriteLineColor("[Thread] CheckAll запущен", ConsoleColor.DarkYellow);

                    Color.WriteLineColor("Проверка цен продаж...", ConsoleColor.Cyan);
                    CheckErrorSailPrice();

                    Color.WriteLineColor("Проверка цен на кассе...", ConsoleColor.Cyan);
                    CheckCurrentPrices();

                    Color.WriteLineColor("Проверка статуса очередности...", ConsoleColor.Cyan);
                    CurrentStatusRemains();

                    Color.WriteLineColor("Запрос отправки цен на кассу...", ConsoleColor.Cyan);
                    CheckSendUkmPrice();

                    UpdateAllSailMainPrice();

                    Color.WriteLineColor("[Thread] CheckAll остановлен", ConsoleColor.DarkYellow);

                    SendUserNewState();
                }); ;
                th.Name = "CheckAll";
                th.Start();
        }

        public static void SendUserNewState()
        {
            using (MD5 md5Hash = MD5.Create())
            {
                Random rnd = new Random();

                Packages.StatusString = Packages.GetMd5Hash(md5Hash, (Packages.GetMd5Hash(md5Hash, rnd.Next(100000).ToString())));

                Color.WriteLineColor("Версия базы:" + Packages.StatusString, ConsoleColor.Cyan);
            }

            foreach (System.Collections.DictionaryEntry de in Server.clients)
            {
                Msg.SendUser((de.Value).ToString(), "PrioritySale", 9, Packages.StatusString + ";" + DateTime.Now);
            }
        }

        public static void CheckErrorSailPrice()
        {
            using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
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
            using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(@"SELECT p.id,p.turn_price,b.price FROM " + Connector.BarrierDataBase + ".priority p," + Connector.UkmDataBase + ".trm_in_var C LEFT JOIN " + Connector.UkmDataBase + ".trm_in_items A ON A.id=C.item LEFT JOIN " + Connector.UkmDataBase + ".trm_in_pricelist_items B ON B.item=c.item WHERE C.item = p.bar AND p.turn_price != b.price AND b.pricelist_id= 1", conn);

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


            using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
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
                            TasksAdd(id,1," Нужна прогрузка цены на кассу",3);
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
                            //TODO товар на подходе 99 status;
                        }

                        if (turn_count > sail_price)
                        {
                            //Все ок!
                        }
                    }
                }
            }
        }

        private static void CheckSendUkmPrice()
        {
            while (!Server.clients.ContainsValue("LsTradeAgent"))
            {
                Color.WriteLineColor("Внимание не запущен LsTradeAgent .\nДля продолжения работы нужно запустить LsTradeAgent", ConsoleColor.Red);
                    Thread.Sleep(5000);
            }

            using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT t.tasks_id,p.id,p.bar,p.date FROM tasks t,priority p WHERE  p.id = t.priority_id", conn);
                cmd.CommandTimeout = 0;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {
                            int tasks_id = dr.GetInt32(0);
                            int priority_id = dr.GetInt32(1);
                            string bar = dr.GetString(2);
                            string datetime = dr.GetDateTime(3).ToString("yyyy-MM-dd,HH:mm:ss");

                            //send user tasks_id;bar;datetime
                            Msg.SendUser("LsTradeAgent", "DR", 1, tasks_id + ";" + bar + ";" + datetime);
                            Thread.Sleep(200);
                        }
                    }

                    Color.WriteLineColor("Данные для проверки задач отправлены в LsTradeAgent.", ConsoleColor.Cyan);

                    Msg.SendUser("LsTradeAgent", "DR", 2, "Данные для проверки задач отправлены.");
                }
            }
        }

        private static void UpdateAllSailMainPrice()
        {
            if (Packages.connector.ExecuteNonQuery("UPDATE priority p SET `sailed` = (SELECT SUM(`count`) FROM operations o WHERE o.id = p.id )"))
                Color.WriteLineColor("Обновлен расход в основном перечне.", ConsoleColor.Gray);
            else
                Color.WriteLineColor("Обновлен расход в основном перечне.", ConsoleColor.Gray);
        }

        public static void TasksAdd(int id,int user_group, string task_text, int priority)
        {
            string b = Config.GetParametr("BarrierDataBase");

            string query = @"INSERT INTO `tasks`(`tasks_id`,`priority_id`,`user_group`,`task_text`,`user_id`,`priority`,`date`,`inactive`) VALUES ( NULL,'" + id + "','" + user_group + "','" + task_text + "','0','" + priority + "','" + DateTime.Now.ToString("yyyy-MM-dd,HH:mm:ss") + "','0');";

            string uquery = @"UPDATE `priority` SET `status` = " + priority + " WHERE id =" + id;

//           Log.LogWriteDebug(query);

            using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM `tasks` WHERE priority_id = " + id + " AND task_text = '" + task_text + "' AND priority =" + priority, conn);

                cmd.CommandTimeout = 0;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr == null) { Packages.connector.ExecuteNonQuery(query + uquery); return; }

                    if (!dr.HasRows) { Packages.connector.ExecuteNonQuery(query + uquery); return; }

                    while (dr.Read())
                    {
                        Color.WriteLineColor("Обнаружена идентичная задача.Статус обновлен.Записано в лог.Пропущено.", ConsoleColor.Yellow);
                        Packages.connector.ExecuteNonQuery(uquery);
                        Log.Write(cmd.CommandText,"1","IDENTICAL");
                        Log.Write(query, "2", "IDENTICAL");
                    }
                }
            }
        }
    }
}
