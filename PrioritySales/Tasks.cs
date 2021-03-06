﻿using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
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
                    Packages.mf.ButtonTasks.ForeColor = System.Drawing.Color.Green;
                    break;
                case Keys.Up:
                    {
                        if (DataGridViewTasks.Rows.GetFirstRow(DataGridViewElementStates.Selected) == 0)
                        {
                            Packages.mf.ButtonTasks.Focus();
                            Packages.mf.ButtonTasks.ForeColor = System.Drawing.Color.Green;
                            DataGridViewTasks.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.DodgerBlue;
                            MainFormClassic.infocontrol.Hide();
                            Packages.mf.Controls.Remove(MainFormClassic.infocontrol);
                            break;
                        }

                        if (DataGridViewTasks.Rows.Count == 0)
                        {
                            Packages.mf.ButtonTasks.Focus();
                            Packages.mf.ButtonTasks.ForeColor = System.Drawing.Color.Green;
                            return;
                        }

                        if (Packages.mf.Controls.Contains(MainFormClassic.infocontrol))
                            GetInfo(DataGridViewTasks.Rows[DataGridViewTasks.SelectedCells[0].RowIndex - 1].Cells[0].Value.ToString());

                        break;
                    }
                case Keys.Down:
                    {
                        if (DataGridViewTasks.Rows.Count == 0)
                        {
                            Packages.mf.ButtonTasks.Focus();
                            Packages.mf.ButtonTasks.ForeColor = System.Drawing.Color.Green;
                            return;
                        }

                        if (DataGridViewTasks.SelectedCells[0].RowIndex == DataGridViewTasks.RowCount - 1 && DataGridViewAccepted.Rows.Count > 0)
                        {
                            DataGridViewTasks.DefaultCellStyle.SelectionForeColor = Color.DodgerBlue;
                            DataGridViewAccepted.DefaultCellStyle.SelectionForeColor = Color.ForestGreen;
                            DataGridViewAccepted.Focus();
                            return;
                        }

                        if (DataGridViewTasks.SelectedCells[0].RowIndex == DataGridViewTasks.RowCount - 1)
                            break;

                        if (Packages.mf.Controls.Contains(MainFormClassic.infocontrol))
                            GetInfo(DataGridViewTasks.Rows[DataGridViewTasks.SelectedCells[0].RowIndex + 1].Cells[0].Value.ToString());

                        break;
                    }
                case Keys.ControlKey:
                        {
                            string s = DataGridViewTasks.Rows[DataGridViewTasks.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                            ShowInfo(s);
                            break;
                        }
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

            Action<object> action = (object obj) =>
            {
                Thread.Sleep(100);

                try
                {
                    Boolean foc = true;

                    (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { foc = MainFormClassic.tasks.DataGridViewTasks.Focused; }));

                    using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                    {
                        conn.Open();

                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.tasks.DataGridViewTasks.Rows.Clear(); }));

                        string username = Packages.mf.LabelUserName.Text.Replace("Пользователь:  ", "");

                        MySqlCommand cmd = new MySqlCommand(@"SELECT tasks_id,priority_id,task_text FROM tasks 
                                                                WHERE `user_group` = (SELECT `group` FROM users WHERE `username` = '"
                                                                + username + "') AND `user_id` = 0 AND `inactive` = 0" +
                                                                " ORDER BY priority DESC", conn);

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Application.DoEvents();

                                int tasks_id = dr.GetInt32(0);
                                int priority_id = dr.GetInt32(1);
                                string tasks_text = dr.GetString(2);

                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.tasks.DataGridViewTasks.Rows.Add(tasks_id, "[" + priority_id + "] " + tasks_text); }));
                            }
                        }

                        if (foc)
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.tasks.DataGridViewTasks.Focus(); }));
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("[UpdateDataGrid] " + exc.Message);
                    Log.ExcWrite("[UpdateDataGrid]" + exc.Message);
                }
            };

            Task t = new Task(action, "UpdateDataGrid");
            t.Start();
        }

        public void UpdateDataGridAcceptedTasks()
        {
            Action<object> action = (object obj) =>
            {
                Thread.Sleep(100);

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                    {
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.tasks.DataGridViewAccepted.Rows.Clear(); }));

                        conn.Open();

                        string username = MainFormClassic.UserName;

                        MySqlCommand cmd = new MySqlCommand(@"SELECT tasks_id,priority_id,task_text FROM tasks WHERE `user_group` = 
                                                                (SELECT `group` FROM users WHERE `username` = '"
                                                                + username + "') AND `user_id` = (SELECT `id` FROM users WHERE `username` = '"
                                                                + username + "') AND `inactive` = 0 ORDER BY priority DESC", conn);

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Application.DoEvents();

                                int tasks_id = dr.GetInt32(0);
                                int priority_id = dr.GetInt32(1);
                                string tasks_text = dr.GetString(2);

                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.tasks.DataGridViewAccepted.Rows.Add(tasks_id, "[" + priority_id + "] " + tasks_text); }));
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("[UpdateDataGridAcceptedTasks]" + exc.Message);
                    Log.ExcWrite("[UpdateDataGridAcceptedTasks]" + exc.Message);
                }
            };

            Task t = new Task(action, "UpdateDataGrid");
            t.Start();
        }

        private void DataGridViewTasks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string s = DataGridViewTasks.Rows[DataGridViewTasks.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
            ShowInfo(s);
        }

        private void ShowInfo(string s)
        {
            if (!Packages.mf.Controls.Contains(MainFormClassic.infocontrol))
            {
                Packages.mf.Controls.Add(MainFormClassic.infocontrol);

                MainFormClassic.infocontrol.BringToFront();

                MainFormClassic.infocontrol.Show();

                MainFormClassic.infocontrol.Focus();

               GetInfo(s);
            }
            else
            {
                MainFormClassic.infocontrol.Hide();

                Packages.mf.Controls.Remove(MainFormClassic.infocontrol);
            }
        }

        private void CleanInfoControl(string s)
        {
            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.labelBarText.Text = "..."; }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.NameBarText.Text = "..."; }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceBarText.Text = "..."; }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.CountBarText.Text = "..."; }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DetectedBarText.Text = "..."; }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.ActionText.Text = "..."; }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DataGridViewSail.Rows.Clear(); }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DataGridViewSend.Rows.Clear(); }));
        }

        private void GetMainInfo(string s)
        {
            Action<object> action = (object obj) =>
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                    {
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand(@"SELECT bar,name,turn_price,count,sailed,status,status_text 
                                                                FROM priority WHERE id = (SELECT `priority_id` FROM `tasks` 
                                                                WHERE `tasks_id` = " + s + ")", conn);
                        cmd.CommandTimeout = 0;

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr == null) { return; }

                            if (!dr.HasRows) { }

                            while (dr.Read())
                            {
                                Application.DoEvents();

                                string bar = dr.GetString(0).Replace(" ", "");
                                string name = dr.GetString(1);
                                int turn_price = Convert.ToInt32(dr.GetValue(2));
                                float count = dr.GetFloat(3);
                                //try { float sailed = dr.GetFloat(4); }
                                //catch { float sailed = 0; }
                                int status = Convert.ToInt32(dr.GetValue(5));
                                string status_text = dr.GetString(6);

                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.labIdText.Text = s; }));
                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.labelBarText.Text = bar; }));
                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.NameBarText.Text = name; }));
                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceBarText.Text = turn_price.ToString(); }));
                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.CountBarText.Text = count.ToString(); }));
                            }
                        }
                    }
                }
                catch(Exception exc)
                {
                    MessageBox.Show("[GetMainInfo]" + exc.Message);
                    Log.ExcWrite("[GetMainInfo]" + exc.Message);
                }
            };

            Task t = new Task(action, "GetMainInfo");
            t.Start();
        }

        private void GetCurrentUkmPrice(string s)
        {
            Action<object> action = (object obj) =>
            {
                try
                {
                    string bar;

                    while (MainFormClassic.infocontrol.labelBarText.Text == "...")
                        Thread.Sleep(300);

                    bar = MainFormClassic.infocontrol.labelBarText.Text;

                    using (MySqlConnection conn = new MySqlConnection(Connector.UkmStringConnecting))
                    {
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand(@"SELECT b.price,MAX(b.version) FROM trm_in_var C LEFT JOIN trm_in_items A ON A.id=C.item " +
                            "LEFT JOIN trm_in_pricelist_items B ON B.item=c.item WHERE ( C.item= '"
                            + bar + "' OR C.id= '" + bar + "')AND b.pricelist_id= 1", conn);
                        cmd.CommandTimeout = 0;

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr == null)
                                return;

                            if (!dr.HasRows) { }

                            while (dr.Read())
                            {
                                Application.DoEvents();

                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceUkmText.Text = dr.GetString(0).Replace(",0000", ""); }));

                                try { CheckPrice(Convert.ToInt32(MainFormClassic.infocontrol.PriceBarText.Text), Convert.ToInt32(MainFormClassic.infocontrol.PriceUkmText.Text)); }
                                catch { }
                            }

                            if (!dr.IsClosed)
                                dr.Close();
                        }
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("[GetCurrentUkmPrice]" + exc.Message);
                    Log.ExcWrite("[GetCurrentUkmPrice]" + exc.Message);
                }
            };

            Task t = new Task(action, "GetCurrentUkmPrice");
            t.Start();
        }

        private void CheckPrice(int pricebar, int priceukm)
        {
            if (pricebar > priceukm)
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceUkmText.ForeColor = Color.Red; }));

            if (pricebar < priceukm)
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceUkmText.ForeColor = Color.Yellow; }));

            if (pricebar == priceukm)
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceUkmText.ForeColor = Color.ForestGreen; }));
        }

        private void RequestSendPrice(string s)
        {
            Action<object> action = (object obj) =>
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                    {
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand(@"SELECT `action`,`price`,`kod_isp`,`datetime` FROM `sendPOS` WHERE id = 
                                                                (SELECT priority_id FROM `tasks` WHERE tasks_id = " + s + ")", conn);
                        cmd.CommandTimeout = 0;

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr == null)
                                return;

                            if (!dr.HasRows) { }

                            while (dr.Read())
                            {
                                Application.DoEvents();

                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DataGridViewSend.Rows.Add(dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetString(3)); }));
                            }

                            if (!dr.IsClosed)
                                dr.Close();
                        }
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("[RequestSendPrice]" + exc.Message);
                    Log.ExcWrite("[RequestSendPrice]" + exc.Message);
                }
            };

            Task t = new Task(action, "RequestSendPrice");
            t.Start();
        }

        private void RequestSailPriceAndCount(string s)
        {
            Action<object> action = (object obj) =>
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                    {
                        conn.Open();

                        MySqlCommand cmd = new MySqlCommand(@"SELECT operation,price,count FROM `operations` WHERE `id` = 
                                                                (SELECT `priority_id` FROM `tasks` WHERE `tasks_id` = " + s + ")", conn);
                        cmd.CommandTimeout = 0;

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr == null) { return; }

                            if (!dr.HasRows) { }

                            while (dr.Read())
                            {
                                Application.DoEvents();

                                if (dr.GetString(0) == "0" || dr.GetString(1) == "0")
                                    continue;

                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DataGridViewSail.Rows.Add(dr.GetString(0), dr.GetString(1), dr.GetString(2)); }));
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("[RequestSailPriceAndCount]" + exc.Message);
                    Log.ExcWrite("[RequestSailPriceAndCount]" + exc.Message);
                }
            };

            Task t = new Task(action, "RequestSailPriceAndCount");
            t.Start();

        }

        private void GetInfo(string s)
        {
            // Cleanup after close form infoControl
            // mb not need

            // CleanInfoControl(s);

            GetMainInfo(s);

            GetCurrentUkmPrice(s);

            RequestSailPriceAndCount(s);

            RequestSendPrice(s);

        }

