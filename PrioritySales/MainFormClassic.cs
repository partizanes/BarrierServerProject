
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrioritySales
{
    public partial class MainFormClassic : Form
    {
        public  MainFormClassic()
        {
            InitializeComponent();

            dateTimePicker1.CustomFormat = "dd-MM-yyyy HH:mm:ss";
        }

        public static bool _forBlinking = true;
        private bool nonNumberEntered = false;
        private int pricelistId = 1;

        public int xOffset, yOffset;
        public bool isMouseDown = false;
        private Point mouseOffset;
        public static string StatusUpdate;
        public static int UserGroup = 0;
        public static int UserId = 0;

        public static Tasks tasks = new Tasks();
        public static InfoControl infocontrol = new InfoControl();
        public static PriorityDetails prioritydetails = new PriorityDetails();
        public static MsgDesk msgdesk = new MsgDesk();
        public static LogForm logform = new LogForm();
        public static string UserName;

        // ForeColor for all button block start

        private void MainFormClassic_Shown(object sender, EventArgs e)
        {
            //WARNING
            ButtonAdd.Focus();

            Action<object> action = (object obj) =>
            {
                CheckTasks();

                Packages.GetMsgBoard();
            };

            Task t = new Task(action, "CheckTasksAndGetMsgBoard");
            t.Start();

            return;
        }

        private void GetUserIdAndGroup()
        {
            Action<object> action = (object obj) =>
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                    {
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand("SELECT `id`,`group` FROM `users` WHERE `username` = '" + UserName + "'", conn);

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr == null) { return; }
                            if (dr.Read())
                            {
                                UserId = dr.GetInt32(0);
                                UserGroup = dr.GetInt32(1);
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Log.log_write("[GetUserIdAndGroup]" + ex.Message, "EXCEPTION", "exception");
                }
            };

            Task t = new Task(action, "GetUserIdAndGroup");
            t.Start();
        }

        private void CheckTasks()
        {
            Thread.Sleep(300);

            Action<object> action = (object obj) =>
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                    {
                        Application.DoEvents();

                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand("SELECT COUNT(tasks_id) FROM tasks WHERE `user_group` = (SELECT `group` FROM `users` WHERE `username` = '" + UserName + "' ) AND `user_id` = 0", conn);

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr == null) { return; }

                            if (dr.Read())
                            {
                                Application.DoEvents();

                                if (AuthFormClassic.permanentPanel)
                                    Packages.parse("PrioritySale", 5, "");

                                if (dr.GetInt32(0) > 0) {
                                    Action<object> action1 = (object obj1) => { Packages.parse("PrioritySale", 8, ""); };
                                    Task t1 = new Task(action1, "Packages.parse(PrioritySale 8)");
                                    t1.Start();
                                    return;
                                }
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Log.ExcWrite("[CheckTasks]" + ex.Message);
                }
            };

            Task t = new Task(action, "CheckTasks");
            t.Start();
        }

        private void ButtonAdd_Enter(object sender, EventArgs e)
        {
            ButtonAdd.ForeColor = Color.Green;
        }

        private void ButtonAdd_Leave(object sender, EventArgs e)
        {
            ButtonAdd.ForeColor = Color.DodgerBlue;
        }

        private void ButtonList_Enter(object sender, EventArgs e)
        {
            ButtonList.ForeColor = Color.Green;
        }

        private void ButtonList_Leave(object sender, EventArgs e)
        {
            ButtonList.ForeColor = Color.DodgerBlue;
        }

        private void ButtonMsg_Enter(object sender, EventArgs e)
        {
            ButtonMsg.ForeColor = Color.Green;
        }

        private void ButtonMsg_Leave(object sender, EventArgs e)
        {
            ButtonMsg.ForeColor = Color.DodgerBlue;
        }

        private void ButtonTasks_Enter(object sender, EventArgs e)
        {
            ButtonTasks.ForeColor = Color.Green;
        }

        private void ButtonTasks_Leave(object sender, EventArgs e)
        {
            ButtonTasks.ForeColor = Color.DodgerBlue;
        }

        private void ButtonUnk2_Enter(object sender, EventArgs e)
        {
            ButtonLog.ForeColor = Color.Green;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            ButtonLog.ForeColor = Color.DodgerBlue;
        }

        private void ButtonSetting_Enter(object sender, EventArgs e)
        {
            ButtonSetting.ForeColor = Color.Green;
        }

        private void ButtonSetting_Leave(object sender, EventArgs e)
        {
            ButtonSetting.ForeColor = Color.DodgerBlue;
        }

        private void ButtonHide_Enter(object sender, EventArgs e)
        {
            ButtonHide.ForeColor = Color.Green;
        }

        private void ButtonHide_Leave(object sender, EventArgs e)
        {
            ButtonHide.ForeColor = Color.DodgerBlue;
        }

        private void ButtonExit_Enter(object sender, EventArgs e)
        {
            ButtonExit.ForeColor = Color.Green;
        }

        private void ButtonExit_Leave(object sender, EventArgs e)
        {
            ButtonExit.ForeColor = Color.DodgerBlue;
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            string[] split_data = LabelUserName.Text.Split(new Char[] { ':' });
            Server.Sender("User", 0000, split_data[1] + ": Bye!");
            System.Threading.Thread.Sleep(500);
            Application.Exit();
        }

        // ForeColor for all button block End

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            PanelAddBg.Location = new System.Drawing.Point(69, 31);

            dateTimePicker1.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            if (PanelAddBg.Visible == false)
            {
                PanelAddHide(false);
                TextboxAddBar.Focus();
            }
            else
            {
                PanelAddHide(true);
                ButtonAdd.Focus();
            }
        }

        //START TextboxBarAction

        private void TextboxAddBar_Enter(object sender, EventArgs e)
        {
            LabelButtonItem.ForeColor = Color.Green;
        }

        private void TextboxAddBar_Leave(object sender, EventArgs e)
        {
            LabelButtonItem.ForeColor = Color.DodgerBlue;
        }

        private void TextboxAddBar_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;
            TextboxAddBar.Text = TextboxAddBar.Text.Trim();

            switch (e.KeyCode)
            {
                case Keys.Escape:
                case Keys.Left:
                    {
                        PanelAddBg.Visible = false;            
                        PanelAddTask.Visible = false;
                        ButtonAdd.Focus();
                        return;
                    }
                case Keys.Up:
                    {
                        dateTimePicker1.Focus();
                        return;
                    }
                case Keys.Right:
                case Keys.Down:
                case Keys.Enter:
                    {
                        if (TextboxAddBar.Text.Length == 0)
                        {
                            PanelAddBg.Visible = false;
                            PanelAddTask.Visible = false;
                            ButtonAdd.Focus();
                            return;
                        }

                        if (TextboxAddBar.Text.Length < 5)
                        {
                            DeclineErr(true, "                                          Баркод должен быть более 4 цифр!");
                            LabelInfo.ForeColor = Color.Yellow;
                            TextboxAddBar.Focus();
                            return;
                        }

                        getname();

                        return;
                    }
            }

            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    if (e.KeyCode != Keys.Back)
                    {
                        if (Control.ModifierKeys != Keys.Control)
                            nonNumberEntered = true;
                    }
                }
            }

            if (Control.ModifierKeys == Keys.Shift)
                nonNumberEntered = true;

            if (Control.ModifierKeys == Keys.Control)
            {
                if (e.KeyCode == Keys.V)
                {
                    nonNumberEntered = false;

                    string tempBar = Clipboard.GetText().Trim();

                    try { Int64.Parse(tempBar); }
                    catch
                    {
                        LabelInfo.ForeColor = Color.Yellow;
                        DeclineErr(true, "                                  Баркод может состоять только из чисел!");
                        return;
                    }

                    if (tempBar.Length == 13)
                    {
                        tempBar = tempBar.Substring(0, 12);
                        Log.log_write("Обрезка 13 символа","SubString","PrioritySales");
                    }

                    if (tempBar.Length == 5 || tempBar.Length == 12)
                    {
                        getname(tempBar);
                    }
                }

                if (e.KeyCode == Keys.X)
                {
                    nonNumberEntered = false;
                }

                if (e.KeyCode == Keys.C)
                {
                    nonNumberEntered = false;
                }

                if (e.KeyCode == Keys.A)
                {
                    nonNumberEntered = false;
                    TextboxAddBar.SelectAll();
                }

                return;
            }

            if (nonNumberEntered)
            {
                LabelInfo.ForeColor = Color.Yellow;
                DeclineErr(true, "                                  Баркод может состоять только из чисел!");
                return;
            }

        }

        //END TextboxBarAction

        //START DateTimeAction

        private void getname()
        {
            PanelAddBg.BackColor = Color.Yellow;

            Action<object> action = (object obj) =>
            {
                Thread.Sleep(300);

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(Connector.UkmStringConnecting))
                    {
                        conn.Open();

                        string str = "c.id";

                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() {
                            if (TextboxAddBar.Text.Length == 5)
                                str = "a.id";
                        }));


                        MySqlCommand cmd = new MySqlCommand(@"SELECT a.name, b.price
                FROM trm_in_var C
                LEFT JOIN trm_in_items A ON A.id=C.item
                LEFT JOIN trm_in_pricelist_items B ON B.item=c.item
                WHERE " + str + "='" + TextboxAddBar.Text +
                        "' AND (b.pricelist_id=" + pricelistId + ")", conn);

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr == null)
                            {
                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() {
                                    DeclineErr(true, "                             Запрос ничего не вернул,повторите попытку!");
                                    return;
                                }));
                            }

                            if (!dr.HasRows)
                            {
                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() {
                                    DeclineErr(true, "                                                Штрихкод не найден в базе!");
                                    TextboxAddBar.Text = "";
                                    TextboxCountAdd.Text = "";
                                    TextboxNameItem.Text = "";
                                    TextboxPrice.Text = "";
                                    TextboxAddBar.Focus();
                                    return;
                                }));
                            }

                            if (dr.Read())
                            {
                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() {
                                    TextboxNameItem.Text = dr.GetString(0);
                                    string[] split_data = dr.GetString(1).Split(new Char[] { ',' });
                                    TextboxPrice.Text = Convert.ToInt32(split_data[0]).ToString();
                                }));
                            }

                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate()
                            {
                                TextboxCountAdd.Focus();
                                PanelAddBg.BackColor = Color.DodgerBlue;
                            }));
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { DeclineErr(true, "                             Не удалось соединиться с Mysql сервером!"); }));

                    Log.ExcWrite("[getname]" + ex.Message);
                }
            };

            Task t = new Task(action, "getname");
            t.Start();
        }

        private void getname(string tempBar)
        {
            PanelAddBg.BackColor = Color.Yellow;

            Action<object> action = (object obj) =>
            {
                Thread.Sleep(300);

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(Connector.UkmStringConnecting))
                    {
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { TextboxAddBar.Text = tempBar; }));

                        conn.Open();

                        string str = "c.id";

                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() {
                            if (TextboxAddBar.Text.Length == 5)
                                str = "a.id";
                        }));

                        MySqlCommand cmd = new MySqlCommand(@"SELECT a.name, b.price
                FROM trm_in_var C
                LEFT JOIN trm_in_items A ON A.id=C.item
                LEFT JOIN trm_in_pricelist_items B ON B.item=c.item
                WHERE " + str + "='" + tempBar +
                        "' AND (b.pricelist_id=" + pricelistId + ")", conn);

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr == null)
                            {
                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate()
                                {
                                    DeclineErr(true, "                             Запрос ничего не вернул,повторите попытку!");
                                    return;
                                }));
                            }

                            if (!dr.HasRows)
                            {
                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate()
                                {
                                    DeclineErr(true, "                                                Штрихкод не найден в базе!");
                                    TextboxAddBar.Text = "";
                                    TextboxCountAdd.Text = "";
                                    TextboxNameItem.Text = "";
                                    TextboxPrice.Text = "";
                                    TextboxAddBar.Focus();
                                    return;
                                }));
                            }

                            if (dr.Read())
                            {
                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate()
                                {
                                    TextboxNameItem.Text = dr.GetString(0);
                                    string[] split_data = dr.GetString(1).Split(new Char[] { ',' });
                                    TextboxPrice.Text = Convert.ToInt32(split_data[0]).ToString();
                                }));
                            }

                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() {
                                TextboxCountAdd.Focus();
                                PanelAddBg.BackColor = Color.DodgerBlue;
                            }));
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { DeclineErr(true, "                             Не удалось соединиться с Mysql сервером!"); }));
                    Log.ExcWrite("[getname]" + ex.Message);
                }
            };

            Task t = new Task(action, "SendBar");
            t.Start();
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            DataTimeLabel.ForeColor = Color.Green;
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            DataTimeLabel.ForeColor = Color.DodgerBlue;
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        PanelAddHide(true);
                        ButtonAdd.Focus();
                        break;
                    }

                case Keys.Enter:
                    {
                        TextboxAddBar.Focus();
                        break;
                    }
            }
        }

        //END DateTimeAction

        //START TextboxCountAction

        private void TextboxCountAdd_Enter(object sender, EventArgs e)
        {
            LabelCountAdd.ForeColor = Color.Green;
        }

        private void TextboxCountAdd_Leave(object sender, EventArgs e)
        {
            TextboxCountAdd.Text = TextboxCountAdd.Text.Replace(".", ",");
            LabelCountAdd.ForeColor = Color.DodgerBlue;
        }

        private void TextboxCountAdd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        PanelAddBg.Visible = false;
                        PanelAddTask.Visible = false;
                        ButtonAdd.Focus();
                        break;
                    }
                case Keys.Left:
                    {
                        TextboxAddBar.Text = "";
                        TextboxCountAdd.Text = "";
                        TextboxNameItem.Text = "";
                        TextboxPrice.Text = "";
                        TextboxAddBar.Focus();
                        break;
                    }
                case Keys.Right:
                    TextboxPrice.Focus();
                    break;
                case Keys.Enter:
                case Keys.Down:
                    {
                        ButtonTurn.Focus();
                        break;
                    }
            }
        }

        private void ButtonTurn_Enter(object sender, EventArgs e)
        {
            ButtonTurn.ForeColor = Color.Green;
            PanelAddBg.BackColor = Color.DodgerBlue;

            if (TextboxAddBar.Text.Length != 12)
                ButtonTurn.ForeColor = Color.Red;

            if (TextboxNameItem.Text.Length < 3)
                ButtonTurn.ForeColor = Color.Red;

            double count;

            if (!double.TryParse(TextboxCountAdd.Text, out count))
                ButtonTurn.ForeColor = Color.Red;
            else
                ButtonTurn.ForeColor = Color.Green;
        }

        private void ButtonTurn_Leave(object sender, EventArgs e)
        {
            ButtonTurn.ForeColor = Color.DodgerBlue;
            PanelAddBg.BackColor = Color.DodgerBlue;
        }

        private void ButtonTurn_Click(object sender, EventArgs e)
        {
            double count;

            if (TextboxAddBar.Text.Length < 5)
            {
                DeclineErr(true, "                                          Баркод должен быть более 4 цифр!");
                TextboxAddBar.Focus();
                return;
            }

            if (TextboxNameItem.Text.Length < 3)
            {
                DeclineErr(true, "                  Внимание!Не выбран товар для постановки в очередь!");
                TextboxAddBar.Focus();
                return;
            }

            if (!Double.TryParse(TextboxCountAdd.Text, out count))
            {
                    DeclineErr(true, "                      Внимание!В поле количество должно быть число!");
                    TextboxCountAdd.Focus();
                    return;
            }

            if (TextboxPrice.Text.Length < 2)
            {
                DeclineErr(true, "                      Внимание!Цена должна быть более 3 символов!");
                TextboxPrice.Focus();
                return;
            }

            ButtonTurn.Enabled = false;


            Application.DoEvents();
            Server.Connect();

            if (Server.server.Connected)
            {
                DateTime datetime = new DateTime();

                datetime = Convert.ToDateTime(dateTimePicker1.Text);

                Server.Sender("PrioritySale", 5, TextboxAddBar.Text + ";" + TextboxCountAdd.Text + ";" + TextboxPrice.Text + ";" + datetime + ";" + TextboxNameItem.Text);
            }

            //TIMER TO ENABLED BUTTON
//             int i = 0;
// 
//             Application.DoEvents();
// 
//             while (ButtonTurn.Enabled == false)
//             {
//                 i++;
//                 Thread.Sleep(1000);
// 
//                 if (i > 30)
//                     ButtonTurn.Enabled = true;
//             }
        }

        private void TimerClearMsg_Tick(object sender, EventArgs e)
        {
            LabelInfo.Text = "";
            TimerClearMsg.Enabled = false;
            ButtonTurn.ForeColor = Color.DodgerBlue;
            PanelAddBg.BackColor = Color.DodgerBlue;
            LabelInfo.ForeColor = Color.Red;
        }

        private void TextboxAddBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true)
            {
                e.Handled = true;
            }
        }

        public void DeclineErr(bool st,string txt)
        {
            TimerClearMsg.Enabled = false;

            if(st)
            {
                PanelAddBg.BackColor = Color.Red;
                ButtonTurn.ForeColor = Color.Red;
                LabelInfo.ForeColor = Color.Red;
                LabelInfo.Text = txt;
                TimerClearMsg.Enabled = true;
                return;
            }
            else
            {
                PanelAddBg.BackColor = Color.Green;
                ButtonTurn.ForeColor = Color.Green;
                LabelInfo.ForeColor = Color.Green;
                LabelInfo.Text = txt;
                TimerClearMsg.Enabled = true;
                return;
            }
        }

        private void PanelAddHide(bool st)
        {
            if (st)
            {
                PanelAddBg.Visible = false;
                PanelAddTask.Visible = false;
            }
            else
            {
                PanelAddBg.Visible = true;
                PanelAddTask.Visible = true;
            }

            TextboxPrice.Text = "";
            TextboxAddBar.Text = "";
            TextboxCountAdd.Text = "";
            TextboxNameItem.Text = "";
            PanelAddBg.BackColor = Color.DodgerBlue;
            ButtonTurn.ForeColor = Color.DodgerBlue;
            TimerClearMsg.Enabled = false;
            LabelInfo.Text = "";
        }
        private void ButtonTurn_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    PanelAddHide(true);
                    ButtonAdd.Focus();
                    break;
                case Keys.Up:
                    TextboxCountAdd.Text = "";
                    TextboxCountAdd.Focus();
                    break;
            }
        }

        private void TextboxPrice_Enter(object sender, EventArgs e)
        {
            LabelPrice.ForeColor = Color.Green;
        }

        private void TextboxPrice_Leave(object sender, EventArgs e)
        {
            LabelPrice.ForeColor = Color.DodgerBlue;
        }

        private void TextboxPrice_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        PanelAddHide(true);
                        ButtonAdd.Focus();
                        break;
                    }
                case Keys.Left:
                    TextboxCountAdd.Focus();
                    break;
                case Keys.Right:
                case Keys.Enter:
                    {
                        if (TextboxPrice.Text.Length < 3)
                        {
                            DeclineErr(true, "                                          Цена должна быть более 3 цифр!");
                            LabelInfo.ForeColor = Color.Yellow;
                            TextboxPrice.Focus();
                            return;
                        }

                        int i;

                        if (Int32.TryParse(TextboxPrice.Text,out i))
                            ButtonTurn.Focus();
                        else
                        {                                                        
                            DeclineErr(true, "                                          В поле цена могут быть только цифры!");
                            LabelInfo.ForeColor = Color.Yellow;
                            TextboxPrice.Focus();
                            return;
                        }
                        break;
                    }
            }
        }

        //
        // MainForm MOVE BlockStart
        //
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
        private void form_move(MouseEventArgs e, int xa, int ya)
        {

            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X - xa, mouseOffset.Y - ya);
                Location = mousePos;
                tasks.Location = new System.Drawing.Point((mousePos.X + this.Size.Width + 1), (mousePos.Y));

                // Location = new System.Drawing.Point((Packages.mf.Location.X + 40), (Packages.mf.Location.Y + 90));
            }
        }
        private void PanelMainClassic_MouseDown(object sender, MouseEventArgs e)
        {
            form_MouseDown(e);
        }
        private void PanelMainClassic_MouseMove(object sender, MouseEventArgs e)
        {
            form_move(e, 0, 0);
        }
        private void PanelMainClassic_MouseUp(object sender, MouseEventArgs e)
        {
            form_MouseUp();
        }
        private void PanelButton_MouseDown(object sender, MouseEventArgs e)
        {
            form_MouseDown(e);
        }
        private void PanelButton_MouseMove(object sender, MouseEventArgs e)
        {
            form_move(e, PanelButton.Location.X + PanelBackButton.Location.X, PanelButton.Location.Y + PanelBackButton.Location.Y);
        }
        private void PanelButton_MouseUp(object sender, MouseEventArgs e)
        {
            form_MouseUp();
        }
        private void PanelMainBlock_MouseDown(object sender, MouseEventArgs e)
        {
            form_MouseDown(e);
        }
        private void PanelMainBlock_MouseMove(object sender, MouseEventArgs e)
        {
            form_move(e, PanelMainBlock.Location.X + PanelBackMain.Location.X, PanelMainBlock.Location.Y + PanelBackMain.Location.Y);
        }
        private void PanelMainBlock_MouseUp(object sender, MouseEventArgs e)
        {
            form_MouseUp();
        }
        private void PanelInfoBar_MouseDown(object sender, MouseEventArgs e)
        {
            form_MouseDown(e);
        }
        private void PanelInfoBar_MouseMove(object sender, MouseEventArgs e)
        {
            form_move(e, PanelInfoBar.Location.X + PanelBackInfoBar.Location.X, PanelInfoBar.Location.Y + PanelBackInfoBar.Location.Y);
        }
        private void PanelInfoBar_MouseUp(object sender, MouseEventArgs e)
        {
            form_MouseUp();
        }
        private void LabelUserName_MouseDown(object sender, MouseEventArgs e)
        {
            form_MouseDown(e);
        }
        private void LabelUserName_MouseMove(object sender, MouseEventArgs e)
        {
            form_move(e, PanelInfoBar.Location.X + PanelBackInfoBar.Location.X + LabelUserName.Location.X, PanelInfoBar.Location.Y + PanelBackInfoBar.Location.Y + LabelUserName.Location.Y);
        }
        private void LabelUserName_MouseUp(object sender, MouseEventArgs e)
        {
            form_MouseUp();
        }
        //
        // MainForm MOVE BlockEnd
        //

        private void ButtonList_Click(object sender, EventArgs e)
        {
            if (dataGridViewMainForm.Visible == false)
            {
                dataGridViewMainForm.Visible = true;
                LabelVersionBd.Visible = true;
                Server.Sender("PrioritySale", 9,StatusUpdate);

                if (!Packages.mf.PanelAddBg.Visible)
                    (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { Packages.mf.dataGridViewMainForm.Focus(); }));
            }
            else
            {
                LabelVersionBd.Visible = false;
                dataGridViewMainForm.Visible = false;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        ButtonList.Focus();
                        dataGridViewMainForm.Visible = false;
                        LabelVersionBd.Visible = false;
                        break;
                    }
                case Keys.Up:
                    {
                        if (dataGridViewMainForm.Rows.GetFirstRow(DataGridViewElementStates.Selected) == 0)
                            ButtonList.Focus();
                        break;
                    }
                case Keys.ControlKey:
                    Point pCell = dataGridViewMainForm.GetCellDisplayRectangle(dataGridViewMainForm.CurrentCell.ColumnIndex, dataGridViewMainForm.CurrentCell.RowIndex, true).Location;

                    MenuStripDataGrid.Show(pCell.X + Packages.mf.Location.X + dataGridViewMainForm.Size.Width/4, pCell.Y + Packages.mf.Location.Y + 100);

                    MenuStripDataGrid.Items[0].Select();
                    break;
            }
        }

        private void ButtonAdd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    ButtonAdd_Click(sender, e);
                    break;
                case Keys.Escape:
                    UpdateOrderHide();
                    break;
            }
        }

        private void ButtonAdd_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void ButtonList_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void ButtonList_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridViewMainForm.Visible)
            {
                dataGridViewMainForm.Focus();
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Down:
                    ButtonList_Click(sender, e);
                    break;
                case Keys.Escape:
                    UpdateOrderHide();
                    break;
            }
        }

        private void ButtonMsg_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void ButtonMsg_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    ButtonMsg_Click(sender, e);
                    break;
                case Keys.Escape:
                    UpdateOrderHide();
                    break;
            }
        }

        private void ButtonMsg_Click(object sender, EventArgs e)
        {
            if (!Packages.mf.Controls.Contains(MainFormClassic.msgdesk))
            {
                Packages.mf.Controls.Add(MainFormClassic.msgdesk);

                MainFormClassic.msgdesk.BringToFront();

                MainFormClassic.msgdesk.Show();

                MainFormClassic.msgdesk.Focus();

                MainFormClassic.msgdesk.TextBoxMessage.Focus();
            }
            else
            {
                MainFormClassic.msgdesk.Hide();

                Packages.mf.Controls.Remove(MainFormClassic.msgdesk);
            }
        }

        private void MainFormClassic_Load(object sender, EventArgs e)
        {
            if (tasks.Visible)
            {
                tasks.Activate();
            }
        }

        private void ButtonHide_Click(object sender, EventArgs e)
        {
            UpdateOrderHide();
        }

        public void UpdateOrderHide()
        {
            PrioritySalesIcon.Visible = true;
            this.Hide();
            tasks.Hide();
        }

        private void PrioritySales_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UpdateOrderShow();
        }

        public void UpdateOrderShow()
        {
            Application.DoEvents();

            PrioritySalesIcon.Visible = false;
            Packages.mf.Show();
            tasks.Location = new System.Drawing.Point((this.Location.X + this.Size.Width) + 1, (this.Location.Y));
            tasks.Show();
            tasks.Refresh();

            Action<object> action = (object obj) =>
            {
                tasks.UpdateDataGrid();
                tasks.UpdateDataGridAcceptedTasks();
            };

            Task t = new Task(action, "UpdateData");
            t.Start();

            TimerIconChange.Enabled = false;

            var icon1 = Properties.Resources.logo;

            PrioritySalesIcon.Icon = icon1;

            ButtonHide.Focus();
        }

        private void MainFormClassic_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void TimerIconChange_Tick(object sender, EventArgs e)
        {
            var icon1 = Properties.Resources.logo;
            var icon2 = Properties.Resources.logo2;

            _forBlinking = !_forBlinking;

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { Packages.mf.PrioritySalesIcon.Icon = _forBlinking ? icon1 : icon2; }));

            if (Packages.careform.LabelCareForm.ForeColor == Color.DodgerBlue)
                Packages.careform.LabelCareForm.ForeColor = Color.Yellow;
            else
                Packages.careform.LabelCareForm.ForeColor = Color.DodgerBlue;
        }

        private void dataGridViewMainForm_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

