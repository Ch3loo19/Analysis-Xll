using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;
using NUnit.Framework;
using System;

namespace Functions_Logic.Tests.Utilities.Tests
{
    [Label("Extension Methods Tests")]
    [FeatureDescription("These are tests on exetension methods for Functions Logic")]
    public partial class ExtensionsTests
    {
        [Label("SCENARIO-1s: Testing investion")]
        [TestCaseSource(nameof(GetValueArrayNestedData))]
        [Scenario]
        public void Value_array_inversion_works_as_expected_test(double[][] input, double[][] result)
        {
            Runner.RunScenario(_ => Given_INPUT_I_get_RESULT(input, result));
        }

        [Label("SCENARIO-2s: Testing conversion from 2D to 1D")]
        [TestCaseSource(nameof(GetValid2DData))]
        [Scenario]
        public void Conversion_from_2D_to_1D_works_well_test(object[,] input, object[] expectation)
        {
            Runner.RunScenario(_ => Given_INPUT_I_get_RESULT(input, expectation));
        }


        [Label("SCENARIO-3s: Conversion from 2D to 1D fails when input has multiple columns")]
        [TestCaseSource(nameof(GetInvalid2DData))]
        [Scenario]
        public void Conversion_from_2D_to_1D_fails_when_more_than_1_column_test(object[,] input, Type exceptionType)
        {
            Runner.RunScenario(_ => Given_2D_array_with_multiple_columns_conversion_fails(input, exceptionType));
        }

    }
}