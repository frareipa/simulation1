// -----------------------------------------------------------------------
// <copyright file="NICKCommandHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using IRCServer1.Entities.Commands;
using IRCServer1.Entities;
using IRCServer1.Utilities;
using IRCServer1.Backend;
using System.Threading;
namespace IRCServer1.CommandHandlers
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
            if (command is NICKCommand)
            {
                NICKCommand nickCommand = (NICKCommand)command;
                if (nickCommand.NickName == "")
                {
                    return Errors.GetErrorResponse(ErrorCode.ERR_NONICKNAMEGIVEN, null);
                }
                else
                {
                    foreach (User user in ServerBackend.Instance.Users)
                    {
                        if (user.Nickname == nickCommand.NickName)
                        {
                            return Errors.GetErrorResponse(ErrorCode.ERR_NICKNAMEINUSE, null);
                        }
                    }

                }


                foreach (User user in ServerBackend.Instance.Users)
                {
                    if (user == session.User)
                    {
                        user.Nickname = nickCommand.NickName;
                        session.User.Nickname = nickCommand.NickName;
                        session.ConnectionState = ConnectionState.Registered;
                        return "OK";
                    }
                }
                session.User = new User();
                session.User.Nickname = nickCommand.NickName;
                ServerBackend.Instance.Users.Add(session.User);
                session.ConnectionState = ConnectionState.NotRegistered;
                return "OK";
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}