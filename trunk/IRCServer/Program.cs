using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using ICRServer.Entities;
using IRC.Utilities;

namespace ICRServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Instance.Verbose = true;
            // Start your server using port number passed in the args array.
            ICRServer server = new ICRServer();
            server.Port = int.Parse(args[0]);
            server.Start();

        }
    }
}
