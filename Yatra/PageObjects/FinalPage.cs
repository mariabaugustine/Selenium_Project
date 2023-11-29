using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatra.PageObjects
{
    internal class FinalPage
    {
        IWebDriver driver;

        public FinalPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How=How.Id,Using =("paytm"))]
         public IWebElement Paytm {  get; set; }
        [FindsBy(How=How.Id,Using = "payNow")]
        public IWebElement PayNow { get; set; }
        public void PaytmClick()
        {
            Paytm.Click();
        }
        public void PayNowClick()
        { 
            PayNow.Click();
        }
    }
}
