using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

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
                            break;
                    }
                    break;
            }
        }

        private static void CheckSailOff(string msg)
        {
            try
            {
                StatusParse = true;

                string[] split_data = msg.Replace("\0", "").Split(new Char[] { ';' });

                string barcode = split_data[0].Replace(" ", "");

                OleDbDataReader dr = Dbf.dbf_read("SELECT k_mat FROM sprmat WHERE k_grup ='" + barcode + "' AND p_time > {^" + split_data[3] + "}");

                if (dr == null)
                {
                    Color.WriteLineColor("Новых партий нет по штрихкоду " + barcode, ConsoleColor.Yellow);
                    return;
                }

                while (dr.Read())
                {
                    Color.WriteLineColor("Найдены партии " + dr.GetString(0), ConsoleColor.Yellow);

                    OleDbDataReader datareader = Dbf.dbf_read("SELECT n_mat FROM dvmat WHERE k_mat IN ('" + dr.GetString(0).Replace(" ", "") + "') AND k_op IN ('53', '61','62','72','93')");

                    if (datareader == null)
                    {
                        Color.WriteLineColor("Списаний нету по партии : " + dr.GetString(0).Replace(" ", ""), ConsoleColor.DarkRed);
                        continue;
                    }

                    while (datareader.Read())
                    {
                        Color.WriteLineColor("Списано по партии: " + dr.GetString(0).Replace(" ", "") + "Количество: " + datareader.GetString(0), ConsoleColor.Green);
                    }
                }
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
