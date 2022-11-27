using LVPages.IClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace LVPages
{
    public class BasePage
    {
        private readonly Wait Wait;
        private readonly IUserActions I;
        protected IWebDriver driver;
        private static int secondsToLoadPage = 30;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(secondsToLoadPage);
            Wait = new Wait(driver);
            I = new UserActions(driver);
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
                Wait.ForAjax();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public void AssertDriverUrlIsEqualToPageUrl()
        {
            try
            {
                Assert.That(driver.Url, Is.EqualTo(PageUrl));
            }
            catch (Exception)
            {
                Console.WriteLine("The PageUrl and DriverUrl are not equal");
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

        public void IDragAndDrop(IWebElement element, IWebElement place)
        {
            try
            {
                Wait.ForElementToBeClickable(element);
                I.DragAndDrop(element, place);
            }
            catch (Exception)
            {
                Console.WriteLine($"The element: {element} cannot be drag and dropped to place: {place}");
                throw;
            }
        }

        public void AssertThereIsNoErrorAndException()
        {
            try
            {
                Wait.SetTimeout(2);
                var warning = Warning;
                var exception = Exception;
                if (warning.Count > 0 || exception.Count > 0)
                {
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
                        By.XPath("//div[contains(@class, 'k-loading-color')]")));
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
                        By.XPath("//div[@class='k-loading-image']")));
                    Assert.IsTrue(Exception.Count == 0, "Exception is thrown on the Page");
                    Assert.IsTrue(Warning.Count == 0, "Warning is thrown on the Page");
                    Wait.ResetTimeoutToDefault();
                }
                else
                {
                    Wait.ResetTimeoutToDefault();
                    return;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("An exception or warning is thrown on the page.");
                throw;
            }
        }

        public void ClearAllInputFields()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("$('input').val('');");
        }

    }
}
