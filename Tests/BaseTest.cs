using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using LVPages;

namespace LVTests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
            PageHelper.PageBuilder(driver);
            driver.Manage().Window.Size = new System.Drawing.Size(1440, 900);
            driver.Manage().Cookies.DeleteAllCookies();
            PageHelper.LoginPage.ClearCache();
        }

        //[SetUp]
        //public void SetUp()
        //{
        //    driver = new ChromeDriver();
        //    PageHelper.PageBuilder(driver);
        //    driver.Manage().Window.Size = new System.Drawing.Size(1440, 900);
        //    driver.Manage().Cookies.DeleteAllCookies();
        //    //PageHelper.LoginPage.OpenLVAndLogin();
        //}

        //[TearDown]
        //public void TearDown()
        //{
        //    driver.Manage().Cookies.DeleteAllCookies();
        //    if (driver == null)
        //        return;
        //    driver.Quit();
        //}

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            if (driver == null)
                return;
            driver.Quit();
        }
    }
}