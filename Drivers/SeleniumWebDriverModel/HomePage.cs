using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WebAutomation.Drivers.SeleniumWebDriverModel
{
    public class HomePage
    {
        protected IWebDriver Driver;

        public HomePage()
        {
        }

        public HomePage(IWebDriver driver)
        {
            Driver = driver;
        }

        #region Homepage functions

        //Start Chrome and goto URL.
        public async Task<bool> BeforeTestAsync(string homePageUrl)
        {
            // Make sure this is a valid url for HTTP and HTTPS
            if (!IsValidUrl(homePageUrl))
                return false;

            try
            {
                // Navigate to Homepage
                Driver.Navigate().GoToUrl(homePageUrl);
                Driver.Manage().Window.Maximize();
                await Task.Delay(300);
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error going to HomePage: {exception.Message}");
                await Task.Delay(300);
                throw new Exception(exception.Message);
            }
        }

        public void TearDown()
        {
            Driver.Close();
        }

        #endregion Homepage functions

        #region Processors

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

        #endregion Processors
    }
}