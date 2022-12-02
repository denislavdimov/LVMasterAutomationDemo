using LVPages;
using NUnit.Framework;

namespace LVTests
{
    public class LoginTest
    {
        public static void LoginWithValidCredentials()
        {
            //PageHelper.LoginPage.ClearCache();
            PageHelper.LoginPage.OpenLoanVantageAndLogin();
            PageHelper.PortalPage.VerifyPortalPage();
        }
    }
}
