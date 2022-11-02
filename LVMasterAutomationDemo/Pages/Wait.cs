using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace LVMasterAutomationDemo.Pages
{
    public class Wait : IWait
    {
        //protected readonly IWebDriver _driver;
        protected IWebDriver _driver;
        public static int _secondsBeforeTimeout = 30;
        private  WebDriverWait wait { get { return new WebDriverWait(_driver, TimeSpan.FromSeconds(_secondsBeforeTimeout)); } }

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

        public void IWaitForLoader()
        {
            try
            {
                //wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='k-loading-image']")));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='k-loading-image']")));
            }
            catch (TimeoutException te)
            {
                Assert.Fail($"The element loading image didn't appear., The exception was:\n {te}", te.ToString());
            }
        }

        public void WaitForAjax()
        {
            var js = (IJavaScriptExecutor)_driver;
            wait.Until(wd => js.ExecuteScript("return jQuery.active").ToString() == "0");
        }

        public void IWaitUntilPageLoadsCompletely()
        {
            var js = (IJavaScriptExecutor)_driver;
            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
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
    }
}
