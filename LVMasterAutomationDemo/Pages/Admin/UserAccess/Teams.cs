using LVPages.IClasses;
using OpenQA.Selenium;

namespace LVPages.Pages.Admin.UserAccess
{
    public class Teams : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;

        public Teams(IWebDriver driver) : base(driver)
        {
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }

        public override string PageUrl => "https://loanvantage.dev/IBS/master/LVWEB/Admin/#/Teams/";

        private IWebElement NoticeCloseButton => driver.FindElement(By.CssSelector(".k-icon.k-i-close"));
        private IList<IWebElement> GridItems => driver.FindElements
            (By.CssSelector("div[class='k-grid-content k-auto-scrollable'] tr")).ToList();
        private IWebElement SearchInputArea => driver.FindElement(By.CssSelector("input[name='GridToolbarSearch']"));
        private IWebElement LinkAdd => driver.FindElement(By.CssSelector("a[data-bind='click: add']"));
        private IWebElement NameInputField => driver.FindElement(By.CssSelector("input[name='Name']"));
        private IWebElement DeleteButton => driver.FindElement(By.CssSelector("button[data-bind='visible: displayRemoveBtn, click: remove']"));
        private IWebElement YesButton => driver.FindElement(By.CssSelector("button[data-ui='confirmation-yes']"));
        private IWebElement AddAllLink => driver.FindElement(By.CssSelector("a[data-bind='click: addAll']"));
        private IWebElement SaveButton => driver.FindElement(By.CssSelector("button[data-bind='visible: permission.AdminTeamEdit, click: onSave']"));
        private IWebElement EditButton => driver.FindElement(By.CssSelector("a[class='v-icon icon-edit k-grid-Edit']"));
        private IWebElement UserAssignmentTab => driver.FindElement(By.XPath("//span[text()='User Assignment']"));
        private IWebElement RoleAssignmentTab => driver.FindElement(By.XPath("//span[text()='Role Assignment']"));
        private IList<IWebElement> AvailableUsers => driver.FindElements(By.CssSelector("#team-user-assignment #available div")).ToList();
        private IList<IWebElement> AvailableRoles => driver.FindElements(By.CssSelector("#team-role-assignment #available div")).ToList();



        private By TeamsGrid = By.CssSelector("#teams-kendo-grid tr");
        private By SearchArea = By.XPath("//input[contains(@class,'search-query form-control')]");
        private By UserAssignedItem = By.CssSelector("#team-user-assignment #assigned div");
        private By RoleAssignedItem = By.CssSelector("#team-role-assignment #assigned div");
        private By NoticeModal = By.XPath("//div[@class='k-widget k-window']");
        private By TeamsModal = By.XPath("//div[@class='k-widget k-window']");
        private By ConfirmationDialog = By.CssSelector(".confimation-dialog h5");

        public void VerifyTeamsPage()
        {
            Wait.ForElementToBeClickable(LinkAdd);
            Wait.ForElement(SearchArea);
            Wait.ForElements(TeamsGrid);
            AssertDriverUrlIsEqualToPageUrl();
        }

        public void AssignUserAndRoleToTeam()
        {
            I.Click(UserAssignmentTab);
            I.Click(AvailableUsers[1]);
            Wait.ForElement(UserAssignedItem);
            I.Click(RoleAssignmentTab);
            I.Click(AvailableRoles[4]);
            Wait.ForElement(RoleAssignedItem);
        }

        public void EditTheUserAndRole()
        {
            I.Click(UserAssignmentTab);
            I.Click(AvailableUsers[10]);
            Wait.ForElements(UserAssignedItem);
            I.Click(RoleAssignmentTab);
            I.Click(AvailableRoles[5]);
            Wait.ForElements(RoleAssignedItem);
        }

        public void AddTeamWithUserAndRole()
        {
            Wait.ForElement(NoticeModal);
            I.Click(NoticeCloseButton);
            I.Click(LinkAdd);
            Wait.ForAjax();
            Wait.ForElement(TeamsModal);
            I.FillInField(NameInputField, $"DenisAutomationTeamTest{randomNumber}");
            AssignUserAndRoleToTeam();
            I.Click(SaveButton);
            Wait.ForAjax();
            Wait.ForNoErrorAndException();
        }

        public void EditTeam()
        {
            I.FillInField(SearchInputArea, $"DenisAutomationTeamTest{randomNumber}");
            Wait.ForItemInTheGrid(GridItems.Count, 1);
            I.Click(EditButton);
            Wait.ForAjax();
            Wait.ForElement(TeamsModal);
            EditTheUserAndRole();
            I.Click(SaveButton);
            Wait.ForAjax();
            Wait.ForNoErrorAndException();
        }

        public void DeleteTeam()
        {
            I.Click(EditButton);
            Wait.ForAjax();
            Wait.ForElement(TeamsModal);
            I.Click(DeleteButton);
            Wait.ForElement(ConfirmationDialog);
            I.Click(YesButton);
            Wait.ForAjax();
            Wait.ForNoErrorAndException();
            Wait.ForItemInTheGrid(GridItems.Count, 0);
        }
    }
}
