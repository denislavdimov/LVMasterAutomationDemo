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

        public void IWaitForElementAndType(IWebElement element, string data)
        {
            IWaitForElementToBeClickable(element);
            element.Click();
            element.SendKeys(data);
        }

        public void IWaitAndClick(IWebElement element)
        {
            IWaitForElementToBeClickable(element);
            element.Click();
        }
        private void IWaitForElementToBeClickable(IWebElement element)
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
    }
}
