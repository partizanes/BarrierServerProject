using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrioritySales
{
    public partial class Tasks : Form
    {
        public Tasks()
        {
            InitializeComponent();
            
            DataGridViewTasks.Rows.Add("1", "фцвцфвфцвцфвцфвцфвцфвфцвфцвфц");
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
    }
}
