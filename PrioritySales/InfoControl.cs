﻿using System;
using System.Drawing;
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
                    CleanUpAfterHide();
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
                    CleanUpAfterHide();
                    Packages.mf.Controls.Remove(MainFormClassic.infocontrol);
                    Packages.mf.ButtonTasks.Focus();
                    Packages.mf.ButtonTasks.ForeColor = Color.DodgerBlue;
                    MainFormClassic.tasks.DataGridViewTasks.Focus();
                    break;
            }
        }

        private void CleanUpAfterHide()
        {
            MainFormClassic.infocontrol.DataGridViewSail.Rows.Clear();
            MainFormClassic.infocontrol.DataGridViewSend.Rows.Clear();

            MainFormClassic.infocontrol.PriceBarText.Text = "...";
            MainFormClassic.infocontrol.CountBarText.Text = "...";
            MainFormClassic.infocontrol.PriceUkmText.Text = "...";
            MainFormClassic.infocontrol.NameBarText.Text = "...";
            MainFormClassic.infocontrol.labelBarText.Text = "...";
            MainFormClassic.infocontrol.labIdText.Text = "...";
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
            Connector.ExecuteNonQuery("UPDATE `tasks` SET `user_id` = (SELECT id FROM users WHERE username = '" + Packages.mf.LabelUserName.Text.Replace("Пользователь:  ", "") + "' ) WHERE  tasks_id = " + labIdText.Text);

            if (Server.server.Connected)
                Server.Sender("PrioritySale", 6, "Пользователь принял задачу " + labIdText.Text);

            Clipboard.SetDataObject(MainFormClassic.infocontrol.labelBarText.Text);

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
