using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace BarrierServerProject
{
    class Packages
    {
        public static void parse(string p_id, string com, string msg, User user,System.Net.Sockets.Socket r_client)
        {
            switch (p_id)
            {
                case "00":
                    user.username = msg;    //not use
                    break;
                case "01":
                    Msg.SendUser("BalanceModule", "BS 1 Идентификация пройдена.");   //not use
                    break;
                case "02":
                    Console.WriteLine(user.username);   //not use
                    Console.WriteLine(user.ipaddress);  //not use
                    Console.WriteLine(user.port);       //not use
                    break;
                case "CL":
                    user.userid = 0;
                    switch (com)
                    {
                        case "0":
                            string[] split_data = msg.Replace("\0","").Replace(" ", "").Split(new Char[] { ':' });

                            if ((split_data[0].ToString() == "partizanes") && (split_data[1].ToString() == "216567"))
                            {
                                Server.clients[r_client] = split_data[0];
                                Color.WriteLineColor(split_data[0] + " Добавлен!", "Cyan");
                                Msg.SendUser(split_data[0], "CL 1 " + split_data[0]);
                            }
                            else
                            {
                                Server.clients[r_client] = split_data[0];
                                Msg.SendUser(split_data[0], "CL 0 Идентификация не пройдена.");
                                Color.WriteLineColor(split_data[0] +" авторизация неудачна", "Red");
                            }

                            using (MD5 md5Hash = MD5.Create())
                            {
                                Color.WriteLineColor(split_data[0] + " " + GetMd5Hash(md5Hash, split_data[1]), "Red");
                                break;
                            }
                    }
                    break;
                case "BS":
                    switch (com)
                    {
                        case "0":
                            user.userid = 1;
                            Server.clients[r_client] = "BalanceModule";
                            Color.WriteLineColor("Модуль проверки весов загружен!","Cyan");
                            Msg.SendUser("BalanceModule", "BS 1 Идентификация пройдена.");
                                break;
                        case "1":
                            Color.WriteLineColor("BM: " + msg, "Cyan");
                                break;
                        case "9":
                            Color.WriteLineColor("Модуль проверки весов отключен!", "Red");
                            Thread.Sleep(3000);
                            r_client.Disconnect(false);
                            r_client.Close();
                            Server.clients.Remove(r_client);
                                break;
                    }
                        break;
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
