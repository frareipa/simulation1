// -----------------------------------------------------------------------
// <copyright file="NICKCommandHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using ICRServer.Entities.Commands;
using ICRServer.Entities;
using ICRServer.Utilities;
using ICRServer.Backend;
using System.Threading;
namespace ICRServer.CommandHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
     class NICKCommandHandler : CommandHandlerBase
    {
         public override string HandleCommand(IRCCommandBase command, Session session)
         {
             if (command is QUITCommand)
             {
                 NICKCommand nickCommand = (NICKCommand)command;
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
