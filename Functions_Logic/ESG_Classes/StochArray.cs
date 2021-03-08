using Functions_Logic.Utilities;
using MathNet.Numerics.Statistics;
using System;
using System.Linq;

namespace Functions_Logic.ESG_Classes
{
    /// <summary>
    /// Stochastic arrays to hold stochastic data.
    /// </summary>
    public class StochArray
    {
        private readonly double[][] _values;
        private readonly int _maxHorizon;
        private readonly int _maxSim;

        // sim -> value by year
        public StochArray(double[][] values)
        {
            _values = values;
            _maxHorizon = values.FirstOrDefault()?.Length ?? 0;
            _maxSim = values?.Length ?? 0;
        }

        public double[][] Values => _values;

        public int MaxHorizon => _maxHorizon;

        public int MaxSim => _maxSim;

        public double[] this[int i] => _values[i];


        public Sample GetYearAsSample(int year) => new Sample(_values.Select(sim => sim[year-1]).ToArray());

        //Have this as a 'Scenario' class instead?
        //public Sample GetSimAsSample() => throw new NotImplementedException();

        /// <summary>
        /// The mean calculated across time for each sim. A median is then taken sims.
        /// </summary>
        /// <returns>The mean</returns>
        public double Mean() => _values.Select(val => val.Mean()).Median();

        /// <summary>
        /// The median calculated across time for each sim. A median is then taken sims.
        /// </summary>
        /// <returns>The median</returns>
        public double Median() => _values.Select(val => val.Median()).Median();

        // Don't think this is necessary
        //public double Mode() => throw new NotImplementedException();

        /// <summary>
        /// The volatility calculated across time for each sim. A median is then taken sims.
        /// </summary>
        /// <returns>The population standard deviation</returns>
        public double Volatility() => _values.Select(val => val.PopulationStandardDeviation()).Median();

        /// <summary>
        /// Accumulates the return in the stoch array across time, for each sim
        /// </summary>
        /// <param name="year">The year up to which the accumulation will proceed</param>
        /// <param name="accumulation">Whether the accumualtion result should be <see cref="Accumulation.Annualised"/>or <see cref="Accumulation.Cumulative"/></param>
        /// <returns>The accumulated stocharray returned as a sample</returns>
        public Sample Accumulate(int year, Accumulation accumulation)
        {
            var values = Enumerable.Repeat(1d, MaxSim).ToArray();
            for (int i = 0; i < MaxSim; i++)
            {
                for (int j = 0; j < year; j++)
                {
                    values[i] *= (1 + _values[i][j]);
                }

                values[i] = accumulation == Accumulation.Annualised ? values[i] = Math.Pow(values[i], 1d / year) - 1 : values[i] - 1;
            }
           
            return new Sample(values);
        }

        /// <summary>
        /// Combine two or more stocharrays based on an inittial set of proportions
        /// </summary>
        /// <param name="rebalancingPolicy">Whether the holdings rebalance to the initial set of weights or are allowed to drift</param>
        /// <returns>A combined stocharray</returns>
        public static StochArray Combine(StochArray[] stochArrays, double[] proportions, Rebalancing rebalancingPolicy)
        {
            return rebalancingPolicy == Rebalancing.NoRebalancing ?
                CombineNoRebalancing(stochArrays, proportions) :
                CombineWithRebalancing(stochArrays, proportions);
        }

        private static StochArray CombineWithRebalancing(StochArray[] stochArrays, double[] proportions)
        {
            var result = stochArrays.Zip(proportions, (stochArray, proportion) => stochArray * proportion)
                                    .Aggregate((s1, s2) => s1 + s2);
            return result; ;
        }

        private static StochArray CombineNoRebalancing(StochArray[] stochArrays, double[] proportions)
        {
            int arrayCount = stochArrays.Length;
            int maxHor = stochArrays.First().Values.First().Length;
            int maxSim = stochArrays.First().Values.Length;
            var result = new double[maxSim][];
            result = result.Select(val => new double[maxHor]).ToArray();
            
            for (int j = 0; j < maxSim; j++)
            {
                // initialising proportions. This creates a copy that has a different reference in memory
                var simProportions = proportions.ToArray();
                for (int i = 0; i < maxHor; i++)
                {
                    // initialising the accumulator
                    var accumulator = new double[arrayCount];
                    for (int k = 0; k < arrayCount; k++)
                    {
                        result[j][i] += stochArrays[k][j][i] * simProportions[k];
                        accumulator[k] = (1 + stochArrays[k][j][i]) * simProportions[k];
                    }

                    double sum = accumulator.Sum();
                    for (int k = 0; k < arrayCount; k++)
                    {
                        simProportions[k] = accumulator[k] / sum;
                    }
                }
            }
            return new StochArray(result);
        }

        /// <summary>
        /// Calculates the VAR
        /// </summary>
        /// <param name="year">The year as at which VAR should be calculated</param>
        public double VAR(int year, double percentileRank)
        {
            var sample = Accumulate(year, Accumulation.Annualised);        
            return sample.Percentile(percentileRank);
        }

        /// <summary>
        /// Scales stoch array returns by a scalar
        /// </summary>
        public static StochArray operator *(StochArray a, double scalar)
        {
            return ReusableOperatorLogic(a.Values, (i, j) => a.Values[i][j] * scalar);
        }

        /// <summary>
        /// Adds together arithmetically the returns of to stocharrays. Dimensions need to match
        /// </summary>
        public static StochArray operator +(StochArray a, StochArray b)
        {
            if (a.MaxHorizon != b.MaxHorizon)
            {
                throw new ArgumentException($"Add operation has failed.\nHorizons need to match ({a.MaxHorizon} <> {b.MaxHorizon})");
            }

            if (a.MaxSim != b.MaxSim)
            {
                throw new ArgumentException($"Add operation has failed.\nMax number of simulations need to match ({a.MaxSim} <> {b.MaxSim})");
            }

            return ReusableOperatorLogic(a.Values, (i, j) => a[i][j] + b[i][j]);
        }
           

        private static StochArray ReusableOperatorLogic(double[][] values, Func<int, int, double> operatorFunc)
        {
            int maxHor = values.First().Length;
            int maxSim = values.Length;
            var result = new double[maxSim][];
            result = result.Select(val => new double[maxHor]).ToArray();

            for (int i = 0; i < maxSim; i++)
            {
                for (int j = 0; j < maxHor; j++)
                {
                    result[i][j] = operatorFunc(i, j);
                }
            }

            return new StochArray(result);
        }

       
    }



}
