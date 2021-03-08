using ExcelDna.Integration;
using Functions_Logic.ESG_Classes;
using Functions_Logic.Importers;
using System;
using static Analysis_Xll.Utilities.Utilities;

namespace Analysis_Xll.Importer
{
    public static class Importer
    {
        //+ Constants
        private const string FunctionCategory = "Importer";

        private class ImporterFunctionAttribute : ExcelFunctionAttribute
        {
            internal ImporterFunctionAttribute()
            {
                Category = FunctionCategory;
            }
        }


        //+ Functions

        /// <summary>
        /// Imports an <see cref="FxArray"/> directly from a csv saved in a physical location
        /// </summary>
        /// <param name="fullPath">The csv path</param>
        /// <returns></returns>
        [ImporterFunction(
            Name = "Lloyds.Importer.LoadFxArrayFromCsv",
            Description = "Imports an FxArray directly from a csv saved in a physical location")]
        public static object ImportFxArrayCsv(
            [ExcelArgument(Description = "The csv path", Name = "CSV path")] string fullPath,
            [ExcelArgument(Description = "[OPTIONAL] The maximum horizon to read", Name = "MaxHorizon")] object horizon,
            [ExcelArgument(Description = "[OPTIONAL] The maximum sim to read", Name = "MaxSim")] object sim)
        {
            horizon = horizon is ExcelMissing ? string.Empty : horizon;
            sim = sim is ExcelMissing ? string.Empty : sim;
            return Run(() => CsvImporter.ReadFxArray(fullPath, horizon, sim));
        }

        /// <summary>
        /// Imports a <see cref="StochArray"/> directly from a csv saved in a physical location
        /// </summary>
        /// <param name="fullPath">The csv path</param>
        /// <returns></returns>
        [ImporterFunction(
            Name = "Lloyds.Importer.LoadStochArrayFromCsv",
            Description = "Imports a StochArray directly from a csv saved in a physical location")]
        public static object ImportStochArrayCsv(
            [ExcelArgument(Description = "The csv path", Name = "CSV path")] string fullPath,
            [ExcelArgument(Description = "[OPTIONAL] The maximum horizon to read", Name = "MaxHorizon")] object horizon,
            [ExcelArgument(Description = "[OPTIONAL] The maximum sim to read", Name = "MaxSim")] object sim)
        {
            horizon = horizon is ExcelMissing ? string.Empty : horizon;
            sim = sim is ExcelMissing ? string.Empty : sim;
            return Run(() => CsvImporter.ReadStochArray(fullPath, horizon, sim));
        }

        /// <summary>
        /// Imports a <see cref="StochasticCurve"/> directly from a csv saved in a physical location
        /// </summary>
        /// <param name="fullPath">The csv path</param>
        /// <returns></returns>
        [ImporterFunction(
           Name = "Lloyds.Importer.LoadStochasticCurveFromCsv",
           Description = "Imports a stochastic curve directly from a csv saved in a physical location")]
        public static object ImportStochasticCurveFromCsv([ExcelArgument(Description = "The csv path, include \"\" ", Name = "CSV path")] string fullPath)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Loads data from range and converts to a <see cref="Sample"/> 
        /// </summary>
        /// <param name="data">The data. This is read in as a 2D object array</param>
        /// <returns></returns>
        [ImporterFunction(
           Name = "Lloyds.Importer.LoadSampleFromRange",
           Description = "Loads data from range and converts to a Sample")]
        public static object LoadSampleFromRange([ExcelArgument(Description = "The range to be read in", Name = "Range")] object[,] data)
        {
            return Run(() => RangeLoader.LoadSample(data));
        }


        //+ Async Functions

        /// <summary>
        /// Imports an <see cref="FxArray"/> directly from a csv saved in a physical location
        /// </summary>
        /// <param name="fullPath">The csv path</param>
        /// <returns></returns>
        [ImporterFunction(
            Name = "Lloyds.Async.Importer.LoadFxArrayFromCsv",
            Description = "Imports an FxArray directly from a csv saved in a physical location",
            IsHidden =true)]
        public static object ImportFxArrayCsvAsync(
            [ExcelArgument(Description = "The csv path", Name = "CSV path")] string fullPath,
            [ExcelArgument(Description = "[OPTIONAL] The maximum horizon to read", Name = "MaxHorizon")] object horizon,
            [ExcelArgument(Description = "[OPTIONAL] The maximum sim to read", Name = "MaxSim")] object sim)
        {
            horizon = horizon is ExcelMissing ? string.Empty : horizon;
            sim = sim is ExcelMissing ? string.Empty : sim;
            return RunAsync(() => CsvImporter.ReadFxArray(fullPath, horizon, sim), "Lloyds.Async.Importer.LoadFxArrayFromCsv");
        }

        /// <summary>
        /// Imports a <see cref="StochArray"/> directly from a csv saved in a physical location
        /// </summary>
        /// <param name="fullPath">The csv path</param>
        /// <returns></returns>
        [ImporterFunction(
            Name = "Lloyds.Async.Importer.LoadStochArrayFromCsv",
            Description = "Imports a StochArray directly from a csv saved in a physical location",
            IsHidden = true)]
        public static object ImportStochArrayCsvAsync(
            [ExcelArgument(Description = "The csv path", Name = "CSV path")] string fullPath,
            [ExcelArgument(Description = "[OPTIONAL] The maximum horizon to read", Name = "MaxHorizon")] object horizon,
            [ExcelArgument(Description = "[OPTIONAL] The maximum sim to read", Name = "MaxSim")] object sim)
        {
            horizon = horizon is ExcelMissing ? string.Empty : horizon;
            sim = sim is ExcelMissing ? string.Empty : sim;
            return RunAsync(() => CsvImporter.ReadStochArray(fullPath, horizon, sim), "Lloyds.Async.Importer.LoadStochArrayFromCsv");
        }

        /// <summary>
        /// Loads data from range and converts to a <see cref="Sample"/> 
        /// </summary>
        /// <param name="data">The data. This is read in as a 2D object array</param>
        /// <returns></returns>
        [ImporterFunction(
           Name = "Lloyds.Async.Importer.LoadSampleFromRange",
           Description = "Loads data from range and converts to a Sample",
            IsHidden = true)]
        public static object LoadSampleFromRangeAsync([ExcelArgument(Description = "The range to be read in", Name = "Range")] object[,] data)
        {
            return RunAsync(() => RangeLoader.LoadSample(data), "Lloyds.Async.Importer.LoadSampleFromRange");
        }

    }
}
