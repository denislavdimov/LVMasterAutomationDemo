using NUnit.Framework;
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

        private IList<IWebElement> AdminPageLinks => driver.FindElements(By.XPath("//div[@class='lv-custom-admin-container']//a"));
        private IWebElement HamburgerButton => driver.FindElement(By.XPath("//div[@class='lv-header-dropdown-menu']//button"));
        private IWebElement SearhInputArea => driver.FindElement(By.XPath("//input[contains(@class,'lv-form-control-input')]"));
        private IWebElement MainMenuButton => driver.FindElement(By.XPath("//button[contains(@data-ui,'main-menu-header')]"));

        public IWebElement LinkRoles => driver.FindElement(By.LinkText("Roles"));
        public IWebElement LinkTeams => driver.FindElement(By.LinkText("Teams"));

        public void VerifyAdminPage()
        {
            _wait.IWaitForElementToBeClickable(MainMenuButton);
            _wait.IWaitForElementToBeClickable(HamburgerButton);
            ISeeElement(SearhInputArea, By.XPath("//input[contains(@class,'lv-form-control-input')]"));
            ISeeElements(By.XPath("//div[@class='lv-custom-admin-container']//a"));
            Assert.That(driver.Url, Is.EqualTo(PageUrl), "The PageUrl and DriverUrl are not equal");
        }
        public void INavigateToAdminPage(IWebElement element)
        {
            _wait.IWaitForElementToBeClickable(element);
            IWaitAndClick(element);
            //_wait.IWaitForLoaderToDissaper();
            _wait.IWaitPageToLoad();
            _wait.WaitForAjax();
        }
    }
}
 