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
                if (nickCommand.NickName == null)
                {
                    return Errors.GetErrorResponse(ErrorCode.ERR_NONICKNAMEGIVEN, null);
                }
                else
                {
                    foreach (Session S in ServerBackend.Instance.ClientSessions)
                    {
                        if (S.User != null)
                            if (S.User.Nickname == nickCommand.NickName)
                            {
                                if (S.Socket != session.Socket)
                                    return Errors.GetErrorResponse(ErrorCode.ERR_NICKNAMEINUSE, nickCommand.NickName);
                            }
                    }

                }


                foreach (User user in ServerBackend.Instance.Users)
                {
                    if (user == session.User)
                    {
                        if (user.Realname != null)
                        {
                            user.Nickname = nickCommand.NickName;
                            session.User.Nickname = nickCommand.NickName;
                            session.ConnectionState = ConnectionState.Registered;
                            return "OK";
                        }
                        else
                        {
                            user.Nickname = nickCommand.NickName;
                            session.User.Nickname = nickCommand.NickName;
                            session.ConnectionState = ConnectionState.NotRegistered;
                            return "OK";
                        }
                    }
                }
                if (session.User == null)
                {
                    session.User = new User();
                    session.User.Nickname = nickCommand.NickName;
                    ServerBackend.Instance.Users.Add(session.User);
                }

                return "OK";
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}