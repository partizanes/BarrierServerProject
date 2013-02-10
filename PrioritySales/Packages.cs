using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PrioritySales
{
    class Packages
    {
        static MainFormClassic mf = new MainFormClassic();

        public static void parse(string p_id, int com, string msg)
        {
            switch (p_id)
            {
                case "PrioritySale":

                    switch (com)
                    {
                        case 0:
                            (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).textboxPass.Text = ""; }));
                            (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).textboxPass.Focus(); }));
                            (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).LabelMsg.Text = "            Данные неверны!"; }));
                            (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).LabelMsg.Update(); }));
                            (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).Refresh(); }));
                            (Application.OpenForms[0] as AuthForm).buttonLogin.Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).buttonLogin.PerformClick(); }));
                            break;
                        case 1:
                            (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { (Application.OpenForms[0] as AuthForm).Hide(); }));
                            (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.Show(); }));
                            (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.LabelUserName.Text += msg; }));

                            break;
                        case 2:
                            QueryStatus(true, msg);
                            break;
                        case 3:
                            QueryStatus(false, msg);
                            break;
                    }
                    break;
            }
        }

        public static void QueryStatus(bool a,string str)
        {
            if (a)
            {
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.PanelAddBg.BackColor = Color.Green; }));
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.LabelInfo.ForeColor = Color.Green; }));
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TextboxNameItem.Text = ""; }));
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TextboxAddBar.Text = ""; }));
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TextboxCountAdd.Text = ""; }));
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TextboxPrice.Text = ""; }));
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.LabelInfo.Text = str; }));
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TimerClearMsg.Enabled = true; ; }));
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TextboxAddBar.Focus(); }));
            }
            else
            {
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.PanelAddBg.BackColor = Color.Red; }));
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.LabelInfo.ForeColor = Color.Red; }));
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.LabelInfo.Text = str; }));
                (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.TimerClearMsg.Enabled = true; ; }));
            }

            (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.Refresh(); }));
        }
    }
}
