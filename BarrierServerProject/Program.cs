using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using ConsoleFunctions;

namespace BarrierServerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            if (System.Diagnostics.Process.GetProcessesByName(Application.ProductName).Length > 1)
            {
                WriteLineColor("\n", "Red");
                WriteLineColor("Приложение уже запущено!", "Red");
                WriteLineColor("\n", "Red");
                WriteLineColor("Запускаю завершение работы...", "Red");
                WriteLineColor("\n", "Red");

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
                Console.Title = "Barrier_Server";

                Console.Write("\n");
                WriteLineColor("    Barrier_Server...", "Green");
                WriteLineColor("            Loading.. ", "Red");
                WriteLineColor("                      /\\", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("                     /  \\", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("                    /    \\", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("                   /      \\", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("                  /        \\", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("                 /          \\", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("     The idea and execution  \\", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("          by Part!zanes       \\", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("              /                \\", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("              \\                /", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("               \\              /", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("                \\            /", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("                 \\          /", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("                  \\        /", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("                   \\      /", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("                    \\    /", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("                     \\  /", "Magenta");
                Thread.Sleep(200);
                WriteLineColor("                      \\/", "Magenta");
                WriteLineColor("        Complete ... ", "Green");
                Console.Write("\n");
            }
            Console.ReadKey();
        }

        static void WriteLineColor(string value, string color)
        {
            if (color == "Red")
                Console.ForegroundColor = ConsoleColor.Red;
            else if (color == "Green")
                Console.ForegroundColor = ConsoleColor.Green;
            else if (color == "Magenta")
                Console.ForegroundColor = ConsoleColor.Magenta;
            else if (color == "Yellow")
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (color == "Cyan")
                Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(value.PadRight(Console.WindowWidth - 1)); // <-- see note

            Console.ResetColor();
        }
    }
}
