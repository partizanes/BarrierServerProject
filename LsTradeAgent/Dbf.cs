using System;
using System.Data.OleDb;

namespace LsTradeAgent
{
    class Dbf
    {
        static string LsTradeDir = Config.GetParametr("LsTradeDir");
        public static OleDbDataReader DbfDataReader = null;

        public static OleDbDataReader dbf_read(string str)
        {
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();

            Boolean isExecuting = true;

            conn.ConnectionString = "Provider=vfpoledb.1;Data Source=" + LsTradeDir + ";Mode=Read;Collating Sequence=MACHINE;CODEPAGE=866";

            cmd.Connection = conn;

            try
            {
                conn.Open();

                DbfDataReader = null;

                cmd.CommandText = str;

                if (Program.Debug)
                {
                    Color.WriteLineColor("\nДелаю запрос: ", ConsoleColor.Magenta);

                    Color.WriteLineColor("" + str, ConsoleColor.Green);
                }

                DbfDataReader = cmd.ExecuteReader();

                if (!DbfDataReader.HasRows)
                    return null;

                return DbfDataReader;
            }
            catch (System.Exception ex)
            {
                Console.Write("[DBF] [Исключение при попытке чтения:]" + ex.Message);
                isExecuting = false;
                return null;
            }

            finally
            {
                if (isExecuting && Program.Debug)
                    Color.WriteLineColor("[DEBUG] Запрос завершен успешно!", ConsoleColor.Cyan);
            }
        }  
    }
}
