using LVPages.IClasses;
using OpenQA.Selenium;

namespace LVPages.Pages.Admin.UserAccess
{
    public class Teams : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;
        int randomNumber = (int)(new Random().NextInt64(2022) + 20);
        public Teams(IWebDriver driver) : base(driver)
        {
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/LVWEB/Admin/#/Teams/";

        private IWebElement NoticeModal => driver.FindElement(By.XPath("//div[@class='k-widget k-window']"));
        private IWebElement NoticeCloseButton => driver.FindElement(By.CssSelector(".k-icon.k-i-close"));
        private IList<IWebElement> GridItems => driver.FindElements
            (By.XPath("//div[contains(@class,'k-grid-content k-auto-scrollable')]//tr")).ToList();
        private IWebElement LinkAdd => driver.FindElement(By.LinkText("Add"));
        public IWebElement TeamsModal => driver.FindElement(By.XPath("//div[@class='k-widget k-window']"));
        private IWebElement EditButton => driver.FindElement(By.XPath("//a[contains(@class,'v-icon icon-edit k-grid-Edit')]"));
        private IWebElement DeleteButton => driver.FindElement(By.XPath("//button[contains(.,'Delete')]"));
        private IWebElement SearchArea => driver.FindElement(By.XPath("//input[contains(@class,'search-query form-control')]"));
        private IWebElement NameInputField => driver.FindElement(By.XPath("//input[@name='Name']"));
        private IWebElement SaveButton => driver.FindElement(By.XPath("//button[contains(.,'Save')]"));
        private IWebElement UserAssignmentTab => driver.FindElement(By.XPath("//span[contains(.,'User Assignment')]"));
        private IWebElement RoleAssignmentTab => driver.FindElement(By.XPath("//span[contains(.,'Role Assignment')]"));
        private IWebElement ApprovalsTab => driver.FindElement(By.XPath("//span[@unselectable='on'][contains(.,'Approvals')]"));
        private IWebElement UserAssignedItem => driver.FindElement(By.CssSelector("#team-user-assignment #assigned div"));
        private IList<IWebElement> AllUserAvaiableItems => driver.FindElements(By.CssSelector("#team-user-assignment #available div")).ToList();
        private IWebElement RoleAssignedItem => driver.FindElement(By.CssSelector("#team-role-assignment #assigned div"));
        private IList<IWebElement> AllRoleAvaiableItems => driver.FindElements(By.CssSelector("#team-role-assignment #available div")).ToList();
        private IWebElement ConfirmationDialog => driver.FindElement(By.CssSelector(".confimation-dialog h5"));
        private IWebElement YesButton => driver.FindElement(By.XPath("//button[contains(.,'Yes')]"));

        public void VerifyTeamsPage()
        {
            Wait.ForElementToBeClickable(LinkAdd);
            ISeeElement(SearchArea, By.XPath("//input[contains(@class,'search-query form-control')]"));
            ISeeElements(By.CssSelector("#teams-kendo-grid tr"));
            AssertDriverUrlIsEqualToPageUrl();
        }
        public void AssignUserAndRoleToTeam()
        {
            I.Click(UserAssignmentTab);
            //ISeeElements(By.CssSelector("#team-user-assignment #available div"));
            I.Click(AllUserAvaiableItems[1]);
            ISeeElement(UserAssignedItem, By.CssSelector("#team-user-assignment #assigned div"));
            I.Click(RoleAssignmentTab);
            //ISeeElements(By.CssSelector("#team-role-assignment #available div"));
            I.Click(AllRoleAvaiableItems[4]);
            ISeeElement(RoleAssignedItem, By.CssSelector("#team-role-assignment #assigned div"));
        }
        public void EditTheUserAndRole()
        {
            I.Click(UserAssignmentTab);
            //ISeeElements(By.CssSelector("#team-user-assignment #available div"));
            I.Click(AllUserAvaiableItems[10]);
            ISeeElements(By.CssSelector("#team-user-assignment #assigned div"));
            I.Click(RoleAssignmentTab);
            //ISeeElements(By.CssSelector("#team-role-assignment #available div"));
            I.Click(AllRoleAvaiableItems[5]);
            ISeeElements(By.CssSelector("#team-role-assignment #assigned div"));
        }

        public void AddTeamWithUserAndRole()
        {
            ISeeElement(NoticeModal, By.XPath("//div[@class='k-widget k-window']"));
            I.Click(NoticeCloseButton);
            I.Click(LinkAdd);
            Wait.ForAjax();
            ISeeElement(TeamsModal, By.XPath("//div[@class='k-widget k-window']"));
            I.FillInField(NameInputField, "DenisAutomationTeamTest" + randomNumber);
            AssignUserAndRoleToTeam();
            I.Click(SaveButton);
            Wait.ForAjax();
            AssertThereIsNoErrorAndException();
        }

        public void EditTeam()
        {
            I.FillInField(SearchArea, "DenisAutomationTeamTest" + randomNumber);
            Wait.ForItemInTheGrid(GridItems.Count, 1);
            I.Click(EditButton);
            Wait.ForAjax();
            ISeeElement(TeamsModal, By.XPath("//div[@class='k-widget k-window']"));
            EditTheUserAndRole();
            I.Click(SaveButton);
            Wait.ForAjax();
            AssertThereIsNoErrorAndException();
        }

        public void DeleteTeam()
        {
            I.Click(EditButton);
            Wait.ForAjax();
            ISeeElement(TeamsModal, By.XPath("//div[@class='k-widget k-window']"));
            I.Click(DeleteButton);
            ISeeElement(ConfirmationDialog, By.CssSelector(".confimation-dialog h5"));
            I.Click(YesButton);
            Wait.ForAjax();
            AssertThereIsNoErrorAndException();
        }
    }
}
