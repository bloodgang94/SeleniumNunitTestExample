using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumNunitTestExample.WrapperBrowser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNunitTestExample.Helper
{
    public static class HelpUtil
    {
        /// <summary>
        /// Find an element, waiting until a timeout is reached if necessary.
        /// </summary>
        /// <param name="context">The search context.</param>
        /// <param name="by">Method to find elements.</param>
        /// <param name="timeoutInSeconds">How many seconds to wait.</param>
        /// <param name="displayed">Require the element to be displayed?</param>
        /// <returns>The found element.</returns>
        public static IWebElement FindElement(this ISearchContext context, By by, uint timeoutInSeconds = 30, bool displayed = false)
        {
            var wait = new DefaultWait<ISearchContext>(context)
            {
                Timeout = TimeSpan.FromSeconds(timeoutInSeconds)
            };
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));
            return wait.Until(d => {
                if (displayed && !d.FindElement(by).Displayed)
                    return null;
                return d.FindElement(by);
            });
        }

        public static string TakeScreenshot()
        {
            var path = string.Format("{0}\\{1}", Environment.CurrentDirectory, "Screenshots");
            var filePath = string.Format("{0}\\{1}.png", path, Guid.NewGuid());
            try
            {
                var screen = ((ITakesScreenshot)DriverFactory.DriverStored).GetScreenshot();
                Directory.CreateDirectory(path);
                screen.SaveAsFile(filePath);
                return filePath;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void MoveToElementClick(this IWebElement element, IWebDriver driver)
        {
            var action = new Actions(driver);
            action.MoveToElement(element).Perform();
            action.Click(element).Perform();
        }
    }
}
