using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PrioritySales
{
    public partial class PriorityDetails : UserControl
    {
        public PriorityDetails()
        {
            InitializeComponent();
        }

        private void PriorityDetails_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(30, 90);
        }

        private void dataGridViewMainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    MainFormClassic.prioritydetails.Hide();
                    Packages.mf.Controls.Remove(MainFormClassic.prioritydetails);
                    Packages.mf.dataGridViewMainForm.Focus();
                    break;
            }
        }

        private void PriorityDetails_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    MainFormClassic.prioritydetails.Hide();
                    Packages.mf.Controls.Remove(MainFormClassic.prioritydetails);
                    Packages.mf.dataGridViewMainForm.Focus();
                    break;
            }
        }

        private void dataGridViewMainForm_Enter(object sender, EventArgs e)
        {

        }
    }
}
