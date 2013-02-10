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

namespace BarrierServerProject
{
    class Packages
    {
        public static void parse(string p_id, int com, string msg, User user,System.Net.Sockets.Socket r_client)
        {
            switch (p_id)
            {
                case "PrioritySale":

                    user.userid = 0;

                    switch (com)
                    {
                        case 0:
                            string[] split_data = msg.Replace("\0", "").Replace(" ", "").Split(new Char[] { ':' });

                            Dbf dbf = new Dbf();

                            OleDbDataReader dr;

                            dr = dbf.ExecuteReader("SELECT name,hash FROM users.dbf WHERE name = '" + split_data[0] + "' AND hash = '" + split_data[1] + "'");

                            if (dr == null)
                                Log.log_write("Запрос вернул null", "Exception", "Exception");

                            if (dr.Read())
                            {
                                Server.clients[r_client] = split_data[0];
                                Color.WriteLineColor(split_data[0] + " Добавлен!", "Cyan");
                                Msg.SendUser(split_data[0], "PrioritySale", 1, split_data[0]);
                                user.username = split_data[0];
                            }
                            else
                            {
                                Server.clients[r_client] = split_data[0];
                                Msg.SendUser(split_data[0], "PrioritySale", 0, "Идентификация не пройдена.");
                                Color.WriteLineColor(split_data[0] +" авторизация неудачна", "Red");
                            }

                            using (MD5 md5Hash = MD5.Create())
                            {
                                Color.WriteLineColor(split_data[0] + " " + split_data[1], "Red");
                                break;
                            }

                        case 5:
                            string[] sd = msg.Replace("\0", "").Replace(" ", "").Split(new Char[] { ':' });

                            string bar = sd[0];
                            string count = sd[1];
                            string price = sd[2];

                            DateTime datetime = new DateTime();

                            datetime = Convert.ToDateTime(sd[3]);

                            if (BalanceAddItem("INSERT INTO balance.dbf (barcode,price,count,date) VALUES ('" + bar + "'," + price + "," + count + ",{^" + datetime.ToString("yyyy-MM-dd") + "})"))
                                Msg.SendUser(user.username, "PrioritySale", 2, "                Штрихкод: " + bar + " в количестве: " + count + " поставлен в очередь.");
                            else
                                Msg.SendUser(user.username, "PrioritySale", 3, "                                                                      Отклонено!");

                            break;
                    }
                    break;
                case "BalanceModule":
                    switch (com)
                    {
                        case 0:
                            user.userid = 1;
                            Server.clients[r_client] = "BalanceModule";
                            Color.WriteLineColor("Модуль проверки весов загружен!","Cyan");
                            Msg.SendUser("BalanceModule", "BS", 1, "Идентификация пройдена.");
                                break;
                        case 1:
                            Color.WriteLineColor(msg, "Cyan");
                                break;
                        case 2:
                                user.userid = 1;
                                Server.clients[r_client] = "BalanceModule";
                                break;
                        case 9:
                            Color.WriteLineColor("Модуль проверки весов отключен!", "Red");
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
                            Color.WriteLineColor(Server.clients[r_client] + ": Завершение сеанса.", "Red");
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

        public static bool BalanceAddItem(string str)
        {
            Dbf dbf = new Dbf();

            try
            {
                dbf.ExecuteNonQuery(str);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }
    }
}
