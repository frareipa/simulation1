using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoutingDaemon1.ServerInterface.Commands;

namespace RoutingDaemon1.ServerInterface.CommandHandlers
{
    public abstract class CommandHandlerBase
    {
        public abstract string HandleCommand(DaemonCommandBase command);
    }
}
