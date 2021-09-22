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
    public class NavigatePage<TWebDriver> where TWebDriver : IWebDriver, new()
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
        /// Test: Go through a few different pages and make sure elements are visible
        /// </summary>
        /// <returns></returns>
        [Test, Order(1)]
        public async Task NavigateAlkamiAsync()
        {
            // Wait for Cookies pop-up to show, then accept. -- This will prevent future click intercepts if not handled
            var elem = ElementUtility.GetElement(Driver, By.CssSelector("#cookie_action_close_header"), 60);            
            elem.Click();
            await ElementUtility.PauseAsync(3000).ConfigureAwait(false);

            // Wait until "See the platform" is visible then click it -- Should go to a new page
            ElementUtility.GetElement(Driver, By.CssSelector("#content > div > div > div > section.elementor-section.elementor-top-section.elementor-element.elementor-element-fb9ecb2.white-bg.home-sect2.elementor-section-boxed.elementor-section-height-default.elementor-section-height-default > div > div > div > div > div > div.elementor-element.elementor-element-22ee045.elementor-align-left.btn.elementor-widget.elementor-widget-global.elementor-global-1996.elementor-widget-button > div > div > a > span > span"), 60).Click();
            await ElementUtility.PauseAsync(3000).ConfigureAwait(false);
                      
            // Test Passed.
            Assert.Pass();
        }

        #endregion Test Case
    }
}