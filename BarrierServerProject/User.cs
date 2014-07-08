using System.Net;

namespace BarrierServerProject
{
    public class User
    {
        public string username { get; set; }
        public int userid { get; set; }
        public IPAddress ipaddress { get; set; }
        public int port { get; set; }
    }
}

