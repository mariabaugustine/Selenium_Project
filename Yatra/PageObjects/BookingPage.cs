using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatra.PageObjects
{
    internal class BookingPage
    {
        IWebDriver driver;

        public BookingPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        //[FindsBy(How = How.XPath, Using = "//*[@id=\"roomWrapper0001825107\"]/div[2]/div[5]/button")]
        [FindsBy(How =How.XPath,Using = "//*[@id=\"roomWrapper0001823921\"]/div[2]/div[5]/button")]
        public IWebElement BookNowButton { get; set; }

        public ConfirmationPage ClickBookNowButton()
        {
            Thread.Sleep(1000);
            BookNowButton.Click();
            return new ConfirmationPage(driver);
        }
    }
}
