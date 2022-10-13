using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace LVMasterAutomationDemo.Pages
{
    public class PortalPage : BasePage
    {
        private readonly IWait _wait;
        //Actions action = new Actions(driver);
        public PortalPage (IWebDriver driver, IWait wait) : base(driver)
        {
            _wait = wait;
        }
        public override string PageUrl => "url";

        public IWebElement hamburgerMenu => driver.FindElement(By.CssSelector(".fal.fa-bars"));
        public IWebElement searchField => driver.FindElement(By.CssSelector("input[placeholder='Search']"));
        public IWebElement setupAdministratorLink => driver.FindElement(By.XPath("//a[contains(.,'Setup (Administrator)')]"));
        public IWebElement searchButton => driver.FindElement(By.XPath("//button[contains(.,'Search')]"));
        public IWebElement addPartyButton => driver.FindElement(By.XPath("//button[contains(.,'Add party')]"));
        public IWebElement adminLink => driver.FindElement(By.LinkText("Setup (Administrator)"));

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
            var action = new Actions(driver);  
            ISee(hamburgerMenu, By.CssSelector(".fal.fa-bars"));
            action.MoveToElement(hamburgerMenu).Click();
            ISee(adminLink, By.LinkText("Setup (Administrator)"));
            adminLink.Click();
        }
    }
}
