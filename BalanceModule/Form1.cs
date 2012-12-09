using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Balance_module;
using System.IO;
using System.Threading;
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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            listBox1.Items.Add("Модуль загружен!");

            Thread thh = new Thread(delegate()
            {
                Test_Click();
            });;
            thh.Start();
        }

        private void Test_Click()
        {
            //Thread.Sleep(2000);

            if (cas.Init() < 0)
            {
                listBox1.Items.Add("Отказ инициализации библиотеки.");
                return;
            }

            OleDbDataReader dr; 

            Dbf dbf = new Dbf();

            dr = dbf.ExecuteReader("SELECT ip,name,port,model  FROM struct.dbf");

            if (!dr.HasRows)
                this.Invoke((Action)delegate { listBox1.Items.Add("База весов пустая!"); });

            while (dr.Read())
            {
                try
                {
                    m_ip = dr.GetString(0);
                    m_name = Convertall(dr.GetString(1).Replace(" ", ""), Encoding.GetEncoding(866), Encoding.GetEncoding(1251));
                    m_port = Convert.ToInt32(dr.GetString(2));
                    m_model = Convert.ToInt32(dr.GetString(3));
                }
                catch (System.Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    //TODO Написать обработчик исключений ,с отправкой на сервер отчетов ;
                    this.Invoke((Action)delegate { listBox1.Items.Add("Произошло исключение при считывании параметров весов! Адрес" + m_ip + " Имя: " + m_name + " Порт: " + m_port + " Модель: " + m_model); });
                    this.Invoke((Action)delegate { listBox1.Items.Add("Текст исключения: " + ex.Message); });

                    Log.log_write(ex.Message, "Exception", "Exception");

                    continue;
                }

                if (cas.Connection(m_ip, m_port, 1, m_model) == -1)
                {
                    this.Invoke((Action)delegate { listBox1.Items.Add("Соединение с весами " + m_ip + ": " + m_port + " не удалось!"); });

                    int i = 3;

                    while (i != 0)
                    {
                        this.Invoke((Action)delegate { listBox1.Items.Add("Пробуем еще раз..."); });
                        Thread.Sleep(2000);
                        if (cas.Connection(m_ip, m_port, 1, m_model) != -1)
                            i = 0;
                        else
                        {
                            this.Invoke((Action)delegate { listBox1.Items.Add("Соединение с весами " + m_ip + ": " + m_port + " не удалось!"); });
                            i--;
                        }
                    }

                    continue;
                }

                if (cas.RecvPLU() < 0)
                {
                    this.Invoke((Action)delegate { listBox1.Items.Add("Соединение не удалось!Количество полученых записей меньше 0"); });
                    continue;
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
                    this.Invoke((Action)delegate { listBox1.Items.Add(str); });
                }

                string dataplu = "";

                while (cas.RecvPLUData(ref dataplu) >= 0)
                {
                    parse_plu_str(dataplu);
                }

                cas.DisconnectAll();
            }

            cas.DeInit();

            this.Invoke((Action)delegate { listBox1.Items.Add("Выполнено!"); });
            this.Invoke((Action)delegate { listBox1.Items.Add("Количество найденых проблем: " + count_error); });
        }

        private void parse_plu_str(string str)
        {
            try
            {
                string articul = str.Substring(5, 5);

                string barcode = str.Substring(28, 13);

                Int32 price_c = int.Parse(str.Substring(14, 10));

                string name = str.Substring(159, 54);

                query_sql(articul, price_c);
            }

            catch (System.Exception ex)
            {
                this.Invoke((Action)delegate { listBox1.Items.Add("Текст исключения: " + ex.Message); });
            }
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
                    count_error++;
                    this.Invoke((Action)delegate { listBox1.Items.Add(articul + " Цена Не найдена на кассе!"); });
                }

                while (reader.Read())
                {
                    UInt32 price_c = Convert.ToUInt32(reader.GetInt32(1));

                    if (price != price_c)
                    {
                        this.Invoke((Action)delegate { listBox1.Items.Add(articul + " " + reader.GetString(0) + " Цена на весах: " + price + " Цена на кассе: " + price_c); });
                        count_error++;
                    }
                }
            }
            catch(Exception ex)
            {
                this.Invoke((Action)delegate { listBox1.Items.Add("Текст исключения: " + ex.Message); });
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
    }
}
