using OpenQA.Selenium;
using System;
using System.Threading.Tasks;

namespace WebAutomation.Helpers.PageObjs
{
    public class HomePage : ElementUtility
    {
        private string alkamiHomePage = "https://www.alkami.com/";
        private string alkamiButton = "body > div.elementor.elementor-297.elementor-location-header > div > section > div > div > div.elementor-column.elementor-col-33.elementor-top-column.elementor-element.elementor-element-04831f1 > div > div > div > div > div > a > img";
        private IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        #region Homepage functions

        /// <summary>
        /// Goes to Alkami's HomePage and wait until an element is visible, confirming it's loaded.
        /// </summary>
        /// <returns>A boolean indicating success</returns>
        public async Task<bool> GoToHomePage()
        {
            try
            {
                // Navigate to Homepage
                driver.Navigate().GoToUrl(alkamiHomePage);
                driver.Manage().Window.Maximize();
                await PauseAsync(300);

                // Make sure there's a button visible before we assume the page has loaded.
                GetElement(driver, By.CssSelector(alkamiButton), 15);
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