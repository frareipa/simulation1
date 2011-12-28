using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoutingDaemon1.Entities;
using IRC.Utilities.Entities;

namespace RoutingDaemon1.Backend
{
    /// <summary>
    /// Routing Daemon Backend
    /// </summary>
    public class DaemonBackEnd
    {
        /// <summary>
        /// Static instance (Singleton).
        /// </summary>
        private static DaemonBackEnd instance;

        private const int DefaultVersion = 1;
        private const int DefaultTTL = 32;

        private List<Node> allNodes;

        private DaemonBackEnd()
        {
            allNodes = new List<Node>();
            LocalNode = new Node();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static DaemonBackEnd Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DaemonBackEnd();
                }
                return instance;
            }
        }

        /// <summary>
        /// Gets or sets the local node.
        /// </summary>
        /// <value>
        /// The local node.
        /// </value>
        public Node LocalNode { get; set; }

        /// <summary>
        /// Gets or sets the routing table.
        /// </summary>
        /// <value>
        /// The routing table.
        /// </value>
        public List<RoutingTableEntry> RoutingTable { get; set; }

        /// <summary>
        /// Gets or sets the configuration of the local node.
        /// </summary>
        /// <value>
        /// The configuration of the node.
        /// </value>
        public List<NodeConfiguration> Configuration { get; set; }

        /// <summary>
        /// Updates the routing table using Dijkstra.
        /// </summary>
        public void UpdateRoutingTable()
        {
            DijkstraMapper mapper = new DijkstraMapper(allNodes);
            mapper.Run();
            this.RoutingTable = mapper.GetRoutingTable();
        }

        /// <summary>
        /// Gets the node by ID. If the node doesn't exist it creates it.
        /// </summary>
        /// <param name="nodeID">The node ID.</param>
        /// <returns>The Node object.</returns>
        public Node GetNodeByID(int nodeID)
        {
            Node node = this.allNodes.FirstOrDefault(n => n.NodeID == nodeID);

            if (node == null)
            {
                node = new Node() { NodeID = nodeID };
                this.allNodes.Add(node);
            }

            return node;
        }

        /// <summary>
        /// Updates the network with a newly received LSA (Link State Announcement).
        /// </summary>
        /// <param name="lsa">The LSA.</param>
        /// <returns>Null on successful update, and an LSA if the sequence number is older than the latest received from that sender.</returns>
        public LSA UpdateBackEndWithLSA(LSA lsa)
        {
            Node SenderNode = GetNodeByID(lsa.SenderNodeID);
            if (lsa.SequenceNumber > GetNodeByID(lsa.SenderNodeID).LastSequenceNumber)
            {
              
                this.allNodes = lsa.Links;
                this.LocalNode.Users = lsa.Users;
                this.UpdateRoutingTable();
                return null;
            }
            else
            {
                return lsa;
            }
            //foreach (Node n in this.allNodes)
            //{
            //    if (n.NodeID == lsa.SenderNodeID)
            //    {
            //        if (lsa.SequenceNumber < n.LastSequenceNumber)
            //        {
                       
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Configures the local node using the info in the configuration file.
        /// </summary>
        /// <param name="configuration">The configuration loaded from the configuration file.</param>
        public void ConfigureLocalNode(List<NodeConfiguration> configuration)
        {
            //how can i know the local one is it the first one or WHAT?
            this.LocalNode.Configuration = configuration[0];
            this.LocalNode.LastSequenceNumber = 0;
            this.LocalNode.NodeID = configuration[0].NodeID;
            foreach (NodeConfiguration temp in configuration)
            {
                if (!(temp.NodeID == LocalNode.NodeID))
                {
                   Node n=GetNodeByID(temp.NodeID);
                }
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the local node LSA.
        /// </summary>
        /// <returns>The LSA of the local node.</returns>
        public LSA GetLocalNodeLSA()
        {
            LSA lsa = new LSA();
            lsa.SenderNodeID = this.LocalNode.NodeID;
            lsa.SequenceNumber = this.LocalNode.LastSequenceNumber + 1;
            lsa.Type = LSAType.Advertisement;
            lsa.Users = this.LocalNode.Users;
            lsa.Links = this.LocalNode.Neighbors;
            lsa.Version = 1;
            lsa.TTL = 32;
            return lsa;

           // throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all nodes.
        /// </summary>
        /// <param name="rootNode">The root node.</param>
        /// <returns>List of all nodes mapped.</returns>
        private List<Node> GetAllNodes(Node rootNode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Explores the node recursively updating the list of nodes.
        /// </summary>
        /// <param name="node">The node to explore.</param>
        /// <param name="nodes">The nodes list to update.</param>
        private void ExploreNode(Node node, List<Node> nodes)
        {
            throw new NotImplementedException();
        }
    }
}
