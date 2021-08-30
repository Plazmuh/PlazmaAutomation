using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System;
using System.Threading.Tasks;
using WebAutomation.Helpers;

namespace WebAutomation.Helpers.PageObjs
{
    public class HomePage : ElementUtility
    {
        private string affirmHomePage = "https://www.affirm.com/";
        private string loginButton = "#gatsby-focus-wrapper > div.Header-outer--NMN0k > div > div > div.Container-container--3LGAe.Header-wrapper--rEEKq.Header-hideOnSmartphone--3LTpT > a:nth-child(4) > span";
        private IWebDriver driver;

        public HomePage(IWebDriver driver)
        {            
            this.driver = driver;
        }        

        #region Homepage functions

        /// <summary>
        /// Goes to Affirm's HomePage and wait until an element is visible, confirming it's loaded.
        /// </summary>
        /// <returns>A boolean indicating success</returns>      
        public async Task<bool> GoToHomePage()
        {
            try
            {
                // Navigate to Homepage
                driver.Navigate().GoToUrl(affirmHomePage);
                driver.Manage().Window.Maximize();
                await PauseAsync(300);

                // Make sure there's a button visible before we assume the page has loaded.
                GetElement(driver, By.CssSelector(loginButton), 15);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }                     
        }                

        #endregion Homepage functions
    }
}