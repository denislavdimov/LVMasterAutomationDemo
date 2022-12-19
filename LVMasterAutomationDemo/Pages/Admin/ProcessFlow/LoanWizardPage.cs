using LVPages.IClasses;
using OpenQA.Selenium;
using System.Security.Cryptography.X509Certificates;

namespace LVPages.Pages.Admin.ProcessFlow
{
    public class LoanWizardPage : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;
        public LoanWizardPage(IWebDriver driver) : base(driver)
        {
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }

        public override string PageUrl => base.PageUrl + "lvadmin/#/Loan-Wizard/";

        private IWebElement AddLoanWizard => driver.FindElement(By.CssSelector("button[data-ui='loan-wizard-add']"));
        private IWebElement SearchBox => driver.FindElement(By.CssSelector("input[data-ui='search-component-input']"));
        private IWebElement NoWizardSelected => driver.FindElement(By.XPath("//p[contains(.,'No Wizard Selected')]"));
        private IWebElement Save => driver.FindElement(By.CssSelector("button[data-ui='loan-wizard-modify-page-save']"));
        private IWebElement DeleteButton => driver.FindElement(By.CssSelector("button[data-ui='loan-wizard-modify-page-delete']"));
        private IWebElement YesButton => driver.FindElement(By.CssSelector("button[data-ui='loan-wizard-modify-page-accept-delete']"));
        private IWebElement LoanWizardTitle => driver.FindElement(By.CssSelector("input[data-ui='loan-wizard-setup-tab-title']"));
        private IWebElement IsActiveCheckbox => driver.FindElement(By.CssSelector("input[name='IsActive']"));
        private IWebElement IncludePFSCheckbox => driver.FindElement(By.CssSelector("input[name='IsIncludePFS']"));
        private IWebElement IncludeReviewCheckbox => driver.FindElement(By.CssSelector("IsIncludeReview"));
        private IList<IWebElement> GridItems => driver.FindElements(By.XPath("//tr[contains(@class,'k-master-row')]")).ToList();
        private IWebElement WizardName => driver.FindElement(By.XPath("//td[contains(@data-ui,'field-wizard-name')]"));
        private IWebElement SetupTab => driver.FindElement(By.XPath("//span[contains(.,'Party Information')]"));
        private IWebElement RoleAssignmentTab => driver.FindElement(By.XPath("//span[contains(.,'Role Assignment')]"));
        private IWebElement AddAllButton => driver.FindElement(By.CssSelector("button[data-ui='drag-n-drop-add-all-button']"));

        private By LoanWizardContent = By.CssSelector("div[data-ui='loan-wizard-modify-page-setup-tab-content']");
        private By SaveButton = By.CssSelector("button[data-ui='loan-wizard-modify-page-save']");
        private By SuccessMessage = By.XPath("//div[text()='Changes saved successfully.']");
        private By AssignedItems = By.CssSelector("#lv-droppable-AssignedArea i");
        private By NoRecordsAvailable = By.XPath("//td[text()='No records available']");

        List<string> LWDropdowns = new List<string>()
            {
                "setup-tab-process-definition",
                "setup-tab-file-page-layout-label",
                "setup-tab-set-facility-status-",
                "party-information-tab-page-field-templates",
                "loan-request-tab-page-field-templates",
                "compliance-tab-page-field-templates",
                "collateral-tab-page-field-templates"
            };

        public void PopulateAllLWDropdowns(List<string> LoanWizardDropdowns)
        {
            foreach (var dropdown in LWDropdowns)
            {
                if (dropdown == LWDropdowns[3])
                {
                    var PartyInfTab = driver.FindElement(By.XPath("//span[contains(.,'Party Information')]"));
                    I.Click(PartyInfTab);
                    Wait.ForTheLoader();
                }
                else if (dropdown == LWDropdowns[4])
                {
                    var LRTab = driver.FindElement(By.XPath("//span[contains(.,'Loan Request')]"));
                    I.Click(LRTab);
                    Wait.ForTheLoader();
                }
                else if(dropdown == LWDropdowns[5])
                {
                    var ComplianceTab = driver.FindElement(By.XPath("//span[contains(.,'Compliance')]"));
                    I.Click(ComplianceTab);
                    Wait.ForTheLoader();
                }
                else if(dropdown == LWDropdowns[6])
                {
                    var CollateralTab = driver.FindElement(By.XPath("//span[contains(.,'Collateral')]"));
                    I.Click(CollateralTab);
                    Wait.ForTheLoader();
                }
                var lwDropdown = driver.FindElement(By.XPath($"//div[@class='lv-select-wrapper'][contains(@data-ui,'loan-wizard-{dropdown}')]"));
                I.SelectItemFromDropdown(lwDropdown, 2);
            }
        }

        public void CreateLoanWizard()
        {
            I.Click(AddLoanWizard);
            Wait.ForTheLoader();
            Wait.ToSee(LoanWizardContent);
            Wait.ToSee(SaveButton);
            I.FillInField(LoanWizardTitle, $"DDAutoLoanWizard{randomNumber}");
            //I.Click(IsActiveCheckbox);
            PopulateAllLWDropdowns(LWDropdowns);
            I.Click(Save);
            Wait.ForAjax();
            Wait.ForTheLoader();
            Wait.ToSee(SuccessMessage);
        }

        public void EditLoanWizard()
        {
            I.FillInField(SearchBox, $"DDAutoLoanWizard{randomNumber}");
            SearchBox.SendKeys(Keys.Enter);
            Wait.ForItemInTheGrid(GridItems.Count, 1);
            I.Click(WizardName);
            Wait.ForTheLoader();
            //I.Click(SetupTab);
            //I.Click(IncludePFSCheckbox);
            //I.Click(IncludeReviewCheckbox);
            I.Click(RoleAssignmentTab);
            Wait.ForTheLoader();
            I.Click(AddAllButton);
            Wait.ForElements(AssignedItems);
            I.Click(Save);
            Wait.ForAjax();
            Wait.ForTheLoader();
            Wait.ToSee(SuccessMessage);
        }

        public void DeleteLoanWizard()
        {
            Wait.ForNoElement(SuccessMessage);
            I.Click(DeleteButton);
            I.Click(YesButton);
            Wait.ForAjax();
            Wait.ForTheLoader();
            Wait.ToSee(SuccessMessage);
            Wait.ToSee(NoRecordsAvailable);
        }
    }
}
