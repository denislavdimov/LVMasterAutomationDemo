using LVPages.IClasses;
using OpenQA.Selenium;

namespace LVPages.Pages.Admin.ProcessFlow
{
    public class ReportDefinitionPage : BasePage
    {
        private readonly IWait Wait;
        private readonly IUserActions I;
        public ReportDefinitionPage(IWebDriver driver, IWait wait) : base(driver)
        {
            Wait = wait;
            I = new UserActions(driver);
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/lvadmin/#/Define-Reports/";
        private IWebElement BuilderTab => driver.FindElement(By.CssSelector("span[data-ui='report-definitions-tab-item-title-builder']"));
        private IWebElement AddBuildReportButton => driver.FindElement(By.XPath("//button[contains(.,'Add/Build Report')]"));
        private IWebElement BuilderModalComponentsTab => driver.FindElement(By.CssSelector("span[data-ui='report-builder-components-tab-title']"));
        private IList<IWebElement> PreConditionsItems => driver.FindElements
            (By.XPath("//div[contains(@id,'lv-droppable-Prerequisite Conditions')]//div[@class='lv-draggable-item']")).ToList();
        private IWebElement AssignedItemArea => driver.FindElement(By.CssSelector("#lv-droppable-assigned-drop-area"));
        private IWebElement AssignedItemAreaXpath => driver.FindElement
            (By.XPath("//*[text()='Drop items here.']"));

        public void AddBuildReport()
        {
            Wait.ForPageToLoad();
            IClick(BuilderTab);
            IClick(AddBuildReportButton);
            //Wait.ForAjax();
            IClick(BuilderModalComponentsTab);
            ISeeElements(By.XPath("//div[contains(@id,'lv-droppable-Prerequisite Conditions')]//div[@class='lv-draggable-item']"));
            IDragAndDrop(PreConditionsItems[0], AssignedItemArea);
            IDragAndDrop(PreConditionsItems[1], AssignedItemAreaXpath);
        }


    }
}