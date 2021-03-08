using ExcelDna.Integration;
using ExcelDna.IntelliSense;
using ExcelDna.Logging;
using System.Diagnostics;

namespace Analysis_Xll.AddIn
{
    public class AddInManager : IExcelAddIn
    {
        public void AutoOpen()
        {
            var listener = new LogDisplayTraceListener();            
            Trace.Listeners.Add(listener);
            ExcelIntegration.RegisterUnhandledExceptionHandler(
                ex => "Unhandled EXCEPTION: " + ex.ToString());
            IntelliSenseServer.Install();
        }

        public void AutoClose()
        {
            IntelliSenseServer.Uninstall();            
        }

       
    }
}
