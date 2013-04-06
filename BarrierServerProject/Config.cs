using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace BarrierServerProject
{
    class Config
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        public static string ConfigTarget = Environment.CurrentDirectory + "\\config.ini";

        public static void Set(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, ConfigTarget);
        }

        public static string GetParametr(string par)
        {
            StringBuilder buffer = new StringBuilder(50, 50);

            GetPrivateProfileString("SETTINGS", par, "null", buffer, 50, ConfigTarget);


            if (buffer.ToString() == "null")
            {
                Color.WriteLineColor("Не заданы параметры подключения сервера. ", ConsoleColor.Red);
                Color.WriteLineColor("Отсутствует параметр " + par, ConsoleColor.Red);
                Color.WriteLineColor("Установите параметры и запустите приложение снова.", ConsoleColor.Red);
                Color.WriteLineColor("Завершение работы через 10 секунд.\n", ConsoleColor.Yellow);
                Thread.Sleep(10000);
                Environment.Exit(0);
            }

            return buffer.ToString();
        }
    }
}
