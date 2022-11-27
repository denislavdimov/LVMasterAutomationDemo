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
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }

        //public IWebElement IbsField => driver.FindElement(By.XPath("//input[@data-ui='institution-code-textbox']"));
        //public IWebElement NextButton => driver.FindElement(By.XPath("//button[contains(.,'Next')]"));
        private IWebElement UsernameField => driver.FindElement(By.Id("signInName"));
        private IWebElement PasswordField => driver.FindElement(By.Id("password"));
        //public IWebElement RememberMe => driver.FindElement(By.XPath("//span[.='Remember Me']/following-sibling::input"));
        private IWebElement LoginButton => driver.FindElement(By.XPath("//button[contains(.,'Log in')]"));
        //public IWebElement BackButton => driver.FindElement(By.XPath("//button[contains(.,'Back')]"));
        //public IWebElement ResetPassLink => driver.FindElement(By.LinkText("Reset it now"));
        //public IWebElement ChangePassLink => driver.FindElement(By.LinkText("Change it now"));
        public override string PageUrl => "https://loanvantage.dev/IBS/master/lvweb/Portal/Index#/";
        private string CachePage => "https://loanvantage.dev/IBS/master/lvweb/Cache/ClearAll";
        private IList<IWebElement> CacheTableContent => driver.FindElements(By.XPath("//div[@id='cache-table-content']//tr")).ToList();

        public void ClearCache()
        {
            try
            {
                driver.Navigate().GoToUrl(CachePage);
                Wait.ForPageToLoad();
                Wait.SetTimeout(2);
                Assert.IsTrue(CacheTableContent.Count == 0, "The cache is not cleared");
                Wait.ResetTimeoutToDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        private void FillInUsernameAndPassword(string username, string password)
        {
            ISeeElement(UsernameField, By.Id("signInName"));
            ISeeElement(PasswordField, By.Id("password"));
            I.FillInField(UsernameField, username);
            I.FillInField(PasswordField, password);
        }

        public void OpenLoanVantageAndLogin()
        {
            IGoToThisPageUrl();
            FillInUsernameAndPassword("ddimov@vsgbg.com", "De126000!");
            I.Click(LoginButton);
            Wait.ForPageToLoad();
            Wait.ForLoaderToDissaper();
            Wait.ForAjax();
            PageHelper.PortalPage.VerifyPortalPage();
            //IsPageOpen();
        }
    }
}
