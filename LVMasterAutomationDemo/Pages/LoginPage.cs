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

        //public IWebElement ibsField => driver.FindElement(By.XPath("//input[@data-ui='institution-code-textbox']"));
        //public IWebElement nextButton => driver.FindElement(By.XPath("//button[contains(.,'Next')]"));
        public IWebElement usernameField => driver.FindElement(By.Id("signInName"));
        public IWebElement passwordField => driver.FindElement(By.Id("password"));
        //public IWebElement rememberMe => driver.FindElement(By.XPath("//span[.='Remember Me']/following-sibling::input"));
        public IWebElement loginButton => driver.FindElement(By.XPath("//button[contains(.,'Log in')]"));
        //public IWebElement backButton => driver.FindElement(By.XPath("//button[contains(.,'Back')]"));
        //public IWebElement resetPassLink => driver.FindElement(By.LinkText("Reset it now"));
        //public IWebElement changePassLink => driver.FindElement(By.LinkText("Change it now"));
        public override string PageUrl => "https://loanvantage.dev/master#/";

        public void OpenLVAndLogin()
        {
            IGoToThisPageUrlAndCheckIsItOpen();
            _wait.IWaitForElementAndType(usernameField, "ddimov@vsgbg.com");
            _wait.IWaitForElementAndType(passwordField, "De126000!");
            _wait.IWaitAndClick(loginButton);
            IWaitForLoader();
            IWaitUntilPageLoadsCompletely();          
        }
    }
}
