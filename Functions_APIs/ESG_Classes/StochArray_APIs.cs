using ExcelDna.Integration;
using Functions_Logic.ESG_Classes;
using Functions_Logic.Logic;
using static Analysis_Xll.Utilities.Utilities;

namespace Analysis_Xll.ESG_Classes
{

    /// <summary>
    /// Todo: To add function hyperlinks when documentation ready
    /// </summary>
    public static class StochArray_APIs
    {
        //+ Constants
        private const string FunctionCategory = "StochArray";


        //+ Functions

        /// <summary>
        /// Calculates the mean of a <see cref="StochArray"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="StochArray"/> object</param>
        [ExcelFunction(
            Name = "Lloyds.StochArray.Mean",
            Description = "Calculates the mean of a stoch array",
            Category = FunctionCategory)]
        public static object Mean([ExcelArgument(Description = "The hash of a valid StochArray object", Name = "Stoch Array")] string hash)
        {
            return Run(() => StochArrayLogic.Mean(hash));
        }

        /// <summary>
        /// Calculates the median of a <see cref="StochArray"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="StochArray"/> object</param>
        [ExcelFunction(
            Name = "Lloyds.StochArray.Median",
            Description = "Calculates the median of a stoch array",
            Category = FunctionCategory)]
        public static object Median([ExcelArgument(Description = "The hash of a valid StochArray object", Name = "Stoch Array")] string hash)
        {
            return Run(() => StochArrayLogic.Median(hash));
        }

        /// <summary>
        /// Gets a <see cref="Sample"/> from a <see cref="StochArray"/> at a particular year
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="StochArray"/> object</param>
        [ExcelFunction(
            Name = "Lloyds.StochArray.GetYearAsSample",
            Description = "Gets a Sample from a Stocharray at a particular year",
            Category = FunctionCategory)]
        public static object GetYearAsSample(
            [ExcelArgument(Description = "The hash of a valid StochArray object", Name = "Stoch Array")] string hash,
            [ExcelArgument(Description = "The year. Must be an ineger > 0", Name = "Year")] double year)
        {
            return Run(() => StochArrayLogic.GetYearAsSample(hash, year));
        }

        /// <summary>
        /// Calculates the volatility of a <see cref="StochArray"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="StochArray"/> object</param>
        [ExcelFunction(
            Name = "Lloyds.StochArray.Volatility",
            Description = "Calculates the volatility of a stoch array",
            Category = FunctionCategory)]
        public static object Volatility([ExcelArgument(Description = "The hash of a valid StochArray object", Name = "Stoch Array")] string hash)
        {
            return Run(() => StochArrayLogic.Volatility(hash));
        }

        ///// <summary>
        ///// Gets the values of a <see cref="StochArray"/>
        ///// </summary>
        ///// <param name="hash">The hash of a valid <see cref="StochArray"/> object</param>
        //[ExcelFunction(
        //    Name = "Lloyds.StochArray.Values",
        //    Description = "Gets the values of a stoch array",
        //    Category = FunctionCategory)]
        //public static object Values([ExcelArgument(Description = "The hash of a valid StochArray object", Name = "Stoch Array")] string hash)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Gets the value of a <see cref="StochArray"/> at a particular year and for a particular sim
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="StochArray"/> object</param>
        /// <param name="year">The relevant year</param>
        /// <param name="sim">The relevant sim</param>
        [ExcelFunction(
            Name = "Lloyds.StochArray.Value",
            Description = "Gets the values of a stoch array at a particular year and for a particular sim",
            Category = FunctionCategory)]
        public static object Value(
            [ExcelArgument(Description = "The hash of a valid StochArray object", Name = "Stoch Array")] string hash,
            [ExcelArgument(Description = "The relevant sim", Name = "Sim")] double sim,
            [ExcelArgument(Description = "The relevant year", Name = "Year")] double year)
        {
            return Run(() => StochArrayLogic.Value(hash, sim, year));
        }

