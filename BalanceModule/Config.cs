using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BalanceModule
{
    class Config
    {    
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        static extern uint GetPrivateProfileString(string lpAppName,string lpKeyName,string lpDefault,StringBuilder lpReturnedString,uint nSize,string lpFileName);


        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName,string lpKeyName, string lpString, string lpFileName);

        public static void Set(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, Environment.CurrentDirectory + "\\config.ini");
        }

        public static string GetParametr(string par)
        {
            StringBuilder buffer = new StringBuilder(50, 50);

            try { GetPrivateProfileString("SETTINGS", par, "null", buffer, 50, Environment.CurrentDirectory + "\\config.ini"); }

            catch (Exception exc) { MessageBox.Show(exc.Message); }

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
                bool st = true;

                Form2 parDialog = new Form2();

                parDialog.label_info.Text = parDialog.label_info.Text +" "+ par;

                parDialog.button1.DialogResult = DialogResult.OK;

                parDialog.buttonQuit.DialogResult = DialogResult.Cancel;

                parDialog.button1.Enabled = true;

                while (st)
                {
                    switch (parDialog.ShowDialog())
                    {
                        case DialogResult.Cancel:
                            System.Diagnostics.Process.GetCurrentProcess().Kill();
                            break;
                        case DialogResult.OK:
                            if (parDialog.TextBox1.Text.Length > 0)
                            {
                                st = false;
                                Set("SETTINGS", par, parDialog.TextBox1.Text);
                                return parDialog.TextBox1.Text;
                            }
                            else
                                MessageBox.Show("Введите данные!");
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.log_write(ex.Message, "Form2", "exception");
            }

            return null;
        }
    }
}
