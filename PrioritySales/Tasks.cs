using MySql.Data.MySqlClient;
using System;
using System.Threading;
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
                case Keys.Up:
                        if (DataGridViewTasks.Rows.GetFirstRow(DataGridViewElementStates.Selected) == 0)
                            Packages.mf.ButtonTasks.Focus();
                        else
                            GetInfo(DataGridViewTasks.Rows[DataGridViewTasks.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                        break;
                case Keys.Down:
                        GetInfo(DataGridViewTasks.Rows[DataGridViewTasks.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                    break;
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
            Boolean foc = MainFormClassic.tasks.DataGridViewTasks.Focused;

            using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "PrioritySailR", "***REMOVED***", "barrierserver")))
            {
                (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.tasks.DataGridViewTasks.Rows.Clear(); }));

                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT id, text, barcode FROM tasks WHERE `group` = (SELECT `group` FROM users WHERE `username` = '" + Packages.mf.LabelUserName.Text.Replace("Пользователь:  ","") + "') AND `user_id` = 0 ORDER BY priority DESC", conn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                        (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.tasks.DataGridViewTasks.Rows.Add(dr.GetString(0),dr.GetString(2) +" "+ dr.GetString(1)); }));

                if (!dr.IsClosed)
                    dr.Close();
                }

                if (foc)
                    MainFormClassic.tasks.DataGridViewTasks.Focus();
            }
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
                //MainFormClassic.infocontrol.Visible = true;

                Packages.mf.Controls.Add(MainFormClassic.infocontrol);

                MainFormClassic.infocontrol.BringToFront();

                MainFormClassic.infocontrol.Show();

                GetInfo(s);
            }
            else
            {
                MainFormClassic.infocontrol.Hide();

                Packages.mf.Controls.Remove(MainFormClassic.infocontrol);
            }
        }

        private void GetInfo(string s)
        {
            string bar = "";
            DateTime date = new DateTime();

            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.labelBarText.Text = "..."; }));

            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.NameBarText.Text = "..."; }));

            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceBarText.Text = "..."; }));

            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.CountBarText.Text = "..."; }));

            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DataGridViewSail.Rows.Clear(); }));

            using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", "barrierserver")))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(@"SELECT state.barcode,state.name,state.price,state.count,state.date FROM `state` WHERE barcode = (SELECT barcode FROM tasks WHERE `id` = '" + s + "' AND tasks.sailprice = state.sailprice )", conn);
                cmd.CommandTimeout = 0;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr == null)
                        return;

                    if (!dr.HasRows)
                    {

                    }

                    while (dr.Read())
                    {
                        (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.labelBarText.Text = dr.GetString(0); }));
                        bar = dr.GetString(0).Replace(" ","");

                        (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.NameBarText.Text = dr.GetString(1); }));

                        (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceBarText.Text = dr.GetString(2); }));

                        (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.CountBarText.Text = dr.GetString(3); }));

                        date = dr.GetDateTime(4);
                    }

                    if (!dr.IsClosed)
                        dr.Close();
                }
            }

            using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", "barrierserver")))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(@"select state.sailprice,state.sailed from `state` where barcode = (Select barcode from tasks where `id` = '" + s + "')", conn);
                cmd.CommandTimeout = 0;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr == null)
                        return;

                    if (!dr.HasRows)
                    {

                    }

                    while (dr.Read())
                    {
                        (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DataGridViewSail.Rows.Add(dr.GetString(0), dr.GetString(1)); }));
                    }

                    if (!dr.IsClosed)
                        dr.Close();
                }
            }

            Thread thh = new Thread(delegate()
            {
                using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", Config.GetParametr("BdName"))))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT b.price FROM trm_in_var C LEFT JOIN trm_in_items A ON A.id=C.item LEFT JOIN trm_in_pricelist_items B ON B.item=c.item WHERE C.item= '"+ bar + "' AND b.pricelist_id= 1", conn);
                    cmd.CommandTimeout = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr == null)
                            return;

                        if (!dr.HasRows) {}

                        while (dr.Read())
                        {
                            (Application.OpenForms[1] as AuthForm).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceUkmText.Text = dr.GetString(0).Replace(",0000",""); }));
                        }

                        if (!dr.IsClosed)
                            dr.Close();
                    }
                }
            }); ;
            thh.Name = "Запрос цены";
            Server.threads.Add(thh);
            thh.Start();
        }
    }
}
