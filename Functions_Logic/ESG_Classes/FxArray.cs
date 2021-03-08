using System;
using Functions_Logic.Utilities;
using System.Linq;
using System.Collections.Generic;

namespace Functions_Logic.ESG_Classes
{
    public class FxArray : StochArray
    {
        private readonly double _currentExchangeRate;

        private Func<double, double, double> _convertFromGBPToLocal = (currReturn, gbpReturn) => (1 / (1 + currReturn))* (1+gbpReturn)-1;
        private Func<double, double, double> _convertFromLocalToGBP = (currReturn, gbpReturn) => (1 + currReturn) * (1 + gbpReturn) - 1;

        public double CurrentExchangeRate => _currentExchangeRate;

        public FxArray(double[][] values, double currentRate) : base(values)
        {
            _currentExchangeRate = currentRate;
        }

        public StochArray GetCurrencyReturns(Accumulation accumulation)
        {
            return new StochArray(
                Values.Select(simReturns => GetCurrencyReturn(simReturns, accumulation).ToArray()).ToArray());
        }

        private IEnumerable<double> GetCurrencyReturn(double[] simReturns, Accumulation accumulation)
        {
            for (int i = 0; i < MaxHorizon; i++)
            {
                double cumulativeCurrencyReturn = _currentExchangeRate / simReturns[i];
                yield return accumulation == Accumulation.Cumulative ?
                                              cumulativeCurrencyReturn - 1 :
                                              Math.Pow(cumulativeCurrencyReturn, 1d / (i + 1)) - 1;
            }
        }

        public StochArray GetLocalStochArrayReturns(StochArray gbpReturnsStochArray)=>
                 ConvertReturns(gbpReturnsStochArray, _convertFromGBPToLocal);


        public StochArray GetGBPStochArrayReturns(StochArray localReturnsStochArray)=>
            ConvertReturns(localReturnsStochArray, _convertFromLocalToGBP);

        private StochArray ConvertReturns(StochArray returnsArray, Func<double, double, double> converter)
        {
            var currencyReturns = GetCurrencyReturns(Accumulation.Annualised);

            int sim = MaxSim;
            int hor = MaxHorizon;
            var result = new double[sim][];

            for (int i = 0; i < sim; i++)
            {
                var simReturns = new double[hor];
                for (int j = 0; j < hor; j++)
                {
                    simReturns[j] = converter(currencyReturns[i][j], returnsArray[i][j]);
                }
                result[i] = simReturns;
            }

            return new StochArray(result);
        }

     



    }
}
