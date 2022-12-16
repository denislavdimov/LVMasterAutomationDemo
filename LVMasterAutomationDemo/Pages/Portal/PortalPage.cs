using LVPages.IClasses;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LVPages.Pages.Portal
{
    public class PortalPage : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;
        public PortalPage(IWebDriver driver) : base(driver)
        {
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }
        public override string PageUrl => base.PageUrl + "lvweb/Portal/Index#/";

        private IWebElement HamburgerMenu => driver.FindElement(By.XPath("//button[@class='lv-dropdown-icon-button']"));
        private IWebElement QueuesButton => driver.FindElement(By.XPath("//button[contains(.,'Queues')]"));
        private IWebElement NewButton => driver.FindElement(By.XPath("//button[contains(.,'New')]"));
        private IWebElement SearchField => driver.FindElement(By.CssSelector("input[placeholder='Search']"));
        private IWebElement SetupAdministratorLink => driver.FindElement(By.XPath("//a[contains(.,'Setup (Administrator)')]"));
        private IWebElement SearchButton => driver.FindElement(By.XPath("//button[contains(.,'Search')]"));
        private IWebElement AddPartyButton => driver.FindElement(By.XPath("//button[contains(.,'Add party')]"));
        private IWebElement SetupAdministrator => driver.FindElement(By.LinkText("Setup (Administrator)"));

        private By SearchArea = By.CssSelector("input[placeholder='Search']");
        private By Hamburger = By.XPath("//button[@class='lv-dropdown-icon-button']");
        private By AdminLink = By.LinkText("Setup (Administrator)");

        public void VerifyPortalPage()
        {
            Wait.ForElementToBeClickable(HamburgerMenu);
            Wait.ForElementToBeClickable(QueuesButton);
            Wait.ForElementToBeClickable(NewButton);
            Wait.ForElement(SearchArea);
            AssertDriverUrlIsEqualToPageUrl();
        }

        public void GoToAdmin()
        {
            Wait.ForElement(Hamburger);
            I.Click(HamburgerMenu);
            Wait.ForElement(AdminLink);
            I.Click(SetupAdministrator);
            Wait.ForPageToLoad();
            Wait.ForAjax();
            Wait.ForTheLoader();
            PageHelper.AdminPage.VerifyAdminPage();
        }
    }
}
