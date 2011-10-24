using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultipleQueue.Simulation
{
    public class Model
    {
        /// <summary>
        /// Number of servers
        /// </summary>
        private int serversCount;

        /// <summary>
        /// The server priority rule
        /// </summary>
        private ServerSelection serverSelection;

        /// <summary>
        /// Customers inter-arrival distribution
        /// </summary>
        private Distributions.IDistribution customerInterArrivalDistribution;

        /// <summary>
        /// Number of customer at which to stop simulation.
        /// </summary>
        private int customerCountStoppingCondition;

        /// <summary>
        /// List of servers
        /// </summary>
        private List<Entities.Server> servers;

        /// <summary>
        /// Gets the servers.
        /// </summary>
        public List<Entities.Server> Servers
        {
            get { return servers; }
        }

        /// <summary>
        /// The customers queue, the queue is changed with time according to the current number of customer waiting.
        /// </summary>
        private Queue<MultipleQueue.Entities.Customer> customerQueue;

        /// <summary>
        /// The list of customers who finished their service.
        /// </summary>
        private List<MultipleQueue.Entities.Customer> completedCustomers;

        /// <summary>
        /// Contains the future events sorted by time ASC
        /// </summary>
        private SortedList<MultipleQueue.Entities.Customer, EventType> eventsList;

        /// <summary>
        /// The clock of the simulation, changes to the time of the next event with every cycle of the run loop
        /// </summary>
        private float clock;
        /// <summary>
        /// Average waiting time of customers in the system
        /// </summary>
        private float averageWaitingTime = 0;

        /// <summary>
        /// Gets or sets the average waiting time of customers in the system.
        /// </summary>
        /// <value>
        /// The average waiting time.
        /// </value>
        public float AverageWaitingTime
        {
            get { return averageWaitingTime; }
            set { averageWaitingTime = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private float probabilityOfWait = 0;

        /// <summary>
        /// Gets or sets the probability that a customer will wait in the system.
        /// </summary>
        /// <value>
        /// The probability of wait.
        /// </value>
        public float ProbabilityOfWait
        {
            get { return probabilityOfWait; }
            set { probabilityOfWait = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="priorityType">Type of the priority.</param>
        /// <param name="customerInterArrivalDistribution">The customer inter arrival distribution.</param>
        /// <param name="servers">The servers.</param>
        /// <param name="customerCountStoppingCondition">The customer count stopping condition.</param>
        /// <param name="lcgRandomNumberGenerator">The LCG random number generator.</param>
        public Model(ServerSelection serverSelection,
            Distributions.IDistribution customerInterArrivalDistribution,
            List<Entities.Server> servers, int customerCountStoppingCondition)
        {
            this.customerInterArrivalDistribution = customerInterArrivalDistribution;
            this.serverSelection = serverSelection;
            this.serversCount = servers.Count;
            this.customerCountStoppingCondition = customerCountStoppingCondition;
            this.completedCustomers = new List<MultipleQueue.Entities.Customer>();
            this.customerQueue = new Queue<MultipleQueue.Entities.Customer>();
            this.eventsList = new SortedList<MultipleQueue.Entities.Customer, EventType>();
            this.servers = servers;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            throw new NotImplementedException(@"Implement Initialize in Simulation\Model.cs");
          //this.clock=  customerInterArrivalDistribution.GetValue(LCGRandomNumberGenerator.GetVariate());
            //initialize clock time
            this.clock = 0;
            //Generate initial arrival Time
            MultipleQueue.Entities.Customer c = GenerateCustomer();
           // MultipleQueue.Entities.Customer c=new MultipleQueue.Entities.Customer(0,0);
            eventsList.Add ( c,EventType.Arrival);            
            //1-generate the first event -> get next customer
            //2-add arrival event in the events list
        }

        /// <summary>
        /// Generates the customer.
        /// </summary>
        /// <returns></returns>
        private Entities.Customer GenerateCustomer()
        {
            throw new NotImplementedException(@"Implement GenerateCustomer in Simulation\Model.cs");
          float inter_arrival=  customerInterArrivalDistribution.GetValue(LCGRandomNumberGenerator.GetVariate());
            //use LCGRandomNumberGenerator to generate inter-arrival time for the customer
            MultipleQueue.Entities.Customer c=new MultipleQueue.Entities.Customer(inter_arrival,0);
            return c;
            //return the new customer object
        }


        /// <summary>
        /// Arrives the customer.
        /// </summary>
        /// <param name="arrivingCustomer">The arriving customer.</param>
        private void ArriveCustomer(Entities.Customer arrivingCustomer)                                    
        {
            throw new NotImplementedException(@"Implement ArriveCustomer in Simulation\Model.cs");

            //set clock
            clock = arrivingCustomer.ArrivalTime;
            MultipleQueue.Entities.Server c;
            foreach c in servers
            {
                if(c.IsBusy==true)
                {
                }
            }
            //select a server, if any are available
            //enter queue or assign to a server
            //schedule arrival of next customer
        }

        /// <summary>
        /// Departs the customer.
        /// </summary>
        /// <param name="departingCustomer">The departing customer.</param>
        private void DepartCustomer(Entities.Customer departingCustomer)
        {
            throw new NotImplementedException(@"Implement DepartCustomer in Simulation\Model.cs");

            //Set the clock to the current event.
            
            //The server is no longer busy.
            
            //Add the departed customer to the list of completed customers.
            
            //enter another customer to the servers, 
            //but still by ServerSelection because maybe a more efficient server is idle too.

            //We know that serverIndex should not be -1 because a server just go IDLE.
            //But we are using server Selection cause maybe another server is IDLE too, and better
        }

        /// <summary>
        /// Assigns the customer to server.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="serverIndex">Index of the server.</param>
        private void AssignCustomerToServer(Entities.Customer customer, int serverIndex)
        {
            throw new NotImplementedException(@"Implement AssignCustomerToServer in Simulation\Model.cs");

            //update customer's data
            //schedule the departure of this customer
            //update this server's statistics
        }
        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            throw new NotImplementedException(@"Implement Run in Simulation\Model.cs");
            int numOfCustomer = 0;
            //initialize model
            Initialize();
            while (numOfCustomer < customerCountStoppingCondition)
            {
                CalculateSystemPerformance();
            }
            //while the stopping condition is not reached yet, handle the next event in the events list
            //calculate the system performance
        }

        /// <summary>
        /// Calculates the system performance.
        /// </summary>
        public void CalculateSystemPerformance()
        {
            throw new NotImplementedException(@"Implement CalculateSystemPerformance in Simulation\Model.cs");

            //Calculate the value system performance measures
           
        }
    }
}
