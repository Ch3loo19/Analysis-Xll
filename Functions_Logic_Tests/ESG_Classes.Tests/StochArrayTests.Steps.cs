using Functions_Logic.ESG_Classes;
using Functions_Logic.Utilities;
using LightBDD.NUnit3;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace Functions_Logic_Tests.ESG_Classes.Tests
{
    public partial class StochArrayTests : FeatureFixture
    {
        //+ Fields
        private StochArray _stochArray;
        private StochArray[] _stochArrays;

        //+ TestMethods
        private void a_valid_stochArray(double[][] values)
        {
            _stochArray = new StochArray(values);
        }

        private void valid_stochArrays(double[][][] stochArrays)
        {
            _stochArrays = stochArrays.Select(sa => new StochArray(sa)).ToArray();
        }

        private void calculated_mean_equals_EXPECTATION(double expectation)
        {
            _stochArray.Mean().ShouldBe(expectation, 1E-06);
        }

        private void calculated_median_equals_EXPECTATION(double expectation)
        {
            _stochArray.Median().ShouldBe(expectation, 1E-06);
        }

        private void calculated_volatility_equals_EXPECTATION(double expectation)
        {
            _stochArray.Volatility().ShouldBe(expectation, 1E-06);
        }

        private void calculated_accumulation_is_correct(int year, Accumulation accum, double[] expectation)
        {
            _stochArray.Accumulate(year, accum).Values.ShouldBe(expectation, 1E-06);
        }

        private void get_year_as_sample_is_correct(int year, double[] expectation)
        {
            _stochArray.GetYearAsSample(year).Values.ShouldBe(expectation, 1E-06);
        }

        private void combine_is_calculated_correctly(double [] proportions,Rebalancing rebalancingPolicy, double[][] expectation)
        {
            var combined = StochArray.Combine(_stochArrays, proportions, rebalancingPolicy);
            int maxSim = combined.MaxSim;
            int maxHor = combined.MaxHorizon;

            for (int i = 0; i < maxSim; i++)
            {
                for (int j = 0; j < maxHor; j++)
                {
                    combined.Values[i][j].ShouldBe(expectation[i][j], 1E-06, $"Error at sim {i} and horizon {j}");
                }
            }
        }

        //+ TestData
        private static double[][] ValidData1 => new[]
        {
            new [] {0.010,   0.009,   -0.025,  0.020,   -0.005 },
            new [] {0.084,   0.046,   0.034,   0.043,   -0.003 },
            new [] {0.057,   0.084,   0.013,   0.093,   0.089 },
            new [] {0.054,   0.051,   0.015,   0.070,   0.007 },
            new [] {0.066,   0.013,   0.035,   0.070,   0.026 },
            new [] {0.047,   0.028 ,  -0.002 , 0.035,   0.065 }
        };

        private static double[][] ValidData2 => new[]
        {
            new [] {-0.025,  0.028,   -0.056,  0.002,   0.1},
            new [] {-0.017,  0.074,   0.09,    0.002 ,  -0.001},
            new [] { 0.009,  0.054,   0.074,   0.0187,  0.197},
            new [] { 0.078,  0.003,   -0.04,   -0.05,   0.03},
            new [] { -0.008, 0.008,   -0.1,    -0.005,  0.003},
            new [] {0.0308,  0.0444,  -0.2016, 0.0247,  0.0711}
        };

        private static double[][] ValidData3 => new[]
        {
           new [] { 0.002986,    0.003877,    0.005608,    0.000677,    0.00852 },
           new [] { 0.009611,    0.00202 ,    0.006881,    0.000361,    0.009675 },
           new [] { 0.003643,    0.004102,    0.001873,    0.006108,    0.004873},
           new [] { 0.007367,    0.009458,    0.002931,    0.007873,    0.0028},
           new [] { 0.008108,    0.009856,    0.006735,    0.002608,    0.00456},
           new [] { 0.005564,    0.002406,    0.009445,    0.001076,    0.00711}

        };

        private static IEnumerable<object> GetValidMeanTestData()
        {
            yield return new object[] { ValidData1, 0.0401 };
            yield return new object[] { ValidData2, 0.007 };
            yield return new object[] { ValidData3, 0.005415 };
        }

        private static IEnumerable<object> GetValidMedianTestData()
        {
            yield return new object[] { ValidData1, 0.039};
            yield return new object[] { ValidData2, 0.0025 };
            yield return new object[] { ValidData3, 0.00615 };
        }

        private static IEnumerable<object> GetValidVolatilityTestData()
        {
            yield return new object[] { ValidData1, 0.023296 };
            yield return new object[] { ValidData2, 0.049980 };
            yield return new object[] { ValidData3, 0.002674 };
        }
      
        private static IEnumerable<object> GetValidGetYearAsSampleTestData()
        {
            yield return new object[] { ValidData1, 5, new double[] { -0.005, -0.003, 0.089 ,0.007, 0.026, 0.065 } };
            yield return new object[] { ValidData2, 2, new double[] { 0.028, 0.074, 0.054, 0.003, 0.008, 0.0444 } };
            yield return new object[] { ValidData2, 1, new double[] { -0.025, -0.017, 0.009, 0.078, -0.008, 0.0308 } };
            yield return new object[] { ValidData3, 5, new double[] { 0.00852, 0.009675, 0.004873, 0.0028, 0.00456, 0.00711 } };
        }

        private static IEnumerable<object> GetValidAccumulationTestData()
        {
            yield return new object[] { ValidData1, ValidData1.First().Length, Accumulation.Cumulative, new double[] { 0.008418, 0.219161, 0.381535, 0.211498, 0.226982, 0.184023 } };
            yield return new object[] { ValidData2, ValidData2.First().Length, Accumulation.Cumulative, new double[] { 0.042870, 0.151907, 0.392761, 0.015668, -0.101871, -0.056616 } };
            yield return new object[] { ValidData3, ValidData3.First().Length, Accumulation.Cumulative, new double[] { 0.021839, 0.028838, 0.020764, 0.030783, 0.032259, 0.025841 } };
            yield return new object[] { ValidData1, ValidData1.First().Length, Accumulation.Annualised, new double[] { 0.001678, 0.040428, 0.066774, 0.039117, 0.041760, 0.034361  } };
            yield return new object[] { ValidData2, ValidData2.First().Length, Accumulation.Annualised, new double[] { 0.008431, 0.028688, 0.068502, 0.003114, -0.021259, -0.011589 } };
            yield return new object[] { ValidData3, ValidData3.First().Length, Accumulation.Annualised, new double[] { 0.004330, 0.005702, 0.004119, 0.006082, 0.006370, 0.005116 } };
            yield return new object[] { ValidData1, 3, Accumulation.Annualised, new double[] { -0.002134, 0.054453, 0.050924, 0.039848, 0.037773, 0.024134 } };
            yield return new object[] { ValidData2, 3, Accumulation.Annualised, new double[] { -0.018275, 0.047920, 0.045311, 0.012505, -0.034531, -0.049204 } };
            yield return new object[] { ValidData3, 3, Accumulation.Annualised, new double[] { 0.004156, 0.006166, 0.003206, 0.006582, 0.008232, 0.005801 } };
        }


        private static IEnumerable<object> GetValidCombineTestData()
        {
            var case1Expectation = new double[][]
            {
               new double [] { -0.018000,   0.024200,  -0.049800,    0.005600  ,   0.079000   },
               new double [] { 0.003200 ,   0.068400,   0.078800,    0.010200  ,   -0.001400  } ,
               new double [] { 0.018600 ,   0.060000,   0.061800,    0.033560  ,   0.175400   } ,
               new double [] { 0.073200 ,   0.012600,  -0.029000,    -0.026000 ,   0.025400   } ,
               new double [] { 0.006800 ,   0.009000,  -0.073000,    0.010000  ,   0.007600   } ,
               new double [] { 0.034040,    0.041120,  -0.161680,    0.026760,     0.069880 }
            };

            var case2Expectation = new double[][]
            {
              new double[] { 0.014180  ,  -0.005399 ,  0.030095 ,   0.000195  ,  -0.024861   },
              new double[] { 0.020255  ,  -0.025721 ,  -0.028431,   -0.000420 ,  0.014775    },
              new double[] { 0.001500  ,  -0.016007 ,  -0.029262,   0.000094  ,  -0.088591   },
              new double[] { -0.020886 ,  0.012302  ,  0.021664 ,   0.031602  ,  -0.007470   },
              new double[] { 0.014551  ,  0.010582  ,  0.048373 ,   0.005156  ,  0.005077    },
              new double[] { -0.004530 ,  -0.014988 ,  0.102129 ,   -0.006440 ,  -0.013886   }

            };

            var case3Expectation = new double[][]
            {
              new double [] {  -0.003093,   0.014143,    -0.028496,   0.009809,   0.032624 },
              new double [] {  0.034304 ,   0.045564,    0.046514 ,   0.020089,   0.000448 },
              new double [] {  0.029421 ,   0.056522,    0.030682 ,   0.049365,   0.106132 },
              new double [] {  0.051661 ,   0.026021,    -0.005805,   0.016732,   0.013666 },
              new double [] {  0.028844 ,   0.010658,    -0.015768,   0.030424,   0.013693 },
              new double [] {  0.032538 ,   0.027781,    -0.065350,   0.024138,   0.054277 }
            };

            var case4Expectation = new double[][]
            {
             new double [] { -0.003093,   0.013998 ,   -0.028436 ,  0.009895,   0.031159 },
             new double [] { 0.034304 ,   0.045337 ,   0.046463  ,  0.020794,   0.000119 },
             new double [] { 0.029421 ,   0.057159 ,   0.030405  ,  0.050813,   0.108237 },
             new double [] { 0.051661 ,   0.026009 ,   -0.005690 ,  0.018695,   0.012979 },
             new double [] { 0.028844 ,   0.010731 ,   -0.013926 ,  0.033459,   0.015013 },
             new double [] { 0.032538 ,   0.027919 ,   -0.066420 ,  0.024388,   0.053966 }

            };

            yield return new object[] { new[] {ValidData1, ValidData2 }, new [] { 0.2,0.8 }, Rebalancing.Rebalance, case1Expectation };
            yield return new object[] { new[] {ValidData2, ValidData3 }, new[] { -0.4, 1.4 }, Rebalancing.NoRebalancing, case2Expectation };
            yield return new object[] { new[] {ValidData1, ValidData2, ValidData3 }, new[] { 0.45, 0.33, 0.22 }, Rebalancing.Rebalance, case3Expectation };
            yield return new object[] { new[] {ValidData1, ValidData2, ValidData3 }, new[] { 0.45, 0.33, 0.22 }, Rebalancing.NoRebalancing, case4Expectation };
        }

        //+ TestFlow
        [SetUp]
        public void TestInitialiser()
        {
            _stochArray = null;
            _stochArrays = null;

        }

        [TearDown]
        public void TestCompletion()
        {
            _stochArray = null;
            _stochArrays = null;

        }
    }
}