using LVPages;
using NUnit.Framework;

namespace LVTests.Admin.ProcessFlow.ReportDefinition
{
    [TestFixture]
    [Category("Admin/ProcessFlow/ReportDefinition")]
    public class AddEditDeleteSection : BaseTest
    {
        [Test]
        public void AddEditDelete_Section_Test()
        {
            LoginTest.LoginWithValidCredentials();
            PageHelper.PortalPage.GoToAdmin();
            PageHelper.AdminPage.NavigateToAdminPage(PageHelper.AdminPage.ReportDefinition);
            PageHelper.ReportDefinitionPage.VerifyReportDefinitionPage();
            PageHelper.ReportDefinitionPage.AddSection();
            PageHelper.ReportDefinitionPage.EditSection();
            PageHelper.ReportDefinitionPage.DeleteSection();
        }
    }
}
