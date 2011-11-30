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
                session.User.Username = userCommand.UserName;
                session.User.Hostname = userCommand.HostName;
                session.User.Realname = userCommand.RealName;
                session.ConnectionState = ConnectionState.NotRegistered; 

                ServerBackend.Instance.Users.Add(session.User);
                return "Done";
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
    }
