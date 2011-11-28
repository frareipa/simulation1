// -----------------------------------------------------------------------
// <copyright file="NICKCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using ICRServer.CommandHandlers;
namespace ICRServer.Entities.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NICKCommand : IRCCommandBase
    {
        public NICKCommand(string[] parameters) :
            base(parameters)
        {
            if (parameters.Length > 0)
            {
                this.Message = parameters[0];
            }
        }

        public string Message { get; set; }

        public override string ExecuteCommand(Session session)
        {
            NICKCommandHandler handler = new NICKCommandHandler();
            return handler.HandleCommand(this, session);
        }
    }
}
