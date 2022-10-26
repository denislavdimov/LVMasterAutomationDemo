using LVMasterAutomationDemo.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Runtime.InteropServices;

namespace LVMasterAutomationDemo.Tests
{
    public class LVMaster_AdminTests : BaseTests
    {
        [Test]
        public void Add_Delete_RoleTest()
        {
            var portalPage = new PortalPage(driver, new Wait(driver));
            var adminPage = new AdminPage(driver, new Wait(driver));
            var rolesPage = new Roles(driver, new Wait(driver));
            var loginPage = new LoginPage(driver, new Wait(driver));
            //var loginTests = new LVMaster_LoginTests();
            //loginTests.LoginWithValidCredentialsTest();
            loginPage.OpenLVAndLogin();
            portalPage.IGoToAdmin();
            adminPage.INavigateToAdminPage(adminPage.linkRoles);
            rolesPage.AddRole();
            rolesPage.DeleteRole();
        }

        [Test]
        public void Add_Edit_Delete_Team_Test()
        {
            var portalPage = new PortalPage(driver, new Wait(driver));
            var adminPage = new AdminPage(driver, new Wait(driver));
            var rolesPage = new Roles(driver, new Wait(driver));
            var loginPage = new LoginPage(driver, new Wait(driver));
            var teamsPage = new Teams(driver, new Wait(driver));
            loginPage.OpenLVAndLogin();
            portalPage.IGoToAdmin();
            adminPage.INavigateToAdminPage(adminPage.linkTeams);
            teamsPage.AddTeamWithUserAndRole();
        }
    }
}
