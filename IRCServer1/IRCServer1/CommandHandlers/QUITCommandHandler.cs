using System;
using System.Collections.Generic;
using System.Text;
using IRCServer1.Entities.Commands;
using IRCServer1.Entities;
using IRCServer1.Utilities;
using IRCServer1.Backend;
using System.Threading;

namespace IRCServer1.CommandHandlers
{
    class QUITCommandHandler : CommandHandlerBase
    {
        public override string HandleCommand(IRCCommandBase command, Session session)
        {
            if (command is QUITCommand)
            {
                QUITCommand quitCommand = (QUITCommand)command;
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
