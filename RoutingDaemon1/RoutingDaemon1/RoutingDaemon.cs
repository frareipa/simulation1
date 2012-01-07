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
            daemonUDPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            daemonUDPSocket.Bind(ServerEP1);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            this.daemonTCPSocket.Listen(100);
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
            while (true)
            {
                try
                {

                    DaemonCommandBase daemand = ircServer.ReceiveCommand();
                    string Response = daemand.ExecuteCommand();
                    ircServer.SendResponse(Response);
                }
                catch { continue; }
            }
        }

        /// <summary>
        /// Waits for routing daemon connections.
        /// </summary>
        private void WaitForLSAs()
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                EndPoint EP = new IPEndPoint(IPAddress.Any, 0);
                try
                {
                    daemonUDPSocket.ReceiveFrom(buffer, ref EP);
                }
                catch
                {
                    continue;
                }
                LSA lsa;
                lsa = Utilities.LSAUtility.CreateLSAFromByteArray(buffer);
                if (Backend.DaemonBackEnd.Instance.GetNodeByID(lsa.SenderNodeID).IsDown == true)
                {
                    Logger.Instance.Debug("LSA Deliver from down node");
                    Backend.DaemonBackEnd.Instance.GetNodeByID(lsa.SenderNodeID).IsDown = false;
                }

                if (lsa.Type == LSAType.Advertisement)
                {
                    LSA newLsa = Backend.DaemonBackEnd.Instance.UpdateBackEndWithLSA(lsa);
                    if (newLsa == null)
                    {
                        Logger.Instance.Debug("LSA Deliver new lsa advertisment");
                        daemonUDPSocket.SendTo(Utilities.LSAUtility.CreateAckLSAFromLSA(lsa), EP);
                        foreach (Node n in Backend.DaemonBackEnd.Instance.LocalNode.Neighbors)
                        {
                            if (!(n.IsDown) && n.NodeID != lsa.SenderNodeID)
                            {
                                try
                                {
                                    daemonUDPSocket.SendTo(Utilities.LSAUtility.GetByteArrayFromLSA(lsa), n.Configuration.GetNodeEndPoint());
                                   //n.IsAcknowledged = false;
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }

                    }
                    else if (newLsa != null)// && newLsa.SequenceNumber != lsa.SequenceNumber)
                    {
                        Logger.Instance.Debug("LSA Deliver old number lsa");
                        try
                        {
                            daemonUDPSocket.SendTo(Utilities.LSAUtility.GetByteArrayFromLSA(newLsa), EP);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    else
                    {
                        Logger.Instance.Debug("LSA Deliver same lsa number and i send ack");
                       daemonUDPSocket.SendTo(Utilities.LSAUtility.CreateAckLSAFromLSA(newLsa), EP);
                    }

                }
                else if (lsa.Type == LSAType.Acknowledgement)
                {
                    Logger.Instance.Debug("LSA Deliver type acknowledgement");
                    Backend.DaemonBackEnd.Instance.GetNodeByID(lsa.SenderNodeID).IsAcknowledged = true;
                    Backend.DaemonBackEnd.Instance.UpdateRoutingTable();
                }
            }
        }

        /// <summary>
        /// Broadcasts the local LSA to all neighbors.
        /// </summary>
        private void BroadcastLSA()
        {
            while (true)
            {
                Thread.Sleep(AdvertisementCycleTime);
                Backend.DaemonBackEnd.Instance.LocalNode.LastSequenceNumber += 1;
                LSA localLsa = Backend.DaemonBackEnd.Instance.GetLocalNodeLSA();
                foreach (Node n in Backend.DaemonBackEnd.Instance.LocalNode.Neighbors)
                {
                    if (!n.IsDown)
                    {
                        EndPoint EPN = n.Configuration.GetNodeEndPoint();
                        /**************************************************************/
                        try
                        {
                            daemonUDPSocket.SendTo(Utilities.LSAUtility.GetByteArrayFromLSA(localLsa), EPN);
                            Logger.Instance.Debug("i send new lsa to broad cast to port" + n.Configuration.RoutingPort.ToString());
                        }
                        catch
                        {
                            continue;
                        }

                        n.IsAcknowledged = false;
                    }
                    //wait for ack
                }

                Thread WaitForAckThread = new Thread(new ThreadStart(this.WaitForAck));
                WaitForAckThread.Start();
            }
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Waits for acknowledgment LSA from all neighbors.
        /// </summary>
        private void WaitForAck()
        {
            int OldLsa = Backend.DaemonBackEnd.Instance.LocalNode.LastSequenceNumber;
            while (true)
            {
                //To DO:  new boardcase Lsa i want to break?????
                Thread.Sleep(RetransmissionTimeout);
                //byte[] buffer = new byte[1024];
                //EndPoint EP = new IPEndPoint(IPAddress.Any, 0);
                //daemonUDPSocket.ReceiveFrom(buffer, ref EP);
                //LSA Alsa = new LSA();
                //Alsa = Utilities.LSAUtility.CreateLSAFromByteArray(buffer);
                LSA localLsa = Backend.DaemonBackEnd.Instance.GetLocalNodeLSA();
                bool allAkc = true;
                foreach (Node n in Backend.DaemonBackEnd.Instance.LocalNode.Neighbors)
                {
                    if (!n.IsDown && !n.IsAcknowledged && n.LastSequenceNumber == OldLsa)
                    {
                        allAkc = false;
                        //******************** until   acknowledged, or a newer LSA has been issued by the BroadcastLSA thread and This thread does not wait for acknowledgments for flooded LSAs.
                        // EndPoint EPN = new IPEndPoint(IPAddress.Any, n.Configuration.RoutingPort);
                        try
                        {
                            Logger.Instance.Debug("not reseive ack i send lsa again");
                            daemonUDPSocket.SendTo(Utilities.LSAUtility.GetByteArrayFromLSA(localLsa), n.Configuration.GetNodeEndPoint());
                        }
                        catch
                        {
                            continue;
                        }

                        n.IsAcknowledged = false;
                    }
                }
                if (allAkc)
                    return;

            }
            // Handle retransmission
        }

        /// <summary>
        /// Marks the neighbor as down.
        /// </summary>
        private void MarkNeighborAsDown()
        {
            while (true)
            {
                Thread.Sleep(NeighborTimeout);
                foreach (Node n in Backend.DaemonBackEnd.Instance.LocalNode.Neighbors)
                {
                    if (DateTime.Now.Subtract(n.LastUpdateTime).TotalMilliseconds >= NeighborTimeout && !n.IsDown)
                    {
                        n.IsDown = true;
                        Backend.DaemonBackEnd.Instance.UpdateRoutingTable();
                        Logger.Instance.Debug("node number " + n.NodeID.ToString() + " is down ");
                    }
                }
            }
        }
    }
}
