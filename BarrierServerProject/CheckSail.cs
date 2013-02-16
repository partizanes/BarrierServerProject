using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Data.OleDb;
using System.Collections;
using System.Net.Sockets;

namespace BarrierServerProject
{
    public static class CheckSail
    {
        public static void StartCheck()
        {
            while(!File.Exists(Environment.CurrentDirectory + "\\data\\" + "balance.dbf"))
            {
                Color.WriteLineColor("Для проверки очередности продаж в папке программы 'data'\n должен находиться файл базы данных balance.dbf.\n\nПовторная проверка наличия файла через 10 секунд!", "REd");
                Thread.Sleep(10000);
            }
            Thread.Sleep(5000);
            CheckInfo();
        }

        public static void CheckInfo()
        {

            if (File.Exists(Environment.CurrentDirectory + "\\data\\" + "balance.dbf"))
            {
                Dbf dbf = new Dbf();

                OleDbDataReader dr = dbf.ExecuteReader("SELECT * FROM balance.dbf");

                if (dr == null)
                    return;

                if (!dr.HasRows)
                    Color.WriteLineColor("База очередности пустая!","Red");

                while (dr.Read())
                {
                    string bar = dr.GetString(0).Replace(" ","");
                    object price = dr.GetValue(1);
                    object count = dr.GetValue(2);
                    DateTime date = Convert.ToDateTime((dr.GetDateTime(3).ToString().Replace(" 0:00:00", "") + " " + dr.GetString(4).Replace(".", ":")));

                    Console.WriteLine(bar + ";" + price + ";" + count + ";" + date);

                    Msg.SendUser("LsTradeAgent", "DR", 0, bar + ";" + price + ";" + count + ";" + date);

                    Thread.Sleep(100);
                }
            }
        }
    }
}
