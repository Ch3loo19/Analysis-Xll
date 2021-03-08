using Functions_Logic.ESG_Classes;
using Functions_Logic.Utilities;
using LightBDD.NUnit3;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace Functions_Logic_Tests.ESG_Classes.Tests
{
    public partial class FxArrayTests : FeatureFixture
    {
    
        //+ Fields
        private FxArray _fxArray;
        private StochArray _stochArray;

        //+ TestMethods
        private void a_valid_fxArray(double[][] rates, double currentFx)
        {
            _fxArray = new FxArray(rates, currentFx);
        }

        private void a_valid_stochArray(double[][] rates)
        {
            _stochArray = new StochArray(rates);
        }      

        private void currency_returns_equal_EXPECTATION(Accumulation accumulation, double[][] expectation)
        {
            var currencyReturns = _fxArray.GetCurrencyReturns(accumulation);

            int maxSim = currencyReturns.MaxSim;
            int maxHor = currencyReturns.MaxHorizon;

            for (int i = 0; i < maxSim; i++)
            {
                for (int j = 0; j < maxHor; j++)
                {
                    currencyReturns.Values[i][j].ShouldBe(expectation[i][j], 1E-5);
                }
            }
        }

        private void calculated_local_stocharray_returns_equal_EXPECTATION(double[][] expectation)
        {
            var localStochArray = _fxArray.GetLocalStochArrayReturns(_stochArray);

            int maxSim = localStochArray.MaxSim;
            int maxHor = localStochArray.MaxHorizon;

            for (int i = 0; i < maxSim; i++)
            {
                for (int j = 0; j < maxHor; j++)
                {
                    localStochArray.Values[i][j].ShouldBe(expectation[i][j], 1E-8);
                }
            }
        }

        private void calculated_GBP_stoch_array_returns_equals_EXPECTATION(double[][] expectation)
        {
            var gbpStochArray = _fxArray.GetGBPStochArrayReturns(_stochArray);

            int maxSim = gbpStochArray.MaxSim;
            int maxHor = gbpStochArray.MaxHorizon;

            for (int i = 0; i < maxSim; i++)
            {
                for (int j = 0; j < maxHor; j++)
                {
                    gbpStochArray.Values[i][j].ShouldBe(expectation[i][j], 1e-8);
                }
            }
        }



        //+ TestData
        private static double[][] ValidFxData1 => new[]
        {
            new [] {1.23,    1.45,    1.67  },
            new [] {1.34,    1.56,    1.89   },
            new [] {1.13,    1.23,    1.57  },

        };

        private static double[][] ValidFxData2 => new[]
        {
            new [] {1.612850889, 2.04845728 , 1.809715783},
            new [] {1.444805871, 2.38124546 , 1.588401427 },
            new [] {1.593221941, 1.350171462, 2.300146997 }
         };

        private static double[][] ValidStochArrayData1 => new[]
       {
         new double[] {  0.049917476,  0.099433325, 0.036153579 },
         new double[] {  0.048120486, 0.085800746 ,0.063077133  },
         new double[] {  0.042927316, 0.039996537 ,0.087732121 }
        };

        private static double[][] ValidStochArrayData2 => new[]
        {
          new double [] { -0.064460203, -0.081079879,    -0.012243257 },
          new double [] {  0.009355503, -0.103412657,    -0.010337127 },
          new double [] {  0.040535892, 0.035822441 , 0.018070545 }
         };

        private static double[][] ExpectedCurrenyReturns1 => new[]
        {
           new [] { -0.10569 ,   -0.12901 ,   -0.12992   },
           new [] {-0.17910,     -0.16028,    -0.16508},
           new [] { -0.02655 ,   -0.05432 ,   -0.11183 }
        };

        private static double[][] ExpectedCurrenyReturns2 => new[]
       {
         new double[]  {  0.115412522,  -0.121781013,    -0.005924525    },
         new double[] {  0.245145879, -0.244515483 ,   0.132581504      },
         new double[] {  0.129154721, 0.332418976  , -0.217878649 }
        };

        private static double[][] ExpectedLocalReturns1 => new[]
        {
        new double [] { 0.173998632 ,   0.262282483, 0.190872848  },
        new double [] { 0.276801319 ,   0.293052304, 0.273271885  },
        new double [] { 0.071370788 ,   0.09973515 , 0.224684655 }

        };

        private static double[][] ExpectedLocalReturns2 => new[]
        {
           new double [] { -0.161261167 ,   -0.019435085,    -0.010284854      },
           new double [] { -0.189367672 ,   0.031525146 ,  -0.050567436        },
           new double [] { -0.07848245  ,  -0.102643711 ,   0.104976797 }

        };

        private static double[][] ExpectedGBPReturns1 => new[]
        {
           new double [] {  -0.061049412 ,   -0.042406392 ,   -0.098464424  },
           new double [] {  -0.139602586 ,   -0.08823235  ,   -0.112418169  },
           new double [] {  0.01523898   ,   -0.016497021 ,   -0.033905452 }
        };

        private static double[][] ExpectedGBPReturns2 => new[]
        {
         new double [] { 0.043512804 ,  -0.138849274,    -0.014197785 },
         new double [] { 0.256794845 ,-0.220698723  ,   0.031597862   },
         new double [] { 0.174926015 ,0.195654548   ,  -0.062000544 }  


        };


        private static IEnumerable<object> GetCurrencyReturnsValidData()
        {
            yield return new object[] { ValidFxData1, 1.1, Accumulation.Annualised, ExpectedCurrenyReturns1 };
            yield return new object[] { ValidFxData2, 1.798994077, Accumulation.Cumulative, ExpectedCurrenyReturns2 };
        }

        private static IEnumerable<object> GetValidGetLocalStochArrayReturnsTestData()
        {
            yield return new object[] { ValidFxData1,1.1,ValidStochArrayData1, ExpectedLocalReturns1 };
            yield return new object[] { ValidFxData2, 1.798994077, ValidStochArrayData2, ExpectedLocalReturns2 };
        }

        private static IEnumerable<object> GetValidGetGBPStochArrayReturnsTestData()
        {
            yield return new object[] { ValidFxData1,1.1, ValidStochArrayData1, ExpectedGBPReturns1 };
            yield return new object[] { ValidFxData2, 1.798994077, ValidStochArrayData2, ExpectedGBPReturns2 };
        }


        //+ TestFlow
        [SetUp]
        public void TestInitialiser()
        {
            _fxArray = null;
            _stochArray = null;

        }

        [TearDown]
        public void TestCompletion()
        {
            _fxArray = null;
            _stochArray = null;

        }
    }
}