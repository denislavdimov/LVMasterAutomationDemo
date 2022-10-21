using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Tests
{
    public class BaseTests
    {
        protected IWebDriver driver;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Size = new System.Drawing.Size(1440, 900);
            //driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

        [SetUp]
        public void SetUp()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            //driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Manage().Cookies.DeleteAllCookies();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (driver == null)
                return;
            driver.Quit();
        }

    }
}
