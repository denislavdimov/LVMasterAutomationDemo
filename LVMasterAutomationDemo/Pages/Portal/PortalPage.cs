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
            Wait.ForElementToBeClickable(HamburgerMenu);
            Wait.ForElementToBeClickable(QueuesButton);
            Wait.ForElementToBeClickable(NewButton);
            ISeeElement(SearchField, By.CssSelector("input[placeholder='Search']"));
            Assert.That(driver.Url, Is.EqualTo(PageUrl), "The PageUrl and DriverUrl are not equal");
        }

        public void IGoToAdmin()
        {
            ISeeElement(HamburgerMenu, By.XPath("//button[@class='lv-dropdown-icon-button']"));
            I.Click(HamburgerMenu);
            ISeeElement(AdminLink, By.LinkText("Setup (Administrator)"));
            I.Click(AdminLink);
            Wait.ForPageToLoad();
            PageHelper.AdminPage.VerifyAdminPage();
        }
    }
}
