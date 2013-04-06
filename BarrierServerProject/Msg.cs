using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Serialization;

namespace BarrierServerProject
{
    class Msg
    {
        public static bool SendUser(string user_name, string group, int type, string msg)
        {
            try
            {
                MSG packet = new MSG(group, type, msg);

                byte[] buf = new byte[2048];

                buf = Util.Serialization(packet);

                foreach (DictionaryEntry de in Server.clients)
                {
                    if ((de.Value).ToString() == user_name)
                        Server.MessageSender((Socket)de.Key, buf);
                }
            }
            catch (System.Exception ex)
            {
                //parse this error and other send to administrator 
                Log.Write(ex.Message, "Exception", "exception");
                Color.WriteLineColor(ex.Message, ConsoleColor.Red);
                return false;
            }

            return true;
        }
    }
}
