using LVMasterAutomationDemo.Pages;
using NUnit.Framework;

namespace LVMasterAutomationDemo.Tests
{
    public class LVMaster_AdminTests : BaseTests
    {
        [Test]
        public void Add_Delete_RoleTest()
        {
            //PageHelper.LoginPage.OpenLVAndLogin();
            PageHelper.PortalPage.IGoToAdmin();
            PageHelper.AdminPage.INavigateToAdminPage(PageHelper.AdminPage.LinkRoles);
            PageHelper.RolesPage.AddRole();
            //PageHelper.RolesPage.DeleteRole();
        }

        [Test]
        public void Add_Edit_Delete_Team_Test()
        {
            //PageHelper.LoginPage.OpenLVAndLogin();
            PageHelper.PortalPage.IGoToAdmin();
            PageHelper.AdminPage.INavigateToAdminPage(PageHelper.AdminPage.LinkTeams);
            PageHelper.TeamsPage.AddTeamWithUserAndRole();
        }
    }
}
