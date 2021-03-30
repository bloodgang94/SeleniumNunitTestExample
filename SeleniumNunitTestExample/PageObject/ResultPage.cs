using OpenQA.Selenium;
using SeleniumNunitTestExample.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNunitTestExample.PageObject
{
    public class ResultPage : BasePage
    {
        public IWebElement HeaderTitle => Browser.FindElement(By.XPath("//h3[@role='heading']"), 10);
    }
}
