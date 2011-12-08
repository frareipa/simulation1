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
                PRIVMSGCommand privmsgCommand  = (PRIVMSGCommand)command;
                if (privmsgCommand.Message == null && privmsgCommand.Targets.Count == 0)
                {
                    return Errors.GetErrorResponse(ErrorCode.ERR_NORECIPIENT, "privmsg")+" and "+Errors.GetErrorResponse(ErrorCode.ERR_NOTEXTTOSEND, null);
                }
                else if (privmsgCommand.Message==null)
                {
                            bool isNickName = false;
                            foreach (Session s in ServerBackend.Instance.ClientSessions)
                            {
                                if (s.User.Nickname == privmsgCommand.Targets[0])
                                {
                                    isNickName = true;
                                }
                            }
                        if (isNickName == true)
                        {
                            return Errors.GetErrorResponse(ErrorCode.ERR_NOTEXTTOSEND, null);
                        }
                        else
                        {
                            return Errors.GetErrorResponse(ErrorCode.ERR_NORECIPIENT, "privmsg");
                        }
                    }
                else if (privmsgCommand.Message == null)
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
                            s.Socket.Send(Encoding.ASCII.GetBytes(privmsgCommand.Message));
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        return Errors.GetErrorResponse(ErrorCode.ERR_NOSUCHNICK, privmsgCommand.Targets[i]);
                    }
                }
                return String.Empty;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
