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

                        if (!dr.IsClosed)
                            dr.Close();
                    }

                    if (i > 0)
                        LabelCareForm.Visible = true;
                    else
                    {
                        Packages.mf.TimerIconChange.Enabled = false;
                        (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { Packages.mf.PrioritySalesIcon.Icon = Properties.Resources.logo; }));
                    }
                }
            }
            catch{}

        }
    }
}
