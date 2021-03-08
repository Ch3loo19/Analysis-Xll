using LightBDD.NUnit3;
using Shouldly;
using System;
using System.Collections.Generic;
using static Functions_Logic.Utilities.Extensions;

namespace Functions_Logic.Tests.Utilities.Tests
{
    public partial class ExtensionsTests : FeatureFixture
    {
        //+ TestMethods

        private void Given_INPUT_I_get_RESULT(double[][] input, double[][] result)
        {
            var invertedInput = input.InvertEnumerableOrder();
            int outer = invertedInput.Length;
            int inner = invertedInput[0].Length;

            for (int i = 0; i < outer; i++)
            {
                for (int j = 0; j < inner; j++)
                {
                    invertedInput[i][j].ShouldBe(result[i][j]);
                }
            }

        }

        private void Given_INPUT_I_get_RESULT(object[,] input, object[] expectation)
        {
            var result = input.Convert2DParameterTo1DArray("Data");
            for (int i = 0; i < result.Length; i++)
            {
                Convert.ToDouble(result[i]).ShouldBe(Convert.ToDouble(expectation[i]), 1E-06);
            }
        }

        private void Given_2D_array_with_multiple_columns_conversion_fails(object[,] input, Type exceptionType)
        {
            Should.Throw(() => input.Convert2DParameterTo1DArray("Data"), exceptionType);
        }

        //+ TestData

        private static IEnumerable<object> GetValid2DData()
        {
            var input = new object[,]
            {
                 {1}, {2}, {3d }
            };

            var input2 = new object[,]
            {
                 {1, 2, 3d }
            };

            var result = new object[] { 1d, 2, 3 };



            yield return new object[] { input, result };
            yield return new object[] { input2, result };

        }
        private static IEnumerable<object> GetInvalid2DData()
        {
            var invalidInput = new object[,]
           {
                 {1d, 4},
                 {2, 5},
                 {3, 6},
           };

            yield return new object[] { invalidInput, typeof(ArgumentException) };
        }


        private static IEnumerable<object> GetValueArrayNestedData()
        {
            var input1 = new double[][]
            {
                new double[] {1, 4},
                new double[] {2, 5},
                new double[] {3, 6},
            };
            var result1 = new double[][]
            {
                new double[] {1, 2, 3},
                new double[] {4, 5, 6}
            };

            var input2 = new double[][]
            {
                new double[] {1},
                new double[] {2},
                new double[] {3 },
            };
            var result2 = new double[][]
            {
                new double[] {1, 2, 3}
            };

            var input3 = new double[][]
            {
                new double[] {1,4,7},
                new double[] {2,5,8},
                new double[] {3,6,9}
            };
            var result3 = new double[][]
            {
                new double[] {1, 2, 3},
                new double[] {4, 5, 6},
                new double[] {7, 8, 9}
            };

            return new object[][]
            {
                new object[] {input1, result1},
                new object[] {input2, result2},
                new object[] {input3, result3},
            };
        }
    }
}