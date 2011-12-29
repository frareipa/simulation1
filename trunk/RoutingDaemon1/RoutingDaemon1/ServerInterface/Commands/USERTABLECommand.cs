// -----------------------------------------------------------------------
// <copyright file="USERTABLECommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace RoutingDaemon1.ServerInterface.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class USERTABLECommand : DaemonCommandBase
    {
            //Parameters: <username> <hostname> <servername> <realname>
        public USERTABLECommand(string[] parameters){}


        public override string ExecuteCommand()
        {
         ServerInterface.CommandHandlers.USERTABLECommandHandler handler = new ServerInterface.CommandHandlers.USERTABLECommandHandler();
            return handler.HandleCommand(this);
        }
    }
}
