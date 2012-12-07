using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BarrierServerProject
{
    class Server
    {
        //Cтатус сервера
        static bool isServerRunning;
        // Клиенты
        public static Hashtable clients;
        // Сокет
        static Socket listener;
        // Порт
        static int port = 1991;
        // Точка для прослушки входящих соединений (состоит из адреса и порта)
        static IPEndPoint Point;
        // Список потоков
        static List<Thread> threads = new List<Thread>();


        public static void ServerStart()
        {
            Thread thd = new Thread(delegate()
            {
                clients = new Hashtable(30);
                isServerRunning = true;
                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // Определяем конечную точку, IPAddress.Any означает что наш сервер будет принимать входящие соединения с любых адресов
                Point = new IPEndPoint(IPAddress.Any, port);
                // Связываем сокет с конечной точкой
                listener.Bind(Point);
                // Начинаем слушать входящие соединения
                Color.WriteLineColor("Использую порт: " + port, "Yellow");
                listener.Listen(10);

                SocketAccepter();

            });

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
                    // Создаем новый сокет, по которому мы сможем обращаться клиенту
                    // Этот цикл остановится, пока какой-нибудь клиент не попытается присоединиться к серверу
                    Socket client = listener.Accept();
                    // Теперь, обратившись к объекту client, мы сможем отсылать и принимать пакеты от последнего подключившегося пользователя.
                    // Добавляем подключенного клиента в список всех клиентов, для дальнейшей массовой рассылки пакетов
                    clients.Add(client, client.RemoteEndPoint);
                    Color.WriteLineColor("Клиент добавлен: " + client.RemoteEndPoint, "Yellow");
                    // Начинаем принимать входящие пакеты
                    Thread thh = new Thread(delegate()
                    {
                        MessageReceiver(client);
                    });
                    thh.Start();
                }
            });

            th.Start();
            threads.Add(th);
        }

        private static void MessageReceiver(Socket r_client)
        {
            Thread th = new Thread(delegate()
            {
                while (isServerRunning)
                {
                    try
                    {
                         if (!r_client.Connected)
                            return;

                        byte[] bytes = new byte[1024];

                        r_client.Receive(bytes);

                        if (bytes.Length != 0)
                        {
                            string data = Encoding.UTF8.GetString(bytes);

                            string[] split_data = data.Split(new Char[] { '|' });

                            if (split_data.Length != 2)
                            {
                                Console.WriteLine("Структура неверна!");
                            }
                            else
                            {
                                UInt32 p_id = Convert.ToUInt32(split_data[0].Replace("\0", ""));
                                string com = split_data[1].Replace("\0", "");

                                Packages.parse(p_id, com);
                            }
                        }
                    }
                    catch(SocketException exc)
                    {
                        if (exc.ErrorCode == 10054)
                        {
                            Color.WriteLineColor("Клиент отключился!" + r_client.RemoteEndPoint, "Cyan");

                            if (!r_client.Connected)
                                r_client.Disconnect(true);

                            clients.Remove(r_client);

                            if (!abort_thread(Thread.CurrentThread))
                            {
                                Color.WriteLineColor("Поток не завершен!","Red");
                            }

                            break;
                        }
                    }
                    catch(Exception exc) 
                    { 
                        Console.WriteLine(exc.Message);
                    }
                }
            });
            th.Start();
            threads.Add(th);
        }

        private static Boolean abort_thread(Thread th)
        {
            try
            {
                th.Abort("Клиент отключился без сигнала завершения");

                th.Join();

                threads.Remove(th);
            }
            catch (ThreadAbortException abortException)
            {
                Console.WriteLine((string)abortException.ExceptionState);
            }
            catch (System.Exception exc)
            {
                Console.WriteLine(exc.Message);
                return false;
            }
            Color.WriteLineColor("Поток завершен успешно", "Green");
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
        private static void MessageSender(Socket c_client, byte[] bytes)
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
