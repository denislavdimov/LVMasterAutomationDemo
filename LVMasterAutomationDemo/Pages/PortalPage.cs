using NUnit.Framework;
using OpenQA.Selenium;

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

        public IWebElement hamburgerMenu => driver.FindElement(By.CssSelector(".fal.fa-bars"));
        public IWebElement searchField => driver.FindElement(By.CssSelector("input[placeholder='Search']"));
        public IWebElement setupAdministratorLink => driver.FindElement(By.XPath("//a[contains(.,'Setup (Administrator)')]"));
        public IWebElement searchButton => driver.FindElement(By.XPath("//button[contains(.,'Search')]"));
        public IWebElement addPartyButton => driver.FindElement(By.XPath("//button[contains(.,'Add party')]"));

        public void ISearchForFileWithId(string id)
        {
            _wait.IWaitForElementAndType(searchField, id);
            _wait.IWaitAndClick(searchButton);
            IWaitForLoader();
            WaitForAjax();
            //Assert.That(driver.Url, Is.EqualTo("https://loanvantage.dev/IBS/master/lvweb/Layout/#"+"/"+id+"/"));
        }

        public void CreatePartyFromSeachField()
        {
            _wait.IWaitForElementAndType(searchField, "denislavdimov");
            IWaitForLoader();
            _wait.IWaitAndClick(addPartyButton);
            IWaitForLoader();
            WaitForAjax();
        }
    }
}
