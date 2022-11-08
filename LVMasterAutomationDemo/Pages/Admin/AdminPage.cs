using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Pages
{
    public class AdminPage : BasePage
    {
        private readonly IWait _wait;
        public AdminPage(IWebDriver driver, IWait wait) : base(driver)
        {
            _wait = wait;
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/lvadmin/#/";

        public IWebElement LinkRoles => driver.FindElement(By.LinkText("Roles"));
        public IWebElement LinkTeams => driver.FindElement(By.LinkText("Teams"));


        public void INavigateToAdminPage(IWebElement element)
        {
            _wait.IWaitForElementToBeClickable(element);
            IWaitAndClick(element);
            //_wait.IWaitForLoaderToDissaper();
            _wait.IWaitForLoaderToDiss();
            _wait.IWaitPageToLoad();
        }
    }
}
 