using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace LVMasterAutomationDemo.Pages
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
        //public WebDriverWait WaitForInvisibility { get { return new WebDriverWait(driver, TimeSpan.FromSeconds(secondsForInvisibility)); } }
        public IList<IWebElement> Exception =>
          driver.FindElements(By.XPath("//div[@class='toast toast-error']")).ToList();
        private IList<IWebElement> Warning =>
            driver.FindElements(By.XPath("//div[contains(@class, 'toast toast-warning')]")).ToList();

        public void IsPageOpen(string Url)
        {
            string DriverUrl = driver.Url;
            Url = driver.Url;
            try
            {
                Assert.That(driver.Url, Is.EqualTo(Url));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public void IGoToThisPageUrl()
        {
            try
            {
                driver.Navigate().GoToUrl(this.PageUrl);
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
                Assert.Fail($"The element with selector {by} didn't appear. The exception was:\n {te}", te.ToString());
            }
        }

        public void IWaitForElementAndType(IWebElement element, string data)
        {
            try
            {
                _wait.IWaitForElementToBeClickable(element);
                Interactions.IClick(element);
                Interactions.IType(element ,data);
            }
            catch (Exception)
            {
                Console.WriteLine($"The {element} can't be filled in");
                throw;
            }
        }

        public void IWaitAndClick(IWebElement element)
        {
            try
            {
                _wait.IWaitForElementToBeClickable(element);
                Interactions.IClick(element);

            }
            catch (Exception)
            {
                Console.WriteLine($"The {element} is not clickable");
                throw;
            }
        }


        public void AssertThereIsNoErrorAndException()
        {
            _wait.SetTimeout(5);
            var waitforinvs = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            waitforinvs.Until(ExpectedConditions.InvisibilityOfElementLocated(
                By.XPath("//div[contains(@class, 'k-loading-color')]")));
            waitforinvs.Until(ExpectedConditions.InvisibilityOfElementLocated(
                By.XPath("//div[@class='k-loading-image']")));
            Assert.IsTrue(Warning.Count == 0, "Warning is thrown on the Page");
            Assert.IsTrue(Exception.Count == 0, "Exception is thrown on the Page");
            _wait.ResetTimeoutToDefault();
        }
    }
}
