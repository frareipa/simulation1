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
            while (true)
            {
                try
                {
                    DaemonCommandBase daemand = ircServer.ReceiveCommand();
                    string Response = daemand.ExecuteCommand();
                    ircServer.SendResponse(Response);
                }
                catch { }
            }
        }

        /// <summary>
        /// Waits for routing daemon connections.
        /// </summary>
        private void WaitForLSAs()
        {
            byte[] buffer = new byte[1024];
            EndPoint EP = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                daemonUDPSocket.ReceiveFrom(buffer, ref EP);
            }
            catch
            {
            }
            LSA lsa = new LSA();
            lsa = Utilities.LSAUtility.CreateLSAFromByteArray(buffer);
            if(lsa.Type == LSAType.Advertisement)
            {
                LSA newLsa = Backend.DaemonBackEnd.Instance.UpdateBackEndWithLSA(lsa);
                if (newLsa == null)
                {
                    //flood the newLsa
                    foreach (Node n in Backend.DaemonBackEnd.Instance.LocalNode.Neighbors)
                    {
                        if (!(n.IsDown) && n.NodeID != newLsa.SenderNodeID)
                        {
                            EndPoint EPN = new IPEndPoint(IPAddress.Any, n.Configuration.RoutingPort);
                            try
                            {
                                daemonUDPSocket.SendTo(Utilities.LSAUtility.GetByteArrayFromLSA(newLsa), EPN);
                            }
                            catch
                            {
                            }
                        }
                    }
                    
                }
                else
                {
                    try
                    {
                        daemonUDPSocket.SendTo(Utilities.LSAUtility.GetByteArrayFromLSA(newLsa), EP);
                    }
                    catch
                    {
                    }
                }

            }
            else if (lsa.Type == LSAType.Acknowledgement)
            {
                Backend.DaemonBackEnd.Instance.GetNodeByID(lsa.SenderNodeID).IsAcknowledged = true;
                //foreach (Node n in Backend.DaemonBackEnd.Instance.LocalNode.Neighbors)
                //{
                //    //question
                //    if (lsa.SenderNodeID != n.NodeID)
                //    {
                //        Byte[] Ackbuffer =  Utilities.LSAUtility.CreateAckLSAFromLSA(lsa);
                //        EndPoint EPN = new IPEndPoint(IPAddress.Any, n.Configuration.RoutingPort);
                //        daemonUDPSocket.SendTo(Ackbuffer, EPN);

                //    }
                //}               
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
                    EndPoint EPN = new IPEndPoint(IPAddress.Any, n.Configuration.RoutingPort);
                    /**************************************************************/
                    try
                    {
                        daemonUDPSocket.SendTo(Utilities.LSAUtility.GetByteArrayFromLSA(localLsa), EPN);
                    }
                    catch
                    {
                    }

                    n.IsAcknowledged = false;

                    //wait for ack
                    Thread WaitForAckThread = new Thread(new ThreadStart(this.WaitForAck));
                    WaitForAckThread.Start();
                }
            }
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Waits for acknowledgment LSA from all neighbors.
        /// </summary>
        private void WaitForAck()
        {

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
                foreach (Node n in Backend.DaemonBackEnd.Instance.LocalNode.Neighbors)
                {
                    if (n.IsDown == false && n.IsAcknowledged == false)
                    {
                        
                        EndPoint EPN = new IPEndPoint(IPAddress.Any, n.Configuration.RoutingPort);
                        try
                        {
                            daemonUDPSocket.SendTo(Utilities.LSAUtility.GetByteArrayFromLSA(localLsa), EPN);
                        }
                        catch
                        {
                        }

                        n.IsAcknowledged = false;

                        //wait for ack
                        Thread WaitForAckThread = new Thread(new ThreadStart(this.WaitForAck));
                        WaitForAckThread.Start();
                    }
                }
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
                foreach (Node n in Backend.DaemonBackEnd.Instance.LocalNode.Neighbors)
                {
                    if (DateTime.Now.Subtract(n.LastUpdateTime).TotalMilliseconds> NeighborTimeout)
                        n.IsDown = true;
                }
                Thread.Sleep(NeighborTimeout);
            }
            //throw new NotImplementedException();
        }
    }
}
