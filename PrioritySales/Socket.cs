using Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace PrioritySales
{
    class Server
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

            listbox_msg("                Соединение...");

            try
            {
                server.Connect(ip, port);

                Receiver(server);
            }
            catch (SocketException exc)
            {
                if (exc.ErrorCode == 10061)
                {
                    listbox_msg("           Сервер недоступен.");
                    MessageBox.Show("Сервер возможно отключен или недоступен в данный момент времени , уточните параметры сервера в настройках и повторите подключение \n Текст исключения: " + exc.Message);
                    try
                    {
                        (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).buttonCancel_Click(); }));
                        (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).textboxPass.Text = ""; }));
                        (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).textboxPass.Focus(); }));
                    }
                    catch { }
                }
            }
            catch (System.Exception ex)
            {
                listbox_msg("           Сервер недоступен.");
                MessageBox.Show("Текст исключения: " + ex.Message);
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).buttonCancel_Click(); }));
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

            catch (ThreadAbortException)
            {
                threads.Remove(Thread.CurrentThread);
                Thread.ResetAbort();
            }
        }

        public static void listbox_msg(string msg)
        {
            try
            {
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).LabelMsg.Text = msg; }));
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).LabelMsg.Update(); }));
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).Refresh(); }));
            }
            catch (Exception ex)
            {
                Log.log_write(ex.Message, "exception", "exception");

                try
                {
                    Thread.CurrentThread.Abort();
                }
                catch (ThreadAbortException)
                {
                    threads.Remove(Thread.CurrentThread);
                    Thread.ResetAbort();
                }
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
                    catch (SocketException ex)
                    {
                        if (ex.ErrorCode == 10054)
                            Application.Exit();

                        //TODO FORM RECCONECT TO SERVER!!!!
                    }
                    catch (Exception exc)
                    {
                        (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).LabelMsg.Text = exc.Message; }));
                        (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).LabelMsg.Update(); }));
                        (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).Refresh(); }));
                    }
                }
        });
            th.Start();
            th.Name = "Слушаю ответ";
            threads.Add(th);
        }
    }
}
