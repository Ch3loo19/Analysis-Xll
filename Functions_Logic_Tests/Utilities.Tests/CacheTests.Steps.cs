using Functions_Logic.ESG_Classes;
using Functions_Logic.Utilities;
using LightBDD.NUnit3;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace Functions_Logic.Tests.Utilities.Tests
{
    public partial class CacheTests : FeatureFixture
    {
        //+ Constants
        const string SpuriousInput = "DesuInput";
        const double ValidResult = 4.0;
        const string ValidResultInput = "ValidResultInput";
        const double ValidResult2 = 21.0;
        const string ValidResultInput2 = "ValidResultInput2";
        const string ValidStochArrayInput = "ValidStochArrayInput";

        //+ Fields
        Cache<StochArray> _stochArrayCache;
        Cache<double> _resultsCache;
        StochArray _stochArray;

        //+ Steps
        private void a_valid_result_cache_item()
        {
            var cacheOperation = _resultsCache.TryAddItem(ValidResult, out _, ValidResultInput);
            cacheOperation.ShouldBeTrue();
        }
        private void a_valid_stochArray_cache_item()
        {
            var values = new double[][]
            {
                new double[] {0.02, 0.03},
                new double[] {0.01, 0.005},
                new double[] {0.0, -0.034},
            };
            _stochArray = new StochArray(values);
            var cacheOperation = _stochArrayCache.TryAddItem(_stochArray, out _, ValidStochArrayInput);
            cacheOperation.ShouldBeTrue();
        }

        private void another_valid_result_cache_item()
        {
            var cacheOperation = _resultsCache.TryAddItem(ValidResult2, out _, ValidResultInput2);
            cacheOperation.ShouldBeTrue();
        }

        private void I_cannot_retrieve_an_item_from_an_empty_cache()
        {
            var result = _resultsCache.TryGetItem(out double res, out string hash, SpuriousInput);
            result.ShouldBeFalse();
            res.ShouldBe(0d);
            hash.ShouldBe(Cache<double>.GetHashString(SpuriousInput));
        }

        private void I_cannot_retrieve_an_item_via_wrong_key()
        {
            _ = _resultsCache.TryGetItem(out double res, out _, SpuriousInput);
            res.ShouldNotBe(ValidResult);
        }

        private void I_cannot_add_an_item_to_the_cache_twice()
        {
            var cacheOperation = _resultsCache.TryAddItem(ValidResult, out _, ValidResultInput);
            cacheOperation.ShouldBeFalse();
        }
        private void I_cannot_retrieve_a_missing_cache_item()
        {
            var cacheOperation = _resultsCache.TryGetItem(out _, out _, ValidResultInput2);
            cacheOperation.ShouldBeFalse();
        }
        private void I_cannot_retrieve_the_item_from_the_wrong_cache()
        {
            var result = _stochArrayCache.TryGetItem(out var res, out _, ValidResultInput);
            result.ShouldBeFalse();
            res.ShouldBeNull();
        }
        private void I_cannot_add_an_item_with_null_inputs()
        {
            var result = _resultsCache.TryAddItem(ValidResult, out var hash, null);
            result.ShouldBeFalse();
            hash.ShouldBeNullOrWhiteSpace();

        }

        private void I_cannot_add_an_item_with_some_null_inputs()
        {
            var result = _resultsCache.TryAddItem(ValidResult, out var hash, ValidResultInput, null);
            result.ShouldBeFalse();
            hash.ShouldBeNullOrWhiteSpace();

        }

        private void I_cannot_duplicate_cached_items_by_passing_in_empty_strings()
        {
            var result = _resultsCache.TryAddItem(ValidResult, out var hash, ValidResultInput, "");
            result.ShouldBeFalse();
            hash.ShouldBe(Cache<double>.GetHashString(ValidResultInput));

        }

        private void I_cannot_get_an_item_with_null_inputs()
        {
            var cacheOperation = _resultsCache.TryGetItem(out _, out var hash, null);
            cacheOperation.ShouldBeFalse();
            hash.ShouldBeNullOrWhiteSpace();

        }

        private void I_cannot_get_an_item_with_some_null_inputs()
        {
            var cacheOperation = _resultsCache.TryGetItem(out _, out var hash, ValidResultInput, null);
            cacheOperation.ShouldBeFalse();
            hash.ShouldBeNullOrWhiteSpace();

        }

        private void I_can_get_cached_items_when_passing_in_superfluous_empty_strings()
        {
            var cacheOperation = _resultsCache.TryGetItem(out var result, out var hash, ValidResultInput, "");
            cacheOperation.ShouldBeTrue();
            hash.ShouldBe(Cache<double>.GetHashString(ValidResultInput));
            result.ShouldBe(ValidResult);

        }

        private void I_can_retrieve_the_item_from_the_correct_cache()
        {
            var result = _resultsCache.TryGetItem(out var res, out string hash, ValidResultInput);
            result.ShouldBeTrue();
            res.ShouldBe(ValidResult);
            hash.ShouldBe(Cache<double>.GetHashString(ValidResultInput));
        }

        private void I_can_retrieve_an_item_existing_in_the_cache()
        {
            var result = _resultsCache.TryGetItem(out var res, out string hash, ValidResultInput2);
            result.ShouldBeTrue();
            res.ShouldBe(ValidResult2);
            hash.ShouldBe(Cache<double>.GetHashString(ValidResultInput2));
        }

        private void I_can_retrieve_the_stochArray_from_the_cache()
        {
            var cacheOperation = _stochArrayCache.TryGetItem(out var stochArray, out string hash, ValidStochArrayInput);
            cacheOperation.ShouldBeTrue();
            stochArray.ShouldBe(_stochArray);
            hash.ShouldBe(Cache<StochArray>.GetHashString(ValidStochArrayInput));
            object.ReferenceEquals(stochArray, _stochArray);
        }

        private void resetting_the_cache_removes_the_items_from_cache()
        {
            _resultsCache.ResetCache();
            var result = _resultsCache.TryGetItem(out _, out _, ValidResultInput);
            result.ShouldBeFalse();
            result = _resultsCache.TryGetItem(out _, out _, ValidResultInput2);
            result.ShouldBeFalse();
        }

        private void I_access_the_cache_again_the_result_is_still_there()
        {
            var cache = Cache<double>.GetCache;
            var result = cache.TryGetItem(out var res, out string hash, ValidResultInput);
            result.ShouldBeTrue();
            res.ShouldBe(ValidResult);
            hash.ShouldBe(Cache<double>.GetHashString(ValidResultInput));
        }

        private void Arrays_of_1D_and_2D_get_hashed_correctly(object array, object input1, object input2, object input3, object input4)
        {
            string result = Cache<double>.GetHashString(array, "RandomString");
            string expectation = Cache<double>.GetHashString(input1, input2, input3, input4, "RandomString");
            result.ShouldBe(expectation);

        }

        //+ TestData
        private static IEnumerable<object> GetHashingData()
        {
            yield return new object[] { new object[] { 1, 2, 3, 4 }, 1, 2, 3, 4 };
            yield return new object[] { new object[] { 1, 2, 3, 4 }, 1d, 2, 3, 4 };
            yield return new object[] { new object[] { 1, 2, 3, 4 }, "1", 2, 3, 4 };
            yield return new object[] { new object[] { 1, 2, 3, 4 }, "1", "2", "3", "4" };
            yield return new object[] { new double[] { 1, 2, 3, 4 }, "1", "2", "3", "4" };
            yield return new object[] { new string[] { "", "bananarama", "coyote", " " }, "", "bananarama", "coyote", " " };
            yield return new object[] { new object[] { 1f, "2", 3d, 4 }, 1, 2d, "3", 4f };
            yield return new object[] { new object[,] { { 1f, "2", 3d, 4 } }, 1, 2d, "3", 4f };
            yield return new object[] { new object[,] { { 1f }, { "2" }, { 3d }, { 4 } }, 1, 2d, "3", 4f };
            yield return new object[] { new object[,] { { 1f, "2" }, { 3d, 4 } }, 1, 2d, "3", 4f };
        }

        //+ TestFlow
        [SetUp]
        public void TestInitialiser()
        {
            _stochArrayCache = Cache<StochArray>.GetCache;
            _resultsCache = Cache<double>.GetCache;
        }

        [TearDown]
        public void TestCompletion()
        {
            _stochArrayCache.ResetCache();
            _resultsCache.ResetCache();
        }
    }
}