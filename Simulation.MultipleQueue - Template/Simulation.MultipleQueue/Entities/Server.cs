using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultipleQueue.Entities
{
    /// <summary>
    /// Represents a single server instance.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// The service time distribution
        /// </summary>
        private Distributions.IDistribution serviceTimeDistribution;

        /// <summary>
        /// The busy flag
        /// </summary>
        private bool isBusy;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; }
        }

        /// <summary>
        /// The server pirority
        /// </summary>
        private int? priority;

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public int? Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        /// <summary>
        /// Server busy time list
        /// </summary>
        private List<ServerBusyTime> serverStatusList;

        /// <summary>
        /// Gets or sets the server status list.
        /// </summary>
        /// <value>
        /// The server status list.
        /// </value>
        public List<ServerBusyTime> ServerStatusList
        {
            get { return serverStatusList; }
            set { serverStatusList = value; }
        }

        /// <summary>
        /// Total service time for all customers served at this server
        /// </summary>
        private float totalServiceTime;

        /// <summary>
        /// Gets the total service time for all customers served at this server.
        /// </summary>
        public float TotalServiceTime
        {
            get { return totalServiceTime; }
        }

        private float utilization;

        /// <summary>
        /// Gets or sets the utilization of the server.
        /// </summary>
        /// <value>
        /// The utilization.
        /// </value>
        public float Utilization
        {
            get { return utilization; }
            set { utilization = value; }
        }

        /// <summary>
        /// Gets the average service time.
        /// </summary>
        public float AverageServiceTime
        {
            get { return this.TotalServiceTime / this.TotalCustomerServed; }
        }

        private int totalCustomerServed = 0;

        /// <summary>
        /// Gets or sets the total customer served.
        /// </summary>
        /// <value>
        /// The total customer served.
        /// </value>
        public int TotalCustomerServed
        {
            get { return totalCustomerServed; }
            set { totalCustomerServed = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="serviceTimeDistribution">The service time distribution.</param>
        /// <param name="priority">The priority.</param>
        public Server(Distributions.IDistribution serviceTimeDistribution, int? priority)
        {
            this.serviceTimeDistribution = serviceTimeDistribution;
            this.isBusy = false;
            this.priority = priority;
            this.totalServiceTime = 0;
            this.ServerStatusList = new List<ServerBusyTime>();
        }

        /// <summary>
        /// Gets the next server time distribution.
        /// </summary>
        /// <returns></returns>
        public float GetNextServerTimeDistribution()
        {
            float randomVariable = Simulation.LCGRandomNumberGenerator.GetVariate();
            float serviceTime = this.serviceTimeDistribution.GetValue(randomVariable);
            this.totalServiceTime += serviceTime;
            return serviceTime;
        }
    }
}
