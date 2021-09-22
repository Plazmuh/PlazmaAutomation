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
    public class RequestDemo<TWebDriver> where TWebDriver : IWebDriver, new()
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
        /// Test: Request a demo -
        /// </summary>
        /// <returns></returns>
        [Test, Order(1)]
        public async Task RequestADemoTestAsync()
        {
            // Wait until Demo button is visible, then click it
            ElementUtility.GetElement(Driver, By.CssSelector("#content > div > div > div > section.elementor-section.elementor-top-section.elementor-element.elementor-element-fa2647d.elementor-section-stretched.elementor-section-items-top.blue-bg.elementor-section-full_width.elementor-reverse-mobile.elementor-section-height-min-height.elementor-section-height-default > div > div > div.elementor-column.elementor-col-50.elementor-top-column.elementor-element.elementor-element-235a21a > div > div > div.elementor-element.elementor-element-c3e0e65.elementor-align-left.btn.elementor-mobile-align-center.elementor-widget.elementor-widget-button > div > div > a > span > span"), 60).Click();
            await ElementUtility.PauseAsync(3000).ConfigureAwait(false);

            // Wait for fields to be visible, then send appropriate keys.
            string[] fields = new string[7] 
            {
                "#input_7_1",
                "#input_7_5",
                "#input_7_3",
                "#input_7_2",
                "#input_7_7",
                "#input_7_8",
                "#input_7_4", 
            };
            if (!ElementUtility.GetElements(Driver, fields, 60))
                Assert.Fail("Not all fields are visible in Alkami Request Demo...");

            // Send keys to each of the fields
            ElementUtility.GetElement(Driver, By.CssSelector("#input_7_1"), 60).SendKeys("Raymond");
            ElementUtility.GetElement(Driver, By.CssSelector("#input_7_5"), 60).SendKeys("Dasilva");
            ElementUtility.GetElement(Driver, By.CssSelector("#input_7_3"), 60).SendKeys("raymond.dasilva@outlook.com");
            ElementUtility.GetElement(Driver, By.CssSelector("#input_7_2"), 60).SendKeys("Alkami Automation");
            ElementUtility.GetElement(Driver, By.CssSelector("#input_7_7"), 60).Click();
            ElementUtility.GetElement(Driver, By.CssSelector("#input_7_7 > option:nth-child(4)"), 60).Click();
            ElementUtility.GetElement(Driver, By.CssSelector("#input_7_8"), 60).Click();
            ElementUtility.GetElement(Driver, By.CssSelector("#input_7_8 > option:nth-child(5)"), 60).Click();
            ElementUtility.GetElement(Driver, By.CssSelector("#input_7_4"), 60).SendKeys("Optional Messages are fun!");
            await Task.Delay(8000);
          
            // Click Request a Demo -- Wait for DynamoDB/Event/ServiceBus to send message
            // TODO

            // Test Passed.
            Assert.Pass();
        }

        #endregion Test Case
    }
}