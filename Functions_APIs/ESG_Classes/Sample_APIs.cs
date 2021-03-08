using ExcelDna.Integration;
using Functions_Logic.ESG_Classes;
using Functions_Logic.Logic;
using static Analysis_Xll.Utilities.Utilities;

namespace Analysis_Xll.ESG_Classes
{
    public static class Sample_APIs
    {
        //+ Constants
        private const string FunctionCategory = "Sample";

        private class SampleFunctionAttribute : ExcelFunctionAttribute
        {
            internal SampleFunctionAttribute()
            {
                Category = FunctionCategory;
            }
        }

        //+ Functions

        /// <summary>
        /// Calculates the mean of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Sample.Mean",
            Description = "Calculates the mean of a sample")]
        public static object Mean([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return Run(() => SampleLogic.Mean(hash));
        }

        /// <summary>
        /// Calculates the median of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Sample.Median",
            Description = "Calculates the median of a sample")]
        public static object Median([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return Run(() => SampleLogic.Median(hash));
        }

        /// <summary>
        /// Calculates the volatility of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Sample.Volatility",
            Description = "Calculates the volatility of a sample")]
        public static object Volatility([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return Run(() => SampleLogic.Volatility(hash));
        }

        /// <summary>
        /// Calculates the skewness of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Sample.Skewness",
            Description = "Calculates the skewness of a sample")]
        public static object Skewness([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return Run(() => SampleLogic.Skewness(hash));
        }

        /// <summary>
        /// Calculates the kurtosis of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Sample.Kurtosis",
            Description = "Calculates the kurtosis of a sample")]
        public static object Kurtosis([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return Run(() => SampleLogic.Kurtosis(hash));
        }

        /// <summary>
        /// Calculates the min of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Sample.Min",
            Description = "Calculates the min of a sample")]
        public static object Min([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return Run(() => SampleLogic.Min(hash));
        }

        /// <summary>
        /// Calculates the max of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Sample.Max",
            Description = "Calculates the max of a sample")]
        public static object Max([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return Run(() => SampleLogic.Max(hash));
        }

        /// <summary>
        /// Calculates the Spread VAR of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Sample.SpreadVAR",
            Description = "Calculates the SpreadVAR of a sample, as an average among VARs from 99.4% to 99.6% confidence levels (inclusive), going in 0.01% steps")]
        public static object SpreadVAR([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return Run(() => SampleLogic.SpreadVAR(hash));
        }

        /// <summary>
        /// Calculates the percentile of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        /// <param name="percentileRank">The perecentile rank/param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Sample.Percentile",
            Description = "Calculates the percentile of a sample")]
        public static object Percentile([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash,
                                        [ExcelArgument(Description = "The percentile rank. Between 0 and 1, inclussive.", Name = "Convidence Level")] object percentileRank)
        {
            return Run(() => SampleLogic.Percentile(hash, percentileRank));
        }


        //+ Async Fnctions

        /// <summary>
        /// Calculates the mean of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Async.Sample.Mean",
            Description = "Calculates the mean of a sample",
            IsHidden = true)]
        public static object MeanAsync([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return RunAsync(() => SampleLogic.Mean(hash), "Lloyds.Sample.Mean");
        }

        /// <summary>
        /// Calculates the median of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Async.Sample.Median",
            Description = "Calculates the median of a sample",
            IsHidden = true)]
        public static object MedianAsync([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return RunAsync(() => SampleLogic.Median(hash), "Lloyds.Sample.Median");
        }

        /// <summary>
        /// Calculates the volatility of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Async.Sample.Volatility",
            Description = "Calculates the volatility of a sample",
            IsHidden = true)]
        public static object VolatilityAsync([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return RunAsync(() => SampleLogic.Volatility(hash), "Lloyds.Async.Sample.Volatility");
        }

        /// <summary>
        /// Calculates the skewness of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Async.Sample.Skewness",
            Description = "Calculates the skewness of a sample",
            IsHidden = true)]
        public static object SkewnessAsync([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return RunAsync(() => SampleLogic.Skewness(hash), "Lloyds.Async.Sample.Skewness");
        }

        /// <summary>
        /// Calculates the kurtosis of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Async.Sample.Kurtosis",
            Description = "Calculates the kurtosis of a sample",
            IsHidden = true)]
        public static object KurtosisAsync([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return RunAsync(() => SampleLogic.Kurtosis(hash), "Lloyds.Async.Sample.Kurtosis");
        }

        /// <summary>
        /// Calculates the min of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Async.Sample.Min",
            Description = "Calculates the min of a sample",
            IsHidden = true)]
        public static object MinAsync([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return RunAsync(() => SampleLogic.Min(hash), "Lloyds.Async.Sample.Min");
        }

        /// <summary>
        /// Calculates the max of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Async.Sample.Max",
            Description = "Calculates the max of a sample",
            IsHidden = true)]
        public static object MaxAsync([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return RunAsync(() => SampleLogic.Max(hash), "Lloyds.Async.Sample.Max");
        }

        /// <summary>
        /// Calculates the percentile of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        /// <param name="percentileRank">The perecentile rank/param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Async.Sample.Percentile",
            Description = "Calculates the percentile of a sample",
            IsHidden = true)]
        public static object PercentileAsync([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash,
                                        [ExcelArgument(Description = "The percentile rank. Between 0 and 1, inclussive.", Name = "Convidence Level")] object percentileRank)
        {
            return RunAsync(() => SampleLogic.Percentile(hash, percentileRank), "Lloyds.Async.Sample.Percentile");
        }

        /// <summary>
        /// Calculates the Spread VAR of a <see cref="Sample"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="Sample"/> object</param>
        [SampleFunctionAttribute(
            Name = "Lloyds.Async.Sample.SpreadVAR",
            Description = "Calculates the SpreadVAR of a sample, as an average among VARs from 99.4% to 99.6% confidence levels (inclusive), going in 0.01% steps",
            IsHidden = true)]
        public static object SpreadVARAsync([ExcelArgument(Description = "The hash of a valid Sample object", Name = "Sample")] string hash)
        {
            return RunAsync(() => SampleLogic.SpreadVAR(hash), "Lloyds.Async.Sample.SpreadVAR");
        }
    }
}
