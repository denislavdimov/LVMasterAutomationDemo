using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.DevTools.V104.Debugger;
using NUnit.Framework.Internal;

namespace LVMasterAutomationDemo.Pages
{
    public class Wait : IWait
    {
        protected IWebDriver _driver;
        public static int _secondsBeforeTimeout = 30;
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
            //var LoaderOldAdmin = wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='k-loading-image']"))); return true;
            //var LoaderNewAdmin = wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='lv-loader-container']"))); return true;
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

        public void IWaitForLoaderToDissaper()
        {
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
    }
}

