using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace LVMasterAutomationDemo.Pages
{
    public class Wait : IWait
    {
        protected readonly IWebDriver _driver;
        public static int _secondsToLoadPage = 25;
        private  WebDriverWait wait { get { return new WebDriverWait(_driver, TimeSpan.FromSeconds(_secondsToLoadPage)); } }

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
            catch (TimeoutException te)
            {
                Assert.Fail("The element with selector {0} didn't appear. The exception was:\n {1}", element, te.ToString());
            }
        }

        public void IWaitForLoader()
        {
            try
            {
                wait.Until(ExpectedConditions.ElementExists(By.Id("loader")));
            }
            catch (TimeoutException te)
            {
                Assert.Fail("The element with selector didn't appear.", te.ToString());
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
            var seconds = _secondsToLoadPage;
            for (int i = 0; i < seconds; i++)
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//body")));
            }
        }
    }
}
