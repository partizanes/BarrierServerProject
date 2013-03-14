﻿using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace PrioritySales
{
    public partial class Tasks : Form
    {
        public Tasks()
        {
            InitializeComponent();
        }

        private void DataGridViewTasks_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Hide();
                    Packages.mf.Activate();
                    break;   
            }
        }

        private void Tasks_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Hide();
                    Packages.mf.Activate();
                    break;
                case Keys.Left:
                case Keys.Up:
                    Packages.mf.Activate();
                    break;
            }
        }

        public void UpdateDataGrid()
        {
            using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "PrioritySailR", "***REMOVED***", "barrierserver")))
            {
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.tasks.DataGridViewTasks.Rows.Clear(); }));

                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT id,text FROM tasks WHERE `group` = (SELECT `group` FROM users WHERE `username` = '" + Packages.mf.LabelUserName.Text.Replace("Пользователь:  ","") + "') AND `user_id` = 0 ORDER BY priority DESC", conn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                        (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.tasks.DataGridViewTasks.Rows.Add(dr.GetString(0), dr.GetString(1)); }));

                if (!dr.IsClosed)
                    dr.Close();
                }
            }
        }
    }
}
