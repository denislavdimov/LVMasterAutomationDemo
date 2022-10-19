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

        private IWebElement linkRoles => driver.FindElement(By.LinkText("Roles"));

        public void INavigateToAdminPage()
        {
            _wait.IWaitForElementToBeClickable(linkRoles);
            IWaitAndClick(linkRoles);
        }

    }
}
 