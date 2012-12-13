using System.Net;

namespace BarrierServerProject
{
    public class User
    {
       private string m_username; //0 - from users 1 - for module
       private int m_userid;
       private IPAddress m_ipaddress;
       private int m_port;

        public string username
        {
            get { return m_username; }

            set { m_username = value; }
        }

        public int userid
        {
            get { return m_userid; }

            set { m_userid = value; }
        }

        public IPAddress ipaddress
        {
            get { return m_ipaddress; }

            set { m_ipaddress = value; }
        }

        public int port
        {
            get { return m_port; }

            set { m_port = value; }
        }
    }
}

