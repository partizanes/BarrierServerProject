using System;
using System.Data;
using System.Data.OleDb;

namespace BarrierServerProject
{
    class Dbf
    {
        public static Boolean ExecuteNonQuery(string str)
        {
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();

            conn.ConnectionString = "Provider=vfpoledb;Data Source=" + Environment.CurrentDirectory + "\\data\\;Collating Sequence=MACHINE;CODEPAGE=1251";

            cmd.Connection = conn;

            try
            {
                conn.Open();

                cmd.CommandText = str;

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor(ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[DBF] [ExecuteNonQuery]" + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public static OleDbDataReader ExecuteReader(string str)
        {
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();

            conn.ConnectionString = "Provider=vfpoledb;Data Source=" + Environment.CurrentDirectory + "\\data\\;Collating Sequence=MACHINE;CODEPAGE=1251";

            cmd.Connection = conn;

            try
            {
                conn.Open();

                cmd.CommandText = str;

                OleDbDataReader dr;

                dr = cmd.ExecuteReader();

                return dr;
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor(ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[DBF] [ExecuteReader]" + ex.Message);
                return null;
            }
        }
    }
}
