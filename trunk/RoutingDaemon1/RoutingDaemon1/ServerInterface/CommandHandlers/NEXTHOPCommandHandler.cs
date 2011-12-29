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
                //foreach(Entities.Node n in Backend.DaemonBackEnd.Instance.LocalNode.Neighbors)
                //{
                //    if(n.Users.Contains(U))
                //    {
                //        respones[0]=n.NodeID.ToString();
                //        break;
                //    }
                //    foreach (Entities.Node nn in n.Neighbors)
                //    {
                //        if (!Backend.DaemonBackEnd.Instance.LocalNode.Neighbors.Contains(nn))
                //        {
                //            if (n.Users.Contains(U))
                //            {
                //                respones[0] = n.NodeID.ToString();
                //                break;
                //            }
                //        }
                //    }
                //}
                            foreach(Entities.RoutingTableEntry e in Backend.DaemonBackEnd.Instance.RoutingTable)
                            {
                                if(e.Node.Users.Contains(U))
                                {
                                    if(e.NextHop.NodeID==int.Parse(respones[0]))
                                    {
                                        respones[0] = e.NextHop.ToString();
                                    respones[1]=e.Distance.ToString();
                                        break;
                                    }

                                }
                            }
                      return Utilities.Responses.GetResponse(Utilities.ResponseCodes.NextHop_OK,respones);
                        }
            else
                return "ERROR";
                    }
                }
            }