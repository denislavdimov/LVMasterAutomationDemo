using NUnit.Framework;
using OpenQA.Selenium;

namespace LVPages.Pages.Portal
{
    public class LoginPage : BasePage
    {
        private readonly IWait _wait;
        public LoginPage(IWebDriver driver, IWait wait) : base(driver)
        {
            _wait = wait;
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
                _wait.IWaitPageToLoad();
                _wait.SetTimeout(5);
                Assert.IsTrue(CacheTableContent.Count == 0, "The cache is not cleared");
                _wait.ResetTimeoutToDefault();
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
            IWaitForElementAndType(UsernameField, username);
            IWaitForElementAndType(PasswordField, password);
        }

        public void OpenLoanVantageAndLogin()
        {
            IGoToThisPageUrl();
            FillInUsernameAndPassword("ddimov@vsgbg.com", "De126000!");
            IWaitAndClick(LoginButton);
            _wait.WaitForAjax();
            _wait.IWaitPageToLoad();
            PageHelper.PortalPage.VerifyPortalPage();
            //IsPageOpen();
        }
    }
}
