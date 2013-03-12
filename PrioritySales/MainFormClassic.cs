using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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

        private bool nonNumberEntered = false;
        private int pricelistId = 1;

        public int xOffset, yOffset;
        public bool isMouseDown = false;
        private Point mouseOffset;
        public static string StatusUpdate;

        Tasks tasks = new Tasks();

        // ForeColor for all button block start

        private void MainFormClassic_Shown(object sender, EventArgs e)
        {
            ButtonAdd.Focus();
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

        private void button1_Enter(object sender, EventArgs e)
        {
            ButtonUnk2.ForeColor = Color.Green;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            ButtonUnk2.ForeColor = Color.DodgerBlue;
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
                    TextboxAddBar.Text = Clipboard.GetText();

                    if (TextboxAddBar.Text.Length == 12)
                    {
                        getname();
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
            MySql.Data.MySqlClient.MySqlDataReader dr = Mysql.ExecuteReader("SELECT a.name, b.price \n" +
                "FROM trm_in_var C \n" +
                "LEFT JOIN trm_in_items A ON A.id=C.item \n" +
                "LEFT JOIN trm_in_pricelist_items B ON B.item=c.item \n" +
                "WHERE a.id='" + TextboxAddBar.Text + "'\n" +
                " AND (b.pricelist_id=" + pricelistId + ")");

            if (dr == null)
            {
                DeclineErr(true, "                             Запрос ничего не вернул,повторите попытку!");
                return;
            }

            if (!dr.HasRows)
            {
                DeclineErr(true, "                                                Штрихкод не найден в базе!");
                TextboxAddBar.Text = "";
                TextboxCountAdd.Text = "";
                TextboxNameItem.Text = "";
                TextboxPrice.Text = "";
                TextboxAddBar.Focus();
                return;
            }

            if (dr.Read())
            {
                TextboxNameItem.Text = dr.GetString(0);
                string[] split_data = dr.GetString(1).Split(new Char[] { ',' });
                TextboxPrice.Text = Convert.ToInt32(split_data[0]).ToString();
            }


            TextboxCountAdd.Focus();
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

            Thread thh = new Thread(delegate()
            {
                Server.Connect();

                if (Server.server.Connected)
                {
                    DateTime datetime = new DateTime();

                    datetime = Convert.ToDateTime(dateTimePicker1.Text);

                    Server.Sender("PrioritySale", 5, TextboxAddBar.Text + ";" + TextboxCountAdd.Text + ";" + TextboxPrice.Text + ";" + datetime + ";" + TextboxNameItem.Text);
                }
            }); ;
            thh.Name = "Авторизация";
            Server.threads.Add(thh);
            thh.Start();

            //TODO TIMER TO ENABLED BUTTON
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
            if (dataGridView1.Visible == false)
            {
                dataGridView1.Visible = true;
                LabelVersionBd.Visible = true;
                Server.Sender("PrioritySale", 9,StatusUpdate);

                if (!Packages.mf.PanelAddBg.Visible)
                    (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { Packages.mf.dataGridView1.Focus(); }));
            }
            else
            {
                LabelVersionBd.Visible = false;
                dataGridView1.Visible = false;
            }
        }

        private void ButtonMsg_Click(object sender, EventArgs e)
        {
            if (PanelMsgBg.Visible == true)
                PanelMsgBg.Visible = false;
            else
                PanelMsgBg.Visible = true;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        ButtonList.Focus();
                        dataGridView1.Visible = false;
                        break;
                    }
                case Keys.Up:
                    {
                        if (dataGridView1.Rows.GetFirstRow(DataGridViewElementStates.Selected) == 0)
                            ButtonList.Focus();
                        break;
                    }
            }
        }

        private void ButtonAdd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    ButtonAdd_Click(sender, e);
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
            if (dataGridView1.Visible)
            {
                dataGridView1.Focus();
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Down:
                    ButtonList_Click(sender, e);
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
                tasks.Show();
            }
        }

        private void MainFormClassic_Activated(object sender, EventArgs e)
        {
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
            PrioritySales.Visible = true;
            this.Hide();
            tasks.Hide();
        }

        private void PrioritySales_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PrioritySales.Visible = false;
            Packages.mf.Show();
            tasks.Location = new System.Drawing.Point((this.Location.X + this.Size.Width) + 1, (this.Location.Y));
            tasks.Show();
            tasks.Refresh();
        }

        private void MainFormClassic_FormClosed(object sender, FormClosedEventArgs e)
        {
            PrioritySales.Visible = false;
        }

    }
}
