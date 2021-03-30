using OpenQA.Selenium;
using SeleniumNunitTestExample.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNunitTestExample.PageObject
{
    public class HomePage : BasePage
    {
        private IWebElement SearchInput => Browser.FindElement(By.Name("q"), 5);

        public ResultPage Search(string text)
        {
            SearchInput.SendKeys(text);
            SearchInput.Submit();
            return new ResultPage();
        }

    }
}
