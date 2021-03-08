using Functions_Logic.ESG_Classes;
using Functions_Logic.Utilities;
using LightBDD.NUnit3;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using static Functions_Logic.Importers.RangeLoader;

namespace Functions_Logic.Tests.Importers.Tests
{
    public partial class RangeLoaderTests : FeatureFixture
    {
        //+ Fileds
        Cache<Sample> _cache;

        //+ TestMethods
        private void Given_valid_DATA_i_can_load_a_sample(object[,] data, double[] expectation)
        {
            string hash = LoadSample(data);
            bool cacheOperation = _cache.TryGetItem(hash, out var sample);
            cacheOperation.ShouldBeTrue();
            sample.Values.ShouldBe(expectation);
        }

        private void Given_invalid_DATA_i_get_appropriate_error(object[,] data, Type exceptionType)
        {
            Should.Throw(() => LoadSample(data), exceptionType);
        }

        //+ TestData
        private static IEnumerable<object> GetValidLoadSampleData()
        {
            yield return new object[] { new object[,] { { 1 } }, new[] { 1d } };
            yield return new object[] { new object[,] { { 1 }, { 2 }, { 3 }, { 4 } }, new[] { 1d, 2, 3, 4 } };
            yield return new object[] { new object[,] { { 1, 2, 3, 4 } }, new[] { 1d, 2, 3, 4 } };
            yield return new object[] { new object[,] { { 1, 2d, 3f, 4 } }, new[] { 1d, 2, 3, 4 } };
            yield return new object[] { new object[,] { { 1, "2", "3", 4 } }, new[] { 1d, 2, 3, 4 } };
            yield return new object[] { new object[,] { { "1", "2", "3", "4" } }, new[] { 1d, 2, 3, 4 } };

        }

        private static IEnumerable<object> GetInvalidLoadSampleData()
        {
            yield return new object[] { new object[,] { { 1, 2 }, { 3, 4 } }, typeof(SampleShapeException) };
            yield return new object[] { new object[,] { { "1a", 2, 3, 4 } }, typeof(GenericArgumentParseException) };
            yield return new object[] { new object[,] { { 1, 2, "Desu", 4 } }, typeof(GenericArgumentParseException) };
        }

        //+ TestFlow
        [SetUp]
        public void TestInitialiser()
        {
            _cache = Cache<Sample>.GetCache;
        }

        [TearDown]
        public void TestCompletion()
        {
            _cache.ResetCache();
        }

    }
}