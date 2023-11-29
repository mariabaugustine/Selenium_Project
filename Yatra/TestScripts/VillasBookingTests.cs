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

namespace Yatra.TestScripts
{
    internal class VillasBookingTests:CoreCodes
    {
        [Test]
        public void TestvillasBooking()
        {

            List<SearchData> searchDataList;
            List<TravellerData> travellerDataList;
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
            var villasPage=yatraHP.ClickVillasIcon();
          
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //Thread.Sleep(1000);
            Assert.AreEqual("https://www.yatra.com/homestays", driver.Url);



            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/TestData/VillasBookingData.xlsx";
            string? sheetName = "SearchVilla";

            searchDataList = ExcelUtilities.ReadExcelData(excelFilePath, sheetName);

            foreach (var excelData in searchDataList)
            {

                //string? city = excelData?.City;
                //Console.WriteLine($"City: {city}");
                villasPage.TypeCity(excelData.City);
                //Thread.Sleep(1000);
                //fluentWait.Timeout=TimeSpan.FromMilliseconds(1000);
                

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;


                js.ExecuteScript("arguments[0].innerText= \"" + excelData.CheckIn + "\";", villasPage.CheckIN);
                js.ExecuteScript("arguments[0].innerText= \"" + excelData.CheckOut + "\";", villasPage.CheckOut);
                js.ExecuteScript("arguments[0].innerText= \"" + excelData.CheckIn + "\";", villasPage.CheckINPara);
                js.ExecuteScript("arguments[0].innerText= \"" + excelData.CheckOut + "\";", villasPage.CheckOutPara);


            }


            //Thread.Sleep(1000);
            villasPage.ClickAddButton();
            villasPage.ClickRemoveAdultButton();
            //Thread.Sleep(5000);

            Console.WriteLine("CheckOut:"+driver.FindElement(By.Id("BE_hotel_checkout_date")).Text);
            Console.WriteLine("CheckIn" +driver.FindElement(By.Id("BE_hotel_checkin_date")).Text);
            Console.WriteLine("City"+driver.FindElement(By.Id("BE_hotel_destination_city")).Text);

            var searchResultPage=villasPage.clickSearchVillas();
            Assert.That(driver.Url.Contains("homestay-search"));
            //Thread.Sleep(1000);




            var filterPage=searchResultPage.ClickFilterPropertyType();
            //Thread.Sleep(5000);
            string location = driver.FindElement(By.XPath("//*[@id=\"result0\"]/div[1]/div[1]/ul[1]/li[1]/p/span")).Text;
            //Assert.That(location.Contains("Connaught Place"));
            filterPage.ClickChooseRoom();
           
            //Thread.Sleep(1000);

            
            
            List<string> nextWindow = driver.WindowHandles.ToList();
            driver.SwitchTo().Window(nextWindow[1]);
            var bookingpage=new BookingPage(driver);
            // Thread.Sleep(5000);
           // ScrollIntoView(driver, driver.FindElement(By.XPath("//*[@id=\"roomWrapper0001823650\"]/div[2]/div[5]/button")));
            var confirmation=bookingpage.ClickBookNowButton();
           // Assert.That(driver.Url.Contains("review"));
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
            Thread.Sleep(1000);
        
        }  
    }
}
