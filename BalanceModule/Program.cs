using System;
using System.Windows.Forms;

namespace BalanceModule
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (System.Diagnostics.Process.GetProcessesByName(Application.ProductName).Length > 1)
            {
                MessageBox.Show("Приложение уже запущено!");
                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }

        }
    }
}
