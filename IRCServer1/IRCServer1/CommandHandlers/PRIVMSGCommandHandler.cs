// -----------------------------------------------------------------------
// <copyright file="PRIVMSGCommandHandler.cs" company="">
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
   class PRIVMSGCommandHandler : CommandHandlerBase
    {
        public override string HandleCommand(IRCCommandBase command, Session session)
        {
            if (command is PRIVMSGCommand)
            {

                if (session.ConnectionState == ConnectionState.NotRegistered)
                {
                    return "Unregistered user";
                }
                PRIVMSGCommand privmsgCommand  = (PRIVMSGCommand)command;
                if (privmsgCommand.Targets.Count == 0)
                {
                    return Errors.GetErrorResponse(ErrorCode.ERR_NORECIPIENT, "privmsg");
                }

                 if (privmsgCommand.Message==null)
                 {
                       return Errors.GetErrorResponse(ErrorCode.ERR_NOTEXTTOSEND, null);
                 }

                bool found = false;
                for (int i = 0; i < privmsgCommand.Targets.Count; i++)
                {
                    foreach (Session s in ServerBackend.Instance.ClientSessions)
                    {
                        if (s.User.Nickname == privmsgCommand.Targets[i])
                        {
                            if (s.ConnectionState == ConnectionState.Registered)
                            {
                                found = true;
                            }
                            break;
                        }
                    }
                    if (!found)
                    {
                        return Errors.GetErrorResponse(ErrorCode.ERR_NOSUCHNICK, privmsgCommand.Targets[i]);
                    }
                    found = false;
                }
                for (int i = 0; i < privmsgCommand.Targets.Count; i++)
                {
                    foreach (Session s in ServerBackend.Instance.ClientSessions)
                    {
                        if (s.User.Nickname == privmsgCommand.Targets[i])
                        {
                            s.Socket.Send(Encoding.ASCII.GetBytes(":"+session.User.Nickname+" PRIVMSG :"+privmsgCommand.Message));
                        }
                    }
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
