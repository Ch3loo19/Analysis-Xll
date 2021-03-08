using Functions_Logic.ESG_Classes;
using Functions_Logic.Utilities;
using LightBDD.NUnit3;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using static Functions_Logic.Logic.FxArrayLogic;

namespace Functions_Logic.Tests.Logic.Tests
{
    public partial class FxArrayLogicTests : FeatureFixture
    {
        //+ Constants and fields
        private const string FxArrayIdentifier = "FxArray";
        private const string StochArrayIdentifier = "StochArray";
        private const string InvalidKey = "Desu";

        private string _fxHash;
        private string _stochArrayHash;
        private Cache<FxArray> _fxArrayCache;
        private Cache<StochArray> _stochArrayCache;

        //+ TestMethods    
        private void a_valid_FxArray(double[][] data, double eRate)
        {
            var stochArray = new FxArray(data, eRate);
            bool result = _fxArrayCache.TryAddItem(stochArray, out _fxHash, FxArrayIdentifier);
            result.ShouldBeTrue();
        }


        private void a_valid_stochArray(double[][] data)
        {
            var stochArray = new StochArray(data);
            bool result = _stochArrayCache.TryAddItem(stochArray, out _stochArrayHash, StochArrayIdentifier);
            result.ShouldBeTrue();
        }

        private void get_exchange_rate_works_as_intended()
        {
            Should.NotThrow(() => GetCurrentExchangeRate(_fxHash));
        }

        private void get_exchange_rate_fails_when_FxArray_is_not_found()
        {
            Should.Throw<MissingFxArrayException>(() => GetCurrentExchangeRate(InvalidKey));
        }

        private void get_currency_returns_works_as_intended(string accumulation)
        {
            Should.NotThrow(() => GetCurrencyReturns(_fxHash, accumulation));
        }

        private void get_currency_returns_fails_when_FxArray_is_not_found()
        {
            Should.Throw<MissingFxArrayException>(() => GetCurrencyReturns(InvalidKey,"Cumulative"));
        }
     
        private void get_local_stoch_array_returns_works_as_intended( )
        {
            Should.NotThrow(() => GetLocalStochArrayReturns(_fxHash, _stochArrayHash));
        }

        private void get_gbpl_stoch_array_returns_works_as_intended()
        {
            Should.NotThrow(() => GetGBPStochArrayReturns(_fxHash, _stochArrayHash));
        }

        private void get_local_stoch_array_returns_fails_when_objects_are_not_found()
        {
            Should.Throw<MissingStochArrayException>(() => GetLocalStochArrayReturns(_fxHash, InvalidKey));
            Should.Throw<MissingFxArrayException>(() => GetLocalStochArrayReturns(InvalidKey, _stochArrayHash));
        }

        private void get_gbp_stoch_array_returns_fails_when_objects_are_not_found()
        {
            Should.Throw<MissingStochArrayException>(() => GetGBPStochArrayReturns(_fxHash, InvalidKey));
            Should.Throw<MissingFxArrayException>(() => GetGBPStochArrayReturns(InvalidKey, _stochArrayHash));
        }

        private void get_currency_returns_fails_when_invalid_arguments_are_passed_in(string accumulation, Type exceptionType)
        {
            Should.Throw(() => GetCurrencyReturns(_fxHash, accumulation), exceptionType);
        }

        private void get_local_stoch_array_returns_fails_when_invalid_parameters_passed_in()
        {
            Should.Throw<ArgumentException>(() => GetLocalStochArrayReturns(_fxHash, _stochArrayHash));
        }

        private void get_gbp_stoch_array_returns_fails_when_invalid_parameters_passed_in()
        {
            Should.Throw<ArgumentException>(() => GetGBPStochArrayReturns(_fxHash, _stochArrayHash));
        }



