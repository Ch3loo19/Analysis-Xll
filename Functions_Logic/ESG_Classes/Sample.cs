using MathNet.Numerics.Statistics;
using System;
using System.Linq;

namespace Functions_Logic.ESG_Classes
{
    public class Sample
    {

        private double[] _values;
        public Sample(double[] values)
        {
            _values = values;
        }

        public double[] Values => _values;

        public double Mean() => _values.Mean();

        public double Median() => _values.Median();

        public double Volatility() => _values.StandardDeviation();

        public double Skewness() => _values.Skewness();

        public double Kurtosis() => _values.Kurtosis();

        public double Min() => _values.Minimum();

        public double Max() => _values.Maximum();

        public double Percentile(double percentileRank) => _values.QuantileCustom(percentileRank, QuantileDefinition.Excel);

        public double SpreadVAR()
        {
            const double scale = 1E-04;
            const int startRank = 40;
            const int percentileCount = 21;

            var percentuleRanks = Enumerable.Range(startRank, percentileCount).Select(val => val * scale);
            var varResults = percentuleRanks.Select(Percentile);
            double result = varResults.Mean();
            return result;
        }


    }
}
