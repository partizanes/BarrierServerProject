using System;
using System.Threading;

namespace BarrierServerProject
{
    class Logo
    {
        public void LogoLoad()
        {
            Console.Title = "Barrier_Server";

            Console.Write("\n");
            Color.WriteLineColor("    Barrier_Server...", ConsoleColor.Green);
            Color.WriteLineColor("            Loading.. ", ConsoleColor.Red);
            Color.WriteLineColor("                      /\\", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("                     /  \\", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("                    /    \\", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("                   /      \\", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("                  /        \\", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("                 /          \\", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("     The idea and execution  \\", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("          by Part!zanes       \\", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("              /                \\", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("              \\                /", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("               \\              /", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("                \\            /", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("                 \\          /", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("                  \\        /", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("                   \\      /", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("                    \\    /", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("                     \\  /", ConsoleColor.Magenta);
            Thread.Sleep(200);
            Color.WriteLineColor("Complete ...          \\/", ConsoleColor.Magenta);
            Color.WriteLineColor("         ", ConsoleColor.Green);
            Console.Write("\n");
        }
    }
}
