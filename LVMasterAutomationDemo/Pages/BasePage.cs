using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace LVMasterAutomationDemo.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver driver;
        public static int secondsToLoadPage = 25;
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(secondsToLoadPage);
        }
        public virtual string PageUrl { get; }
        public WebDriverWait wait { get { return new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToLoadPage)); } }

        public void IOpenPageAndCheckIsItOpen() 
        {
            driver.Navigate().GoToUrl(this.PageUrl);
            IsPageOpen();
        }

        public bool IsPageOpen()
        {
            IWaitPageToLoad();
            return driver.Url == this.PageUrl;
        }
        public void IWaitPageToLoad()
        {
            var seconds = secondsToLoadPage;
            for (int i = 0; i < seconds; i++)
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//body")));
            }
        }

        public void WaitForElementToBeClickable(IWebElement element)
        {
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(element));
            }
            catch (TimeoutException te)
            {
                Assert.Fail("The element with selector {0} didn't appear. The exception was:\n {1}", element, te.ToString());
            }
        }


        public void IWaitForElementAndType(IWebElement element, string data)
        {
            WaitForElementToBeClickable(element);
            element.SendKeys(data);
        }

        public void IClick(IWebElement element)
        {
            WaitForElementToBeClickable(element);
            element.Click();
        }

        public void ISee()
        {
            //code
        }
    }
}
