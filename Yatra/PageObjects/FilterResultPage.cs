using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatra.PageObjects
{
    internal class FilterResultPage
    {
        IWebDriver driver;

        public FilterResultPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How =How.XPath,Using = "//*[@id=\"result0\"]/div[1]/div[2]/div/span")]
        public IWebElement ChooseRoomButton {  get; set; }

        public void ClickChooseRoom()
        {
        ChooseRoomButton.Click();
        }
    }
}
