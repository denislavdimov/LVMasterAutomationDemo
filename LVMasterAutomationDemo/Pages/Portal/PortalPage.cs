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

        private IWebElement hamburgerMenu => driver.FindElement(By.XPath("//button[@class='lv-dropdown-icon-button']"));
        private IWebElement searchField => driver.FindElement(By.CssSelector("input[placeholder='Search']"));
        private IWebElement setupAdministratorLink => driver.FindElement(By.XPath("//a[contains(.,'Setup (Administrator)')]"));
        private IWebElement searchButton => driver.FindElement(By.XPath("//button[contains(.,'Search')]"));
        private IWebElement addPartyButton => driver.FindElement(By.XPath("//button[contains(.,'Add party')]"));
        private IWebElement adminLink => driver.FindElement(By.LinkText("Setup (Administrator)"));

        public void ISearchForFileWithId(string id)
        {
            IWaitForElementAndType(searchField, id);
            IWaitAndClick(searchButton);
            _wait.IWaitForLoader();
            _wait.WaitForAjax();
            //Assert.That(driver.Url, Is.EqualTo("https://loanvantage.dev/IBS/master/lvweb/Layout/#"+"/"+id+"/"));
        }

        public void CreatePartyFromSeachField()
        {
            IWaitForElementAndType(searchField, "denislavdimov");
            _wait.IWaitForLoader();
            IWaitAndClick(addPartyButton);
            _wait.IWaitForLoader();
            _wait.WaitForAjax();
        }

        public void IGoToAdmin()
        {
            //var action = new Actions(driver);  
            ISee(hamburgerMenu, By.XPath("//button[@class='lv-dropdown-icon-button']"));
            //action.MoveToElement(hamburgerMenu).Click();
            IWaitAndClick(hamburgerMenu);
            ISee(adminLink, By.LinkText("Setup (Administrator)"));
            IWaitAndClick(adminLink);
            //_wait.IWaitForLoader();
            _wait.IWaitPageToLoad();
        }
    }
}
