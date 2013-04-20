using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Update
{
    class Config
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint GetPrivateProfileString(
        string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        public static void Set(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, Environment.CurrentDirectory + "\\update.ini");
        }

        public static string GetParametr(string par)
        {
            StringBuilder buffer = new StringBuilder(50, 50);

            GetPrivateProfileString("SETTINGS", par, "null", buffer, 50, Environment.CurrentDirectory + "\\update.ini");

            if (buffer.ToString() == "null")
            {
                MessageBox.Show("В файле конфигурации не задан параметр " + par);
                Environment.Exit(0);
            }

            return buffer.ToString();
        }
    }
}
