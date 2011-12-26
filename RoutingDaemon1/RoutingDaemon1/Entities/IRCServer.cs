using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using RoutingDaemon1.ServerInterface.Commands;
using RoutingDaemon1.Utilities;
using IRC.Utilities;

namespace RoutingDaemon1.Entities
{
    class IRCServer
    {
        Socket ircServerSocket;
        byte[] buffer;
        const int maxCommandSize = 1024;
        public IRCServer(Socket daemonSocket)
        {
            ircServerSocket = daemonSocket.Accept();
        }
        public void SendResponse(string response)
        {
            buffer = Encoding.ASCII.GetBytes(response);
            ircServerSocket.Send(buffer);
        }
        public DaemonCommandBase ReceiveCommand()
        {
            buffer = new byte[maxCommandSize];
            int length = ircServerSocket.Receive(buffer);
            if (length == 0)//IRC server terminated connection
            {
                return null;
            }
            string message = Encoding.ASCII.GetString(buffer, 0, length);
            Logger.Instance.Warn(string.Format("Received command {0} from IRC server.", message));
            //Parse the command
            DaemonCommandBase command;
            command = CommandFactory.GetCommandFromMessage(message);
            return command;
        }
    }
}
