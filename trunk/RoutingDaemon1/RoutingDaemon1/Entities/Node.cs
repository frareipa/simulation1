using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRC.Utilities.Entities;

namespace RoutingDaemon1.Entities
{
    public class Node
    {
        public Node()
        {
            Neighbors = new List<Node>();
            Users = new List<User>();
            LastUpdateTime = DateTime.Now;
        }

        public int NodeID { get; set; }

        public int LastSequenceNumber { get; set; }

        public bool IsAcknowledged { get; set; }

        public bool IsDown { get; set; }

        public List<Node> Neighbors { get; set; }

        public List<User> Users { get; set; }

        public NodeConfiguration Configuration { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public override bool Equals(object obj)
        {
            return ((Node)obj).NodeID == this.NodeID;
        }
    }
}
