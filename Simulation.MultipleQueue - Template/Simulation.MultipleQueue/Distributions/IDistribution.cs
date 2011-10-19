using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultipleQueue.Distributions
{
    /// <summary>
    /// Contains supported distributions
    /// </summary>
    public enum DistributionTypes
    {
        Uniform,
        Discrete,
        Exponential
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IDistribution
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="randomVariable">The random variable.</param>
        /// <returns></returns>
        float GetValue(float randomVariable);
    }
}
