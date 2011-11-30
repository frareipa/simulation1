// -----------------------------------------------------------------------
// <copyright file="USERCommandHandler.cs" company="">
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
    class USERCommandHandler : CommandHandlerBase
    {
        public override string HandleCommand(IRCCommandBase command, Session session)
        {
            if (command is USERCommand)
            {
                USERCommand userCommand = (USERCommand)command;
                foreach (User u in ServerBackend.Instance.Users)
                {
                    if (u.Username == userCommand.UserName)
                    {
                        return Errors.GetErrorResponse(ErrorCode.ERR_ALREADYREGISTERED, null);
                    }
                }

                foreach (User user in ServerBackend.Instance.Users)
                {
                    if (user == session.User)
                    {
                        session.User.Username = userCommand.UserName;
                        session.User.Hostname = userCommand.HostName;
                        session.User.Realname = userCommand.RealName;
                        session.ConnectionState = ConnectionState.Registered;
                        return "OK";
                    }
                }
                session.User.Username = userCommand.UserName;
                session.User.Hostname = userCommand.HostName;
                session.User.Realname = userCommand.RealName;
                session.ConnectionState = ConnectionState.NotRegistered; 

                ServerBackend.Instance.Users.Add(session.User);
                return "OK";
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
    }
