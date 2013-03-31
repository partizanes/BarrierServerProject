using MySql.Data.MySqlClient;
using System;
using System.Drawing;
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

                        if (DataGridViewAccepted.Rows.Count > 0)
                        {
                            DataGridViewTasks.DefaultCellStyle.SelectionForeColor = Color.DodgerBlue;
                            DataGridViewAccepted.DefaultCellStyle.SelectionForeColor = Color.ForestGreen;
                            DataGridViewAccepted.Focus();
                            return;
                        }

                        if (DataGridViewTasks.SelectedCells[0].RowIndex == DataGridViewTasks.RowCount - 1)
                            break;

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
            Boolean foc = MainFormClassic.tasks.DataGridViewTasks.Focused;

            using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "PrioritySailR", "***REMOVED***", "barrierserver")))
            {
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.tasks.DataGridViewTasks.Rows.Clear(); }));

                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT id, text, barcode FROM tasks WHERE `group` = (SELECT `group` FROM users WHERE `username` = '" + Packages.mf.LabelUserName.Text.Replace("Пользователь:  ","") + "') AND `user_id` = 0 ORDER BY priority DESC", conn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.tasks.DataGridViewTasks.Rows.Add(dr.GetString(0),dr.GetString(2) +" "+ dr.GetString(1)); }));

                if (!dr.IsClosed)
                    dr.Close();
                }

                if (foc)
                    MainFormClassic.tasks.DataGridViewTasks.Focus();
            }
        }

        public void UpdateDataGridAcceptedTasks()
        {
            using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "PrioritySailR", "***REMOVED***", "barrierserver")))
            {
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.tasks.DataGridViewAccepted.Rows.Clear(); }));

                conn.Open();

                string username = Packages.mf.LabelUserName.Text.Replace("Пользователь:  ", "");

                MySqlCommand cmd = new MySqlCommand("SELECT id, text, barcode FROM tasks WHERE `group` = (SELECT `group` FROM users WHERE `username` = '" + username + "') AND `user_id` = (SELECT `id` FROM users WHERE `username` = '" + username + "') ORDER BY priority DESC", conn);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.tasks.DataGridViewAccepted.Rows.Add(dr.GetString(0), dr.GetString(2) + " " + dr.GetString(1)); }));

                    if (!dr.IsClosed)
                        dr.Close();
                }
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

                MainFormClassic.infocontrol.Focus();

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

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.labelBarText.Text = "..."; }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.NameBarText.Text = "..."; }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceBarText.Text = "..."; }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.CountBarText.Text = "..."; }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DetectedBarText.Text = "..."; }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.ActionText.Text = "..."; }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.labIdText.Text = s; }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DataGridViewSail.Rows.Clear(); }));

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DataGridViewSend.Rows.Clear(); }));

            using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", "barrierserver")))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(@"SELECT barcode,name,price,count,date,status FROM `state` WHERE barcode = (SELECT barcode FROM tasks WHERE `id` = '" + s + "' AND tasks.sailprice = state.sailprice )", conn);
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
                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.labelBarText.Text = dr.GetString(0); }));
                        bar = dr.GetString(0).Replace(" ","");

                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.NameBarText.Text = dr.GetString(1); }));

                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceBarText.Text = dr.GetString(2); }));

                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.CountBarText.Text = dr.GetString(3); }));

                        date = dr.GetDateTime(4);

                        DetectedText(Convert.ToInt32(dr.GetValue(5)),date);
                    }

                    if (!dr.IsClosed)
                        dr.Close();
                }
            }

            using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", "barrierserver")))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(@"select state.sailprice,state.sailed from `state` where barcode = (Select barcode from tasks where `id` = '" + s + "' AND `priority` != 1)", conn);
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
                        if (dr.GetString(0) == "0" || dr.GetString(1) == "0")
                            continue;

                        (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DataGridViewSail.Rows.Add(dr.GetString(0), dr.GetString(1)); }));
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
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceUkmText.Text = dr.GetString(0).Replace(",0000",""); }));

                            try { CheckPrice(Convert.ToInt32(MainFormClassic.infocontrol.PriceBarText.Text), Convert.ToInt32(MainFormClassic.infocontrol.PriceUkmText.Text)); }
                            catch { }
                        }

                        if (!dr.IsClosed)
                            dr.Close();
                    }
                }
            }); ;
            thh.Name = "Запрос цены";
            Server.threads.Add(thh);
            thh.Start();

            Thread thg = new Thread(delegate()
            {
                using (MySqlConnection conn = new MySqlConnection(string.Format("server={0};uid={1};pwd={2};database={3};Connect Timeout=60;", Config.GetParametr("IpCashServer"), "BarrierServerR", "***REMOVED***", "barrierserver")))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT `action`,`price`,`kod_isp`,`datetime` FROM `sendprice` WHERE id = " + s, conn);
                    cmd.CommandTimeout = 0;

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr == null)
                            return;

                        if (!dr.HasRows) { }

                        while (dr.Read())
                        {
                            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DataGridViewSend.Rows.Add(dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetString(3)); }));
                        }

                        if (!dr.IsClosed)
                            dr.Close();
                    }
                }
            }); ;
            thg.Name = "Запрос отправки цен на кассу";
            Server.threads.Add(thg);
            thg.Start();
        }

        private void CheckPrice(int pricebar,int priceukm)
        {
            if(pricebar > priceukm)
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceUkmText.ForeColor = Color.Red; }));

            if (pricebar < priceukm)
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceUkmText.ForeColor = Color.Yellow; }));

            if(pricebar == priceukm)
                (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.PriceUkmText.ForeColor = Color.ForestGreen; }));
        }

        private void DetectedText(int i,DateTime date)
        {
            string DetectedText = "";
            string ActionText = "";

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DetectedBarText.ForeColor = System.Drawing.Color.ForestGreen; }));
            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.ActionText.ForeColor = System.Drawing.Color.ForestGreen; }));

            int TotalDay = Convert.ToInt32((DateTime.Now - date).TotalSeconds) / 86400;

            switch(i)
            {
                case 0:
                    break;
                case 1:
                    MainFormClassic.infocontrol.DetectedBarText.ForeColor = System.Drawing.Color.LightYellow;
                    MainFormClassic.infocontrol.ActionText.ForeColor = System.Drawing.Color.LightYellow;

                    DetectedText = "Товар не продается долгое время.Число дней: " + TotalDay;
                    ActionText = "Проверить фактическое наличие и штрихкод товара на зале";
                    break;
                case 2:
                    MainFormClassic.infocontrol.DetectedBarText.ForeColor = System.Drawing.Color.Orange;
                    MainFormClassic.infocontrol.ActionText.ForeColor = System.Drawing.Color.Orange;

                    DetectedText = "Весь остаток товара продан по цене очередности.";
                    ActionText = "Прогрузить новую цену на кассу.";
                    break;
                case 3:
                    MainFormClassic.infocontrol.DetectedBarText.ForeColor = System.Drawing.Color.Yellow;
                    MainFormClassic.infocontrol.ActionText.ForeColor = System.Drawing.Color.Red;

                    DetectedText = "Продано больше чем было в очередности";
                    ActionText = "Прогрузка цены на кассу.Принять меры к возмещению ущерба.";
                    break;
                case 4:
                    MainFormClassic.infocontrol.DetectedBarText.ForeColor = System.Drawing.Color.Yellow;
                    MainFormClassic.infocontrol.ActionText.ForeColor = System.Drawing.Color.DarkRed;

                    DetectedText = "Товар продается дешевле цены очередности.";
                    ActionText = "Прогрузка цены на кассу.Принять меры к возмещению ущерба.";
                    break;
                case 5:
                    MainFormClassic.infocontrol.DetectedBarText.ForeColor = System.Drawing.Color.OrangeRed;
                    MainFormClassic.infocontrol.ActionText.ForeColor = System.Drawing.Color.OrangeRed;

                    DetectedText = "Были продажи дешевле цены очереди";
                    ActionText = "Поиск причины.Возмещение ущерба.";
                    break;
                case 6:
                    MainFormClassic.infocontrol.DetectedBarText.ForeColor = System.Drawing.Color.Yellow;
                    MainFormClassic.infocontrol.ActionText.ForeColor = System.Drawing.Color.Yellow;

                    DetectedText = "Завышение цены очередности";
                    ActionText = "Прогрузка цены на кассу.Выявление причины.";
                    break;
            }

            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.DetectedBarText.Text = DetectedText; }));
            (Application.OpenForms[1] as AuthFormClassic).Invoke((MethodInvoker)(delegate() { MainFormClassic.infocontrol.ActionText.Text = ActionText; }));
        }

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
                    if (DataGridViewTasks.Rows.GetFirstRow(DataGridViewElementStates.Selected) == 0)
                    {
                        DataGridViewAccepted.DefaultCellStyle.SelectionForeColor = Color.DodgerBlue;
                        DataGridViewTasks.DefaultCellStyle.SelectionForeColor = Color.ForestGreen;
                        DataGridViewTasks.Focus();
                        break;
                    }
            	break;
            }
        }
    }
}
