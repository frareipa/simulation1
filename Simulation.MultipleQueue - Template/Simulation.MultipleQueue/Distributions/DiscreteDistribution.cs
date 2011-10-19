using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultipleQueue.Distributions
{
    public class DiscreteDistribution : IDistribution
    {
        #region IDistribution Members
        private List<float> probabilities;
        private List<float> values;
        private List<float> cumulativeProbabilities;
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscreteDistribution"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="probabilities">The probabilities.</param>
        public DiscreteDistribution(List<float> values, List<float> probabilities)
        {
            this.probabilities = probabilities;
            this.cumulativeProbabilities = new List<float>();
            this.values = values;
            float sum = 0;
            for (int i = 0; i < this.probabilities.Count; i++)
            {
                sum += this.probabilities[i];
                this.cumulativeProbabilities.Add(sum);
            }
        }

        /// <summary>
        /// Gets the val.
        /// </summary>
        /// <param name="randomVariable">The random variable.</param>
        /// <returns></returns>
        public float GetValue(float randomVariable)
        {
            int i;
            for (i = 0; randomVariable > this.cumulativeProbabilities[i]; i++)
                ;//Empty loop
            return this.values[i];
        }
        #endregion
    }
}
