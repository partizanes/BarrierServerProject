using System;
using System.IO;

namespace BarrierServerProject
{
    class Log
    {
        public static void Write(string str, string reason, string logname)
        {
            string EntryTime = DateTime.Now.ToLongTimeString();
            string EntryDate = DateTime.Today.ToShortDateString();
            string fileName = "log/" + logname + ".log";  //log + data +logname ? 

            if (!Directory.Exists(Environment.CurrentDirectory + "/log/"))
                Directory.CreateDirectory((Environment.CurrentDirectory + "/log/"));

            try
            {
                StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.UTF8);
                sw.WriteLine("[" + EntryDate + "][" + EntryTime + "][" + reason + "]" + " " + str);
                sw.Close();
                //check this
                sw.Dispose();
            }
            catch (Exception ex) { Write(ex.Message, "Exception", "Exception"); }
        }

        public static void ExcWrite(string text)
        {
            Write(text, "EXCEPTION", "exception");
        }

        public static void LogWriteDebug(string text)
        {
            if (Program.debug)
                Write(text, "DEBUG", "debug");
        }
    }
}


