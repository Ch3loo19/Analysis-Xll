using ExcelDna.Integration;
using Functions_Logic.ESG_Classes;
using Functions_Logic.Logic;
using static Analysis_Xll.Utilities.Utilities;

namespace Analysis_Xll.ESG_Classes
{

    /// <summary>
    /// Todo: To add function hyperlinks when documentation ready
    /// </summary>
    public static class FxArray_APIs
    {
        //+ Constants
        private const string FunctionCategory = "FxArray";


        //+ Functions

        /// <summary>
        /// Retrieves the current exchange rate from a <see cref="FxArray"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="FxArray"/> object</param>
        [ExcelFunction(
            Name = "Lloyds.FxArray.GetCurrentExchangeRate",
            Description = "Retrieves the current exchange rate of an FxArray",
            Category = FunctionCategory)]
        public static object GetCurrentExchangeRate([ExcelArgument(Description = "The hash of a valid FxArray object", Name = "FX Array")] string hash)
        {
            return Run(() => FxArrayLogic.GetCurrentExchangeRate(hash));
        }

        /// <summary>
        /// Retrieves the currency returns from a <see cref="FxArray"/>
        /// </summary>
        /// <param name="fxHash">The hash of a valid <see cref="FxArray"/> object</param>
        /// <param name="accumulation">Whether returns are expressed cumulatively or annualised</param>
        [ExcelFunction(
            Name = "Lloyds.FxArray.GetCurrencyReturns",
            Description = "Retrieves the currency returns",
            Category = FunctionCategory)]
        public static object GetCurrencyReturns(
            [ExcelArgument(Description = "The hash of a valid FxArray object", Name = "FX Array")] string fxHash,
            [ExcelArgument(Description = "Whether returns are expressed cumulatively or annualised. 'Cumulative' or 'Annualised' ", Name = "Accumulation" )] string accumulation)
        {
            return Run(() => FxArrayLogic.GetCurrencyReturns(fxHash, accumulation));
        }

        /// <summary>
        /// Converts GBP Returns into local returns using a <see cref="FxArray"/> for a <see cref="StochArray"/>
        /// </summary>
        /// <param name="fxHash">The hash of a valid <see cref="FxArray"/> object</param>
        /// <param name="stochArrayHash">The hash of a valid <see cref="StochArray"/> object</param>
        [ExcelFunction(
            Name = "Lloyds.FxArray.GetLocalStochArrayReturns",
            Description = "Converts GBP Returns into local returns using an FxArray for a StochArray",
            Category = FunctionCategory)]
        public static object GetLocalStochArrayReturns(
            [ExcelArgument(Description = "The hash of a valid FxArray object", Name = "FX Array")] string fxHash,
            [ExcelArgument(Description = "The hash of a valid StochArray object ", Name = "Stoch Array")] string stochArrayHash)
        {
            return Run(() => FxArrayLogic.GetLocalStochArrayReturns(fxHash, stochArrayHash));
        }

        /// <summary>
        /// Converts local Returns into GBP returns using a <see cref="FxArray"/> for a <see cref="StochArray"/>
        /// </summary>
        /// <param name="fxHash">The hash of a valid <see cref="FxArray"/> object</param>
        /// <param name="stochArrayHash">The hash of a valid <see cref="StochArray"/> object</param>
        [ExcelFunction(
            Name = "Lloyds.FxArray.GetGBPStochArrayReturns",
            Description = "Converts local Returns into GBP returns using an FxArray for a StochArray",
            Category = FunctionCategory)]
        public static object GetGBPStochArrayReturns(
            [ExcelArgument(Description = "The hash of a valid FxArray object", Name = "FX Array")] string fxHash,
            [ExcelArgument(Description = "The hash of a valid StochArray object ", Name = "Stoch Array")] string stochArrayHash)
        {
            return Run(() => FxArrayLogic.GetGBPStochArrayReturns(fxHash, stochArrayHash));
        }



        //+ Async Functions

