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
                        return "OK";
                    }
                }
                session.User.Nickname = nickCommand.NickName;
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