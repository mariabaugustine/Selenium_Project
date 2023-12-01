using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatra.Utilities;
using Yatra.PageObjects;

namespace Yatra.TestScripts
{
    internal class CareerTest:CoreCodes
    {
        [Test]
       public void CareerTesting()
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(10);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Message = "Element not found";
            CareerHomePage CHP = new CareerHomePage(driver);
            ScrollIntoView(driver, driver.FindElement(By.XPath("//*[@id=\"js_yt_footer\"]/div/ul/li[3]/a/span")));
            var careerpage = CHP.ClickCareers();
            Assert.That(driver.Url.Contains("career"));


            ScrollIntoView(driver, driver.FindElement(By.XPath("//a[text()='Explore Jobs Opening']")));
            var careermain=careerpage.ClickCareerButton();
            Assert.That(driver.Url.Contains("job-portal"));

            var joblist = careermain.ClickChooseCourse();
            careermain.ClickApplyButton();
            Thread.Sleep(10000);
        }
    }
}
