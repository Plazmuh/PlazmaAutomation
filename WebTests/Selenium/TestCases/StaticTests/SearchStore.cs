using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading.Tasks;
using WebAutomation.Helpers;
using WebAutomation.Helpers.PageObjs;

namespace WebAutomation.WebTests.Selenium.TestCases.StaticTests
{
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]
    [Parallelizable(ParallelScope.Fixtures)]
    [Author("Raymond Dasilva", "raymond.dasilva@outlook.com")]
    public class SearchStore<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private IWebDriver Driver;

        #region Test Case

        [OneTimeSetUp]
        public void CreateDriver()
        {
            if (typeof(TWebDriver).Name == "ChromeDriver")
                Driver = new ChromeDriver();
            else
                Driver = new TWebDriver();
        }

        /// <summary>
        /// Initializes Driver, and heads to desired Homepage for Testing
        /// </summary>
        /// <returns></returns>
        [SetUp]
        public async Task SetUpTestAsync()
        {
            try
            {
                var Page = new HomePage(Driver);
                await Page.GoToHomePage().ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                Assert.Fail($"Failed going to Home Page... Error: {exception.Message}");
            }
        }

        /// <summary>
        /// Tear the Driver down once done
        /// </summary>
        /// <returns></returns>
        [TearDown]
        public async Task TearDownTestAsync()
        {
            await ElementUtility.TearDown(Driver);
        }

        /// <summary>
        /// Test: Attempt Searching for a specific store, and opening it.
        /// </summary>
        /// <returns></returns>
        [Test, Order(1)]
        public async Task SearchForStoreAsync()
        {
            // Wait until search bar are visible then click it
            var searchBar = ElementUtility.GetElement(Driver, By.CssSelector("#SearchBar"), 60);
            searchBar.Click();
            await ElementUtility.PauseAsync(1000);

            // Click search bar and type in desired store (Best Buy)
            string store = "Best Buy";
            searchBar.SendKeys(store);
            await ElementUtility.PauseAsync(1000);

            // Make sure BestBuy is in the List -- And click it.
            int listNumber = int.Parse(ElementUtility.GetElement(Driver, By.CssSelector("#searchContainer > div.Search-searchResultsDropdown--rr2yn > div > span.Search-searchResultsDropdown__title__count--1U8YM"), 90).Text);
            if (listNumber != 1)
                Assert.Fail("Best Buy was not populated in the list.");

            ElementUtility.GetElement(Driver, By.CssSelector("#search-result-WGUM9CGP0OK9R3CL"), 60).Click();
            await ElementUtility.PauseAsync(1000);

            // Press Shop now -- Should open a new Tab.
            ElementUtility.GetElement(Driver, By.CssSelector("#maincontent > div.Modal-modal--2164I.Modal-modal__open--39ICx > div.Modal-modal__wrapper--e1FXM > div > div.MerchantDetailsPage-buttons__container--3Vla9 > div > a > span")).Click();
            await ElementUtility.PauseAsync(5000);

            // Test Passed.
            Assert.Pass();
        }

        #endregion Test Case
    }
}