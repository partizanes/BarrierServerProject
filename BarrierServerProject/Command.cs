using System;
using System.Collections;
using System.Security.Cryptography;

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
                        Console.WriteLine("======================\n", ConsoleColor.Cyan);

                        foreach (DictionaryEntry de in Server.clients)
                        {
                            Console.WriteLine(de.Value.ToString(), ConsoleColor.Cyan);
                        }

                        Console.WriteLine("\n======================", ConsoleColor.Cyan);

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
                            Color.WriteLineColor("Сервер не работает", ConsoleColor.Red);
                            return "";
                        }
                    }
                case "user add":
                    {
                        Color.WriteLineColor("Введите логин пароль", ConsoleColor.Yellow);
                        string line = Console.ReadLine();
                        string[] split_data = line.Replace("\0", "").Split(new Char[] { ' ' });

                        using (MD5 md5Hash = MD5.Create())
                        {
                            string hash = Packages.GetMd5Hash(md5Hash, (Packages.GetMd5Hash(md5Hash, "1?234%5aZ!") + Packages.GetMd5Hash(md5Hash, split_data[1])));

                            if (Packages.connector.ExecuteNonQuery("INSERT INTO `users`(`id`,`username`,`hash`,`group`,`online`,`ip`,`tasks_count`) VALUES ( NULL,'" + split_data[0] + "','" + hash + "','1','0',NULL,0)"))
                                return "Успешно!";
                            else
                                return "Ошибка!";
                        }
                    }
                case "user del":
                    {
                        Color.WriteLineColor("Введите имя пользователя которого хотите удалить...", ConsoleColor.Yellow);

                        string line = Console.ReadLine();

                        if (Packages.connector.ExecuteNonQuery("DELETE FROM users WHERE username ='" + line + "'"))
                            return "Успешно!";
                        else
                            return "Ошибка!";
                    }
                case "quit":
                    {
                        Environment.Exit(0);
                        return "quit";
                    }
                case "bs next start":
                    {
                        int hour;

                        Color.WriteLineColor("Введите через какое время запусить модуль(в часах)", ConsoleColor.Yellow);

                        if (int.TryParse(Console.ReadLine(), out hour))
                        {
                            Msg.SendUser("BalanceModule", "SleepTime", 0, hour.ToString());
                            return "Отправлено!";
                        }
                        else
                        {
                            return "Неверный тип параметра!";
                        }
                    }
                case "help":
                case "command":
                    {
                        Color.WriteLineColor("\nauthor            Выводит информацию об авторе\n", ConsoleColor.Cyan);
                        Color.WriteLineColor("logo              Выводит логотип\n", ConsoleColor.Cyan);
                        Color.WriteLineColor("list              Пользователи онлайн\n", ConsoleColor.Cyan);
                        Color.WriteLineColor("version           Выводит версию\n", ConsoleColor.Cyan);
                        Color.WriteLineColor("quit              Аварийно завершает приложение\n", ConsoleColor.Cyan);
                        Color.WriteLineColor("help              Выводит информацию о коммандах\n", ConsoleColor.Cyan);
                        Color.WriteLineColor("status            Выводит статус сервера\n", ConsoleColor.Cyan);
                        Color.WriteLineColor("command           Выводит информацию о коммандах\n", ConsoleColor.Cyan);
                        Color.WriteLineColor("clr               Очищает окно программы\n", ConsoleColor.Cyan);
                        Color.WriteLineColor("bs next start     Устанавливает время запуска проверки весов в часах. not implement\n", ConsoleColor.Cyan);
                        Color.WriteLineColor("user add          Добавляет пользователя (логин пароль)\n", ConsoleColor.Cyan);
                        Color.WriteLineColor("user del          Удаляет пользователя (логин)\n", ConsoleColor.Cyan);
                        Color.WriteLineColor("uptime            Время работы сервера)\n", ConsoleColor.Cyan);
                        return "done.";

                    }
                case "uptime":
                    return ((DateTime.Now - Program.DateTimeStartPrg).ToString(@"dd\.hh\:mm\:ss"));
                default:
                    {
                        return "Комманда не найдена!";
                    }
            }
        }
    }
}
