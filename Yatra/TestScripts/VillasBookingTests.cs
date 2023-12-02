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
        [Test, Category("E2E")]
       // [TestCase("Friday, 8 December 2023","Monday, 18 December 2023"),]
        public void TestvillasBooking()
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

            //try
            //{
            //    Assert.AreEqual("https://www.yatra.com/homestays", driver.Url);
            //    Log.Information("Test passed for Booking villas");
            //    test = extent.CreateTest("Booking Villas");
            //    test.Pass(" villas booked Successfully");
            //}
            //catch (AssertionException ex)
            //{
            //    Log.Error($"Test failed for booking Villas and Stays . \n Exception: {ex.Message}");
            //    test = extent.CreateTest("Booking Villas");
            //    test.Fail("Villas booked failed");
            //}



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
                villasPage.GetCheckInDateClicked(excelData.CheckIn);
                Thread.Sleep(4000);
                villasPage.GetCheckOutClicked(excelData.CheckOut);
                Log.Information("All the input field are entered");

      


            }


            //Thread.Sleep(1000);
            villasPage.ClickAddButton();
            villasPage.ClickRemoveAdultButton();
            //Thread.Sleep(5000);

            //Console.WriteLine("CheckOut:"+driver.FindElement(By.Id("BE_hotel_checkout_date")).Text);
            //Console.WriteLine("CheckIn" +driver.FindElement(By.Id("BE_hotel_checkin_date")).Text);
            //Console.WriteLine("City"+driver.FindElement(By.Id("BE_hotel_destination_city")).Text);

            TakeScreenShot();
            var searchResultPage=villasPage.clickSearchVillas();
            TakeScreenShot();
            Log.Information("Search villas option clicked");
            
            //Thread.Sleep(1000);


            var filter = searchResultPage.ClickFilterPropertyType("Trendy B N B");
            TakeScreenShot();
            Log.Information("Filter result displayed successfully");

             filter.ClickChooseRoom();
            TakeScreenShot();
            Log.Information("Room choosed successfully");
            //Thread.Sleep(5000);
           

            
            
            List<string> nextWindow = driver.WindowHandles.ToList();
            driver.SwitchTo().Window(nextWindow[1]);
            var bookingpage=new BookingPage(driver);
            // Thread.Sleep(5000);
           // ScrollIntoView(driver, driver.FindElement(By.XPath("//*[@id=\"roomWrapper0001823650\"]/div[2]/div[5]/button")));
            var confirmation=bookingpage.ClickBookNowButton();
            TakeScreenShot();
           Thread.Sleep(3000);


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
            TakeScreenShot();
            //Thread.Sleep(3000);

            lastPage.PaytmClick();
            TakeScreenShot();
            lastPage.PayNowClick();
            TakeScreenShot();
            Log.Information("Room booked successfully");
            try
            {
                Assert.That(driver.Url.Contains("PaySwift"));
                Log.Information("Test passed for Applying booking villas and stays");
                test = extent.CreateTest("Book villas");
                test.Pass("Villas booked successfully");
            }
            catch (AssertionException ex) 
            {
                Log.Error($"Test failed for booking villas and stays. \n Exception: {ex.Message}");
                test = extent.CreateTest("Book Villas");
                test.Fail("Villas booked failed");
            }
        
        }  
    }
}
