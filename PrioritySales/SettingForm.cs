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
    public partial class SettingForm : Form
    {
        public int xOffset, yOffset;
        public bool isMouseDown = false;
        private Point mouseOffset;

        public SettingForm()
        {
            InitializeComponent();
        }

        private void ButtonSettingBack_Click(object sender, EventArgs e)
        {
            try
            {
                Config.Set("SETTINGS", "IpAuthServer", TextBoxIpAuthServer.Text);
                Config.Set("SETTINGS", "IpAgentServer", TextBoxlIpAgentServer.Text);
                Config.Set("SETTINGS", "IpCashServer", TextBoxIpCashServer.Text);

                Config.Set("SETTINGS", "PortAuthServer", TextBoxPortAuthServer.Text);
                Config.Set("SETTINGS", "PortAgentServer", TextBoxPortAgentServer.Text);
                Config.Set("SETTINGS", "PortCashServer", TextBoxPortCashServer.Text);

                Config.Set("SETTINGS", "SaveLastLogin", CheckBoxSaveLastUser.Checked.ToString());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Hide();
            AuthForm af = new AuthForm();
            af.Show();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            try
            {
                TextBoxIpAuthServer.Text = Config.GetParametr("IpAuthServer");
                TextBoxlIpAgentServer.Text = Config.GetParametr("IpAgentServer");
                TextBoxIpCashServer.Text = Config.GetParametr("IpCashServer");

                TextBoxPortAuthServer.Text = Config.GetParametr("PortAuthServer");
                TextBoxPortAgentServer.Text = Config.GetParametr("PortAgentServer");
                TextBoxPortCashServer.Text = Config.GetParametr("PortCashServer");

                Boolean st = false;

                if (bool.TryParse((Config.GetParametr("SaveLastLogin")), out st))
                {
                    if (st)
                    {
                        CheckBoxSaveLastUser.Checked = st;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //start move form
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
            }
        }
        private void SettingForm_MouseMove(object sender, MouseEventArgs e)
        {
            form_move(e, 0, 0);
        }
        private void SettingForm_MouseUp(object sender, MouseEventArgs e)
        {
            form_MouseUp();
        }
        private void SettingForm_MouseDown(object sender, MouseEventArgs e)
        {
            form_MouseDown(e);
        }
        //end move form

        private void TextBoxIpAuthServer_Enter(object sender, EventArgs e)
        {
            LabelIpAuthServer.ForeColor = Color.Lime;
        }

        private void TextBoxIpAuthServer_Leave(object sender, EventArgs e)
        {
            LabelIpAuthServer.ForeColor = Color.White;
        }

        private void TextBoxlIpAgentServer_Enter(object sender, EventArgs e)
        {
            LabelIpAgentServer.ForeColor = Color.Lime;
        }

        private void TextBoxlIpAgentServer_Leave(object sender, EventArgs e)
        {
            LabelIpAgentServer.ForeColor = Color.White;
        }

        private void TextBoxIpCashServer_Enter(object sender, EventArgs e)
        {
            LabelIpCashServer.ForeColor = Color.Lime;
        }

        private void TextBoxIpCashServer_Leave(object sender, EventArgs e)
        {
            LabelIpCashServer.ForeColor = Color.White;
        }

        private void TextBoxPortAuthServer_Enter(object sender, EventArgs e)
        {
            LabelPortAuthServer.ForeColor = Color.Lime;
        }

        private void TextBoxPortAuthServer_Leave(object sender, EventArgs e)
        {
            LabelPortAuthServer.ForeColor = Color.White;
        }

        private void TextBoxPortAgentServer_Enter(object sender, EventArgs e)
        {
            LabelPortAgentServer.ForeColor = Color.Lime;
        }

        private void TextBoxPortAgentServer_Leave(object sender, EventArgs e)
        {
            LabelPortAgentServer.ForeColor = Color.White;
        }

        private void TextBoxPortCashServer_Enter(object sender, EventArgs e)
        {
            LabelCashPortServer.ForeColor = Color.Lime;
        }

        private void TextBoxPortCashServer_Leave(object sender, EventArgs e)
        {
            LabelCashPortServer.ForeColor = Color.White;
        }

        private void CheckBoxSaveLastUser_Enter(object sender, EventArgs e)
        {
            CheckBoxSaveLastUser.ForeColor = Color.Lime;
        }

        private void CheckBoxSaveLastUser_Leave(object sender, EventArgs e)
        {
            CheckBoxSaveLastUser.ForeColor = Color.White;
        }

        private void ButtonSettingBack_Enter(object sender, EventArgs e)
        {
            ButtonSettingBack.ForeColor = Color.Lime;
        }

        private void ButtonSettingBack_Leave(object sender, EventArgs e)
        {
            ButtonSettingBack.ForeColor = Color.White;
        }


    }
}
