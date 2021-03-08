using ExcelDna.Integration.CustomUI;
using ExcelDna.Logging;
using Functions_Logic.ESG_Classes;
using Functions_Logic.Utilities;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Analysis_Xll.AddIn
{
    [ComVisible(true)]
    public class RibbonController : ExcelRibbon
    {
        public override string GetCustomUI(string RibbonID)
        {
            const string ribbonCode = "RibbonCode.txt";
            var stream = GetEmbeddedReource(ribbonCode);

            string content;
            using (var reader = new StreamReader(stream))
            {
                content = reader.ReadToEnd();
            }

            return content;

           
        }

        public void OnLogButtonPressed(IRibbonControl control)
        {
            LogDisplay.Show();
        }

        public void OnNukeCacheButtonPressed(IRibbonControl control)
        {
            Cache<StochArray>.GetCache.ResetCache();
            Cache<Sample>.GetCache.ResetCache();
            Cache<FxArray>.GetCache.ResetCache();
            Cache<double>.GetCache.ResetCache();
            Cache<int>.GetCache.ResetCache();
            Cache<string>.GetCache.ResetCache();
        }

        public Bitmap GetLogImage(IRibbonControl control)
        {
            const string logImageName = "Log.bmp";
            var logResource = GetEmbeddedReource(logImageName);
            var bmp = new Bitmap(logResource);
            return bmp;
        }

        public Bitmap GetNukeCacheImage(IRibbonControl control)
        {
            const string logImageName = "Nuke Cache.jpg";
            var logResource = GetEmbeddedReource(logImageName);
            var bmp = new Bitmap(logResource);
            return bmp;
        }

        private static Stream GetEmbeddedReource(string logImageName)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var resources = executingAssembly.GetManifestResourceNames();
            string sourghtResource = resources.Single(res => res.Contains(logImageName));
            var logResource = executingAssembly.GetManifestResourceStream(sourghtResource);
            return logResource;
        }
    }
}
