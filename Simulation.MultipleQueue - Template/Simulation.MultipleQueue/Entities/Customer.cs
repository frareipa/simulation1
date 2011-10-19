using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultipleQueue.Entities
{
    public class Customer : IComparable
    {
        #region Members
        /// <summary>
        /// 
        /// </summary>
        private bool isArrived = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is arrived.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is arrived; otherwise, <c>false</c>.
        /// </value>
        public bool IsArrived
        {
            get { return isArrived; }
            set { isArrived = value; }
        }

        /// <summary>
        /// The arrival time.
        /// </summary>
        private float arrivalTime = -1;

        /// <summary>
        /// The arrival time.
        /// </summary>
        public float ArrivalTime
        {
            get { return arrivalTime; }
            set { arrivalTime = value; }
        }

        /// <summary>
        /// The generated interarrival time.
        /// </summary>
        private float interarrivalTime;

        /// <summary>
        /// The generated interarrival time.
        /// </summary>
        public float InterarrivalTime
        {
            get { return interarrivalTime; }
            set { interarrivalTime = value; }
        }

        /// <summary>
        /// The generated service time
        /// </summary>
        private float serviceTime;

        /// <summary>
        /// The generated service time
        /// </summary>
        public float ServiceTime
        {
            get { return serviceTime; }
            set { serviceTime = value; }
        }

        /// <summary>
        /// The departure time which equals (arrivalTime+WaitTime+ServiceTime)
        /// </summary>
        private float departureTime = -1;

        /// <summary>
        /// The departure time which equals (arrivalTime+WaitTime+ServiceTime)
        /// </summary>
        public float DepartureTime
        {
            get { return departureTime; }
            set { departureTime = value; }
        }

        /// <summary>
        /// The total time the customer spent in the queue.
        /// </summary>
        private float waitTime;

        /// <summary>
        /// The total time the customer spent in the queue.
        /// </summary>
        public float WaitTime
        {
            get { return waitTime; }
            set { waitTime = value; }
        }

        /// <summary>
        /// The index of server that served this customer.
        /// </summary>
        private int serverIndex;

        /// <summary>
        /// The index of server that served this customer.
        /// </summary>
        public int ServerIndex
        {
            get { return serverIndex; }
            set { serverIndex = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="interarrivalTime">The interarrival time.</param>
        /// <param name="arrivalTime">The arrival time.</param>
        public Customer(float interarrivalTime, float arrivalTime)
        {
            this.interarrivalTime = interarrivalTime;
            this.arrivalTime = arrivalTime;
        }

        #region IComparable Members

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This instance is less than <paramref name="obj"/>.
        /// Zero
        /// This instance is equal to <paramref name="obj"/>.
        /// Greater than zero
        /// This instance is greater than <paramref name="obj"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="obj"/> is not the same type as this instance.
        ///   </exception>
        public int CompareTo(object obj)
        {
            Customer otherCustomer = (Customer)obj;
            //The comparison is tricky here, because the customers should be sorted according to their next event
            float thisCustomerNextEvent, otherCustomerNextEvent;
            if (this.isArrived)
            {
                thisCustomerNextEvent = this.departureTime;
            }
            else
            {
                thisCustomerNextEvent = this.arrivalTime;
            }
            if (otherCustomer.isArrived)
            {
                otherCustomerNextEvent = otherCustomer.departureTime;
            }
            else
            {
                otherCustomerNextEvent = otherCustomer.arrivalTime;
            }
            
            if (thisCustomerNextEvent >= otherCustomerNextEvent)
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }

        #endregion
    }
}
