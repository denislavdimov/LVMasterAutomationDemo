using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace LVPages
{
    public class BasePage
    {
        private Wait _wait;
        protected IWebDriver driver;
        private static int secondsToLoadPage = 30;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(secondsToLoadPage);
            _wait = new Wait(driver);
        }

        public virtual string PageUrl { get; }
        public WebDriverWait wait { get { return new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToLoadPage)); } }
        public IList<IWebElement> Exception =>
          driver.FindElements(By.XPath("//div[@class='toast toast-error']")).ToList();
        private IList<IWebElement> Warning =>
            driver.FindElements(By.XPath("//div[contains(@class, 'toast toast-warning')]")).ToList();

        public void IGoToThisPageUrl()
        {
            try
            {
                driver.Navigate().GoToUrl(PageUrl);
                _wait.WaitForAjax();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public void ISeeElement(IWebElement element, By by)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (NoSuchElementException te)
            {
                Assert.Fail($"The element: {element} with selector {by} didn't appear. The exception was:\n {te}", te.ToString());
            }
        }
        public void ISeeElements(By by)
        {
            try
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
            }
            catch (NoSuchElementException te)
            {
                Assert.Fail($"The elements with selector {by} didn't appear. The exception was:\n {te}", te.ToString());
            }
        }

        public void IType(IWebElement element, string data)
        {
            try
            {
                _wait.IWaitForElementToBeClickable(element);
                Interactions.Click(element);
                Interactions.Type(element, data);
            }
            catch (Exception)
            {
                Console.WriteLine($"The {element} can't be filled in");
                throw;
            }
        }

        public void IClick(IWebElement element)
        {
            try
            {
                _wait.IWaitForElementToBeClickable(element);
                Interactions.Click(element);

            }
            catch (Exception)
            {
                Console.WriteLine($"The {element} is not clickable");
                throw;
            }
        }

        public void AssertThereIsNoErrorAndException()
        {
            try
            {
                _wait.SetTimeout(2);
                var warning = Warning;
                var exception = Exception;
                if (warning.Count > 0 || exception.Count > 0)
                {
                    _wait.SetTimeout(2);
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
                        By.XPath("//div[contains(@class, 'k-loading-color')]")));
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
                        By.XPath("//div[@class='k-loading-image']")));
                    Assert.IsTrue(Warning.Count == 0, "Warning is thrown on the Page");
                    Assert.IsTrue(Exception.Count == 0, "Exception is thrown on the Page");
                    _wait.ResetTimeoutToDefault();
                }
                else
                {
                    _wait.ResetTimeoutToDefault();
                    return;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An exception or warning is thrown on the page.");
                throw;
            }
        }

        public void CleanAllInputFields()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("$('input').val('');");
        }
    }
}
