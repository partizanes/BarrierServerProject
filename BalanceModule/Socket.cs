using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Serialization;

namespace BalanceModule
{
    class Server
    {
        //=========================================================================================
        public static Socket client;
        private static IPAddress ip = IPAddress.Parse(Config.GetParametr("server_ip"));
        private int port = int.Parse(Config.GetParametr("server_port"));
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

        public void Sender(string id, int type, string msg)
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

                MSG m = new MSG(id, type, msg);

                byte[] buf = new byte[1024];

                buf = Util.Serialization(m);

                client.Send(buf);
            }
            catch (SocketException se)
            {
                stat = false;

                if (se.ErrorCode == 10054)
                {
                    listbox_msg("Соединение потеряно.Пробуем переподключиться... ");

                    Thread.Sleep(3000);

                    Sender(id, type, msg);
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

                server.Sender("BalanceModule", 2, "status");

                while (client.Connected)
                {
                    try
                    {
                        byte[] bytes = new byte[256];

                        client.Receive(bytes);

                        MSG msg1 = new MSG("0", 0, "null");

                        msg1 = Util.DeSerialization(bytes);

                        Packages.parse(msg1.group, msg1.type, msg1.message);
                    }
                    catch (SocketException exc)
                    {
                        Thread.Sleep(5000);
                        continue;
                    }
                    catch (Exception exc)
                    {
                        (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.Items.Add(exc.Message); }));
                    }
                }

                if (!client.Connected)
                {
                    try { Thread.CurrentThread.Abort(); }
                    catch
                    {
                        Thread.CurrentThread.Join();
                        Thread.ResetAbort();
                    }

                }
        });
            th.Start();
            threads.Add(th);
        }
    }
}
