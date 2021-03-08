using Functions_Logic.ESG_Classes;
using Functions_Logic.Utilities;
using LightBDD.NUnit3;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using static Functions_Logic.Logic.StochArrayLogic;

namespace Functions_Logic.Tests.Logic.Tests
{
    public partial class StochArrayLogicTests : FeatureFixture
    {
        //+ Constants and fields
        private const string CacheIdentifier = "StochArray";
        private const string CacheIdentifier2 = "StochArray2";
        private const string InvalidKey = "Desu";

        private string _stochArrayHash;
        private string _stochArrayHash2;
        private Cache<StochArray> _stochArrayCache;

        //+ TestMethods    
        private void a_valid_stochArray(double[][] data)
        {
            var stochArray = new StochArray(data);
            bool result = _stochArrayCache.TryAddItem(stochArray, out _stochArrayHash, CacheIdentifier);
            result.ShouldBeTrue();
        }


        private void another_valid_stochArray(double[][] data)
        {
            var stochArray = new StochArray(data);
            bool result = _stochArrayCache.TryAddItem(stochArray, out _stochArrayHash2, CacheIdentifier2);
            result.ShouldBeTrue();
        }

        private void mean_works_as_intended()
        {
            Should.NotThrow(() => Mean(_stochArrayHash));
        }

        private void mean_fails_when_stochArray_is_not_found()
        {
            Should.Throw<MissingStochArrayException>(() => Mean(InvalidKey));
        }

        private void median_works_as_intended()
        {
            Should.NotThrow(() => Median(_stochArrayHash));
        }

        private void median_fails_when_stochArray_is_not_found()
        {
            Should.Throw<MissingStochArrayException>(() => Median(InvalidKey));
        }

        private void volatility_works_as_intended()
        {
            Should.NotThrow(() => Volatility(_stochArrayHash));
        }

        private void volatility_fails_when_stochArray_is_not_found()
        {
            Should.Throw<MissingStochArrayException>(() => Volatility(InvalidKey));
        }

        private void value_works_as_intended(object sim, object horizon, double expectation)
        {
            double result = Value(_stochArrayHash, sim, horizon);
            result.ShouldBe(expectation);
        }

        private void value_fails_when_stochArray_is_not_found()
        {

            Should.Throw<MissingStochArrayException>(() => Value(InvalidKey, 1, 1));
        }

        private void value_fails_when_invalid_arguments_are_passed_in(object sim, object horizon, Type exceptionType)
        {
            Should.Throw(() => Value(_stochArrayHash, sim, horizon), exceptionType);
        }

        private void get_year_as_sample_works_as_intended(object year )
        {
            Should.NotThrow(() => GetYearAsSample(_stochArrayHash, year));
        }

        private void get_year_as_sample_fails_when_stochArray_is_not_found(object year)
        {
            Should.Throw<MissingStochArrayException>(() => GetYearAsSample(InvalidKey, year));
        }

        private void get_year_as_sample_fails_when_invalid_arguments_are_passed_in(object year, Type exceptionType)
        {
            Should.Throw(() => GetYearAsSample(_stochArrayHash, year), exceptionType);
        }

        private void accumulate_works_as_intended(object year, object annualisation)
        {
            Should.NotThrow(() => Accumulate(_stochArrayHash, year, annualisation));
        }

        private void accumulate_fails_when_stochArray_is_not_found(object year, object annualisation)
        {
            Should.Throw<MissingStochArrayException>(() => Accumulate(InvalidKey, year, annualisation));
        }

        private void accumulate_fails_when_invalid_arguments_are_passed_in(object year, object annualisation, Type exceptionType)
        {
            Should.Throw(() => Accumulate(_stochArrayHash, year, annualisation), exceptionType);
        }

        private void combine_works_as_intended(object[,] proportions, object rebalance)
        {
            var hashes = new string[,] { { _stochArrayHash }, { _stochArrayHash2 } };
            Should.NotThrow(() => Combine(hashes, proportions, rebalance));
        }

        private void combine_fails_when_at_least_one_stochArray_is_not_found(string[] identifiers)
        {
            var hashes1D = identifiers.Select(str => Cache<StochArray>.GetHashString(str)).ToArray();
            var hashes2D = new string[2, 1];
            hashes2D[0, 0] = hashes1D[0];
            hashes2D[1, 0] = hashes1D[1];

            var proportions = new object[,] { { 0.5 }, { 0.5 } };
            Should.Throw<MissingStochArrayException>(() => Combine(hashes2D, proportions, string.Empty));
        }

        private void combine_fails_when_invalid_arguments_are_passed_in(object[,] proportions, object rebalance, Type exceptionType)
        {
            var hashes = new string[,] { { _stochArrayHash }, { _stochArrayHash2 } };
            Should.Throw(() => Combine(hashes, proportions, rebalance), exceptionType);

        }

        private void VAR_works_as_intended(object year, object confidenceLevel)
        {
            Should.NotThrow(() => VAR(_stochArrayHash, year, confidenceLevel));
        }

        private void VAR_fails_when_stochArray_is_not_found(object year)
        {
            Should.Throw<MissingStochArrayException>(() => VAR(InvalidKey, year, 0.05));
        }

        private void VAR_fails_when_invalid_arguments_are_passed_in(object year, object confidenceLevel,  Type exceptionType)
        {
            Should.Throw(() => VAR(_stochArrayHash, year, confidenceLevel), exceptionType);
        }

        //+ TestData
        private static IEnumerable<object> GetValidStochArrays()
        {
            yield return new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } };
            yield return new double[][] { new double[] { 0 }, new double[] { 0 } };
            yield return new double[][] { new double[] { 0 } };
        }

        private static IEnumerable<object> GetMissingStochArray()
        {
            yield return new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } };
        }

        private static IEnumerable<object> GetValidGetYearAsSampleStochArrays()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, 2 };
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { 0 } }, 1 };
        }

        private static IEnumerable<object> GetMissingGetYearAsSampleStochArray()
        {
            yield return new object[] { new double[][] { new double[] { 0, 0 }, new double[] { 0, 0 } }, 2 };
        }

        private static IEnumerable<object> GetInvalidGetYearAsSampleStochArrays()
        {
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { -1 } }, -1,  typeof(StochArrayHorizonException) };
            yield return new object[] { new double[][] { new double[] { 0 }, new double[] { -1 } }, 2,  typeof(StochArrayHorizonException) };
            yield return new object[] { new double[][] { new double[] { 0 } }, "2.1",  typeof(GenericArgumentParseException) };
            yield return new object[] { new double[][] { new double[] { 0 } }, "Desu",  typeof(GenericArgumentParseException) };
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
            yield return new object[] { new double[][] { new double[] { 0 } }, new double[][] { new double[] { 0 } }, new string[] { CacheIdentifier, InvalidKey } };
            yield return new object[] { new double[][] { new double[] { 0 } }, new double[][] { new double[] { 0 } }, new string[] { InvalidKey, CacheIdentifier2 } };
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
            _stochArrayCache = Cache<StochArray>.GetCache;
            _stochArrayHash = "";
            _stochArrayHash2 = "";
        }

        [TearDown]
        public void TestCompletion()
        {
            _stochArrayCache.ResetCache();
        }
    }
}