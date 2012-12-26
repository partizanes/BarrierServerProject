using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrioritySales
{
    public partial class MainForm : Form
    {
        private int m_stat_auth = 0;
 

        public MainForm()
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

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AuthForm af = new AuthForm();
            af.TaskbarIcon.Visible = false;
            Process.GetCurrentProcess().Kill(); 
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).Hide(); }));
        }
    }
}
