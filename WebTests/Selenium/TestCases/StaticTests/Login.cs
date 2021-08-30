using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
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
    public class Login<TWebDriver> where TWebDriver : IWebDriver, new()
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
        /// Test: Attempt signing in through UI.
        /// </summary>
        /// <returns></returns>
        [Test, Order(1)]
        public async Task CustomerSignInTestAsync()
        {

            // Wait until elements are visible then click it
            ElementUtility.GetElement(Driver, By.CssSelector("#gatsby-focus-wrapper > div.Header-outer--NMN0k > div > div > div.Container-container--3LGAe.Header-wrapper--rEEKq.Header-hideOnSmartphone--3LTpT > a:nth-child(4) > span"), 60).Click();
            await Task.Delay(3000);

            // Wait for Mobile Sign-in number to be present -- Then send random mobile number
            string mobileSignInNum = "1234567890";
            var mobileNumberBox = ElementUtility.GetElement(Driver, By.CssSelector("#Identity-container > div > div > div.pro1M70H9P9.proqW_lcgAc > form > div.pro1M70H9P9.pro1zXd09Vc > div > div > input"), 60);
            mobileNumberBox.SendKeys(mobileSignInNum);            

            // Click Continue -- Make sure it's valid and we don't have a popup
            ElementUtility.GetElement(Driver, By.CssSelector("#Identity-container > div > div > div.pro1M70H9P9.proqW_lcgAc > form > div:nth-child(3) > button"), 60).Click();
            await Task.Delay(1000);

            if (ElementUtility.GetElement(Driver, By.CssSelector("#Identity-container > div > div > div.pro1M70H9P9.proqW_lcgAc > div.pro3PQmJOfN.proPEJz23Bn > strong > p"), 60).Displayed)
                Assert.Fail("Looks like we don't have a valid test sign-in Number.");

            // We just texted you Popup -- continue with Login Auth.
            // TODO

            // Test Passed.
            Assert.Pass();
        }        

        #endregion Test Case
    }
}