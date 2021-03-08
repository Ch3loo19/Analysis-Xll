using Functions_Logic.ESG_Classes;
using Functions_Logic.Utilities;
using LightBDD.NUnit3;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using static Functions_Logic.Logic.SampleLogic;

namespace Functions_Logic.Tests.Logic.Tests
{
    public partial class SampleLogicTests : FeatureFixture
    {
        //+ Constants and fields
        private const string CacheIdentifier = "Sample";
        private const string InvalidKey = "Desu";

        private string _sampleHash;
        private Cache<Sample> _sampleCache;

        //+ TestMethods    
        private void a_valid_sample(double[] data)
        {
            var sample = new Sample(data);
            bool result = _sampleCache.TryAddItem(sample, out _sampleHash, CacheIdentifier);
            result.ShouldBeTrue();
        }

        private void mean_works_as_intended()
        {
            Should.NotThrow(() => Mean(_sampleHash));
        }

        private void mean_fails_when_sample_is_not_found()
        {
            Should.Throw<MissingSampleException>(() => Mean(InvalidKey));
        }

        private void median_works_as_intended()
        {
            Should.NotThrow(() => Median(_sampleHash));
        }

        private void median_fails_when_sample_is_not_found()
        {
            Should.Throw<MissingSampleException>(() => Median(InvalidKey));
        }

        private void volatility_works_as_intended()
        {
            Should.NotThrow(() => Volatility(_sampleHash));
        }

        private void volatility_fails_when_sample_is_not_found()
        {
            Should.Throw<MissingSampleException>(() => Volatility(InvalidKey));
        }

        private void skewness_works_as_intended()
        {
            Should.NotThrow(() => Skewness(_sampleHash));
        }

        private void skewness_fails_when_sample_is_not_found()
        {
            Should.Throw<MissingSampleException>(() => Skewness(InvalidKey));
        }

        private void kurtosis_works_as_intended()
        {
            Should.NotThrow(() => Kurtosis(_sampleHash));
        }

        private void kurtosis_fails_when_sample_is_not_found()
        {
            Should.Throw<MissingSampleException>(() => Kurtosis(InvalidKey));
        }

        private void min_works_as_intended()
        {
            Should.NotThrow(() => Min(_sampleHash));
        }

        private void min_fails_when_sample_is_not_found()
        {
            Should.Throw<MissingSampleException>(() => Min(InvalidKey));
        }
        private void max_works_as_intended()
        {
            Should.NotThrow(() => Max(_sampleHash));
        }

        private void max_fails_when_sample_is_not_found()
        {
            Should.Throw<MissingSampleException>(() => Max(InvalidKey));
        }

        private void SpreadVAR_works_as_intended()
        {
            Should.NotThrow(() => Max(_sampleHash));
        }

        private void SpreadVAR_fails_when_sample_is_not_found()
        {
            Should.Throw<MissingSampleException>(() => Max(InvalidKey));
        }

        private void percentile_works_as_intended(object percentileRnk)
        {
            Should.NotThrow(() => Percentile(_sampleHash, percentileRnk));
        }

        private void percentile_fails_when_sample_is_not_found()
        {
            Should.Throw<MissingSampleException>(() => Percentile(InvalidKey, 0.05));
        }

        private void percentile_fails_when_invalid_arguments_are_passed_in(object percentileRnk, Type exceptionType)
        {
            Should.Throw(() => Percentile(_sampleHash, percentileRnk), exceptionType);
        }



        //+ Test Data
        private static IEnumerable<object> GetValidSamples()
        {
            yield return new double[] { 0, 0 };
            yield return new double[] { 0 };
        }

        private static IEnumerable<object> GetMissingSample()
        {
            yield return new double[] { 0, 0 };
        }
        private static IEnumerable<object> GetValidPercentileSamples()
        {
            yield return new object[] { new double[] { 0, 1 }, 0.05 };
            yield return new object[] { new double[] { 0, 1 }, "0.05" };
            yield return new object[] { new double[] { 0 }, 0.1 };
            yield return new object[] { new double[] { 0 }, 0.5 };
        }

        private static IEnumerable<object> GetInvalidPercentileSamples()
        {
            yield return new object[] { new double[] { 0, 1 }, -0.05, typeof(ArgumentException) };
            yield return new object[] { new double[] { 0, 1 }, 1.05, typeof(ArgumentException) };
            yield return new object[] { new double[] { 0, 1 }, "Desu", typeof(GenericArgumentParseException) };
        }


        //+ TestFlow
        [SetUp]
        public void TestInitialiser()
        {
            _sampleCache = Cache<Sample>.GetCache;
            _sampleHash = "";
        }

        [TearDown]
        public void TestCompletion()
        {
            _sampleCache.ResetCache();
        }
    }
}