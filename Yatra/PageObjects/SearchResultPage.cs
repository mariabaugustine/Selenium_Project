using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Yatra.Utilities;

namespace Yatra.PageObjects
{
    internal class SearchResultPage
    {
        IWebDriver driver;

        public SearchResultPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        //[FindsBy(How =How.XPath,Using = "(//label[@class='filter-label ng-binding'])[8]")]

        [FindsBy(How =How.Id,Using = "hotelFilterInput")]
        private IWebElement FilterByPropertyType { get; set; }
        public FilterResultPage ClickFilterPropertyType(string name)
        {
            FilterByPropertyType.SendKeys(name);
            FilterByPropertyType.SendKeys(Keys.Enter);

            return new FilterResultPage(driver);
        }

    }
}