//         private void DetectedText(int i,DateTime date)
//         {
//             string DetectedText = String.Empty;
//             string ActionText = String.Empty;
// 
//             (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DetectedBarText.ForeColor = System.Drawing.Color.ForestGreen; }));
//             (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.ActionText.ForeColor = System.Drawing.Color.ForestGreen; }));
// 
//             int TotalDay = Convert.ToInt32((DateTime.Now - date).TotalSeconds) / 86400;
// 
//             switch(i)
//             {
//                 case 0:
//                     break;
//                 case 1:
//                     MainFormClassic.infocontrol.DetectedBarText.ForeColor = System.Drawing.Color.LightYellow;
//                     MainFormClassic.infocontrol.ActionText.ForeColor = System.Drawing.Color.LightYellow;
// 
//                     DetectedText = "Товар не продается долгое время.Число дней: " + TotalDay;
//                     ActionText = "Проверить фактическое наличие и штрихкод товара на зале";
//                     break;
//                 case 2:
//                     MainFormClassic.infocontrol.DetectedBarText.ForeColor = System.Drawing.Color.Orange;
//                     MainFormClassic.infocontrol.ActionText.ForeColor = System.Drawing.Color.Orange;
// 
//                     DetectedText = "Весь остаток товара продан по цене очередности.";
//                     ActionText = "Прогрузить новую цену на кассу.";
//                     break;
//                 case 3:
//                     MainFormClassic.infocontrol.DetectedBarText.ForeColor = System.Drawing.Color.Yellow;
//                     MainFormClassic.infocontrol.ActionText.ForeColor = System.Drawing.Color.Red;
// 
//                     DetectedText = "Продано больше чем было в очередности";
//                     ActionText = "Прогрузка цены на кассу.Принять меры к возмещению ущерба.";
//                     break;
//                 case 4:
//                     MainFormClassic.infocontrol.DetectedBarText.ForeColor = System.Drawing.Color.Yellow;
//                     MainFormClassic.infocontrol.ActionText.ForeColor = System.Drawing.Color.DarkRed;
// 
//                     DetectedText = "Товар продается дешевле цены очередности.";
//                     ActionText = "Прогрузка цены на кассу.Принять меры к возмещению ущерба.";
//                     break;
//                 case 5:
//                     MainFormClassic.infocontrol.DetectedBarText.ForeColor = System.Drawing.Color.OrangeRed;
//                     MainFormClassic.infocontrol.ActionText.ForeColor = System.Drawing.Color.OrangeRed;
// 
//                     DetectedText = "Были продажи дешевле цены очереди";
//                     ActionText = "Поиск причины.Возмещение ущерба.";
//                     break;
//                 case 6:
//                     MainFormClassic.infocontrol.DetectedBarText.ForeColor = System.Drawing.Color.Yellow;
//                     MainFormClassic.infocontrol.ActionText.ForeColor = System.Drawing.Color.Yellow;
// 
//                     DetectedText = "Завышение цены очередности";
//                     ActionText = "Прогрузка цены на кассу.Выявление причины.";
//                     break;
//             }
// 
//             (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DetectedBarText.Text = DetectedText; }));
//             (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.ActionText.Text = ActionText; }));
//         }

        private void DataGridViewTasks_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
             if (Packages.mf.Controls.Contains(MainFormClassic.infocontrol))
                 GetInfo(DataGridViewTasks.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void DataGridViewAccepted_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (DataGridViewAccepted.SelectedCells[0].RowIndex == 0 && DataGridViewTasks.RowCount > 0)
                    {
                        DataGridViewAccepted.DefaultCellStyle.SelectionForeColor = Color.DodgerBlue;
                        DataGridViewTasks.DefaultCellStyle.SelectionForeColor = Color.ForestGreen;
                        DataGridViewTasks.Focus();
                        break;
                    }
                    if (DataGridViewAccepted.SelectedCells[0].RowIndex == 0 && DataGridViewTasks.RowCount == 0)
                    {
                        Packages.mf.ButtonTasks.Focus();
                        Packages.mf.ButtonTasks.ForeColor = Color.Green;
                        DataGridViewAccepted.DefaultCellStyle.SelectionForeColor = Color.DodgerBlue;
                        DataGridViewTasks.DefaultCellStyle.SelectionForeColor = Color.DodgerBlue;
                        MainFormClassic.infocontrol.Hide();
                        Packages.mf.Controls.Remove(MainFormClassic.infocontrol);
                        break;
                    }
                    break;
                case Keys.ControlKey:
                    Point pCell = DataGridViewAccepted.GetCellDisplayRectangle(DataGridViewAccepted.CurrentCell.ColumnIndex, DataGridViewAccepted.CurrentCell.RowIndex, true).Location;
                    MenuStripDataGrid.Show(pCell.X + MainFormClassic.tasks.Location.X + DataGridViewAccepted.Location.X + DataGridViewAccepted.Size.Width / 4, pCell.Y + MainFormClassic.tasks.Location.Y + DataGridViewAccepted.Location.Y + 23);
                    MenuStripDataGrid.Items[0].Select();
                    break;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string s = DataGridViewAccepted.Rows[DataGridViewAccepted.SelectedCells[0].RowIndex].Cells[0].Value.ToString();

            //check this
            Action<object> action = (object obj) =>
            {
                Connector.ExecuteNonQuery("UPDATE `tasks` SET `user_id` = 0 WHERE  tasks_id = " + s);
            };

            Task t = new Task(action, "ExecuteNonQuery");
            t.Start();

            if (Server.server.Connected)
                Server.Sender("PrioritySale", 6, "Пользователь отказался от задачи " + s);

            CheckRowCount();
        }

        private void CompliteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = DataGridViewAccepted.Rows[DataGridViewAccepted.SelectedCells[0].RowIndex].Cells[0].Value.ToString();

            //check this
            Action<object> action = (object obj) =>
            {
                Connector.ExecuteNonQuery("UPDATE `tasks` SET `inactive` = 2 WHERE  tasks_id = " + s);
            };

            Task t = new Task(action, "ExecuteNonQuery");
            t.Start();

            if (Server.server.Connected)
                Server.Sender("PrioritySale", 6, "Пользователь выполнил задачу " + s);

            CheckRowCount();
        }

        private void CheckRowCount()
        {

            if (DataGridViewAccepted.Rows.Count == 1)
            {
                if (DataGridViewTasks.Rows.Count > 0)
                {
                    DataGridViewAccepted.DefaultCellStyle.SelectionForeColor = Color.DodgerBlue;
                    DataGridViewTasks.DefaultCellStyle.SelectionForeColor = Color.ForestGreen;
                    DataGridViewTasks.Focus();
                }
                else
                {
                    Packages.mf.ButtonTasks.Focus();
                    Packages.mf.ButtonTasks.ForeColor = System.Drawing.Color.Green;
                    DataGridViewTasks.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.DodgerBlue;
                }
            }

            MainFormClassic.tasks.UpdateDataGrid();
            MainFormClassic.tasks.UpdateDataGridAcceptedTasks();
        }
    }
}
