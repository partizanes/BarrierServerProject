using System;
using System.Net;
using System.Collections;
using System.Net.Sockets;

namespace BarrierServerProject
{
    class Command
    {
        public static string SwitchCommand(string com)
        {
            switch (com)
            {
                case "version":
                    {
                        return "Alpa 0.4.Created by Part!zanes";
                    }
                case "list":
                    {
                        Console.WriteLine("======================");

                        foreach (DictionaryEntry de in Server.clients)
                        {
                            Color.WriteLineColor(de.Value.ToString(),"Cyan");
                        }

                        Console.WriteLine("======================");

                        return "\nDone.";
                    }
                case "logo":
                    {
                        Logo LogoLoad = new Logo();

                        LogoLoad.LogoLoad();

                        return "";
                    }
                case "author":
                    {
                        return "The idea and execution by Part!zanes";
                    }
                case "quit":
                    {
                        Environment.Exit(0);
                        return "quit";
                    }
                default:
                    {
                        return "Комманда не найдена!";
                    }
            }
        }
    }
}
