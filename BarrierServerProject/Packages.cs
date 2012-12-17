using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
                    Msg.SendUser("BalanceModule", "Идентификация пройдена.");   //not use
                    break;
                case "02":
                    Console.WriteLine(user.username);   //not use
                    Console.WriteLine(user.ipaddress);  //not use
                    Console.WriteLine(user.port);       //not use
                    break;
                case "BS":
                    switch (com)
                    {
                        case "0":
                            user.userid = 1;
                            Server.clients[r_client] = "BalanceModule";
                            Color.WriteLineColor("Модуль проверки весов загружен!","Cyan");
                            Msg.SendUser("ModuleBalance", "Идентификация пройдена.");
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
