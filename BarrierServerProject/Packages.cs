using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarrierServerProject
{
    class Packages
    {
        public static void parse(string p_id, string com, string msg, User user)
        {
            switch (p_id)
            {
                case "00":
                    user.username = msg;
                    break;
                case "01":
                    Console.WriteLine("password: " + msg);
                    break;
                case "02":
                    Console.WriteLine(user.username);
                    Console.WriteLine(user.ipaddress);
                    Console.WriteLine(user.port);
                    break;
            }
        }
    }
}
