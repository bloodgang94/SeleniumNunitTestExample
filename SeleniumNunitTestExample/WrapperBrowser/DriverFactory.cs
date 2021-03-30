using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using System;
using System.Text;
using System.Threading;

namespace SeleniumNunitTestExample.WrapperBrowser
{
    public enum BrowserTypes
    {
        Firefox,
        Chrome,
        Opera
    }
    public class DriverFactory
    {
        private readonly static ThreadLocal<IWebDriver> storedDriver = new ThreadLocal<IWebDriver>();
        public static IWebDriver DriverStored
        {
            get => storedDriver.Value ?? throw new ArgumentException("WebDriver not initialized");
            set => storedDriver.Value = value;
        }
        public static string DownloadPath => Environment.CurrentDirectory;
        public static string ApiUrl { get; private set; }
       
        public static void InitDriver(BrowserTypes browserTypes, string hub,  int implicitWait = 0)
        {
            ApiUrl = hub.Remove(hub.Length - 7);
            var options = SetOptions(browserTypes);
            DriverStored = new RemoteWebDriver(new Uri(hub), options);
            DriverStored.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait);
        }

        private static DriverOptions SetOptions(BrowserTypes browserTypes)
        {
            DriverOptions options = null;
            return browserTypes switch
            {
                BrowserTypes.Chrome => ((Func<DriverOptions>)(() => {
                    options = new ChromeOptions();
                    //((ChromeOptions)options).AddExtension(Environment.CurrentDirectory + "\\CryptoProExtension.crx");
                    ((ChromeOptions)options).AddAdditionalCapability("enableVNC", true, true);
                    ((ChromeOptions)options).AddArguments("--ignore-ssl-errors=yes", "--ignore-certificate-errors", "--start-maximized");
                    ((ChromeOptions)options).AddArgument("--profile-directory=Default");
                    ((ChromeOptions)options).AddArgument("--disable-notifications");
                    return options;
                }))(),

                BrowserTypes.Firefox => ((Func<DriverOptions>)(() => {
                    options = new FirefoxOptions();
                    FirefoxProfile ffprofile = new FirefoxProfile()
                    {
                        AcceptUntrustedCertificates = true,
                        AssumeUntrustedCertificateIssuer = false,

                    };
                    ffprofile.SetPreference("browser.download.folderList", 1);
                    ffprofile.SetPreference("browser.download.panel.shown", false);
                    ffprofile.SetPreference("browser.download.dir", "/c:/home/selenium/Downloads");
                    ffprofile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf, " +
                        "application/zip, application/pdf, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                    ((FirefoxOptions)options).Profile = ffprofile;
                    ((FirefoxOptions)options).AddAdditionalCapability("enableVNC", true, true);
                    options.AcceptInsecureCertificates = true;
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    return options;
                }))(),

                BrowserTypes.Opera => ((Func<DriverOptions>)(() => {
                    options = new OperaOptions();
                    ((OperaOptions)options).AddAdditionalCapability("enableVNC", true, true);
                    ((OperaOptions)options).AcceptInsecureCertificates = true;
                    return options;
                }))(),

                _ => throw new NotSupportedException()
            };

        }
    }
}
