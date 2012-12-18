using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Balance_module;
using MySql.Data.MySqlClient;

namespace BalanceModule
{
    public partial class Form1 : Form
    {
        private string m_ip;
        private string m_name;
        private int m_port;
        private int m_model;
        private Int32 count_error;

        private MySqlCommand cmd;
        private MySqlConnection serverConn;
        private string connStr;

        private bool drag = false;
        private Point start_point = new Point(0, 0);
        private bool draggable = true;
        private string exclude_list = "";

        private int[] data_delete = new int[5000];

        //import dll from use configuration file
        [DllImport("kernel32.dll")]
        static extern uint GetPrivateProfileString(
        string lpAppName,
        string lpKeyName,
        string lpDefault,
        StringBuilder lpReturnedString,
        uint nSize,
        string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName,
           string lpKeyName, string lpString, string lpFileName);

        Server server = new Server();

        public Form1()
        {
                InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            //mb this not need . Check this and remove
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case 49741:
                case 49615:
                case 49566:
                case 49313:
                case 49541:
                case 49770:
                case 49567:
                case 49547:
                    (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.SelectionMode = SelectionMode.One; }));
                    (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.SetSelected(listBox1.Items.Count - 1, true); }));
                    (Application.OpenForms[0] as Form1).listBox1.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as Form1).listBox1.SetSelected(listBox1.Items.Count - 1, false); }));
                        break;
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            listBox1.Items.Add("Модуль загружен!");

            Thread thh = new Thread(delegate()
            {
                send_msg("BS 0 123456");

                StartScan();
            }); ;
            thh.Name = "Сканирование";
            thh.Start();
        }

        private void restart()
        {
            send_msg("BS 1 Запуск запланированной проверки весов.");

            string EntryTime = DateTime.Now.ToLongTimeString().Replace(":","_");
            string EntryDate = DateTime.Today.ToShortDateString().Replace(".", "_");

            foreach (string item in listBox1.Items)
            {
                Log.log_write(item, "Backup", EntryDate +"_"+ EntryTime );
            }

            this.Invoke((Action)delegate { checkedListBox1.Items.Clear(); });
            this.Invoke((Action)delegate { listBox1.Items.Clear(); });

            list_msg("Сделана копия событий: " + EntryDate + "_" + EntryTime+".log");

             Thread thh = new Thread(delegate()
            {
                StartScan();
            }); ;
            thh.Name = "Повторное Сканирование";
            list_msg("Запуск запланированного сканирования...");
            thh.Start();
        }

        private void send_msg(string msg)
        {
            server.Sender(msg);
        }

        private void StartScan()
        {
            if (!server.disc_client)
            {
                ThreadAbort.th_abort();
                return;
            }

            if (cas.Init() < 0)
            {
                list_msg("Отказ инициализации библиотеки.");
                    return;
            }

            OleDbDataReader dr; 

            Dbf dbf = new Dbf();

            dr = dbf.ExecuteReader("SELECT ip,name,port,model  FROM struct.dbf");

            if (!dr.HasRows)
                list_msg("База весов пустая!");

            while (dr.Read())
            {
                try
                {
                    m_ip = dr.GetString(0).Replace(" ","");
                    m_name = Convertall(dr.GetString(1).Replace(" ", ""), Encoding.GetEncoding(866), Encoding.GetEncoding(1251));
                    m_port = Convert.ToInt32(dr.GetString(2));
                    m_model = Convert.ToInt32(dr.GetString(3));

                    this.Invoke((Action)delegate { checkedListBox1.Items.Add(m_ip + ":" + m_port + " " + m_name); });
                }
                catch (System.Exception ex)
                {
                    //TODO Написать обработчик исключений ,с отправкой на сервер отчетов ;
                    list_msg("Произошло исключение при считывании параметров весов! Адрес" + m_ip + " Имя: " + m_name + " Порт: " + m_port + " Модель: " + m_model);
                    list_msg("Текст исключения: " + ex.Message);

                    Log.log_write(ex.Message, "Exception", "Exception");

                    break;
                }

                if (cas.Connection(m_ip, m_port, 1, m_model) == -1)
                {
                    list_msg("Соединение с весами " + m_ip + ": " + m_port + " не удалось!");

                    int i = 3;

                    while (i != 0)
                    {
                        try
                        {
                            list_msg("Пробуем еще раз...");
                            Thread.Sleep(2000);

                            if (cas.Connection(m_ip, m_port, 1, m_model) != -1)
                                i = 0;
                            else
                            {
                                list_msg("Соединение с весами " + m_ip + ": " + m_port + " не удалось!");
                                i--;
                            }
                        }
                        catch(Exception ex)
                        {
                            i = 0;
                            send_msg("BS 1 BalanceModule: Соединение с весами " + m_ip + ": " + m_port + " не удалось!");
                            Log.log_write(ex.Message, "Exception", "Exception");
                        }
                        finally
                        {
                            if (i == 0)
                                send_msg("BS 1 BalanceModule: " + m_ip + ": " + m_port + " весы пропущены!");
                        }
                    }
                    continue;
                }

                if (cas.RecvPLU() < 0)
                {
                    list_msg("Соединение не удалось!Количество полученых записей меньше 0");
                    break ;
                }
                    /* 
                    1	Disconnect or don't start
                    11	Connecting
                    20	Reconnect
                    30	Disconnection
                    40	Receive data
                    50	Send data
                    51	1:Success
                    52	2:Connect fail
                    53	3:Retry fail
                    54	4:Data error
                    55	5:Timeout
                    60	Send fail
                    65	Find not ip of scale
                    70	No define
                    80	Send timeout
                    82	Data range over
                    88	Direct MSG full
                    89	Full data
                    97	Format Error
                    98	Retry over
                    99	Disconnect 
                    */

                int r = cas.GetState();

                while (!(r == 99 || r == 55 || r == 30))
                {
                    r = cas.GetState();
                    string str = "";
                    cas.GetTransStatus(m_ip, ref str);  //ipadress
                    list_msg(str);
                    Thread.Sleep(500);
                }

                string dataplu = "";

                while (cas.RecvPLUData(ref dataplu) >= 0)
                {
                    parse_plu_str(dataplu);
                }

                list_msg("Сканирование " + m_ip + " завершено!");
                list_msg("Количество найденых проблем: " + count_error);
                send_msg("BS 1 "+ m_ip +" "+ m_name + " Количество найденых проблем: " + count_error);

                cas.DisconnectAll();

                delete_plu();

                list_msg("Удаление с весов " + m_ip + " завершено!");

                this.Invoke((Action)delegate { checkedListBox1.SetItemChecked(checkedListBox1.Items.IndexOf(m_ip + ":" + m_port + " " + m_name),true); });
            }

            list_msg("Деинициализация...");
            send_msg("BS 1 Спящий режим активирован.");

            try
            {
                cas.DeInit();
            }
            catch (System.Exception ex)
            {
                Log.log_write(ex.Message, "Exception", "Exception");
                Application.Exit();
            }
            finally
            {
                timer_par();
                send_msg("BS 1 Деактивация спящего режима через: " + (timer_start_scan.Interval)/3600000 + " часа");
            }
        }

        private void timer_par()
        {
            try
            {
                this.Invoke((Action)delegate { timer_start_scan.Enabled = true; });
            }
            catch(Exception ex)
            {
                Log.log_write(ex.Message, "Exception", "Exception");
                return;
            }
        }

        private void parse_plu_str(string str)
        {
            try
            {
                Int32 check_articul = int.Parse(str.Substring(4, 1));

                string articul = str.Substring(5, 5);

                string barcode = str.Substring(28, 13);

                Int32 price_c = int.Parse(str.Substring(14, 10));

                string name = str.Substring(159, 54);

                if (check_articul > 0)
                    articul = check_articul + articul;

                query_sql(articul, price_c);
            }

            catch (System.Exception ex)
            {
                list_msg("Текст исключения: " + ex.Message);
                Log.log_write(ex.Message, "Exception", "Exception");
            }
        }

        private void delete_plu()
        {
            int count = 0;

            string EntryTime = DateTime.Now.ToLongTimeString().Replace(":", "_");
            string EntryDate = DateTime.Today.ToShortDateString().Replace(".", "_");

            this.Invoke((Action)delegate { label2.Visible = true; ; });

            while (count < count_error)
            {
                this.Invoke((Action)delegate { label2.Text = count + " / " + count_error; });

                if (BalanceModule.Visible == true)
                    BalanceModule.ShowBalloonTip(1, "", label2.Text, ToolTipIcon.None);

                if (cas.Connection(m_ip, m_port, 1, m_model) == -1)
                {
                    list_msg("Соединение с весами " + m_ip + ": " + m_port + " не удалось!");

                    int i = 3;

                    while (i != 0)
                    {
                        list_msg("Пробуем еще раз...");
                        Thread.Sleep(2000);
                        if (cas.Connection(m_ip, m_port, 1, m_model) != -1)
                            i = 0;
                        else
                        {
                            list_msg("Соединение с весами " + m_ip + ": " + m_port + " не удалось!");
                            i--;
                        }
                    }
                }

                string articul = data_delete[count].ToString();

                if (articul == "0")
                {
                    count++;
                    cas.DisconnectAll();
                    continue;
                }

                int pluno = int.Parse(articul);

                if (cas.DeletePLU(1, pluno) != 1)
                {
                    list_msg("Удалить товар с весов не удалось:" + m_ip);
                    cas.DisconnectAll();
                    count++; //check this
                    continue;
                }
                else
                {
                    list_msg("C Весов : " + m_ip + " удален товар:" + articul);
                    Log.articul_write(articul, "remove_" + m_ip + "_" + EntryDate + "_" + EntryTime);
                }

                int r = cas.GetState();

                while (!(r == 99 || r == 55))
                {
                    r = cas.GetState();
                    string str = "";
                    cas.GetTransStatus(m_ip, ref str);
                    list_msg(str);
                    Thread.Sleep(300);
                }

                count++;
                cas.DisconnectAll();
            }

            this.Invoke((Action)delegate { label2.Visible = false; });

            return;
        }

        private void query_sql(string articul, Int32 price)
        {

            MySqlDataReader reader;

            connStr = string.Format("server={0};uid={1};pwd={2};database={3};", "192.168.1.100", "pricechecker", "***REMOVED***", "ukmserver");
            
            serverConn = new MySqlConnection(connStr);

            try
            {
                serverConn.Open();

                cmd = new MySqlCommand("SELECT a.name, b.price FROM trm_in_var C LEFT JOIN trm_in_items A ON A.id=C.item LEFT JOIN trm_in_pricelist_items B ON B.item=c.item WHERE a.id='" + articul + "' AND (b.pricelist_id= 1 )", serverConn);

                reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    data_delete[count_error] = int.Parse(articul);
                    count_error++;
                    list_msg(articul + " Цена Не найдена на кассе!");
                    list_msg(articul + " Цена на весах: " + price); //remove after debug
                    Log.log_write(articul + "Причина:Цена не найдена на кассе","INFO","REASON");
                }

                while (reader.Read())
                {
                    UInt32 price_c = Convert.ToUInt32(reader.GetInt32(1));

                    if (price != price_c)
                    {
                        data_delete[count_error] = int.Parse(articul);
                        list_msg(articul + " " + reader.GetString(0) + " Цена на весах: " + price + " Цена на кассе: " + price_c);
                        Log.log_write(articul + "Причина:Цены не равны ;Цена на весах: " + price + " Цена на кассе: " + price_c, "INFO", "REASON");
                        count_error++;
                    }
                }
            }
            catch(Exception ex)
            {
                list_msg(ex.Message);
                Log.log_write(ex.Message, "Exception", "Mysql_Exception");
            }
            finally
            {
                if (serverConn.State == ConnectionState.Open)
                {
                    serverConn.Close();
                }
            }
        }

        //end main block

        private string Convertall(string value, Encoding src, Encoding trg)
        {
            Decoder dec = src.GetDecoder();
            byte[] ba = trg.GetBytes(value);
            int len = dec.GetCharCount(ba, 0, ba.Length);
            char[] ca = new char[len];
            dec.GetChars(ba, 0, ba.Length, ca, 0);
            return new string(ca);
        }

        //form move block

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (this.Draggable && (this.ExcludeList.IndexOf(e.Control.Name) == -1))
            {
                e.Control.MouseDown += new MouseEventHandler(Form_MouseDown);
                e.Control.MouseUp += new MouseEventHandler(Form_MouseUp);
                e.Control.MouseMove += new MouseEventHandler(Form_MouseMove);
            }
            base.OnControlAdded(e);
        }

        void Form_MouseDown(object sender, MouseEventArgs e)
        {
            this.drag = true;
            this.start_point = new Point(e.X, e.Y);
        }

        void Form_MouseUp(object sender, MouseEventArgs e)
        {
            this.drag = false;
        }

        void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.drag)
            {
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - this.start_point.X,
                                     p2.Y - this.start_point.Y);
                this.Location = p3;
            }
        }

        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.drag)
            {
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - this.start_point.X,
                                     p2.Y - this.start_point.Y);
                this.Location = p3;
            }
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.drag = false;
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.drag = true;
            this.start_point = new Point(e.X, e.Y);
        }

        private void splitContainer1_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.drag = true;
            this.start_point = new Point(e.X, e.Y);
        }

        private void splitContainer1_Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.drag)
            {
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - this.start_point.X,
                                     p2.Y - this.start_point.Y);
                this.Location = p3;
            }
        }

        private void splitContainer1_Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            this.drag = false;
        }

        public string ExcludeList
        {
            set
            {
                this.exclude_list = value;
            }
            get
            {
                return this.exclude_list.Trim();
            }
        }

        public bool Draggable
        {
            set
            {
                this.draggable = value;
            }
            get
            {
                return this.draggable;
            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            panel1.Focus();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Thread.Sleep(1000);
            server.disc_client = false;
            Application.Exit();
        }

        private void list_msg(string msg)
        {
            try
            {
                this.Invoke((Action)delegate { listBox1.Items.Add(msg); });

                this.Invoke((Action)delegate { listBox1.SelectionMode = SelectionMode.One; });
                this.Invoke((Action)delegate { listBox1.SetSelected(listBox1.Items.Count - 1, true); });
                this.Invoke((Action)delegate { listBox1.SetSelected(listBox1.Items.Count - 1, false); });
            }

            catch(Exception ex)
            {
                Log.log_write(ex.Message, "Exception", "Exception");
                return;
            }
        }

        private void timer_start_scan_Tick(object sender, EventArgs e)
        {
            restart();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Server.client.Connected)
                send_msg("BS 9 00");

            BalanceModule.Visible = false;
        }

        private void BalanceModule_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BalanceModule.Visible = false;
            this.Visible = true;
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            BalanceModule.Visible = true;
            this.Visible = false;
        }
    }
}
