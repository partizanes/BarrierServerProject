using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
                    (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).buttonCancel_Click(); }));
                }
            }
            catch (System.Exception ex)
            {
                listbox_msg("           Сервер недоступен.");
                MessageBox.Show("Текст исключения: " + ex.Message);
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).buttonCancel_Click(); }));
            }
        }

        public static void Sender(string msg)
        {
            int size = msg.Length;

            string send_packet = size + "|" + msg;

            byte[] bytes = new byte[Encoding.UTF8.GetBytes(send_packet).Length];

            bytes = Encoding.UTF8.GetBytes(send_packet);

            server.Send(bytes);

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
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).LabelMsg.Text = msg; }));
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).LabelMsg.Update(); }));
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).Refresh(); }));
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
                        byte[] bytes = new byte[256];

                        client.Receive(bytes);

                        if (bytes.Length != 0)
                        {
                            //Принимаемый пакет разбор структуры

                            string data = Encoding.UTF8.GetString(bytes);

                            string[] split_data = data.Split(new Char[] { '|' });

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
                            }

                            string com_id = split_data[1].Substring(0, 2);  // id комманды

                            string com_type = split_data[1].Substring(3, 1); // type комманды

                            string msg_data = split_data[1].Substring(5, (Convert.ToInt32(str_len)) - 4); //сообщение

                            Packages.parse(com_id, com_type, msg_data);
                        }
                    }

                    catch (Exception exc)
                    {
                        (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).LabelMsg.Text = exc.Message; }));
                        (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).LabelMsg.Update(); }));
                        (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).Refresh(); }));
                    }
                }
        });
            th.Start();
            th.Name = "Слушаю ответ";
            threads.Add(th);
        }
    }
}
