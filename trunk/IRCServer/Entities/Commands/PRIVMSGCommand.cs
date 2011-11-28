// -----------------------------------------------------------------------
// <copyright file="PRIVMSGCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using ICRServer.CommandHandlers;
namespace ICRServer.Entities.Commands
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    class PRIVMSGCommand : IRCCommandBase
    {
        public string Message { get; set; }

        public PRIVMSGCommand(string[] parameters) :
            base(parameters)
        {
            if (parameters.Length > 0)
            {
                this.Message = parameters[0];
            }
        }

        public override string ExecuteCommand(Session session)
        {
            QUITCommandHandler handler = new QUITCommandHandler();
            return handler.HandleCommand(this, session);
        }

    }
}
