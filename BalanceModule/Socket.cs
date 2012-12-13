using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BalanceModule
{
    class Server
    {
        //=========================================================================================
        private static Socket client;
        private static IPAddress ip = IPAddress.Parse("127.0.0.1"); //TODO config!!!
        private const int port = 1991;
        private static List<Thread> threads = new List<Thread>();
        public  bool disc_client = true;
        //=========================================================================================
        public void Disconnect()
        {
            client.Blocking = false;
            client.Close();
        }

        public void Connect()
        {
            listbox_msg("Соединение...");

            try
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(ip, port);
                Receiver();
            }
            catch (SocketException se)
            {
                if (se.ErrorCode == 10061)
                {
                    listbox_msg("Подключение к серверу не удалось.Пробуем переподключиться... ");

                    Thread.Sleep(5000);

                    if (!client.Connected)
                        Connect();
                    else
                    {
                        listbox_msg("Подключение создано. ");
                    }  
                }
            }
            catch (Exception exc)
            {
                listbox_msg(exc.Message);
            }
        }

        public void Sender(string msg)
        {
            bool stat = true;

            try
            {
                if (!disc_client)
                    return;      

                while (client == null || !client.Connected)
                {
                    listbox_msg("Соединение отсутствует. ");
                    Connect();
                    Thread.Sleep(2000);
                }

                int size = msg.Length;

                string send_packet = size + "|" + msg;

                byte[] bytes = new byte[Encoding.UTF8.GetBytes(send_packet).Length];

                bytes = Encoding.UTF8.GetBytes(send_packet);

                client.Send(bytes);
            }
            catch (SocketException se)
            {
                stat = false;

                if (se.ErrorCode == 10054)
                {
                    listbox_msg("Соединение потеряно.Пробуем переподключиться... ");

                    Thread.Sleep(3000);

                    Sender(msg);
                }
            }
            catch (Exception exc)
            {
                stat = false;

                listbox_msg(exc.Message);
            }
            finally
            {
                if (stat)
                    listbox_msg("Пакет отправлен!");
            }
        }

        private void listbox_msg(string msg)
        {
            try { (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.Items.Add(msg); })); }
            catch { }
        }

        private static void Receiver()
        {
//             Thread th = new Thread(delegate()
//             {
//                 while (client_running)
//                 {
//                     try
//                     {
//                         byte[] bytes = new byte[1024];
//                         // Принимает данные от сервера в формате "X|Y"
//                         client.Receive(bytes);
//                         if (bytes.Length != 0)
//                         {
//                             string data = Encoding.UTF8.GetString(bytes);
//                             string[] split_data = data.Split(new Char[] { '|' });
//                             Console.WriteLine(data.Replace("\0", ""));
//                         }
//                     }
//                     catch (Exception exc)
//                     {
//                         (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.Items.Add(exc.Message); }));
//                     }
//                 }
//             });
//             th.Start();
//             threads.Add(th);
        }
    }
}
