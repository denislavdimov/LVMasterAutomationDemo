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
        private IList<IWebElement> PreConditionsItems => driver.FindElements
            (By.XPath("//div[contains(@id,'lv-droppable-Prerequisite Conditions')]//div[@class='lv-draggable-item']")).ToList();
        private IWebElement AssignedItemArea => driver.FindElement(By.CssSelector("#lv-droppable-assigned-drop-area"));
        private IWebElement AssignedItemAreaXpath => driver.FindElement
            (By.XPath("//*[text()='Drop items here.']"));
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

        public void AddBuildReport()
        {
            I.Click(BuilderTab);
            I.Click(AddBuildReportButton);
            I.Click(BuilderModalComponentsTab);
            ISeeElements(By.XPath("//div[contains(@id,'lv-droppable-Prerequisite Conditions')]//div[@class='lv-draggable-item']"));
            IDragAndDrop(PreConditionsItems[0], AssignedItemArea);
            IDragAndDrop(PreConditionsItems[1], AssignedItemAreaXpath);
        }


    }
}