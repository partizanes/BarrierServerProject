using System;
using System.IO;

namespace PrioritySales
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
            }
            catch { }
        }

        public static void ExcWrite(string text)
        {
            log_write(text, "EXCEPTION", "exception");
        }

        public static void LogWriteDebug(string text)
        {
            if (AuthFormClassic.debug)
                log_write(text, "DEBUG", "debug");
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
            }
            catch { }
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
            catch { }
        }
    }
}


