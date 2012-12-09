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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Test_Click();
        }

        private void Test_Click()
        {
            if (cas.Init() < 0)
            {
                Console.WriteLine("Init fail");
                return;
            }

            OleDbDataReader dr; 

            Dbf dbf = new Dbf();

            dr = dbf.ExecuteReader("SELECT ip,name,port,model  FROM struct.dbf");

            if (dr == null)
                return;

            while (dr.Read())
            {
                m_ip = dr.GetString(0);
                m_name = Convertall(dr.GetString(1).Replace(" ",""), Encoding.GetEncoding(866), Encoding.GetEncoding(1251));
                m_port = Convert.ToInt32(dr.GetString(2));
                m_model = Convert.ToInt32(dr.GetString(3));

                if (cas.Connection(m_ip, m_port, 1, m_model) == -1)
                {
                    Console.WriteLine("Connect fail");
                    return;
                }

                if (cas.RecvPLU() < 0)
                {
                    Console.WriteLine("Connect fail");
                    return;
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
                    Console.WriteLine(str);
                }

                string dataplu = "";

                while (cas.RecvPLUData(ref dataplu) >= 0)
                {
                    parse_plu_str(dataplu);
                }

                cas.DisconnectAll();
            }

            cas.DeInit();
            Console.WriteLine("Выполнено!");
            Console.WriteLine(count_error);
        }

        private void parse_plu_str(string str)
        {
            string articul = str.Substring(5, 5);

            string barcode = str.Substring(28, 13);

            Int32 price_c = int.Parse(str.Substring(14, 10));
            string name = str.Substring(159, 54);

            //Console.WriteLine(articul + " " + barcode + " " + name + " " + price_c);

            query_sql(articul, price_c);
        }

        private string Convertall(string value, Encoding src, Encoding trg)
        {
            Decoder dec = src.GetDecoder();
            byte[] ba = trg.GetBytes(value);
            int len = dec.GetCharCount(ba, 0, ba.Length);
            char[] ca = new char[len];
            dec.GetChars(ba, 0, ba.Length, ca, 0);
            return new string(ca);
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
                    Console.WriteLine(articul + " Цена Не найдена на кассе!");
                }

                while (reader.Read())
                {
                    UInt32 price_c = Convert.ToUInt32(reader.GetInt32(1));

                    if (price != price_c)
                    {
                        Console.WriteLine(articul + " " + reader.GetString(0) + " Цена на весах: " + price + " Цена на кассе: " + price_c);
                        count_error++;
                    }
                }
            }
            catch(Exception exc) 
            { 
                MessageBox.Show(exc.Message); 
            }
            finally
            {
                if (serverConn.State == ConnectionState.Open)
                {
                    serverConn.Close();
                }
            }
        }
    }
}
