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

        private IList<IWebElement> GridItems => driver.FindElements(By.XPath("//tr[contains(@class,'k-master-row')]")).ToList();
        private IWebElement AddNewPostApproval => driver.FindElement(By.CssSelector("button[data-ui='post-approval-toolbar-add-btn']"));
        private IWebElement TestPostApproval => driver.FindElement(By.CssSelector("button[data-ui='post-approval-toolbar-test-btn']"));
        private IWebElement PostApprovalName => driver.FindElement(By.CssSelector("input[data-ui='post-approval-details-input-name']"));
        private IWebElement BusinessRuleDropdown => driver.FindElement(By.CssSelector("div[data-ui='post-approval-details-select-business-rule']"));
        private IWebElement MinTeamsRequiredToApprove => driver.FindElement(By.CssSelector("input[name='MinTeamsRequiredToApprove']"));
        private IWebElement IsActiveCheckbox => driver.FindElement(By.CssSelector("label[data-ui='post-approval-details-checkbox-is-active']"));
        private IWebElement PostApprovalEntitiesTab => driver.FindElement(By.XPath("//span[text()='Post-Approval Entities']"));
        private IWebElement AddTeam => driver.FindElement(By.CssSelector("button[data-ui='post-approval-entities-add-team-btn']"));
        private IWebElement ApproveOnAmount => driver.FindElement(By.CssSelector("input[name='ApprovalLimitAmt']"));
        private IWebElement MembersRequired => driver.FindElement(By.CssSelector("input[name='NumberOfUsersRequired']"));
        private IWebElement ReasonDropdown => driver.FindElement(By.CssSelector("div[data-ui='post-approval-entities-multiselect-reason']"));
        private IWebElement FinalDecisionTab => driver.FindElement(By.CssSelector("//span[text()='Final Decision']"));
        private IWebElement NotificationsTab => driver.FindElement(By.CssSelector("//span[text()='Notifications']"));
        private IWebElement EditButton => driver.FindElement(By.CssSelector("button[data-ui='post-approval-grid-edit-btn']"));
        private IWebElement SaveAndClose => driver.FindElement(By.CssSelector("button[data-ui='post-approval-edit-save-close-btn']"));
        private IWebElement DeleteButton => driver.FindElement(By.CssSelector("button[data-ui='post-approval-entities-grid-delete-btn']"));
        private IWebElement PostApprovalSearchBox => driver.FindElement(By.CssSelector("input[data-ui='search-component-input']"));

        private By SearchBox = By.CssSelector("input[data-ui='search-component-input']");
        private By NoRecordsAvailable = By.XPath("//td[text()='No records available']");
        private By ReasonDropdownValues = By.XPath("//div[contains(@class,'multiValue')]");
        private By DeleteTeamButton = By.CssSelector("button[data-ui='post-approval-entities-grid-delete-btn']");

        List<string> PAEDropdowns = new List<string>()
            {
                "business-rule",
                "select-team",
                "select-team-router",
                "select-approve-on"
            };

        public void VerifyPostApprovalPage()
        {
            Wait.ForElementToBeClickable(AddNewPostApproval);
            Wait.ForElementToBeClickable(TestPostApproval);
            Wait.ToSee(SearchBox);
        }

        public void PopulateDropdowns(List<string> paeDropdowns)
        {
            foreach (var dropdown in paeDropdowns)
            {
                int randomItem = new Random().Next(2, 5);
                var dropdowns = driver.FindElement(By.CssSelector($"div[data-ui='post-approval-entities-{dropdown}']"));
                I.SelectItemFromDropdown(dropdowns, randomItem);
                var dropdownValues = By.XPath($"//div[@data-ui='post-approval-entities-{dropdown}']//div[contains(@class,'single-value')]");
                Wait.ToSee(dropdownValues);
            }
            I.SelectItemFromDropdown(ReasonDropdown, 0);
            Wait.ToSee(ReasonDropdownValues);
        }

        public void AddPostApproval()
        {
            I.Click(AddNewPostApproval);
            Wait.ForTheLoader();
            I.FillInField(PostApprovalName, $"DDAutoPostApproval{randomNumber}");
            I.SelectItemFromDropdown(BusinessRuleDropdown, 3);
            I.FillInField(MinTeamsRequiredToApprove, "1");
            I.Click(PostApprovalEntitiesTab);
            Wait.ForTheLoader();
            Wait.ToSee(NoRecordsAvailable);
            I.Click(AddTeam);
            Wait.ToSee(DeleteTeamButton);
            PopulateDropdowns(PAEDropdowns);
            I.FillInField(ApproveOnAmount, "100000");
            I.Click(SaveAndClose);
            Wait.ForAjax();
            Wait.ForTheLoader();
        }

        public void EditPostApproval()
        {
            I.FillInField(PostApprovalSearchBox, $"DDAutoPostApproval{randomNumber}");
            PostApprovalSearchBox.SendKeys(Keys.Enter);
            Wait.ForItemInTheGrid(GridItems.Count, 1);
            I.Click(EditButton);
            Wait.ForTheLoader();
            I.FillInField(MinTeamsRequiredToApprove, "2");
            I.Click(IsActiveCheckbox);
            I.Click(PostApprovalEntitiesTab);
            Wait.ForTheLoader();
            Wait.ForNoElement(NoRecordsAvailable);
            Wait.ToSee(ReasonDropdownValues);
            I.SelectItemFromDropdown(ReasonDropdown, 1);
            I.Click(SaveAndClose);
            Wait.ForAjax();
            Wait.ForTheLoader();
            Wait.ToSee(NoRecordsAvailable);
        }

    }
}
