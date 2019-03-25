using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarrierServerProject
{
    public static class CheckTasks
    {
        public static void StartCheck()
        {
            Color.WriteLineColor("Запуск проверки заданий.", ConsoleColor.Green);
            TasksCheck("1");

            TasksCheck("2");

            GC.Collect();

            System.Threading.Thread.Sleep(1200000);

            StartCheck();

            //10 min 600000
        }
        private static void TasksCheck(string group)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("SELECT COUNT(tasks_id) FROM tasks WHERE `user_group` = " + group + " AND `user_id` = 0", conn);

                    int i = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr == null)
                            return;

                        if (dr.Read())
                            i = dr.GetInt32(0);
                    }

                    if (i > 0)
                        SendNotice(group, 8);
                    else
                        SendNotice(group, 7);
                }
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor("[TasksCheck] " + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[TasksCheck] " + ex.Message);
            }
        }

        private static void SendNotice(string group,int i)
        {
            try
            {
                using (MySqlConnection conn2 = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "", Config.GetParametr("BarrierDataBase"))))
                {
                    conn2.Open();

                    MySqlCommand cmd2 = new MySqlCommand("SELECT username FROM `users` WHERE `online` = 1 AND `group` = " + group, conn2);

                    using (MySqlDataReader usersonline = cmd2.ExecuteReader())
                    {
                        if (!usersonline.HasRows)
                        {
                            if (group == "1")
                                Color.WriteLineColor("Пользователи онлайн не обнаружены..", ConsoleColor.DarkGray);  //TODO Send to administrator msg and tasks ;
                            else
                                Color.WriteLineColor("Администраторы онлайн не обнаружены..", ConsoleColor.DarkGray);
                        }

                        while (usersonline.Read())
                        {
                            Msg.SendUser(usersonline.GetString(0), "PrioritySale", i, "");

                            if (i == 7)
                            {
                                if (group == "1")
                                    Color.WriteLineColor("Отправка уведомления пользователю " + usersonline.GetString(0) + " задания не найдены. ", ConsoleColor.DarkGray);
                                else
                                    Color.WriteLineColor("Отправка уведомления администратору " + usersonline.GetString(0) + " задания не найдены. ", ConsoleColor.DarkGray);
                            }
                            else
                            {
                                if (group == "1")
                                    Color.WriteLineColor("Отправка уведомления пользователю " + usersonline.GetString(0) + " о наличии новых заданий.", ConsoleColor.DarkGray);
                                else
                                    Color.WriteLineColor("Отправка уведомления администратору " + usersonline.GetString(0) + " о наличии новых заданий.", ConsoleColor.DarkGray);
                            }
                        }

                        if (!usersonline.IsClosed)
                        {
                            usersonline.Close();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Color.WriteLineColor("[SendNotice] " + ex.Message, ConsoleColor.Red);
                Log.ExcWrite("[SendNotice] " + ex.Message);
            }
        }
    }
}
