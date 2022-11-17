using LVMasterAutomationDemo.Pages;
using NUnit.Framework;

namespace LVMasterAutomationDemo.Tests.Admin.UserAccess
{
    public class Add_Delete_Role : BaseTests
    {
        [Test]
        public void AddDelete_Role_Test()
        {
            //PageHelper.LoginPage.OpenLVAndLogin();
            PageHelper.PortalPage.IGoToAdmin();
            PageHelper.AdminPage.INavigateToAdminPage(PageHelper.AdminPage.LinkRoles);
            PageHelper.RolesPage.AddRole();
            PageHelper.RolesPage.DeleteRole();
        }
    }
}
