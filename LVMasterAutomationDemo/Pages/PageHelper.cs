using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Pages
{
    public static class PageHelper
    {
        public static Teams TeamsPage;
        public static Roles RolesPage;
        public static AdminPage AdminPage;
        public static PortalPage PortalPage;
        public static LoginPage LoginPage;

        public static void PageBuilder(IWebDriver driver)
        {
            TeamsPage = new Teams(driver, new Wait(driver));
            RolesPage = new Roles(driver, new Wait(driver));
            AdminPage = new AdminPage(driver, new Wait(driver));
            PortalPage = new PortalPage(driver, new Wait(driver));
            LoginPage = new LoginPage(driver, new Wait(driver));
        }

    }
}
