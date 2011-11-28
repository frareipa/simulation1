using System;
using System.Collections.Generic;
using System.Text;
using ICRServer.CommandHandlers;

namespace ICRServer.Entities.Commands
{
    class QUITCommand : IRCCommandBase
    {
        public QUITCommand(string[] parameters) :
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
            QUITCommandHandler handler = new QUITCommandHandler();
            return handler.HandleCommand(this, session);
        }
    }
}
