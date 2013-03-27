using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrioritySales
{
    public partial class InfoControl : UserControl
    {
        public InfoControl()
        {
            InitializeComponent();
        }

        private void InfoControl_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(50, 90);
        }
        private void ButtonAccept_Enter(object sender, EventArgs e)
        {
            ButtonAccept.ForeColor = Color.Green;
        }
        private void ButtonAccept_Leave(object sender, EventArgs e)
        {
            ButtonAccept.ForeColor = Color.DodgerBlue;
        }
        private void ButtonDenied_Enter(object sender, EventArgs e)
        {
            ButtonDenied.ForeColor = Color.Green;
        }
        private void ButtonDenied_Leave(object sender, EventArgs e)
        {
            ButtonDenied.ForeColor = Color.DodgerBlue;
        }
        private void ButtonMark_Enter(object sender, EventArgs e)
        {
            ButtonMark.ForeColor = Color.Green;
        }
        private void ButtonMark_Leave(object sender, EventArgs e)
        {
            ButtonMark.ForeColor = Color.DodgerBlue;
        }

        private void ButtonAccept_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                case Keys.ControlKey:
                    MainFormClassic.infocontrol.Hide();
                    Packages.mf.Controls.Remove(MainFormClassic.infocontrol);
                    Packages.mf.ButtonTasks.Focus();
                    Packages.mf.ButtonTasks.ForeColor = Color.DodgerBlue;
                    MainFormClassic.tasks.DataGridViewTasks.Focus();
                    break;
            }
        }

        private void ButtonDenied_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                case Keys.ControlKey:
                    MainFormClassic.infocontrol.Hide();
                    Packages.mf.Controls.Remove(MainFormClassic.infocontrol);
                    Packages.mf.ButtonTasks.Focus();
                    Packages.mf.ButtonTasks.ForeColor = Color.DodgerBlue;
                    MainFormClassic.tasks.DataGridViewTasks.Focus();
                    break;
            }
        }

        private void ButtonMark_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                case Keys.ControlKey:
                    MainFormClassic.infocontrol.Hide();
                    Packages.mf.Controls.Remove(MainFormClassic.infocontrol);
                    Packages.mf.ButtonTasks.Focus();
                    Packages.mf.ButtonTasks.ForeColor = Color.DodgerBlue;
                    MainFormClassic.tasks.DataGridViewTasks.Focus();
                    break;
            }
        }

        private void ButtonDenied_Click(object sender, EventArgs e)
        {
            MainFormClassic.infocontrol.Hide();
            Packages.mf.Controls.Remove(MainFormClassic.infocontrol);
            Packages.mf.ButtonTasks.Focus();
            Packages.mf.ButtonTasks.ForeColor = Color.DodgerBlue;
            MainFormClassic.tasks.DataGridViewTasks.Focus();
        }

        private void ButtonAccept_Click(object sender, EventArgs e)
        {
            Packages.connector.ExecuteNonQuery("UPDATE `tasks` SET `user_id` = (SELECT id FROM users WHERE username = '" + Packages.mf.LabelUserName.Text.Replace("Пользователь:  ", "") + "' ) WHERE  id = " + labIdText.Text, "barrierserver");

            MainFormClassic.infocontrol.Hide();
            Packages.mf.Controls.Remove(MainFormClassic.infocontrol);
            Packages.mf.ButtonTasks.Focus();
            Packages.mf.ButtonTasks.ForeColor = Color.DodgerBlue;
            MainFormClassic.tasks.DataGridViewTasks.Focus();

            MainFormClassic.tasks.UpdateDataGrid();

            MainFormClassic.tasks.UpdateDataGridAcceptedTasks();
        }
    }
}
