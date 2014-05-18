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
    public partial class LogForm : UserControl
    {
        public LogForm()
        {
            InitializeComponent();
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(17, 78);
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                case Keys.Enter:
                    this.Hide();
                    Packages.mf.Controls.Remove(MainFormClassic.logform);
                    Packages.mf.ButtonLog.Focus();
                    break;
            }
        }


    }
}
