using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatra.PageObjects
{
    internal class CareerHomePage
    {
        IWebDriver driver;

        public CareerHomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How=How.XPath,Using = "//a[span[text()='Careers']]")] 
        public IWebElement CareerLink { get; set; }
        public CareerPage ClickCareers()
        {
            CareerLink.Click();
            return new CareerPage(driver);
        }
    }
}
