using OpenQA.Selenium;

namespace LVMasterAutomationDemo.Pages
{
    public class Teams : BasePage
    {
        private readonly IWait _wait;
        int randomNumber = (int)(new Random().NextInt64(11) + 20);
        public Teams(IWebDriver driver, IWait wait) : base(driver)
        {
            _wait = wait;
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/LVWEB/Admin/#/Teams/";

        private IWebElement noticeModal => driver.FindElement(By.XPath("//div[@class='k-widget k-window']"));
        private IWebElement noticeCloseButton => driver.FindElement(By.CssSelector(".k-icon.k-i-close"));
        private IWebElement linkAdd => driver.FindElement(By.LinkText("Add"));
        public IWebElement teamsModal => driver.FindElement(By.XPath("//div[@class='k-widget k-window']"));
        private IWebElement editButton => driver.FindElement(By.XPath("(//a[contains(@class,'v-icon icon-edit k-grid-Edit')])"));
        private IWebElement searchArea => driver.FindElement(By.XPath("//input[contains(@class,'search-query form-control')]"));
        private IWebElement nameInputField => driver.FindElement(By.XPath("//input[contains(@class,'search-query form-control')]"));
        private IWebElement saveButton => driver.FindElement(By.XPath("//button[contains(.,'Save')]"));
        private IWebElement userAssignmentTab => driver.FindElement(By.XPath("//span[contains(.,'User Assignment')]"));
        private IWebElement roleAssignmentTab => driver.FindElement(By.XPath("//span[contains(.,'Role Assignment')]"));
        private IWebElement approvalsTab => driver.FindElement(By.XPath("//span[@unselectable='on'][contains(.,'Approvals')]"));
        private IList<IWebElement> allAssignedItems => driver.FindElements(By.CssSelector("#assigned > div")).ToList();
        private IList<IWebElement> allAvailableItems => driver.FindElements(By.CssSelector("#available > div")).ToList();
        private IWebElement assignedItem => driver.FindElement(By.CssSelector("#assigned > div"));


        public void AddTeamWithUserAndRole()
        {
            //IGoToThisPageUrlAndCheckIsItOpen();
            //_wait.IWaitPageToLoad();
            ISeeElement(noticeModal, By.XPath("//div[@class='k-widget k-window']"));
            IWaitAndClick(noticeCloseButton);
            IWaitAndClick(linkAdd);
            _wait.IWaitForLoader();
            ISeeElement(teamsModal, By.XPath("//div[@class='k-widget k-window']"));
            IWaitForElementAndType(nameInputField, "DenisAutomationTeamTest" + randomNumber);
            IWaitAndClick(userAssignmentTab);
            IWaitAndClick(allAvailableItems[1]);
            ISeeElement(assignedItem, By.CssSelector("#assigned > div"));
            IWaitAndClick(roleAssignmentTab);
            IWaitAndClick(allAvailableItems[4]);
            ISeeElement(assignedItem, By.CssSelector("#assigned > div"));
            IWaitAndClick(saveButton);
            ISeeNoErrorAndException();
        }

    }
}
 