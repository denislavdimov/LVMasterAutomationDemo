using LVPages.IClasses;
using OpenQA.Selenium;

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
        private IWebElement AboutPresentationReportsLink => driver.FindElement(By.XPath("//a[contains(.,'About Presentation Reports')]"));
        private IWebElement AdminBreadCrumbLink => driver.FindElement(By.XPath("//a[contains(.,'Admin')]"));

        private IWebElement BuilderTab => driver.FindElement(By.CssSelector("span[data-ui='report-definitions-tab-item-title-builder']"));
        private IWebElement AddBuildReportButton => driver.FindElement(By.XPath("//button[contains(.,'Add/Build Report')]"));
        private IWebElement BuilderModalComponentsTab => driver.FindElement(By.CssSelector("span[data-ui='report-builder-components-tab-title']"));
        private IWebElement AddAllComponentsButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-assign-all']"));
        private IList<IWebElement> PreConditionsItems => driver.FindElements
            (By.XPath("//div[contains(@id,'lv-droppable-Prerequisite Conditions')]//div[@class='lv-draggable-item']")).ToList();
        private IWebElement AssignedItemArea => driver.FindElement(By.CssSelector("#lv-droppable-assigned-drop-area"));
        private IWebElement AssignedItems => driver.FindElement
            (By.CssSelector("#lv-droppable-assigned-drop-area div[class='lv-draggable-item']"));
        private IWebElement AssignedItem => driver.FindElement
            (By.XPath("//div[@class='lv-header-right-section']//button"));

        public void VerifyReportDefinitionPage()
        {
            Wait.ForElementToBeClickable(AddNewPresentationReportButton);
            Wait.ForElementToBeClickable(BuilderTab);
            ISeeElement(AboutPresentationReportsLink, By.XPath("//a[contains(.,'About Presentation Reports')]"));
            ISeeElement(AdminBreadCrumbLink, By.XPath("//a[contains(.,'Admin')]"));
            ISeeElements(By.CssSelector("#reportsGrid tr"));
        }

        public IList<string> Components()
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

        public void AddComponentsFromEachSection(IList<string> sections)
        {
            foreach (var item in sections)
            {
                var section = driver.FindElement(By.XPath("//span[contains(.,'" + sections + "')]"));
                I.Click(section);
                I.Click(AddAllComponentsButton);
                ISeeElements(By.CssSelector("#lv-droppable-assigned-drop-area div[class='lv-draggable-item']"));
            }
        }

        public void AddComponentsFromEachSection2(IList<string> Components)
        {
            foreach (var item in Components)
            {
                var section = driver.FindElement(By.XPath("//span[contains(.,'" + item + "')]"));
                I.Click(section);
                I.Click(AddAllComponentsButton);
                ISeeElements(By.CssSelector("#lv-droppable-assigned-drop-area div[class='lv-draggable-item']"));
            }
        }

        public void AddBuildReport()
        {
            I.Click(BuilderTab);
            Wait.ForLoaderToDissaper();
            I.Click(AddBuildReportButton);
            I.Click(BuilderModalComponentsTab);
            AddComponentsFromEachSection2(Components());
        }


    }
}