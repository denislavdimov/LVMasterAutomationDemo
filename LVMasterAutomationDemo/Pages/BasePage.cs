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
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }
        public virtual string PageUrl { get; }

        public void Open() 
        {
            driver.Navigate().GoToUrl(this.PageUrl);
        }

        public bool IsPageOpen()
        {
            return driver.Url == this.PageUrl;
        }

        public void WaitForElementToBeVisible(string xpath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
            }
            catch (TimeoutException te)
            {
                Assert.Fail("The element with selector {0} didn't appear. The exception was:\n {1}", xpath, te.ToString());
            }
        }
        
        public void IWaitForElementAndType(string xpath, string data)
        {
            WaitForElementToBeVisible(xpath);
            driver.FindElement(By.XPath(xpath)).SendKeys(data);
        }

        public void IClick(IWebElement element)
        {
            //WaitForElementToBeVisible(cssselector);
            driver.FindElement((By)element);
        }

    }
}
