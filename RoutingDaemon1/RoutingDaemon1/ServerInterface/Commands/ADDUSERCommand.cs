﻿// -----------------------------------------------------------------------
// <copyright file="USERCommand.cs" company="fcis">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using RoutingDaemon1.ServerInterface.CommandHandlers;
namespace RoutingDaemon1.ServerInterface.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ADDUSERCommand  :DaemonCommandBase 
    {
        //Parameters: <username> <hostname> <servername> <realname>
        public ADDUSERCommand(string[] parameters) 
        {
            if (parameters.Length > 0)
            {
                this.NickName = parameters[0].Split('\0')[0];
            }
        }

        public string NickName { get; set; }

        public override string ExecuteCommand()
        {
         ServerInterface.CommandHandlers.ADDUSERCommandHandler handler = new ServerInterface.CommandHandlers.ADDUSERCommandHandler();
            return handler.HandleCommand(this);
        }
    }
}
