// -----------------------------------------------------------------------
// <copyright file="PRIVMSGCommandHandler.cs" company="">
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
   class PRIVMSGCommandHandler : CommandHandlerBase
    {
        public override string HandleCommand(IRCCommandBase command, Session session)
        {
            if (command is PRIVMSGCommand)
            {
                PRIVMSGCommand privmsgCommand  = (PRIVMSGCommand)command;
                if(privmsgCommand.Targets[0]==null)
                {
                    return Errors.GetErrorResponse(ErrorCode.ERR_NORECIPIENT, null);
                }
                if (privmsgCommand.Message == null)
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
                            s.Socket.Receive(s.Buffer);
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
