using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatra.Utilities;
using Yatra.PageObjects;
using Serilog;

namespace Yatra.TestScripts
{
    internal class CareerTest:CoreCodes
    {
        [Test,Category("E2E")]
       public void CareerTesting()
        {
            List<CareerData> CareerList;
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? logfilepath = currDir + "/Logs/log_" +
               DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day)
           .CreateLogger();


            //DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            //fluentWait.Timeout = TimeSpan.FromSeconds(10);
            //fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
            //fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            //fluentWait.Message = "Element not found";
            CareerHomePage CHP = new CareerHomePage(driver);
            ScrollIntoView(driver, driver.FindElement(By.XPath("//*[@id=\"js_yt_footer\"]/div/ul/li[3]/a/span")));
            var careerpage = CHP.ClickCareers();
            TakeScreenShot();

            //Assert.That(driver.Url.Contains("career"));


            ScrollIntoView(driver, driver.FindElement(By.XPath("//a[text()='Explore Jobs Opening']")));
            var careermain=careerpage.ClickCareerButton();
            //Assert.That(driver.Url.Contains("job-portal"));

            careermain.ClickChooseCourse();
            TakeScreenShot();
            careermain.ClickApplyButton();
            TakeScreenShot();
            Thread.Sleep(10000);
            string? excelFilePath = currDir + "/TestData/VillasBookingData.xlsx";
            string sheetName = "Career";
            CareerList=CareerUtilities.ReadExcelData(excelFilePath, sheetName);
            foreach(var cdata in CareerList) 
            {
                string? name= cdata.Name;
                
                string? city= cdata.City;
                string? phone = cdata.Mobile;
                string? linkedin = cdata.LinkedInUrl;
                string? emai = cdata.EmailId;
                careermain.ApplyJob(name, emai, phone, linkedin, city);
                //Thread.Sleep(2000);
                
            }
            TakeScreenShot();
            try
            {
                string text = driver.FindElement(By.XPath("//div//h3[text()='Technology Buff? Yatra is growing']")).Text;
                Assert.AreEqual(text, "Technology Buff? Yatra is growing");
                Log.Information("Test passed for Applying Job");
                test = extent.CreateTest("Apply job");
                test.Pass(" Applied job Successfully");
            }
            catch(AssertionException ex)
            {
                Log.Error($"Test failed for Applying job. \n Exception: {ex.Message}");
                test = extent.CreateTest("Apply Job");
                test.Fail("Apply Job Failed");
            }
        }
    }
}
