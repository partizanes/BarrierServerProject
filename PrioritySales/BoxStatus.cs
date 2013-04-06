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
            int i ;
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    try { i = int.Parse(textBox1.Text); }
                    catch { return; }
                    AddMessageToBase(i);
                    break;
                case Keys.Escape:             
                    this.Hide();
                    this.Dispose();
                    break;
            }
        }

        private void AddMessageToBase(int priority)
        {
            string message = Packages.mf.TextBoxMessage.Text;
            string msg_color = Packages.mf.TextBoxMessage.ForeColor.Name.ToString();
            string username = Packages.mf.LabelUserName.Text.Replace("Пользователь:  ", "");
            string datetime = DateTime.Now.ToString("yyyy-MM-dd,HH:mm:ss");
            Color color = Color.FromName(msg_color);

            if(message.Length < 5 || msg_color.Length < 2 || username.Length < 3)
                return;

            if (Packages.connector.ExecuteNonQuery("INSERT IGNORE INTO `message`(`userid`,`msg_priority`,`msg_color`,`msg`,`msg_datetime`) VALUES ( (SELECT id FROM users WHERE username = '" + username + "'),'" + priority + "','" + msg_color + "','" + message + "','" + datetime + "')"))
            {
                Packages.mf.TextBoxMessage.Text = "";
                Packages.mf.TextBoxMessage.ForeColor = Color.Silver;

                if (Server.server.Connected)
                    Server.Sender("PrioritySale", 7, MainFormClassic.StatusUpdate);

                this.Hide();
                this.Dispose();
            }
            else
            {
                this.Hide();
                this.Dispose();
            }
        }
    }
}
