using LVPages.IClasses;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LVPages.Pages.Portal
{
    public class LoginPage : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;
        public LoginPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }

        private IWebElement Username => driver.FindElement(By.Id("signInName"));
        private IWebElement Password => driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => driver.FindElement(By.XPath("//button[contains(.,'Log in')]"));
        public override string PageUrl => "https://loanvantage.dev/IBS/master/lvweb/Portal/Index#/";
        private string CachePage => "https://loanvantage.dev/IBS/master/lvweb/Cache/ClearAll";
        private IList<IWebElement> CacheTableContent => driver.FindElements(By.XPath("//div[@id='cache-table-content']//tr")).ToList();

        public By UsernameField = By.Id("signInName");
        public By PasswordField = By.Id("password");
        public By Login = By.XPath("//button[contains(.,'Log in')]");

        public void ClearCache()
        {
            try
            {
                driver.Navigate().GoToUrl(CachePage);
                Wait.ForPageToLoad();
                Assert.IsTrue(CacheTableContent.Count == 0, "The cache is not cleared");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        private void FillInUsernameAndPassword(string username, string password)
        {
            Wait.ToSeeElement(UsernameField);
            Wait.ToSeeElement(PasswordField);
            Wait.ToSeeElement(Login);
            I.FillInField(Username, username);
            I.FillInField(Password, password);
        }

        public void OpenLoanVantageAndLogin()
        {
            IGoToThisPageUrl();
            FillInUsernameAndPassword("ddimov@vsgbg.com", "De126000!");
            I.Click(LoginButton);
            Wait.ForPageToLoad();
            Wait.ForAjax();
            //Wait.ForNoErrorAndException();
            PageHelper.PortalPage.VerifyPortalPage();
        }
    }
}
