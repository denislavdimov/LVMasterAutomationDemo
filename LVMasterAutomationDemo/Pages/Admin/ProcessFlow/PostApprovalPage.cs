using LVPages.IClasses;
using OpenQA.Selenium;

namespace LVPages.Pages.Admin.ProcessFlow
{
    public class PostApprovalPage : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;
        string ItemNumber = "";

        public PostApprovalPage(IWebDriver driver) : base(driver)
        {
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }

        public override string PageUrl => "https://loanvantage.dev/IBS/master/lvadmin/#/Post-Approvals/";

        private IList<IWebElement> DropdownItems => driver.FindElements(By.CssSelector("div[class='lv-select__option css-1cyd34j-option']")).ToList();
        private IWebElement DdItems => driver.FindElement(By.Id($"react-select-3-option-{ItemNumber}"));

        private IWebElement MultiSelectDropdownItems => driver.FindElement(By.CssSelector("div[class='lv-multi-select__option css-1cyd34j-option']"));

        private IWebElement AddNewPostApproval => driver.FindElement(By.CssSelector("button[data-ui='post-approval-toolbar-add-btn']"));
        private IWebElement PostApprovalName => driver.FindElement(By.CssSelector("input[data-ui='post-approval-details-input-name']"));
        private IWebElement BusinessRuleDropdown => driver.FindElement(By.CssSelector("div[data-ui='post-approval-details-select-business-rule']"));
        private IWebElement MinTeamsRequiredToApprove => driver.FindElement(By.CssSelector("input[name='MinTeamsRequiredToApprove']"));
        private IWebElement IsActiveCheckbox => driver.FindElement(By.CssSelector("label[data-ui='post-approval-details-checkbox-is-active']"));
        private IWebElement PostApprovalEntitiesTab => driver.FindElement(By.CssSelector("//span[text()='Post-Approval Entities']"));
        private IWebElement AddTeam => driver.FindElement(By.CssSelector("button[data-ui='post-approval-entities-add-team-btn']"));
        private IWebElement TeamRule => driver.FindElement(By.CssSelector("div[data-ui='post-approval-entities-business-rule']"));
        private IWebElement TeamDropdown => driver.FindElement(By.CssSelector("div[data-ui='post-approval-entities-select-team']"));
        private IWebElement ApproveOnDropdown => driver.FindElement(By.CssSelector("div[data-ui='post-approval-entities-select-approve-on']"));
        private IWebElement ApproveOnAmount => driver.FindElement(By.CssSelector("input[name='ApprovalLimitAmt']"));
        private IWebElement MembersRequired => driver.FindElement(By.CssSelector("input[name='NumberOfUsersRequired']"));
        private IWebElement ReasonDropdown => driver.FindElement(By.CssSelector("div[data-ui='post-approval-entities-multiselect-reason']"));

        private IWebElement FinalDecisionTab => driver.FindElement(By.CssSelector("//span[text()='Final Decision']"));
        private IWebElement NotificationsTab => driver.FindElement(By.CssSelector("//span[text()='Notifications']"));
        private IWebElement EditButton => driver.FindElement(By.CssSelector("button[data-ui='post-approval-grid-edit-btn']"));
        private IWebElement SaveAndClose => driver.FindElement(By.CssSelector("button[data-ui='post-approval-edit-save-close-btn']"));


        private IWebElement PostApprovalSearchBox => driver.FindElement(By.CssSelector("input[data-ui='search-component-input']"));

        private By NoRecordsAvailable = By.XPath("//td[text()='No records available']");

        public void VerifyPostApprovalPage()
        {
            Wait.ForElementToBeClickable(AddNewPostApproval);
        }

        private void SelectBR()
        {
            ItemNumber = "1";
            I.SelectFromDropdown(BusinessRuleDropdown, DdItems);
        }

        public void AddPostApproval()
        {
            I.Click(AddNewPostApproval);
            Wait.ForTheLoader();
            SelectBR();
        }

    }
}
