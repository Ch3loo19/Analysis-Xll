using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;
using NUnit.Framework;
using System;

namespace Functions_Logic.Tests.Logic.Tests
{
    [Label("Stoch Array Logic Tests")]
    [FeatureDescription("The StochArray logic behind the Excel APIs is called via the StochArrayLogic class which is tested here")]
    public partial class StochArrayLogicTests
    {
        [Label("Mean works as intended with valid stoch arrays")]
        [TestCaseSource(nameof(GetValidStochArrays))]
        [Scenario]
        public void Mean_works_correctly_test(double[][] data)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => mean_works_as_intended());
        }

        [Label("Mean fails when no matching stoch array is found in cache")]
        [TestCaseSource(nameof(GetMissingStochArray))]
        [Scenario]
        public void Mean_fails_when_no_stochArray_test(double[][] data)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => mean_fails_when_stochArray_is_not_found());
        }

        [Label("Median works as intended with valid stoch arrays")]
        [TestCaseSource(nameof(GetValidStochArrays))]
        [Scenario]
        public void Median_works_correctly_test(double[][] data)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => median_works_as_intended());
        }

        [Label("Median fails when no matching stoch array is found in cache")]
        [TestCaseSource(nameof(GetMissingStochArray))]
        [Scenario]
        public void Median_fails_when_no_stochArray_test(double[][] data)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => median_fails_when_stochArray_is_not_found());
        }

        [Label("Volatility works as intended with valid stoch arrays")]
        [TestCaseSource(nameof(GetValidStochArrays))]
        [Scenario]
        public void Volatility_works_correctly_test(double[][] data)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => volatility_works_as_intended());
        }

        [Label("Volatility fails when no matching stoch array is found in cache")]
        [TestCaseSource(nameof(GetMissingStochArray))]
        [Scenario]
        public void Volatility_fails_when_no_stochArray_test(double[][] data)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => volatility_fails_when_stochArray_is_not_found());
        }

        [Label("Value works as intended with valid stoch arrays")]
        [TestCaseSource(nameof(GetValidValueStochArrays))]
        [Scenario]
        public void Value_works_correctly_test(double[][] data, object sim, object horizon, double expectation)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => value_works_as_intended(sim, horizon, expectation));
        }

        [Label("Value fails when no matching stoch array is found in cache")]
        [TestCaseSource(nameof(GetMissingValueStochArray))]
        [Scenario]
        public void Value_fails_when_no_stochArray_test(double[][] data)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => value_fails_when_stochArray_is_not_found());
        }

        [Label("Value fails when invlid parameters passed in")]
        [TestCaseSource(nameof(GetInvalidValueStochArrays))]
        [Scenario]
        public void Value_fails_when_invalid_arguments_test(double[][] data, object sim, object horizon, Type type)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => value_fails_when_invalid_arguments_are_passed_in(sim, horizon, type));
        }

        [Label("GetYearAsSample works as intended with valid stoch arrays")]
        [TestCaseSource(nameof(GetValidGetYearAsSampleStochArrays))]
        [Scenario]
        public void GetYearAsSample_works_correctly_test(double[][] data, object year)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => get_year_as_sample_works_as_intended(year));
        }

        [Label("GetYearAsSample fails when no matching stoch array is found in cache")]
        [TestCaseSource(nameof(GetMissingGetYearAsSampleStochArray))]
        [Scenario]
        public void GetYearAsSample_fails_when_no_stochArray_test(double[][] data, object year)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => get_year_as_sample_fails_when_stochArray_is_not_found(year));
        }

        [Label("GetYearAsSample fails when invlid parameters passed in")]
        [TestCaseSource(nameof(GetInvalidGetYearAsSampleStochArrays))]
        [Scenario]
        public void GetYearAsSample_fails_when_invalid_arguments_test(double[][] data, object year, Type type)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => get_year_as_sample_fails_when_invalid_arguments_are_passed_in(year, type));
        }

        [Label("Accumulate works as intended with valid stoch arrays")]
        [TestCaseSource(nameof(GetValidAccumulateStochArrays))]
        [Scenario]
        public void Accumulate_works_correctly_test(double[][] data, object year, object annualisation)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => accumulate_works_as_intended(year, annualisation));
        }

        [Label("Accumulate fails when no matching stoch array is found in cache")]
        [TestCaseSource(nameof(GetMissingAccumulateStochArray))]
        [Scenario]
        public void Accumulate_fails_when_no_stochArray_test(double[][] data, object year, object annualisation)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => accumulate_fails_when_stochArray_is_not_found(year, annualisation));
        }

        [Label("Accumulate fails when invlid parameters passed in")]
        [TestCaseSource(nameof(GetInvalidAccumulateStochArrays))]
        [Scenario]
        public void Accumulate_fails_when_invalid_arguments_test(double[][] data, object year, object annualisation, Type type)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => accumulate_fails_when_invalid_arguments_are_passed_in(year, annualisation, type));
        }

        [Label("Combine works as intended with valid stoch arrays")]
        [TestCaseSource(nameof(GetValidCombineStochArrays))]
        [Scenario]
        public void Combine_works_correctly_test(double[][] data1, double[][] data2, object[,] proportions, object rebalance)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data1),
             And_given => another_valid_stochArray(data2),
             Then => combine_works_as_intended(proportions, rebalance));
        }

        [Label("Combine fails when no matching stoch array is found in cache")]
        [TestCaseSource(nameof(GetMissingCombineStochArray))]
        [Scenario]
        public void Combine_fails_when_no_stochArray_test(double[][] data1, double[][] data2, string[] hashes1D)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data1),
             And_given => another_valid_stochArray(data2),
             Then => combine_fails_when_at_least_one_stochArray_is_not_found(hashes1D));
        }

        [Label("Combine fails when invlid parameters passed in")]
        [TestCaseSource(nameof(GetInvalidCombineStochArrays))]
        [Scenario]
        public void Combine_fails_when_invalid_arguments_test(double[][] data1, double[][] data2, object[,] proportions, object rebalance, Type type)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data1),
             And_given => another_valid_stochArray(data2),
             Then => combine_fails_when_invalid_arguments_are_passed_in(proportions, rebalance, type));
        }

        [Label("VAR works as intended with valid stoch arrays")]
        [TestCaseSource(nameof(GetValidVARStochArrays))]
        [Scenario]
        public void VAR_works_correctly_test(double[][] data, object year, object confidenceLevel)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => VAR_works_as_intended(year, confidenceLevel));
        }

        [Label("VAR fails when no matching stoch array is found in cache")]
        [TestCaseSource(nameof(GetMissingVARStochArray))]
        [Scenario]
        public void VAR_fails_when_no_stochArray_test(double[][] data, object year)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => VAR_fails_when_stochArray_is_not_found(year));
        }

        [Label("VAR fails when invlid parameters passed in")]
        [TestCaseSource(nameof(GetInvalidVARStochArrays))]
        [Scenario]
        public void VAR_fails_when_invalid_arguments_test(double[][] data, object year, object confidenceLevel, Type type)
        {
            Runner.RunScenario(
             Given => a_valid_stochArray(data),
             Then => VAR_fails_when_invalid_arguments_are_passed_in(year, confidenceLevel, type));
        }
    }
}