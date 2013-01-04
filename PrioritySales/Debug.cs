﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace PrioritySales
{
    class Debug
    {
        public static void show_msg(string msg)
        {
            switch (AuthForm.log_level)
            {
                case 0:
                    Console.WriteLine(msg);
                    break;
                case 1:
                    Log.log_write(msg, "LEVEL1", "DEBUG");
                    break;
                case 2:
                    MessageBox.Show(msg, "Debug_level_2");
                    break;
                case 3:
                    Log.log_write(msg, "LEVEL3", "DEBUG");
                    MessageBox.Show(msg, "Debug_level_3");
                    break;
                default:             
                    Log.log_write(msg, "LEVEL3", "DEBUG");
                    MessageBox.Show(msg, "Debug_level_3");
                    break;
            }
            
        }
    }
}
