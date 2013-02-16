using System;

namespace LsTradeAgent
{
    class Color
    {
        public static void WriteLineColor(string value, string color)
        {
            switch (color)
            {
                case "Red":
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    }
                case "DarkBlue":
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                    }
                case "Green":
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    }
                case "Magenta":
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    }
                case "Yellow":
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    }
                case "Cyan":
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    }
                default:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    }
            }

            Console.WriteLine(("[" + DateTime.Now.ToLongTimeString() + "] " + value).PadRight(Console.WindowWidth - 1)); // <-- see note

            Console.ResetColor();
        }
    }
}
