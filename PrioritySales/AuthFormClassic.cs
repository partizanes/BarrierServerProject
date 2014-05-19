using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrioritySales
{
    public partial class AuthFormClassic : Form
    {
        public int xOffset, yOffset;
        public bool isMouseDown = false;
        private Point mouseOffset;
        public static int log_level = 3;
        public static int version = 13;
        public static bool debug;
        public static bool status = true;
        private string lastLogin;
        private bool SaveLastLogin;
        public static bool permanentPanel;
        public static Connecting connecting = new Connecting();

        public AuthFormClassic()
        {
            InitializeComponent();

            //проверка конфигурационных файлов и модуля обновления
            checkDllAndConfig();

            //Отобржение формы подключения к бд и проверка версии приложения
            CheckVersion();
        }

        private void checkDllAndConfig()
        {
            if (!System.IO.File.Exists(Environment.CurrentDirectory + "\\" + "config.ini"))
            {
                MessageBox.Show("Внимание при запуске приложения было обнаружено отсутсвие конфигурационного файла.");
                Application.Exit();
            }
            if (!System.IO.File.Exists(Environment.CurrentDirectory + "\\" + "Update.exe"))
            {
                MessageBox.Show("Внимание при запуске приложения было обнаружено отсутсвие модуля обновления.");
                Application.Exit();
            }
            if (!System.IO.File.Exists(Environment.CurrentDirectory + "\\" + "update.ini"))
            {
                MessageBox.Show("Внимание при запуске приложения было обнаружено отсутсвие конфигурации модуля обновления.");
                Application.Exit();
            }
            if (!System.IO.File.Exists(Environment.CurrentDirectory + "\\" + "Serialization.dll"))
            {
                MessageBox.Show("[" + DateTime.Now.ToLongTimeString() + "] " + "Внимание при запуске приложения было обнаружено отсутсвие библиотеки Serialization.dll");
                Application.Exit();
            }
            if (!System.IO.File.Exists(Environment.CurrentDirectory + "\\" + "MySql.Data.dll"))
            {
                MessageBox.Show("[" + DateTime.Now.ToLongTimeString() + "] " + "Внимание при запуске приложения было обнаружено отсутсвие библиотеки MySql.Data.dll");
                Application.Exit();
            }
            try
            {
                log_level = int.Parse(Config.GetParametr("log_level"));
                debug = Boolean.Parse(Config.GetParametr("Debug"));
                SaveLastLogin = bool.Parse(Config.GetParametr("SaveLastLogin"));
                lastLogin = Config.GetParametr("LastLogin");
                permanentPanel = bool.Parse(Config.GetParametr("permanentPanel"));
            }
            catch (FormatException) { 
                Config.Set("SETTINGS", "log_level", "3");
                debug = true;
                SaveLastLogin = true;
                lastLogin = "partizanes";
                permanentPanel = false;
            }

        }

        public void CheckVersion()
        {
            ButtonSend.Enabled = false;

            connecting.Show();

            Action<object> action = (object obj) =>
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
                finally
                {
                    AuthFormClassic.status = false;
                    (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { ButtonSend.Enabled = true; }));
                }
            };

            Task t = new Task(action, "CheckVersion");
            t.Start();
        }

        private void AuthFormClassic_Shown(object sender, EventArgs e)
        {
            try
            {
                if (SaveLastLogin)
                {
                    LabelUserText.Text = lastLogin;

                    LabelUserText.SelectionStart = LabelUserText.Text.Length;
                }

            }
            catch (Exception exc) { Log.ExcWrite("[AuthFormClassic_Shown]" + exc.Message); }

            connecting.Hide();

            LabelUserText.Focus();

             Thread th = new Thread(delegate()
             {
                 try
                 {
                     Thread.Sleep(100);
                     (Application.OpenForms[1] as AuthFormClassic).BeginInvoke((MethodInvoker)(delegate() { labelB.Visible = true; }));
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
                 
                 finally
                 {
                     //Визуальный эффект(ожидание проверки версии приложения)
                     VisualEffect();
                 }

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
                        if (LabelUserText.Text.Length > 3){
                            e.SuppressKeyPress = true;
                            PassTextBox.Focus();
                        }
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
                    e.SuppressKeyPress = true;
                    LabelUserText.Focus();
                    break;
                case Keys.Enter:
                case Keys.Down:
                    if (LabelUserText.Text.Length > 3 && PassTextBox.Text.Length > 3){
                        e.SuppressKeyPress = true;
                        ButtonSend.Focus();
                    }
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
                    e.SuppressKeyPress = true;
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
                    e.SuppressKeyPress = true;
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

        private void VisualEffect()
        {
            Thread th = new Thread(delegate()
            {
                try
                {
                    while (!ButtonSend.Enabled)
                    {
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelB.ForeColor = Color.DarkBlue; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelB.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelC.ForeColor = Color.DarkBlue; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelC.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelD.ForeColor = Color.DarkBlue; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelD.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelE.ForeColor = Color.DarkBlue; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelE.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelF.ForeColor = Color.DarkBlue; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelF.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelG.ForeColor = Color.DarkBlue; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelG.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelI.ForeColor = Color.DarkBlue; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelI.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelJ.ForeColor = Color.DarkBlue; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelJ.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelK.ForeColor = Color.DarkBlue; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelK.ForeColor = Color.Yellow; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelL.ForeColor = Color.DarkBlue; }));
                        Thread.Sleep(50);
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { labelL.ForeColor = Color.Yellow; }));
                    }
                }
                catch (Exception exc) { MessageBox.Show(exc.Message); }

            }); ;
            th.Name = "Visual AutoForm";
            th.Start();
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
