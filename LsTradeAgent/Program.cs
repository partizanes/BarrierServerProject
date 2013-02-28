using System;
using System.Threading;
using System.Windows.Forms;

namespace LsTradeAgent
{
    class Program
    {
        public static Boolean Debug = Convert.ToBoolean(Config.GetParametr("Debug"));

        static void Main(string[] args)
        {
            if (System.Diagnostics.Process.GetProcessesByName(Application.ProductName).Length > 1)
            {
                Color.WriteLineColor("\n", ConsoleColor.Red);
                Color.WriteLineColor("Приложение уже запущено!", ConsoleColor.Red);
                Color.WriteLineColor("\n", ConsoleColor.Red);
                Color.WriteLineColor("Завершение работы через 5 секунд..", ConsoleColor.Red);
                Color.WriteLineColor("\n", ConsoleColor.Red);

                Thread.Sleep(5000);

                Environment.Exit(0);
            }
            else
            {
                CheckDll();
            
                Thread th = new Thread(delegate()
                    {
                        Server.Connect();
                    });
                th.Start();
                th.Name = "Соединение";

                Console.CancelKeyPress += new ConsoleCancelEventHandler(AppExit);

                while (true)
                {  
                    string com = Console.ReadLine().ToLower();
                    Color.WriteLineColor(com, ConsoleColor.Yellow);
                }
            }
        }

        private static void AppExit(object sender, ConsoleCancelEventArgs args)
        {
            Server.Sender("LsTradeAgent", 0, "LsTradeAgent:Bye! " + "Причина: " + args.SpecialKey);
        }

        private static void CheckDll()
        {
            while (!System.IO.File.Exists(Environment.CurrentDirectory + "\\" + "Serialization.dll"))
            {
                Color.WriteLineColor("[" + DateTime.Now.ToLongTimeString() + "] " + "В папке с программой отсутствует нужная для работы библиотека Serialization.dll \n Скопируйте в папку с программой библиотеку и нажмите ок!",ConsoleColor.Red);
            }
        }

    }
}
