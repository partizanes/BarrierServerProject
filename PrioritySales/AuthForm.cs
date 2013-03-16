﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PrioritySales
{
    public partial class AuthForm : Form
    {
        public static int log_level = 3;
        public const string ver = "0.87 alfa";
        public int xOffset, yOffset;
        public bool isMouseDown = false;
        private Point mouseOffset;
        public static Connecting connecting = new Connecting();

        public static void HideThis()
        {
            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).Hide(); }));
        }

        public void check_dll()
        {
            if(Boolean.Parse(Config.GetParametr("ConnectorCheck")))
            {
                if(Environment.Is64BitOperatingSystem)
                    while (!System.IO.File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\MySQL\\MySQL Connector Net " + Config.GetParametr("ConnectorVersion") + "\\Assemblies\\v4.0\\MySql.Data.dll"))
                    {
                        MessageBox.Show("[" + DateTime.Now.ToLongTimeString() + "] " + "Внимание на компьютере не найден MySQL Connector Net,установите и нажмите ок.Отключить проверку коннектора возможно в файле конфигурации");

                        if (!Boolean.Parse(Config.GetParametr("ConnectorCheck")))
                            return;
                    }
                else
                    while (!System.IO.File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\MySQL\\MySQL Connector Net " + Config.GetParametr("ConnectorVersion") + "\\Assemblies\\v4.0\\MySql.Data.dll"))
                    {
                        MessageBox.Show("[" + DateTime.Now.ToLongTimeString() + "] " + "Внимание на компьютере не найден MySQL Connector Net,установите и нажмите ок.Отключить проверку коннектора возможно в файле конфигурации");

                        if (!Boolean.Parse(Config.GetParametr("ConnectorCheck")))
                            return;
                    }
            }

            while (!System.IO.File.Exists(Environment.CurrentDirectory + "\\" + "Serialization.dll"))
            {
                MessageBox.Show("[" + DateTime.Now.ToLongTimeString() + "] " + "В папке с программой отсутствует нужная для работы библиотека Serialization.dll \n Скопируйте в папку с программой библиотеку и нажмите ок!");
            }

            if(!File.Exists(Environment.CurrentDirectory + "\\" + "config.ini"))
            {
                try
                {
                    Encoding outputEnc = new UTF8Encoding(false);

                    string[] createText = { @"[SETTINGS]",
                                              ";Это файл конфигурации не изменяйте параметры ,если вы не понимаете что делаете!",
                                              "",
                                              ";Расположение сервера авторизации (barrier server)",
                                              "IpAuthServer=127.0.0.1",
                                              "",
                                              ";Порт сервера авторизации (по умолчанию 1991)",
                                              "PortAuthServer=1991",
                                              "",
                                              "; расположение кассового сервера (ukm server)",
                                              "IpCashServer=192.168.1.100",
                                              "",
                                              ";Порт кассового сервера (по умолчанию 7000)",
                                              "PortCashServer=7000",
                                              "",
                                              ";Сохранять последнее имя пользователя",
                                              "SaveLastLogin=true",
                                              "",
                                              ";Последнее имя пользователя",
                                              "LastLogin=partizanes",
                                              "",
                                              ";Уровень записи в лог",
                                              "log_level=3",
                                              "",
                                              ";Название база данных ukm на кассовом севрере",
                                              "BdName=ukmserver",
                                              "",
                                              ";Проверка наличия установленного Mysql connector",
                                              "ConnectorCheck=true",
                                              "",
                                              ";Версия Mysql Connector",
                                              "ConnectorVersion=6.6.5"
                                          };

                    File.WriteAllLines(Environment.CurrentDirectory + "\\" + "config.ini", createText, outputEnc);
                }
                catch (System.Exception ex)
                {
                    Log.log_write(ex.Message,"EXCEPTION","exception");
                }
            }
        }

        public AuthForm()
        {
            InitializeComponent();

            check_dll();

            connecting.Show();

            Application.DoEvents();

            Packages.connector.ExecuteNonQuery("SHOW DATABASES", "barrierserver");
            try { log_level = int.Parse(Config.GetParametr("log_level")); }
            catch (FormatException){ Config.Set("SETTINGS", "log_level", "3"); }

            LabelVersion.Text = ver;

            bool st = false;

            if (bool.TryParse((Config.GetParametr("SaveLastLogin")), out st))
            {
                if (st)
                {
                    check_save_login.Checked = true;
                    TextboxLogin.Text = Config.GetParametr("LastLogin");
                    TextboxLogin.Select(TextboxLogin.Text.Length, 0);
                }
            }
        }

        public void buttonCancel_Click()
        {
            buttonLogin.Text = "Войти";
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (buttonLogin.Text != "Отмена")
            {
                if (Config.GetParametr("IpAuthServer") == "" || Config.GetParametr("PortAuthServer") == "")
                {
                    MessageBox.Show("Не настроены параметры подлючения.Для настройки подключения нажмите значок 'н' в верхем правом углу формы авторизации двойным нажатием мыши или обратитесь к администратору.");
                    return;
                }

                if (TextboxLogin.Text.Length < 3 || textboxPass.Text.Length < 1)
                {
                    set_msg_on_timer("         Чего то не хватает!");
                }
                else
                {
                    if (check_save_login.Checked)
                    {
                        Config.Set("SETTINGS", "SaveLastLogin", "true");
                        Config.Set("SETTINGS", "LastLogin", TextboxLogin.Text);
                    }
                    else
                    {
                        Config.Set("SETTINGS", "SaveLastLogin", "false");
                        Config.Set("SETTINGS", "LastLogin", "");
                    }

                    buttonLogin.Text = "Отмена";

                    Thread thh = new Thread(delegate()
                    {
                        Server.Connect();

                        string hash;

                        using (MD5 md5Hash = MD5.Create())
                        {
                            hash = GetMd5Hash(md5Hash, (GetMd5Hash(md5Hash, "1?234%5aZ!") + GetMd5Hash(md5Hash, textboxPass.Text)));
                        }

                        if (Server.server.Connected)
                        {
                            Server.Sender("PrioritySale", 0, TextboxLogin.Text + ":" + hash);
                        }
                        else
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).buttonCancel_Click(); }));
                    }); ;
                    thh.Name = "Авторизация";
                    Server.threads.Add(thh);
                    thh.Start();
                    //thh.Join();
                }
            }
            else
            {
                try { (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).buttonCancel_Click(); })); }
                catch { }
            }
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private void login_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (TextboxLogin.TextLength < 3)
                {
                    Random rnd = new Random();
                    int i = rnd.Next(3);
                    switch (i)
                    {
                        case 0:
                            set_msg_on_timer("    Логин кто вводить будет?");
                            break;
                        case 1:
                            set_msg_on_timer("    Так дело не пойдёт...Логин?");
                            break;
                        case 2:
                            set_msg_on_timer("    Так сложно представиться?");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    check_save_login.Visible = true;
                    textboxPass.Focus();

                    Random rnd = new Random();
                    int i = rnd.Next(3);
                    switch (i)
                    {
                        case 0:
                            set_msg_on_timer("      Очень приятно ;) я Action!");
                            break;
                        case 1:
                            set_msg_on_timer("           Привет " + TextboxLogin.Text + "!");
                            break;
                        case 2:
                            set_msg_on_timer("              Готов к работе!");
                            break;
                        default:
                            break;
                    }
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        public void set_msg_on_timer(string text)
        {
            labelTimerMsg.Enabled = false;
            labelTimerMsg.Interval = 3000;
            labelTimerMsg.Enabled = true;
            LabelMsg.Text = text;
        }

        private void labelTimerMsg_Tick(object sender, EventArgs e)
        {
            labelTimerMsg.Enabled = false;
            LabelMsg.Text = "";
        }

        //start блок перетаскивание формы
        //-////////////////////////////////////////////////////////////////
        private void pass_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (textboxPass.TextLength < 3)
                {
                    Random rnd = new Random();
                    int i = rnd.Next(3);

                    switch (i)
                    {
                        case 0:
                            set_msg_on_timer("Издеваетесь?без пароля не пущу");
                            break;
                        case 1:
                            set_msg_on_timer("          Забыли?Не верю!");
                            break;
                        case 2:
                            set_msg_on_timer("      Увы,без пароля нельзя!");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    check_save_login.Focus();
                }
            }

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                TextboxLogin.Focus();
            }

            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }
        private void AuthForm_MouseDown(object sender, MouseEventArgs e)
        {
            form_MouseDown(e);
        }
        private void AuthForm_MouseMove(object sender, MouseEventArgs e)
        {
            form_move(e,0,0);
        }
        private void AuthForm_MouseUp(object sender, MouseEventArgs e)
        {
            form_MouseUp();
        }
        private void login_panel_MouseMove(object sender, MouseEventArgs e)
        {
            form_move(e,0,0);
        }
        private void login_panel_MouseUp(object sender, MouseEventArgs e)
        {
            form_MouseUp();
        }
        private void login_panel_MouseDown(object sender, MouseEventArgs e)
        {
            form_MouseDown(e);
        }
        private void form_MouseUp()
        {
            isMouseDown = false;
        }
        private void form_MouseDown(MouseEventArgs e)
        {           
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }
        private void form_move(MouseEventArgs e,int xa,int ya)
        {
                        
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X-xa, mouseOffset.Y-ya);
                Location = mousePos;
            }
        }
        private void login_button_Enter(object sender, EventArgs e)
        {
            check_save_login.Visible = true;
            buttonLogin.ForeColor = Color.Green;
            buttonLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }
        private void login_button_Leave(object sender, EventArgs e)
        {
            buttonLogin.ForeColor = Color.DodgerBlue;
            buttonLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }
        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            form_MouseUp();
        }
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            form_move(e,132,18);
        }
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            form_MouseDown(e);
        }
        //-////////////////////////////////////////////////////////////////
        //end блок перетаскивание формы

        private void login_textbox_Enter(object sender, EventArgs e)
        {
            labelLogin.ForeColor = Color.Green;
        }

        private void login_textbox_Leave(object sender, EventArgs e)
        {
            labelLogin.ForeColor = Color.DodgerBlue;
        }

        private void pass_textbox_Enter(object sender, EventArgs e)
        {
            if (TextboxLogin.Text.Length < 3)
            {
                TextboxLogin.Focus();
                return;
            }
            else
            {
                check_save_login.Visible = true;
                labelPass.ForeColor = Color.Green;
            }
        }

        private void pass_textbox_Leave(object sender, EventArgs e)
        {
            labelPass.ForeColor = Color.DodgerBlue;
        }

        private void check_save_login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                buttonLogin.Focus();
            }
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
            {
                textboxPass.Focus();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void check_save_login_Enter(object sender, EventArgs e)
        {
            check_save_login.ForeColor = Color.Green;
        }

        private void check_save_login_Leave(object sender, EventArgs e)
        {
            check_save_login.ForeColor = Color.DodgerBlue;
        }

        private void LabelHide_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            TaskbarIcon.Visible = true;
        }

        private void TaskbarIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            TaskbarIcon.Visible = false;
        }

        private void LabelExit_DoubleClick(object sender, EventArgs e)
        {
            TaskbarIcon.Visible = false;
            Application.Exit();
        }

        private void LabelSetting_DoubleClick(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory + "\\config.ini";

            if (File.Exists(path))
                Process.Start(path);
            else
            {
                check_dll();
                Process.Start(path);
            }
        }

        private void AuthForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            TaskbarIcon.Visible = false;
            Process.GetCurrentProcess().Kill();
        }

        private void AuthForm_Load(object sender, EventArgs e)
        {
            connecting.Hide();
        }
    }
}
