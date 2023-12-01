using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatra.PageObjects
{
    internal class SearchJob
    {
        IWebDriver driver;

        public SearchJob(IWebDriver driver)
        {
            this.driver = driver;
            //pag
        }
    }
}
