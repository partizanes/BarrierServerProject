using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Serialization;

namespace BarrierServerProject
{
    class Server
    {
        public static bool isServerRunning;
        public static Hashtable clients;
        static Socket listener;
        static int port = 1991;
        static IPEndPoint Point;
        static List<Thread> threads = new List<Thread>();

        public static void ServerStart()
        {
            Thread thd = new Thread(delegate()
            {
                clients = new Hashtable(30);
                isServerRunning = true;
                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Point = new IPEndPoint(IPAddress.Any, port);
                try { listener.Bind(Point); }
                catch (SocketException) {
                    Color.WriteLineColor("Порт " + port + " занят другим приложением!", ConsoleColor.Red);
                    Color.WriteLineColor("Освободите порт " +  port + " и перезапустите приложение: " , ConsoleColor.Yellow);
                    Thread.Sleep(2000);
                    Program.another_run();
                }

                Color.WriteLineColor("Использую порт: " + port, ConsoleColor.Yellow);
                listener.Listen(10);

                SocketAccepter();

            });
            thd.Name = "Слушает порт";
            thd.Start();
            threads.Add(thd);
        }

        private static void SocketAccepter()
        {
            // Запускаем цикл в отдельном потоке, чтобы приложение не зависло
            Thread th = new Thread(delegate()
            {
                while (isServerRunning)
                {
                    Socket client = listener.Accept();

                    clients.Add(client, client.RemoteEndPoint);
                    Color.WriteLineColor("Установлено соединение: " + client.RemoteEndPoint, ConsoleColor.Yellow);

                    Thread thh = new Thread(delegate()
                    {
                        MessageReceiver(client);
                    });
                    thh.Name = "Получение данных";
                    thh.Start();
                }
            });
            th.Name = "Слушает порт";
            th.Start();
            threads.Add(th);
        }

        private static void MessageReceiver(Socket r_client)
        {
                User user = new User();

                while (isServerRunning)
                {
                    try
                    {
                        if (!r_client.Connected)
                            return;

                        user.ipaddress = IPAddress.Parse(((IPEndPoint)r_client.RemoteEndPoint).Address.ToString());
                        user.port = ((IPEndPoint)r_client.RemoteEndPoint).Port;

                        byte[] bytes = new byte[1024];

                        r_client.Receive(bytes);

                        MSG packet = new MSG("0", 0, "null");

                        packet = Util.DeSerialization(bytes);

                        //add debug paramter
                        Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] " + "[DEBUG] " + packet.group + " " + packet.type + " " + packet.message);

                        Packages.parse(packet.group, packet.type, packet.message, user, r_client);
                    }
                    catch (SocketException exc)
                    {
                        if (exc.ErrorCode == 10054)
                        {
                            Connector.ExecuteNonQuery("UPDATE `barrierserver`.`users` SET `status`='0' WHERE `username`='" + user.username + "'");

                            Color.WriteLineColor("Клиент отключился: " + Server.clients[r_client], ConsoleColor.Cyan);

                            if (!r_client.Connected)
                                r_client.Disconnect(true);

                            clients.Remove(r_client);

                            if (!abort_thread(Thread.CurrentThread))
                            {
                                Color.WriteLineColor("Поток не завершен!", ConsoleColor.Red);
                            }

                            break;
                        }
                    }

                    catch(ThreadAbortException) { break; }

                    catch (Exception exc)
                    {
                        Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] " + exc.Message);
                        Thread.Sleep(5000);
                    }
                }
        }

        public static Boolean abort_thread(Thread th)
        {
            try
            {
                th.Abort("Клиент отключился без сигнала завершения");

                th.Join();

                threads.Remove(th);
            }
            catch (ThreadAbortException abortException)
            {
                Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] " + (string)abortException.ExceptionState);
            }
            catch (System.Exception exc)
            {
                Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] " + exc.Message);
                return false;
            }
            Color.WriteLineColor("Поток завершен успешно", ConsoleColor.Green);
            return true;
        }

        private static void check_state()
        {
            int i = 0;

            while (i < threads.Count)
            {
                if (threads[i].ThreadState == ThreadState.Stopped)
                {
                    threads.RemoveAt(i);
                }
                i++;
            }

            return;
        }
        public static void MessageSender(Socket c_client, byte[] bytes)
        {
            try
            {
                // Отправляем пакет
                c_client.Send(bytes);
            }
            catch { }
        }
    }
}
