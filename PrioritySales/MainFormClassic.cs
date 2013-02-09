using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrioritySales
{
    public partial class MainFormClassic : Form
    {
        public MainFormClassic()
        {
            InitializeComponent();
        }

        private bool nonNumberEntered = false;

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

        private void ButtonUnk_Enter(object sender, EventArgs e)
        {
            ButtonUnk.ForeColor = Color.Green;
        }

        private void ButtonUnk_Leave(object sender, EventArgs e)
        {
            ButtonUnk.ForeColor = Color.DodgerBlue;
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
                PanelAddBg.Visible = true;
                PanelAddTask.Visible = true;
                TextboxAddBar.Focus();
            }
            else
            {
                PanelAddBg.Visible = false;
                PanelAddTask.Visible = false;
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
                        break;
                    }
                case Keys.Up:
                    {
                        dateTimePicker1.Focus();
                        break;
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

                        TextboxCountAdd.Focus();
                        break;
                    }
            }

            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    if (e.KeyCode != Keys.Back)
                    {
                        nonNumberEntered = true;
                    }
                }
            }

            if (Control.ModifierKeys == Keys.Shift)
                nonNumberEntered = true;

            if (nonNumberEntered)
            {
                LabelInfo.ForeColor = Color.Yellow;
                DeclineErr(true, "                                  Баркод может состоять только из чисел!");
                return;
            }

        }

        //END TextboxBarAction

        //START DateTimeAction

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
                        PanelAddBg.Visible = false;
                        PanelAddTask.Visible = false;
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
                        TextboxAddBar.Focus();
                        break;
                    }
                case Keys.Right:
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
            
            int count;

            if (int.TryParse(TextboxCountAdd.Text, out count))
                ButtonTurn.ForeColor = Color.Red;
        }

        private void ButtonTurn_Leave(object sender, EventArgs e)
        {
            ButtonTurn.ForeColor = Color.DodgerBlue;
            PanelAddBg.BackColor = Color.DodgerBlue;
        }

        private void ButtonTurn_Click(object sender, EventArgs e)
        {
            int count;

            if (TextboxAddBar.Text.Length < 5)
            {
                DeclineErr(true, "                                          Баркод должен быть более 4 цифр!");
                TextboxAddBar.Focus();
            }

            if (TextboxNameItem.Text.Length < 3)
            {
                DeclineErr(true, "                  Внимание!Не выбран товар для постановки в очередь!");
                TextboxAddBar.Focus();
            }
            else if (int.TryParse(TextboxCountAdd.Text, out count))
            {
                    DeclineErr(true, "                      Внимание!В поле количество должно быть число!");
                    TextboxCountAdd.Focus();
            }
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

        private void DeclineErr(bool st,string txt)
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

            }


        }
    }
}
