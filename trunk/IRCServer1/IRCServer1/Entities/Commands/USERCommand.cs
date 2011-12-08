// -----------------------------------------------------------------------
// <copyright file="USERCommand.cs" company="fcis">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using IRCServer1.CommandHandlers;
namespace IRCServer1.Entities.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class USERCommand : IRCCommandBase
    {
        //Parameters: <username> <hostname> <servername> <realname>
        public USERCommand(string[] parameters) :
            base(parameters)
        {
            if (parameters.Length > 3)
            {
                this.UserName = parameters[0].Split('\0')[0];
                this.RealName = parameters[1].Split('\0')[0];
                this.HostName = parameters[2].Split('\0')[0];
                this.ServerName = parameters[3].Split('\0')[0];
            }
        }

        public string UserName { get; set; }
        public string RealName { get; set; }
        public string HostName { get; set; }
        public string ServerName { get; set; }

        public override string ExecuteCommand(Session session)
        {
            USERCommandHandler handler = new USERCommandHandler();
            return handler.HandleCommand(this, session);
        }
    }
}
