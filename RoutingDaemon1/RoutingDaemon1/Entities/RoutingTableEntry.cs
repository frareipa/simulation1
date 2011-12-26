using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoutingDaemon1.Entities
{
    public class RoutingTableEntry
    {
        public Node Node { get; set; }

        public Node NextHop { get; set; }

        public int Distance { get; set; }
    }
}
