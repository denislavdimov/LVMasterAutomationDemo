using LVPages;
using NUnit.Framework;

namespace LVTests.Admin.UserAccess
{
    public class AddEditUser : BaseTest
    {
        [Test]
        public static void AddEdit_User_Test()
        {
            LoginTest.LoginWithValidCredentials();
            PageHelper.PortalPage.IGoToAdmin();
            PageHelper.AdminPage.INavigateToAdminPage(PageHelper.AdminPage.LinkUsers);
            PageHelper.UsersPage.VerifyUsersPage();
            PageHelper.UsersPage.AddUser();
            PageHelper.UsersPage.EditUser();
        }
    }
}
