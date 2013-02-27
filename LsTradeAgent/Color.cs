using System;

namespace LsTradeAgent
{
    class Color
    {
        public static void WriteLineColor(string value, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            Console.WriteLine(("[" + DateTime.Now.ToLongTimeString() + "] " + value).PadRight(Console.WindowWidth - 1)); // <-- see note

            Console.ResetColor();
        }
    }
}
