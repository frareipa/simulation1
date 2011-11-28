using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICRServer.Entities.Commands;
using ICRServer.Entities;

namespace ICRServer.CommandHandlers
{
    abstract class CommandHandlerBase
    {
        public abstract string HandleCommand(IRCCommandBase command, Session session);

        protected void UpdateConnectionState(Session session)
        {
            if (session.User != null && session.User.Username != null && session.User.Nickname != null)
            {
                session.ConnectionState = ConnectionState.Registered;
            }
        }
    }
}
