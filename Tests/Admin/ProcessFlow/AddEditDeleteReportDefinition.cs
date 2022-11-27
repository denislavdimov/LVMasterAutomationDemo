using LVPages;
using NUnit.Framework;

namespace LVTests.Admin.ProcessFlow
{
    public class AddEditDeleteReportDefinition : BaseTest
    {
        [Test]
        public void AddEditDelete_ReportDefinition_Test()
        {
            LoginTest.LoginWithValidCredentials();
            PageHelper.PortalPage.IGoToAdmin();
            PageHelper.AdminPage.INavigateToAdminPage(PageHelper.AdminPage.ReportDefinition);
            PageHelper.ReportDefinitionPage.VerifyReportDefinitionPage();
            PageHelper.ReportDefinitionPage.AddBuildReport();
        }
    }
}