        //+ TestData
        private static IEnumerable<object> GetValidFxArrays()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, 1d };
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { 0 } }, 1d };
            yield return new object[] { new double[][] { new double[] { 0 } }, 1d };
        }

        private static IEnumerable<object> GetValidCurrencyFxArrays()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, 1d, "Annualised" };
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { 0 } }, 1d, "Cumulative" };
            yield return new object[] { new double[][] { new double[] { 0 } }, 1d, "Annualised" };
        }

        private static IEnumerable<object> GetMissingFxArray()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, 1d };
        }

        private static IEnumerable<object> GetValidFxAndStochArray()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, 2d , new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } } };
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { 0 } }, 1, new double[][] { new double[] { 0 }, new double[] { 0 } } };
        }

        private static IEnumerable<object> GetMissingFxAndStochArray()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, 2 };
        }

        private static IEnumerable<object> GetInvalidCurrencyReturnsData()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, 1d, "Desu", typeof(InvalidEnumParseException<Accumulation>) };
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { 0 } }, 1d, "NotSpecified", typeof(ArgumentException) };
        }

        private static IEnumerable<object> GetInvalidFxAndStochArray()
        {
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { 0 } }, 1, new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } } };
            yield return new object[] { new double[][] { new double[] { 0,0 }, new double[] { 0, 0} }, 1, new double[][] { new double[] { 0 }, new double[] { 0 } } };

        }


        private static IEnumerable<object> GetValidAccumulateStochArrays()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, 2, "Annualised" };
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { 0 } }, string.Empty, "Cumulative" };
            yield return new object[] { new double[][] { new double[] { 0 } }, string.Empty, string.Empty };
        }

        private static IEnumerable<object> GetMissingAccumulateStochArray()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, 2, "Annualised" };
        }

        private static IEnumerable<object> GetInvalidAccumulateStochArrays()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, 2, "Desu", typeof(InvalidEnumParseException<Accumulation>) };
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { -1 } }, -1, "Cumulative", typeof(StochArrayHorizonException) };
            yield return new object[] { new double[][] { new double[] { 0 } }, "2.1", string.Empty, typeof(GenericArgumentParseException) };
        }

        private static IEnumerable<object> GetValidCombineStochArrays()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, new object[,] { { 0.5, 0.5 } }, "Rebalance" };
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, new object[,] { { 0.5, 0.5 } }, "NoRebalancing" };
            yield return new object[] { new double[][] { new double[] { 0 } }, new double[][] { new double[] { 0 } }, new object[,] { { 0.5, 0.5 } }, string.Empty };

        }

        private static IEnumerable<object> GetMissingCombineStochArray()
        {
            yield return new object[] { new double[][] { new double[] { 0 } }, new double[][] { new double[] { 0 } }, new string[] { FxArrayIdentifier, InvalidKey } };
            yield return new object[] { new double[][] { new double[] { 0 } }, new double[][] { new double[] { 0 } }, new string[] { InvalidKey, StochArrayIdentifier } };
            yield return new object[] { new double[][] { new double[] { 0 } }, new double[][] { new double[] { 0 } }, new string[] { InvalidKey, InvalidKey } };

        }

        private static IEnumerable<object> GetInvalidCombineStochArrays()
        {
            yield return new object[] { new double[][] { new double[] { 0 } }, new double[][] { new double[] { 0, 0 } }, new object[,] { { 0.5, 0.5 } }, "Rebalance", typeof(StochArrayDimensionsMismatchException) };
            yield return new object[] { new double[][] { new double[] { 0 } }, new double[][] { new double[] { 0 }, new double[] { 0 } }, new object[,] { { 0.5, 0.5 } }, "Rebalance", typeof(StochArrayDimensionsMismatchException) };
            yield return new object[] { new double[][] { new double[] { 0 } }, new double[][] { new double[] { 0 } }, new object[,] { { 0.5 }, { 0.25 }, { 0.25 } }, "Rebalance", typeof(ArgumentException) };
            yield return new object[] { new double[][] { new double[] { 0 } }, new double[][] { new double[] { 0 } }, new object[,] { { 0.5 }, { 0.75 } }, "Rebalance", typeof(ArgumentException) };
            yield return new object[] { new double[][] { new double[] { 0 } }, new double[][] { new double[] { 0 } }, new object[,] { { 0.5 }, { "Desu" } }, "Rebalance", typeof(GenericArgumentParseException) };
            yield return new object[] { new double[][] { new double[] { 0 } }, new double[][] { new double[] { 0 } }, new object[,] { { 1 } }, "Rebalance", typeof(ArgumentException) };
        }

        private static IEnumerable<object> GetValidVARStochArrays()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, 2,0.05 };
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { 0 } }, string.Empty,0 };
            yield return new object[] { new double[][] { new double[] { 0 } }, string.Empty, 1 };
        }

        private static IEnumerable<object> GetMissingVARStochArray()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, 2 };
        }

        private static IEnumerable<object> GetInvalidVARStochArrays()
        {
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { -1 } }, -1, 0.05, typeof(StochArrayHorizonException) };
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { -1 } }, 2, 0.05, typeof(StochArrayHorizonException) };
            yield return new object[] { new double[][] { new double[] { 0 } }, "2.1", 0.05, typeof(GenericArgumentParseException) };
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { -1 } }, 1, -0.05, typeof(ArgumentException) };
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { -1 } }, 1, 1.05, typeof(ArgumentException) };
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { -1 } }, 1, "DESU", typeof(GenericArgumentParseException) };

        }

        private static IEnumerable<object> GetValidValueStochArrays()
        {
            yield return new object[] { new double[][] { new double[] { 0, 1 }, new double[] { 2, 3 } }, 2, "1", 2d };
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { 1 } }, "2", 1, 1d };
            yield return new object[] { new double[][] { new double[] { 0 } }, "1", "1", 0d };
        }

        private static IEnumerable<object> GetMissingValueStochArray()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } } };
        }

        private static IEnumerable<object> GetInvalidValueStochArrays()
        {
            yield return new object[] { new double[][] { new double[] { 0, 1 }, new double[] { 2, 3 } }, -1, 2, typeof(StochArrayHorizonException) };
            yield return new object[] { new double[][] { new double[] { 0, 1 }, new double[] { 2, 3 } }, 3, 2, typeof(StochArrayHorizonException) };
            yield return new object[] { new double[][] { new double[] { 0, 1 }, new double[] { 2, 3 } }, 1, -1, typeof(StochArrayHorizonException) };
            yield return new object[] { new double[][] { new double[] { 0, 1 }, new double[] { 2, 3 } }, 1, 3, typeof(StochArrayHorizonException) };
            yield return new object[] { new double[][] { new double[] { 0 } }, "2.1", 1, typeof(GenericArgumentParseException) };
            yield return new object[] { new double[][] { new double[] { 0 } }, "Desu", 1, typeof(GenericArgumentParseException) };
        }

        //+ TestFlow
        [SetUp]
        public void TestInitialiser()
        {
            _fxArrayCache = Cache<FxArray>.GetCache;
            _stochArrayCache = Cache<StochArray>.GetCache;
            _fxHash = "";
            _stochArrayHash = "";
        }

        [TearDown]
        public void TestCompletion()
        {
            _fxArrayCache.ResetCache();
            _stochArrayCache.ResetCache();
        }
    }
}