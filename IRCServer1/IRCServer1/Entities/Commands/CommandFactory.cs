﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRCServer1.Utilities;

namespace IRCServer1.Entities.Commands
{
    public class CommandFactory
    {
        public static IRCCommandBase GetCommandFromMessage(string message, Session session)
        {
            string[] parameters = Utilities.CommandParser.GetParameters(message);

            if (parameters.Length == 0)
            {
                session.Buffer = Encoding.ASCII.GetBytes(Errors.GetErrorResponse(ErrorCode.ERR_UNKNOWNCOMMAND, null));
                return null;
            }
            IRCCommandType? type = CommandParser.GetIRCCommandFromString(parameters[0]);

            if (!type.HasValue)
            {
               session.Buffer= Encoding.ASCII.GetBytes(Errors.GetErrorResponse(ErrorCode.ERR_UNKNOWNCOMMAND, parameters[0]));
               return null;
            }

            string[] arguments = parameters.Skip(1).ToArray(); 

            switch (type.Value)
            {
                case IRCCommandType.NICK:
                    return new NICKCommand(arguments.ToArray());
                case IRCCommandType.USER:
                    return new USERCommand(arguments.ToArray());
                default:
                    break;
            }

            if (session.ConnectionState == ConnectionState.Registered)
            {
                switch (type.Value)
                {
                    case IRCCommandType.PRIVMSG:
                        return new PRIVMSGCommand(arguments.ToArray());
                    case IRCCommandType.QUIT:
                        return new QUITCommand(arguments.ToArray());
                    default:
                        throw new InvalidOperationException();
                }
            }
            else
            {
                switch (type.Value)
                {
                    case IRCCommandType.QUIT:
                        return new QUITCommand(arguments.ToArray());
                    case IRCCommandType.PRIVMSG:
                        {
                            session.Buffer = Encoding.ASCII.GetBytes(Errors.GetErrorResponse(ErrorCode.ERR_UNKNOWNCOMMAND, null));
                            return null;
                        }
                }
                return null;
            }
        }
    }
}