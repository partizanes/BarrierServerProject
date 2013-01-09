using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PrioritySales
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
                //TODO check dll in directory;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new AuthForm());
            }
        }
    }
}
