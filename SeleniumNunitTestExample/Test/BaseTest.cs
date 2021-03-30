using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using SeleniumNunitTestExample.Settings;
using SeleniumNunitTestExample.WrapperBrowser;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using SeleniumNunitTestExample.Helper;

namespace SeleniumNunitTestExample.Test
{
    class BaseTest
    {
        protected IOptions<ConnectionStrings> appSettings;
        protected Stopwatch stopwatch;
        readonly BrowserTypes _browserTypes;

        public BaseTest(BrowserTypes browserTypes)
        {
            _browserTypes = browserTypes;
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            appSettings = server.Host.Services.GetService<IOptions<ConnectionStrings>>();
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        [SetUp]
        public void SetUp()
        {
            DriverFactory.InitDriver(_browserTypes, appSettings.Value.SelenoidUrl);
            DriverFactory.DriverStored.Url = appSettings.Value.SiteUrl;
            DriverFactory.DriverStored.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            HelpUtil.TakeScreenshot();
            DriverFactory.DriverStored.Quit();
            Trace.WriteLine($"Elapsed test : {stopwatch.Elapsed.TotalSeconds}s");
        }

    }
}
