using System;

namespace Update
{
    class Connector
    {
        private string IpCashServer;
        private string UpdateServerBase;
        private int CommandTimeout;
        private int ConnectTimeout;

        Connector()
        {
            try
            {
                IpCashServer = Config.GetParametr("IpCashServer");
                UpdateServerBase = Config.GetParametr("UpdateServerBase");
                CommandTimeout = int.Parse(Config.GetParametr("CommandTimeout"));
                ConnectTimeout = int.Parse(Config.GetParametr("ConnectTimeout"));
            }
            catch (Exception exc)
            {
                Log.ExcWrite("[Connector init]" + exc.Message);
            }
        }

    }
}
