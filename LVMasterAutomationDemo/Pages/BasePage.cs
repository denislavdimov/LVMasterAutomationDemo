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
        private const int C = 30;
        private readonly Wait _wait;
        protected readonly IWebDriver driver;
        private static readonly int secondsToLoadPage = C;
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(secondsToLoadPage);
            _wait = new Wait(driver);
        }
        public virtual string PageUrl { get; }
        public WebDriverWait wait { get { return new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToLoadPage)); } }
        private IList<IWebElement> Exception =>
          driver.FindElements(By.XPath("//div[@class='toast toast-error']")).ToList();

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
            catch (TimeoutException te)
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
            catch (TimeoutException te)
            {
                Assert.Fail($"The element with selector {0} didn't appear. The exception was:\n {1}", te.ToString());
            }
        }

        public void IWaitForElementAndType(IWebElement element, string data)
        {
            _wait.IWaitForElementToBeClickable(element);
            element.Click();
            element.SendKeys(data);
        }

        public void IWaitAndClick(IWebElement element)
        {
            _wait.IWaitForElementToBeClickable(element);
            element.Click();
        }
        public void ISeeNoErrorAndException()
        {
            //wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            //    By.XPath("//div[contains(@class, 'toast toast-error')]")));
            //wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
            //    By.XPath("//div[contains(@class, 'toast toast-warning')]")));
            Assert.IsTrue(Exception.Count == 0, "Exception is thrown on the Page");
        }
    }
}
