using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;
using NUnit.Framework;
using System;

namespace Functions_Logic.Tests.Importers.Tests
{
    [Label("RangeLoader Tests")]
    [FeatureDescription(@"In order to work with ESGs I need to load samples from excel")]
    public partial class RangeLoaderTests
    {
        [Label("SCENARIO-1s: Sample load works correctly")]
        [TestCaseSource(nameof(GetValidLoadSampleData))]
        [Scenario]
        public void Given_valid_data_I_can_load_a_sample_test(object[,] data, double[] expectation)
        {
            Runner.RunScenario(
                _ => Given_valid_DATA_i_can_load_a_sample(data, expectation));
        }

        [Label("SCENARIO-2s: Sample load fails appropriately when I pass in invalid data")]
        [TestCaseSource(nameof(GetInvalidLoadSampleData))]
        [Scenario]
        public void Given_invalid_DATA_i_get_appropriate_error_test(object[,] data, Type exceptionType)
        {
            Runner.RunScenario(
              _ => Given_invalid_DATA_i_get_appropriate_error(data, exceptionType));
        }
    }
}