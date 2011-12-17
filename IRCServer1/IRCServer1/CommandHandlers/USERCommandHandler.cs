// -----------------------------------------------------------------------
// <copyright file="USERCommandHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
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
                if(userCommand.UserName==null||userCommand.HostName==null||userCommand.ServerName==null||userCommand.RealName==null)
                {
                    return Errors.GetErrorResponse(ErrorCode.ERR_NEEDMOREPARAMS,"User");
                }
                if (session.User != null)
                {
                    if (session.User.Username != null)
                    {
                        return Errors.GetErrorResponse(ErrorCode.ERR_ALREADYREGISTERED, null);
                    }
                }
                //foreach (User u in ServerBackend.Instance.Users)
                //{
                //    if (u.Username == userCommand.UserName)
                //    {
                //        return Errors.GetErrorResponse(ErrorCode.ERR_ALREADYREGISTERED, null);
                //    }
                //}

                foreach (User user in ServerBackend.Instance.Users)
                {
                    if (user == session.User)
                    {
                        if (user.Nickname != null)
                        {
                            session.User.Username = userCommand.UserName;
                            session.User.Hostname = userCommand.HostName;
                            session.User.Realname = userCommand.RealName;
                            session.ConnectionState = ConnectionState.Registered;
                            return "OK";
                        }

                        else
                        {

                            session.User.Username = userCommand.UserName;
                            session.User.Hostname = userCommand.HostName;
                            session.User.Realname = userCommand.RealName;
                            session.ConnectionState = ConnectionState.NotRegistered;
                            return "OK";
                        }
                    }
                }
                if (session.User == null)
                {
                    session.User = new User();
                    session.User.Username = userCommand.UserName;
                    session.User.Hostname = userCommand.HostName;
                    session.User.Realname = userCommand.RealName;
                    session.ConnectionState = ConnectionState.NotRegistered;
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
