using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultipleQueue.Entities
{
    /// <summary>
    /// Represents a server's serving time (busy time).
    /// </summary>
    public class ServerBusyTime
    {
        float fromTime;

        /// <summary>
        /// Gets or sets from time.
        /// </summary>
        /// <value>
        /// From time.
        /// </value>
        public float FromTime
        {
            get { return fromTime; }
            set { fromTime = value; }
        }
        float toTime;

        /// <summary>
        /// Gets or sets to time.
        /// </summary>
        /// <value>
        /// To time.
        /// </value>
        public float ToTime
        {
            get { return toTime; }
            set { toTime = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerBusyTime"/> class.
        /// </summary>
        /// <param name="fromTime">From time.</param>
        /// <param name="toTime">To time.</param>
        public ServerBusyTime(float fromTime, float toTime)
        {
            this.fromTime = fromTime;
            this.toTime = toTime;
        }

    }
}
