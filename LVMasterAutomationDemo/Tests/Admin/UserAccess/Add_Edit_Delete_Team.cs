using LVMasterAutomationDemo.Pages;
using NUnit.Framework;

namespace LVMasterAutomationDemo.Tests.Admin.UserAccess
{
    public class Add_Edit_Delete_Team : BaseTests
    {
        [Test]
        public void AddEditDelete_Team_Test()
        {
            //PageHelper.LoginPage.OpenLVAndLogin();
            PageHelper.PortalPage.IGoToAdmin();
            PageHelper.AdminPage.INavigateToAdminPage(PageHelper.AdminPage.LinkTeams);
            PageHelper.TeamsPage.AddTeamWithUserAndRole();
            PageHelper.TeamsPage.EditTeam();
            PageHelper.TeamsPage.DeleteTeam();
        }
    }
}
