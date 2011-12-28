// -----------------------------------------------------------------------
// <copyright file="USERCommandHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using RoutingDaemon1.Entities;
using RoutingDaemon1.Utilities;
using RoutingDaemon1.Backend;
using System.Threading;
using RoutingDaemon1.ServerInterface.Commands;

namespace RoutingDaemon1.ServerInterface.CommandHandlers
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    class ADDUSERCommandHandler : CommandHandlerBase
    {
        public override string HandleCommand(DaemonCommandBase command)
        {
            if (command is ADDUSERCommand)
            {
                ADDUSERCommand AddUserCommand = (ADDUSERCommand)command;
                if (AddUserCommand.NickName == null)
                {
                    return "Error";
                }
                User n = new User();
                n.Nickname = AddUserCommand.NickName;
               bool found= DaemonBackEnd.Instance.LocalNode.Users.Contains(n);
               if (found == false)
               {
                   DaemonBackEnd.Instance.LocalNode.Users.Add(n);
               }
                return "ok";
            }
            else
            {
                return "Error";
            }
        }
    }
}
