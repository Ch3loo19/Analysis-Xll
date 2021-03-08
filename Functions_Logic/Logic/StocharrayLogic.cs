using Functions_Logic.ESG_Classes;
using Functions_Logic.Utilities;
using System;
using System.Linq;

namespace Functions_Logic.Logic
{
    public static class StochArrayLogic
    {

        public static double Mean(string hash)
        {
            var cache = Cache<StochArray>.GetCache;
            if (!cache.TryGetItem(hash, out var stochArray)) { throw new MissingStochArrayException(hash); }
            return stochArray.Mean();
        }

        public static double Median(string hash)
        {
            var cache = Cache<StochArray>.GetCache;
            if (!cache.TryGetItem(hash, out var stochArray)) { throw new MissingStochArrayException(hash); }
            return stochArray.Median();
        }

        public static double Volatility(string hash)
        {
            var cache = Cache<StochArray>.GetCache;
            if (!cache.TryGetItem(hash, out var stochArray)) { throw new MissingStochArrayException(hash); }
            return stochArray.Volatility();
        }

        public static double Value(string hash, object sim, object horizon)
        {
            var stochArrayCache = Cache<StochArray>.GetCache;
            if (!stochArrayCache.TryGetItem(hash, out var stochArray)) { throw new MissingStochArrayException(hash); }

            int simInt = sim.ConvertParameterToInt("Simulation");

            if (simInt < 1 || simInt > stochArray.MaxSim)
            {
                throw new StochArrayHorizonException(simInt, stochArray.MaxSim);
            }

            int horizonInt = horizon.ConvertParameterToInt("Horizon");

            if (horizonInt < 1 || horizonInt > stochArray.MaxHorizon)
            {
                throw new StochArrayHorizonException(horizonInt, stochArray.MaxHorizon);
            }

            double value = stochArray[simInt - 1][horizonInt - 1];

            return value;
        }

        public static string GetYearAsSample(string hash, object year)
        {
            // Check first if the result is already in the cache
            var sampleCache = Cache<Sample>.GetCache;
            if (sampleCache.TryGetItem(out _, out string key, nameof(GetYearAsSample), hash, year)) { return key; }

            // If not, perform the calcs
            var stochArrayCache = Cache<StochArray>.GetCache;
            if (!stochArrayCache.TryGetItem(hash, out var stochArray)) { throw new MissingStochArrayException(hash); }

            int sliceAt = year.ConvertParameterToInt("Year");

            if (sliceAt < 1 || sliceAt > stochArray.MaxHorizon)
            {
                throw new StochArrayHorizonException(sliceAt, stochArray.MaxHorizon);
            }

            var sampleResult = stochArray.GetYearAsSample(sliceAt);

            _ = sampleCache.TryAddItem(sampleResult, out var sampleHash, nameof(Accumulate), hash, year);

            return sampleHash;

        }

        public static string Accumulate(string hash, object year, object accumulate)
        {
            // Check first if the result is already in the cache
            var sampleCache = Cache<Sample>.GetCache;
            if (sampleCache.TryGetItem(out _, out string key, nameof(Accumulate), hash, year, accumulate)) { return key; }

            // If not, perform the calcs
            var stochArrayCache = Cache<StochArray>.GetCache;
            if (!stochArrayCache.TryGetItem(hash, out var stochArray)) { throw new MissingStochArrayException(hash); }

            int accumulateTo = year.ConvertOptionalParameterToInt("Year", stochArray.MaxHorizon);

            if (accumulateTo < 1 || accumulateTo > stochArray.MaxHorizon)
            {
                throw new StochArrayHorizonException(accumulateTo, stochArray.MaxHorizon);
            }

            var accumulationEnum = accumulate.ToString().ParseAsEnum<Accumulation>();
            accumulationEnum = accumulationEnum == Accumulation.NotSpecified ? Accumulation.Annualised : accumulationEnum;

            var sampleResult = stochArray.Accumulate(accumulateTo, accumulationEnum);

            _ = sampleCache.TryAddItem(sampleResult, out var sampleHash, nameof(Accumulate), hash, year, accumulate);

            return sampleHash;

        }

