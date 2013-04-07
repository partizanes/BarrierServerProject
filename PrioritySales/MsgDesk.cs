using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PrioritySales
{
    public partial class MsgDesk : UserControl
    {
        private BoxStatus boxstatus = new BoxStatus();

        public MsgDesk()
        {
            InitializeComponent();
        }

        private void MsgDesk_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(17, 78);
        }

        private void TextBoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 18 && MainFormClassic.msgdesk.ListViewMsg.Items.Count > 0)
            {
                MainFormClassic.msgdesk.ListViewMsg.Items[MainFormClassic.msgdesk.ListViewMsg.Items.Count - 1].Selected = true;

                MainFormClassic.msgdesk.ListViewMsg.Items[MainFormClassic.msgdesk.ListViewMsg.Items.Count - 1].Focused = true;

                MainFormClassic.msgdesk.ListViewMsg.Focus();
            }

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    boxstatus.Location = new Point(Packages.mf.Location.X + Packages.mf.Size.Width / 3 + 50, Packages.mf.Location.Y + 370);
                    boxstatus.Show();
                    break;
                case Keys.Escape:
                case Keys.Up:
                    MainFormClassic.msgdesk.Hide();
                    Packages.mf.Controls.Remove(MainFormClassic.msgdesk);
                    Packages.mf.ButtonMsg.Focus();
                    break;
                case Keys.ControlKey:
                    ColorDialog colorDlg = new ColorDialog();
                    colorDlg.AllowFullOpen = false;
                    colorDlg.AnyColor = false;
                    colorDlg.SolidColorOnly = true;

                    colorDlg.Color = TextBoxMessage.ForeColor;

                    if (colorDlg.ShowDialog() == DialogResult.OK)
                        TextBoxMessage.ForeColor = colorDlg.Color;
                    break;
            }
        }

        private void ListViewMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 18)
            {
                MainFormClassic.msgdesk.TextBoxMessage.Focus();
            }

            switch (e.KeyCode)
            {
                case Keys.ControlKey:

                    int firstIndex = MainFormClassic.msgdesk.ListViewMsg.SelectedIndices[0];

                    Point pCell = new Point(this.Location.X + Packages.mf.Location.X + MainFormClassic.msgdesk.ListViewMsg.Size.Width - 135, this.Location.Y + Packages.mf.Location.Y + firstIndex*18);

                    MenuMsgBoard.Show(pCell);

                    //MenuMsgBoard.Items[0].Select();
                    break;
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainFormClassic.UserGroup == 0)
                return;

            string _MsgId = GetMsgId();

            if (MainFormClassic.UserGroup > 1 || CheckOwner(_MsgId))
                DeleteFromMsg(_MsgId);
            else
                MessageBox.Show("Это сообщение вам не принадлежит.");
        }

        private string GetMsgId()
        {
            int _index = MainFormClassic.msgdesk.ListViewMsg.SelectedIndices[0];

            string[] split = MainFormClassic.msgdesk.ListViewMsg.Items[_index].Text.Split(new Char[] { ':' });

            return split[0];
        }

        private bool CheckOwner(string _MsgId)
        {
            int OwnerMsg = GetОwnerMsg(_MsgId);

            if (OwnerMsg == 0)
            {
                MessageBox.Show("Недостаточно прав для удаления.");
                return false;
            }

            if (OwnerMsg == MainFormClassic.UserId)
                return true;
            else
                return false;
        }

        private void DeleteFromMsg(string _MsgId)
        {
            string message = "Вы действительно хотите удалить сообщение с номером " + _MsgId + "?";
            string caption = "Удаление";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Connector.ExecuteNonQuery("DELETE FROM `message` WHERE `msg_id` = " + _MsgId);

                Server.Sender("PrioritySale", 7, "Удаление сообщения с доски.");

                TextBoxMessage.Focus();
            }
        }

        private void РедактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainFormClassic.UserGroup == 0)
                return;

            string _MsgId = GetMsgId();

            if (MainFormClassic.UserGroup > 1 || CheckOwner(_MsgId))
                EditPanelLoad(_MsgId);
            else
                MessageBox.Show("Это сообщение вам не принадлежит.");

        }

        private int GetОwnerMsg(string _MsgId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("SELECT `userid` FROM `message` WHERE  `msg_id` = " + _MsgId, conn);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr == null)
                        {
                            MessageBox.Show("Недостаточно прав для удаления.");
                        }
                        if (dr.Read())
                        {
                            return dr.GetInt32(0);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Log.log_write("[CheckUserDeleteMsg]" + ex.Message, "EXCEPTION", "exception");
            }

            return 0;
        }

        private void EditPanelLoad(string _MsgId)
        {
            PanelEditMsgBack.Visible = true;


            
            CleanPanelEdited(_MsgId);

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("SELECT msg_priority,msg FROM `message` WHERE `msg_id` = " +_MsgId, conn);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr == null)
                        {
                            MessageBox.Show("Что-то пошло не так,повторите попытку.Так же можно обратиться к Администратору!");
                        }
                        if (dr.Read())
                        {
                            TextBoxEditedPriority.Text = dr.GetString(0);
                            TextboxMsgEdited.Text = dr.GetString(1);
                        }
                    }

                    TextboxMsgEdited.Focus();
                    TextboxMsgEdited.SelectionStart = TextboxMsgEdited.Text.Length - 1;
                }
            }
            catch (System.Exception ex)
            {
                Log.log_write("[CheckUserDeleteMsg]" + ex.Message, "EXCEPTION", "exception");
                MessageBox.Show("Что-то поломалось, сделана запись в лог файл . Попробуйте повторить.");
                PanelEditMsgBack.Visible = false;
            }
        }

        private void ButtonCancelChanges_Click(object sender, EventArgs e)
        {
            PanelEditMsgBack.Visible = false;

            TextBoxMessage.Focus();
        }

        private void CleanPanelEdited(string _MsgId)
        {
            LabelMsgIdEdited.Text = _MsgId;
            TextBoxEditedPriority.Text = "";
            TextboxMsgEdited.Text = "Загрузка...";
        }

        private void TextboxMsgEdited_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Enter:
                    ButtonAcceptChanges.Focus();
                    break;
                case Keys.Escape:
                    ButtonCancelChanges.PerformClick();
                    break;
            }
        }

        private void ButtonAcceptChanges_Click(object sender, EventArgs e)
        {
            try
            {
                string _MsgId = LabelMsgIdEdited.Text;
                string _MsgPriority = TextBoxEditedPriority.Text;
                string _MsgText = TextboxMsgEdited.Text;

                if (_MsgId.Length == 0 || _MsgPriority.Length == 0 || _MsgText.Length == 0)
                    throw new Exception("Что-то поломалось.Попробуйте еще раз.");

                Connector.ExecuteNonQuery("UPDATE `message` SET `msg_priority`='" + _MsgPriority + "',`msg`='" + _MsgText + "' WHERE `msg_id`='" + _MsgId + "'");

                Server.Sender("PrioritySale", 7, "Редактирование сообщения с доски.");

                PanelEditMsgBack.Visible = false;

                TextBoxMessage.Focus();
            }
            catch (System.Exception ex)
            {
                Log.log_write("[ButtonAcceptChanges_Click]" + ex.Message, "EXCEPTION", "exception");
                MessageBox.Show(ex.Message);
            }

        }

        private void ButtonAcceptChanges_Enter(object sender, EventArgs e)
        {
            ButtonAcceptChanges.ForeColor = Color.Green;
        }

        private void ButtonCancelChanges_Enter(object sender, EventArgs e)
        {
            ButtonCancelChanges.ForeColor = Color.Green;
        }

        private void ButtonAcceptChanges_Leave(object sender, EventArgs e)
        {
            ButtonAcceptChanges.ForeColor = Color.DodgerBlue;
        }

        private void ButtonCancelChanges_Leave(object sender, EventArgs e)
        {
            ButtonCancelChanges.ForeColor = Color.DodgerBlue;
        }

        private void TextBoxEditedPriority_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Down:
                    TextboxMsgEdited.Focus();
                    TextboxMsgEdited.SelectionStart = TextboxMsgEdited.Text.Length - 1;
                    break;
            }
        }

        private void TextBoxEditedPriority_TextChanged(object sender, EventArgs e)
        {
            try {
                if (TextBoxEditedPriority.Text == "")
                    return;

                if(MainFormClassic.UserGroup == 1 && (int.Parse(TextBoxEditedPriority.Text)) > 3)
                    throw new Exception("Максимальный для вас приоритет 3.");
            }
            catch (System.Exception ex)
            {
                Log.log_write("[TextBoxEditedPriority_TextChanged]" + ex.Message, "EXCEPTION", "exception");
                TextBoxEditedPriority.Text = ""; 
                MessageBox.Show(ex.Message); 
            }
            
        }
    }
}
