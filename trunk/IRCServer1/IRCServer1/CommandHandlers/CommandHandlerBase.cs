using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRCServer1.Entities.Commands;
using IRCServer1.Entities;

namespace IRCServer1.CommandHandlers
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
