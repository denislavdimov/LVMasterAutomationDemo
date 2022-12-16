using LVPages;
using NUnit.Framework;

namespace LVTests.Admin.ProcessFlow.LoanWizard
{
    [TestFixture]
    [Category("Admin/ProcessFlow/LoanWizard")]
    public class AddEditDelete_LoanWizard : BaseTest
    {
        [Test]
        public void AddEditDelete_LoanWizard_Test()
        {
            LoginTest.LoginWithValidCredentials();
            PageHelper.PortalPage.GoToAdmin();
            PageHelper.AdminPage.NavigateToAdminPage(PageHelper.AdminPage.LoanWizard);
            PageHelper.LoanWizardPage.CreateLoanWizard();
            PageHelper.LoanWizardPage.EditLoanWizard();
            PageHelper.LoanWizardPage.DeleteLoanWizard();
        }
    }
}
