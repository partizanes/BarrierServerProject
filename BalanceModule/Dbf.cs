using System;
using System.Data;
using System.Data.OleDb;

namespace BalanceModule
{
    class Dbf
    {
        public Boolean ExecuteNonQuery(string str)
        {
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();

            conn.ConnectionString = "Provider=vfpoledb;Data Source=" + Environment.CurrentDirectory + ";Collating Sequence=MACHINE;CODEPAGE=866";

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
                Log.log_write(ex.Message, "Exception", "Exception");
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public OleDbDataReader ExecuteReader(string str)
        {
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();

            conn.ConnectionString = "Provider=vfpoledb;Data Source=" + Environment.CurrentDirectory + ";Collating Sequence=MACHINE;CODEPAGE=866";

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
                Log.log_write(ex.Message, "Exception", "Exception");
                return null;
            }
            finally
            {
//                 if (conn.State == ConnectionState.Open)
//                     conn.Close();
            }
            
        }
    }
}
