using System;
using System.Data.OleDb;

namespace LsTradeAgent
{
    class Packages
    {
        private static Boolean StatusParse = true;

        public static void parse(string p_id, int com, string msg)
        {
            switch (p_id)
            {
                case "LS":
                    switch (com)
                    {
                        case 0:
                            break;
                        case 1:
                            Color.WriteLineColor(msg, ConsoleColor.Cyan);
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                    }
                    break;
                case "DR":
                    switch (com)
                    {
                        case 0:
                            Color.WriteLineColor("Принято: " + msg, ConsoleColor.Green);

                            CheckSailOff(msg);

                            Color.WriteLineColor("Пакет обработан!", ConsoleColor.Green);

                            if (!Dbf.DbfDataReader.IsClosed)
                                Dbf.DbfDataReader.Close();

                            Dbf.DbfDataReader.Dispose();

                            GC.Collect();

                            break;
                    }
                    break;
            }
        }

        private static void CheckSailOff(string msg)
        {
            try
            {
                OleDbDataReader dr = null;

                StatusParse = true;

                string[] split_data = msg.Replace("\0", "").Split(new Char[] { ';' });

                string barcode = split_data[0].Replace(" ", "");

                dr = Dbf.dbf_read("SELECT k_mat,k_op,SUM(n_mat),n_cenu FROM dvmat WHERE k_mat IN(SELECT k_mat FROM sprmat WHERE k_grup  ='" + barcode + "' AND p_time > {^" + split_data[3] + "}) AND k_op IN ('51','53', '61','62','72','93') AND p_time > {^" + split_data[3] + "} GROUP BY k_mat,k_op,n_cenu");

                if (dr == null)
                {
                    Color.WriteLineColor("Отсутствуют партии расхода по баркоду: " + barcode, ConsoleColor.Yellow);
                    return;
                }

                while (dr.Read())
                {
                    Color.WriteLineColor("Код операции: " + dr.GetValue(1) + " Количество: " + dr.GetValue(2) + " Цена: " + dr.GetValue(3), ConsoleColor.Yellow);

                    Server.Sender("LsTradeAgent", 9, barcode + ";" + dr.GetValue(1) + ";" + dr.GetValue(2) + ";" + dr.GetValue(3) + ";" + split_data[3]);

                    System.Threading.Thread.Sleep(1000);
                }

                if (!dr.IsClosed)
                    dr.Close();

                dr.Dispose();
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor(ex.Message, ConsoleColor.Red);
                StatusParse = false;
            }
            finally
            {
                Server.Sender("LsTradeAgent", 8, "Пакет успешно обработан!");

                if (StatusParse && Program.Debug)
                {
                    Color.WriteLineColor("[DEBUG] Пакет успешно обработан!", ConsoleColor.Cyan);
                }

                StatusParse = false;
            }
        }
    }
}
