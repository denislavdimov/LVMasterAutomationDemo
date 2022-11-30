using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
//using SeleniumExtras.WaitHelpers;
using LVPages.IClasses;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace LVPages
{
    public class Wait : IWait
    {
        protected IWebDriver driver;
        private static int _secondsBeforeTimeout = 30;
        private WebDriverWait wait { get { return new WebDriverWait(driver, TimeSpan.FromSeconds(_secondsBeforeTimeout)); } }
        private IList<IWebElement> Loader =>
            driver.FindElements(By.XPath("//div[@class='lv-loader-container']")).ToList();
        private IList<IWebElement> LoaderBackdrop =>
            driver.FindElements(By.XPath("//div[@class='loader-backdrop']")).ToList();

        public Wait(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ToSeeElement(By by)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (TimeoutException)
            {
                Console.WriteLine($"The element with selector: {by} didn't appear for - {_secondsBeforeTimeout}");
                throw;
            }
        }

        public void ToSeeElements(By by)
        {
            try
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
            }
            catch (TimeoutException)
            {
                Console.WriteLine($"The element with selector: {by} didn't appear for - {_secondsBeforeTimeout}");
                throw;
            }
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
            var element = By.XPath("");
            var elemetn2 = wait.Until(ExpectedConditions.ElementIsVisible(element));
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

        public bool TheElementIsDisplayed(By path)
        {
            try
            {
                return driver.FindElement(path).Displayed;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UntilElementIsNotDisplayed(By by)
        {
            for (int i = 0; i < _secondsBeforeTimeout; i++)
            {
                if (!TheElementIsDisplayed(by))
                {
                    Thread.Sleep(10);
                    return true;
                }
                Thread.Sleep(1000);
            }
            return false;
        }

        public void ForNoLoader()
        {
            //try
            //{
            //    if (!UntilElementIsNotDisplayed(By.XPath("//div[@class='lv-loader-container']")))
            //    {
            //        var loader = driver.FindElement(By.XPath("//div[@class='lv-loader-container']"));
            //        if (loader.Displayed)
            //        {
            //            UntilElementIsNotDisplayed(By.XPath("//div[@class='lv-loader-container']"));
            //            Assert.IsTrue(Exception.Count > 0, "An exception is thrown");
            //            return;
            //        }
            //        else if (!loader.Displayed)
            //        {
            //            Thread.Sleep(800);
            //            UntilElementIsNotDisplayed(By.XPath("//div[@class='lv-loader-container']"));
            //            Assert.IsTrue(Exception.Count > 0, "An exception is thrown");
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        UntilElementIsNotDisplayed(By.XPath("//div[@class='lv-loader-container']"));
            //        Assert.IsTrue(Exception.Count > 0, "An exception is thrown");
            //    }
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("The Loader is not displayed on the page.");
            //    throw;
            //}
        }

        public bool CheckForElement(By by)
        {
            try
            {
                return driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        //public void ForNoErrorAndException()
        //{
        //    try
        //    {
        //        SetTimeout(2);
        //        var Warning = PageHelper.BasePage.Warning;
        //        var Exception = PageHelper.BasePage.Exception;
        //        if (Warning.Count > 0 || Exception.Count > 0)
        //        {
        //            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
        //                By.XPath("//div[contains(@class, 'k-loading-color')]")));
        //            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
        //                By.XPath("//div[@class='k-loading-image']")));
        //            Assert.IsTrue(Exception.Count == 0, "Exception is thrown on the Page");
        //            Assert.IsTrue(Warning.Count == 0, "Warning is thrown on the Page");
        //            ResetTimeoutToDefault();
        //        }
        //        else
        //        {
        //            ResetTimeoutToDefault();
        //            return;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("An exception or warning is thrown on the page.");
        //        throw;
        //    }
        //}

        public void ForNoErrorAndException()
        {
            try
            {
                //SetTimeout(1);
                var WarningIsDisplayed = CheckForElement(By.XPath("//div[contains(@class, 'toast toast-warning')]"));
                var ExceptionIsDisplayed = CheckForElement(By.XPath("//div[@class='toast toast-error']"));
                if (WarningIsDisplayed || ExceptionIsDisplayed)
                {
                    var LoadingIsDisplayed = CheckForElement(By.XPath("//div[@class='k-loading-image']"));
                    if (LoadingIsDisplayed)
                    {
                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='k-loading-image']")));
                        Assert.IsFalse(ExceptionIsDisplayed, "Exception is thrown on the Page");
                        Assert.IsFalse(WarningIsDisplayed, "Warning is thrown on the Page");
                    }
                    else if (!LoadingIsDisplayed)
                    {
                        Thread.Sleep(1000);
                        Assert.IsFalse(ExceptionIsDisplayed, "Exception is thrown on the Page");
                        Assert.IsFalse(WarningIsDisplayed, "Warning is thrown on the Page");
                    }
                }
                else
                {
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='k-loading-image']")));
                    Assert.IsFalse(ExceptionIsDisplayed, "Exception is thrown on the Page");
                    Assert.IsFalse(WarningIsDisplayed, "Warning is thrown on the Page");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Please check. ", e.ToString());
                throw;
            }
            finally
            {
                //ResetTimeoutToDefault();
            }

        }

        public void ForPageToLoad()
        {
            var body = driver.FindElement(By.XPath("//body")).Displayed;
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
                    bool ajaxIsComplete = (bool)((IJavaScriptExecutor)driver).ExecuteScript("return !!window.jQuery && window.jQuery.active == 0");
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
            if (driver.Manage().Timeouts().ImplicitWait.Seconds.Equals(_secondsBeforeTimeout))
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(secondstowait);
            }
            else { return; }
        }
        public void ResetTimeoutToDefault()
        {
            if (!driver.Manage().Timeouts().ImplicitWait.Seconds.Equals(_secondsBeforeTimeout))
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_secondsBeforeTimeout);
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

