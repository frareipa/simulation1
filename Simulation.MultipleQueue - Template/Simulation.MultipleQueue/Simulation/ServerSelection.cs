using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultipleQueue.Simulation
{
    public class ServerSelection
    {
        public enum ServerSelectionTypes
        {
            HighestPriority,
            LowestUtilization,
            Random
        }
        private ServerSelectionTypes priorityType;

        public ServerSelection(ServerSelectionTypes priorityType)
        {
            this.priorityType = priorityType;
        }

        public int GetServerByPriority(List<Entities.Server> servers, float clock)
        {
            //check at least one server is not busy
            
            switch (this.priorityType)
            {
                case ServerSelectionTypes.HighestPriority:
                    {
                        Entities.Server server = new Entities.Server(null, null);
                        bool first = true;
                        int index = 0;
                        foreach (Entities.Server temp in servers)
                        {
                            if (!temp.IsBusy && first)
                            {
                                server = temp;
                                index = servers.IndexOf(server);
                                first = false;
                            }
                            else if (!temp.IsBusy)
                            {
                                if (temp.Priority > server.Priority)
                                {
                                    server = temp;
                                    index = servers.IndexOf(server);
                                }
                            }
                        }
                        return index;
                    }// end case HighestPriority
                    break;
                case ServerSelectionTypes.LowestUtilization:
                    {
                        Entities.Server server= new Entities.Server(null,null);
                        bool first = true;
                        int index = 0;
                        foreach (Entities.Server temp in servers)
                        {
                            if (!temp.IsBusy && first)
                            {
                                server = temp;
                                index = servers.IndexOf(server);
                                first = false;
                            }
                            else if (!temp.IsBusy)
                            {
                                if(temp.Utilization < server.Utilization)
                                {
                                    server = temp;
                                    index = servers.IndexOf(server);
                                }
                            }
                        }
                        return index;
                    }// end case lowestUtilization
                    break;
                case ServerSelectionTypes.Random:
                    {
                        foreach (Entities.Server temp in servers)
                        {
                            if (!temp.IsBusy)
                            {
                                return servers.IndexOf(temp);
                            }
                        }
                    }//end case Random
                    break;
            }//end swith
                //HighestPriority
                //Random
                //LowestUtilization
            //throw new NotImplementedException(@"Implement GetServerByPriority in Simulation\ServerSelection.cs");
        }
    }
}
