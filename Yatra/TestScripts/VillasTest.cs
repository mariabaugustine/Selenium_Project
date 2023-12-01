using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatra.PageObjects;
using Yatra.Utilities;

namespace Yatra.TestScripts
{
    internal class VillasTest : CoreCodes
    {
        [Test, Category("Smoke Testing")]
        public void LogoCheck()
        {
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? logfilepath = currDir + "/Logs/log_" +
               DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day)
           .CreateLogger();
            YatraHomePage yatra=new YatraHomePage(driver);
            yatra.LogoCheck();
            Log.Information("Checking logo is refreshing or not");
            try
            {
                Assert.AreEqual(driver.Url, "https://www.yatra.com/");
                Log.Information("Test passed for Checking logo");
                test = extent.CreateTest("Logo Check");
                test.Pass("Logo Checked  successfully");
            }
            catch(AssertionException ex)
            {
                Log.Error($"Test failed for Logo Check. \n Exception: {ex.Message}");
                test = extent.CreateTest("Logo Check");
                test.Fail("Logo Check failed");
            }

            
        }
    }
}
