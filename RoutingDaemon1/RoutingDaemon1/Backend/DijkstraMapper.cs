using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoutingDaemon1.Entities;

namespace RoutingDaemon1.Backend
{
    /// <summary>
    /// Network mapper using Dijkstra.
    /// </summary>
    public class DijkstraMapper
    {
        private int rank = 0;
        private IList<Node> allNodes;
        private int[,] links;
        private int[] currentNodes;
        private int[] previousNodes;
        public int[] distances;
        private int trank = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="DijkstraMapper"/> class.
        /// </summary>
        /// <param name="allNodes">All nodes.</param>
        public DijkstraMapper(IList<Node> allNodes)
        {
            this.allNodes = allNodes;
            int count = allNodes.Count;

            links = new int[count, count];
            currentNodes = new int[count];
            distances = new int[count];
            previousNodes = new int[count];

            rank = count;

            // Initializes the links matrix.
            for (int i = 0; i < rank; i++)
            {
                for (int j = 0; j < rank; j++)
                {
                    links[i, j] = (allNodes[i].Neighbors.Contains(allNodes[j])) ? 1 : -1;
                }
            }

            // Initializes the current node array, and the previous node array.
            for (int i = 0; i < rank; i++)
            {
                currentNodes[i] = i;
                previousNodes[i] = -1;
            }
            currentNodes[0] = -1;

            // Updates the distance array with the distances of the neighbors to the root node.
            for (int i = 1; i < rank; i++)
            {
                distances[i] = links[0, i];
                previousNodes[i] = (distances[i] == -1) ? -1 : 0;
            }
        }

        /// <summary>
        /// Solves one iteration of the Dijkstra algorithm.
        /// </summary>
        private void DijkstraSolving()
        {
            int minValue = Int32.MaxValue;
            int minNode = 0;
            for (int i = 0; i < rank; i++)
            {
                if (currentNodes[i] == -1)
                    continue;
                if (distances[i] > 0 && distances[i] < minValue)
                {
                    minValue = distances[i];
                    minNode = i;
                }
            }
            currentNodes[minNode] = -1;
            for (int i = 0; i < rank; i++)
            {
                if (links[minNode, i] < 0)
                    continue;
                if (distances[i] < 0)
                {
                    distances[i] = minValue + links[minNode, i];
                    previousNodes[i] = minNode;
                    continue;
                }
                if ((distances[minNode] + links[minNode, i]) < distances[i])
                {
                    distances[i] = minValue + links[minNode, i];
                    previousNodes[i] = minNode;
                }
            }
        }

        /// <summary>
        /// Runs the mapper to calculate the shortest paths.
        /// </summary>
        public void Run()
        {
            for (trank = 1; trank < rank; trank++)
            {
                DijkstraSolving();
            }
        }

        /// <summary>
        /// Gets the routing table.
        /// </summary>
        /// <returns>Routing table based on the mapped network.</returns>
        public List<RoutingTableEntry> GetRoutingTable()
        {
            List<RoutingTableEntry> routingTable = new List<RoutingTableEntry>();

            for (int i = 1; i < rank; i++)
            {
                RoutingTableEntry entry = new RoutingTableEntry();
                entry.Node = allNodes[i];
                entry.Distance = distances[i];

                if (entry.Distance == -1)
                    continue;

                int currentNode = i;

                while (previousNodes[currentNode] != 0)
                {
                    currentNode = previousNodes[currentNode];
                }

                if (currentNode != i)
                    entry.NextHop = allNodes[currentNode];

                routingTable.Add(entry);
            }

            return routingTable;
        }
    }
}
