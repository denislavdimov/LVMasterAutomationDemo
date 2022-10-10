using NUnit.Framework;
using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Pages
{
    public class PortalPage : BasePage
    {
        public PortalPage (IWebDriver driver) : base(driver)
        {
        }
        public override string PageUrl => "url";

        public IWebElement hamburgerMenu => driver.FindElement(By.CssSelector(".fal.fa-bars"));
        public IWebElement searchField => driver.FindElement(By.CssSelector("input[placeholder='Search']"));
        public IWebElement setupAdministratorLink => driver.FindElement(By.XPath("//a[contains(.,'Setup (Administrator)')]"));
        public IWebElement searchButton => driver.FindElement(By.XPath("//button[contains(.,'Search')]"));
        public IWebElement addPartyButton => driver.FindElement(By.XPath("//button[contains(.,'Add party')]"));

        public void ISearchForFileWithId(string id)
        {
            IWaitForElementAndType(searchField, id);
            IWaitAndClick(searchButton);
            IWaitForLoader();
            WaitForAjax();
            //Assert.That(driver.Url, Is.EqualTo("https://loanvantage.dev/IBS/master/lvweb/Layout/#"+"/"+id+"/"));
        }

        public void CreatePartyFromSeachField()
        {
            IWaitForElementAndType(searchField, "denislavdimov");
            IWaitForLoader();
            IWaitAndClick(addPartyButton);
            IWaitForLoader();
            WaitForAjax();
        }
    }
}
