using OpenQA.Selenium;

namespace LVPages.Pages.Admin.UserAccess
{
    public class Roles : BasePage
    {
        private readonly IWait _wait;
        int randomNumber = (int)(new Random().NextInt64(2022) + 20);
        public Roles(IWebDriver driver, IWait wait) : base(driver)
        {
            _wait = wait;
        }
        public override string PageUrl => "https://loanvantage.dev/IBS/master/LVWEB/Admin/#/Roles/";
        private IWebElement NameInputField => driver.FindElement(By.XPath("//input[@name='Name']"));
        private IWebElement LinkAdd => driver.FindElement(By.LinkText("Add"));
        private IWebElement RoleModal => driver.FindElement(By.XPath("//div[@class='k-widget k-window']"));
        private IWebElement SearchArea => driver.FindElement(By.XPath("//input[contains(@class,'search-query form-control')]"));
        private IWebElement DeleteButton => driver.FindElement(By.XPath("//button[contains(.,'Delete')]"));
        private IWebElement AddAllLink => driver.FindElement(By.CssSelector("a[data-bind='click: addAll'] strong"));
        private IWebElement SaveButton => driver.FindElement(By.XPath("//button[contains(.,'Save')]"));
        private IWebElement AvailableColumn => driver.FindElement(By.Id("available"));
        private IWebElement AssignedColumn => driver.FindElement(By.Id("assigned"));
        private IWebElement AvailableItem => driver.FindElement(By.CssSelector("#available > div"));
        private IWebElement AssignedItem => driver.FindElement(By.CssSelector("#assigned > div"));
        private IWebElement ConfirmationDialog => driver.FindElement(By.CssSelector(".confimation-dialog h5"));
        private IWebElement YesButton => driver.FindElement(By.XPath("//button[contains(.,'Yes')]"));
        private IWebElement EditButton => driver.FindElement(By.XPath("(//a[contains(@class,'v-icon icon-edit k-grid-Edit')])"));
        private IList<IWebElement> AssignedItems => driver.FindElements(By.CssSelector("#assigned > div")).ToList();
        private IList<IWebElement> AvailableItems => driver.FindElements(By.CssSelector("#available > div")).ToList();


        public void AddRole()
        {
            IWaitAndClick(LinkAdd);
            _wait.WaitForAjax();
            ISeeElement(RoleModal, By.XPath("//div[@class='k-widget k-window']"));
            ISeeElements(By.CssSelector("#available > div"));
            IWaitForElementAndType(NameInputField, "DenisAutomationRoleTest" + randomNumber);
            IWaitAndClick(AddAllLink);
            ISeeElements(By.CssSelector("#assigned > div"));
            IWaitAndClick(SaveButton);
            _wait.WaitForAjax();
            AssertThereIsNoErrorAndException();
        }

        public void DeleteRole()
        {
            IWaitForElementAndType(SearchArea, "DenisAutomationRole");
            IWaitAndClick(EditButton);
            _wait.WaitForAjax();
            ISeeElement(RoleModal, By.XPath("//div[@class='k-widget k-window']"));
            ISeeElements(By.CssSelector("#assigned > div"));
            IWaitAndClick(DeleteButton);
            ISeeElement(ConfirmationDialog, By.CssSelector(".confimation-dialog h5"));
            IWaitAndClick(YesButton);
            _wait.WaitForAjax();
            AssertThereIsNoErrorAndException();
        }
    }
}
