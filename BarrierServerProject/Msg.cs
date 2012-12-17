using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace BarrierServerProject
{
    class Msg
    {
        public static bool SendUser(string user_name, string msg)
        {
            try
            {
                int size = msg.Length;

                msg = size + "|" + msg;

                byte[] bytes = new byte[256];  //TODO CHECK THIS SIZE
                bytes = Encoding.UTF8.GetBytes(msg);

                foreach (DictionaryEntry de in Server.clients)
                {
                    if ((de.Value).ToString() == user_name)
                        Server.MessageSender((Socket)de.Key, bytes);
                }
            }
            catch (System.Exception ex)
            {
                //parse this error and other send to administrator 
                Log.log_write(ex.Message, "Exception", "exception");
                Color.WriteLineColor(ex.Message, "Red");
                return false;
            }

            return true;
        }

        //TODO send to group users
    }
}
