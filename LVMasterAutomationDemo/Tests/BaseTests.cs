using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using LVMasterAutomationDemo.Pages;

namespace LVMasterAutomationDemo.Tests
{
    public class BaseTests
    {
        protected IWebDriver driver;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
            PageHelper.PageBuilder(driver);
            driver.Manage().Window.Size = new System.Drawing.Size(1440, 900);
        }

        [SetUp]
        public void SetUp()
        {
            driver.Manage().Cookies.DeleteAllCookies();
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
