using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private IWebElement City { get; set; }

        [FindsBy(How=How.XPath,Using = "//input[@id='BE_hotel_checkin_date']//following::p[1]")]
        public IWebElement CheckIN { get; set; }

        [FindsBy(How=How.XPath,Using = "//input[@id='BE_hotel_checkout_date']//following::p[1]")]
        public IWebElement checkOut{ get; set;}
        [FindsBy(How =How.XPath,Using = "//*[@id=\"BE_Hotel_pax_info\"]/i")]
        private IWebElement AddButton { get; set; }
        [FindsBy(How =How.XPath,Using = "//*[@id=\"BE_Hotel_pax_box\"]/div[1]/div[2]/div[2]/div/div/span[1]")]
        private IWebElement RemoveAdult{ get; set; }
        [FindsBy(How =How.Id,Using = "BE_hotel_htsearch_btn")]
        private IWebElement searchvillasButton { get; set; }

        public void TypeCity(string city)
        {
            
            
                City.Click();
                Thread.Sleep(1000);
                City.SendKeys(city);
                City.SendKeys(Keys.Enter);
           
        }

        public void CheckInInput(string date)
        {
            CheckIN?.SendKeys(date);
            CheckIN?.SendKeys(Keys.Enter);  
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
            return new SearchResultPage(); ;
        }
       


    }
}
