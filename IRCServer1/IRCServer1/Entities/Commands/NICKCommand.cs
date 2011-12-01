// -----------------------------------------------------------------------
// <copyright file="NICKCommand.cs" company="">
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
    public class NICKCommand : IRCCommandBase
    {
        public NICKCommand(string[] parameters) :
            base(parameters)
        {
            if (parameters.Length > 0)
            {
                this.NickName = parameters[0];
            }
        }

        public string NickName { get; set; }

        public override string ExecuteCommand(Session session)
        {
            NICKCommandHandler handler = new NICKCommandHandler();
            return handler.HandleCommand(this, session);
        }
    }
}