//             switch (Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].Cells[6].Value.ToString())
//             {
//                 case "0":
//                     Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.DodgerBlue;
//                     break;
//                 case "1":
//                     Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.Olive;
//                     break;
//                 case "2":
//                     Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.Orange;
//                     break;
//                 case "3":
//                     Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.OrangeRed;
//                     break;
//                 case "4":
//                     Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.Red;
//                     break;
//                 case "5":
//                     Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.DarkRed;
//                     break;
//             }
        }

        private void dataGridViewMainForm_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            switch (Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].Cells[6].Value.ToString())
            {
                case "0":
                    Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DodgerBlue;
                    break;
                case "1":
                    Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Olive;
                    break;
                case "2":
                    Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Orange;
                    break;
                case "3":
                    Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.OrangeRed;
                    break;
                case "4":
                    Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                    break;
                case "5":
                    Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                    break;
                case "6":
                    Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.RosyBrown;     // Товар продается дороже цены очерёдности , завышение цены.
                    break;
                case "7":
                    Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.OrangeRed;     // Цена на кассе больше цены очередности
                    break;
                case "8":
                    Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.PaleVioletRed;  //Цена на кассе меньше цены очередности
                    break;
                case "99":
                    Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Salmon;  //товар на подходе
                    break;
            }
        }

        private void dataGridViewMainForm_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

            switch (Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].Cells[6].Value.ToString())
            {
                case "0":
                    Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.DodgerBlue;
                    break;
                case "1":
                    Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.Olive;
                    break;
                case "2":
                    Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.Orange;
                    break;
                case "3":
                    Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.OrangeRed;
                    break;
                case "4":
                    Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.Red;
                    break;
                case "5":
                    Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.DarkRed;
                    break;
                case "6":
                    Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.RosyBrown;     // Товар продается дороже цены очерёдности , завышение цены.
                    break;
                case "7":
                    Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.OrangeRed;     // Цена на кассе больше цены очередности
                    break;
                case "8":
                    Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.PaleVioletRed;  //Цена на кассе меньше цены очередности
                    break;
                case "99":
                    Packages.mf.dataGridViewMainForm.RowsDefaultCellStyle.SelectionForeColor = Color.PaleVioletRed;  //товар на подходе
                    break;
            }
        }

        private void dataGridViewMainForm_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Packages.mf.dataGridViewMainForm.Rows[e.RowIndex].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void ButtonTasks_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    UpdateOrderHide();
                    break;
                case Keys.Down:
                case Keys.Enter:
                    {
                        ButtonTasks.ForeColor = Color.DodgerBlue;
                        tasks.DataGridViewTasks.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Green;

                        if (tasks.Visible)
                        {
                            if (tasks.DataGridViewTasks.Rows.Count == 0)
                            {
                                if (tasks.DataGridViewAccepted.Rows.Count > 0)
                                {
                                    tasks.DataGridViewTasks.DefaultCellStyle.SelectionForeColor = Color.DodgerBlue;
                                    tasks.DataGridViewAccepted.DefaultCellStyle.SelectionForeColor = Color.ForestGreen;
                                    tasks.DataGridViewAccepted.Focus();
                                    return;
                                }
                                else
                                {
                                    tasks.Hide();
                                    ButtonTasks.ForeColor = Color.Green;
                                    return;
                                }
                            }

                            tasks.DataGridViewTasks.Focus();
                        }
                        else
                        {
                            ButtonTasks_Click(sender, e);
                        }
                        break;
                    }
            }
        }

        private void ButtonTasks_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Enter:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void ButtonTasks_Click(object sender, EventArgs e)
        {
            if (tasks.Visible)
            {
                tasks.Hide();
            }
            else
            {
                tasks.Location = new System.Drawing.Point((this.Location.X + this.Size.Width) + 1, (this.Location.Y));
                tasks.DataGridViewTasks.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Green;
                tasks.Show();
                tasks.UpdateDataGrid();
                tasks.UpdateDataGridAcceptedTasks();
                tasks.DataGridViewTasks.Focus();

                if (tasks.DataGridViewTasks.Rows.Count == 0)
                {
                    ButtonTasks.ForeColor = Color.Green;
                    ButtonTasks.Focus();
                    return;
                }
            }
        }

        private void dataGridViewMainForm_Enter(object sender, EventArgs e)
        {
            Thread.Sleep(100);

            if (dataGridViewMainForm.Rows.Count == 0)
                Packages.mf.ButtonList.Focus();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UserGroup == 0)
                return;

            try
            {
                int _index = dataGridViewMainForm.Rows.GetFirstRow(DataGridViewElementStates.Selected);

                string _priorityid = dataGridViewMainForm.Rows[_index].Cells[0].Value.ToString();

                if (UserGroup > 1)
                {
                    DeleteFromPriority(_priorityid);
                }
                else
                {
                    CheckDeleteFromPriority(_priorityid);
                }

            }
            catch (System.Exception ex) { Log.ExcWrite("[удалитьToolStripMenuItem_Click]" + ex.Message); }

        }

        private void DeleteFromPriority(string _priorityid)
        {
            string message = "Вы действительно хотите удалить строку с номером " + _priorityid + "?";
            string caption = "Удаление";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Connector.ExecuteNonQuery("DELETE FROM `priority` WHERE `id` = " + _priorityid + ";DELETE FROM `priority` WHERE `id` = " + _priorityid + ";DELETE FROM `sendpos` WHERE `id` = " + _priorityid + "; DELETE FROM `tasks` WHERE `priority_id` = " + _priorityid + ";");

                Server.Sender("PrioritySale", 6, "Удаление строки.");
            }
        }

        private void CheckDeleteFromPriority(string id)
        {
            MessageBox.Show("Уровень вашего доступа не позволяет удалять вам записи!");
        }

        private void LabelUserName_TextChanged(object sender, EventArgs e)
        {
            UserName = Packages.mf.LabelUserName.Text.Replace("Пользователь:  ", "");

            GetUserIdAndGroup();
        }

        private void подробноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Packages.mf.Controls.Contains(MainFormClassic.prioritydetails))
            {
                Packages.mf.Controls.Add(MainFormClassic.prioritydetails);

                MainFormClassic.prioritydetails.BringToFront();

                MainFormClassic.prioritydetails.Show();

                MainFormClassic.prioritydetails.Focus();

                MainFormClassic.prioritydetails.dataGridViewMainForm.Focus();

                string index = dataGridViewMainForm.SelectedRows[0].Cells[0].Value.ToString();

                LoadPriorityDetails(index);
            }
        }

        private void LoadPriorityDetails(string index)
        {
            using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(@"SELECT dok,d_vv,k_op,n_mat,n_cenu,n_sum,n_matost,n_izg,ndsp,n_tn FROM `movement` WHERE `id` = " + index + " ORDER BY `d_vv`", conn);
                cmd.CommandTimeout = 0;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr == null) { return; }

                    if (!dr.HasRows) { }

                    while (dr.Read())
                    {
                        Application.DoEvents();

                        if (dr.GetString(0) == "0" || dr.GetString(1) == "0")
                            continue;

                        string dok = dr.GetString(0);
                        string d_vv = dr.GetString(1);
                        string k_op = dr.GetString(2);
                        string k_mat = dr.GetString(3);
                        string n_cenu = dr.GetString(4);
                        string n_sum = dr.GetString(5);
                        string n_matost = dr.GetString(6);
                        string n_izg = dr.GetString(7);
                        string ndsp = dr.GetString(8);
                        string n_tn = dr.GetString(9);
                                                                                                                                                        // 1   2     3     4       5       6       7      8      9
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { prioritydetails.dataGridViewMainForm.Rows.Add(dok, d_vv, k_op, k_mat, n_cenu, n_matost, n_izg, ndsp, n_tn); }));
                    }
                }
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implement! lolz!");
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dataGridViewMainForm.SelectedRows[0].Cells[1].Value.ToString());
        }

        private void ButtonLog_Click(object sender, EventArgs e)
        {
            if (!Packages.mf.Controls.Contains(MainFormClassic.logform))
            {
                Packages.mf.Controls.Add(MainFormClassic.logform);

                MainFormClassic.logform.BringToFront();

                MainFormClassic.logform.Show();

                MainFormClassic.logform.Focus();

                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.logform.listBox1.Items.Clear(); }));

                Action<object> action = (object obj) =>
                {
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                        {
                            conn.Open();

                            MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM `log` ", conn);
                            cmd.CommandTimeout = 0;

                            using (MySqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr == null || !dr.HasRows)
                                {
                                    (Application.OpenForms[1] as AuthFormClassic).BeginInvoke((MethodInvoker)(delegate() { MainFormClassic.logform.listBox1.Items.Add("Записи не обнаружены!"); }));
                                }
                                while (dr.Read())
                                {
                                    Application.DoEvents();
                                    (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.logform.listBox1.Items.Add(dr.GetValue(1)); }));
                                }
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("[ButtonLog_Click]" + exc.Message);
                        Log.ExcWrite("[ButtonLog_Click]" + exc.Message);
                    }
                };


                Task t = new Task(action, "ButtonLog_ClickQuery");
                t.Start();
            }
            else
            {
                MainFormClassic.logform.Hide();

                Packages.mf.Controls.Remove(MainFormClassic.logform);
            }
        }

        private void ButtonLog_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    UpdateOrderHide();
                    break;
            }
        }

        private void ButtonSetting_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    UpdateOrderHide();
                    break;
            }
        }

        private void ButtonHide_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    UpdateOrderHide();
                    break;
            }
        }

        private void ButtonExit_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    UpdateOrderHide();
                    break;
            }
        }
    }
}
