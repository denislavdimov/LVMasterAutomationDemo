using LVPages;
using NUnit.Framework;

namespace LVTests.Admin.UserAccess
{
    public class AddDeleteRole : BaseTest
    {
        [Test]
        public void AddDelete_Role_Test()
        {
            LoginTest.LoginWithValidCredentials();
            PageHelper.PortalPage.IGoToAdmin();
            PageHelper.AdminPage.INavigateToAdminPage(PageHelper.AdminPage.LinkRoles);
            PageHelper.RolesPage.AddRole();
            PageHelper.RolesPage.DeleteRole();
        }
    }
}
