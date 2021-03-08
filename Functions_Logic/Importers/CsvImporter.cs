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
        public static string ReadFxArray(string fullPath, object horizon, object sim)
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

            var cache = Cache<FxArray>.GetCache;
            if (cache.TryGetItem(out var _, out var hash, fullPath, horizonVal, simVal))
            {
                return hash;
            }

            if (!File.Exists(fullPath))
            {
                throw new ArgumentException("Non-existent file");
            }

            return ReadFxArray(fullPath, cache, horizonVal, simVal);
        }

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

        private static string ReadFxArray(string fullPath, Cache<FxArray> cache, int maxHorizon, int maxSim)
        {
            List<List<double>> unstructuredData= new List<List<double>>();
            double currentExchangeRate;
            using (var reader = new StreamReader(fullPath))
            {                
                string line = reader.ReadLine();
                var values = line.Split(',');
                if (!(string.Equals(values[0],"1") && string.Equals(values[1],"0")))
                {
                    throw new ArgumentException("Fx Data is in an unexpected format");
                }
                var firstLoopData = new List<double>();
                currentExchangeRate =values[2].ConvertParameterToDouble("Fx Value");
                int colsToRead = 1;

                do
                {
                    if (reader.EndOfStream)
                    { throw new ArgumentException("Fx Data is in an unexpected format"); }
                    line = reader.ReadLine();
                    values = line.Split(',');                  

                    if (values[1].ConvertParameterToInt("Fx Data") == 0) 
                    {
                        if (currentExchangeRate!=values[2].ConvertParameterToDouble("Fx Value"))
                        {
                            throw new ArgumentException("Fx Data is in an unexpected format");
                        }
                        break;  
                    }

                    if (colsToRead > maxHorizon)
                    {
                        continue;
                    }

                    if (!string.Equals(values[0],"1") || values[1].ConvertParameterToInt("Fx Value") !=colsToRead)
                    {
                        throw new ArgumentException("Fx Data is in an unexpected format");
                    }                   

                    var data = new List<double>();
                    data.Add(values[2].ConvertParameterToDouble("Fx Value"));
                    unstructuredData.Add(data);
                    colsToRead++;
                  

                } while (true); 

                int currentSim = 2;            
                var yearsRead = new Queue<int>(Enumerable.Range(1, colsToRead-1));
                int currentYear = 0;
                
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    values = line.Split(',');
                    // in case you have blank rows
                    if (values.All(val => string.IsNullOrWhiteSpace(val))) { continue; }

                    int simRead = values[0].ConvertParameterToInt("Fx Data");
                    int yearRead = values[1].ConvertParameterToInt("Fx Data");

                    if (yearRead > maxHorizon)
                    {
                        continue;
                    }

                    if (yearsRead.Count()==0)
                    {
                        yearsRead = new Queue<int>(Enumerable.Range(1, colsToRead-1));
                        currentSim++;
                        if (currentSim > maxSim) { break; }
                        if (!values[2].ConvertParameterToDouble("Fx Value").EqualsWithPrecision(currentExchangeRate))
                        {
                            throw new ArgumentException("Current Exchange Rates do not match in ESG File");
                        }
                        continue;
                    }

                  
                    currentYear = yearsRead.Dequeue();                 


                    if (simRead!=currentSim || yearRead != currentYear)
                    {
                        throw new ArgumentException("Fx Data is in an unexpected format");
                    }

                    unstructuredData[currentYear - 1].Add(values[2].ConvertParameterToDouble("Fx Data"));                
                }

            }

            var structuredData = unstructuredData.InvertEnumerableOrder();
            var fxArray = new FxArray(structuredData, currentExchangeRate);
            _ = cache.TryAddItem(fxArray, out var hash, fullPath, maxHorizon, maxSim);

            return hash;
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
