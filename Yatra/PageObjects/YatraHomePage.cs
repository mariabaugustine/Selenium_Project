using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatra.PageObjects
{
    internal class YatraHomePage
    {
        IWebDriver driver;

        public YatraHomePage(IWebDriver driver)
        {
            this.driver = driver??throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How =How.Id,Using = "booking_engine_homestays")]
        private IWebElement villasIcon {  get; set; }

        [FindsBy(How=How.XPath,Using = "//div//a[@class='logo']")]
        private IWebElement Logo {  get; set; }

        public void LogoCheck()
        {
            Logo.Click();
        }
        public VillasBookingPage ClickVillasIcon()
        {
            villasIcon.Click();
            return new VillasBookingPage(driver);
        }
    }
}
