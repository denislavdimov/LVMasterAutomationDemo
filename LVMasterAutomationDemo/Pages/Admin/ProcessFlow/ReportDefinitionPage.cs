using LVPages.IClasses;
using OpenQA.Selenium;
using System.Diagnostics;

namespace LVPages.Pages.Admin.ProcessFlow
{
    public class ReportDefinitionPage : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;

        public ReportDefinitionPage(IWebDriver driver) : base(driver)
        {
            Wait = new Wait(driver);
            I = new UserActions(driver);
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/lvadmin/#/Define-Reports/";

        //private IList<IWebElement> PresentationGridElements => driver.FindElements(By.CssSelector("#reportsGrid tr")).ToList();
        private IWebElement AddNewPresentationReportButton => driver.FindElement(By.XPath("//button[contains(.,'Add New Presentation Report')]"));
        private IWebElement BuilderTab => driver.FindElement(By.CssSelector("span[data-ui='report-definitions-tab-item-title-builder']"));
        private IList<IWebElement> BuilderReports => driver.FindElements(By.CssSelector("#report-builder-tab div[class='k-grid-container'] tr")).ToList();
        private IWebElement BuilderSearchBox => driver.FindElement(By.CssSelector("div[data-ui='report-builder-tab-toolbar'] div input[name='searchBox']"));
        private IWebElement AddBuildReportButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-add']"));
        private IWebElement EditBuildReportButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-modify-item']"));
        private IWebElement DeleteBuildReportButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-delete-item']"));
        private IWebElement DeleteBuildReportYesButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-delete-yes']"));
        private IWebElement BuildReportName => driver.FindElement(By.CssSelector("input[name='Name']"));
        private IWebElement BuildReportIsActiveCheckbox => driver.FindElement(By.CssSelector("label[data-ui='report-builder-edit-is-active']"));
        private IWebElement BuildReportIsBoardingCheckbox => driver.FindElement(By.CssSelector("label[data-ui='report-builder-edit-is-boarding']"));
        private IWebElement BuilderModalComponentsTab => driver.FindElement(By.CssSelector("span[data-ui='report-builder-components-tab-title']"));
        private IWebElement AddAllBuilderComponentsButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-assign-all']"));
        private IWebElement RemoveAllBuilderComponentsButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-move-all-to-available']"));
        private IWebElement SaveBuildReportButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-save']"));

        private By AllAssignedBuildReportComponents = By.CssSelector("#lv-droppable-assigned-drop-area div[class='lv-draggable-item']");
        private By AllAvailableBuildReportComponents = By.CssSelector("div[data-ui='report-builder-drop-area'] div[class='lv-draggable-item']");
        private By AdminBreadCrumbLink = By.XPath("//a[contains(.,'Admin')]");
        private By AboutPresentationReportsLink = By.XPath("//a[contains(.,'About Presentation Reports')]");
        private By WarningMessage = By.XPath("//div[contains(@class,'Toastify__toast Toastify__toast--warning')]");

        private IWebElement SectionTab => driver.FindElement(By.CssSelector("span[data-ui=report-definitions-tab-title-sections]"));
        private IWebElement SectionTabSearchBox => driver.FindElement(By.CssSelector("#report-builder-sections-tab input[data-ui='search-component-input'] "));
        private IList<IWebElement> SectionReports => driver.FindElements(By.CssSelector("#report-builder-sections-tab div[class='k-grid-container'] tr")).ToList();
        private IWebElement AddSectionButton => driver.FindElement(By.CssSelector("button[data-ui=report-builder-sections-add]"));
        private IWebElement EditSectionButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-sections-select-item']"));
        private IWebElement DeleteSectionButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-sections-delete-item']"));
        private IWebElement DeleteSectionYesButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-sections-delete-yes']"));
        private IWebElement SectionCode => driver.FindElement(By.CssSelector("input[data-ui='report-builder-add-edit-code']"));
        private IWebElement SectionDescription => driver.FindElement(By.CssSelector("input[data-ui='report-builder-add-edit-description']"));
        private IWebElement SectionComponentsTab => driver.FindElement(By.CssSelector("span[data-ui='report-builder-add-edit-tab-title-section']"));
        private IWebElement AddAllSectionComponentsButton => driver.FindElement(By.CssSelector("button[data-ui='drag-n-drop-add-all-button']"));
        private IWebElement SaveSectionButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-add-edit-add-or-update']"));
        private IWebElement FacilityCheckbox => driver.FindElement(By.CssSelector("label[data-ui='report-builder-add-edit-facility']"));

        private By AllSectionAssignedComponents = By.CssSelector("#lv-droppable-AssignedArea div[class='lv-draggable-item']");

        public IList<string> BuilderComponents()
        {
            List<string> componentslist = new List<string>()
            {
                "Prerequisite Conditions",
                "Covenants",
                "Relationship Information",
                "Other",
                "Policy Exceptions",
                "Guarantors",
                "Loans Summary",
                "Recurring Documents",
                "Beneficial Ownership",
                "Recommendations"
            };
            return componentslist.AsReadOnly();
        }

        public void VerifyReportDefinitionPage()
        {
            Wait.ForElementToBeClickable(AddNewPresentationReportButton);
            Wait.ForElementToBeClickable(BuilderTab);
            Wait.ForElementToBeClickable(SectionTab);
            Wait.ToSeeElement(AboutPresentationReportsLink);
            Wait.ToSeeElement(AdminBreadCrumbLink);
        }

        public void AddComponentsFromEachSection(IList<string> Components)
        {
            foreach (var item in Components)
            {
                var section = driver.FindElement(By.XPath($"//span[contains(.,'{item}')]"));
                I.Click(section);
                I.Click(AddAllBuilderComponentsButton);
                Wait.ToSeeElements(AllAssignedBuildReportComponents);
            }
        }

        public void AddBuildReport()
        {
            I.Click(BuilderTab);
            Wait.ForLoaderToDissaper();
            I.Click(AddBuildReportButton);
            Wait.ForAjax();
            I.FillInField(BuildReportName, $"DDAutomationBuildReport{randomNumber}");
            I.Click(BuildReportIsActiveCheckbox);
            I.Click(BuilderModalComponentsTab);
            AddComponentsFromEachSection(BuilderComponents());
            I.Click(SaveBuildReportButton);
            Wait.ForAjax();
            Wait.ForLoaderToDissaper();
        }

        public void EditBuildReport()
        {
            I.FillInField(BuilderSearchBox, $"DDAutomationBuildReport{randomNumber}");
            BuilderSearchBox.SendKeys(Keys.Enter);
            Wait.ForItemInTheGrid(BuilderReports.Count, 1);
            I.Click(EditBuildReportButton);
            Wait.ForAjax();
            I.Click(BuildReportIsBoardingCheckbox);
            I.Click(BuilderModalComponentsTab);            
            Wait.ToSeeElements(AllAssignedBuildReportComponents);
            I.Click(RemoveAllBuilderComponentsButton);
            Wait.ToSeeElements(AllAvailableBuildReportComponents);
            I.Click(SaveBuildReportButton);
            Wait.ToSeeElement(WarningMessage);
            I.Click(AddAllBuilderComponentsButton);
            Wait.ToSeeElements(AllAssignedBuildReportComponents);
            I.Click(SaveBuildReportButton);
            Wait.ForAjax();
            Wait.ForLoaderToDissaper();
        }

        public void DeleteBuildReport()
        {
            I.Click(DeleteBuildReportButton);
            I.Click(DeleteBuildReportYesButton);
            Wait.ForAjax();
            Wait.ForLoaderToDissaper();
        }

        public void AddSection()
        {
            I.Click(SectionTab);
            Wait.ForLoaderToDissaper();
            I.Click(AddSectionButton);
            Wait.ForAjax();
            I.FillInField(SectionCode, $"DDAuto{randomNumber}");
            I.FillInField(SectionDescription, $"DDAutomationSection{randomNumber}");
            I.Click(SectionComponentsTab);
            I.Click(AddAllSectionComponentsButton);
            Wait.ToSeeElements(AllSectionAssignedComponents);
            I.Click(SaveSectionButton);
            Wait.ForAjax();
            Wait.ForLoaderToDissaper();
        }

        public void EditSection()
        {
            I.FillInField(SectionTabSearchBox, $"DDAuto{randomNumber}");
            SectionTabSearchBox.SendKeys(Keys.Enter);
            Wait.ForItemInTheGrid(SectionReports.Count, 1);
            I.Click(EditSectionButton);
            Wait.ForAjax();
            I.Click(FacilityCheckbox);
            I.Click(SectionComponentsTab);
            I.Click(SaveSectionButton);
            Wait.ToSeeElement(WarningMessage);
            I.Click(AddAllSectionComponentsButton);
            I.Click(SaveSectionButton);
            Wait.ForAjax();
            Wait.ForLoaderToDissaper();
        }

        public void DeleteSection()
        {
            I.Click(DeleteSectionButton);
            I.Click(DeleteSectionYesButton);
            Wait.ForAjax();
            Wait.ForLoaderToDissaper();
        }
    }
}