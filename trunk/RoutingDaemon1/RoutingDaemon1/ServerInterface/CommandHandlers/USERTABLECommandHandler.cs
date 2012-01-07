// -----------------------------------------------------------------------
// <copyright file="USERTABLECommandHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using RoutingDaemon1.ServerInterface.Commands;
namespace RoutingDaemon1.ServerInterface.CommandHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class USERTABLECommandHandler : CommandHandlerBase
    {
        public override string HandleCommand(DaemonCommandBase command)
        {
            if(command is USERTABLECommand)
            {
                int count = 0;
                string []respones=new string[1];
                respones[0] = "";
                Backend.DaemonBackEnd.Instance.UpdateRoutingTable();
              foreach(Entities.RoutingTableEntry e in Backend.DaemonBackEnd.Instance.RoutingTable)
                {
                    if (e.Node.NodeID != Backend.DaemonBackEnd.Instance.LocalNode.NodeID)
                    {
                        count += e.Node.Users.Count;
                        foreach (Entities.User u in e.Node.Users)
                        {
                            if (e.NextHop == null)
                            {
                                respones[0] += u.Nickname + " " + e.Node.NodeID.ToString() + " " + e.Distance.ToString() + "\n";
                            }
                            else
                            {
                                respones[0] += u.Nickname + " " + e.NextHop.NodeID.ToString() + " " + e.Distance.ToString() + "\n";
                            }
                        }
                    }
                }
                              respones[0] =  count.ToString() + "\n" + respones[0];
                            return  Utilities.Responses.GetResponse(Utilities.ResponseCodes.UserTable_OK, respones);
            }
            else
                return "ERROR";

        }
    }
}
