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
            

            //switch (this.priorityType)
                //HighestPriority
                //Random
                //LowestUtilization
            throw new NotImplementedException(@"Implement GetServerByPriority in Simulation\ServerSelection.cs");
        }
    }
}
