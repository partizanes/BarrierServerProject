using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BarrierServerProject
{
    public static class CheckThisBar
    {
        public static bool busyLsTradeAgent = false;
        public static string GetBarOnID(int id)
        {
            string bar = "";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT `bar` FROM `priority` WHERE `id` = " + id, conn);

                    cmd.CommandTimeout = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            bar = dr.GetString(0);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor("[GetBarOnID] " + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[GetBarOnID] " + ex.Message);
            }

            return bar;
        }

        public static int GetPriceUkmId(int id)
        {
            int price = 0;

            string _ukmservname = Config.GetParametr("UkmDataBase");
            string _mainservname = Config.GetParametr("BarrierDataBase");

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.UkmStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT b.price,MAX(b.version) FROM " + _ukmservname + ".trm_in_var C LEFT JOIN " + _ukmservname + ".trm_in_items A ON A.id=C.item LEFT JOIN "
                        + _ukmservname + ".trm_in_pricelist_items B ON B.item=c.item WHERE (C.id= (SELECT `bar` FROM "
                        + _mainservname + ".`priority` WHERE `id` = '" + id + "' ) OR C.item= (SELECT `bar` FROM "
                        + _mainservname + ".`priority` WHERE `id` = '" + id + "' )) AND b.pricelist_id= "
                        + Config.GetParametr("pricelist_id"), conn);

                    cmd.CommandTimeout = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            price = dr.GetInt32(0);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor("[GetPriceUkmId] " + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[GetPriceUkmId] " + ex.Message);
            }

            if(price == 0)
            {
                Color.WriteLineColor("[GetPriceUkmId] Запрос не вернул цену.", ConsoleColor.Red);
                Log.ExcWrite("[GetPriceUkmId] Запрос не вернул цену.");
            }

            return price;
        }

        public static int GetPriceUkm(string bar)
        {
            int price = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.UkmStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT b.price,MAX(b.version) FROM trm_in_var C LEFT JOIN trm_in_items A ON A.id=C.item LEFT JOIN trm_in_pricelist_items B ON B.item=c.item WHERE (C.item= '"
                        + bar + "' OR C.id= '" + bar + "')  AND b.pricelist_id= "
                        + Config.GetParametr("pricelist_id"), conn);
                    
                    cmd.CommandTimeout = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            price = dr.GetInt32(0);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor("[GetPriceUkm] " + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[GetPriceUkm] " + ex.Message);
            }

            if (price == 0)
            {
                Color.WriteLineColor("[GetPriceUkmId] Запрос не вернул цену.", ConsoleColor.Red);
                Log.ExcWrite("[GetPriceUkmId] Запрос не вернул цену.");
            }

            return price;
        }

        public static void GetLsTradeSail(int id)
        {
            if (!TimeSpanExtensions.IsTimeWork())
                return;

            while (busyLsTradeAgent)
            {
                Color.WriteLineColor("LsTradeAgent занят...", ConsoleColor.Red);
                Thread.Sleep(1000);
            }

            Color.WriteLineColor("busyLsTradeAgent = true", ConsoleColor.Cyan);
            busyLsTradeAgent = true;

            while (!Server.clients.ContainsValue("LsTradeAgent"))
            {
                Color.WriteLineColor("Внимание не запущен LsTradeAgent ..Ожидаю подключение...", ConsoleColor.Red);
                    Thread.Sleep(5000);
            }

            string bar;
            int price;
            DateTime date;

            Color.WriteLineColor("Запрос в LsTrade информации по списаниям...", ConsoleColor.Cyan);

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT `bar`,`turn_price`,`date` FROM `priority` WHERE `id` = " + id, conn);

                    cmd.CommandTimeout = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            bar = dr.GetString(0);
                            price = dr.GetInt32(1);
                            date = dr.GetDateTime(2);

                            Msg.SendUser("LsTradeAgent", "DR", 0, id + ";" + bar + ";" + price + ";" + date.ToString("yyyy-MM-dd,HH:mm:ss"));
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor("[GetLsTradeSail] " + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[GetLsTradeSail] " + ex.Message);
            }

            finally
            {
                Color.WriteLineColor("Запрос отправлен.", ConsoleColor.Gray);
                Thread.Sleep(2000);
                Color.WriteLineColor("busyLsTradeAgent = false", ConsoleColor.Cyan);
                busyLsTradeAgent = false;
            }
        }

        public static void GetUkmSailParametrs(int id)
        {
            string bar;
            int turn_price;
            DateTime date;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT `bar`,`turn_price`,`date` FROM `priority` WHERE `id` = " + id, conn);

                    cmd.CommandTimeout = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            bar = dr.GetString(0);
                            turn_price = dr.GetInt32(1);
                            date = dr.GetDateTime(2);

                            GetUkmSail(id, bar, turn_price, date);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor("[GetUkmSailParametrs] " + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[GetUkmSailParametrs] " + ex.Message);
            }
        }

        public static void GetUkmSail(int id, string bar, int turn_price, DateTime date)
        {
            Color.WriteLineColor("Запрос на кассовый сервер информации по продажам...", ConsoleColor.Cyan);

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.UkmStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT SUM(IF(h.type IN (0,5), 1, -1) * i.quantity) quantity,i.price
             												FROM trm_in_pos c INNER JOIN 
             												trm_out_receipt_header h ON h.cash_id = c.cash_id INNER JOIN 
             												trm_out_receipt_item i ON i.cash_id = h.cash_id AND i.receipt_header = h.id  LEFT JOIN 
             												trm_out_receipt_item i2 ON (h.cash_id = i2.cash_id AND h.id = i2.receipt_header AND i2.link_item = i.id) INNER JOIN 
             												trm_out_receipt_footer f ON f.cash_id = h.cash_id AND f.id = h.id 
             												WHERE i2.link_item IS NULL AND i.type = 0 AND 
             												h.type IN (0,5,1,4) AND f.result IN (0) AND 1001 = c.store_id AND i.item LIKE '" + bar + "%' AND f.date >= '" + date.ToString("yyyy-MM-dd,HH:mm:ss") + "' GROUP BY i.price", conn);
                    cmd.CommandTimeout = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        Packages.connector.ExecuteNonQuery("DELETE FROM `operations` WHERE `id` = " + id + " AND `operation` = '51'");
                        if (dr == null) { }

                        if (!dr.HasRows) { NoSales(id, bar, turn_price, date); }

                        while (dr.Read())
                        {
                            string count = dr.GetValue(0).ToString().Replace(",", ".");

                            if (Packages.connector.ExecuteNonQuery("INSERT INTO `operations`(`id`,`operation`,`count`,`price`,`inactive`) VALUES ( '" + id + "','51','" + count + "','" + dr.GetString(1).Replace(",", ".") + "','0')"))
                                Color.WriteLineColor("[" + id + "] Успешно добавлена операция расхода(51)  по штрихкоду " + bar, ConsoleColor.Blue);
                            else
                                Color.WriteLineColor("[" + id + "] Отклонена операция расхода(51) по штрихкоду " + bar, ConsoleColor.Red);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor("[GetUkmSail] " + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[GetUkmSail] " + ex.Message);
            }
        }

        public static void UpdatePrice(int id)
        {
            if(Program.debug)
                Color.WriteLineColor("[" + id + "] Запрос текущей цены на кассе. ", ConsoleColor.Blue);

            int price_ukm = GetPriceUkmId(id);

            if(Packages.connector.ExecuteNonQuery("UPDATE `priority` SET `current_price_ukm` = " + price_ukm + "   WHERE `id` = " + id))
                Color.WriteLineColor("[" + id + "] Цена обновлена. ", ConsoleColor.Blue);
            else
                Color.WriteLineColor("[" + id + "] Отказ обновления цены. ", ConsoleColor.Red);
        }

        public static void UpdateCountOut(int id)
        {
            if(Packages.connector.ExecuteNonQuery("UPDATE `priority` SET `sailed` = (SELECT SUM(`count`) FROM `operations` WHERE `id` = " + id + ")"))
                Color.WriteLineColor("[" + id + "] Обновлен расход в основном перечне при добавлении штрихкода", ConsoleColor.Blue);
            else
                Color.WriteLineColor("[" + id + "] Отказано в обновлении расхода в основном перечне при добавлении штрихкода.", ConsoleColor.Red);
        }

        public static void GetSailAndPrice(int id)
        {
            Color.WriteLineColor("Запущено обновление информации по штрихкоду " + CheckThisBar.GetBarOnID(id), ConsoleColor.DarkYellow);

            CheckThisBar.UpdatePrice(id);

            CheckThisBar.GetLsTradeSail(id);

            CheckThisBar.GetUkmSailParametrs(id);

            CheckThisBar.UpdateCountOut(id);

            CheckSail.CheckAll();
        }

        public static void CheckSailAndPriceUpdate()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT `id` FROM `priority`", conn);

                    cmd.CommandTimeout = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CheckSailAndPriceUpdateStartThread(dr.GetInt32(0));
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor("[CheckSailandPriceUpdate] " + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[CheckSailandPriceUpdate] " + ex.Message);
            }
        }

        public static void CheckSailAndPriceUpdateStartThread(int id)
        {
            Color.WriteLineColor("Запущено обновление информации по штрихкоду " + CheckThisBar.GetBarOnID(id), ConsoleColor.DarkYellow);

            CheckThisBar.UpdatePrice(id);

            CheckThisBar.GetLsTradeSail(id);

            CheckThisBar.GetUkmSailParametrs(id);

            CheckThisBar.UpdateCountOut(id);
        }

        private static void NoSales(int id, string bar, int price, DateTime date)
        {
            Color.WriteLineColor("[" + id + "] Реализация товара: " + bar + " не обнаружена с " + date, ConsoleColor.Blue);

            int TotalDay = Convert.ToInt32((DateTime.Now - date).TotalSeconds) / 86400;
            int ConfigTotalDayNoSales;

            try { ConfigTotalDayNoSales = int.Parse(Config.GetParametr("TotalDayNoSales"));}
            catch { ConfigTotalDayNoSales = 4 ;}

            if (TotalDay > ConfigTotalDayNoSales)
            {
                Color.WriteLineColor("[" + id + "] Товар " + bar + " не продается долгое время.Число дней " + TotalDay, ConsoleColor.Red);

                CheckSail.TasksAdd(id, 1, " " + bar +" Нет продаж.Число дней: [" + TotalDay + "] " , 1);
            }
        }
    }
}
