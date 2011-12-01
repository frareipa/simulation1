using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using IRCServer1.Entities;
using IRC.Utilities;

namespace IRCServer1
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Instance.Verbose = true;
            // Start your server using port number passed in the args array.

            IRCServer1 server = new IRCServer1();
            if (args.Length == 0)
            {
                server.Port = 9000;
            }
            else
            {
                server.Port = int.Parse(args[0]);
            }

            server.Start();

        }
    }
}
