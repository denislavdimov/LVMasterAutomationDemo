using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace LVMasterAutomationDemo.Pages
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

        public void OpenLVAndLogin()
        {
            IGoToThisPageUrl();
            IWaitForElementAndType(UsernameField, "ddimov@vsgbg.com");
            IWaitForElementAndType(PasswordField, "De126000!");
            IWaitAndClick(LoginButton);
            _wait.WaitForAjax();
            _wait.IWaitPageToLoad();
            PageHelper.PortalPage.VerifyPortalPage();
            //IsPageOpen();
        }
    }
}
