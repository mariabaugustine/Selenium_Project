using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatra.PageObjects;
using Yatra.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AventStack.ExtentReports.Gherkin.Model;
using Serilog;

namespace Yatra.TestScripts
{
    internal class VillasBookingTests:CoreCodes
    {
        [Test]
        [TestCase("Friday, 8 December 2023","Monday, 18 December 2023")]
        public void TestvillasBooking(string date1,string date2)
        {

            List<SearchData> searchDataList;
            List<TravellerData> travellerDataList;
            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? logfilepath = currDir + "/Logs/log_" +
               DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

           Log.Logger = new LoggerConfiguration()
          .WriteTo.Console()
          .WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day)
          .CreateLogger();

            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(10);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Message = "Element not found";
            var yatraHP = new YatraHomePage(driver);
            if(!driver.Url.Contains("https://www.yatra.com/"))
            {
                driver.Navigate().GoToUrl("https://www.yatra.com/");
            }

            Log.Information("Villas booking test started");

            var villasPage=yatraHP.ClickVillasIcon();

            Log.Information("Villas and stays option clicked");
          
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //Thread.Sleep(1000);

            try
            {
                Assert.AreEqual("https://www.yatra.com/homestays", driver.Url);
                Log.Information("Test passed for Villas and Stays option Clicking");
                test = extent.CreateTest("Villas and Stays Page Loading");
                test.Pass(" Page Loaded Successfully");
            }
            catch (AssertionException ex)
            {
                Log.Error($"Test failed for Villas and Stays Option Clicking. \n Exception: {ex.Message}");
                test = extent.CreateTest("Villas and Stays Page Loading");
                test.Fail("Villas and Stays Page Loading failed");
            }



            //string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/TestData/VillasBookingData.xlsx";
            string? sheetName = "SearchVilla";

            searchDataList = ExcelUtilities.ReadExcelData(excelFilePath, sheetName);

            foreach (var excelData in searchDataList)
            {

                //string? city = excelData?.City;
                //Console.WriteLine($"City: {city}");
               // villasPage.ClickCity();
                villasPage.TypeCity(excelData.City);
                Thread.Sleep(3000);
                villasPage.LabelClickedFunction();
                Thread.Sleep(4000);
                villasPage.GetCheckInDateClicked(date1);
                Thread.Sleep(4000);
                villasPage.GetCheckOutClicked(date2);
                Log.Information("All the input field are entered");

                //fluentWait.Timeout=TimeSpan.FromMilliseconds(1000);
                

                //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;


                //js.ExecuteScript("arguments[0].innerText= \"" + excelData.CheckIn + "\";", villasPage.CheckIN);
                //js.ExecuteScript("arguments[0].innerText= \"" + excelData.CheckOut + "\";", villasPage.CheckOut);
                //js.ExecuteScript("arguments[0].innerText= \"" + excelData.CheckIn + "\";", villasPage.CheckINPara);
                //js.ExecuteScript("arguments[0].innerText= \"" + excelData.CheckOut + "\";", villasPage.CheckOutPara);


            }


            //Thread.Sleep(1000);
            villasPage.ClickAddButton();
            villasPage.ClickRemoveAdultButton();
            //Thread.Sleep(5000);

            Console.WriteLine("CheckOut:"+driver.FindElement(By.Id("BE_hotel_checkout_date")).Text);
            Console.WriteLine("CheckIn" +driver.FindElement(By.Id("BE_hotel_checkin_date")).Text);
            Console.WriteLine("City"+driver.FindElement(By.Id("BE_hotel_destination_city")).Text);

            var searchResultPage=villasPage.clickSearchVillas();
            Log.Information("Search villas option clicked");
            try
            {
                Assert.That(driver.Url.Contains("homestay-search"));
                Log.Information("Test passed for Searching Villas");
                test = extent.CreateTest("Display Search Result");
                test.Pass(" Search result displayed Successfully");
            }
            catch(AssertionException ex)
            {
                Log.Error($"Test failed for Displaying search result. \n Exception: {ex.Message}");
                test = extent.CreateTest("Display Search Result");
                test.Fail("Search result displaying failed");
            }
            //Thread.Sleep(1000);


            var filter = searchResultPage.ClickFilterPropertyType("Star Inn");
            Log.Information("Filter result displayed successfully");

             filter.ClickChooseRoom();
            Log.Information("Room choosed successfully");
            //Thread.Sleep(5000);
           

            
            
            List<string> nextWindow = driver.WindowHandles.ToList();
            driver.SwitchTo().Window(nextWindow[1]);
            var bookingpage=new BookingPage(driver);
            // Thread.Sleep(5000);
           // ScrollIntoView(driver, driver.FindElement(By.XPath("//*[@id=\"roomWrapper0001823650\"]/div[2]/div[5]/button")));
            var confirmation=bookingpage.ClickBookNowButton();
            try
            {
                Log.Information("Booked successfully");
                Assert.That(driver.Url.Contains("review"));
            }
            catch (AssertionException ex)
            {
            }
            //Thread.Sleep(3000);


            string personDatasheet ="TravellerData";
            travellerDataList=PersonUtilities.ReadExcelData(excelFilePath, personDatasheet);
            ScrollIntoView(driver, driver.FindElement(By.Id("traveller-dom")));
            //Thread.Sleep(3000);
            foreach (var data in travellerDataList)
            {
                confirmation.TypeEmail(data.Email);
                //Thread.Sleep(1000);
                confirmation.TypeMobile(data.Mobile);
                confirmation.TypeFirstName(data.Fname);
                confirmation.TypeLastName(data.Lname);
            }
            confirmation.TypeTitle();
           // Thread.Sleep(5000);
            var lastPage = confirmation.ClickSubmit();
            //Thread.Sleep(3000);

            lastPage.PaytmClick();
            lastPage.PayNowClick();
            Thread.Sleep(3000);
        
        }  
    }
}
