using System;
using System.IO;

namespace BarrierServerProject
{
    class Log
    {
        public static void log_write(string str, string reason, string logname)
        {
            string EntryTime = DateTime.Now.ToLongTimeString();
            string EntryDate = DateTime.Today.ToShortDateString();
            string fileName = "log/" + logname + ".log";  //log + data +logname ? 

            if (!Directory.Exists(Environment.CurrentDirectory + "/log/"))
            {
                Directory.CreateDirectory((Environment.CurrentDirectory + "/log/"));
            }

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
                log_write(ex.Message, "Exception", "Exception");
            }
        }

        public static void ExcWrite(string text)
        {
            log_write(text, "EXCEPTION", "exception");
        }

        public static void LogWriteDebug(string text, string reason)
        {
            if (Program.debug)
                log_write(text, "DEBUG", "debug");
        }
    }
}


