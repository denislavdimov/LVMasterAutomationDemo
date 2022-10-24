using LVMasterAutomationDemo.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Tests
{
    public class LVMaster_AdminTests : BaseTests
    {
        [Test]
        public void LVMaster_AddRoleTest()
        {
            var portalPage = new PortalPage(driver, new Wait(driver));
            var loginPage = new LoginPage(driver, new Wait(driver));
            var adminPage = new AdminPage(driver, new Wait(driver));
            var rolesPage = new Roles(driver, new Wait(driver));
            var Wait = new Wait(driver);
            loginPage.OpenLVAndLogin();
            //Wait.IWaitForLoader();
            portalPage.IGoToAdmin();
            //Wait.IWaitPageToLoad();
            adminPage.INavigateToAdminPage(adminPage.linkRoles);
            //Wait.IWaitPageToLoad();
            rolesPage.AddRole();
        }
    }
}
