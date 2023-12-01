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
        [FindsBy(How=How.Id,Using = "First_name")]
        private IWebElement Name { get; set; }
        [FindsBy(How =How.Id,Using = "address")]
        private IWebElement City { get; set; }
        [FindsBy(How=How.Id,Using = "email")]
        private IWebElement Email {  get; set; }
        [FindsBy(How=How.Id,Using = "phone")]
        private IWebElement Mobile { get; set; }
        [FindsBy(How=How.Id,Using = "linkedin")]
        private IWebElement LinkedIn { get; set; }
        public void ClickChooseCourse()
        {
            ChooseCourse.Click();
            
        }
        public void ClickApplyButton()
        {
            ClickApply.Click();
        }

        public void ClickName(string name)
        { 
            Name.SendKeys(name);
        }
        public void ClickCity(string city) 
        { 
            City.SendKeys(city);
        }
        public void ClickEmail(string email) 
        { 
            Email.SendKeys(email);
        }
        public void ClickMobile(string mobile)
        {
            Mobile.SendKeys(mobile);
        }
        public void ClickLinkedIn(string linkedIn)
        {
            LinkedIn.SendKeys(linkedIn);
        }
        public void ApplyJob(string name, string email, string mob, string linkedin, string city)
        {
            Name.SendKeys(name);
            Email.SendKeys(email);
            Mobile.SendKeys(mob);
            LinkedIn.SendKeys(linkedin);
            City.SendKeys(city);
        }
    }
}