        /// <summary>
        /// Calculates the VAR of a <see cref="StochArray"/> at a particular year 
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="StochArray"/> object</param>
        /// <param name="year">The relevant year</param>
        [ExcelFunction(
            Name = "Lloyds.StochArray.VAR",
            Description = "Calculates the VAR of a StochArray at a particular year and confidence level",
            Category = FunctionCategory)]
        public static object VAR(
            [ExcelArgument(Description = "The hash of a valid StochArray object", Name = "Stoch Array")] string hash,
            [ExcelArgument(Description = "The relevant year", Name = "Year")] object year,
            [ExcelArgument(Description = "The relevant confidence level", Name = "Confidence level")] object confidenceLevel)
        {
            return Run(() => StochArrayLogic.VAR(hash, year, confidenceLevel));
        }

        /// <summary>
        /// Calculates the accumulation of a <see cref="StochArray"/> at a particular year 
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="StochArray"/> object</param>
        /// <param name="year">The accumulation year</param>
        /// <param name="annualise">Whether to annualise. Default = 'Y'</param>
        /// <returns>A <see cref="Sample"/></returns>
        [ExcelFunction(
            Name = "Lloyds.StochArray.Accumulate",
            Description = "Calculates the accumulation of a StochArray at a particular year",
            Category = FunctionCategory)]
        public static object Accumulate(
            [ExcelArgument(Description = "The hash of a valid StochArray object", Name = "Stoch Array")] string hash,
            [ExcelArgument(Description = "[OPTIONAL] The accumulation year. Default = MaxHorizon", Name = "Year")] object year,
            [ExcelArgument(Description = "[OPTIONAL] Annualise the result? 'Annualised' or 'Cumulative'. Deafult = 'Annualised'", Name = "Annualise")] object annualise)
        {
            year = year is ExcelMissing ? string.Empty : year;
            annualise = annualise is ExcelMissing ? string.Empty : annualise;
            return Run(() => StochArrayLogic.Accumulate(hash, year, annualise));
        }

        /// <summary>
        /// Combines two or more <see cref="StochArray"/>
        /// </summary>
        /// <param name="hashes">The hashes of valid <see cref="StochArray"/> objects</param>
        /// <param name="proportions">The starting porpotions to use for combining</param>
        /// <param name="rebalance">Whether to rebalance at each time step. Default = 'N'</param>
        /// <returns>A <see cref="StochArray"/></returns>
        [ExcelFunction(
            Name = "Lloyds.StochArray.Combine",
            Description = "Combines two or more StochArray according to proportions ",
            Category = FunctionCategory)]
        public static object Combine(
            [ExcelArgument(Description = "The hashes of valid StochArray objects", Name = "Stoch Array Range")] object[,] hashes,
            [ExcelArgument(Description = "The starting porpotions to use for combining", Name = "Proportions Range")] object[,] proportions,
            [ExcelArgument(Description = "[OPTIONAL] Rebalance the stocharray at each time step? 'Rebalance' or 'NoRebalancing'. Deafult = 'Rebalance'", Name = "Rebalance")] object rebalance)
        {
            rebalance = rebalance is ExcelMissing ? string.Empty : rebalance;
            return Run(() => StochArrayLogic.Combine(hashes, proportions, rebalance));
        }


        //+ Async Functions           

        /// <summary>
        /// Calculates the VAR of a <see cref="StochArray"/> at a particular year 
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="StochArray"/> object</param>
        /// <param name="year">The relevant year</param>
        [ExcelFunction(
            Name = "Lloyds.Async.StochArray.VAR",
            Description = "Calculates the VAR of a StochArray at a particular year and confidence level",
            Category = FunctionCategory,
            IsHidden = true)]
        public static object VARAsync(
            [ExcelArgument(Description = "The hash of a valid StochArray object", Name = "Stoch Array")] string hash,
            [ExcelArgument(Description = "The relevant year", Name = "Year")] double year,
            [ExcelArgument(Description = "The relevant confidence level", Name = "Confidence level")] object confidenceLevel)
        {
            return RunAsync(() => StochArrayLogic.VAR(hash, year, confidenceLevel), "Lloyds.Async.StochArray.VAR");
        }      
      
    }
}
