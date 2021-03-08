using Functions_Logic.ESG_Classes;
using Functions_Logic.Utilities;
using System;

namespace Functions_Logic.Logic
{
    public static class SampleLogic
    {
        public static double Mean(string hash)
        {
            var cache = Cache<Sample>.GetCache;
            if (!cache.TryGetItem(hash, out var sample)) { throw new MissingSampleException(hash); }
            return sample.Mean();
        }

        public static double Median(string hash)
        {
            var cache = Cache<Sample>.GetCache;
            if (!cache.TryGetItem(hash, out var sample)) { throw new MissingSampleException(hash); }
            return sample.Median();
        }

        public static double Volatility(string hash)
        {
            var cache = Cache<Sample>.GetCache;
            if (!cache.TryGetItem(hash, out var sample)) { throw new MissingSampleException(hash); }
            return sample.Volatility();
        }

        public static double Skewness(string hash)
        {
            var cache = Cache<Sample>.GetCache;
            if (!cache.TryGetItem(hash, out var sample)) { throw new MissingSampleException(hash); }
            return sample.Skewness();
        }

        public static double Kurtosis(string hash)
        {
            var cache = Cache<Sample>.GetCache;
            if (!cache.TryGetItem(hash, out var sample)) { throw new MissingSampleException(hash); }
            return sample.Kurtosis();
        }

        public static double Min(string hash)
        {
            var cache = Cache<Sample>.GetCache;
            if (!cache.TryGetItem(hash, out var sample)) { throw new MissingSampleException(hash); }
            return sample.Min();
        }

        public static double Max(string hash)
        {
            var cache = Cache<Sample>.GetCache;
            if (!cache.TryGetItem(hash, out var sample)) { throw new MissingSampleException(hash); }
            return sample.Max();
        }

        public static double SpreadVAR(string hash)
        {
            // Check first if the result is already in the cache
            var resultsCache = Cache<double>.GetCache;
            if (resultsCache.TryGetItem(out var result, out _, nameof(SpreadVAR), hash)) { return result; }

            var cache = Cache<Sample>.GetCache;
            if (!cache.TryGetItem(hash, out var sample)) { throw new MissingSampleException(hash); }

            double newResult = sample.SpreadVAR();
            _ = resultsCache.TryAddItem(newResult, out var _, nameof(SpreadVAR), hash);

            return newResult;
        }

        public static double Percentile(string hash, object percentileRank)
        {
            // Check first if the result is already in the cache
            var resultsCache = Cache<double>.GetCache;
            if (resultsCache.TryGetItem(out var result, out _, nameof(Percentile), hash, percentileRank)) { return result; }

            var cache = Cache<Sample>.GetCache;
            if (!cache.TryGetItem(hash, out var sample)) { throw new MissingSampleException(hash); }
            double percentileRankVal = percentileRank.ConvertParameterToDouble("Percentile Rank");

            if (percentileRankVal < 0 || percentileRankVal > 1)
            {
                throw new ArgumentException($"Percentile rank needs to be between 0 and 1. Argument passed in: {percentileRankVal}");
            }

            double newResult = sample.Percentile(percentileRankVal);
            _ = resultsCache.TryAddItem(newResult, out var _, nameof(Percentile), hash, percentileRank);
            return newResult;
        }
    }
}
