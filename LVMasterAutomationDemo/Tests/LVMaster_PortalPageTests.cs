using LVMasterAutomationDemo.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Tests
{
    public class LVMaster_PortalPageTests : BaseTests
    {
        [Test]
        public void LVMaster_LoginWithValidCredentialsTest()
        {
            var portalPage = new PortalPage(driver, new Wait(driver));
            var loginPage = new LoginPage(driver, new Wait(driver));
            var adminPage = new AdminPage(driver, new Wait(driver));    
            var Wait = new Wait(driver);
            loginPage.OpenLVAndLogin();
            Wait.IWaitForLoader();
            //portalPage.ISearchForFileWithId("45012");
            portalPage.IGoToAdmin();
            Wait.IWaitPageToLoad();
        }
    }
}
