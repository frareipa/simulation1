using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace IRCServer1.Entities
{
    public enum ConnectionState
    {
        NotRegistered,
        Registered,
        Destroyed
    };

    public class Session
    {
        public Session()
        {
            this.ConnectionState = Entities.ConnectionState.NotRegistered;
            this.Buffer = new byte[1024];
        }

        public int ClientID { get; set; }

        public User User { get; set; }

        public Socket Socket { get; set; }

        public ConnectionState ConnectionState { get; set; }

        public byte[] Buffer { get; set; }
    }
}
