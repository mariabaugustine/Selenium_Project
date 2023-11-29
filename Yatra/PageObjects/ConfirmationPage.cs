using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatra.Utilities;

namespace Yatra.PageObjects
{
    internal class ConfirmationPage
    {
        IWebDriver driver;

        public ConfirmationPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How =How.XPath,Using = "//*[@id=\"additionalContactEmail\"]")]
        public IWebElement EmailType {  get; set; }

        [FindsBy(How=How.XPath,Using = "//*[@id=\"paxNum0\"]/div[2]/div[1]/span/select/option[4]")]
        public IWebElement Title { get; set; }
        [FindsBy(How =How.Id,Using = "travellerf0")]
        public IWebElement FirstName { get; set; }

        [FindsBy(How = How.Id, Using = "travellerl0")]
        public IWebElement LastName { get; set; }

        [FindsBy(How = How.Id, Using = "additionalContactMobile")]

        public IWebElement Mobile { get; set; }
        [FindsBy(How =How.XPath,Using = "//*[@id=\"traveller-dom\"]/form/div[4]/button[1]")]
        public IWebElement FinalSubmit { get; set; }

        public void TypeEmail(string email)
        {
            
         EmailType?.SendKeys(email);
        }
        public void TypeTitle()
        {
            Title.Click();
        }
        public void TypeFirstName(string firstname) 
        { 
            FirstName.SendKeys(firstname);
        }
        public void TypeLastName(string lastname) 
        {
            LastName.SendKeys(lastname);
        }
        public void TypeMobile(string mobil)
        {
            Mobile.SendKeys(mobil);
        }
        public FinalPage ClickSubmit()
        {
            FinalSubmit.Click();
            return new FinalPage(driver);
        }


    }
}
