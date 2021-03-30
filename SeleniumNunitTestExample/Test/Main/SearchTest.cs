using NUnit.Framework;
using SeleniumNunitTestExample.PageObject;
using SeleniumNunitTestExample.Test.TestCase;
using SeleniumNunitTestExample.WrapperBrowser;

namespace SeleniumNunitTestExample.Test.Main
{
    [TestFixture(BrowserTypes.Chrome)]
    [TestFixture(BrowserTypes.Firefox)]
    [TestFixture(BrowserTypes.Opera)]
    class SearchTest : BaseTest
    {
        public SearchTest(BrowserTypes browserTypes)
            : base(browserTypes) { }

        [TestCaseSource(typeof(SearchTestCase), nameof(SearchTestCase.TestCases))]
        [Test, Description("Display check section header")]
        public void SearchByTest(string textSearch)
        {
            var resultPage = new HomePage().Search(textSearch);
            Assert.That(resultPage.HeaderTitle.Text, Does.Contain(textSearch));
        }
    }
}