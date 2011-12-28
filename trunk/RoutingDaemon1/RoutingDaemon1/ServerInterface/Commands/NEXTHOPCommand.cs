// -----------------------------------------------------------------------
// <copyright file="NEXTHOPCommand.cs" company="">
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
    public class NEXTHOPCommand :DaemonCommandBase 
    {
            //Parameters: <username> <hostname> <servername> <realname>
        public NEXTHOPCommand(string[] parameters) 
        {
            if (parameters.Length > 0)
            {
                this.NickName = parameters[0].Split('\0')[0];
            }
        }

        public string NickName { get; set; }

        public override string ExecuteCommand()
        {
            ServerInterface.CommandHandlers.NEXTHOPCommandHandler handler = new ServerInterface.CommandHandlers.NEXTHOPCommandHandler();
            return handler.HandleCommand(this);
        }
    }
}
