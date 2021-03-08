using Functions_Logic.Utilities;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;
using NUnit.Framework;

namespace Functions_Logic_Tests.ESG_Classes.Tests
{
    [Label("Stoch Array Tests")]
    [FeatureDescription("The StochArray is the primordial class behind the xll, holding stochastic data and methods to interact with it")]
    public partial class FxArrayTests
    {
        [Label("GetCurrencyReturns is behaving correctly, i.e. for each sim, across time")]
        [TestCaseSource(nameof(GetCurrencyReturnsValidData))]
        [Scenario]
        public void GetCurrencyReturns_is_performed_correctly_test(double[][] rates, double currentFx, Accumulation accumulation, double[][] expectation)
        {
            Runner.RunScenario(
             Given => a_valid_fxArray(rates, currentFx),
             Then => currency_returns_equal_EXPECTATION(accumulation, expectation));
        }

        [Label("GetLocalStochArrayReturns is caluclated correctly, i.e. for each sim, across time")]
        [TestCaseSource(nameof(GetValidGetLocalStochArrayReturnsTestData))]
        [Scenario]
        public void GetLocalStochArrayReturns_is_calculated_correctly_test(double[][] fxValues, double currentFx, double[][] stochArrayValues, double[][] expectation)
        {
            Runner.RunScenario(
             Given => a_valid_fxArray(fxValues, currentFx),
             And => a_valid_stochArray(stochArrayValues),
             Then => calculated_local_stocharray_returns_equal_EXPECTATION(expectation));
        }

        [Label("GetGBPStochArrayReturns is caluclated correctly, i.e. for each sim, across time")]
        [TestCaseSource(nameof(GetValidGetGBPStochArrayReturnsTestData))]
        [Scenario]
        public void GetGBPStochArrayReturns_is_calculated_correctly_test(double[][] values, double currentFx, double[][] stochArrayValues, double[][] expectation)
        {
            Runner.RunScenario(
             Given => a_valid_fxArray(values, currentFx),
             And => a_valid_stochArray(stochArrayValues),
             Then => calculated_GBP_stoch_array_returns_equals_EXPECTATION(expectation));
        }

    }
}