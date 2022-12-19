using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using LVPages.IClasses;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace LVPages
{
    public class Wait : IWait
    {
        private IWebDriver driver;
        private static int _secondsBeforeTimeout = 35;

        public Wait(IWebDriver driver)
        {
            this.driver = driver;
        }

        private WebDriverWait wait => new WebDriverWait(driver, TimeSpan.FromSeconds(_secondsBeforeTimeout));

        public IList<IWebElement> NewAdminException =>
            driver.FindElements(By.XPath("//div[contains(@class, 'Toastify__toast--error')]")).ToList();

        public void ForElement(By by)
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

        public void ForElements(By by)
        {
            try
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
            }
            catch (TimeoutException)
            {
                Console.WriteLine($"The elements with selector: {by} didn't appear for - {_secondsBeforeTimeout}");
                throw;
            }
        }

        public void ForNoElement(By by)
        {
            try
            {
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
            }
            catch (Exception)
            {
                Console.WriteLine($"The element with selector: {by} is visible on the page.");
                throw;
            }
        }

        public bool ToSee(By by)
        {
            try
            {
                return driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                throw;
            }
        }

        public void ForElementToBeClickable(IWebElement element)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(element));
            }
            catch (TimeoutException)
            {
                Console.WriteLine($"The element: {element} was not ready to be clicked for - {_secondsBeforeTimeout}");
                throw;
            }
        }

        public void ForTheLoader()
        {
            try
            {
                var NewAdminLoaderIsDisplayed = IsElementDisplayed(By.XPath("//div[@class='lv-loader-container']"));
                var NewAdminLoaderBackdropIsDisplayed = IsElementDisplayed(By.XPath("//div[@class='loader-backdrop']"));
                if (NewAdminLoaderIsDisplayed || NewAdminLoaderBackdropIsDisplayed)
                {
                    if (NewAdminLoaderIsDisplayed)
                    {
                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='lv-loader-container']")));
                        Assert.IsTrue(NewAdminException.Count == 0, "An exception is thrown");
                    }
                    else if (NewAdminLoaderBackdropIsDisplayed)
                    {
                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='loader-backdrop']")));
                        Assert.IsTrue(NewAdminException.Count == 0, "An exception is thrown");
                    }
                    else
                    {
                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='lv-loader-container']")));
                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='loader-backdrop']")));
                        Assert.IsTrue(NewAdminException.Count == 0, "An exception is thrown");
                        return;
                    }
                }
                else
                {
                    Thread.Sleep(500);
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='lv-loader-container']")));
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='loader-backdrop']")));
                    Assert.IsTrue(NewAdminException.Count == 0, "An exception is thrown");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Please check. ", e.Message);
                throw;
            }   
        }

        public bool IsElementDisplayed(By by)
        {
            try
            {
                return driver.FindElement(by).Displayed;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void ForNoErrorAndException()
        {
            try
            {
                var WarningIsDisplayed = IsElementDisplayed(By.XPath("//div[contains(@class, 'toast toast-warning')]"));
                var ExceptionIsDisplayed = IsElementDisplayed(By.XPath("//div[@class='toast toast-error']"));
                if (WarningIsDisplayed || ExceptionIsDisplayed)
                {
                    var LoadingIsDisplayed = IsElementDisplayed(By.XPath("//div[@class='k-loading-image']"));
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
                Console.WriteLine("Please check. ", e.Message);
                throw;
            }
        }

        public void ForPageToLoad()
        {
            try
            {
                var body = IsElementDisplayed(By.XPath("//body"));
                if (body != true)
                {
                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//body")));
                }
                return;
            }
            catch (TimeoutException)
            {
                Console.WriteLine($"The element body of the page didn't load for - {_secondsBeforeTimeout}");
                throw;
            }
        }

        public void ForAjax()
        {
            //bool ajaxIsComplete = (bool)((IJavaScriptExecutor)driver).ExecuteScript
            //    ("return !!window.jQuery && window.jQuery.active == 0");
            try
            {
                int toTimeout = 0;
                int timeBeforeTimeout = 30000;
                bool isReady = (bool)((IJavaScriptExecutor)driver).ExecuteScript
                    ("return (window.jQuery != null) && (jQuery.active === 0);");
                while ((!isReady) && (toTimeout < timeBeforeTimeout))
                {
                    var jQueryIsNotDefined = (bool)((IJavaScriptExecutor)driver).ExecuteScript("return typeof jQuery === 'undefined'");
                    if (jQueryIsNotDefined)
                    {
                        wait.Until(driver => (bool)((IJavaScriptExecutor)driver).ExecuteScript("return (document.readyState === 'complete')"));
                        break;
                    }
                    Thread.Sleep(500);
                    if (!isReady)
                    {
                        wait.Until(driver => (bool)((IJavaScriptExecutor)driver).ExecuteScript
                        ("return (window.jQuery != null) && (jQuery.active === 0);"));
                        break;
                    }
                    Thread.Sleep(250);
                    toTimeout += 250;
                }
                Thread.Sleep(800);
            }
            catch (Exception)
            {
                Console.WriteLine("Ajax failed to complete");
                throw;
            }
        }

        public void ForItemInTheGrid(int Item, int NumberOfItems)
        {
            bool success = false;
            int elapsed = 0;
            try
            {
                while ((!success) && (elapsed < 10000))
                {
                    Thread.Sleep(500);
                    elapsed += 500;
                    success = Item == NumberOfItems;
                }
            }
            finally
            {
                Assert.That(Item, Is.EqualTo(NumberOfItems), $"There is/are not only {NumberOfItems} item/s in the grid.");
            }
        }
    }
}

