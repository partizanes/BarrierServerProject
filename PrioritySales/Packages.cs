using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PrioritySales
{
    class Packages
    {
        public static MainFormClassic mf = new MainFormClassic();
        public static Connector connector = new Connector();
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
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).textboxPass.Text = ""; }));
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).textboxPass.Focus(); }));
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).LabelMsg.Text = "            Данные неверны!"; }));
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).LabelMsg.Update(); }));
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).Refresh(); }));
                            (Application.OpenForms[1] as AuthForm).buttonLogin.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).buttonLogin.PerformClick(); }));
                            break;
                        case 1:
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).Hide(); }));
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.Show(); }));
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.LabelUserName.Text = "Пользователь:  " + msg; }));

                            break;
                        case 2:
                            QueryStatus(true, msg);
                            break;
                        case 3:
                            QueryStatus(false, msg);
                            break;
                        case 4:
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).textboxPass.Text = ""; }));
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).textboxPass.Focus(); }));
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).LabelMsg.Text = msg; }));
                            (Application.OpenForms[1] as AuthForm).buttonLogin.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[1] as AuthForm).buttonLogin.PerformClick(); }));
                            break;
                        case 7:
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { Packages.mf.PrioritySalesIcon.Icon = Properties.Resources.logo; }));
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TimerIconChange.Enabled = false; }));
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { careform.LabelCareForm.Visible = false; }));
                            break;
                        case 8:
                            MainFormClassic.tasks.UpdateDataGrid();
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TimerIconChange.Enabled = true; }));
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { careform.LabelCareForm.Visible = true; }));
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { careform.Location = new System.Drawing.Point(Zx, Zy); }));
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { Application.DoEvents(); }));
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { careform.Show(); }));
                            break;
                        case 9:
                            try
                            {
                                if (!mf.dataGridViewMainForm.Visible)
                                    return;

                                string[] split_data = msg.Replace("\0", "").Split(new Char[] { ';' });

                                MainFormClassic.StatusUpdate = split_data[0];

                                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.LabelVersionBd.Text = ("Бд: " + split_data[1]); }));

                                if (mf.dataGridViewMainForm.Visible == true)
                                {
                                    using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "PrioritySailR", "***REMOVED***", "barrierserver")))
                                    {
                                        conn.Open();

                                        MySqlCommand cmd = new MySqlCommand("SELECT * FROM `state`", conn);

                                        using (MySqlDataReader dr = cmd.ExecuteReader())
                                        {
                                            if (dr == null)
                                                return;

                                            if (!dr.HasRows)
                                            {
                                                //TODO
                                            }

                                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.dataGridViewMainForm.Rows.Clear(); }));


                                            while (dr.Read())
                                            {
                                                string barcode = dr.GetString(0).Replace(" ", "");
                                                string name = dr.GetString(1).Replace("  ", "");
                                                object price = dr.GetValue(2);
                                                object pricesail = dr.GetValue(3);
                                                object count = dr.GetValue(4);
                                                object sail = dr.GetValue(5);
                                                object status = dr.GetValue(6);
                                                string dt = dr.GetString(7);

                                                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.dataGridViewMainForm.Rows.Add(barcode, name, price, pricesail, count.ToString().Replace(",000", ""), sail.ToString().Replace(",000", ""), status, dt); }));
                                            }
                                        }
                                    }
                                }
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
                                if(Server.server.Connected)
                                    Server.Sender("PrioritySale", 8, MainFormClassic.StatusUpdate);

                                GC.Collect();

//                                 if (!mf.ButtonList.ContainsFocus)
//                                     (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.dataGridView1.Focus(); }));
                            }
                            break;
                    }
                    break;
            }
        }

        public static void QueryStatus(bool a,string str)
        {
            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.ButtonTurn.Enabled = true; }));

            if (a)
            {
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.PanelAddBg.BackColor = Color.Green; }));
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.LabelInfo.ForeColor = Color.Green; }));
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TextboxNameItem.Text = ""; }));
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TextboxAddBar.Text = ""; }));
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TextboxCountAdd.Text = ""; }));
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TextboxPrice.Text = ""; }));
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.LabelInfo.Text = str; }));
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TimerClearMsg.Enabled = true; ; }));
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TextboxAddBar.Focus(); }));
            }
            else
            {
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.PanelAddBg.BackColor = Color.Red; }));
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.LabelInfo.ForeColor = Color.Red; }));
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.LabelInfo.Text = str; }));
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TimerClearMsg.Enabled = true; ; }));
            }

            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.Refresh(); }));
        }
    }
}
