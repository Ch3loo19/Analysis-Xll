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
        /// Imports a <see cref="StochArray"/> directly from a csv saved in a physical location
        /// </summary>
        /// <param name="fullPath">The csv path</param>
        /// <returns></returns>
        [ImporterFunction(
            Name = "Xll.Importer.LoadStochArrayFromCsv",
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
        /// Loads data from range and converts to a <see cref="Sample"/> 
        /// </summary>
        /// <param name="data">The data. This is read in as a 2D object array</param>
        /// <returns></returns>
        [ImporterFunction(
           Name = "Xll.Importer.LoadSampleFromRange",
           Description = "Loads data from range and converts to a Sample")]
        public static object LoadSampleFromRange([ExcelArgument(Description = "The range to be read in", Name = "Range")] object[,] data)
        {
            return Run(() => RangeLoader.LoadSample(data));
        }
    }
}
