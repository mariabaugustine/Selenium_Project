﻿using OpenQA.Selenium.Support.UI;
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
            var VillasPage=yatraHP.ClickVillasIcon();
            Thread.Sleep(1000);
            Assert.AreEqual("https://www.yatra.com/homestays", driver.Url);



            string? currDir = Directory.GetParent(@"../../../")?.FullName;
            string? excelFilePath = currDir + "/TestData/VillasBookingData.xlsx";
            string? sheetName = "SearchVilla";

            searchDataList = ExcelUtilities.ReadExcelData(excelFilePath, sheetName);

            foreach (var excelData in searchDataList)
            {

                string? city = excelData?.City;
                Console.WriteLine($"City: {city}");
                VillasPage.TypeCity(excelData.City);
                Thread.Sleep(1000);
                //fluentWait.Timeout=TimeSpan.FromMilliseconds(1000);
                

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;


                js.ExecuteScript("arguments[0].innerText= \"" + excelData.CheckIn + "\";", VillasPage.CheckIN);
                js.ExecuteScript("arguments[0].innerText= \"" + excelData.CheckOut + "\";", VillasPage.checkOut);


            }

            VillasPage.ClickAddButton();
            VillasPage.ClickRemoveAdultButton();
            //fluentWait.Timeout = TimeSpan.FromMilliseconds(1000);
            var searchResultPage=VillasPage.clickSearchVillas();
            Assert.That(driver.Url.Contains("homestay-search"));
            Thread.Sleep(1000);
            var filterPage=searchResultPage.ClickFilterPropertyType();
            Thread.Sleep(5000);
            filterPage.ClickChooseRoom();
            //Thread.Sleep(1000);

            List<string> nextWindow = driver.WindowHandles.ToList();
            driver.SwitchTo().Window(nextWindow[1]);
           var bookingpage=new BookingPage(driver);
            Thread.Sleep(5000);
            var confirmation=bookingpage.ClickBookNowButton();
            Thread.Sleep(3000);
            string personDatasheet ="TravellerData";
            travellerDataList=PersonUtilities.ReadExcelData(excelFilePath, personDatasheet);
            ScrollIntoView(driver, driver.FindElement(By.Id("traveller-dom")));
            Thread.Sleep(3000);
            foreach (var data in travellerDataList)
            {
                confirmation.TypeEmail(data.Email);
                Thread.Sleep(1000);
                confirmation.TypeMobile(data.Mobile);
                confirmation.TypeFirstName(data.Fname);
                confirmation.TypeLastName(data.Lname);
            }
            confirmation.TypeTitle();
            Thread.Sleep(1000);
        
        }  
    }
}
