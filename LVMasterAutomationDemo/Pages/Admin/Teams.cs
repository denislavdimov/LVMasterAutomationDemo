using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace LVMasterAutomationDemo.Pages
{
    public class Teams : BasePage
    {
        //private readonly IWait _wait;
        private IWait _wait;
        int randomNumber = (int)(new Random().NextInt64(11) + 20);
        public Teams(IWebDriver driver, IWait wait) : base(driver)
        {
            _wait = wait;
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/LVWEB/Admin/#/Teams/";

        private IWebElement NoticeModal => driver.FindElement(By.XPath("//div[@class='k-widget k-window']"));
        private IWebElement NoticeCloseButton => driver.FindElement(By.CssSelector(".k-icon.k-i-close"));
        private IWebElement LinkAdd => driver.FindElement(By.LinkText("Add"));
        public IWebElement TeamsModal => driver.FindElement(By.XPath("//div[@class='k-widget k-window']"));
        private IWebElement EditButton => driver.FindElement(By.XPath("(//a[contains(@class,'v-icon icon-edit k-grid-Edit')])"));
        private IWebElement SearchArea => driver.FindElement(By.XPath("//input[contains(@class,'search-query form-control')]"));
        private IWebElement NameInputField => driver.FindElement(By.XPath("//input[@name='Name']"));
        private IWebElement SaveButton => driver.FindElement(By.XPath("//button[contains(.,'Save')]"));
        private IWebElement UserAssignmentTab => driver.FindElement(By.XPath("//span[contains(.,'User Assignment')]"));
        private IWebElement RoleAssignmentTab => driver.FindElement(By.XPath("//span[contains(.,'Role Assignment')]"));
        private IWebElement ApprovalsTab => driver.FindElement(By.XPath("//span[@unselectable='on'][contains(.,'Approvals')]"));
        private IList<IWebElement> AllAssignedItems => driver.FindElements(By.CssSelector("#assigned > div")).ToList();
        private IList<IWebElement> AllAvailableItems => driver.FindElements(By.CssSelector("#available > div")).ToList();
        private IWebElement AssignedItem => driver.FindElement(By.CssSelector("#assigned > div"));

        public void TeamsPageReady()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            var waitforinvs = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            waitforinvs.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='k-loading-image']")));
            ISeeElement(NoticeModal, By.XPath("//div[@class='k-widget k-window']"));
            IWaitAndClick(NoticeCloseButton);
            waitforinvs.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='k-overlay']")));;
        }

        //public void TeamsPageReadyWithFluentWait()
        //{
        //    DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
        //    fluentWait.Timeout = TimeSpan.FromSeconds(5);
        //    fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
        //    //fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        //    //fluentWait.Message = "Element to be searched not found";
        //    fluentWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='k-loading-image']")));
        //    ISeeElement(NoticeModal, By.XPath("//div[@class='k-widget k-window']"));
        //    IWaitAndClick(NoticeCloseButton);
        //    fluentWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='k-overlay']")));
        //}

        public void AssignUserAndRoleToTeam() 
        {
            //Add verify that assigned column is empty before assigning items
            IWaitAndClick(UserAssignmentTab);
            IWaitAndClick(AllAvailableItems[1]);
            ISeeElements(By.CssSelector("#assigned > div"));
            //IWaitAndClick(RoleAssignmentTab);
            //IWaitAndClick(AllAvailableItems[4]);
            //ISeeElement(AssignedItem, By.CssSelector("#assigned > div"));
        }

        public void AddTeamWithUserAndRole()
        {
            //IGoToThisPageUrlAndCheckIsItOpen();
            //_wait.IWaitPageToLoad();
            //_wait.IWaitForLoader();
            //ISeeElement(NoticeModal, By.XPath("//div[@class='k-widget k-window']"));
            //IWaitAndClick(NoticeCloseButton);
            //IWaitAndClick(LinkAdd);
            TeamsPageReady();
            IWaitAndClick(LinkAdd);
            _wait.IWaitForLoader();
            ISeeElement(TeamsModal, By.XPath("//div[@class='k-widget k-window']"));
            IWaitForElementAndType(NameInputField, "DenisAutomationTeamTest" + randomNumber);
            AssignUserAndRoleToTeam();
            IWaitAndClick(SaveButton);
            AssertThereIsNoErrorAndException();
        }

    }
}
 