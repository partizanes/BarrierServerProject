using System;
using System.IO;
using System.Windows.Forms;

namespace Update
{
    class Log
    {

        public static void log_write(string str, string reason)
        {
            string EntryTime = DateTime.Now.ToLongTimeString();
            string EntryDate = DateTime.Today.ToShortDateString();
            string fileName = "log/update.log";

            if (!Directory.Exists(Environment.CurrentDirectory + "/log/"))
            {
                Directory.CreateDirectory((Environment.CurrentDirectory + "/log/"));
            }

            try
            {
                StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.UTF8);
                sw.WriteLine("[" + EntryDate + "][" + EntryTime + "][" + reason + "]" + " " + str);
                sw.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public static void ExcWrite(string text)
        {
            log_write(text, "EXCEPTION");
        }
    }
}
