using LVMasterAutomationDemo.Pages;
using NUnit.Framework;
namespace LVMasterAutomationDemo.Tests
{
    public class LVMaster_PortalPageTests : BaseTests
    {
        [Test]
        public void LVMaster_LoginWithValidCredentialsTest()
        {
            var loginPage = new LoginPage(driver);
            loginPage.OpenLVAndLogin();
        }
    }
}
