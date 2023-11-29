﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Yatra.PageObjects
{
    internal class VillasBookingPage 
    {
        IWebDriver driver;

        public VillasBookingPage(IWebDriver driver)
        {
            this.driver = driver??throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How =How.Id,Using =("BE_hotel_destination_city"))]
        public IWebElement City { get; set; }

        [FindsBy(How=How.XPath,Using = "//input[@id='BE_hotel_checkin_date']//following::p[1]")]
        public IWebElement CheckINPara { get; set; }

        [FindsBy(How=How.XPath,Using = "//input[@id='BE_hotel_checkout_date']//following::p[1]")]
        public IWebElement CheckOutPara{ get; set;}
        [FindsBy(How = How.Id, Using = "BE_hotel_checkin_date")]
        public IWebElement CheckIN { get; set; }

        [FindsBy(How = How.Id, Using = "BE_hotel_checkout_date")]
        public IWebElement CheckOut { get; set; }

        [FindsBy(How =How.XPath,Using = "//*[@id=\"BE_Hotel_pax_info\"]/i")]
        private IWebElement AddButton { get; set; }
        [FindsBy(How =How.XPath,Using = "//*[@id=\"BE_Hotel_pax_box\"]/div[1]/div[2]/div[2]/div/div/span[1]")]
        private IWebElement RemoveAdult{ get; set; }
        [FindsBy(How =How.Id,Using = "BE_hotel_htsearch_btn")]
        private IWebElement searchvillasButton { get; set; }

        public void TypeCity(string city)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(10);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Message = "Element not found";
            
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            City.Clear();
            Thread.Sleep(4000);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            City.Click();
            Thread.Sleep(1000);

            //            fluentWait.Equals(City.Text == null);
           // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
            City.SendKeys(city);


            Thread.Sleep(5000);
            //fluentWait.Until(c => City.Displayed);
            //WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000));
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
           // WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            //wait.Until(driver =>City.GetAttribute("value") == "New Delhi");


            City.SendKeys(Keys.Enter);
           
        }

        public void CheckInInput(string date)
        {
            CheckIN?.SendKeys(date);
            //CheckIN?.SendKeys(Keys.Enter);
            
        }
        public void CheckOutInput(string date)
        {
            CheckOut?.SendKeys(date);
            //CheckOut?.SendKeys(Keys.Enter);
            
        }
        public void CheckOutInputLabel(string date)
        {
            CheckOutPara?.SendKeys(date);
            //CheckOut?.SendKeys(Keys.Enter);

        }
        public void CheckInInputLabel(string date)
        {
            CheckINPara?.SendKeys(date);
            //CheckOut?.SendKeys(Keys.Enter);

        }
        public void ClickAddButton()
        {
            AddButton.Click();
        }

        public void ClickRemoveAdultButton() 
        { 
            RemoveAdult.Click();
        }
        public SearchResultPage clickSearchVillas()
        {
            searchvillasButton.Click();
            return new SearchResultPage(driver); ;
        }
        
       


    }
}
