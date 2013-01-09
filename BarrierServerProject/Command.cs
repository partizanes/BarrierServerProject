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
                        Console.WriteLine("======================\n","Cyan");

                        foreach (DictionaryEntry de in Server.clients)
                        {
                            Console.WriteLine(de.Value.ToString(), "Cyan");
                        }

                        Console.WriteLine("\n======================", "Cyan");

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
                case "user add":
                    {
                        Color.WriteLineColor("Введите логин пароль", "Yellow");
                        string line = Console.ReadLine();
                        string[] split_data = line.Replace("\0", "").Split(new Char[] { ' ' });
                        Dbf dbf = new Dbf();
                        using (MD5 md5Hash = MD5.Create())
                        {
                            string hash = Packages.GetMd5Hash(md5Hash, (Packages.GetMd5Hash(md5Hash, "1?234%5aZ!") + Packages.GetMd5Hash(md5Hash, split_data[1])));
                            if (dbf.ExecuteNonQuery("INSERT INTO users.dbf (name,hash) VALUES ('" + split_data[0] + "','" + hash + "')"))
                                return "Успешно!";
                            else
                                return "Ошибка!";
                        }
                    }
                case "user del":
                    {
                        Color.WriteLineColor("Введите имя пользователя которого хотите удалить...", "Yellow");

                        Dbf dbf = new Dbf();

                        string line = Console.ReadLine();

                        if (dbf.ExecuteNonQuery("DELETE FROM users.dbf WHERE NAME ='" + line + "'" ))
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

                        Color.WriteLineColor("Введите через какое время запусить модуль(в часах)", "Yellow");

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
                        Color.WriteLineColor("\nauthor            Выводит информацию об авторе\n", "Cyan");
                        Color.WriteLineColor("logo              Выводит логотип\n", "Cyan");
                        Color.WriteLineColor("list              Пользователи онлайн\n", "Cyan");
                        Color.WriteLineColor("version           Выводит версию\n", "Cyan");
                        Color.WriteLineColor("quit              Аварийно завершает приложение\n", "Cyan");
                        Color.WriteLineColor("help              Выводит информацию о коммандах\n", "Cyan");
                        Color.WriteLineColor("status            Выводит статус сервера\n", "Cyan");
                        Color.WriteLineColor("command           Выводит информацию о коммандах\n", "Cyan");
                        Color.WriteLineColor("clr               Очищает окно программы\n", "Cyan");
                        Color.WriteLineColor("bs next start     Устанавливает время запуска проверки весов в часах\n", "Cyan");
                        Color.WriteLineColor("user add          Добавляет пользователя (логин пароль)\n", "Cyan");
                        Color.WriteLineColor("user delete       Удаляет пользователя (логин)\n", "Cyan");
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
