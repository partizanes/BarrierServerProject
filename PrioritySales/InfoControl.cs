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
    }
}
