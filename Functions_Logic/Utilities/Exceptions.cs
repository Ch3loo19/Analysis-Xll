using System;
using System.Linq;

namespace Functions_Logic.Utilities
{
    public class MissingStochArrayException : ArgumentException
    {
        public MissingStochArrayException(string hash) : base(GetMessage(hash)) { }

        private static string GetMessage(string hash)
        {
            return $"Stoch Array requested ({hash}) is missing from the cache";
        }
    }

    public class MissingFxArrayException : ArgumentException
    {
        public MissingFxArrayException(string hash) : base(GetMessage(hash)) { }

        private static string GetMessage(string hash)
        {
            return $"Fx Array requested ({hash}) is missing from the cache";
        }
    }

    public class MissingSampleException : ArgumentException
    {
        public MissingSampleException(string hash) : base(GetMessage(hash)) { }

        private static string GetMessage(string hash)
        {
            return $"Samplerequested ({hash}) is missing from the cache";
        }
    }


    public class InvalidEnumParseException<T> : InvalidCastException where T : Enum
    {
        internal readonly string _description;
        public InvalidEnumParseException(string attemptedValue) : base()
        {
            var enumValues = Enum.GetNames(typeof(T)).Select(x => x.ToUpper()); ;
            string possibleValues = string.Join(",", enumValues);
            _description = $"Attempted parameter value {attemptedValue.ToUpper()} is not in the list of valid options: {possibleValues}";
        }

        public new string Message => _description;
    }

    public class StochArrayHorizonException : ArgumentException
    {
        public StochArrayHorizonException(int attemptedYear, int maxAvailableHorizon) : base(GetMessage(attemptedYear, maxAvailableHorizon))
        {

        }

        private static string GetMessage(int attemptedYear, int maxAvailableHorizon)
        {
            return $"The horizon requested, {attemptedYear}, is outside the available horizon of 1 to {maxAvailableHorizon}";
        }
    }

    public class StochArrayDimensionsMismatchException : ArgumentException
    {
        public StochArrayDimensionsMismatchException(string dimension, int maxValue1, int maxValue2) : base(GetMessage(dimension, maxValue1, maxValue2))
        {

        }

        private static string GetMessage(string dimension, int maxValue1, int maxValue2)
        {
            return $"The max {dimension} does not agree among all the Stoch Array arguments passed in, e.g. {maxValue1} is different from {maxValue2}";
        }
    }

    public class GenericArgumentParseException : ArgumentException
    {
        public GenericArgumentParseException(object attemptedValue, string parameterName) : base(GetMessage(attemptedValue, parameterName))
        {

        }

        private static string GetMessage(object attemptedValue, string parameterName)
        {
            return $"The value {attemptedValue} cannot be converted to a suitable argument for parameter: {parameterName}";
        }
    }

    public class SampleShapeException : ArgumentException
    {
        public SampleShapeException(int dim1, int dim2) : base(GetMessage(dim1, dim2))
        {

        }

        private static string GetMessage(int dim1, int dim2)
        {
            return $"Samples can be only one-dimensional. Your input is {dim1} x {dim2}";
        }
    }
}
