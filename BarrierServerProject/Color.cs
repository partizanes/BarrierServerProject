using System;

namespace BarrierServerProject
{
    class Color
    {
        public static void WriteLineColor(string value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            
            Console.WriteLine(("[" + DateTime.Now.ToLongTimeString() + "] " + value).PadRight(Console.WindowWidth - 1));

            Console.ResetColor();

            value = value.Replace("/", "").Replace("\\", "");
            Packages.connector.ExecuteNonQuery(@"INSERT INTO `log`(`id`,`message`) VALUES ( NULL,'""[" + DateTime.Now.ToLongTimeString() + "] " +value + "');");
        }
    }
}
