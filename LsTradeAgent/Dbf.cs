using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace LsTradeAgent
{
    class Dbf
    {

        static string LsTradeDir = Config.GetParametr("LsTradeDir");

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

                OleDbDataReader dr = null;

                cmd.CommandText = str;

                //Color.WriteLineColor("\nДелаю запрос: ", ConsoleColor.Magenta);
                //Color.WriteLineColor("" + str, ConsoleColor.Green);

                dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    return null;
                }

                return dr;
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                isExecuting = false;
                return null;
            }
            finally
            {
               // conn.Close();

                if (isExecuting)
                {
                    Color.WriteLineColor("Запрос завершен успешно!", ConsoleColor.Cyan);
                }
            }
        }
    }
}
