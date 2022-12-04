using LVPages;
using NUnit.Framework;

namespace LVTests.Admin.UserAccess
{
    [TestFixture]
    [Category("Admin/UserAccess")]
    public class AddEditUser : BaseTest
    {
        //[Test]
        public static void AddEdit_User_Test()
        {
            LoginTest.LoginWithValidCredentials();
            PageHelper.PortalPage.GoToAdmin();
            PageHelper.AdminPage.NavigateToAdminPage(PageHelper.AdminPage.Users);
            PageHelper.UsersPage.VerifyUsersPage();
            PageHelper.UsersPage.AddUser();
            PageHelper.UsersPage.EditUser();
        }
    }
}
