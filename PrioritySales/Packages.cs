using System;
using System.Collections;
using System.Collections.Generic;
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
                            MainForm mf = new MainForm();
                            (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.Show(); }));
                            (Application.OpenForms[0] as AuthForm).Invoke((MethodInvoker)(delegate() { mf.LabelUserName.Text += msg; }));
                            break;
                    }
                    break;
            }
        }
    }
}
