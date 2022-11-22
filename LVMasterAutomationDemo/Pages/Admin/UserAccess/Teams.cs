using NUnit.Framework;
using OpenQA.Selenium;

namespace LVPages.Pages.Admin.UserAccess
{
    public class Teams : BasePage
    {
        private IWait _wait;
        int randomNumber = (int)(new Random().NextInt64(2022) + 20);
        public Teams(IWebDriver driver, IWait wait) : base(driver)
        {
            _wait = wait;
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/LVWEB/Admin/#/Teams/";

        private IWebElement NoticeModal => driver.FindElement(By.XPath("//div[@class='k-widget k-window']"));
        private IWebElement NoticeCloseButton => driver.FindElement(By.CssSelector(".k-icon.k-i-close"));
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
            _wait.IWaitForElementToBeClickable(LinkAdd);
            ISeeElement(SearchArea, By.XPath("//input[contains(@class,'search-query form-control')]"));
            ISeeElements(By.CssSelector("#teams-kendo-grid tr"));
            Assert.That(driver.Url, Is.EqualTo(PageUrl), "The PageUrl and DriverUrl are not equal");
        }
        public void AssignUserAndRoleToTeam()
        {
            IClick(UserAssignmentTab);
            ISeeElements(By.CssSelector("#team-user-assignment #available div"));
            IClick(AllUserAvaiableItems[1]);
            ISeeElement(UserAssignedItem, By.CssSelector("#team-user-assignment #assigned div"));
            IClick(RoleAssignmentTab);
            ISeeElements(By.CssSelector("#team-role-assignment #available div"));
            IClick(AllRoleAvaiableItems[4]);
            ISeeElement(RoleAssignedItem, By.CssSelector("#team-role-assignment #assigned div"));
        }
        public void EditTheUserAndRole()
        {
            IClick(UserAssignmentTab);
            ISeeElements(By.CssSelector("#team-user-assignment #available div"));
            IClick(AllUserAvaiableItems[10]);
            ISeeElements(By.CssSelector("#team-user-assignment #assigned div"));
            IClick(RoleAssignmentTab);
            ISeeElements(By.CssSelector("#team-role-assignment #available div"));
            IClick(AllRoleAvaiableItems[5]);
            ISeeElements(By.CssSelector("#team-role-assignment #assigned div"));
        }

        public void AddTeamWithUserAndRole()
        {
            ISeeElement(NoticeModal, By.XPath("//div[@class='k-widget k-window']"));
            IClick(NoticeCloseButton);
            IClick(LinkAdd);
            _wait.WaitForAjax();
            ISeeElement(TeamsModal, By.XPath("//div[@class='k-widget k-window']"));
            IType(NameInputField, "DenisAutomationTeamTest" + randomNumber);
            AssignUserAndRoleToTeam();
            IClick(SaveButton);
            _wait.WaitForAjax();
            AssertThereIsNoErrorAndException();
        }

        public void EditTeam()
        {
            IType(SearchArea, "DenisAutomationTeamTest" + randomNumber);
            _wait.IWaitForOneUserInTheGrid();
            IClick(EditButton);
            _wait.WaitForAjax();
            ISeeElement(TeamsModal, By.XPath("//div[@class='k-widget k-window']"));
            EditTheUserAndRole();
            IClick(SaveButton);
            _wait.WaitForAjax();
            AssertThereIsNoErrorAndException();
        }

        public void DeleteTeam()
        {
            IClick(EditButton);
            _wait.WaitForAjax();
            ISeeElement(TeamsModal, By.XPath("//div[@class='k-widget k-window']"));
            IClick(DeleteButton);
            ISeeElement(ConfirmationDialog, By.CssSelector(".confimation-dialog h5"));
            IClick(YesButton);
            _wait.WaitForAjax();
            AssertThereIsNoErrorAndException();
        }
    }
}
