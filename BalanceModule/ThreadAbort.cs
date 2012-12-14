using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BalanceModule
{
    public static class ThreadAbort
    {
        public static void th_abort()
        {
            try { Thread.CurrentThread.Abort(); }
            
            catch
            {
                Thread.ResetAbort();
                Server.remove_tr();
            }
        }
    }
}
