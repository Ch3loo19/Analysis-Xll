using ExcelDna.Integration;
using System;
using System.Diagnostics;

namespace Analysis_Xll.Utilities
{
    internal static class Utilities
    {
        internal static object Run(Func<object> functionToEvaluate)
        {
            if (ExcelDnaUtil.IsInFunctionWizard())  { return "[In Function Wizard]";  }
            object result;
            try
            {               
                result = functionToEvaluate();
            }
            catch (Exception e)
            {
                Trace.TraceWarning(e.Message);
                return ExcelError.ExcelErrorValue;
            }

            return result;
        }

        internal static object RunAsync(Func<object> functionToEvaluate, string functionName)
        {
            if (ExcelDnaUtil.IsInFunctionWizard()) { return "[In Function Wizard]"; }
            object result;
            try
            {
                result = ExcelAsyncUtil.Run(functionName, null, () => functionToEvaluate());
            }
            catch (Exception e)
            {
                Trace.TraceWarning(e.Message);
                return ExcelError.ExcelErrorValue;
            }

            return result;
        }
    }
}
