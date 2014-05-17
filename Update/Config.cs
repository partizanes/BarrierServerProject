using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Update
{
    class Config
    {
        public static void Set(string section, string key, string value)
        {
            NativeMethods.WritePrivateProfileString(section, key, value, Environment.CurrentDirectory + "\\update.ini");
        }

        public static string GetParametr(string par)
        {
            StringBuilder buffer = new StringBuilder(50, 50);

            NativeMethods.GetPrivateProfileString("SETTINGS", par, "null", buffer, 50, Environment.CurrentDirectory + "\\update.ini");

            if (buffer.ToString() == "null")
            {
                MessageBox.Show("В файле конфигурации не задан параметр " + par);
                Environment.Exit(0);
            }

            return buffer.ToString();
        }
    }

    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetPrivateProfileString(
        string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

    }
}
