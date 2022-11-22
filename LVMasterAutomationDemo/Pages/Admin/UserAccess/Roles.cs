using NUnit.Framework;
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
        private IWebElement ConfirmationDialog => driver.FindElement(By.CssSelector(".confimation-dialog h5"));
        private IWebElement YesButton => driver.FindElement(By.XPath("//button[contains(.,'Yes')]"));
        private IWebElement EditButton => driver.FindElement(By.XPath("(//a[contains(@class,'v-icon icon-edit k-grid-Edit')])"));
        private IList<IWebElement> AvailableItems => driver.FindElements(By.CssSelector("#admin-menu-role-edit #available div")).ToList();
        private IList<IWebElement> AssignedItems => driver.FindElements(By.CssSelector("#admin-menu-role-edit #assigned div")).ToList();

        public void VerifyRolesPage()
        {
            _wait.IWaitForElementToBeClickable(LinkAdd);
            ISeeElement(SearchArea, By.XPath("//input[contains(@class,'search-query form-control')]"));
            ISeeElements(By.CssSelector("#roles-kendo-grid tr"));
            Assert.That(driver.Url, Is.EqualTo(PageUrl), "The PageUrl and DriverUrl are not equal");
        }


        public void AddRole()
        {
            IClick(LinkAdd);
            _wait.WaitForAjax();
            ISeeElement(RoleModal, By.XPath("//div[@class='k-widget k-window']"));
            ISeeElements(By.CssSelector("#admin-menu-role-edit #available div"));
            IType(NameInputField, "DenisAutomationRoleTest" + randomNumber);
            IClick(AddAllLink);
            ISeeElements(By.CssSelector("#admin-menu-role-edit #assigned div"));
            IClick(SaveButton);
            _wait.WaitForAjax();
            AssertThereIsNoErrorAndException();
        }

        public void DeleteRole()
        {
            IType(SearchArea, "DenisAutomationRoleTest" + randomNumber);
            _wait.IWaitForOneUserInTheGrid();
            IClick(EditButton);
            _wait.WaitForAjax();
            ISeeElement(RoleModal, By.XPath("//div[@class='k-widget k-window']"));
            ISeeElements(By.CssSelector("#admin-menu-role-edit #assigned div"));
            IClick(DeleteButton);
            ISeeElement(ConfirmationDialog, By.CssSelector(".confimation-dialog h5"));
            IClick(YesButton);
            _wait.WaitForAjax();
            AssertThereIsNoErrorAndException();
        }
    }
}
