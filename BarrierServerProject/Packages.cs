using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using MySql.Data.MySqlClient;
using DicEnt = System.Collections.DictionaryEntry;

namespace BarrierServerProject
{
    class Packages
    {
        public static string StatusString = "";
        public static Connector connector = new Connector();

        public static void parse(string p_id, int com, string msg, User user,System.Net.Sockets.Socket r_client)
        {
            switch (p_id)
            {
                case "LsTradeAgent":
                    switch (com)
                    {
                        case 0:
                            Color.WriteLineColor(msg, ConsoleColor.Blue);
                            Color.WriteLineColor("Модуль связи с LsTrade отключен!", ConsoleColor.Red);
                            Thread.Sleep(3000);
                            r_client.Disconnect(false);
                            r_client.Close();
                            Server.clients.Remove(r_client);
                            break;
                        case 1:
                            user.userid = 2;
                            Server.clients[r_client] = p_id;
                            Color.WriteLineColor("Модуль связи с LsTrade загружен!",ConsoleColor.Cyan);
                            Msg.SendUser("LsTradeAgent", "LS", 1, "Идентификация пройдена.");
                            break;
                    }
                    break;
                case "PrioritySale":

                    user.userid = 0;

                    switch (com)
                    {
                        case 0:
                            string[] split_data = msg.Replace("\0", "").Replace(" ", "").Split(new Char[] { ':' });

                            var username = split_data[0];
                            var hash = split_data[1];

                            using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                            using (MySqlCommand cmd = new MySqlCommand("SELECT username,hash FROM users WHERE username = '" + username + "' AND hash = '" + hash + "'", conn))
                            using (MySqlDataReader dr = cmd.ExecuteReader())
                            {
                                try { conn.Open(); }
                                catch (MySqlException ex)
                                {
                                    Log.LogWriteDebug("[USERAUTH] [" + ex.Number + "] [" + ex.Message + "]");

                                    switch (ex.Number)
                                    {
                                        case 1042:
                                            Server.clients[r_client] = username;
                                            Msg.SendUser(username, "PrioritySale", 4, "         Mysql недоступен");
                                            break;
                                        default:
                                            Server.clients[r_client] = username;
                                            Msg.SendUser(username, "PrioritySale", 4, "         Код: " + ex.Number);
                                            break;
                                    }
                                }

                                if (dr == null)
                                {
                                    Log.Write("Запрос вернул null", "Exception", "Exception");
                                    Log.ExcWrite("[AUTH] Запрос вернул null");
                                    return;
                                }

                                if (dr.Read())
                                {
                                    Server.clients[r_client] = username;
                                    Color.WriteLineColor(username + " Добавлен!", ConsoleColor.Cyan);
                                    Msg.SendUser(username, "PrioritySale", 1, username);
                                    user.username = username;
                                    Packages.connector.ExecuteNonQuery("UPDATE `users` SET `online`='1',`ip`='" + IPAddress.Parse(((IPEndPoint)r_client.RemoteEndPoint).Address.ToString()) + "' WHERE `username`='" + username + "'");
                                    Log.Write(username, "[AUTH_S]", "AUTHLOG");

                                }
                                else
                                {
                                    Server.clients[r_client] = username;
                                    Msg.SendUser(username, "PrioritySale", 0, "Идентификация не пройдена.");
                                    Color.WriteLineColor(username + " авторизация неудачна", ConsoleColor.Red);
                                    Log.Write(username, "[AUTH_F]", "AUTHLOG");
                                }

                                Color.WriteLineColor(username + " " + hash, ConsoleColor.DarkGray);
                                break;
                            }
                        case 4:
                        case 5:
                            Int32 _LastId;

                            if (PriorityAddBarcode(msg, user, out _LastId))
                            {
                                Color.WriteLineColor("Присвоен уникальный номер " + _LastId, ConsoleColor.Green);

                                Thread th = new Thread(delegate() {
                                    CheckThisBar.GetSailAndPrice(_LastId);
                                }); ;
                                th.Name = "GetUkmSailParametrs";
                                th.Start();
                            }
                            break;
                        case 6:
                            Color.WriteLineColor("[" + user.username + "] "  + msg, ConsoleColor.DarkGreen);

                            if(msg.Contains("отказался"))
                                Log.Write("[" + user.username + "] " + msg, "[DECLINE]", "Decline");
                            else if (msg.Contains("выполнил"))
                                CheckStatusUpdate(msg);

                            foreach (DicEnt de in Server.clients)
                            {
                                Msg.SendUser((de.Value).ToString(), "PrioritySale", 9, Packages.StatusString + ";" + DateTime.Now);
                            }
                            break;
                        case 7:
                            Color.WriteLineColor("Есть новые сообщение на доске объявлений.Отправка уведомления...", ConsoleColor.Yellow);

                            foreach (DicEnt de in Server.clients)
                            {
                                Msg.SendUser((de.Value).ToString(), "PrioritySale", 6, "");
                            }
                            break;
                        case 8:
                            Color.WriteLineColor("Версия очередности обновлена у клиента " + user.username, ConsoleColor.Yellow);
                            break;
                        case 9:
                            if (msg == StatusString)
                                Color.WriteLineColor("Версия очередности у клиента " + user.username + " проверена успешно!", ConsoleColor.Green);
                            else
                            {
                                Color.WriteLineColor("Версия очередности устарела у клиента " + user.username + ".", ConsoleColor.DarkYellow);
                                Msg.SendUser(user.username, "PrioritySale", 9, Packages.StatusString + ";" + DateTime.Now);
                            }
                            break;
                    }
                    break;
                case "BalanceModule":
                    switch (com)
                    {
                        case 0:
                            user.userid = 1;
                            Server.clients[r_client] = "BalanceModule";
                            Color.WriteLineColor("Модуль проверки весов загружен!",ConsoleColor.Cyan);
                            Msg.SendUser("BalanceModule", "BS", 1, "Идентификация пройдена.");
                                break;
                        case 1:
                            Color.WriteLineColor(msg, ConsoleColor.Cyan);
                                break;
                        case 2:
                                user.userid = 1;
                                Server.clients[r_client] = "BalanceModule";
                                break;
                        case 9:
                            Color.WriteLineColor("Модуль проверки весов отключен!", ConsoleColor.Red);
                            Thread.Sleep(3000);
                            r_client.Disconnect(false);
                            r_client.Close();
                            Server.clients.Remove(r_client);
                                break;
                    }
                    break;
                case "User":
                    switch (com)
                    {
                        case 0000:
                            Color.WriteLineColor(Server.clients[r_client] + ": Завершение сеанса.", ConsoleColor.Red);
                            Packages.connector.ExecuteNonQuery("UPDATE `barrierserver`.`users` SET `online`='0' WHERE `username`='" + user.username + "'");
                            Thread.Sleep(3000);
                            r_client.Disconnect(false);
                            r_client.Close();
                            Server.clients.Remove(r_client);
                                break;
                    }
                        break;
            }
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private static bool PriorityAddBarcode(string msg, User user, out Int32 id)
        {
            string[] sd = msg.Replace("\0", "").Split(new Char[] { ';' });

            var bar = sd[0].Replace(" ", "");
            var turncount = sd[1].Replace(" ", "");
            var turnprice = sd[2].Replace(" ", "");

            var datetime = new DateTime();

            datetime = Convert.ToDateTime(sd[3]);

            var name = sd[4];

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"INSERT INTO `priority`(`id`,`bar`,`name`,`turn_price`,`count`,`sailed`,`status`,`status_text`,`current_price_ukm`,`date`) VALUES ( 
                                NULL,'" + bar + "','" + name + "','" + turnprice + "','" + turncount.Replace(",", ".") + "','0','0','','0','" + datetime.ToString("yyyy-MM-dd,HH:mm:ss") + "');SELECT LAST_INSERT_ID();", conn);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            id = dr.GetInt32(0);
                            
                            Color.WriteLineColor("Штрихкод: " + bar + " в количестве: " + turncount + " поставлен в очередь.", ConsoleColor.Green); 
       
                            Msg.SendUser(user.username, "PrioritySale", 2, "                Штрихкод: " + bar + " в количестве: " + turncount + " поставлен в очередь.");
                            return true;
                        }
                        else
                            throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                Color.WriteLineColor("[PriorityAddBarcode] " + ex.Message, ConsoleColor.Red);

