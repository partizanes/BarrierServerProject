using System;

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
