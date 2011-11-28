using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICRServer.Entities;

namespace ICRServer.Backend
{
    public class ServerBackend
    {
        private static ServerBackend instance;

        private ServerBackend() { }

        public static ServerBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServerBackend();
                    instance.Users = new List<Entities.User>();
                    instance.ClientSessions = new List<Entities.Session>();
                }
                return instance;
            }
        }

        public List<Entities.User> Users { get; set; }
        /// <summary>
        /// Gets or sets the client sessions.
        /// </summary>
        /// <value>
        /// The client sessions.
        /// </value>
        public List<Session> ClientSessions { get; set; }
    }
}
