using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Data.OleDb;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BarrierServerProject
{
    class Packages
    {
        public static string StatusString = "";
        public static string MainDbName = Config.GetParametr("BarrierDataBase");  //TODO  CHECK CONFIG AND IF MISS WRITE THIS;
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

                            using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=15;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", MainDbName)))
                            {
                                try { conn.Open(); }
                                catch (MySqlException ex)
                                {
                                    Log.LogWriteDebug("[USERAUTH] [" + ex.Number + "] [" + ex.Message + "]");

                                    switch (ex.Number)
                                    {
                                        case 1042:
                                            Server.clients[r_client] = split_data[0];
                                            Msg.SendUser(split_data[0], "PrioritySale", 4, "         Mysql недоступен");
                                            break;
                                        default:
                                            Server.clients[r_client] = split_data[0];
                                            Msg.SendUser(split_data[0], "PrioritySale", 4, "         Код: " + ex.Number);
                                            break;
                                    }
                                }

                                MySqlCommand cmd = new MySqlCommand("SELECT username,hash FROM users WHERE username = '" + split_data[0] + "' AND hash = '" + split_data[1] + "'", conn);

                                using (MySqlDataReader dr = cmd.ExecuteReader())
                                {
                                    if (dr == null)
                                    {
                                        Log.log_write("Запрос вернул null", "Exception", "Exception");
                                        Log.ExcWrite("[AUTH] Запрос вернул null");
                                    }

                                    if (dr.Read())
                                    {
                                        Server.clients[r_client] = split_data[0];
                                        Color.WriteLineColor(split_data[0] + " Добавлен!", ConsoleColor.Cyan);
                                        Msg.SendUser(split_data[0], "PrioritySale", 1, split_data[0]);
                                        user.username = split_data[0];
                                        Packages.connector.ExecuteNonQuery("UPDATE `users` SET `online`='1',`ip`='" + IPAddress.Parse(((IPEndPoint)r_client.RemoteEndPoint).Address.ToString()) + "' WHERE `username`='" + split_data[0] + "'");
                                        Log.log_write(split_data[0], "[AUTH_S]", "AUTHLOG");

                                    }
                                    else
                                    {
                                        Server.clients[r_client] = split_data[0];
                                        Msg.SendUser(split_data[0], "PrioritySale", 0, "Идентификация не пройдена.");
                                        Color.WriteLineColor(split_data[0] + " авторизация неудачна", ConsoleColor.Red);
                                        Log.log_write(split_data[0], "[AUTH_F]", "AUTHLOG");
                                    }

                                    using (MD5 md5Hash = MD5.Create())
                                    {
                                        Color.WriteLineColor(split_data[0] + " " + split_data[1], ConsoleColor.DarkGray);
                                        break;
                                    }

                                }
                            }
                        case 5:
                            Int32 _LastId;

                            if (PriorityAddBarcode(msg, user, out _LastId))
                            {
                                Color.WriteLineColor("Присвоен уникальный номер " + _LastId, ConsoleColor.Green);

                                Thread th = new Thread(delegate()
                                {
                                    Color.WriteLineColor("[Thread] GetSailAndPrice запущен... ", ConsoleColor.DarkYellow);

                                    CheckThisBar.GetSailAndPrice(_LastId);

                                    Color.WriteLineColor("[Thread] GetSailAndPrice завершен.", ConsoleColor.DarkYellow);
                                }); ;
                                th.Name = "GetUkmSailParametrs";
                                th.Start();

                                //TODO SEND ALL USER INFORMATION ABOUT 
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
                            Packages.connector.ExecuteNonQuery("UPDATE `barrierserver`.`users` SET `status`='0' WHERE `username`='" + user.username + "'");
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

            string bar = sd[0].Replace(" ", "");
            string turncount = sd[1].Replace(" ", "");
            string turnprice = sd[2].Replace(" ", "");

            DateTime datetime = new DateTime();

            datetime = Convert.ToDateTime(sd[3]);

            string name = sd[4];

            try
            {
                using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "PrioritySailR", "***REMOVED***", MainDbName)))
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

                Log.log_write("Штрихкод: " + bar + " в количестве: " + turncount + " Отклонён!", "[ADD]", "PRIORITY");

                Msg.SendUser(user.username, "PrioritySale", 3, "                                                                     Отклонено!");
                
                id = 0;
                
                return false;
            }
        }
    }
}
