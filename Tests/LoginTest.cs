using LVPages;
using NUnit.Framework;

namespace LVTests
{
    public class LoginTest
    {
        [Test]
        public static void LoginWithValidCredentials()
        {
            //PageHelper.LoginPage.ClearCache();
            PageHelper.LoginPage.OpenLoanVantageAndLogin();
        }
    }
}
