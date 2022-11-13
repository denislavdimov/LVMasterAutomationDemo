﻿using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.DevTools.V104.Debugger;
using NUnit.Framework.Internal;

namespace LVMasterAutomationDemo.Pages
{
    public class Wait : IWait
    {
        protected IWebDriver _driver;
        public static int _secondsBeforeTimeout = 30;
        private WebDriverWait wait { get { return new WebDriverWait(_driver, TimeSpan.FromSeconds(_secondsBeforeTimeout)); } }

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

        public void IWaitForLoaderToDissaper()
        {
            try
            {
                //wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='k-loading-image']")));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='k-loading-image']")));
            }
            catch (TimeoutException te)
            {
                Assert.Fail($"The Loader didn't disappear., The exception was:\n {te}", te.ToString());
            }
        }

        private bool IWaitForLoader()
        {
            try
            {
                wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='k-loading-image']")));
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Loader did not appear");
                throw;
            }

        }

        public void IWaitForLoaderToDiss()
        {
            try
            {
                if (IWaitForLoader() != true)
                {
                    Assert.Fail("Loader did not appear");
                }
                else
                {
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='k-loading-image']")));
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        //public void WaitForAjax()
        //{
        //    var js = (IJavaScriptExecutor)_driver;
        //    wait.Until(wd => js.ExecuteScript("return jQuery.active").ToString() == "0");
        //}

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

        public void WaitForAjax()
        {
            try
            {
                while (true)
                {
                    bool ajaxIsComplete = (bool)((IJavaScriptExecutor)_driver).ExecuteScript("return jQuery.active == 0");
                    if (ajaxIsComplete)
                    {
                        break;
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Ajax failed to complete");
                throw;
            }
        }
    }
}

