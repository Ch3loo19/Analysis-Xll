using Functions_Logic.ESG_Classes;
using LightBDD.NUnit3;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace Functions_Logic_Tests.ESG_Classes.Tests
{
    public partial class SampleTests : FeatureFixture
    {
        //+ Fields
        Sample _sample;

        //+ Test Methods
        private void a_valid_sample(double[] values)
        {
            _sample = new Sample(values);
        }

        private void calculated_mean_equals_EXPECTATION(double expectation)
        {
            _sample.Mean().ShouldBe(expectation, 1E-06);
        }

        private void calculated_median_equals_EXPECTATION(double expectation)
        {
            _sample.Median().ShouldBe(expectation, 1E-06);
        }

        private void calculated_volatility_equals_EXPECTATION(double expectation)
        {
            _sample.Volatility().ShouldBe(expectation, 1E-06);
        }
        private void calculated_skewness_equals_EXPECTATION(double expectation)
        {
            _sample.Skewness().ShouldBe(expectation, 1E-06);
        }
        private void calculated_kurtosis_equals_EXPECTATION(double expectation)
        {
            _sample.Kurtosis().ShouldBe(expectation, 1E-06);
        }
        private void calculated_min_equals_EXPECTATION(double expectation)
        {
            _sample.Min().ShouldBe(expectation, 1E-06);
        }
        private void calculated_max_equals_EXPECTATION(double expectation)
        {
            _sample.Max().ShouldBe(expectation, 1E-06);
        }
        private void calculated_SpreadVAR_equals_EXPECTATION(double expectation)
        {
            _sample.SpreadVAR().ShouldBe(expectation, 1E-06);
        }

        private void calculated_percentile_equals_EXPECTATION(double percentileRank, double expectation)
        {
            _sample.Percentile(percentileRank).ShouldBe(expectation, 1E-06);
        }



        //+ TestData
        private static double[] ValidData1 => new[] { 0.010, 0.009, -0.025, 0.020, -0.005 };

        private static double[] ValidData2 => new[] { -0.025, 0.028, -0.056, 0.002, 0.1 };

        private static double[] ValidData3 => new[] { 0.002986, 0.003877, 0.005608, 0.000677, 0.00852 };

        private static double[] ValidVARData1 => Enumerable.Range(1, 10000).Select(val => (double)val).ToArray();
        private static double[] ValidVARData2
        {
            get
            {
                int count = ValidVARData1.Length;
                var result = new double[count];
                for (int i = 0; i < count; i++)
                {
                    if (ValidVARData1[i] % 1000 == 0)
                    {
                        result[i] = 1d;
                    }
                    else
                    {
                        result[i] = ValidVARData1[i];
                    }
                }
                return result;
            }

        }

        private static IEnumerable<object> GetValidMeanTestData()
        {
            yield return new object[] { ValidData1, 0.00180 };
            yield return new object[] { ValidData2, 0.00980 };
            yield return new object[] { ValidData3, 0.004334 };
        }

        private static IEnumerable<object> GetValidMedianTestData()
        {
            yield return new object[] { ValidData1, 0.00900 };
            yield return new object[] { ValidData2, 0.00200 };
            yield return new object[] { ValidData3, 0.003877 };
        }

        private static IEnumerable<object> GetValidVolatilityTestData()
        {
            yield return new object[] { ValidData1, 0.017427 };
            yield return new object[] { ValidData2, 0.059306 };
            yield return new object[] { ValidData3, 0.002939 };
        }

        private static IEnumerable<object> GetValidSkewnessTestData()
        {
            yield return new object[] { ValidData1, -0.992749 };
            yield return new object[] { ValidData2, 0.823762 };
            yield return new object[] { ValidData3, 0.394117 };
        }

        private static IEnumerable<object> GetValidKurtosisTestData()
        {
            yield return new object[] { ValidData1, 0.604982 };
            yield return new object[] { ValidData2, 0.742533 };
            yield return new object[] { ValidData3, 0.244076 };
        }

        private static IEnumerable<object> GetValidMinTestData()
        {
            yield return new object[] { ValidData1, -0.025000 };
            yield return new object[] { ValidData2, -0.056000 };
            yield return new object[] { ValidData3, 0.000677 };
        }

        private static IEnumerable<object> GetValidMaxTestData()
        {
            yield return new object[] { ValidData1, 0.020000 };
            yield return new object[] { ValidData2, 0.100000 };
            yield return new object[] { ValidData3, 0.008520 };
        }

        private static IEnumerable<object> GetValidSpreadVARTestData()
        {
            yield return new object[] { ValidVARData1, 50.995 };
            yield return new object[] { ValidVARData2, 40.995 };
        }

        private static IEnumerable<object> GetValidPercentileTestData()
        {
            yield return new object[] { ValidVARData1, 0.004, 40.996 };
            yield return new object[] { ValidVARData1, 0.005, 50.995 };
            yield return new object[] { ValidVARData1, 0.0053, 53.9947 };
            yield return new object[] { ValidVARData1, 0.006, 60.994 };
            yield return new object[] { ValidVARData2, 0.004, 30.996 };
            yield return new object[] { ValidVARData2, 0.005, 40.995 };
            yield return new object[] { ValidVARData2, 0.0053, 43.9947 };
            yield return new object[] { ValidVARData2, 0.006, 50.994 };
        }

        //+ TestFlow
        [SetUp]
        public void TestInitialiser()
        {
            _sample = null;
        }

        [TearDown]
        public void TestCompletion()
        {
            _sample = null;
        }
    }
}