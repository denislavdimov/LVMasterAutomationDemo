using LVPages;
using NUnit.Framework;

namespace LVTests.Admin.ProcessFlow.ReportDefinition
{
    [TestFixture]
    [Category("Admin/ProcessFlow/ReportDefinition")]
    public class AddDeletePresentationReport : BaseTest
    {
        [Test]
        public void AddDelete_PresentationReport_Test()
        {
            LoginTest.LoginWithValidCredentials();
            PageHelper.PortalPage.GoToAdmin();
            PageHelper.AdminPage.NavigateToAdminPage(PageHelper.AdminPage.ReportDefinition);
            PageHelper.ReportDefinitionPage.VerifyReportDefinitionPage();
            PageHelper.ReportDefinitionPage.AddPresentationReport();
            PageHelper.ReportDefinitionPage.DeletePresentationReport();
        }
    }
}
