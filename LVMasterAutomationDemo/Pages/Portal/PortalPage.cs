using NUnit.Framework;
using OpenQA.Selenium;

namespace LVPages.Pages.Portal
{
    public class PortalPage : BasePage
    {
        private readonly IWait _wait;
        public PortalPage(IWebDriver driver, IWait wait) : base(driver)
        {
            _wait = wait;
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/lvweb/Portal/Index#/";

        private IWebElement HamburgerMenu => driver.FindElement(By.XPath("//button[@class='lv-dropdown-icon-button']"));
        private IWebElement QueuesButton => driver.FindElement(By.XPath("//button[contains(.,'Queues')]"));
        private IWebElement NewButton => driver.FindElement(By.XPath("//button[contains(.,'New')]"));
        private IWebElement SearchField => driver.FindElement(By.CssSelector("input[placeholder='Search']"));
        private IWebElement SetupAdministratorLink => driver.FindElement(By.XPath("//a[contains(.,'Setup (Administrator)')]"));
        private IWebElement SearchButton => driver.FindElement(By.XPath("//button[contains(.,'Search')]"));
        private IWebElement AddPartyButton => driver.FindElement(By.XPath("//button[contains(.,'Add party')]"));
        private IWebElement AdminLink => driver.FindElement(By.LinkText("Setup (Administrator)"));

        public void VerifyPortalPage()
        {
            _wait.IWaitForElementToBeClickable(HamburgerMenu);
            _wait.IWaitForElementToBeClickable(QueuesButton);
            _wait.IWaitForElementToBeClickable(NewButton);
            ISeeElement(SearchField, By.CssSelector("input[placeholder='Search']"));
            Assert.That(driver.Url, Is.EqualTo(PageUrl), "The PageUrl and DriverUrl are not equal");
        }

        public void IGoToAdmin()
        {
            ISeeElement(HamburgerMenu, By.XPath("//button[@class='lv-dropdown-icon-button']"));
            IWaitAndClick(HamburgerMenu);
            ISeeElement(AdminLink, By.LinkText("Setup (Administrator)"));
            IWaitAndClick(AdminLink);
            _wait.IWaitPageToLoad();
            PageHelper.AdminPage.VerifyAdminPage();
        }
    }
}
