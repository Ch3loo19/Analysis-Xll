using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;
using NUnit.Framework;
using System;

namespace Functions_Logic.Tests.Logic.Tests
{
    [Label("Fx Array Logic Tests")]
    [FeatureDescription("The FxArray logic behind the Excel APIs is called via the FxArrayLogic class which is tested here")]
    public partial class FxArrayLogicTests
    {
        [Label("Get current exchange rate works as intended with valid fx arrays")]
        [TestCaseSource(nameof(GetValidFxArrays))]
        [Scenario]
        public void GetCurrentExRate_works_correctly_test(double[][] data, double eRate)
        {
            Runner.RunScenario(
             Given => a_valid_FxArray(data, eRate),
             Then => get_exchange_rate_works_as_intended());
        }

        [Label("Get current exchange rate fails when no matching fx array is found in cache")]
        [TestCaseSource(nameof(GetMissingFxArray))]
        [Scenario]
        public void GetCurrentExRate_fails_when_no_FxArray_test(double[][] data, double eRate)
        {
            Runner.RunScenario(
             Given => a_valid_FxArray(data, eRate),
             Then => get_exchange_rate_fails_when_FxArray_is_not_found());
        }

        [Label("Get currency returns works as intended with valid fx arrays")]
        [TestCaseSource(nameof(GetValidCurrencyFxArrays))]
        [Scenario]
        public void GetCurrencyReturns_works_correctly_test(double[][] data, double erRate, string accumulation)
        {
            Runner.RunScenario(
             Given => a_valid_FxArray(data, erRate),
             Then => get_currency_returns_works_as_intended(accumulation));
        }

        [Label("Get currency returns fails when no matching fx array is found in cache")]
        [TestCaseSource(nameof(GetMissingFxArray))]
        [Scenario]
        public void GetCurrencyReturns_fails_when_no_FxArray_test(double[][] data, double eRate)
        {
            Runner.RunScenario(
             Given => a_valid_FxArray(data, eRate),
             Then => get_currency_returns_fails_when_FxArray_is_not_found());
        }

        [Label("Get currency reuturns fails when invlid parameters passed in")]
        [TestCaseSource(nameof(GetInvalidCurrencyReturnsData))]
        [Scenario]
        public void GetCurrencyReturns_fails_when_invalid_arguments_test(double[][] data, double eRate, string accumulation, Type type)
        {
            Runner.RunScenario(
             Given => a_valid_FxArray(data,eRate),
             Then => get_currency_returns_fails_when_invalid_arguments_are_passed_in(accumulation, type));
        }

        [Label("Get local stocharray returns works as intended with valid arrays")]
        [TestCaseSource(nameof(GetValidFxAndStochArray))]
        [Scenario]
        public void GetLocalStochArrayReturns_works_correctly_test(double[][] fxData, double eRate, double[][] stochArrayData)
        {
            Runner.RunScenario(
             Given => a_valid_FxArray(fxData, eRate),
             And => a_valid_stochArray(stochArrayData),
             Then => get_local_stoch_array_returns_works_as_intended());
        }

        [Label("Get local stocharray returns fails when no matching stoch array or fx array is found in cache")]
        [TestCaseSource(nameof(GetValidFxAndStochArray))]
        [Scenario]
        public void GetLocalStochArrayReturns_fails_when_no_arrays_test(double[][] fxData, double eRate, double[][] stochArrayData)
        {
            Runner.RunScenario(
             Given => a_valid_FxArray(fxData, eRate),
             And => a_valid_stochArray(stochArrayData),
             Then => get_local_stoch_array_returns_fails_when_objects_are_not_found());
        }

        [Label("Get local stocharray returns fails when invlid parameters passed in")]
        [TestCaseSource(nameof(GetInvalidFxAndStochArray))]
        [Scenario]
        public void GetLocalStochArrayReturns_fails_when_invalid_arguments_test(double[][] fxData, double eRate, double[][] stochArrayData)
        {
            Runner.RunScenario(
              Given => a_valid_FxArray(fxData, eRate),
             And => a_valid_stochArray(stochArrayData),
             Then => get_local_stoch_array_returns_fails_when_invalid_parameters_passed_in());
        }

        [Label("Get GBP stocharray returns works as intended with valid arrays")]
        [TestCaseSource(nameof(GetValidFxAndStochArray))]
        [Scenario]
        public void GetGBPStochArrayReturns_works_correctly_test(double[][] fxData, double eRate, double[][] stochArrayData)
        {
            Runner.RunScenario(
             Given => a_valid_FxArray(fxData, eRate),
             And => a_valid_stochArray(stochArrayData),
             Then => get_gbpl_stoch_array_returns_works_as_intended());
        }

        [Label("Get GBP stocharray returns fails when no matching stoch array or fx array is found in cache")]
        [TestCaseSource(nameof(GetValidFxAndStochArray))]
        [Scenario]
        public void GetGBPStochArrayReturns_fails_when_no_arrays_test(double[][] fxData, double eRate, double[][] stochArrayData)
        {
            Runner.RunScenario(
             Given => a_valid_FxArray(fxData, eRate),
             And => a_valid_stochArray(stochArrayData),
             Then => get_gbp_stoch_array_returns_fails_when_objects_are_not_found());
        }

        [Label("Get GBP stocharray returns fails when invlid parameters passed in")]
        [TestCaseSource(nameof(GetInvalidFxAndStochArray))]
        [Scenario]
        public void GetGBPStochArrayReturns_fails_when_invalid_arguments_test(double[][] fxData, double eRate, double[][] stochArrayData)
        {
            Runner.RunScenario(
              Given => a_valid_FxArray(fxData, eRate),
             And => a_valid_stochArray(stochArrayData),
             Then => get_gbp_stoch_array_returns_fails_when_invalid_parameters_passed_in());
        }


    }
}