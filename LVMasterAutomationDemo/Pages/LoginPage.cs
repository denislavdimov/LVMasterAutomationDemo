﻿using NUnit.Framework;
using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement ibsField => driver.FindElement(By.XPath("//input[@data-ui='institution-code-textbox']"));
        public IWebElement nextButton => driver.FindElement(By.XPath("//button[contains(.,'Next')]"));
        public IWebElement usernameField => driver.FindElement(By.XPath("//input[@data-ui='login-name-or-email-texbox']"));
        public IWebElement passwordField => driver.FindElement(By.XPath("//input[@type='password']"));
        public IWebElement rememberMe => driver.FindElement(By.XPath("//span[.='Remember Me']/following-sibling::input"));
        public IWebElement loginButton => driver.FindElement(By.XPath("//button[contains(.,'Log in')]"));
        public IWebElement backButton => driver.FindElement(By.XPath("//button[contains(.,'Back')]"));
        public IWebElement resetPassLink => driver.FindElement(By.LinkText("Reset it now"));
        public IWebElement changePassLink => driver.FindElement(By.LinkText("Change it now"));
        public override string PageUrl => "url";

        public void OpenLVAndLogin()
        {
            IOpenPageAndCheckIsItOpen();
            IWaitForElementAndType(ibsField, "INSTCODE");
            IClick(nextButton);
            IWaitForElementAndType(usernameField, "username");
            IWaitForElementAndType(passwordField, "password");
            //IClick(rememberMe);
            IClick(loginButton);
            //Assert.IsTrue(IsPageOpen());
            IWaitPageToLoad();
            IsPageOpen();
        }


    }
}