        public static string Combine(object[,] hashes, object[,] proportions, object rebalancing)
        {
            // Check first if the result is already in the cache
            var stochArrayCache = Cache<StochArray>.GetCache;
            if (stochArrayCache.TryGetItem(out _, out string key, nameof(Combine), hashes, proportions, rebalancing)) { return key; }


            //tranform inputs
            var hashes1d = hashes.Convert2DParameterTo1DArray("Stoch Array Hashes")
                                 .Select(val => val.ToString())
                                 .ToArray();

            var proportions1d = proportions.Convert2DParameterTo1DArray("Stoch Array Proportions")
                                           .Select(val => val.ConvertParameterToDouble("Proportion"))
                                           .ToArray();

            Rebalancing parsedRebalEnum = rebalancing.ToString().ParseAsEnum<Rebalancing>();
            parsedRebalEnum = parsedRebalEnum == Rebalancing.NotSpecified ? Rebalancing.Rebalance : parsedRebalEnum;

            int stochArrayNo = hashes1d.Length;
            int proportionsNo = proportions1d.Length;

            // validate            
            if (!proportions1d.Sum().EqualsWithPrecision(1d))
            {
                throw new ArgumentException("Sum of porportions does not equal to 1");
            }

            if (stochArrayNo != proportionsNo)
            {
                throw new ArgumentException($"Number of proportions ({proportionsNo}) does not equal number of stoch array hashes ({stochArrayNo})");
            }

            var stochArrays = new StochArray[stochArrayNo];

            for (int i = 0; i < stochArrayNo; i++)
            {
                if (!stochArrayCache.TryGetItem(hashes1d[i], out stochArrays[i]))
                {
                    throw new MissingStochArrayException(hashes1d[i]);
                }
            }

            int maxSim = stochArrays.First().MaxSim;
            int maxHor = stochArrays.First().MaxHorizon;
            foreach (var stochArray in stochArrays)
            {
                if (stochArray.MaxHorizon != maxHor)
                {
                    throw new StochArrayDimensionsMismatchException("Horizon", maxHor, stochArray.MaxHorizon);
                }

                if (stochArray.MaxSim != maxSim)
                {
                    throw new StochArrayDimensionsMismatchException("Simulations", maxSim, stochArray.MaxSim);
                }
            }

            // calculate
            var combinedArray = StochArray.Combine(stochArrays, proportions1d, parsedRebalEnum);
            stochArrayCache.TryAddItem(combinedArray, out string newKey, nameof(Combine), hashes, proportions, rebalancing);
            return newKey;

        }

        public static double VAR(string hash, object year, object confidenceLevel)
        {
            // Check first if the result is already in the cache
            var resultsCache = Cache<double>.GetCache;
            if (resultsCache.TryGetItem(out var result, out _, nameof(VAR), hash, year, confidenceLevel)) { return result; }

            // If not, perform the calcs
            var stochArrayCache = Cache<StochArray>.GetCache;
            if (!stochArrayCache.TryGetItem(hash, out var stochArray)) { throw new MissingStochArrayException(hash); }

            int varAsAtYear = year.ConvertOptionalParameterToInt("Year", 1);
            double varConfidenceLevel = confidenceLevel.ConvertParameterToDouble("Confidence Level");

            if (varAsAtYear < 1 || varAsAtYear > stochArray.MaxHorizon)
            {
                throw new StochArrayHorizonException(varAsAtYear, stochArray.MaxHorizon);
            }

            if (varConfidenceLevel <0 || varConfidenceLevel>1)
            {
                throw new ArgumentException($"Confidence level needs to be between 0 and 1. Argument passed in: {varConfidenceLevel}");

            }

            double newResult = stochArray.VAR(varAsAtYear, varConfidenceLevel);

            _ = resultsCache.TryAddItem(newResult, out var _, nameof(Accumulate), hash, year, confidenceLevel);

            return newResult;

        }



    }
}
