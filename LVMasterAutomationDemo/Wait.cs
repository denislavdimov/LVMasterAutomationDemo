using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using LVPages.IClasses;

namespace LVPages
{
    public class Wait : IWait
    {
        protected IWebDriver _driver;
        private static int _secondsBeforeTimeout = 30;
        private WebDriverWait wait { get { return new WebDriverWait(_driver, TimeSpan.FromSeconds(_secondsBeforeTimeout)); } }
        private IList<IWebElement> Loader =>
            _driver.FindElements(By.XPath("//div[@class='lv-loader-container']")).ToList();
        private IList<IWebElement> LoaderBackdrop =>
            _driver.FindElements(By.XPath("//div[@class='loader-backdrop']")).ToList();
        public Wait(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ForElementToBeClickable(IWebElement element)
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

        public void ForLoaderToDissaper()
        {
            SetTimeout(1);
            var NewAdminLoader = Loader;
            var NewAdminLoaderBackdrop = LoaderBackdrop;
            int elapsed = 0;
            int timeout = 20000;
            bool stop1 = false;
            bool stop2 = false;
            try
            {
                if (Loader.Count > 0 || LoaderBackdrop.Count > 0)
                {
                    while ((!stop1) && (!stop2) && (elapsed <= timeout))
                    {
                        Thread.Sleep(1000);
                        elapsed += 1000;
                        stop1 = Loader.Count == 0;
                        stop2 = LoaderBackdrop.Count == 0;
                    }
                }
                else
                {
                    Thread.Sleep(2000);
                    if (Loader.Count > 0 || LoaderBackdrop.Count > 0)
                    {
                        while ((!stop1) && (!stop2) && (elapsed <= timeout))
                        {
                            Thread.Sleep(1000);
                            elapsed += 1000;
                            stop1 = Loader.Count == 0;
                            stop2 = LoaderBackdrop.Count == 0;
                        }
                    }
                    return;
                }
            }
            finally
            {
                ResetTimeoutToDefault();
                Assert.IsTrue(Loader.Count == 0 && LoaderBackdrop.Count == 0, "Timeout exception. Please check.");
            }
        }

        public void ForPageToLoad()
        {
            var body = _driver.FindElement(By.XPath("//body")).Displayed;
            try
            {
                if (body != true)
                {
                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//body")));
                }
                return;
            }
            catch (TimeoutException te)
            {
                Assert.Fail($"Page failed to load. The exception was:\n {te}", te.ToString());
            }
        }

        public void ForAjax()
        {
            try
            {
                while (true)
                {
                    //bool ajaxIsComplete = (bool)((IJavaScriptExecutor)_driver).ExecuteScript("return jQuery.active == 0");
                    bool ajaxIsComplete = (bool)((IJavaScriptExecutor)_driver).ExecuteScript("return !!window.jQuery && window.jQuery.active == 0");
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
            if (!_driver.Manage().Timeouts().ImplicitWait.Seconds.Equals(_secondsBeforeTimeout))
            {
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_secondsBeforeTimeout);
            }
            else { return; }
        }

        public void ForItemInTheGrid(int Item, int NumberOfItems)
        {
            bool success = false;
            int elapsed = 0;
            try
            {
                while ((!success) && (elapsed < 10000))
                {
                    Thread.Sleep(1000);
                    elapsed += 1000;
                    success = Item == NumberOfItems;
                }
            }
            finally
            {
                Assert.That(Item, Is.EqualTo(NumberOfItems), $"There is not only {NumberOfItems} item in the grid.Please check");
            }
        }
    }
}

