using Functions_Logic.ESG_Classes;
using Functions_Logic.Utilities;
using System;
using System.Linq;

namespace Functions_Logic.Logic
{
    public static class FxArrayLogic
    {

        public static double GetCurrentExchangeRate(string hash)
        {
            var cache = Cache<FxArray>.GetCache;
            if (!cache.TryGetItem(hash, out var fxArray)) { throw new MissingFxArrayException(hash); }
            return fxArray.CurrentExchangeRate;
        }

        public static string GetCurrencyReturns(string hash, string accumulation)
        {
            var cache = Cache<FxArray>.GetCache;
            var resultCache = Cache<StochArray>.GetCache;

            if (resultCache.TryGetItem(out var result, out string key, nameof(GetCurrencyReturns), hash, accumulation)) { return key; }

            var accumParsed = accumulation.ParseAsEnum<Accumulation>();
            if (accumParsed == Accumulation.NotSpecified)
            {
                throw new ArgumentException("Accumulation for currency returns needs to be specified.");
            }

            if (!cache.TryGetItem(hash, out var fxArray)) { throw new MissingFxArrayException(hash); }

            result = fxArray.GetCurrencyReturns(accumParsed);

            resultCache.TryAddItem(result, out key, nameof(GetCurrencyReturns), hash, accumulation);

            return key;

        }

        public static string GetLocalStochArrayReturns(string fxHash, string gbpReturnsHash)
        {
            var fxCache = Cache<FxArray>.GetCache;
            var stochArrayCache = Cache<StochArray>.GetCache;

            if (stochArrayCache.TryGetItem(out var result, out string key, nameof(GetLocalStochArrayReturns), fxHash, gbpReturnsHash)) { return key; }

            if (!fxCache.TryGetItem(fxHash, out var fxArray)) { throw new MissingFxArrayException(fxHash); }
           
            if (!stochArrayCache.TryGetItem(gbpReturnsHash, out var gbpReturnsArray)) { throw new MissingStochArrayException(gbpReturnsHash); }

            if (fxArray.MaxHorizon != gbpReturnsArray.MaxHorizon || fxArray.MaxSim != fxArray.MaxSim)
            {
                throw new ArgumentException("Fx Array and StochArray need to have the same dimensions for currency conversion");
            }

            result = fxArray.GetLocalStochArrayReturns(gbpReturnsArray);
            stochArrayCache.TryAddItem(result, out key, nameof(GetLocalStochArrayReturns), fxHash, gbpReturnsHash);
            return key;
        }

        public static string GetGBPStochArrayReturns(string fxHash, string localReturnsHash)
        {
            var fxCache = Cache<FxArray>.GetCache;
            var stochArrayCache = Cache<StochArray>.GetCache;

            if (stochArrayCache.TryGetItem(out var result, out string key, nameof(GetGBPStochArrayReturns), fxHash, localReturnsHash)) { return key; }

            if (!fxCache.TryGetItem(fxHash, out var fxArray)) { throw new MissingFxArrayException(fxHash); }
           
            if (!stochArrayCache.TryGetItem(localReturnsHash, out var localReturnsArray)) { throw new MissingStochArrayException(localReturnsHash); }

            if (fxArray.MaxHorizon != localReturnsArray.MaxHorizon || fxArray.MaxSim != fxArray.MaxSim)
            {
                throw new ArgumentException("Fx Array and StochArray need to have the same dimensions for currency conversion");
            }

            result = fxArray.GetGBPStochArrayReturns(localReturnsArray);
            stochArrayCache.TryAddItem(result, out key, nameof(GetGBPStochArrayReturns), fxHash, localReturnsHash);
            return key;
        }

    }
}
