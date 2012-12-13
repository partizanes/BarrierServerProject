using System;
using System.Collections.Generic;
using System.Linq;
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
                    user.username = msg;
                    break;
                case "01":
                    Console.WriteLine("password: " + msg);
                    break;
                case "02":
                    Console.WriteLine(user.username);
                    Console.WriteLine(user.ipaddress);
                    Console.WriteLine(user.port);
                    break;
                case "BS":
                    switch (com)
                    {
                        case "0":
                            user.userid = 1;
                            Color.WriteLineColor("Модуль проверки весов загружен!","Cyan");
                                break;
                        case "1":
                            Color.WriteLineColor(msg, "Cyan");
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
    }
}
