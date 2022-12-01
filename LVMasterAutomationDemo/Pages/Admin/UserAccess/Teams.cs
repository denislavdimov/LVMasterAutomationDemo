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
            (By.XPath("//div[contains(@class,'k-grid-content k-auto-scrollable')]//tr")).ToList();
        private IWebElement SearchInputArea => driver.FindElement(By.XPath("//input[contains(@class,'search-query form-control')]"));
        private IWebElement LinkAdd => driver.FindElement(By.LinkText("Add"));
        private IWebElement EditButton => driver.FindElement(By.XPath("//a[contains(@class,'v-icon icon-edit k-grid-Edit')]"));
        private IWebElement DeleteButton => driver.FindElement(By.XPath("//button[contains(.,'Delete')]"));
        private IWebElement NameInputField => driver.FindElement(By.XPath("//input[@name='Name']"));
        private IWebElement SaveButton => driver.FindElement(By.XPath("//button[contains(.,'Save')]"));
        private IWebElement UserAssignmentTab => driver.FindElement(By.XPath("//span[contains(.,'User Assignment')]"));
        private IWebElement RoleAssignmentTab => driver.FindElement(By.XPath("//span[contains(.,'Role Assignment')]"));
        private IWebElement ApprovalsTab => driver.FindElement(By.XPath("//span[@unselectable='on'][contains(.,'Approvals')]"));
        private IList<IWebElement> AvailableUsers => driver.FindElements(By.CssSelector("#team-user-assignment #available div")).ToList();
        private IList<IWebElement> AvailableRoles => driver.FindElements(By.CssSelector("#team-role-assignment #available div")).ToList();
        private IWebElement YesButton => driver.FindElement(By.XPath("//button[contains(.,'Yes')]"));

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
            Wait.ToSeeElement(SearchArea);
            Wait.ToSeeElements(TeamsGrid);
            AssertDriverUrlIsEqualToPageUrl();
        }

        public void AssignUserAndRoleToTeam()
        {
            I.Click(UserAssignmentTab);
            //ISeeElements(By.CssSelector("#team-user-assignment #available div"));
            I.Click(AvailableUsers[1]);
            Wait.ToSeeElement(UserAssignedItem);
            I.Click(RoleAssignmentTab);
            //ISeeElements(By.CssSelector("#team-role-assignment #available div"));
            I.Click(AvailableRoles[4]);
            Wait.ToSeeElement(RoleAssignedItem);
        }

        public void EditTheUserAndRole()
        {
            I.Click(UserAssignmentTab);
            //ISeeElements(By.CssSelector("#team-user-assignment #available div"));
            I.Click(AvailableUsers[10]);
            Wait.ToSeeElements(UserAssignedItem);
            I.Click(RoleAssignmentTab);
            //ISeeElements(By.CssSelector("#team-role-assignment #available div"));
            I.Click(AvailableRoles[5]);
            Wait.ToSeeElements(RoleAssignedItem);
        }

        public void AddTeamWithUserAndRole()
        {
            Wait.ToSeeElement(NoticeModal);
            I.Click(NoticeCloseButton);
            I.Click(LinkAdd);
            Wait.ForAjax();
            Wait.ToSeeElement(TeamsModal);
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
            Wait.ToSeeElement(TeamsModal);
            EditTheUserAndRole();
            I.Click(SaveButton);
            Wait.ForAjax();
            Wait.ForNoErrorAndException();
        }

        public void DeleteTeam()
        {
            I.Click(EditButton);
            Wait.ForAjax();
            Wait.ToSeeElement(TeamsModal);
            I.Click(DeleteButton);
            Wait.ToSeeElement(ConfirmationDialog);
            I.Click(YesButton);
            Wait.ForAjax();
            Wait.ForNoErrorAndException();
        }
    }
}
