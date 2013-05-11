using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Serialization;

namespace LsTradeAgent
{
    public static class Server
    {
        //=========================================================================================
        public static Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static IPAddress ip = IPAddress.Parse(Config.GetParametr("IpAuthServer"));
        private static int port = int.Parse(Config.GetParametr("PortAuthServer"));
        public static List<Thread> threads = new List<Thread>();
        //=========================================================================================

        public static void Connect()
        {
            if (server.Connected)
                return;

            Color.WriteLineColor("Соединение...", ConsoleColor.Green);

            try
            {
                server.Connect(ip, port);

                Receiver(server);
            }
            catch (SocketException exc)
            {
                Log.ExcWrite("[Connect][SocketException] " + exc.Message);

                if (exc.ErrorCode == 10061)
                {
                    Color.WriteLineColor("Сервер недоступен.", ConsoleColor.Red);
                    Color.WriteLineColor("Сервер возможно отключен или недоступен в данный момент времени , уточните параметры сервера в настройках и повторите подключение \n Текст исключения: " + exc.Message,  ConsoleColor.Red);
                }

                while (!server.Connected)
                {
                    Color.WriteLineColor("Пробую переподключиться...", ConsoleColor.Green);
                    try { server.Connect(ip, port); }
                    catch { Color.WriteLineColor("Сервер недоступен.",  ConsoleColor.Red); }
                    Thread.Sleep(3000);
                }

                Receiver(server);
            }
            catch (System.Exception exc)
            {
                Color.WriteLineColor("Текст исключения: " + exc.Message, ConsoleColor.Red);
                Log.ExcWrite("[Connect][Exception] " + exc.Message);
            }
            finally
            {
                if (server.Connected)
                {
                    Color.WriteLineColor("Соединение установлено.", ConsoleColor.Green);
                    Sender("LsTradeAgent", 1, "hello!");
                }
            }
        }

        public static void Sender(string group, int type, string msg)
        {
            MSG packet = new MSG(group, type, msg);

            byte[] buf = new byte[1024];

            buf = Util.Serialization(packet);

            server.Send(buf);

            try
            {
                Thread.CurrentThread.Abort();
            }
            catch (ThreadAbortException exc)
            {
                Log.ExcWrite("[Sender][ThreadAbortException] " + exc.Message);
                threads.Remove(Thread.CurrentThread);
                Thread.ResetAbort();
            }
        }

        private static void Receiver(Socket client)
        {
            Thread th = new Thread(delegate()
            {
                while (true)
                {
                    try
                    {
                        byte[] bytes = new byte[1024];

                        client.Receive(bytes);

                        if (bytes.Length != 0)
                        {
                            //Принимаемый пакет разбор структуры

                            MSG packet = new MSG("0", 0, "null");

                            packet = Util.DeSerialization(bytes);

                            Packages.parse(packet.group, packet.type, packet.message);
                        }
                    }
                    catch (SocketException exc)
                    {
                         if (exc.ErrorCode == 10054)
                             Environment.Exit(0);

                         Log.ExcWrite("[Receiver][SocketException] " + exc.Message);
                        //TODO FORM RECCONECT TO SERVER!!!!
                    }
                    catch (Exception exc)
                    {
                        Color.WriteLineColor("Текст исключения: " + exc.Message,  ConsoleColor.Red);
                        Log.ExcWrite("[Receiver][Exception] " + exc.Message);
                    }
                }
            });
            th.Start();
            th.Name = "Слушаю ответ";
            threads.Add(th);
        }
    }
    }
