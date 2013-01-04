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

        private void MainForm_Load(object sender, EventArgs e)
        {
            ButtonAdd.Focus();
        }

        private void ButtonAdd_Enter(object sender, EventArgs e)
        {
            ButtonAdd.ForeColor = Color.Green;
        }

        private void ButtonAdd_Leave(object sender, EventArgs e)
        {
            ButtonAdd.ForeColor = Color.DodgerBlue;
        }

        private void ButtonList_Enter(object sender, EventArgs e)
        {
            ButtonList.ForeColor = Color.Green;
        }

        private void ButtonList_Leave(object sender, EventArgs e)
        {
            ButtonList.ForeColor = Color.DodgerBlue;
        }

        private void ButtonMsg_Enter(object sender, EventArgs e)
        {
            ButtonMsg.ForeColor = Color.Green;
        }

        private void ButtonMsg_Leave(object sender, EventArgs e)
        {
            ButtonMsg.ForeColor = Color.DodgerBlue;
        }

        private void ButtonUnk_Enter(object sender, EventArgs e)
        {
            ButtonUnk.ForeColor = Color.Green;
        }

        private void ButtonUnk_Leave(object sender, EventArgs e)
        {
            ButtonUnk.ForeColor = Color.DodgerBlue;
        }

        private void ButtonSetting_Enter(object sender, EventArgs e)
        {
            ButtonSetting.ForeColor = Color.Green;
        }

        private void ButtonSetting_Leave(object sender, EventArgs e)
        {
            ButtonSetting.ForeColor = Color.DodgerBlue;
        }

        private void ButtonHide_Enter(object sender, EventArgs e)
        {
            ButtonHide.ForeColor = Color.Green;
        }

        private void ButtonHide_Leave(object sender, EventArgs e)
        {
            ButtonHide.ForeColor = Color.DodgerBlue;
        }

        private void ButtonExit_Enter(object sender, EventArgs e)
        {
            ButtonExit.ForeColor = Color.Green;
        }

        private void ButtonExit_Leave(object sender, EventArgs e)
        {
            ButtonExit.ForeColor = Color.DodgerBlue;
        }
    }
}
