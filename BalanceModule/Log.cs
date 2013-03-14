using System;
using System.IO;
using System.Windows.Forms;

namespace BalanceModule
{
    class Log
    {
        public static void log_write(string str, string reason, string logname)
        {
            string EntryTime = DateTime.Now.ToLongTimeString();
            string EntryDate = DateTime.Today.ToShortDateString();
            string fileName = "log/" + logname + ".log";

            check_dir();

            try
            {
                StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.UTF8);
                sw.WriteLine("[" + EntryDate + "][" + EntryTime + "][" + reason + "]" + " " + str);
                sw.Close();
                //check this
                sw.Dispose();
            }
            catch (Exception ex)
            {
                (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.Items.Add(ex.Message); }));
            }
        }

        public static void articul_write(string articul, string logname)
        {
            check_dir();

            string fileName = "log/" + logname + ".log";

            try
            {
                StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.UTF8);
                sw.Write(articul + " ");
                sw.Close();
                //check this
                sw.Dispose();
            }
            catch (System.Exception ex)
            {
                (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.Items.Add(ex.Message); }));
            }
        }

        private static void check_dir()
        {
            try
            {
                if (!Directory.Exists(Environment.CurrentDirectory + "/log/"))
                {
                    Directory.CreateDirectory((Environment.CurrentDirectory + "/log/"));
                }
            }
            catch (System.Exception ex)
            {
                (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.Items.Add(ex.Message); }));
            }
        }
    }
}


