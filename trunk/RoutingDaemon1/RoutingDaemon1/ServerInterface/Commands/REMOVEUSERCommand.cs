// -----------------------------------------------------------------------
// <copyright file="REMOVEUSERCommand.cs" company="Fcis">
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
   
    public class REMOVEUSERCommand : DaemonCommandBase
    {
        public REMOVEUSERCommand(string[] parameters) 
        {
            if (parameters.Length > 0)
            {
                this.NickName = parameters[0].Split('\0')[0];
            }
        }

        public string NickName { get; set; }
        public override string ExecuteCommand()
        {
            ServerInterface.CommandHandlers.REMOVEUSERCommandHandler handler = new ServerInterface.CommandHandlers.REMOVEUSERCommandHandler();
            return handler.HandleCommand(this);
        }
    }
}