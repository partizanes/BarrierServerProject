using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LsTradeAgent
{
    class Checker
    {
        public static void CheckCircle()
        {
            while (Server.server.Connected)
            {
                Color.WriteLineColor("Соединение проверено успешно!", ConsoleColor.Green);
                Thread.Sleep(30000);
            }

            Color.WriteLineColor("Соединение отсутсвует!", ConsoleColor.Red);

            Color.WriteLineColor("Попытка переподключения!", ConsoleColor.Red);

            Server.Connect();

            CheckCircle();
        }
    }
}
