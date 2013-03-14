using System.Windows.Forms;

namespace BalanceModule
{
    class Packages
    {
        public static void parse(string p_id, int com, string msg)
        {
            switch (p_id)
            {
                case "BS":
                    (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.Items.Add(msg); }));
                    break;
                case "SleepTime":
                    Server server = new Server();
                    server.Sender("BalanceModule", 1, "Спящий режим отключен.");
                    (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.Items.Add("Спящий режим отключен!"); }));
                    (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).timer_start_scan.Enabled = false; }));
                    (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).timer_start_scan.Interval = (int.Parse(msg)) * 3600000; }));
                    (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.Items.Add("Установлено время запуска через: " + ((Application.OpenForms[0] as Form1).timer_start_scan.Interval) / 3600000 + " часа"); }));
                    (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).timer_start_scan.Enabled = true; }));
                    server.Sender("BalanceModule", 1, "Спящий режим включен.");
                    server.Sender("BalanceModule", 1, "Деактивация спящего режима через: " + ((Application.OpenForms[0] as Form1).timer_start_scan.Interval) / 3600000 + " часа");
                    break;

            }
        }
    }
}
