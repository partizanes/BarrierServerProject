using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarrierServerProject
{
    class СheckMovement
    {
        public static void GetMovementInformation()
        {
            try
            {
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
                            System.Threading.Thread.Sleep(200);
                        }

                        Msg.SendUser("LsTradeAgent", "DR", 4, "Движение товара проверка..");
                    }
                }
            }
            catch (Exception ex)
            {
                Color.WriteLineColor("[GetMovementInformation]" + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[GetMovementInformation]" + ex.Message);
            }
        }
    }
}
