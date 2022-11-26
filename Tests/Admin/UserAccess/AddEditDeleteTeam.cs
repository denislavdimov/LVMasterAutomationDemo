using LVPages;
using NUnit.Framework;

namespace LVTests.Admin.UserAccess
{
    public class AddEditDeleteTeam : BaseTest
    {
        [Test]
        public static void AddEditDelete_Team_Test()
        {
            LoginTest.LoginWithValidCredentials();
            PageHelper.PortalPage.IGoToAdmin();
            PageHelper.AdminPage.INavigateToAdminPage(PageHelper.AdminPage.Teams);
            PageHelper.TeamsPage.VerifyTeamsPage();
            PageHelper.TeamsPage.AddTeamWithUserAndRole();
            PageHelper.TeamsPage.EditTeam();
            PageHelper.TeamsPage.DeleteTeam();
        }
    }
}
