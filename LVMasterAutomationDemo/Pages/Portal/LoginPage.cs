using LVPages.IClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;

namespace LVPages.Pages.Portal
{
    public class LoginPage : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;
        public LoginPage(IWebDriver driver) : base(driver)
        {
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }

        public override string PageUrl => base.PageUrl + "lvweb/Portal/Index#/";
        private string CachePage => base.PageUrl + "lvweb/Cache/ClearAll";

        private IWebElement Username => driver.FindElement(By.Id("signInName"));
        private IWebElement Password => driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => driver.FindElement(By.XPath("//button[contains(.,'Log in')]"));
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
                Wait.ForAjax();
                Assert.IsTrue(CacheTableContent.Count == 0, "The cache is not cleared");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private void FillInUsernameAndPassword(string username, string password)
        {
            Wait.ForElement(UsernameField);
            Wait.ForElement(PasswordField);
            Wait.ForElement(Login);
            I.FillInField(Username, username);
            I.FillInField(Password, password);
        }

        public void OpenLoanVantageAndLogin()
        {
            GoToThisPageUrl();
            FillInUsernameAndPassword("ddimov@vsgbg.com", "De126000!");
            I.Click(LoginButton);
            Wait.ForPageToLoad();
            Wait.ForAjax();
            Wait.ForTheLoader();
        }
    }
}
