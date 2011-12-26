using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoutingDaemon1.Utilities;

namespace RoutingDaemon1.ServerInterface.Commands
{
    class CommandFactory
    {
        public static DaemonCommandBase GetCommandFromMessage(string message)
        {
            string[] parameters = Utilities.CommandParser.GetParameters(message);

            if (parameters.Length == 0)
                throw new InvalidOperationException();

            DaemonCommandType? type = CommandParser.GetIRCCommandFromString(parameters[0]);

            if (!type.HasValue)
                throw new InvalidOperationException();

            string[] arguments = parameters.Skip(1).ToArray();

            switch (type.Value)
            {
                case DaemonCommandType.ADDUSER:
                    throw new NotImplementedException();
                case DaemonCommandType.REMOVEUSER:
                    throw new NotImplementedException();
                case DaemonCommandType.NEXTHOP:
                    throw new NotImplementedException();
                case DaemonCommandType.USERTABLE:
                    throw new NotImplementedException();
                default:
                    break;
            }

            throw new InvalidOperationException();
        }
    }
}
