using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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

        public void WaitForElement(string element)
        {
            IWebElement element12 = wait.Until(ExpectedConditions) 
        }
    }
}
