using LVPages;
using NUnit.Framework;

namespace LVTests.Admin.ProcessFlow.ReportDefinition
{
    [TestFixture]
    [Category("Admin/ProcessFlow/ReportDefinition")]
    public class AddEditDeleteReportBuilder : BaseTest
    {
        [Test]
        public void AddEditDelete_ReportDefinition_Test()
        {
            LoginTest.LoginWithValidCredentials();
            PageHelper.PortalPage.IGoToAdmin();
            PageHelper.AdminPage.INavigateToAdminPage(PageHelper.AdminPage.ReportDefinition);
            PageHelper.ReportDefinitionPage.VerifyReportDefinitionPage();
            PageHelper.ReportDefinitionPage.AddBuildReport();
            PageHelper.ReportDefinitionPage.EditBuildReport();
            PageHelper.ReportDefinitionPage.DeleteBuildReport();
        }
    }
}
