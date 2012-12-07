using System;
using System.Threading;
using System.Windows.Forms;
using ConsoleFunctions;

namespace BarrierServerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            if (System.Diagnostics.Process.GetProcessesByName(Application.ProductName).Length > 1)
            {
               Color.WriteLineColor("\n", "Red");
               Color.WriteLineColor("Приложение уже запущено!", "Red");
               Color.WriteLineColor("\n", "Red");
               Color.WriteLineColor("Запускаю завершение работы...", "Red");
               Color.WriteLineColor("\n", "Red");

                int i = 10;

                while (i != 0)
                {
                    Code.RenderConsoleProgress(i * 10, '\u2592', ConsoleColor.Red, "Завершение работы сервера через [" + i.ToString() + " second]");
                    Thread.Sleep(1000);
                    i--;
                }
                Environment.Exit(0);
            }
            else
            {
                Logo LogoLoad = new Logo();

                LogoLoad.LogoLoad();

                Server.ServerStart();
            }

            Thread.Sleep(1000);

            while (true)
            {
                Color.WriteLineColor("\nВведите комманду:", "Green");

                string com = Console.ReadLine().ToLower();

                Color.WriteLineColor(Command.SwitchCommand(com),"Yellow");
            }
        }
    }
}
