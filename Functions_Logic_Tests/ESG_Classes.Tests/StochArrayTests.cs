using Functions_Logic.Utilities;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;
using NUnit.Framework;

[assembly: LightBddScope]
namespace Functions_Logic_Tests.ESG_Classes.Tests
{
    [Label("Stoch Array Tests")]
    [FeatureDescription("The StochArray is the primordial class behind the xll, holding stochastic data and methods to interact with it")]
    public partial class StochArrayTests
    {
        [Label("Mean is caluclated correctly, i.e. for each sim, across time")]
        [TestCaseSource(nameof(GetValidMeanTestData))]
        [Scenario]
        public void Mean_is_calculated_correctly_test(double[][] values, double expectation)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(values),
             Then => calculated_mean_equals_EXPECTATION(expectation));
        }

        [Label("Median is caluclated correctly, i.e. for each sim, across time")]
        [TestCaseSource(nameof(GetValidMedianTestData))]
        [Scenario]
        public void Median_is_calculated_correctly_test(double[][] values, double expectation)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(values),
             Then => calculated_median_equals_EXPECTATION(expectation));
        }

        [Label("Volatility is caluclated correctly, i.e. for each sim, across time")]
        [TestCaseSource(nameof(GetValidVolatilityTestData))]
        [Scenario]
        public void Volatility_is_calculated_correctly_test(double[][] values, double expectation)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(values),
             Then => calculated_volatility_equals_EXPECTATION(expectation));
        }

        [Label("Accumulation is caluclated correctly ")]
        [TestCaseSource(nameof(GetValidAccumulationTestData))]
        [Scenario]
        public void Accumulation_is_calculated_correctly_test(double[][] values, int year, Accumulation accum, double[] expectation)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(values),
             Then => calculated_accumulation_is_correct(year, accum, expectation));
        }

        [Label("GetYearAsSample is caluclated correctly ")]
        [TestCaseSource(nameof(GetValidGetYearAsSampleTestData))]
        [Scenario]
        public void GetYearAsSample_is_calculated_correctly_test(double[][] values, int year,  double[] expectation)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(values),
             Then => get_year_as_sample_is_correct(year, expectation));
        }

        [Label("StochArrays are combined correctly")]
        [TestCaseSource(nameof(GetValidCombineTestData))]
        [Scenario]
        public void Combine_is_calculated_correctly_test(double[][][] stochArrays, double [] proportions, Rebalancing rebalancingPolicy, double[][] expectation)
        {
            Runner.RunScenario(
             Given => valid_stochArrays(stochArrays),
             Then => combine_is_calculated_correctly(proportions, rebalancingPolicy, expectation));
        }
    }
}