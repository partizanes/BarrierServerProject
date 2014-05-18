using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrioritySales
{
    class Packages
    {
        public static MainFormClassic mf = new MainFormClassic();
        public static CareForm careform = new CareForm();
        public static int Zx = SystemInformation.PrimaryMonitorSize.Width - (SystemInformation.PrimaryMonitorSize.Width/6);
        public static int Zy = SystemInformation.PrimaryMonitorSize.Height - (SystemInformation.PrimaryMonitorSize.Height / 15);

        public static void parse(string p_id, int com, string msg)
        {
            switch (p_id)
            {
                case "PrioritySale":

                    switch (com)
                    {
                        case 0:
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { AuthFormClassic.status = false; }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthFormClassic).BackColor = Color.DarkRed; }));
                            System.Threading.Thread.Sleep(2000);
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthFormClassic).BackColor = Color.DodgerBlue; }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthFormClassic).ButtonSend.Enabled = true; }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthFormClassic).PassTextBox.Text = ""; }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthFormClassic).PassTextBox.Focus(); }));
                            break;
                        case 1:
                            (Application.OpenForms[1] as AuthFormClassic).BeginInvoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthFormClassic).Hide(); }));
                            (Application.OpenForms[1] as AuthFormClassic).BeginInvoke((MethodInvoker)(delegate() { AuthFormClassic.status = false; }));
                            (Application.OpenForms[1] as AuthFormClassic).BeginInvoke((MethodInvoker)(delegate() { mf.Show(); }));
                            (Application.OpenForms[1] as AuthFormClassic).BeginInvoke((MethodInvoker)(delegate() { mf.LabelUserName.Text = "Пользователь:  " + msg; }));
                            break;
                        case 2:
                            QueryStatus(true, msg);
                            break;
                        case 3:
                            QueryStatus(false, msg);
                            break;
                        case 4:
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { AuthFormClassic.status = false; }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthFormClassic).BackColor = Color.Red; }));
                            System.Threading.Thread.Sleep(3000);
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthFormClassic).BackColor = Color.DodgerBlue; }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthFormClassic).ButtonSend.Enabled = true; }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthFormClassic).ButtonSend.Focus(); }));
                            break;
                        case 5:
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { careform.Location = new System.Drawing.Point(Zx, Zy); }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { careform.Show(); }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { careform.LabelCareForm.Visible = false; }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.Focus(); }));
                            break;
                        case 6:
                            Action<object> action = (object obj) =>
                            {
                                GetMsgBoard();
                            };
                            Task t = new Task(action, "GetMsgBoard");
                            t.Start();
                            break;
                        case 7:
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { Packages.mf.PrioritySalesIcon.Icon = Properties.Resources.logo; }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.TimerIconChange.Enabled = false; }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { careform.LabelCareForm.Visible = false; }));
                            break;
                        case 8:
                            MainFormClassic.tasks.UpdateDataGrid();
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.TimerIconChange.Enabled = true; }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { careform.LabelCareForm.Visible = true; }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { careform.Location = new System.Drawing.Point(Zx, Zy); }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { Application.DoEvents(); }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { careform.Show(); }));
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.Focus(); }));
                            break;
                        case 9:
                            try
                            {
                                string[] split_data = msg.Replace("\0", "").Split(new Char[] { ';' });

                                MainFormClassic.StatusUpdate = split_data[0];

                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.LabelVersionBd.Text = ("Бд: " + split_data[1]); }));

                                //                                 if (mf.dataGridViewMainForm.Visible == true)
                                //                                 {
                                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                                {
                                    conn.Open();

                                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `priority`", conn);

                                    using (MySqlDataReader dr = cmd.ExecuteReader())
                                    {
                                        if (dr == null)
                                            return;

                                        if (!dr.HasRows)
                                        {
                                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() {
                                                    if (mf.dataGridViewMainForm.Focused)
                                                        mf.ButtonList.Focus();
                                                }));
                                        }

                                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.dataGridViewMainForm.Rows.Clear(); }));

                                        while (dr.Read())
                                        {
                                            Application.DoEvents();

                                            object u_id = dr.GetValue(0);
                                            string barcode = dr.GetString(1).Replace(" ", "");
                                            string name = dr.GetString(2).Replace("  ", "");
                                            object turn_price = dr.GetValue(3);
                                            object turn_count = dr.GetValue(4);
                                            object sail = dr.GetValue(5);
                                            object status = dr.GetValue(6);
                                            string status_text = dr.GetString(7);
                                            object current_price_ukm = dr.GetValue(8);
                                            string dt = dr.GetString(9);

                                            if (sail.ToString() == "")
                                                sail = 0;

                                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.dataGridViewMainForm.Rows.Add(u_id, barcode, name, turn_price, turn_count.ToString().Replace(",000", ""), sail.ToString().Replace(",000", ""), status, current_price_ukm, dt); }));
                                        }
                                    }
                                }

                                MainFormClassic.tasks.UpdateDataGrid();
                                MainFormClassic.tasks.UpdateDataGridAcceptedTasks();
                            }
                            catch (System.Exception ex)
                            {
                                //to msg to user
                                Log.log_write(ex.Message, "EXCEPTION", "exception");
                                Console.WriteLine("Блок parse: " + ex.Message);
                                return;
                            }
                            finally
                            {
                                if (Server.server.Connected)
                                    Server.Sender("PrioritySale", 8, MainFormClassic.StatusUpdate);

                                GC.Collect();
                            }
                            break;
                    }
                    break;
            }
         }

        public static void QueryStatus(bool a,string str)
        {
            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.ButtonTurn.Enabled = true; }));

            if (a)
            {
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.PanelAddBg.BackColor = Color.Green; }));
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.LabelInfo.ForeColor = Color.Green; }));
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.TextboxNameItem.Text = ""; }));
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.TextboxAddBar.Text = ""; }));
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.TextboxCountAdd.Text = ""; }));
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.TextboxPrice.Text = ""; }));
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.LabelInfo.Text = str; }));
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.TimerClearMsg.Enabled = true; ; }));
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.TextboxAddBar.Focus(); }));
            }
            else
            {
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.PanelAddBg.BackColor = Color.Red; }));
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.LabelInfo.ForeColor = Color.Red; }));
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.LabelInfo.Text = str; }));
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.TimerClearMsg.Enabled = true; ; }));
            }

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { mf.Refresh(); }));
        }

        public static void GetMsgBoard()
        {
            try
            {
                Thread.Sleep(500);

                using (MySqlConnection conn = new MySqlConnection(Connector.BarrierStringConnecting))
                {
                    conn.Open();

                    (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.msgdesk.ListViewMsg.Items.Clear(); }));

                    MySqlCommand cmd = new MySqlCommand("SELECT u.username,m.msg,m.msg_color,m.msg_datetime,m.msg_id FROM users u,message m WHERE u.id = m.userid ORDER BY m.msg_priority", conn);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr == null || !dr.HasRows) { return; }

                        while (dr.Read())
                        {
                            Application.DoEvents();

                            string username = dr.GetString(0);
                            string msg = dr.GetString(1);
                            Color color = Color.FromName(dr.GetString(2));
                            UInt64 id_msg = dr.GetUInt64(4);

                            if (color.ToString().Contains("ff"))
                            {
                                ColorConverter colcon = new ColorConverter();

                                color = (Color)colcon.ConvertFromString("#" + color.Name);
                            }

                            DateTime datetime = dr.GetDateTime(3);

                            string _CompleteMsg = id_msg + ": [" + datetime.ToString("dd.MM") + "] [" + datetime.ToShortTimeString() + "] " + " [" + username + "] " + msg;

                            if (_CompleteMsg.Length > 115 && _CompleteMsg.Length <= 230)
                            {
                                string part1 = _CompleteMsg.Substring(0, 115);
                                string part2 = _CompleteMsg.Substring(115, _CompleteMsg.Length - 115);

                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() {
                                    MainFormClassic.msgdesk.ListViewMsg.Items.Add(part1); MainFormClassic.msgdesk.ListViewMsg.Items[MainFormClassic.msgdesk.ListViewMsg.Items.Count - 1].ForeColor = color;
                                    MainFormClassic.msgdesk.ListViewMsg.Items.Add(part2); MainFormClassic.msgdesk.ListViewMsg.Items[MainFormClassic.msgdesk.ListViewMsg.Items.Count - 1].ForeColor = color;
                                
                                }));

                                continue;
                            }

                            if (_CompleteMsg.Length > 230 && _CompleteMsg.Length <= 345)
                            {
                                string part1 = _CompleteMsg.Substring(0, 115);
                                string part2 = _CompleteMsg.Substring(115, 115);
                                string part3 = _CompleteMsg.Substring(230, _CompleteMsg.Length - 230);

                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() {
                                    MainFormClassic.msgdesk.ListViewMsg.Items.Add(part1); MainFormClassic.msgdesk.ListViewMsg.Items[MainFormClassic.msgdesk.ListViewMsg.Items.Count - 1].ForeColor = color;
                                    MainFormClassic.msgdesk.ListViewMsg.Items.Add(part2); MainFormClassic.msgdesk.ListViewMsg.Items[MainFormClassic.msgdesk.ListViewMsg.Items.Count - 1].ForeColor = color;
                                    MainFormClassic.msgdesk.ListViewMsg.Items.Add(part3); MainFormClassic.msgdesk.ListViewMsg.Items[MainFormClassic.msgdesk.ListViewMsg.Items.Count - 1].ForeColor = color;
                                
                                }));

                                continue;
                            }

                            if (_CompleteMsg.Length > 345 && _CompleteMsg.Length <= 460)
                            {
                                string part1 = _CompleteMsg.Substring(0, 115);
                                string part2 = _CompleteMsg.Substring(115, 115);
                                string part3 = _CompleteMsg.Substring(230, 115);
                                string part4 = _CompleteMsg.Substring(345, _CompleteMsg.Length - 345);

                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() {
                                     MainFormClassic.msgdesk.ListViewMsg.Items.Add(part1); MainFormClassic.msgdesk.ListViewMsg.Items[MainFormClassic.msgdesk.ListViewMsg.Items.Count - 1].ForeColor = color;
                                     MainFormClassic.msgdesk.ListViewMsg.Items.Add(part2); MainFormClassic.msgdesk.ListViewMsg.Items[MainFormClassic.msgdesk.ListViewMsg.Items.Count - 1].ForeColor = color;
                                     MainFormClassic.msgdesk.ListViewMsg.Items.Add(part3); MainFormClassic.msgdesk.ListViewMsg.Items[MainFormClassic.msgdesk.ListViewMsg.Items.Count - 1].ForeColor = color;
                                     MainFormClassic.msgdesk.ListViewMsg.Items.Add(part4); MainFormClassic.msgdesk.ListViewMsg.Items[MainFormClassic.msgdesk.ListViewMsg.Items.Count - 1].ForeColor = color;
                                 
                               }));

                                continue;
                            }

                            if (_CompleteMsg.Length < 115)
                            {
                                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() {
                                    MainFormClassic.msgdesk.ListViewMsg.Items.Add(_CompleteMsg);
                                    MainFormClassic.msgdesk.ListViewMsg.Items[MainFormClassic.msgdesk.ListViewMsg.Items.Count - 1].ForeColor = color;
                                }));
                                continue;
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                Log.ExcWrite("[GetMsgBoard] " + exc.Message);
            }
        }
    }
}
