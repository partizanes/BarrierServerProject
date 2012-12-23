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
    public partial class Form1 : Form
    {
        private int m_stat_auth = 0;
 

        public Form1()
        {
            InitializeComponent();
        }

        public int stat_auth
        {
            set
            {
                m_stat_auth = value;
            }
            get
            {
                return m_stat_auth;
            }
        }
    }
}
