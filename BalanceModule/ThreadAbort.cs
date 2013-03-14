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
