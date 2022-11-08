using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace LVMasterAutomationDemo.Pages
{
    public class PortalPage : BasePage
    {
        private readonly IWait _wait;
        public PortalPage (IWebDriver driver, IWait wait) : base(driver)
        {
            _wait = wait;
        }
        public override string PageUrl => "url";

        private IWebElement HamburgerMenu => driver.FindElement(By.XPath("//button[@class='lv-dropdown-icon-button']"));
        private IWebElement SearchField => driver.FindElement(By.CssSelector("input[placeholder='Search']"));
        private IWebElement SetupAdministratorLink => driver.FindElement(By.XPath("//a[contains(.,'Setup (Administrator)')]"));
        private IWebElement SearchButton => driver.FindElement(By.XPath("//button[contains(.,'Search')]"));
        private IWebElement AddPartyButton => driver.FindElement(By.XPath("//button[contains(.,'Add party')]"));
        private IWebElement AdminLink => driver.FindElement(By.LinkText("Setup (Administrator)"));

        //public void ISearchForFileWithId(string id)
        //{
        //    IWaitForElementAndType(searchField, id);
        //    IWaitAndClick(searchButton);
        //    _wait.IWaitForLoader();
        //    _wait.WaitForAjax();
        //    Assert.That(driver.Url, Is.EqualTo("https://loanvantage.dev/IBS/master/lvweb/Layout/#"+"/"+id+"/"));
        //}

        public void IGoToAdmin()
        { 
            ISeeElement(HamburgerMenu, By.XPath("//button[@class='lv-dropdown-icon-button']"));
            IWaitAndClick(HamburgerMenu);
            ISeeElement(AdminLink, By.LinkText("Setup (Administrator)"));
            IWaitAndClick(AdminLink);
            //_wait.WaitForAjax();
            _wait.IWaitPageToLoad();
        }
    }
}
