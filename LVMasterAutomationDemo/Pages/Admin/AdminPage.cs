using LVPages.IClasses;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LVPages.Pages.Admin
{
    public class AdminPage : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;
        public AdminPage(IWebDriver driver) : base(driver)
        {
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/lvadmin/#/";

        private IList<IWebElement> AdminPageLinks => driver.FindElements(By.XPath("//div[@class='lv-custom-admin-container']//a"));
        private IWebElement HamburgerButton => driver.FindElement(By.XPath("//div[@class='lv-header-dropdown-menu']//button"));
        private IWebElement SearhInputArea => driver.FindElement(By.XPath("//input[contains(@class,'lv-form-control-input')]"));
        private IWebElement MainMenuButton => driver.FindElement(By.XPath("//button[contains(@data-ui,'main-menu-header')]"));
        public IList<IWebElement> OldAdminGridItems => driver.FindElements(By.XPath("//div[contains(@class,'k-grid-content k-auto-scrollable')]//tr")).ToList();
        public IWebElement Roles => driver.FindElement(By.LinkText("Roles"));
        public IWebElement Teams => driver.FindElement(By.LinkText("Teams"));
        public IWebElement Users => driver.FindElement(By.LinkText("Users"));
        public IWebElement ReportDefinition => driver.FindElement(By.LinkText("Report Definition"));

        public void VerifyAdminPage()
        {
            Wait.ForElementToBeClickable(MainMenuButton);
            Wait.ForElementToBeClickable(HamburgerButton);
            ISeeElement(SearhInputArea, By.XPath("//input[contains(@class,'lv-form-control-input')]"));
            ISeeElements(By.XPath("//div[@class='lv-custom-admin-container']//a"));
            Assert.That(driver.Url, Is.EqualTo(PageUrl), "The PageUrl and DriverUrl are not equal");
        }
        public void INavigateToAdminPage(IWebElement element)
        {
            Wait.ForElementToBeClickable(element);
            I.Click(element);
            Wait.ForPageToLoad();
            Wait.ForLoaderToDissaper();
            //Wait.ForAjax();
        }
    }
}
