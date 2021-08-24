using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace WebAutomation.Helpers
{
    public class ElementUtility
    {
        private const int TIMEOUT = 1;

        public enum LocationTag
        {
            CssSelector = 0,
            Xpath = 1,
            Id = 2,
            Name = 3,
            LinkText = 4
        }

        /// <summary>
        /// Get element By Enum
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="cssSelector"></param>
        /// <param name="Timeout"></param>
        /// <returns></returns>
        public static IWebElement GetElement(IWebDriver driver, LocationTag tag, string value, int Timeout = TIMEOUT)
        {
            var element = driver.FindElement(By.CssSelector(value));

            DateTime start = DateTime.Now;
            while (DateTime.Now.Subtract(start).Minutes < Timeout && element == null || (element != null && !element.Displayed))
            {
                switch (tag)
                {
                    case LocationTag.CssSelector:
                        element = driver.FindElement(By.CssSelector(value));
                        break;

                    case LocationTag.Xpath:
                        element = driver.FindElement(By.XPath(value));
                        break;

                    case LocationTag.Id:
                        element = driver.FindElement(By.Id(value));
                        break;

                    case LocationTag.Name:
                        element = driver.FindElement(By.Name(value));
                        break;

                    case LocationTag.LinkText:
                        element = driver.FindElement(By.LinkText(value));
                        break;

                    default:
                        element = driver.FindElement(By.CssSelector(value));
                        break;
                }
            }

            if (element == null)
                Assert.Fail($"Element was not found after {TIMEOUT} seconds");

            return element;
        }
    }
}