using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using IRCServer1.Entities;
using IRCServer1.Utilities;
using IRCServer1.Entities.Commands;
using IRCServer1.Backend;
using IRC.Utilities;
using IRC.Utilities.Entities;

namespace IRCServer1
{
    class IRCServer1 : IServer
    {

        #region IServer Members
        Socket serversock;
        Socket clientsock;
       public  int Port;
        public void Start()
        {
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Any, this.Port);
            serversock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serversock.Bind(serverEP);
            serversock.Listen(100);
            while (1 == 1)
            {
                this.WaitForConnections();
                System.Threading.Thread.Sleep(30000);
            }
        }

        public void WaitForConnections()
        {
            serversock.BeginAccept(new AsyncCallback(AcceptConnection), serversock);
        }

        public void AcceptConnection(IAsyncResult result)
        {
            serversock = (Socket)result.AsyncState;
            clientsock = serversock.EndAccept(result);
            Session newSession = new Session();
            newSession.Socket = clientsock;
            Backend.ServerBackend.Instance.ClientSessions.Add(newSession);
           // newSession.Buffer[0] = 0;
            newSession.Buffer = new byte[1024];
            newSession.Socket.BeginReceive(newSession.Buffer, 0, newSession.Buffer.Length,SocketFlags.None, new AsyncCallback(ReceiveCommand), newSession);

        }

        public void ReceiveCommand(IAsyncResult result)
        {
            Session newsession = (Session)result.AsyncState;
            newsession.Socket.EndReceive(result);
            if (Encoding.ASCII.GetString(newsession.Buffer).Split('\0').Length == 0)
            {
                IRCCommandBase q = new QUITCommand(null);
                q.ExecuteCommand(newsession);
            }
            else
            {
                string response;
                try
                {
                     response = CommandFactory.GetCommandFromMessage(Encoding.ASCII.GetString(newsession.Buffer), newsession).ExecuteCommand(newsession);
                    newsession.Socket.BeginSend(Encoding.ASCII.GetBytes(response), 0, Encoding.ASCII.GetBytes(response).Length, SocketFlags.None, new AsyncCallback(FinalizeSending), newsession);
                }
                catch
                {
                    newsession.Socket.BeginSend(newsession.Buffer, 0, newsession.Buffer.Length, SocketFlags.None, new AsyncCallback(FinalizeSending), newsession);
                }
                   
            }
        }

        public void FinalizeSending(IAsyncResult result)
        {
            Session newSession = (Session)result.AsyncState;
            newSession.Socket.EndSend(result);
            newSession.Buffer = new byte[newSession.Buffer.Length];
          try
          {
                newSession.Socket.BeginReceive(newSession.Buffer, 0, newSession.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCommand), newSession);
          }
            catch
          {
                newSession.Socket.Close();
            }

          }

        }

        #endregion

    }