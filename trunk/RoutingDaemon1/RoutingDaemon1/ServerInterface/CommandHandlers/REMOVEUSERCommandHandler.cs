// -----------------------------------------------------------------------
// <copyright file="REMOVEUSERCommandHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using RoutingDaemon1.ServerInterface.Commands;
using RoutingDaemon1.Entities;
using RoutingDaemon1.Utilities;
using RoutingDaemon1.Backend;
namespace RoutingDaemon1.ServerInterface.CommandHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class REMOVEUSERCommandHandler : CommandHandlerBase
    {
        public override string HandleCommand(DaemonCommandBase command)
        {
            if (command is REMOVEUSERCommand)
            {
                REMOVEUSERCommand RemoveUserCommand = (REMOVEUSERCommand)command;
                if (RemoveUserCommand.NickName == null)
                    return "ERROR";
                User n=new User();
                n.Nickname=RemoveUserCommand.NickName;
                bool found = DaemonBackEnd.Instance.LocalNode.Users.Contains(n);
                if (found)
                {
                    DaemonBackEnd.Instance.LocalNode.Users.Remove(n);
                }
                                return "OK";
                   }
            else
            {
                return "ERROR";
            }

        }
    }
}
