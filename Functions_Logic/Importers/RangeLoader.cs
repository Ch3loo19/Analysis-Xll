using Functions_Logic.ESG_Classes;
using Functions_Logic.Utilities;

namespace Functions_Logic.Importers
{
    public static class RangeLoader
    {
        public static string LoadSample(object[,] data)
        {
            var cache = Cache<Sample>.GetCache;
           
            int dimension1 = data.GetLength(0);
            int dimension2 = data.GetLength(1);

            if (dimension1 > 1 && dimension2 > 1)
            {
                throw new SampleShapeException(dimension1, dimension2);
            }

            if (cache.TryGetItem(out _, out string key, data.Convert2DParameterTo1DArray("Range Data"))) { return key; }

            var result = dimension1 > 1 ? new double[dimension1] : new double[dimension2];

            for (int i = 0; i < dimension1; i++)
            {
                for (int j = 0; j < dimension2; j++)
                {
                    result[i * dimension2 + j] = data[i, j].ConvertParameterToDouble("Sample value");
                }
            }

            var sample = new Sample(result);
            _ = cache.TryAddItem(sample, out var newHash, data);

            return newHash;



        }
    }
}
