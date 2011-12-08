// -----------------------------------------------------------------------
// <copyright file="PRIVMSGCommand.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using IRCServer1.CommandHandlers;
namespace IRCServer1.Entities.Commands
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    class PRIVMSGCommand : IRCCommandBase
    {
        // Parameters: <target>{,<target>} <text to be sent>
        public string Message { get; set; }
       // public string[] Targets = new string[100];
        public List<string> Targets = new List<string>();

        public PRIVMSGCommand(string[] parameters) :
            base(parameters)
        {
            if (parameters.Length > 0)
            {

                for (int i = 0; i < parameters[0].Split(',').Length; i++)
                {
                    this.Targets.Add(parameters[0].Split(',')[i].Split('\0')[0]);
                }
                if (parameters.Length != 1)
                {
                    this.Message = parameters[parameters.Length - 1].Split('\0')[0];
                }
            }
        }
        
        
        
        public override string ExecuteCommand(Session session)
        {
            PRIVMSGCommandHandler handler = new PRIVMSGCommandHandler();
            return handler.HandleCommand(this, session);
        }

    }
}
