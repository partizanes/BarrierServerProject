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
        public static Socket client;
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

        public static  void remove_tr()
        {
             threads.Remove(Thread.CurrentThread);
        }

        public void Connect()
        {
            if (!disc_client)
                return;

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

                    if (disc_client)
                        return;

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
            if (!disc_client)
                return;

            bool stat = true;

            try
            {
                if (!disc_client)
                    return;      

                while (client == null || !client.Connected)
                {
                    if (!disc_client)
                    {
                        stat = false;
                        ThreadAbort.th_abort();
                        return;
                    }

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
            if (!disc_client)
                return;

            try
            {
                (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.Items.Add(msg); }));
                (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.SelectionMode = SelectionMode.One; }));
                (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.SetSelected((Application.OpenForms[0] as Form1).listBox1.Items.Count - 1, true); }));
                (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.SetSelected((Application.OpenForms[0] as Form1).listBox1.Items.Count - 1, false); }));
            }
            catch
            {
                try
                {
                    Thread.CurrentThread.Abort();
                }
                catch
                {
                    Thread.CurrentThread.Join();
                    Thread.ResetAbort();
                    threads.Remove(Thread.CurrentThread);
                }
            }
        }

        private static void Receiver()
        {
            Thread th = new Thread(delegate()
            {
                Server server = new Server();

                while (server.disc_client)
                {
                    try
                    {
                        byte[] bytes = new byte[256];

                        client.Receive(bytes);

                        if (bytes.Length != 0)
                        {
                            //Принимаемый пакет разбор структуры

                            string data = Encoding.UTF8.GetString(bytes);

                            string[] split_data = data.Split(new Char[] { '|' });

                            // 10|01 0 11234

                            string str_len = split_data[0];  // длина строки

                            int converted;

                            if (int.TryParse(str_len, out converted))
                            {
                                if (Convert.ToInt32(str_len) != split_data[1].Replace("\0", "").Length)
                                    return;
                            }
                            else
                            {
                                if (converted == 0)
                                    return;

                                (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.Items.Add("Пакет поврежден!"); }));
                                    return;
                            }

                            string com_id = split_data[1].Substring(0, 2);  // id комманды

                            string com_type = split_data[1].Substring(3, 1); // type комманды

                            string msg_data = split_data[1].Substring(5, (Convert.ToInt32(str_len)) - 4); //сообщение

                            //TODO packet parser
                            Packages.parse(com_id, com_type, msg_data);
                        }
                    }

                    catch (Exception exc)
                    {
                        (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.Items.Add(exc.Message); }));
                    }
                }
        });
            th.Start();
            threads.Add(th);
        }
    }
}
