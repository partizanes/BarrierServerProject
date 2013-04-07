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

            int _index = MainFormClassic.msgdesk.ListViewMsg.SelectedIndices[0];

            string[] split = MainFormClassic.msgdesk.ListViewMsg.Items[_index].Text.Split(new Char[] { ':' });

            if (MainFormClassic.UserGroup > 1)
                DeleteFromMsg(split[0]);
            else
                CheckUserDeleteMsg(split[0]);
        }

        private void CheckUserDeleteMsg(string _MsgId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("SELECT `userid` FROM `message` WHERE  `msg_id` = " +_MsgId, conn);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr == null)
                        {
                            MessageBox.Show("Недостаточно прав для удаления.");
                        }
                        if (dr.Read())
                        {
                            if (dr.GetInt32(0) == MainFormClassic.UserId)
                                DeleteFromMsg(_MsgId);
                            else
                                MessageBox.Show("Недостаточно прав для удаления.");
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Log.log_write("[GetUserGroup]" + ex.Message, "EXCEPTION", "exception");
            }
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
            }
        }
    }
}
