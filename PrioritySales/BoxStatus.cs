using System;
using System.Drawing;
using System.Windows.Forms;

namespace PrioritySales
{
    public partial class BoxStatus : Form
    {
        public BoxStatus()
        {
            InitializeComponent();
        }

        private void BoxStatus_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int i = 0;
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    try { i = int.Parse(textBox1.Text); }
                    catch { MessageBox.Show("Неверный приоритет сообщения!"); return; }

                    switch (MainFormClassic.UserGroup)
                    {
                        case 1:
                            if (i < 4)
                                AddMessageToBase(i);
                            else
                                MessageBox.Show("Вы не имеете права отправлять сообщение с статусом выше 3.");
                            break;
                        case 2:
                            AddMessageToBase(i);
                            break;
                    default:
                            MessageBox.Show("Вы не имеете права отправлять сообщения");
                            break;
                    }
                    break;
                case Keys.Escape:             
                    this.Hide();
                    break;
            }
        }

        private void AddMessageToBase(int priority)
        {
            string message = MainFormClassic.msgdesk.TextBoxMessage.Text;
            string msg_color = MainFormClassic.msgdesk.TextBoxMessage.ForeColor.Name.ToString();
            string datetime = DateTime.Now.ToString("yyyy-MM-dd,HH:mm:ss");
            Color color = Color.FromName(msg_color);

            if (message.Length < 5 || msg_color.Length < 2 || MainFormClassic.UserName.Length < 3)
                return;

            if (Connector.ExecuteNonQuery("INSERT IGNORE INTO `message`(`userid`,`msg_priority`,`msg_color`,`msg`,`msg_datetime`) VALUES ( (SELECT id FROM users WHERE username = '" + MainFormClassic.UserName + "'),'" + priority + "','" + msg_color + "','" + message + "','" + datetime + "')"))
            {
                MainFormClassic.msgdesk.TextBoxMessage.Text = "";
                MainFormClassic.msgdesk.TextBoxMessage.ForeColor = Color.Silver;

                if (Server.server.Connected)
                    Server.Sender("PrioritySale", 7, MainFormClassic.StatusUpdate);
            }

            this.Hide();
            this.textBox1.Text = "";
        }
    }
}
