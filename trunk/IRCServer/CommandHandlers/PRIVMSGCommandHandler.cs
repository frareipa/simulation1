// -----------------------------------------------------------------------
// <copyright file="PRIVMSGCommandHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using ICRServer.Entities.Commands;
using ICRServer.Entities;
using ICRServer.Utilities;
using ICRServer.Backend;
using System.Threading;
namespace ICRServer.CommandHandlers
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
   class PRIVMSGCommandHandler : CommandHandlerBase
    {
        public override string HandleCommand(IRCCommandBase command, Session session)
        {
            if (command is PRIVMSGCommand)
            {
                PRIVMSGCommand privmsgCommand = (PRIVMSGCommand)command;
                ServerBackend.Instance.Users.Remove(session.User);
                session.ConnectionState = ConnectionState.Destroyed;
                ServerBackend.Instance.ClientSessions.Remove(session);
                return String.Empty;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
