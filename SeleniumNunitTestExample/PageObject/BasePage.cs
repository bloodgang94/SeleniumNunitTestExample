using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SeleniumNunitTestExample.WrapperBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNunitTestExample.PageObject
{
    public class BasePage
    {
        public IWebDriver Browser { get; }
        public IWebElement Title => Browser.FindElement(By.TagName("Title"));
        public SessionId SessionID => ((RemoteWebDriver)Browser).SessionId;
        public BasePage()
        {
            Browser = DriverFactory.DriverStored;
        }
    }
}
