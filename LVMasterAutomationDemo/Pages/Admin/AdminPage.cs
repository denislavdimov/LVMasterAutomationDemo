using LVPages.IClasses;
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
        //public override string PageUrl => "https://loanvantage.dev/IBS/master/lvadmin/#/";
        public override string PageUrl => base.PageUrl + "lvadmin/#/";

        private IWebElement HamburgerButton => driver.FindElement(By.XPath("//div[@class='lv-header-dropdown-menu']//button"));
        private IWebElement SearhInputArea => driver.FindElement(By.XPath("//input[contains(@class,'lv-form-control-input')]"));
        private IWebElement MainMenuButton => driver.FindElement(By.XPath("//button[contains(@data-ui,'main-menu-header')]"));
        public IWebElement Roles => driver.FindElement(By.LinkText("Roles"));
        public IWebElement Teams => driver.FindElement(By.LinkText("Teams"));
        public IWebElement Users => driver.FindElement(By.LinkText("Users"));
        public IWebElement ReportDefinition => driver.FindElement(By.LinkText("Report Definition"));
        public IWebElement PostApproval => driver.FindElement(By.LinkText("Post-Approvals"));
        public IWebElement LoanWizard => driver.FindElement(By.LinkText("Loan Wizard"));


        private By AllAdminPages = By.XPath("//div[@class='lv-custom-admin-container']//a");
        private By SearchArea = By.XPath("//input[contains(@class,'lv-form-control-input')]");

        public void VerifyAdminPage()
        {
            Wait.ForElementToBeClickable(MainMenuButton);
            Wait.ForElementToBeClickable(HamburgerButton);
            Wait.ForElement(SearchArea);
            Wait.ForElements(AllAdminPages);
            AssertDriverUrlIsEqualToPageUrl();
        }

        public void NavigateToAdminPage(IWebElement element)
        {
            Wait.ForElementToBeClickable(element);
            I.Click(element);
            Wait.ForPageToLoad();
            Wait.ForTheLoader();
            Wait.ForAjax();
            Wait.ForNoErrorAndException();
        }
    }
}
