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
            Color.WriteLineColor("    Barrier_Server...", "Green");
            Color.WriteLineColor("            Loading.. ", "Red");
            Color.WriteLineColor("                      /\\", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("                     /  \\", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("                    /    \\", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("                   /      \\", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("                  /        \\", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("                 /          \\", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("     The idea and execution  \\", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("          by Part!zanes       \\", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("              /                \\", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("              \\                /", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("               \\              /", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("                \\            /", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("                 \\          /", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("                  \\        /", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("                   \\      /", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("                    \\    /", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("                     \\  /", "Magenta");
            Thread.Sleep(200);
            Color.WriteLineColor("Complete ...          \\/", "Magenta");
            Color.WriteLineColor("         ", "Green");
            Console.Write("\n");
        }
    }
}
