using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAutomation.Helpers
{
    public class ElementUtility
    {
        private const int TIMEOUT = 1;

        /// <summary>
        /// Get element By Enum
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="cssSelector"></param>
        /// <param name="Timeout"></param>
        /// <returns>IWebElement of the visible element</returns>
        public static IWebElement GetElement(IWebDriver driver, By locator, int Timeout = TIMEOUT)
        {            
            var element = driver.FindElement(locator);

            DateTime start = DateTime.Now;
            while (DateTime.Now.Subtract(start).Minutes < Timeout && element == null || (element != null && !element.Displayed))
            {
                element = driver.FindElement(locator);
            }

            if (element == null)
                throw new ArgumentException($"Element was not found after {TIMEOUT} seconds");

            return element;
        }

        /// <summary>
        /// Makes sure all elements are at least visible before performing actions on them
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="cssSelector"></param>
        /// <param name="Timeout"></param>
        /// <returns>A value indicating if all elements are found</returns>
        public static bool GetElements(IWebDriver driver, string[] locators, int Timeout = TIMEOUT)
        {
            if (driver == null || locators.Length == 0)
                return false;

            foreach (var field in locators)
            {
                if (!string.IsNullOrEmpty(field))
                {
                    var element = driver.FindElement(By.CssSelector(field));

                    DateTime start = DateTime.Now;
                    while (DateTime.Now.Subtract(start).Minutes < Timeout && element == null || (element != null && !element.Displayed))
                        element = driver.FindElement(By.CssSelector(field));

                    if (element == null)
                        return false;
                }
                else
                    return false;
            }            

            return true;
        }

        #region Processors

        /// <summary>
        /// Will check any given uri, and ensure it's valid
        /// </summary>
        /// <param name="url"></param>
        /// <returns>A bool indicating success or failure of Uri</returns>
        public bool IsValidUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                Uri uriResult;
                bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                return result;
            }
            else
                return false;
        }

        /// <summary>
        /// Will cause an asynchronous await based on miliseconds
        /// </summary>
        /// <param name="miliseconds"></param>
        /// <returns></returns>
        public static async Task PauseAsync(int miliseconds)
        {
            await Task.Delay(miliseconds).ConfigureAwait(false);
        }

        /// <summary>
        /// Tear down any driver
        /// </summary>
        public static async Task TearDown(IWebDriver driver)
        {
            driver.Quit();
            await PauseAsync(100);
        }

        /// <summary>
        /// Function gets the browsers we will be using for parallel testing from "AutomationSettings.resx"
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> BrowsersToRunWith()
        {
            string[] browsers = AutomationSettings.browsers.Split(',');
            foreach (string browser in browsers)
                yield return browser;
        }

        #endregion Processors
    }
}