        /// <summary>
        /// Retrieves the current exchange rate from a <see cref="FxArray"/>
        /// </summary>
        /// <param name="hash">The hash of a valid <see cref="FxArray"/> object</param>
        [ExcelFunction(
            Name = "Lloyds.Async.FxArray.GetCurrentExchangeRate",
            Description = "Retrieves the current exchange rate of an FxArray",
            Category = FunctionCategory,
            IsHidden =true)]
        public static object GetCurrentExchangeRateAsync([ExcelArgument(Description = "The hash of a valid FxArray object", Name = "FX Array")] string hash)
        {
            return RunAsync(() => FxArrayLogic.GetCurrentExchangeRate(hash), "Lloyds.Async.FxArray.GetCurrentExchangeRate");
        }

        /// <summary>
        /// Retrieves the currency returns from a <see cref="FxArray"/>
        /// </summary>
        /// <param name="fxHash">The hash of a valid <see cref="FxArray"/> object</param>
        /// <param name="accumulation">Whether returns are expressed cumulatively or annualised</param>
        [ExcelFunction(
            Name = "Lloyds.Async.FxArray.GetCurrencyReturns",
            Description = "Retrieves the currency returns",
            Category = FunctionCategory,
            IsHidden =true)]
        public static object GetCurrencyReturnsAsync(
            [ExcelArgument(Description = "The hash of a valid FxArray object", Name = "FX Array")] string fxHash,
            [ExcelArgument(Description = "Whether returns are expressed cumulatively or annualised. 'Cumulative' or 'Annualised' ", Name = "Accumulation")] string accumulation)
        {
            return RunAsync(() => FxArrayLogic.GetCurrencyReturns(fxHash, accumulation), "Lloyds.Async.FxArray.GetCurrencyReturns");
        }

        /// <summary>
        /// Converts GBP Returns into local returns using a <see cref="FxArray"/> for a <see cref="StochArray"/>
        /// </summary>
        /// <param name="fxHash">The hash of a valid <see cref="FxArray"/> object</param>
        /// <param name="stochArrayHash">The hash of a valid <see cref="StochArray"/> object</param>
        [ExcelFunction(
            Name = "Lloyds.Async.FxArray.GetLocalStochArrayReturns",
            Description = "Converts GBP Returns into local returns using an FxArray for a StochArray",
            Category = FunctionCategory,
            IsHidden = true)]
        public static object GetLocalStochArrayReturnsAsync(
            [ExcelArgument(Description = "The hash of a valid FxArray object", Name = "FX Array")] string fxHash,
            [ExcelArgument(Description = "The hash of a valid StochArray object ", Name = "Stoch Array")] string stochArrayHash)
        {
            return RunAsync(() => FxArrayLogic.GetLocalStochArrayReturns(fxHash, stochArrayHash), "Lloyds.Async.FxArray.GetLocalStochArrayReturns");
        }

        /// <summary>
        /// Converts local Returns into GBP returns using a <see cref="FxArray"/> for a <see cref="StochArray"/>
        /// </summary>
        /// <param name="fxHash">The hash of a valid <see cref="FxArray"/> object</param>
        /// <param name="stochArrayHash">The hash of a valid <see cref="StochArray"/> object</param>
        [ExcelFunction(
            Name = "Lloyds.Async.FxArray.GetGBPStochArrayReturns",
            Description = "Converts local Returns into GBP returns using an FxArray for a StochArray",
            Category = FunctionCategory,
            IsHidden = true)]
        public static object GetGBPStochArrayReturnAsync(
            [ExcelArgument(Description = "The hash of a valid FxArray object", Name = "FX Array")] string fxHash,
            [ExcelArgument(Description = "The hash of a valid StochArray object ", Name = "Stoch Array")] string stochArrayHash)
        {
            return RunAsync(() => FxArrayLogic.GetGBPStochArrayReturns(fxHash, stochArrayHash), "Lloyds.Async.FxArray.GetGBPStochArrayReturns");
        }

        
    }
}
