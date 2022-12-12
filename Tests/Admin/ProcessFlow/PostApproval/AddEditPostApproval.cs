using LVPages;
using NUnit.Framework;

namespace LVTests.Admin.ProcessFlow.PostApproval
{
    [TestFixture]
    [Category("Admin/ProcessFlow/PostApproval")]
    public class AddEditPostApproval : BaseTest
    {
        [Test]
        public void AddEdit_PostApproval_Test()
        {
            LoginTest.LoginWithValidCredentials();
            PageHelper.PortalPage.GoToAdmin();
            PageHelper.AdminPage.NavigateToAdminPage(PageHelper.AdminPage.PostApproval);
            PageHelper.PostApprovalPage.VerifyPostApprovalPage();
            PageHelper.PostApprovalPage.AddPostApproval();
            PageHelper.PostApprovalPage.EditPostApproval();
        }
    }
}
