using LVPages.IClasses;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V105.Overlay;

namespace LVPages.Pages.Admin.ProcessFlow
{
    public class ReportDefinitionPage : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;
        int randomNumber = (int)(new Random().NextInt64(2022) + 20);
        int NewRandomNumber = (int)(new Random().NextInt64(2022) + 20);
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
        private IList<IWebElement> BuilderReports => driver.FindElements(By.CssSelector("#report-builder-tab div[class='k-grid-container'] tr")).ToList();
        private IWebElement SearchArea => driver.FindElement(By.CssSelector("div[data-ui='report-builder-tab-toolbar'] div input[name='searchBox']"));
        private IWebElement AddBuildReportButton => driver.FindElement(By.XPath("//button[contains(.,'Add/Build Report')]"));
        private IWebElement EditButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-modify-item']"));
        private IWebElement DeleteButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-delete-item']"));
        private IWebElement DeleteYesButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-delete-yes']"));
        private IWebElement InputFieldName => driver.FindElement(By.CssSelector("input[name='Name']"));
        private IWebElement IsActiveCheckbox => driver.FindElement(By.CssSelector("input[name='IsActive'],.input[type='checkbox']"));
        private IWebElement IsBoardingCheckbox => driver.FindElement(By.CssSelector("input[name='IsBoarding'],.input[type='checkbox']"));
        private IWebElement BuilderModalComponentsTab => driver.FindElement(By.CssSelector("span[data-ui='report-builder-components-tab-title']"));
        private IWebElement AddAllComponentsButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-assign-all']"));
        private IWebElement RemoveAllComponentsButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-move-all-to-available']"));
        private IWebElement SaveButton => driver.FindElement(By.CssSelector("button[data-ui='report-builder-save']"));
        private IWebElement AssignedItemArea => driver.FindElement(By.CssSelector("#lv-droppable-assigned-drop-area"));
        private IWebElement AvailableItems => driver.FindElement(By.CssSelector("div[data-ui='report-builder-drop-area'] div[class='lv-draggable-item']"));
        private IWebElement AssignedItems => driver.FindElement
            (By.CssSelector("#lv-droppable-assigned-drop-area div[class='lv-draggable-item']"));
        private IWebElement AssignedItem => driver.FindElement
            (By.XPath("//div[@class='lv-header-right-section']//button"));
        private IWebElement WarningMsg => driver.FindElement(By.XPath("//div[contains(@class,'Toastify__toast Toastify__toast--warning')]"));
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

        //public IList<string> BoardingComponents()
        //{
        //    List<string> BoardingComponentsList = new List<string>()
        //    {
        //        "Prerequisite Conditions",
        //        "Covenants",
        //        "Relationship Information",
        //        "Other",
        //        "Policy Exceptions",
        //    };
        //    return BoardingComponentsList.AsReadOnly();
        //}

        public void VerifyReportDefinitionPage()
        {
            Wait.ForElementToBeClickable(AddNewPresentationReportButton);
            Wait.ForElementToBeClickable(BuilderTab);
            ISeeElement(AboutPresentationReportsLink, By.XPath("//a[contains(.,'About Presentation Reports')]"));
            ISeeElement(AdminBreadCrumbLink, By.XPath("//a[contains(.,'Admin')]"));
        }

        public void AddComponentsFromEachSection(IList<string> Components)
        {
            foreach (var item in Components)
            {
                var section = driver.FindElement(By.XPath($"//span[contains(.,'{item}')]"));
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
            Wait.ForLoaderToDissaper();
            I.FillInField(InputFieldName, $"BuildReport{randomNumber}");
            //I.Click(IsActiveCheckbox);
            IsActiveCheckbox.Click();
            I.Click(BuilderModalComponentsTab);
            AddComponentsFromEachSection(Components());
            I.Click(SaveButton);
            //Wait.ForAjax();
            Wait.ForLoaderToDissaper();
            //AssertThereIsNoErrorAndException();
        }

        public void EditBuildReport()
        {
            I.FillInField(SearchArea, $"BuildReport{randomNumber}");
            SearchArea.SendKeys(Keys.Enter);
            Wait.ForItemInTheGrid(BuilderReports.Count, 1);
            I.Click(EditButton);
            Wait.ForLoaderToDissaper();
            //I.Click(IsBoardingCheckbox);
            IsBoardingCheckbox.Click();
            I.Click(BuilderModalComponentsTab);
            ISeeElements(By.CssSelector("#lv-droppable-assigned-drop-area div[class='lv-draggable-item']"));
            I.Click(RemoveAllComponentsButton);
            ISeeElements(By.CssSelector("div[data-ui='report-builder-drop-area'] div[class='lv-draggable-item']"));
            I.Click(SaveButton);
            ISeeElement(WarningMsg, By.XPath("//div[contains(@class,'Toastify__toast Toastify__toast--warning')]"));
            I.Click(AddAllComponentsButton);
            ISeeElements(By.CssSelector("#lv-droppable-assigned-drop-area div[class='lv-draggable-item']"));
            I.Click(SaveButton);
            //Wait.ForAjax();
            Wait.ForLoaderToDissaper();
        }

        public void DeleteBuildReport()
        {
            I.Click(DeleteButton);
            I.Click(DeleteYesButton);
            Wait.ForLoaderToDissaper();
        }

    }
}