using LVPages.Pages.Admin;
using LVPages.Pages.Admin.ProcessFlow;
using LVPages.Pages.Admin.UserAccess;
using LVPages.Pages.Portal;
using OpenQA.Selenium;

namespace LVPages
{
    public static class PageHelper
    {
        public static BasePage BasePage;
        public static Teams TeamsPage;
        public static Roles RolesPage;
        public static AdminPage AdminPage;
        public static PortalPage PortalPage;
        public static LoginPage LoginPage;
        public static Users UsersPage;
        public static ReportDefinitionPage ReportDefinitionPage;

        public static void PageBuilder(IWebDriver driver)
        {
            BasePage = new BasePage(driver);
            TeamsPage = new Teams(driver);
            RolesPage = new Roles(driver);
            AdminPage = new AdminPage(driver);
            PortalPage = new PortalPage(driver);
            LoginPage = new LoginPage(driver);
            UsersPage = new Users(driver);
            ReportDefinitionPage = new ReportDefinitionPage(driver);
        }
    }
}
