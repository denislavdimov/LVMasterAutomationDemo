using LVMasterAutomationDemo.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Tests
{
    public class LVMaster_LoginTests : BaseTests
    {
        [Test]
        public void LoginWithValidCredentialsTest()
        {
            var loginPage = new LoginPage(driver, new Wait(driver));
            loginPage.OpenLVAndLogin();
        }
    }
}
