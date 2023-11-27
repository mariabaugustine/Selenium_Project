using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatra.PageObjects;
using Yatra.Utilities;

namespace Yatra.TestScripts
{
    internal class VillasBookingTests:CoreCodes
    {
        [Test]
        public void TestvillasBooking()
        {
            var yatraHP = new YatraHomePage(driver);
            yatraHP.ClickVillasIcon();
            Assert.AreEqual("https://www.yatra.com/homestays", driver.Url);
        }
    }
}
