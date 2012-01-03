// -----------------------------------------------------------------------
// <copyright file="NEXTHOPCommandHandler.cs" company="">
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
    public class NEXTHOPCommandHandler : CommandHandlerBase
    {
        public override string HandleCommand(DaemonCommandBase command)
        {
            string[] respones=new string[2];
            if (command is NEXTHOPCommand)
            {
                NEXTHOPCommand nextHopCommand = (NEXTHOPCommand)command;
                if (nextHopCommand.NickName == null)
                {
                    return "ERROR";
                }
                Entities.User U=new Entities.User();
                U.Nickname=nextHopCommand.NickName;
                Backend.DaemonBackEnd.Instance.UpdateRoutingTable();
                            foreach(Entities.RoutingTableEntry e in Backend.DaemonBackEnd.Instance.RoutingTable)
                            {
                                if(e.Node.Users.Contains(U))
                                {
                                    if (e.NextHop.NodeID != Backend.DaemonBackEnd.Instance.LocalNode.NodeID)
                                    {
                                        respones[0] = e.Node.NodeID.ToString();
                                        respones[1] = e.Distance.ToString();
                                        return Utilities.Responses.GetResponse(Utilities.ResponseCodes.NextHop_OK, respones);
                                    }

                                }
                            }
                            return null;
                      }
            else
                return "ERROR";
                    }
                }
            }