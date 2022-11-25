using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.DevTools.V105.Debugger;

namespace LVPages
{
    public class Wait : IWait
    {
        protected IWebDriver _driver;
        private static int _secondsBeforeTimeout = 30;
        private WebDriverWait wait { get { return new WebDriverWait(_driver, TimeSpan.FromSeconds(_secondsBeforeTimeout)); } }

        public Wait(IWebDriver driver)
        {
            _driver = driver;
        }

        public void IWaitForElementToBeClickable(IWebElement element)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(element));
            }
            catch (NoSuchElementException te)
            {
                Assert.Fail($"The element - {element} didn't appear. The exception was:\n {te}", element, te.ToString());
            }
        }

        private bool IWaitForLoader()
        {
            try
            {
                wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='k-loading-image']")));
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Loader did not appear");
                throw;
            }
        }

        public void IWaitForLoaderToDissaper(int seconds)
        {
            SetTimeout(seconds);
            try
            {
                if (IWaitForLoader() != true)
                {
                    Assert.Fail("Loader did not appear");
                }
                else
                {
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='k-loading-image']")));
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message.ToString());
            }
            ResetTimeoutToDefault();
        }

        public void IWaitPageToLoad()
        {
            try
            {
                for (int i = 0; i < _secondsBeforeTimeout; i++)
                {
                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//body")));
                }
            }
            catch (TimeoutException te)
            {
                Assert.Fail($"Page failed to load. The exception was:\n {te}", te.ToString());
            }
        }

        public void WaitForAjax()
        {
            try
            {
                while (true)
                {
                    bool ajaxIsComplete = (bool)((IJavaScriptExecutor)_driver).ExecuteScript("return jQuery.active == 0");
                    if (ajaxIsComplete)
                    {
                        break;
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Ajax failed to complete");
                throw;
            }
        }

        public void SetTimeout(int secondstowait)
        {
            if (_driver.Manage().Timeouts().ImplicitWait.Seconds.Equals(_secondsBeforeTimeout))
            {
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(secondstowait);
            }
            else { return; }
        }
        public void ResetTimeoutToDefault()
        {
            if (!_driver.Manage().Timeouts().ImplicitWait.Seconds.Equals(30))
            {
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_secondsBeforeTimeout);
            }
            else { return; }
        }

        public void WaitForOneItemInTheGrid(int item)
        {
            bool success = false;
            int elapsed = 0;
            try
            {
                while ((!success) && (elapsed < 10000))
                {
                    Thread.Sleep(1000);
                    elapsed += 1000;
                    success = item == 1;
                }
            }
            finally 
            {
                Assert.That(item, Is.EqualTo(1), "There is not only one item in the grid.Please check");
            }
        }
    }
}

