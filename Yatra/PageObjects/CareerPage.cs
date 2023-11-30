using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatra.PageObjects
{
    internal class CareerPage
    {
        IWebDriver driver;

        public CareerPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//a[text()='Explore Jobs Opening']")]
        public IWebElement CareerButton { get; set; }


        public CareerMainPage ClickCareerButton()
        {
            CareerButton.Click();
            return new CareerMainPage(driver);
        }
    }
}
