using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;
using NUnit.Framework;
using System;

namespace Functions_Logic.Tests.Logic.Tests
{
    public partial class SampleLogicTests
    {
        [Label("Mean works as intended with valid samples")]
        [TestCaseSource(nameof(GetValidSamples))]
        [Scenario]
        public void Mean_works_correctly_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
            Then => mean_works_as_intended());
        }

        [Label("Mean fails when no matching sample is found in cache")]
        [TestCaseSource(nameof(GetMissingSample))]
        [Scenario]
        public void Mean_fails_when_no_sample_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
              Then => mean_fails_when_sample_is_not_found());
        }

        [Label("Median works as intended with valid samples")]
        [TestCaseSource(nameof(GetValidSamples))]
        [Scenario]
        public void Median_works_correctly_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
            Then => median_works_as_intended());
        }

        [Label("Median fails when no matching sample is found in cache")]
        [TestCaseSource(nameof(GetMissingSample))]
        [Scenario]
        public void Median_fails_when_no_sample_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
              Then => median_fails_when_sample_is_not_found());
        }

        [Label("Volatility works as intended with valid samples")]
        [TestCaseSource(nameof(GetValidSamples))]
        [Scenario]
        public void Volatility_works_correctly_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
            Then => volatility_works_as_intended());
        }

        [Label("Volatility fails when no matching sample is found in cache")]
        [TestCaseSource(nameof(GetMissingSample))]
        [Scenario]
        public void Volatility_fails_when_no_sample_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
            Then => volatility_works_as_intended());
        }

        [Label("Skewness works as intended with valid samples")]
        [TestCaseSource(nameof(GetValidSamples))]
        [Scenario]
        public void Skewness_works_correctly_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
            Then => skewness_works_as_intended());
        }

        [Label("Skewness fails when no matching sample is found in cache")]
        [TestCaseSource(nameof(GetMissingSample))]
        [Scenario]
        public void Skewness_fails_when_no_sample_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
              Then => skewness_fails_when_sample_is_not_found());
        }

        [Label("Kurtosis works as intended with valid samples")]
        [TestCaseSource(nameof(GetValidSamples))]
        [Scenario]
        public void Kurtosis_works_correctly_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
            Then => kurtosis_works_as_intended());
        }

        [Label("Kurtosis fails when no matching sample is found in cache")]
        [TestCaseSource(nameof(GetMissingSample))]
        [Scenario]
        public void Kurtosis_fails_when_no_sample_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
            Then => kurtosis_fails_when_sample_is_not_found());
        }

        [Label("Min works as intended with valid samples")]
        [TestCaseSource(nameof(GetValidSamples))]
        [Scenario]
        public void Min_works_correctly_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
            Then => min_works_as_intended());
        }

        [Label("Min fails when no matching sample is found in cache")]
        [TestCaseSource(nameof(GetMissingSample))]
        [Scenario]
        public void Min_fails_when_no_sample_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
              Then => mean_fails_when_sample_is_not_found());
        }

        [Label("Max works as intended with valid samples")]
        [TestCaseSource(nameof(GetValidSamples))]
        [Scenario]
        public void Max_works_correctly_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
            Then => max_works_as_intended());
        }

        [Label("Max fails when no matching sample is found in cache")]
        [TestCaseSource(nameof(GetMissingSample))]
        [Scenario]
        public void Max_fails_when_no_sample_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
              Then => mean_fails_when_sample_is_not_found());
        }

        [Label("SpreadVAR works as intended with valid samples")]
        [TestCaseSource(nameof(GetValidSamples))]
        [Scenario]
        public void SpreadVAR_works_correctly_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
            Then => SpreadVAR_works_as_intended());
        }

        [Label("SpreadVAR fails when no matching sample is found in cache")]
        [TestCaseSource(nameof(GetMissingSample))]
        [Scenario]
        public void SpreadVAR_fails_when_no_sample_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
            Then => SpreadVAR_fails_when_sample_is_not_found());
        }

        [Label("Percentile works as intended with valid samples")]
        [TestCaseSource(nameof(GetValidPercentileSamples))]
        [Scenario]
        public void Percentile_works_correctly_test(double[] data, object percentileRank)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
            Then => percentile_works_as_intended(percentileRank));
        }

        [Label("Percentile fails when no matching sample is found in cache")]
        [TestCaseSource(nameof(GetMissingSample))]
        [Scenario]
        public void Percentile_fails_when_no_sample_test(double[] data)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
            Then => percentile_fails_when_sample_is_not_found());
        }

        [Label("Percentile fails when invlid parameters passed in")]
        [TestCaseSource(nameof(GetInvalidPercentileSamples))]
        [Scenario]
        public void Percentile_fails_when_invalid_arguments_test(double[] data, object percentileRank, Type type)
        {
            Runner.RunScenario(
            Given => a_valid_sample(data),
            Then => percentile_fails_when_invalid_arguments_are_passed_in(percentileRank, type));
        }
    }
}