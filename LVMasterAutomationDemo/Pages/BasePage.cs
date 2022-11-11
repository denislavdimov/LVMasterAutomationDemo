using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Xml.Linq;


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

        public void IGoToThisPageUrlAndCheckIsItOpen()
        {
            driver.Navigate().GoToUrl(this.PageUrl);
            IsPageOpen();
        }

        public bool IsPageOpen()
        {
            _wait.IWaitUntilPageLoadsCompletely();
            //Assert.That(driver.Url, Is.EqualTo(PageUrl));
            return driver.Url == this.PageUrl;
        }

        public void ISeeElement(IWebElement element, By by)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (NoSuchElementException te)
            {
                Assert.Fail($"The element with selector {0} didn't appear. The exception was:\n {1}", element, te.ToString());
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
                Assert.Fail($"The element with selector {0} didn't appear. The exception was:\n {1}", te.ToString());
            }
        }

        public void IWaitForElementAndType(IWebElement element, string data)
        {
            try
            {
                _wait.IWaitForElementToBeClickable(element);
                element.Click();
                Thread.Sleep(1000);
                element.SendKeys(data);
            }
            catch (Exception)
            {
                Console.WriteLine($"The {element} is not clickable");
                throw;
            }
        }

        public void IWaitAndClick(IWebElement element)
        {
            try
            {
                _wait.IWaitForElementToBeClickable(element);
                element.Click();
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                Console.WriteLine($"The {element} is not clickable");
                throw;
            }
        }
        public void AssertThereIsNoErrorAndException()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            var waitforinvs = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            waitforinvs.Until(ExpectedConditions.InvisibilityOfElementLocated(
                By.XPath("//div[contains(@class, 'k-loading-color')]")));
            waitforinvs.Until(ExpectedConditions.InvisibilityOfElementLocated(
                By.XPath("//div[@class='k-loading-image']")));
            Assert.IsTrue(Warning.Count == 0, "Warning is thrown on the Page");
            Assert.IsTrue(Exception.Count == 0, "Exception is thrown on the Page");
        }
    }
}
