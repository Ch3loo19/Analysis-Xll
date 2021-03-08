using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;
using NUnit.Framework;

namespace Functions_Logic.Tests.Utilities.Tests
{
    [Label("Cache tests")]
    [FeatureDescription(@"In order to work with ESGs I need a functioning cache to store and retieve results from")]
    public partial class CacheTests
    {
        [Label("SCENARIO-1: I cannot get an item from an empty cache")]
        [Scenario]
        public void Get_item_from_empty_cache_test()
        {
            Runner.RunScenario(I_cannot_retrieve_an_item_from_an_empty_cache);
        }

        [Label("SCENARIO-2: I cannot get an item via wrong key")]
        [Scenario]
        public void Get_item_via_wrong_key_test()
        {
            Runner.RunScenario(
                Given => a_valid_result_cache_item(),
                Then => I_cannot_retrieve_an_item_via_wrong_key());
        }

        [Label("SCENARIO-3: I cannot add an item in the cache twice")]
        [Scenario]
        public void Add_item_to_cache_twice_test()
        {
            Runner.RunScenario(
               Given => a_valid_result_cache_item(),
               Then => I_cannot_add_an_item_to_the_cache_twice());
        }

        [Label("SCENARIO-4: Caches are separated by underlyin class type")]
        [Scenario]
        public void Caches_stay_seaprated_among_classes_test()
        {
            Runner.RunScenario(
               Given => a_valid_result_cache_item(),
               Then => I_cannot_retrieve_the_item_from_the_wrong_cache(),
               But => I_can_retrieve_the_item_from_the_correct_cache());
        }

        [Label("SCENARIO-5: The cache can be successfully reset")]
        [Scenario]
        public void Cache_reset_test()
        {
            Runner.RunScenario(
               Given => a_valid_result_cache_item(),
               And => another_valid_result_cache_item(),
               Then => resetting_the_cache_removes_the_items_from_cache());
        }

        [Label("SCENARIO-6: A missing item can be successfully added to cache")]
        [Scenario]
        public void Add_missing_item_to_cache_test()
        {
            Runner.RunScenario(
               Given => a_valid_result_cache_item(),
               Then => I_cannot_retrieve_a_missing_cache_item(),
               But_given => another_valid_result_cache_item(),
               Then => I_can_retrieve_an_item_existing_in_the_cache());
        }

        [Label("SCENARIO-7: An existing cache item can be successfully retrieved")]
        [Scenario]
        public void Get_existing_item_from_cache_test()
        {
            Runner.RunScenario(
               Given => a_valid_stochArray_cache_item(),
               Then => I_can_retrieve_the_stochArray_from_the_cache());
        }

        [Label("SCENARIO-8a: A missing item cannot be added to the cache if null items provided")]
        [Scenario]
        public void Add_item_all_null_hash_inputs_test()
        {
            Runner.RunScenario(I_cannot_add_an_item_with_null_inputs);
        }

        [Label("SCENARIO-8b: A missing item cannot be added to the cache if some null items provided")]
        [Scenario]
        public void Add_item_some_null_hash_inputs_test()
        {
            Runner.RunScenario(I_cannot_add_an_item_with_some_null_inputs);
        }

        [Label("SCENARIO-8c: Empty string inputs don't geenrate new hash when adding new items")]
        [Scenario]
        public void Add_item_some_white_space_hash_inputs_test()
        {
            Runner.RunScenario(
               Given => a_valid_result_cache_item(),
               Then => I_cannot_duplicate_cached_items_by_passing_in_empty_strings());
        }

        [Label("SCENARIO-9a: An existing item cannot be retrieved from the cache if null items provided")]
        [Scenario]
        public void Get_existing_item_no_hash_inputs_test()
        {
            Runner.RunScenario(I_cannot_get_an_item_with_null_inputs);
        }

        [Label("SCENARIO-9b: An existing item cannot be retrieved from the cache if some null items provided")]
        [Scenario]
        public void Get_item_some_null_hash_inputs_test()
        {
            Runner.RunScenario(I_cannot_get_an_item_with_some_null_inputs);
        }

        [Label("SCENARIO-8c: Empty string inputs don't affect the retrieval of existing cached item")]
        [Scenario]
        public void Get_item_some_white_space_hash_inputs_test()
        {
            Runner.RunScenario(
               Given => a_valid_result_cache_item(),
               Then => I_can_get_cached_items_when_passing_in_superfluous_empty_strings());
        }

        [Label("SCENARIO-9: Cache instance is static")]
        [Scenario]
        public void Accessing_cache_multiple_times_test()
        {
            Runner.RunScenario(
               Given => a_valid_result_cache_item(),
               Then_if => I_access_the_cache_again_the_result_is_still_there());
        }

        [Label("SCENARIO-10s: Cache can deal with arrays of 1 and 2 dimensions")]
        [TestCaseSource(nameof(GetHashingData))]
        [Scenario]
        public void Arrays_of_1D_and_2D_get_hashed_correctly_test(object array, object input1, object input2, object input3, object input4)
        {
            Runner.RunScenario(
                _ => Arrays_of_1D_and_2D_get_hashed_correctly(array, input1, input2, input3, input4));
        }



    }
}