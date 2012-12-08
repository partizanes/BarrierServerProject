using System;
using System.Collections;

namespace BarrierServerProject
{
    class Command
    {
        public static string SwitchCommand(string com)
        {
            switch (com)
            {
                case "author":
                    {
                        return "The idea and execution by Part!zanes";
                    }
                case "version":
                    {
                        return "Alpa 0.4";
                    }
                case "list":
                    {
                        Color.WriteLineColor("======================\n","Cyan");

                        foreach (DictionaryEntry de in Server.clients)
                        {
                            Color.WriteLineColor(de.Value.ToString(),"Cyan");
                        }

                        Color.WriteLineColor("\n======================","Cyan");

                        return "\nDone.";
                    }
                case "clr":
                case "clear":
                    {
                        Console.Clear();
                        return "done.";
                    }
                case "logo":
                    {
                        Logo LogoLoad = new Logo();

                        LogoLoad.LogoLoad();

                        return "";
                    }
                case "status":
                    {
                        if (Server.isServerRunning)
                            return "Сервер работает!";
                        else
                        {
                            Color.WriteLineColor("Сервер не работает", "Red");
                            return "";
                        }
                    }
                case "quit":
                    {
                        Environment.Exit(0);
                        return "quit";
                    }
                case "help":
                case "command":
                    {
                        Color.WriteLineColor("\nauthor  Выводит информацию об авторе\n", "Cyan");
                        Color.WriteLineColor("logo    Выводит логотип\n", "Cyan");
                        Color.WriteLineColor("list    Пользователи онлайн\n", "Cyan");
                        Color.WriteLineColor("version Выводит версию\n", "Cyan");
                        Color.WriteLineColor("quit    Аварийно завершает приложение\n", "Cyan");
                        Color.WriteLineColor("help    Выводит информацию о коммандах\n", "Cyan");
                        Color.WriteLineColor("status  Выводит статус сервера\n", "Cyan");
                        Color.WriteLineColor("command Выводит информацию о коммандах\n", "Cyan");
                        Color.WriteLineColor("clr     Очищает окно программы\n", "Cyan");
                        return "done.";

                    }
                default:
                    {
                        return "Комманда не найдена!";
                    }
            }
        }
    }
}
