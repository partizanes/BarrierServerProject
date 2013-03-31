using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PrioritySales
{
    public partial class CareForm : Form
    {
        public int xOffset, yOffset;
        public bool isMouseDown = false;
        private Point mouseOffset;

        public CareForm()
        {
            InitializeComponent();
        }

        private void TimerChangeColor_Tick(object sender, EventArgs e)
        {
            if (LabelCareForm.ForeColor == Color.DodgerBlue)
                LabelCareForm.ForeColor = Color.Yellow;
            else
                LabelCareForm.ForeColor = Color.DodgerBlue;
        }

        private void LabelCareForm_DoubleClick(object sender, EventArgs e)
        {
            DoubleClickForm();
        }
        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            DoubleClickForm();
        }

        private void DoubleClickForm()
        {
            if (!Packages.mf.Visible)
            {
                Packages.mf.UpdateOrderShow();
                Packages.mf.TopLevel = true;
                LabelCareForm.Visible = false;
            }
            else
            {
                Packages.mf.UpdateOrderHide();
                Packages.mf.TopLevel = true;
                CheckStatusTask();
            }
        }

        private void CheckStatusTask()
        {         
            try
            {
                using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", "barrierserver")))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("SELECT COUNT(id) FROM tasks WHERE `group` = (SELECT `group` FROM `users` WHERE `username` = '"+ Packages.mf.LabelUserName.Text.Replace("Пользователь:  ","")+"') AND `user_id` = 0", conn);

                    int i = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr == null)
                            return;

                        if (dr.Read())
                            i = dr.GetInt32(0);
                    }

                    if (i > 0)
                        LabelCareForm.Visible = true;
                    else
                    {
                        Packages.mf.TimerIconChange.Enabled = false;
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { Packages.mf.PrioritySalesIcon.Icon = Properties.Resources.logo; }));
                    }
                }
            }
            catch{}

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }

            Packages.Zx = Location.X;
            Packages.Zy = Location.Y;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                xOffset = -e.X;
                yOffset = -e.Y;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        private void CareForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
                Packages.Zx = Location.X;
                Packages.Zy = Location.Y;
            }
        }

        private void CareForm_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void CareForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        private void LabelCareForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                xOffset = -e.X - this.LabelCareForm.Location.X;
                yOffset = -e.Y - this.LabelCareForm.Location.Y;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        private void LabelCareForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
                Packages.Zx = Location.X;
                Packages.Zy = Location.Y;
            }
        }

        private void LabelCareForm_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }
    }
}
