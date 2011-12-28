using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoutingDaemon1.ServerInterface.Commands
{
  public  abstract class DaemonCommandBase
    {
        public abstract string ExecuteCommand();
    }
}