                Color.WriteLineColor("Штрихкод: " + bar + " в количестве: " + turncount + " Отклонён!", ConsoleColor.Red);

                Log.Write("Штрихкод: " + bar + " в количестве: " + turncount + " Отклонён!", "[ADD]", "PRIORITY");

                Msg.SendUser(user.username, "PrioritySale", 3, "                                                                     Отклонено!");
                
                id = 0;
                
                return false;
            }
        }

        private static void CheckStatusUpdate(string msg)
        {
            string taskId = msg.Replace("Пользователь выполнил задачу ", "");

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT `priority_id`,MAX(`priority`) FROM `tasks` WHERE `priority_id` = 
                                                            (SELECT `priority_id` FROM `tasks` WHERE `tasks_id` = " + taskId + 
                                                            ") AND `inactive` = 0 GROUP BY `priority_id`", conn);

                    cmd.CommandTimeout = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr == null) { throw new Exception("Что то запрос не удался!"); }

                        if (!dr.HasRows) {
                            string uquery = @"UPDATE `priority` SET `status` = 0 WHERE `id` = (SELECT `priority_id` FROM `tasks` WHERE `tasks_id` = " + taskId + ")";

                            Packages.connector.ExecuteNonQuery(uquery);
                        }

                        while (dr.Read())
                        {
                            string uquery = @"UPDATE `priority` SET `status` = " + dr.GetValue(1) + " WHERE `id` = " + dr.GetValue(0);

                            Packages.connector.ExecuteNonQuery(uquery);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Color.WriteLineColor("[CheckStatusUpdate]" + exc.Message, ConsoleColor.Red);
                Log.ExcWrite("[CheckStatusUpdate]" + exc.Message);
            }
        }
    }
}
