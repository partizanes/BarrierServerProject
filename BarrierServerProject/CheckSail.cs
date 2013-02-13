using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Data.OleDb;

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

            if (File.Exists(Environment.CurrentDirectory + "\\data\\" + "balance.dbf"))
            {
                Dbf dbf = new Dbf();

                OleDbDataReader dr = dbf.ExecuteReader("SELECT * FROM balance.dbf");


                if (dr == null)
                    return;


                if (!dr.HasRows)
                    Color.WriteLineColor("База очередности пустая пустая!","Red");

                while (dr.Read())
                {

                }
            }
        }
    }
}
