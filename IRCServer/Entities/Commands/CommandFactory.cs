using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICRServer.Utilities;

namespace ICRServer.Entities.Commands
{
    public class CommandFactory
    {
        public static IRCCommandBase GetCommandFromMessage(string message, Session session)
        {
            string[] parameters = Utilities.CommandParser.GetParameters(message);

            if (parameters.Length == 0)
                throw new InvalidOperationException();

            IRCCommandType? type = CommandParser.GetIRCCommandFromString(parameters[0]);

            if (!type.HasValue)
                throw new InvalidOperationException();

            string[] arguments = parameters.Skip(1).ToArray(); 

            switch (type.Value)
            {
                case IRCCommandType.NICK:
                    throw new NotImplementedException("NICK not implemented");
                case IRCCommandType.USER:
                    throw new NotImplementedException("USER not implemented");
                default:
                    break;
            }

            if (session.ConnectionState == ConnectionState.Registered)
            {
                switch (type.Value)
                {
                    case IRCCommandType.PRIVMSG:
                        throw new NotImplementedException("PRIVMSG not implemented");
                    case IRCCommandType.QUIT:
                        return new QUITCommand(arguments.ToArray());
                    default:
                        throw new InvalidOperationException();
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
