using System;
using System.Collections.Generic;
using System.Linq;

namespace Functions_Logic.Utilities
{
    internal static class Extensions
    {
        internal static double[][] InvertEnumerableOrder(this IEnumerable<IEnumerable<double>> nestedEnumerable)
        {
            int outerEnumerableCount = nestedEnumerable.Count();
            int innerEnumerableCount = nestedEnumerable.First().Count();
            var invertedEnumerable = new double[innerEnumerableCount][];

            // Initialise
            invertedEnumerable = invertedEnumerable.Select(sim => sim = new double[outerEnumerableCount]).ToArray();

            // Switch Order
            for (int j = 0; j < outerEnumerableCount; j++)
            {
                for (int i = 0; i < innerEnumerableCount; i++)
                {
                    invertedEnumerable[i][j] = nestedEnumerable.ElementAt(j).ElementAt(i);
                }
            }

            return invertedEnumerable;
        }

        internal static T ParseAsEnum<T>(this string enumString) where T : Enum
        {
            if (string.IsNullOrWhiteSpace(enumString))
            {
                return (T)Enum.ToObject(typeof(T), 0);
            };

            foreach (var enumName in Enum.GetNames(typeof(T)))
            {
                if (string.Equals(enumName, enumString, StringComparison.InvariantCultureIgnoreCase))
                {
                    return (T)Enum.Parse(typeof(T), enumString);
                };
            }

            throw new InvalidEnumParseException<T>(enumString);
        }

        internal static int ConvertParameterToInt(this object attemptedValue, string parameterName)
        {
            int val;

            try
            {
                val = Convert.ToInt32(attemptedValue);

                //trap doubles passed in
                float valf = Convert.ToSingle(attemptedValue);
                if (!(val - valf).Equals(0f))
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                throw new GenericArgumentParseException(attemptedValue, parameterName);
            }

            return val;
        }

        internal static int ConvertOptionalParameterToInt(this object attemptedValue, string parameterName, int defaultValue)
        {
            if (string.IsNullOrWhiteSpace(attemptedValue.ToString()))
            {
                return defaultValue;
            }
            return ConvertParameterToInt(attemptedValue, parameterName);
        }

        internal static double ConvertParameterToDouble(this object attemptedValue, string parameterName)
        {
            double val;

            try
            {
                val = Convert.ToDouble(attemptedValue);
            }
            catch (Exception)
            {
                throw new GenericArgumentParseException(attemptedValue, parameterName);
            }

            return val;
        }

        internal static double ConvertOptionalParameterToDouble(this object attemptedValue, string parameterName, int defaultValue)
        {
            if (string.IsNullOrWhiteSpace(attemptedValue.ToString()))
            {
                return defaultValue;
            }
            return ConvertParameterToInt(attemptedValue, parameterName);
        }

        internal static object[] Convert2DParameterTo1DArray(this object[,] rawParameter, string parameterName)
        {
            int maxCols = rawParameter.GetLength(1);
            int maxRows = rawParameter.GetLength(0);

            if (maxCols > 1 && maxRows > 1)
            {
                throw new ArgumentException($"Dimensions of parameter {parameterName} are {maxCols} columns by {maxRows} rows. One dimension needs to be 1.");
            }

            object[] result;

            if (maxRows > 1)
            {
                result = new object[maxRows];

                for (int i = 0; i < maxRows; i++)
                {
                    result[i] = rawParameter[i, 0];
                }
            }
            else
            {
                result = new object[maxCols];

                for (int i = 0; i < maxCols; i++)
                {
                    result[i] = rawParameter[0, i];
                }
            }

            return result;

        }

        internal static bool EqualsWithPrecision(this double a, double b, int decimals = 10)
        {
            double aRounded = Math.Round(a, decimals);
            double bRounded = Math.Round(b, decimals);
            return aRounded.Equals(bRounded);
        }



    }
}
