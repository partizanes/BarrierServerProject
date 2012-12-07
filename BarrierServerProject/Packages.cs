using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarrierServerProject
{
    class Packages
    {
        public static void parse(uint p_id,string com)
        {
            switch (p_id)
            {
                case 0:
                    Console.WriteLine("username: " + com);
                    break;
                case 1:
                    Console.WriteLine("password: " + com);
                    break;
                case 2:
                    Console.WriteLine(p_id + " | " + com);
                    break;
            }
        }
    }
}
