using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatra.PageObjects
{
    internal class CareerMainPage
    {
        IWebDriver driver;

        public CareerMainPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How =How.XPath,Using = "//div[@class='job-tab']//child::li[3]")]
        private IWebElement ChooseCourse { get; set; }
        [FindsBy(How =How.XPath,Using = "//*[@id=\"Technology-Bangalore\"]/div[1]/span/a")]
        private IWebElement ClickApply { get; set; }

        public SearchJob ClickChooseCourse()
        {
            ChooseCourse.Click();
            return new SearchJob(driver);
        }
        public void ClickApplyButton()
        {
            ClickApply.Click();
        }
    }
}
