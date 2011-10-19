using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultipleQueue.Distributions
{
    public class ExponentialDistribution : IDistribution
    {
        #region IDistribution Members

        private double mean;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentialDistribution"/> class.
        /// </summary>
        /// <param name="Mean">The mean.</param>
        public ExponentialDistribution(float mean)
        {
            this.mean = mean;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="randomVariable">The random variable.</param>
        /// <returns></returns>
        public float GetValue(float randomVariable)
        {
            if (randomVariable != 0)
            {
                return (float)(-this.mean * Math.Log(randomVariable, Math.Exp(1)));
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
