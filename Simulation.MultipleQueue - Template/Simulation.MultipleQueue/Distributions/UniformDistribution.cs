using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultipleQueue.Distributions
{
    public class UniformDistribution : IDistribution
    {
        #region IDistribution Members

        private float a, b;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniformDistribution"/> class.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        public UniformDistribution(float a, float b)
        {
            this.a = a;
            this.b = b;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="randomVariable">The random variable.</param>
        /// <returns></returns>
        public float GetValue(float randomVariable)
        {
            return this.a + randomVariable * (this.b - this.a);
        }
        #endregion
    }
}
