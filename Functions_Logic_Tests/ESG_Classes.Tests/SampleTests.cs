using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;
using NUnit.Framework;

namespace Functions_Logic_Tests.ESG_Classes.Tests
{
    [Label("Sample Tests")]
    [FeatureDescription("The Sample is the primordial class behind the xll, holding a subset (one year) of stochastic data and methods to interact with it")]
    public partial class SampleTests
    {
        [Label("Mean is calculated correctly")]
        [TestCaseSource(nameof(GetValidMeanTestData))]
        [Scenario]
        public void Mean_is_calculated_correctly_test(double[] values, double expectation)
        {
            Runner.RunScenario(
              Given => a_valid_sample(values),
               Then => calculated_mean_equals_EXPECTATION(expectation));
        }

        [Label("Median is calculated correctly")]
        [TestCaseSource(nameof(GetValidMedianTestData))]
        [Scenario]
        public void Median_is_calculated_correctly_test(double[] values, double expectation)
        {
            Runner.RunScenario(
            Given => a_valid_sample(values),
            Then => calculated_median_equals_EXPECTATION(expectation));
        }

        [Label("Volatility is calculated correctly")]
        [TestCaseSource(nameof(GetValidVolatilityTestData))]
        [Scenario]
        public void Volatility_is_calculated_correctly_test(double[] values, double expectation)
        {
            Runner.RunScenario(
            Given => a_valid_sample(values),
              Then => calculated_volatility_equals_EXPECTATION(expectation));
        }

        [Label("Skewness is calculated correctly ")]
        [TestCaseSource(nameof(GetValidSkewnessTestData))]
        [Scenario]
        public void Skewness_is_calculated_correctly_test(double[] values, double expectation)
        {
            Runner.RunScenario(
            Given => a_valid_sample(values),
             Then => calculated_skewness_equals_EXPECTATION(expectation));
        }

        [Label("Kurtosis is calculated correctly ")]
        [TestCaseSource(nameof(GetValidKurtosisTestData))]
        [Scenario]
        public void Kurtosis_is_calculated_correctly_test(double[] values, double expectation)
        {
            Runner.RunScenario(
            Given => a_valid_sample(values),
             Then => calculated_kurtosis_equals_EXPECTATION(expectation));
        }

        [Label("Min is calculated correctly ")]
        [TestCaseSource(nameof(GetValidMinTestData))]
        [Scenario]
        public void Min_is_calculated_correctly_test(double[] values, double expectation)
        {
            Runner.RunScenario(
            Given => a_valid_sample(values),
             Then => calculated_min_equals_EXPECTATION(expectation));
        }

        [Label("Max is calculated correctly ")]
        [TestCaseSource(nameof(GetValidMaxTestData))]
        [Scenario]
        public void Max_is_calculated_correctly_test(double[] values, double expectation)
        {
            Runner.RunScenario(
            Given => a_valid_sample(values),
             Then => calculated_max_equals_EXPECTATION(expectation));
        }

        [Label("Percentile is calculated correctly ")]
        [TestCaseSource(nameof(GetValidPercentileTestData))]
        [Scenario]
        public void Percentile_is_calculated_correctly_test(double[] values, double percentileRank, double expectation)
        {
            Runner.RunScenario(
            Given => a_valid_sample(values),
             Then => calculated_percentile_equals_EXPECTATION(percentileRank, expectation));
        }

        [Label("SpreadVARis calculated correctly ")]
        [TestCaseSource(nameof(GetValidSpreadVARTestData))]
        [Scenario]
        public void SpreadVAR_is_calculated_correctly_test(double[] values, double expectation)
        {
            Runner.RunScenario(
             Given => a_valid_sample(values),
             Then => calculated_SpreadVAR_equals_EXPECTATION(expectation));
        }
    }
}