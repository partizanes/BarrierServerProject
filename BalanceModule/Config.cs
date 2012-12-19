using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BalanceModule
{
    class Config
    {    
        //import dll from use configuration file
        [DllImport("kernel32.dll")]
        static extern uint GetPrivateProfileString(
        string lpAppName,
        string lpKeyName,
        string lpDefault,
        StringBuilder lpReturnedString,
        uint nSize,
        string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName,
           string lpKeyName, string lpString, string lpFileName);

        public static string GetParametr(string par)
        {
            StringBuilder buffer = new StringBuilder(50, 50);

            GetPrivateProfileString("SETTINGS", par, "null", buffer, 50, Environment.CurrentDirectory + "\\config.ini");

            if(buffer.ToString() == "null")
            {
               return ShowMyDialogBox(par);
            }

            return buffer.ToString();
        }

        public static string ShowMyDialogBox(string par)
        {
            try
            {
                Form2 testDialog = new Form2();

                testDialog.label_info.Text = testDialog.label_info.Text +" "+ par;

                testDialog.button1.DialogResult = DialogResult.OK;

                testDialog.button1.Enabled = true;

                if (testDialog.ShowDialog() == DialogResult.OK)
                {
                    if (testDialog.TextBox1.Text.Length == 0)
                    {
                        MessageBox.Show("Введите данные!");
                    }
                    else
                        return testDialog.TextBox1.Text;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return "null";
        }
    }
}
