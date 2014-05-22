using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BarrierServerProject
{
    class СheckMovement
    {
        //Отправка в LsTrade штрихкода и номера из очередности
        public static void GetMovementInformation()
        {
            try
            {
                while (CheckThisBar.busyLsTradeAgent)
                {
                    Color.WriteLineColor("LsTradeAgent занят...", ConsoleColor.Red);
                    Thread.Sleep(1000);
                }

                //Color.WriteLineColor("busyLsTradeAgent = true", ConsoleColor.Cyan);
                CheckThisBar.busyLsTradeAgent = true;

                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT id,bar FROM `priority`", conn);

                    cmd.CommandTimeout = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr == null) { throw new Exception("Что то запрос не удался!"); }

                        if (!dr.HasRows) { throw new Exception("В очередности нет строк для запроса движения."); }

                        while (dr.Read())
                        {
                            object p_id = dr.GetValue(0);
                            object p_bar = dr.GetValue(1);

                            Msg.SendUser("LsTradeAgent", "DR", 3,  p_id + ";" + p_bar);
                            System.Threading.Thread.Sleep(1000);
                        }

                        Msg.SendUser("LsTradeAgent", "DR", 4, "Движение товара проверка...");
                    }
                }
            }
            catch (Exception ex)
            {
                Color.WriteLineColor("[GetMovementInformation]" + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[GetMovementInformation]" + ex.Message);
            }
            finally
            {
                Thread.Sleep(2000);
                //Color.WriteLineColor("busyLsTradeAgent = false", ConsoleColor.Cyan);
                CheckThisBar.busyLsTradeAgent = false;
            }
        }
    }
}
