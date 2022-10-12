﻿using LVMasterAutomationDemo.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Tests
{
    public class LVMaster_PortalPageTests : BaseTests
    {
        [Test]
        public void LVMaster_LoginWithValidCredentialsTest()
        {
            var portalPage = new PortalPage(driver, new Wait(driver));
            var loginPage = new LoginPage(driver, new Wait(driver));
            var basePage = new BasePage(driver);
            loginPage.OpenLVAndLogin();
            basePage.IWaitForLoader();
            portalPage.ISearchForFileWithId("45012");
        }
    }
}
