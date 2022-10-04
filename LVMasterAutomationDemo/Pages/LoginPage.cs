using NUnit.Framework;
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
        public IWebElement rememberMe => driver.FindElement(By.XPath("//input[@type='checkbox']"));
        public IWebElement loginButton => driver.FindElement(By.XPath("//button[contains(.,'Log in')]"));
        public IWebElement backButton => driver.FindElement(By.XPath("//button[contains(.,'Back')]"));
        public IWebElement resetPassLink => driver.FindElement(By.LinkText("Reset it now"));
        public IWebElement changePassLink => driver.FindElement(By.LinkText("Change it now"));
        public override string PageUrl => "https://loanvantage.dev/IBS/master/lvweb/#/";

        public void OpenLVAndLogin()
        {
            driver.Navigate().GoToUrl(PageUrl);
            Assert.That(PageUrl, Is.EqualTo(driver.Url));
            IWaitForElementAndType("//input[@data-ui='institution-code-textbox']", "IBS");
            IClick(nextButton);
            //IWaitForElementAndType2(ibsField, "IBS");
            usernameField.SendKeys("ddimov@vsgbg.com");
            passwordField.SendKeys("De126000!!");
            //rememberMe.Click();
            //loginButton.Click();
            //Assert.IsTrue(IsPageOpen());
        }


    }
}
