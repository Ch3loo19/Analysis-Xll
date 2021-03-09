using Functions_Logic.ESG_Classes;
using Functions_Logic.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Functions_Logic.Utilities.Extensions;

namespace Functions_Logic.Importers
{
    public static class CsvImporter
    {

        public static string ReadStochArray(string fullPath, object horizon, object sim)
        {
            fullPath = fullPath.Replace(@"""", "");

            if (Path.GetExtension(fullPath).ToUpper() != ".CSV")
            {
                throw new ArgumentException("Invalid extension");
            }

            int horizonVal = horizon.ConvertOptionalParameterToInt("Max Horizon", int.MaxValue);
            if (horizonVal < 1)
            {
                throw new ArgumentException("Max horizon must be above 1");
            }

            int simVal = sim.ConvertOptionalParameterToInt("Max Sim", int.MaxValue);
            if (simVal < 1)
            {
                throw new ArgumentException("Max sim must be above 1");
            }

            var cache = Cache<StochArray>.GetCache;
            if (cache.TryGetItem(out var _, out var hash, fullPath, horizonVal, simVal))
            {
                return hash;
            }

            if (!File.Exists(fullPath))
            {
                throw new ArgumentException("Non-existent file");
            }

            return ReadStochArray(fullPath, cache, horizonVal, simVal);

        }

        private static string ReadStochArray(string fullPath, Cache<StochArray> cache, int maxHorizon, int maxSim)
        {
            List<double>[] unstructuredData;
            using (var reader = new StreamReader(fullPath))
            {
                // Ignore first line
                var line = reader.ReadLine();
                var values = line.Split(',');

                // First col is sim numbers
                int colCount = values.Count(val => !string.IsNullOrWhiteSpace(val)) - 1;
                int colsToRead = colCount > maxHorizon ? maxHorizon : colCount;

                // exclude the sim numbers col
                unstructuredData = new List<double>[colsToRead];

                for (int i = 0; i < colsToRead; i++)
                {
                    unstructuredData[i] = new List<double>();
                }

                int currentSim = 1;
                while (!reader.EndOfStream)
                {
                    if (currentSim > maxSim) { break; }

                    line = reader.ReadLine();
                    values = line.Split(',');

                    // in case you have blank rows
                    if (values.All(val => string.IsNullOrWhiteSpace(val))) { continue; }

                    for (int i = 0; i < colsToRead; i++)
                    {
                        var value = values[i + 1].ConvertParameterToDouble("Stoch Array value");
                        unstructuredData[i].Add(value);
                    }

                    currentSim++;
                }

            }

            var structuredData = unstructuredData.InvertEnumerableOrder();
            var stochArray = new StochArray(structuredData);
            _ = cache.TryAddItem(stochArray, out var hash, fullPath, maxHorizon, maxSim);

            return hash;
        }

    }
}
