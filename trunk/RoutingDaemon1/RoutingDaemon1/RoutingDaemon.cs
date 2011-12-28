using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using RoutingDaemon1.Entities;
using RoutingDaemon1.ServerInterface.Commands;
using RoutingDaemon1.Utilities;
using IRC.Utilities;

namespace RoutingDaemon1
{
    public class RoutingDaemon1
    {
        private const int MAXMESSAGESIZE = 1024;
        private const int Second = 1000;
        private const int AdvertisementCycleTime = 30 * Second;
        private const int NeighborTimeout = 120 * Second;
        private const int RetransmissionTimeout = 3 * Second;
        private Socket daemonTCPSocket;
        private Socket daemonUDPSocket;
        private IRCServer ircServer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoutingDaemon1"/> class.
        /// </summary>
        /// <param name="routingPort">The UDP port on the routing daemon used to exchange routing information with other routing daemons.</param>
        /// <param name="localPort">The TCP port on the routing daemon that is used to exchange information between it and the local IRC server.</param>
        public RoutingDaemon1(int routingPort, int localPort)
        {
            Logger.Instance.Debug("Initializing Routing Daemon Sockets");

            // Initialize the socket that will wait for commands from the local IRC server.
            IPEndPoint ServerEP = new IPEndPoint(IPAddress.Any, localPort); 
            daemonTCPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            daemonTCPSocket.Bind(ServerEP);

            // Initialize the socket that will communicate with other routing daemon nodes.
            IPEndPoint ServerEP1 = new IPEndPoint(IPAddress.Any, routingPort);
            daemonUDPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
            daemonTCPSocket.Bind(ServerEP1);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            this.daemonTCPSocket.Listen(10);

            // Start a thread for listening on TCP port
            Thread serverConnectionThread = new Thread(new ThreadStart(this.WaitForIRCServerConnection));
            serverConnectionThread.Start();

            // Start a thread waiting to receive LSAs
            Thread receiveLSAThread = new Thread(new ThreadStart(this.WaitForLSAs));
            receiveLSAThread.Start();

            // Start a thread that sends LSAs every 30 seconds
            Thread broadcastLSAsThread = new Thread(new ThreadStart(this.BroadcastLSA));
            broadcastLSAsThread.Start();

            // Start a thread that checks for down neighbor
            Thread neighborDownTimeoutThread = new Thread(new ThreadStart(this.MarkNeighborAsDown));
            neighborDownTimeoutThread.Start();
        }

        /// <summary>
        /// Waits for IRC server connections.
        /// </summary>
        private void WaitForIRCServerConnection()
        {
            ircServer = new IRCServer(daemonTCPSocket);
        }

        /// <summary>
        /// Waits for routing daemon connections.
        /// </summary>
        private void WaitForLSAs()
        {
            byte[] buffer = new byte[1024];
            EndPoint EP = new IPEndPoint(IPAddress.Any, 0);
            daemonUDPSocket.ReceiveFrom(buffer, ref EP);
            LSA lsa = new LSA();
            lsa = Utilities.LSAUtility.CreateLSAFromByteArray(buffer);
            if(lsa.Type == LSAType.Advertisement)
            {
                LSA newLsa = Backend.DaemonBackEnd.Instance.UpdateBackEndWithLSA(lsa);
                if (newLsa == null)
                {

                }
                else
                {
                }

            }
        }

        /// <summary>
        /// Broadcasts the local LSA to all neighbors.
        /// </summary>
        private void BroadcastLSA()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Waits for acknowledgment LSA from all neighbors.
        /// </summary>
        private void WaitForAck()
        {
            // Handle retransmission
        }

        /// <summary>
        /// Marks the neighbor as down.
        /// </summary>
        private void MarkNeighborAsDown()
        {
            throw new NotImplementedException();
        }
    }
}
