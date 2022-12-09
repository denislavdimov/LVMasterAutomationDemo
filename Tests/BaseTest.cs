using NUnit.Framework;
using OpenQA.Selenium;
using LVPages;

namespace LVTests
{
    public class BaseTest
    {
        protected IWebDriver? driver;

        [OneTimeSetUp]
        public void StartBrowser()
        {
            PageHelper.PageBuilder(driver!);
            PageHelper.BasePage.StartBrowser();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //PageHelper.BasePage.TakeScreenshotIfTestFails();
            PageHelper.BasePage.CloseBrowser();
        }
    }
}
