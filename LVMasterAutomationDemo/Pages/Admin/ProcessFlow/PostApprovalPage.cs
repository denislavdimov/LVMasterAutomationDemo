using LVPages.IClasses;
using OpenQA.Selenium;

namespace LVPages.Pages.Admin.ProcessFlow
{
    public class PostApprovalPage : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;

        public PostApprovalPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }

        public override string PageUrl => base.PageUrl + "lvadmin/#/Post-Approvals/";

        private IList<IWebElement> GridItems => driver.FindElements(By.XPath("//tr[contains(@class,'k-master-row')]")).ToList();
        private IWebElement AddNewPostApproval => driver.FindElement(By.CssSelector("button[data-ui='post-approval-toolbar-add-btn']"));
        private IWebElement TestPostApproval => driver.FindElement(By.CssSelector("button[data-ui='post-approval-toolbar-test-btn']"));
        private IWebElement PostApprovalName => driver.FindElement(By.CssSelector("input[data-ui='post-approval-details-input-name']"));
        private IWebElement BusinessRuleDropdown => driver.FindElement(By.CssSelector("div[data-ui='post-approval-details-select-business-rule']"));
        private IWebElement MinTeamsRequiredToApprove => driver.FindElement(By.CssSelector("input[name='MinTeamsRequiredToApprove']"));
        private IWebElement IsActiveCheckbox => driver.FindElement(By.CssSelector("label[data-ui='post-approval-details-checkbox-is-active']"));
        private IWebElement PostApprovalEntitiesTab => driver.FindElement(By.XPath("//span[text()='Post-Approval Entities']"));
        private IWebElement AddTeam => driver.FindElement(By.CssSelector("button[data-ui='post-approval-entities-add-team-btn']"));
        private IList<IWebElement> ApproveOnAmount => driver.FindElements(By.CssSelector("input[name='ApprovalLimitAmt']")).ToList();
        private IWebElement MembersRequired => driver.FindElement(By.CssSelector("input[name='NumberOfUsersRequired']"));
        private IList<IWebElement> ReasonDropdown => driver.FindElements(By.CssSelector("div[data-ui='post-approval-entities-multiselect-reason']")).ToList();
        private IWebElement EditButton => driver.FindElement(By.CssSelector("button[data-ui='post-approval-grid-edit-btn']"));
        private IWebElement SaveAndClose => driver.FindElement(By.CssSelector("button[data-ui='post-approval-edit-save-close-btn']"));
        private IWebElement DeleteButton => driver.FindElement(By.CssSelector("button[data-ui='post-approval-entities-grid-delete-btn']"));
        private IWebElement PostApprovalSearchBox => driver.FindElement(By.CssSelector("input[data-ui='search-component-input']"));

        private By SearchBox = By.CssSelector("input[data-ui='search-component-input']");
        private By NoRecordsAvailable = By.XPath("//td[text()='No records available']");
        private By ReasonDropdownValues = By.XPath("//div[contains(@class,'multiValue')]");
        private By DeleteButton2ndRow = By.XPath("//td[contains(@data-ui,'row-2')]/button[contains(@data-ui,'grid-delete-btn')]");

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
            AssertDriverUrlIsEqualToPageUrl();
        }

        public void PopulateGridRows(List<string> paeDropdowns)
        {
            foreach (var dropdown in paeDropdowns)
            {
                int randomItem = new Random().Next(2, 5);
                var dropdowns1Row = driver.FindElements(By.CssSelector($"div[data-ui='post-approval-entities-{dropdown}']")).ToList();
                I.SelectItemFromDropdown(dropdowns1Row[0], randomItem);
                var dropdownValues1Row = By.XPath(
                    $"//td[contains(@data-ui,'row-1')]/div[@data-ui='post-approval-entities-{dropdown}']//div[contains(@class,'single-value')]");
                Wait.ToSee(dropdownValues1Row);
            }
            I.SelectItemFromDropdown(ReasonDropdown[0], 0);
            Wait.ToSee(ReasonDropdownValues);
            foreach (var dropdown2Row in paeDropdowns)
            {
                int randomItem2 = new Random().Next(2, 5);
                var dropdowns2Row = driver.FindElements(By.CssSelector($"div[data-ui='post-approval-entities-{dropdown2Row}']")).ToList();
                I.SelectItemFromDropdown(dropdowns2Row[1], randomItem2);
                var dropdownValues2Row = By.XPath(
                    $"//td[contains(@data-ui,'row-2')]/div[@data-ui='post-approval-entities-{dropdown2Row}']//div[contains(@class,'single-value')]");
                Wait.ToSee(dropdownValues2Row);
            }
            I.SelectItemFromDropdown(ReasonDropdown[1], 0);
            I.FillInField(ApproveOnAmount[0], "100000");
            I.Click(ApproveOnAmount[1]);
            I.FillInField(ApproveOnAmount[1], "150000");
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
            I.Click(AddTeam);
            PopulateGridRows(PAEDropdowns);
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
            I.SelectItemFromDropdown(ReasonDropdown[0], 1);
            I.Click(DeleteButton);
            Wait.ForNoElement(DeleteButton2ndRow);
            I.Click(SaveAndClose);
            Wait.ForAjax();
            Wait.ForTheLoader();
            Wait.ToSee(NoRecordsAvailable);
        }

    }
}
