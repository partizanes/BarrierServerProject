using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ConsoleFunctions;

namespace BarrierServerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;

            while (i <= 100)
            {
                Code.RenderConsoleProgress(i, '\u2592', ConsoleColor.Green, i.ToString()+ "%");
                Thread.Sleep(100);
                i++;
            }
            
            Console.ReadKey();
        }
    }
}
