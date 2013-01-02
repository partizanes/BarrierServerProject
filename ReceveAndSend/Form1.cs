using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace ReceveAndSend
{
    public partial class Form1 : Form
    {
        private string[] data = new string[5000];
        private Int32 count;
        private Int32 PortFrom = 7000;
        private Int32 ModelFrom = 5010;
        private Int32 PortWhere = 7000;
        private Int32 ModelWhere = 5010;
        private String[] arguments;
        private Boolean BreakTread = false;

        public Form1()
        {
            InitializeComponent();

            arguments = Environment.GetCommandLineArgs();
            //192.168.1.191 7000 5010 192.168.1.178 7000 5010 1 

            if (arguments.Length != 8)
                return;

            if(arguments[1].Length > 0)
                TextBoxFrom.Text = arguments[1];  //from address

            if (arguments[2].Length > 0)
                Int32.TryParse(arguments[2], out PortFrom);  //port from

            if (arguments[3].Length > 0)
                Int32.TryParse(arguments[3], out ModelFrom);  //model from

            // ===========================================================

            if (arguments[4].Length > 0)
                TextBoxWhere.Text = arguments[4];  //from address

            if (arguments[5].Length > 0)
                Int32.TryParse(arguments[5], out PortWhere);  //port from

            if (arguments[6].Length > 0)
                Int32.TryParse(arguments[6], out ModelWhere);  //model from

            // ===========================================================
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ButtonStart.Text == "Отмена")
            {
                BreakTread = true;
                ResetParametr(true);
                return;
            }

            ResetParametr(false);

            Thread thh = new Thread(delegate()
            {
                if (cas.Init() < 0)
                {
                    this.Invoke((Action)delegate { listBox1.Items.Add(" Отказ инициализации библиотеки."); });
                    ResetParametr(true);
                    return;
                }
                else
                {
                    this.Invoke((Action)delegate { LabelInfo.Text = "  Библиотека инициализирована."; });
                    Thread.Sleep(1000);
                }

                if (cas.Connection(TextBoxFrom.Text, PortFrom, 1, ModelFrom) == -1)
                {
                    this.Invoke((Action)delegate { listBox1.Items.Add("Не удалось соединится"); });
                    this.Invoke((Action)delegate { LabelInfo.Text = "   Не удалось соединится."; });
                    ResetParametr(true);
                    return;
                }
                else
                {
                    this.Invoke((Action)delegate { LabelInfo.Text = "       Соединение успешно."; });
                    Thread.Sleep(1000);
                }

                if (cas.RecvPLU() < 0)
                {
                    this.Invoke((Action)delegate { listBox1.Items.Add("Соединение не удалось!Количество полученых записей меньше 0"); });
                    this.Invoke((Action)delegate { listBox1.Items.Add("     Не удалось соединится"); });
                    ResetParametr(true);
                    return;
                }
                else
                {
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Получение данных."; });
                    Thread.Sleep(500);
                }

                int r = cas.GetState();

                while (!(r == 99 || r == 55))
                {
                    if(BreakTread)
                    {
                        try
                        {
                            this.Invoke((Action)delegate { LabelInfo.Text = "                   Прервано."; });
                            ResetParametr(true);
                            Thread.CurrentThread.Abort();
                        }
                        catch
                        {
                            Thread.CurrentThread.Join();
                            Thread.ResetAbort();
                        }
                    }

                    r = cas.GetState();
                    string str = "";
                    cas.GetTransStatus(TextBoxFrom.Text, ref str);  //ipadress
                    this.Invoke((Action)delegate { listBox1.Items.Add("Чтение параметров: " + str); });
                    this.Invoke((Action)delegate { listBox1.SelectedIndex = listBox1.Items.Count - 1; ; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          З"; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          За"; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Заг"; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Загр"; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Загру"; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Загруз"; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Загрузк"; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Загрузка"; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Загрузка с"; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Загрузка с в"; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Загрузка с ве"; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Загрузка с вес"; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Загрузка с весо"; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Загрузка с весов"; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Загрузка с весов."; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Загрузка с весов.."; });
                    Thread.Sleep(100);
                    this.Invoke((Action)delegate { LabelInfo.Text = "          Загрузка с весов..."; });
                }

                count = 1;

                string dataplu = "";

                this.Invoke((Action)delegate { progressBar1.Maximum = 5000; });

                while (cas.RecvPLUData(ref dataplu) >= 0)
                {
                    this.Invoke((Action)delegate { listBox1.Items.Add(dataplu); });
                    this.Invoke((Action)delegate { listBox1.SelectedIndex = listBox1.Items.Count - 1; ; });
                    data[count] = dataplu;
                    count++;
                    this.Invoke((Action)delegate { progressBar1.Value++; });
                    this.Invoke((Action)delegate { LabelInfo.Text = "       Получено строк: " + count; });
                }

                this.Invoke((Action)delegate { progressBar1.Maximum = count; });
                this.Invoke((Action)delegate { progressBar1.Minimum = 0; });
                this.Invoke((Action)delegate { progressBar1.Value = 0; });

                cas.DisconnectAll();
                cas.DeInit();

                SendThis();

            }); ;
            thh.Name = "Сканирование";
            thh.Start();
        }

        private void SendThis()
        {
            if (cas.Init() < 0)
            {
                this.Invoke((Action)delegate { listBox1.Items.Add("Отказ инициализации библиотеки."); });
                this.Invoke((Action)delegate { listBox1.SelectedIndex = listBox1.Items.Count - 1; ; });
                ResetParametr(true);
                return; 
            }
            else
            {
                this.Invoke((Action)delegate { LabelInfo.Text = "Библиотека инициализирована."; });
                Thread.Sleep(1000);
            }

            if (cas.Connection(TextBoxWhere.Text, PortWhere, 1, ModelWhere) == -1)   
            {
                this.Invoke((Action)delegate { listBox1.Items.Add("Не удалось соединится c принимающей стороной !"); });
                this.Invoke((Action)delegate { LabelInfo.Text = "       Соединение не удалось!"; });
                this.Invoke((Action)delegate { listBox1.SelectedIndex = listBox1.Items.Count - 1; ; });
                ResetParametr(true);
            }
            else
            {
                this.Invoke((Action)delegate { LabelInfo.Text = "       Соединение успешно."; });
                Thread.Sleep(1000);
            }

            for (int i = 1; i < count; i++)
            {
                string stra = data[i].Substring(0,196);

                if (cas.AddPLU(stra) < 0)
                {
                    this.Invoke((Action)delegate { listBox1.Items.Add("Не удалось добавить штрихкод!" + stra); });
                    this.Invoke((Action)delegate { listBox1.SelectedIndex = listBox1.Items.Count - 1; ; });
                    this.Invoke((Action)delegate { LabelInfo.Text = "Добавление штрихкодов на загрузку."; });
                    Thread.Sleep(1000);
                }
            }

            if (cas.SendPLU() < 0)
            {
                this.Invoke((Action)delegate { listBox1.Items.Add("Отправка кодов на весы неудачна!"); });
                this.Invoke((Action)delegate { listBox1.SelectedIndex = listBox1.Items.Count - 1; ; });
                this.Invoke((Action)delegate { LabelInfo.Text = "Отправка на весы."; });
            }

            int r = cas.GetState();

            int p = 0;

            while (!(r == 99 || r == 55))
            {
                if (BreakTread)
                {
                    try
                    {
                        this.Invoke((Action)delegate { LabelInfo.Text = "                   Прервано."; });
                        Thread.CurrentThread.Abort();
                    }
                    catch
                    {
                        Thread.CurrentThread.Join();
                        Thread.ResetAbort();
                    }
                }

                r = cas.GetState();
                string str = "";
                cas.GetTransStatus(TextBoxWhere.Text, ref str);
                this.Invoke((Action)delegate { listBox1.Items.Add("Отправка параметров: " +str); });
                this.Invoke((Action)delegate { listBox1.SelectedIndex = listBox1.Items.Count - 1; ; });

                switch (r)
                {
                    case 1:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Разъединено."; });
                        break;
                    case 11:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Соединяюсь..."; });
                        break;
                    case 20:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Переподключение..."; });
                        break;
                    case 30:
                        switch (p)
                        {
                            case 0:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   С"; });
                                p++;
                                break;
                            case 1:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Ста"; });
                                p++;
                                break;
                            case 2:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Стат"; });
                                p++;
                                break;
                            case 3:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Стату"; });
                                p++;
                                break;
                            case 4:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус"; });
                                p++;
                                break;
                            case 5:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус:"; });
                                p++;
                                break;
                            case 6:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: "; });
                                p++;
                                break;
                            case 7:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: П"; });
                                p++;
                                break;
                            case 8:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Пр"; });
                                p++;
                                break;
                            case 9:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Про"; });
                                p++;
                                break;
                            case 10:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Прои"; });
                                p++;
                                break;
                            case 11:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Проис"; });
                                p++;
                                break;
                            case 12:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происх"; });
                                p++;
                                break;
                            case 13:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происхо"; });
                                p++;
                                break;
                            case 14:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происход"; });
                                p++;
                                break;
                            case 15:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происходи"; });
                                p++;
                                break;
                            case 16:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происходит"; });
                                p++;
                                break;
                            case 17:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происходит "; });
                                p++;
                                break;
                            case 18:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происходит о."; });
                                p++;
                                break;
                            case 19:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происходит от"; });
                                p++;
                                break;
                            case 20:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происходит отп"; });
                                p++;
                                break;
                            case 21:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происходит отпр"; });
                                p++;
                                break;
                            case 22:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происходит отпра"; });
                                p++;
                                break;
                            case 23:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происходит отправ"; });
                                p++;
                                break;
                            case 24:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происходит отправк"; });
                                p++;
                                break;
                            case 25:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происходит отправка"; });
                                p++;
                                break;
                            case 26:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происходит отправка."; });
                                p++;
                                break;
                            case 27:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происходит отправка.."; });
                                p++;
                                break;
                            case 28:
                                this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Происходит отправка..."; });
                                p++;
                                break;
                            case 29:
                                this.Invoke((Action)delegate { LabelInfo.Text = ""; });
                                p++;
                                break;
                            case 30:
                                p = 0;
                                break;
                        }
                        break;
                    case 40:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Получение данных."; });
                        break;
                    case 50:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Отправка данных."; });
                        break;
                    case 51:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Успешно."; });
                        break;
                    case 52:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Неудачно."; });
                        break;
                    case 53:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Переподключение неудачно."; });
                        break;
                    case 54:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Ошибка."; });
                        break;
                    case 55:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Таймаут."; });
                        break;
                    case 60:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Отправка неудачна."; });
                        break;
                    case 65:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Find not ip of scale."; });
                        break;
                    case 80:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Таймаут отправки."; });
                        break;
                    case 89:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Весы заполнены."; });
                        break;
                    case 99:
                        this.Invoke((Action)delegate { LabelInfo.Text = "   Статус: Весы отключены."; });
                        break;
                }
                Thread.Sleep(100);
            }
                
            this.Invoke((Action)delegate { progressBar1.Value++; });

            cas.DisconnectAll();
            cas.DeInit();

            ResetParametr(true);
        }

        private void ResetParametr(Boolean i)
        {
            if (i)
            {
                this.Invoke((Action)delegate { ButtonStart.Text = "Начать"; });
                this.Invoke((Action)delegate { ButtonStart.ForeColor = Color.DodgerBlue; });
                this.Invoke((Action)delegate { TextBoxFrom.Enabled = true; });
                this.Invoke((Action)delegate { TextBoxWhere.Enabled = true; });
            }
            else
            {
                ButtonStart.Text = "Отмена";
                ButtonStart.ForeColor = Color.Red;
                TextBoxFrom.Enabled = false;
                TextBoxWhere.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(235, 197);
            this.Invoke((Action)delegate { LabelInfo.Text = "   Форма успешно загружена."; });
        }

        private void ButtonHide_Click(object sender, EventArgs e)
        {
            if (this.Size == new System.Drawing.Size(235, 197))
            {
                this.Size = new System.Drawing.Size(692, 197);
                this.ButtonHide.Text = "<";
            }
            else
            {
                this.Size = new System.Drawing.Size(235, 197);
                this.ButtonHide.Text = ">";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Приложение будет закрыто?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                e.Cancel = true;
            else
            {
                Process.GetCurrentProcess().Kill();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (arguments.Length != 8)
                return;

            if (arguments[7].Length > 0)
            {
                int start = 0;
                Int32.TryParse(arguments[7], out start);

                if (start > 0)
                {
                    ButtonStart.PerformClick();
                }
            }
        }

        private void ButtonStart_MouseEnter(object sender, EventArgs e)
        {
            if (ButtonStart.Text == "Отмена")
                this.ButtonStart.ForeColor = Color.Red;
            else
                this.ButtonStart.ForeColor = Color.Green;
        }

        private void ButtonStart_MouseLeave(object sender, EventArgs e)
        {
            if(ButtonStart.Text == "Отмена")
                this.ButtonStart.ForeColor = Color.Red;
            else            
                this.ButtonStart.ForeColor = Color.DodgerBlue;
        }

        private void ButtonHide_MouseEnter(object sender, EventArgs e)
        {
            this.ButtonHide.ForeColor = Color.Green;
        }

        private void ButtonHide_MouseLeave(object sender, EventArgs e)
        {
            this.ButtonHide.ForeColor = Color.DodgerBlue;
        }
    }
}
