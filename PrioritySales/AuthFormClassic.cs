using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PrioritySales
{
    public partial class AuthFormClassic : Form
    {
        public static int log_level = 3;
        public int xOffset, yOffset;
        public bool isMouseDown = false;
        private Point mouseOffset;
        public static Connecting connecting = new Connecting();
        public static bool debug = Boolean.Parse(Config.GetParametr("Debug"));
        public static bool status = true;
        public static int version = 1;

        public AuthFormClassic()
        {
            InitializeComponent();

            //check_dll();

            CheckVersion();

            connecting.Show();

            Application.DoEvents();

            Packages.connector.ExecuteNonQuery("SHOW DATABASES");
            try { log_level = int.Parse(Config.GetParametr("log_level")); }
            catch (FormatException) { Config.Set("SETTINGS", "log_level", "3"); }
        }

        public void CheckVersion()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("SELECT `ver` FROM `version` WHERE `name` = 'PrioritySales'", conn);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr == null) { return; }
                        if (dr.Read())
                        {
                            if (version != dr.GetInt32(0))
                            {
                                if (Config.GetUpdateStatus() == 0)
                                {
                                    MessageBox.Show("Внимание!Последние обновление было неудачным , обратитесь с системному администратору!");
                                    return;
                                }

                                System.Diagnostics.Process.Start("Update.exe");

                                Thread.Sleep(100);

                                System.Diagnostics.Process.GetCurrentProcess().Kill();
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Log.LogWriteDebug("[CheckVersion] " + ex.Message);
            }
        }

        public void check_dll()
        {
            if (Boolean.Parse(Config.GetParametr("ConnectorCheck")))
            {
                if (Environment.Is64BitOperatingSystem)
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

            if (!File.Exists(Environment.CurrentDirectory + "\\" + "config.ini"))
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
                                              "; Отладочная информация",
                                              "Debug = true",
                                              "",
                                              ";Название база данных ukm на кассовом севрере",
                                              "UkmDataBase=ukmserver",
                                              "",
                                              ";Проверка наличия установленного Mysql connector",
                                              "ConnectorCheck=true",
                                              "",
                                              ";Версия Mysql Connector",
                                              "ConnectorVersion=6.6.5"
                                          };

                    File.WriteAllLines(Environment.CurrentDirectory + "\\" + "config.ini", createText, outputEnc);
                }
                catch (Exception ex)
                {
                    Log.log_write(ex.Message, "EXCEPTION", "exception");
                }
            }
        }

        private void AuthFormClassic_Shown(object sender, EventArgs e)
        {
            try
            {
                if (bool.Parse(Config.GetParametr("SaveLastLogin")))
                {
                    LabelUserText.Text = Config.GetParametr("LastLogin");

                    LabelUserText.SelectionStart = LabelUserText.Text.Length;
                }


            }
            catch { }

            connecting.Hide();

            LabelUserText.Focus();

             Thread th = new Thread(delegate()
             {
                 try
                 {
                     Thread.Sleep(100);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelB.Visible = true; }));
                     Thread.Sleep(100);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelB.ForeColor = Color.Yellow; }));
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelC.Visible = true; }));
                     Thread.Sleep(100);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelC.ForeColor = Color.Yellow; }));
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelD.Visible = true; }));
                     Thread.Sleep(100);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelD.ForeColor = Color.Yellow; }));
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelE.Visible = true; }));
                     Thread.Sleep(100);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelE.ForeColor = Color.Yellow; }));
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelF.Visible = true; }));
                     Thread.Sleep(100);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelF.ForeColor = Color.Yellow; }));
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelG.Visible = true; }));
                     Thread.Sleep(100);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelG.ForeColor = Color.Yellow; }));
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelI.Visible = true; }));
                     Thread.Sleep(100);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelI.ForeColor = Color.Yellow; }));
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelJ.Visible = true; }));
                     Thread.Sleep(100);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelJ.ForeColor = Color.Yellow; }));
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelK.Visible = true; }));
                     Thread.Sleep(100);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelK.ForeColor = Color.Yellow; }));
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelL.Visible = true; }));
                     Thread.Sleep(100);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelL.ForeColor = Color.Yellow; }));
                     Thread.Sleep(100);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { LabelAction.Visible = true; }));
                     Thread.Sleep(150);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { LabelAction.Visible = false; }));
                     Thread.Sleep(170);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { LabelAction.ForeColor = Color.LightYellow; }));
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { LabelAction.Visible = true; }));
                     Thread.Sleep(150);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { LabelAction.Visible = false; }));
                     Thread.Sleep(100);
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { LabelAction.ForeColor = Color.Yellow; }));
                     (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { LabelAction.Visible = true; }));
                     
                 }
                 catch (Exception exc) { Console.WriteLine(exc.Message); }
                 
             }); ;
             th.Name = "Visual AutoForm";
             th.Start();
        }

        private void LabelUserText_Enter(object sender, EventArgs e)
        {
            BackPanelUserText.BackColor = Color.Green;
        }

        private void LabelUserText_Leave(object sender, EventArgs e)
        {
            BackPanelUserText.BackColor = Color.DodgerBlue;
        }

        private void PassTextBox_Enter(object sender, EventArgs e)
        {
            BackPanelPassText.BackColor = Color.Green;
        }

        private void PassTextBox_Leave(object sender, EventArgs e)
        {
            BackPanelPassText.BackColor = Color.DodgerBlue;
        }

        private void ButtonSend_Leave(object sender, EventArgs e)
        {
            ButtonSend.ForeColor = Color.DodgerBlue;
        }

        private void ButtonSend_Enter(object sender, EventArgs e)
        {
            ButtonSend.ForeColor = Color.Green;
        }

        private void LabelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LabelUserText_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Down:
                        if (LabelUserText.Text.Length > 3)
                            PassTextBox.Focus();
                        else
                            BackPanelUserText.BackColor = Color.Red;
                        break;
                case Keys.Escape:
                        Application.Exit();
                        break;
            }
        }

        private void PassTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    LabelUserText.Text = "";
                    LabelUserText.Focus();
                    break;
                case Keys.Enter:
                case Keys.Down:
                    if (LabelUserText.Text.Length > 3 && PassTextBox.Text.Length > 3)
                        ButtonSend.Focus();
                    else
                        BackPanelPassText.BackColor = Color.Red;
                        break;
                case Keys.Escape:
                    PassTextBox.Text = "";
                    break;
            }
        }

        private void ButtonSend_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    PassTextBox.Text = "";
                    PassTextBox.Focus();
                    break;
                case Keys.Enter:
                case Keys.Down:
                    if (LabelUserText.Text.Length > 3 && PassTextBox.Text.Length > 3)
                    {
                        try
                        {
                            if (bool.Parse(Config.GetParametr("SaveLastLogin")))
                                Config.Set("SETTINGS", "LastLogin", LabelUserText.Text);
                        }
                        catch { }

                        ButtonSend.Enabled = false;

                        StartStatus();
                    }
                    else
                        ButtonSend.ForeColor = Color.Red;
                    break;
                case Keys.Escape:
                    PassTextBox.Text = "";
                    PassTextBox.Focus();
                    break;

            }
        }

        private void ButtonSend_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Enter:
                case Keys.Up:
                case Keys.Escape:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void StartStatus()
        {
            Thread th = new Thread(delegate()
            {
                AuthFormClassic.status = true;

                try
                {
                    while (status)
                    {
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelB.ForeColor = Color.Green; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelB.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelC.ForeColor = Color.Green; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelC.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelD.ForeColor = Color.Green; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelD.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelE.ForeColor = Color.Green; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelE.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelF.ForeColor = Color.Green; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelF.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelG.ForeColor = Color.Green; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelG.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelI.ForeColor = Color.Green; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelI.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelJ.ForeColor = Color.Green; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelJ.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelK.ForeColor = Color.Green; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelK.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelL.ForeColor = Color.Green; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelL.ForeColor = Color.Yellow; }));
                    }

                    ButtonSend.Enabled = false;
                }
                catch { }

            }); ;
            th.Name = "Visual AutoForm";
            th.Start();

            Thread thh = new Thread(delegate()
            {
                Server.Connect();

                string hash;

                using (MD5 md5Hash = MD5.Create()) { hash = GetMd5Hash(md5Hash, (GetMd5Hash(md5Hash, "1?234%5aZ!") + GetMd5Hash(md5Hash, PassTextBox.Text))); }

                if (Server.server.Connected)
                    Server.Sender("PrioritySale", 0, LabelUserText.Text + ":" + hash);

            }); ;
            thh.Name = "Авторизация";
            Server.threads.Add(thh);
            thh.Start();
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

        private void AuthPanelMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                xOffset = -e.X;
                yOffset = -e.Y;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        private void AuthPanelMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void AuthPanelMain_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void LabelAction_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void LabelAction_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width - 30;
                yOffset = -e.Y - SystemInformation.FrameBorderSize.Height - 7;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        private void LabelAction_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }
    }
}
