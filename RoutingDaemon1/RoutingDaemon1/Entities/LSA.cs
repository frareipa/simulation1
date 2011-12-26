using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoutingDaemon1.Entities
{
    public enum LSAType
    {
        Advertisement = 0,
        Acknowledgement = 1
    };

    public class LSA
    {
        public LSA()
        {
            this.Users = new List<User>();
            this.Links = new List<Node>();
        }

        public int Version { get; set; }

        public LSAType Type { get; set; }

        public int TTL { get; set; }

        public int SenderNodeID { get; set; }

        public int SequenceNumber { get; set; }

        public List<Node> Links { get; set; }

        public List<User> Users { get; set; }
    }
}
